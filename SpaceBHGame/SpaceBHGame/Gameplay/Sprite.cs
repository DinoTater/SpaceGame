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
    public abstract class Sprite
    {
        internal SpriteBatch _spriteBatch;
        internal GraphicsDevice _graphicsDevice;
        internal ContentManager _content;

        internal Texture2D texture;
        internal Vector2 Position;

        internal int speed;

        public bool Active
        {
            get;
            set;
        }

        public int NumLives
        {
            get;
            set;
        }

        public int Damage
        {
            get;
            set;
        }

        public Sprite(ref SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _graphicsDevice = graphicsDevice;
        }

        public abstract void Initialize(Difficulty difficulty);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
