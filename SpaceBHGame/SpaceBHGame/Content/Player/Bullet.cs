using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceBHGame.Player
{
    public class Bullet
    {
        public Animation _bullet_animation;
        public Texture2D _texture;
        public int _damage;
        public float _move_speed;
        public int _range;
        public bool _is_active;
        public Vector2 _position; // Where the bullet is located at this moment
        public Vector2 _direction; // Where the bullet is headed at this moment

        //Constructor
        public Bullet(Texture2D texture, int speed, Vector2 position, Vector2 direction)
        {
            _texture = texture;
            _move_speed = speed;
            _position = position;
            _direction = direction;
        }

        public Bullet()
        {

        }

        // Initialize the bullet
        //public void Initialize(Animation animation, Texture2D texture, int speed, Vector2 position, Vector2 direction)
        public void Initialize(Animation animation, Vector2 position, int speed)
        {
            _bullet_animation = animation;
            _is_active = true;
           // _texture = texture;
            _move_speed = speed;
            _position = position;
            //_direction = direction;
        }

        // Update
        public virtual void Update(GameTime gameTime)
        {
            _position.Y -= _move_speed;
            _bullet_animation.Position = _position;
            _bullet_animation.Update(gameTime);
        }

        public int Width
        {
            get { return _bullet_animation.FrameWidth; }
        }

        public int Height
        {
            get { return _bullet_animation.FrameHeight; }
        }

        // Draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _bullet_animation.Draw(spriteBatch);
        }
    }
    
    public class PlayerBullet : Bullet
    {
        // Update
        public override void Update(GameTime gameTime)
        {
            _position.Y -= _move_speed;
            _bullet_animation.Position = _position;
            _bullet_animation.Update(gameTime);
        }
    }

    public class EnemyBullet : Bullet
    {
        // Update
        public override void Update(GameTime gameTime)
        {
            _position.Y += _move_speed;
            _bullet_animation.Position = _position;
            _bullet_animation.Update(gameTime);
        }
    }
    
    class Ball : PlayerBullet
    {
        // Constructor
        public Ball(Texture2D texture, int speed, Vector2 position, Vector2 direction) 
        {
            _damage = 1;
        }

        // Draw function
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationRectangle: new Rectangle((int)_position.X - 6, (int)_position.Y - 10, 12, 20));
            spriteBatch.End();
        }
    }

    class Diamond : EnemyBullet
    {
        // Enemy diamond bullet, three level increasing, goes in the direction of the player
        public Diamond(Texture2D texture, int speed, Vector2 position, Vector2 direction)
        {
            _damage = 1;
        }

        // Draw function
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationRectangle: new Rectangle((int)_position.X - 6, (int)_position.Y - 10, 12, 20));
            spriteBatch.End();
        }
    }

    class BigBall : EnemyBullet
    {
        // Enemy ball bullet, aims in the direction of the player
        // Constructor
        public BigBall(Texture2D texture, int speed, Vector2 position, Vector2 direction) 
        {
            _damage = 2;
        }

        // Draw function
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationRectangle: new Rectangle((int)_position.X - 6, (int)_position.Y - 10, 12, 20));
            spriteBatch.End();
        }
    }
}