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
    internal class Gameplay
    {
        #region UI Stuff
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;

        SpriteBatch _spriteBatch;
        GraphicsDevice _graphicsDevice;
        ContentManager _content;
        Resolution _resolution;

        SpriteFont font;

        Vector2 scorePosition;
        Vector2 highscorePosition;

        Texture2D right_panel;
        List<Texture2D> panels;

        bool debugMode = false;
        Difficulty _difficulty;

        private int right_boundary; // Default resolution = 1920x1080
        readonly double resolution_factor = 0.3125;
        readonly double right_panel_height_stretch_factor = 1.07946027;
        readonly double right_panel_width_factor = 0.76171875;

        readonly double right_panel_height_factor_hs = 0.12222222222222222222222222222222;
        readonly double right_panel_height_factor_s  = 0.24027777777777777777777777777777;
        readonly double right_panel_height_factor_p  = 0.35833333333333333333333333333333;
        readonly double right_panel_height_factor_sh = 0.47638888888888888888888888888888;
        readonly double right_panel_height_factor_b  = 0.59444444444444444444444444444444;

        Texture2D background1;
        Texture2D background2;
        Texture2D background3;

        Rectangle backgroundPosition1;
        Rectangle backgroundPosition2;
        Rectangle backgroundPosition3;

        int background_speed = 2;

        #endregion
        #region Player and Enemy Stff
        Player player;
        Boss boss;

        List<Enemy> enemies;
        TimeSpan enemySpawnTime;
        TimeSpan previousEnemySpawnTime;

        Texture2D bulletTexture;
        TimeSpan bulletSpawnTime;
        TimeSpan previousBulletSpawnTime;
        List<Bullet> bullets;

        Random random;
        #endregion

        public bool GameWin
        {
            get;
            set;
        }
        
        public bool BossLive
        {
            get;
            set;
        }

        long score = 0;
        long _highscore;

        public Gameplay(ref SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content,
            Difficulty difficulty, Resolution resolution, long highscore,
            bool debug = false)
        {
            #region UI Stuff
            _spriteBatch = spriteBatch;
            _content = content;
            _graphicsDevice = graphicsDevice;
            _difficulty = difficulty;
            _resolution = resolution;

            _highscore = highscore;

            debugMode = false;
            GameWin = false;
            BossLive = false;
            #endregion
        }

        public void Initialize()
        {
            #region UI Stuff
            // Load what you need
            background1 = _content.Load<Texture2D>("Backgrounds\\GameplayBackground");
            background2 = _content.Load<Texture2D>("Backgrounds\\GameplayBackground");
            background3 = _content.Load<Texture2D>("Backgrounds\\GameplayBackground");

            backgroundPosition1 = new Rectangle(0, 0, GetWindowWidth() - right_boundary, GetWindowHeight());
            backgroundPosition2 = new Rectangle(0, -GetWindowHeight(), GetWindowWidth() - right_boundary, GetWindowHeight());
            backgroundPosition3 = new Rectangle(0, -2 * GetWindowHeight(), GetWindowWidth() - right_boundary, GetWindowHeight());

            if (_difficulty == Difficulty.Easy)
            {

            }
            else if (_difficulty == Difficulty.Medium)
            {

            }
            else if (_difficulty == Difficulty.Hard)
            {

            }
            else if (_difficulty == Difficulty.Insane)
            {

            }
            else
            {

            }

            // TODO: Add your initialization logic here
            font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreHD");

            panels = new List<Texture2D>();

            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 0"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 1"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 2"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 3"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 4"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 5"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 6"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 7"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 8"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel 9"));
            panels.Add(_content.Load<Texture2D>(@"Gameplay Components\\Dashboard Components\\Right Panel\\Right Panel Max"));
            #endregion
            // Initialize the player class
            player = new Player(ref _spriteBatch, _graphicsDevice, _content, _graphicsDevice.PresentationParameters.BackBufferWidth - right_boundary, GetPlayerPosition());
            player.Initialize(_difficulty);

            boss = new Boss(ref _spriteBatch, _graphicsDevice, _content, 1, 1, _graphicsDevice.PresentationParameters.BackBufferWidth - right_boundary);
            boss.Initialize(_difficulty, player);

            //initialize the enemies list
            enemies = new List<Enemy>();
            previousEnemySpawnTime = TimeSpan.Zero;
            enemySpawnTime = TimeSpan.FromSeconds(1.0);
            
            bulletTexture = _content.Load<Texture2D>("GamePlay Components\\Plasmaball");
            bullets = new List<Bullet>();
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;

            bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            previousBulletSpawnTime = TimeSpan.Zero;

            random = new Random();
        }

        public void Kill()
        {
            // Cleanup code
            // set all variables to null
        }

        public GameState Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            #region UI Stuff

            if (backgroundPosition1.Y > GetWindowHeight())
            {
                backgroundPosition1.Y = -2 * GetWindowHeight();
            }

            if (backgroundPosition2.Y > GetWindowHeight())
            {
                backgroundPosition2.Y = -2 * GetWindowHeight();
            }

            if (backgroundPosition3.Y > GetWindowHeight())
            {
                backgroundPosition3.Y = -2 * GetWindowHeight();
            }

            backgroundPosition1.Y += background_speed;
            backgroundPosition2.Y += background_speed;
            backgroundPosition3.Y += background_speed;

            scorePosition = GetScorePosition();
            highscorePosition = GetHighScorePosition();

            if (keyboardState.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                return GameState.Pause;
            }
            if (keyboardState.IsKeyDown(Keys.X) && !previousKeyboardState.IsKeyDown(Keys.X)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                GameWin = false;
                return GameState.EndOfGame;
            }
            if (keyboardState.IsKeyDown(Keys.Z) && !previousKeyboardState.IsKeyDown(Keys.Z)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                GameWin = true;
                return GameState.EndOfGame;
            }
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                return GameState.Exit;
            }
            GetRightBoundaryWidth();
            #endregion
            // Respond to user actions in the game.
            DebugFunct();

            // ITEM HIT CHECK GOES HERE

            // TODO: Power Up logic

            // Update enemies
            // Handle collisions
            // if (true)//playerDied)
            //  _state = GameState.EndOfGame;

            player.Update(gameTime); // update player
            UpdateEnemies(gameTime);
            boss.Update(gameTime);

            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            return GameState.Gameplay;
        }
        
        public void HandleCollisions()
        {
            Rectangle playerRectangle;
            Rectangle enemyRectangle;
            Rectangle bulletRectangle;

            playerRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Width, player.Height);

            for(var i = 0; i < enemies.Count; i++)
            {
                enemyRectangle = new Rectangle((int)enemies[i].Position.X, (int)enemies[i].Position.Y, enemies[i].Width, enemies[i].Height);

                if (playerRectangle.Intersects(enemyRectangle))
                {
                    player.NumLives -= enemies[i].Damage;

                    if(player.NumLives <= 0)
                    {
                        player.Active = false;
                    }
                }

                for(var b = 0; b < bullets.Count; b++)
                {
                    bulletRectangle = new Rectangle((int)bullets[b]._position.X, (int)bullets[b]._position.Y, bullets[i].Width, bullets[i].Height);

                    if(bulletRectangle.Intersects(enemyRectangle))
                    {
                        enemies[i].NumLives = 0;

                        bullets[b]._is_active = false;
                    }
                }

            }
        }

        public GameState Draw(ref GameState currentState)
        {
            #region UI Stuff
            _spriteBatch.Begin();

            GetFont();
            GetPanel();

            _spriteBatch.Draw(background1, backgroundPosition1,
                Microsoft.Xna.Framework.Color.White);

            _spriteBatch.Draw(background2, backgroundPosition2,
                Microsoft.Xna.Framework.Color.White);

            _spriteBatch.Draw(background3, backgroundPosition3,
                Microsoft.Xna.Framework.Color.White);

            _spriteBatch.Draw(right_panel,
                new Rectangle(_graphicsDevice.PresentationParameters.BackBufferWidth - right_boundary,
                    0, right_boundary,
                    _graphicsDevice.PresentationParameters.BackBufferHeight),
                Microsoft.Xna.Framework.Color.White);

            if (score < _highscore)
            {
                _spriteBatch.DrawString(font, Convert.ToString(_highscore), highscorePosition, Microsoft.Xna.Framework.Color.White);
                _spriteBatch.DrawString(font, Convert.ToString(score), scorePosition, Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                _spriteBatch.DrawString(font, Convert.ToString(score), highscorePosition, Microsoft.Xna.Framework.Color.White);
                _spriteBatch.DrawString(font, Convert.ToString(score), scorePosition, Microsoft.Xna.Framework.Color.White);
            }

            _spriteBatch.End();
            #endregion

            // Draw enemies
            boss.Draw(_spriteBatch);
            // Draw the player
            player.Draw(_spriteBatch);

            // Draw particle effects, etc
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Draw(_spriteBatch);

            return GameState.Gameplay;
        }

        #region GAMEPLAY MECHANICS

        private void AddEnemy()
        {
            Enemy enemy = new Enemy(ref _spriteBatch, _graphicsDevice, _content, 1, 1, _graphicsDevice.PresentationParameters.BackBufferWidth - right_boundary);
            enemy.Initialize(_difficulty);
            enemies.Add(enemy);
        }
        private void UpdateEnemies(GameTime gameTime)
        {

            if (gameTime.TotalGameTime - previousEnemySpawnTime > enemySpawnTime)
            {
                previousEnemySpawnTime = gameTime.TotalGameTime;
                AddEnemy();
            }
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (enemies[i].Active == false)
                {
                    enemies.RemoveAt(i);
                }
            }

            foreach (Enemy e in enemies)
            {
                e.enemyFire(gameTime, player.Position);
            }
        }
        #endregion

        #region UI Stuff

        private int GetWindowWidth()
        {
            if (_resolution == Resolution.Standard)
            {
                return 960;
            }
            else if (_resolution == Resolution.HD)
            {
                return 1280;
            }
            else if (_resolution == Resolution.HDPlus)
            {
                return 1600;
            }
            else if (_resolution == Resolution.FullHD)
            {
                return 1920;
            }
            else if (_resolution == Resolution.FourK)
            {
                return 4096;
            }
            else if (_resolution == Resolution.FullScreen)
            {
                return 1920;
            }
            else
            {
                return 1280;
            }
        }

        private int GetWindowHeight()
        {
            if (_resolution == Resolution.Standard)
            {
                return 540;
            }
            else if (_resolution == Resolution.HD)
            {
                return 720;
            }
            else if (_resolution == Resolution.HDPlus)
            {
                return 900;
            }
            else if (_resolution == Resolution.FullHD)
            {
                return 1080;
            }
            else if (_resolution == Resolution.FourK)
            {
                return 2304;
            }
            else if (_resolution == Resolution.FullScreen)
            {
                return 720;
            }
            else
            {
                return 1280;
            }
        }

        private void GetRightBoundaryWidth()
        {
            right_boundary = (int)(resolution_factor * GetWindowWidth());
        }

        private Vector2 GetHighScorePosition()
        {
            int x, y;
            x = (int)(right_panel_width_factor * GetWindowWidth());
            y = (int)(right_panel_height_factor_hs * GetWindowHeight() * right_panel_height_stretch_factor);
            return new Vector2(x, y);
        }

        private Vector2 GetScorePosition()
        {
            int x, y;
            x = (int)(right_panel_width_factor * GetWindowWidth());
            y = (int)(right_panel_height_factor_s * GetWindowHeight() * right_panel_height_stretch_factor);
            return new Vector2(x, y);
        }

        private Vector2 GetPlayerPosition()
        {
            int x, y;
            x = (int)((GetWindowWidth() - right_boundary) / 2);
            y = (int)(GetWindowHeight() - (right_panel_height_stretch_factor * 120));
            return new Vector2(x, y);
        }

        private void GetFont()
        {
            if (_resolution == Resolution.Standard)
            {
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreStandard");
            }
            else if (_resolution == Resolution.HD)
            {
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreHD");
            }
            else if (_resolution == Resolution.HDPlus)
            {
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreHDPlus");
            }
            else if (_resolution == Resolution.FullHD)
            {
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreFullHD");
            }
            else if (_resolution == Resolution.FourK)
            {
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreFourK");
            }
            else if (_resolution == Resolution.FullScreen)
            {
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreFullHD");
            }
            else { }
        }

        private void GetPanel()
        {
            right_panel = panels[player.PowerLevel];
        }
        #endregion

        #region DEBUG FUNCTIONS
        /* 
         * Press E to spawn enemy
         * Press L to increase power level
         * Press K to use OVERLOAD!
         * Press Space bar to shoot
         * Press M to lose lives
         * Press U to score 5000 points
         * Press Y to reset score
         * Press B to get bomb
         * Press V to use bomb
         * Press T to get shield
         * Press R to use shield
         * Press X to skip to Win screen
         * Press Z to skip to Lose screen
         */
        public void DebugFunct()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.L) && !previousKeyboardState.IsKeyDown(Keys.L)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                if (player.PowerLevel == 10)
                    return;
                else
                    player.PowerLevel++;
            }
            else if (keyboardState.IsKeyDown(Keys.K) && !previousKeyboardState.IsKeyDown(Keys.K)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                player.PowerLevel = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.U) && !previousKeyboardState.IsKeyDown(Keys.U)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                score += 5000;
            }
            else if (keyboardState.IsKeyDown(Keys.Y) && !previousKeyboardState.IsKeyDown(Keys.Y)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                score = 0;
            }
            else { }

            previousKeyboardState = keyboardState;
        }
        #endregion
    }
}
