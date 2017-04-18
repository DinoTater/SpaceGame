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
    public class Player : Sprite
    {
        public Animation PlayerAnimation; // RR
        
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;
        public List<Bullet> bullets = new List<Bullet>();
        Texture2D bulletTexture;

        int _playFieldWidth;

        public int PowerLevel
        {
            get;
            set;
        }
        // RR
        public int Width { get { return PlayerAnimation.FrameWidth; } }
        // RR
        public int Height { get { return PlayerAnimation.FrameHeight; } }

        public Player(ref SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content, int playFieldWidth, Vector2 startingPosition)
            : base(ref spriteBatch, graphicsDevice, content)
        {
            previousKeyboardState = Keyboard.GetState();
            Position = startingPosition;
            _playFieldWidth = playFieldWidth;
            speed = 4;

            Active = true;
            PowerLevel = 0;
        }

        public override void Initialize(Difficulty difficulty)
        {
            if (difficulty == Difficulty.Easy)
            {
                NumLives = 20;
            }
            else if (difficulty == Difficulty.Medium)
            {
                NumLives = 10;
            }
            else if (difficulty == Difficulty.Hard)
            {
                NumLives = 5;
            }
            else if (difficulty == Difficulty.Insane)
            {
                NumLives = 1;
            }
            else
            {
                NumLives = 20;
            }
            Damage = 5;
            texture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Position.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Position.X += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Position.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Position.Y += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                //FireBullet(Position);
                //UpgradedBullet(Position);
                //SpiralFire(Position);
                //ArcFire(Position);
                //RandomFire(Position);
                //LaserFire(Position);
                Explosion(Position);
            }

            foreach (Bullet b in bullets)
            {
                b.Update(gameTime);
            }

            previousKeyboardState = keyboardState;
        }
        
        public void hit()
        {
            NumLives--;
            PowerLevel = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Make sure the player does not go out of bounds
            Position.X = MathHelper.Clamp(Position.X, 0, _playFieldWidth - texture.Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, _graphicsDevice.Viewport.Height - texture.Height);

            _spriteBatch.Begin();

            _spriteBatch.Draw(texture, Position,
                Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();

            foreach(Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }

        public void FireBullet(Vector2 position)
        {
            Vector2 vL = new Vector2(0, 15);
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Bullet leftBullet = new Ball(bulletTexture, 2, position, vL);
            bullets.Add(leftBullet);

            Vector2 right = new Vector2();
            right.X += (position.X + 50);
            right.Y += (position.Y);
            Vector2 vR = new Vector2(0, 15);
            Bullet rightBullet = new Ball(bulletTexture, 2, right, vR);
            bullets.Add(rightBullet);
        }

        public void UpgradedBullet(Vector2 position)
        {
            Vector2 temp = Position;
            temp.X += 25;
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");

            Vector2 v = new Vector2(0, 20);
            Bullet firstBullet = new BallSpread(bulletTexture, 2, temp, v);

            v.X = 5;
            temp.X += 20;
            Bullet secondBullet = new BallSpread(bulletTexture, 2, temp, v);


            v.X *= -1;
            temp.X -= 40;
            Bullet thirdBullet = new BallSpread(bulletTexture, 2, temp, v);

            bullets.Add(firstBullet);
            bullets.Add(secondBullet);
            bullets.Add(thirdBullet);

        }
        
        public void SpiralFire(Vector2 position)
        {
            // Shoot bullets in a radiating circle from big boss man enemy
            // Calculate the new location using slightly increased angle and trigonometry
            double angle = 0.0;
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            Vector2 temp2;
            temp.X += 27;
            temp.Y -= 20;
            temp2 = temp;


            for (int i = 0; i < 30; i++)
            {
                angle = (angle + 12) * (double)Math.PI / 180;
                double newX = Math.Cos(angle) * 2;
                double newY = Math.Sin(angle) * 2;
                temp.X = (float)Math.Cos(angle) * 5 + temp.X;
                temp.Y = temp.Y + (float)Math.Sin(angle) * 5;
                Vector2 v = new Vector2((float)newX, (float)newY);
                EnemyBullet newBullet = new Diamond(bulletTexture, 2, temp, v);
                bullets.Add(newBullet);
                angle = angle * 180 / Math.PI;
            }
        }

        public void ArcFire(Vector2 position)
        {
            double angle = 170.0;
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            Vector2 temp2;
            temp.X += 20;
            temp.Y -= 20;
            temp2 = temp;


            for (int i = 0; i < 7; i++)
            {
                angle = (angle - 20) * (double)Math.PI / 180;
                temp2.X = (float)Math.Cos(angle) * 30 + temp.X;
                temp2.Y = (float)Math.Sin(angle) * 30 + temp.Y;

                double newX = -Math.Cos(angle);
                double newY = Math.Sin(angle);
                Vector2 v = new Vector2((float)newX, (float)newY);

                EnemyBullet newBullet = new Diamond(bulletTexture, 2, temp2, v);
                bullets.Add(newBullet);
                angle = angle * 180 / Math.PI;
            }
        }

        public void LaserFire(Vector2 position)
        {
            double angle = 0.0;
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            Vector2 temp2;
            temp.X += 20;
            temp.Y -= 20;
            temp2 = temp;

            for (int i = 0; i < 6; i++)
            {
                double angle2 = angle + 45;
                angle = (angle - 45) * (double)Math.PI / 180;
                for (int j = 10; j < 20; j++)
                {
                    temp2.X = (float)Math.Cos(angle) * 30 + temp.X;
                    temp2.Y = (float)Math.Sin(angle) * 30 + temp.Y;

                    double newX = -Math.Cos(angle2);
                    double newY = -Math.Sin(angle2);
                    Vector2 v = new Vector2((float)newX, (float)newY);

                    EnemyBullet newBullet = new Diamond(bulletTexture, (float)0.3 * j, temp2, v);
                    bullets.Add(newBullet);
                }

                angle = angle * 180 / Math.PI;
            }
        }

        public void Explosion(Vector2 position)
        {
            double angle = 0.0;
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");
            Vector2 temp = Position;
            Vector2 temp2;
            temp.X += 20;
            temp.Y -= 20;
            temp2 = temp;


            for (int i = 0; i < 8; i++)
            {
                angle = (angle - 45) * (double)Math.PI / 180;

                for (int j = 10; j < 30; j++)
                {
                    temp2.X = (float)Math.Cos(angle) * 30 + temp.X;
                    temp2.Y = (float)Math.Sin(angle) * 30 + temp.Y;

                    double newX = Math.Cos(angle);
                    double newY = Math.Sin(angle);
                    Vector2 v = new Vector2((float)newX, (float)newY);

                    EnemyBullet newBullet = new Diamond(bulletTexture, (float)0.2 * j, temp2, v);
                    bullets.Add(newBullet);
                }

                angle = angle * 180 / Math.PI;
            }
        }

        public void RandomFire(Vector2 Position)
        {
            Vector2 temp = Position;
            Vector2 v;
            Random rnd = new Random();
            bulletTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship");

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

                    PlayerBullet newBullet = new Ball(bulletTexture, 3, temp, v);
                    bullets.Add(newBullet);
                }
            }
        }

        public void despawnBullet(Bullet b)
        {
            bullets.Remove(b);
        }
    }
}
