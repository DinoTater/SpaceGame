using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceBHGame.Player
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
        Texture2D enemyTexture;
        List<Enemy> enemies;
        TimeSpan enemySpawnTime;
        TimeSpan previousEnemySpawnTime;
        Random random;
        int numLives;
        int power_level = 0;
        int player_speed = 4;
        Texture2D bulletTexture;
        TimeSpan bulletSpawnTime;
        TimeSpan previousBulletSpawnTime;
        List<Bullet> bullets;
        Texture2D enemyBulletTexture;
        TimeSpan enemyBulletSpawnTime;
        TimeSpan enemyPreviousBulletSpawnTime;
        List<EnemyBullet> enemyBullets;
        #endregion
        bool gameWin;

        public bool GameWin
        {
            get { return gameWin; }
        }

        bool bossLive;
        public bool BossLive
        {
            get { return bossLive; }
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
            player = new Player();
            player_speed = 4;
            //initialize the enemies list
            enemies = new List<Enemy>();
            previousEnemySpawnTime = TimeSpan.Zero;
            enemySpawnTime = TimeSpan.FromSeconds(1.0);
            random = new Random();
            Vector2 playerPosition = new Vector2((_graphicsDevice.Viewport.TitleSafeArea.Width - (int)(resolution_factor * GetWindowWidth())) / 2, (float)(_graphicsDevice.Viewport.TitleSafeArea.Center.Y * 1.5));
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = _content.Load<Texture2D>("GamePlay Components\\Ship\\Ship1");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 100, 69, 8, 30, Color.White, 1f, true);
            enemyTexture = _content.Load<Texture2D>("Gameplay Components\\Ship\\Ship1");
            player.Initialize(playerAnimation, playerPosition);
            bulletTexture = _content.Load<Texture2D>("GamePlay Components\\Ship\\Ship1");
            bullets = new List<Bullet>();
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;
            const float ENEMY_RATE_OF_FIRE = 100f;
            bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            previousBulletSpawnTime = TimeSpan.Zero;
            enemyBulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / ENEMY_RATE_OF_FIRE);
            enemyPreviousBulletSpawnTime = TimeSpan.Zero;
            enemyBullets = new List<EnemyBullet>();
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
                gameWin = false;
                return GameState.EndOfGame;
            }
            if (keyboardState.IsKeyDown(Keys.Z) && !previousKeyboardState.IsKeyDown(Keys.Z)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                gameWin = true;
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


            // update bullets, checking for 'hit enemy' and offscreen
            for (var i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);
                // Remove the bullet when its deactivated or is at the end of the screen.
                if (!bullets[i]._is_active || bullets[i]._position.Y > _graphicsDevice.Viewport.Height || bullets[i]._position.X > _graphicsDevice.Viewport.Width || bullets[i]._position.X < 0)
                {
                    bullets.Remove(bullets[i]);
                }
                //else
                //{
                //    bullets[i]._position.Y -= bullets[i]._move_speed;
                //}
            }
            for (var i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].Update(gameTime);
                // Remove the beam when its deactivated or is at the end of the screen.
                if (!enemyBullets[i]._is_active || enemyBullets[i]._position.Y < _graphicsDevice.Viewport.Height || enemyBullets[i]._position.X > _graphicsDevice.Viewport.Width || enemyBullets[i]._position.X < 0)
                {
                    enemyBullets.Remove(enemyBullets[i]);
                }
                //else
                //{
                //    enemyBullets[i]._position.Y += enemyBullets[i]._move_speed;
                //}
            }

            // TODO: Power Up logic59

            // Update enemies
            // Handle collisions
            // if (true)//playerDied)
            //  _state = GameState.EndOfGame;
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            UpdatePlayer(gameTime);
            UpdateEnemies(gameTime);
            return GameState.Gameplay;
        }
        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                player.Position.X -= player_speed;
            }
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                player.Position.X += player_speed;
            }
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                player.Position.Y -= player_speed;
            }
            if(keyboardState.IsKeyDown(Keys.Down))
            {
                player.Position.Y += player_speed;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                FireBullet(gameTime);
                EnemyFireBullet(gameTime);
            }
        }

        protected void FireBullet(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime)
            {
                previousBulletSpawnTime = gameTime.TotalGameTime;
                AddBullet();
            }
        }

        protected void AddBullet()
        {
            Animation bulletAnimation = new Animation();
            bulletAnimation.Initialize(bulletTexture, player.Position, 20, 20, 1, 30, Color.OrangeRed, 1f, true);
            Bullet bullet = new Bullet();
            var bulletPosition = player.Position;
            bullet.Initialize(bulletAnimation, bulletPosition, 25);
            bullets.Add(bullet);
        }
        
        // TODO: bullets for enemies
        protected void EnemyFireBullet(GameTime gameTime)
        {
            // TODO: Adjust so each enemy can fire at different times
            if (gameTime.TotalGameTime - enemyPreviousBulletSpawnTime > enemyBulletSpawnTime)
            {
                enemyPreviousBulletSpawnTime = gameTime.TotalGameTime;
                foreach (Enemy e in enemies)
                {
                    AddEnemyBullet(e);
                }
            }
        }

        protected void AddEnemyBullet(Enemy enemy)
        {
             Animation enemyBulletAnimation = new Animation();
            enemyBulletAnimation.Initialize(enemyTexture, enemy.Position, 20, 20, 1, 30, Color.White, 1f, true);
            EnemyBullet enemyBullet = new EnemyBullet();
            var enemyBulletPosition = enemy.Position;
            enemyBullet.Initialize(enemyBulletAnimation, enemyBulletPosition, 25);
            enemyBullets.Add(enemyBullet);
        }
        
        private void UpdateCollisions()
        {
            Rectangle bulletRectangle;

            // detect collisions between the player and all enemies.
            enemies.ForEach(e =>
            {
                //create a rectangle for the enemy
                Rectangle enemyRectangle = new Rectangle(
                    (int)e.Position.X,
                    (int)e.Position.Y,
                    e.Width,
                    e.Height);

                // now see if this enemy collide with any bullet shots
                bullets.ForEach(bull =>
                {
                    // create a rectangle for this bullet
                    bulletRectangle = new Rectangle(
                    (int)bull._position.X,
                    (int)bull._position.Y,
                    bull.Width,
                    bull.Height);

                    // test the bounds of the bullet and enemy
                    if (bulletRectangle.Intersects(enemyRectangle))
                    {
                        // UI stuff here for explosion/kill enemy/pts/etc


                        // kill off the bullet
                        bull._is_active = false;
                    }
                });
            });
        }

        private void AddEnemy()
        {
            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.Yellow, 1f, true);
            Vector2 position = new Vector2((_graphicsDevice.Viewport.TitleSafeArea.Width - (int)(resolution_factor * GetWindowWidth())) - 20, 80);
            //Vector2 position = new Vector2(_graphicsDevice.Viewport.Width - enemyTexture.Width / 2);
            //random.Next(100, _graphicsDevice.Viewport.Height - 100);
            Enemy enemy = new Enemy();
            enemy.Initialize(enemyAnimation, position);
            enemies.Add(enemy);
        }
        private void UpdateEnemies(GameTime gameTime)
        {
            if(gameTime.TotalGameTime - previousEnemySpawnTime > enemySpawnTime)
            {
                previousEnemySpawnTime = gameTime.TotalGameTime;
                AddEnemy();
            }
            for(int i = enemies.Count -1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if(enemies[i].Active == false)
                {
                    enemies.RemoveAt(i);
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

            _spriteBatch.Begin();
            // Draw enemies
            // Draw the player
            player.Draw(_spriteBatch);

            // Draw the bullets.
            foreach (var b in bullets)
            {
                b.Draw(_spriteBatch);
            }

            //Make sure the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, _graphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, _graphicsDevice.Viewport.Height - player.Height);
            // Draw particle effects, etc
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(_spriteBatch);
            }

            foreach (var eb in enemyBullets)
            {
                 eb.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            
            return GameState.Gameplay;
        }

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
            right_panel = panels[power_level];
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
                if (power_level == 10)
                    return;
                else
                    power_level++;
            }
            else if (keyboardState.IsKeyDown(Keys.K) && !previousKeyboardState.IsKeyDown(Keys.K)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                power_level = 0;
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