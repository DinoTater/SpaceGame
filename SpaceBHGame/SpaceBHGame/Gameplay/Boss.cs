using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBHGame.Gameplay
{
    class Boss : Sprite
    {
        public int Value;
        int _playFieldWidth;
        public List<EnemyBullet> bossBullets = new List<EnemyBullet>();
        Texture2D bossBulletTexture;
        float teleportTimer = 0;
        const float TELETIMER = 3000;
        public Player refToPlayer;
        public bool isBoss;
        public Random _rand = new Random();
        Vector2 returnPosition;
        public int health;

        const float SECONDS_IN_MINUTE = 60f;
        const float CIRCLE_RATE_OF_FIRE = 30f;
        const float ARC_RATE_OF_FIRE = 11f;
        const float RANDOM_RATE_OF_FIRE = 17f;
        const float LASER_RATE_OF_FIRE = 20f;
        // circle arc random laser
        TimeSpan circleSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / CIRCLE_RATE_OF_FIRE);
        TimeSpan previousCircleSpawnTime = TimeSpan.Zero;
        TimeSpan arcSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / ARC_RATE_OF_FIRE);
        TimeSpan previousArcSpawnTime = TimeSpan.Zero;
        TimeSpan randomSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RANDOM_RATE_OF_FIRE);
        TimeSpan previousRandomSpawnTime = TimeSpan.Zero;
        TimeSpan laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / LASER_RATE_OF_FIRE);
        TimeSpan previousLaserSpawnTime = TimeSpan.Zero;

        public Boss(ref SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, double x_scale_factor, double y_scale_factor, int playFieldWidth) : base(ref spriteBatch, graphicsDevice, content)
        {
            _playFieldWidth = playFieldWidth;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, _playFieldWidth - texture.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, _graphicsDevice.Viewport.Height - texture.Height);

            _spriteBatch.Begin();

            _spriteBatch.Draw(texture, Position, Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();

            foreach (Bullet b in bossBullets)
            {
                b.Draw(spriteBatch);
            }
        }

        public void Initialize(Difficulty difficulty, Player p)
        {
            isBoss = true;
            refToPlayer = p;
            texture = _content.Load<Texture2D>("GamePlay Components\\Enemies\\Laser Ship\\LaserShip1");
            Position = new Vector2(275, 150);
            speed = _rand.Next(1, 4);
            health = 100;

            Active = true;

            if (difficulty == Difficulty.Easy)
            {
                NumLives = 50;
            }
            else if(difficulty == Difficulty.Medium)
            {
                NumLives = 100;
            }
            else if(difficulty == Difficulty.Hard)
            {
                NumLives = 150;
            }
            else if(difficulty == Difficulty.Insane)
            {
                NumLives = 200;
            }
            else
            {
                NumLives = 50;
            }
            Damage = 1;
            speed = 2;
            Value = 5000;
        }

        public override void Update(GameTime gameTime)
        {
            if(teleportTimer != 0)
            {
                teleportTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if(teleportTimer <= 0)
                {
                    teleportTimer = 0;
                    Teleport(2);
                }
            }
            if(health <= 75)
            {
                if(gameTime.TotalGameTime.Seconds % 9 == 0 && gameTime.TotalGameTime.Milliseconds % 1000 == 0 && teleportTimer == 0)
                {
                    Teleport(1);
                }
            }
            else if(health <= 40)
            {
                if (gameTime.TotalGameTime.Seconds % 8 == 0 && gameTime.TotalGameTime.Milliseconds % 1000 == 0 && teleportTimer == 0)
                {
                    Teleport(1);
                }
            }

            bossFire(gameTime);

            foreach (EnemyBullet b in bossBullets)
            {
                b.Update(gameTime);
            }

        }

        public void bossFire(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds < 98)
            {
                if (gameTime.TotalGameTime - previousArcSpawnTime > arcSpawnTime)
                {
                    previousArcSpawnTime = gameTime.TotalGameTime;

                    ArcFire(Position);

                }
            }
            if ((gameTime.TotalGameTime.TotalSeconds < 98 && gameTime.TotalGameTime.TotalSeconds < 135) || (gameTime.TotalGameTime.TotalSeconds > 145 && gameTime.TotalGameTime.TotalSeconds < 225))
            {
                if (gameTime.TotalGameTime - previousCircleSpawnTime > circleSpawnTime)
                {
                    previousCircleSpawnTime = gameTime.TotalGameTime;

                    CircleFire(Position);

                }
            }
            if (gameTime.TotalGameTime.TotalSeconds > 235)
            {
                if (gameTime.TotalGameTime - previousLaserSpawnTime > laserSpawnTime)
                {
                    previousLaserSpawnTime = gameTime.TotalGameTime;

                    LaserFire(Position);

                }

                if (gameTime.TotalGameTime - previousRandomSpawnTime > randomSpawnTime)
                {
                    previousRandomSpawnTime = gameTime.TotalGameTime;

                    RandomFire(Position);

                }
            }
        }

        public override void Initialize(Difficulty difficulty)
        {
            throw new NotImplementedException();
        }

        public void Teleport(int teleportType)
        {
            if(teleportType == 1)
            {
                returnPosition.X = Position.X;
                returnPosition.Y = Position.Y;
                teleportTimer = TELETIMER;
                Position.Y = refToPlayer.Position.Y;
                if(health <= 40)
                {
                    if(!(refToPlayer.Position.X <= 150))
                    {
                        Position.X = refToPlayer.Position.X - 110;
                    }
                    else
                    {
                        Position.X = refToPlayer.Position.X + 110;
                    }
                }
                else
                {
                    if (!(refToPlayer.Position.X <= 250))                 
                    {
                        Position.X = refToPlayer.Position.X - 200;          
                    }
                    else                                                          
                    {
                        Position.X = refToPlayer.Position.X + 200;          
                    }
                }
            }
            else
            {
                Position = returnPosition;
            }
        }



        public void ArcFire(Vector2 position)
        {
            double angle = 170.0;
            bossBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            temp.X += 20;
            temp.Y += 100;
            Vector2 temp2 = temp;


            for (int i = 0; i < 7; i++)
            {
                angle = (angle - 20) * (double)Math.PI / 180;
                temp2.X = (float)Math.Cos(angle) * 30 + temp.X;
                temp2.Y = (float)Math.Sin(angle) * 30 + temp.Y;

                double newX = -Math.Cos(angle);
                double newY = -Math.Sin(angle);
                Vector2 v = new Vector2((float)newX, (float)newY);

                EnemyBullet newBullet = new Diamond(bossBulletTexture, 2, temp2, v);
                bossBullets.Add(newBullet);
                angle = angle * 180 / Math.PI;
            }
        }

        public void CircleFire(Vector2 position)
        {
            // Shoot bullets in a radiating circle from big boss man enemy
            // Calculate the new location using slightly increased angle and trigonometry
            double angle = 0.0;
            bossBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            Vector2 temp2;
            temp.Y -= 20;
            temp2 = temp;


            for (int i = 0; i < 60; i++)
            {
                angle = (angle + 6) * (double)Math.PI / 180;
                temp2.X = (float)Math.Cos(angle) * 90 + temp.X;
                temp2.Y = (float)Math.Sin(angle) * 90 + temp.Y;

                double newX = -Math.Cos(angle);
                double newY = -Math.Sin(angle);
                Vector2 v = new Vector2((float)newX, (float)newY);

                EnemyBullet newBullet = new Diamond(bossBulletTexture, 6, temp2, v);
                bossBullets.Add(newBullet);
                angle = angle * 180 / Math.PI;
            }
        }

        public void LaserFire(Vector2 position)
        {
            double angle = 0.0;
            bossBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            Vector2 temp2;
            temp.X += 20;
            temp.Y -= 20;
            temp2 = temp;

            for (int i = 0; i < 6; i++)
            {
                double angle2 = angle + 45;
                angle = (angle + 45) * (double)Math.PI / 180;
                for (int j = 10; j < 20; j++)
                {
                    temp2.X = (float)Math.Cos(angle) * 30 + temp.X;
                    temp2.Y = (float)Math.Sin(angle) * 30 + temp.Y;

                    double newX = -Math.Cos(angle2);
                    double newY = -Math.Sin(angle2);
                    Vector2 v = new Vector2((float)newX, (float)newY);

                    EnemyBullet newBullet = new Diamond(bossBulletTexture, (float)0.1 * j, temp2, v);
                    bossBullets.Add(newBullet);
                }

                angle = angle * 180 / Math.PI;
            }
        }

        public void RandomFire(Vector2 Position)
        {
            Vector2 temp = Position;
            Vector2 v;
            Random rnd = new Random();
            bossBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");

            for (int i = 0; i < 3; i++)
            {
                temp.X += rnd.Next(-100, 100);
                temp.Y += rnd.Next(-100, 100);

                for (int j = 1; j < 51; j++)
                {
                    v.X = rnd.Next(-20, 20) / (float)10.5;
                    v.Y = rnd.Next(-20, 20) / (float)10.5;

                    if (v.X == 0)
                        v.X = 1;
                    if (v.Y == 0)
                        v.Y = -1;

                    EnemyBullet newBullet = new Diamond(bossBulletTexture, 3, temp, v);
                    bossBullets.Add(newBullet);
                }
            }
        }

        public void despawnBullet(EnemyBullet b)
        {
            bossBullets.Remove(b);
        }
    }
}
