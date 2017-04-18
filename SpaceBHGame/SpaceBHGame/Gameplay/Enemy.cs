using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceBHGame.Gameplay
{
    class Enemy : Sprite
    {
        public Animation EnemyAnimation; // RR
        //score enemy gives to player
        public int Value;
        int _playFieldWidth;
        public List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
        Texture2D enemyBulletTexture;
        public Random _rand = new Random();
        public int health;

        const float SECONDS_IN_MINUTE = 60f;
        const float RATE_OF_FIRE = 50f;

        TimeSpan enemyBulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        TimeSpan previousEnemyBulletSpawnTime = TimeSpan.Zero;
        
        // RR
        public int Width
        {
            get
            {
                return EnemyAnimation.FrameWidth;
            }
        }
        
        // RR
        public int Height
        {
            get
            {
                return EnemyAnimation.FrameHeight;
            }
        }

        public Enemy(ref SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, double x_scale_factor, double y_scale_factor, int playFieldWidth)
            : base(ref spriteBatch, graphicsDevice, content)
        {
            _playFieldWidth = playFieldWidth;
        }

        public override void Initialize(Difficulty difficulty)
        {
            texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip1");
            Position = new Vector2(_rand.Next(650, 650), _rand.Next(0, 400));

            Active = true;

            if (difficulty == Difficulty.Easy)
            {
                NumLives = 10;
            }
            else if (difficulty == Difficulty.Medium)
            {
                NumLives = 20;
            }
            else if (difficulty == Difficulty.Hard)
            {
                NumLives = 30;
            }
            else if (difficulty == Difficulty.Insane)
            {
                NumLives = 50;
            }
            else
            {
                NumLives = 10;
            }

            Damage = 1;
            speed = _rand.Next(1,4);
            Value = 5000; // Will change when more enemy types are created; must inherrothis class
        }

        public override void Update(GameTime gameTime)
        {
            
            Position.X -= (float)(speed * 0.8);
            Position.Y -= (float)Math.Sin(gameTime.TotalGameTime.Seconds) * 2;
            #region Ship Animation
            if (gameTime.ElapsedGameTime.Milliseconds % 8 == 0)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip1");

            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 1)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip2");
                //FireBullet(Position);
            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 2)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip3");
                //FireBullet(Position);
            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 3)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip4");
                //FireBullet(Position);
            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 4)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip5");
                //FireBullet(Position);
            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 5)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip6");
                //FireBullet(Position);
            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 6)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip7");
                //FireBullet(Position);
            }
            else if (gameTime.ElapsedGameTime.Milliseconds % 8 == 7)
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip8");
                //FireBullet(Position);
            }
            else
            {
                texture = _content.Load<Texture2D>("Gameplay Components\\Enemies\\Laser Ship\\LaserShip1");
                //FireBullet(Position);
            }
            #endregion

            if (Position.X < 0 || NumLives <= 0)
            {
                Active = false;
            }

            foreach (Bullet b in enemyBullets)
            {
                b.Update(gameTime);
            }
        }

        public void enemyFire(GameTime gameTime, Vector2 player_position)
        {
            if (gameTime.TotalGameTime - previousEnemyBulletSpawnTime > enemyBulletSpawnTime)
            {
                previousEnemyBulletSpawnTime = gameTime.TotalGameTime;
                //FireBullet(Position, player_position);
                //CircleFire(Position);
                //FireTriangle(Position, player_position);
                //ArcFire(Position);
                //RandomFire(Position);
                //LaserFire(Position);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Make sure the enemy doesnt go out of bounds
            Position.X = MathHelper.Clamp(Position.X, 0, _playFieldWidth - texture.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, _graphicsDevice.Viewport.Height - texture.Height);

            _spriteBatch.Begin();

            _spriteBatch.Draw(texture, Position,
                Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();

            foreach(Bullet b in enemyBullets)
            {
                b.Draw(spriteBatch);
            }
        }

        public void FireBullet(Vector2 position, Vector2 player_position)
        {
            Vector2 direction = player_position - position;
            
            enemyBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            EnemyBullet leftBullet = new BigBall(enemyBulletTexture, 10, position, direction);
            enemyBullets.Add(leftBullet);

            Vector2 right = new Vector2();
            right.X += (position.X + 50);
            right.Y += (position.Y);
            EnemyBullet rightBullet = new BigBall(enemyBulletTexture, 10, right, direction);
            enemyBullets.Add(rightBullet);
        }
        
        public void FireTriangle(Vector2 position, Vector2 player_position)
        {
            enemyBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 direction = player_position - position;
            Vector2 temp = direction;
            
            EnemyBullet firstBullet = new BigBall(enemyBulletTexture, 3, position, direction);
            enemyBullets.Add(firstBullet);

            temp.X += 20;
            EnemyBullet secondBullet = new BigBall(enemyBulletTexture, 2.5f, position, temp);
            enemyBullets.Add(secondBullet);
            temp.X -= 40;
            EnemyBullet thirdBullet = new BigBall(enemyBulletTexture, 2.5f, position, temp);
            enemyBullets.Add(thirdBullet);
            temp.X = direction.X;
            EnemyBullet fourthBullet = new BigBall(enemyBulletTexture, 2, position, temp);
            enemyBullets.Add(fourthBullet);
            temp.X += 30;
            EnemyBullet fifthBullet = new BigBall(enemyBulletTexture, 2, position, temp);
            enemyBullets.Add(fifthBullet);
            temp.X -= 60;
            EnemyBullet sixthBullet = new BigBall(enemyBulletTexture, 2, position, temp);
            enemyBullets.Add(sixthBullet);

        }

        public void ArcFire(Vector2 position)
        {
            double angle = 170.0;
            enemyBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
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

                EnemyBullet newBullet = new Diamond(enemyBulletTexture, 2, temp2, v);
                enemyBullets.Add(newBullet);
                angle = angle * 180 / Math.PI;
            }
        }

        public void CircleFire(Vector2 position)
        {
            // Shoot bullets in a radiating circle from big boss man enemy
            // Calculate the new location using slightly increased angle and trigonometry
            double angle = 0.0;
            enemyBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
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

                EnemyBullet newBullet = new Diamond(enemyBulletTexture, 1, temp2, v);
                enemyBullets.Add(newBullet);
                angle = angle * 180 / Math.PI;
            }
        }

        public void LaserFire(Vector2 position)
        {
            double angle = 0.0;
            enemyBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
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
                    
                    EnemyBullet newBullet = new Diamond(enemyBulletTexture, (float)0.1 * j, temp2, v);
                    enemyBullets.Add(newBullet);
                }

                angle = angle * 180 / Math.PI;
            }
        }

        public void RandomFire(Vector2 Position)
        {
            Vector2 temp = Position;
            Vector2 v;
            Random rnd = new Random();
            enemyBulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");

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

                    EnemyBullet newBullet = new Diamond(enemyBulletTexture, 3, temp, v);
                    enemyBullets.Add(newBullet);
                }
            }
        }

        public void despawnBullet(EnemyBullet b)
        {
            enemyBullets.Remove(b);
        }
    }
}
