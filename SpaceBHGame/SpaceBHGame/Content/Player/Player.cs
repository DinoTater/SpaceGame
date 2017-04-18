﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceBHGame.Player
{
    public class Player
    {
        public Animation PlayerAnimation;
        public Vector2 Position;
        public bool Active;
        public int Health;
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }
        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;
            Position = position;
            Active = true;
            Health = 100;
        }
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
    }
}
