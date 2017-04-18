﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SpaceBHGame.Gameplay;

namespace SpaceBHGame.UI_Control
{
    public class UIControl
    {
        #region Member Variables
        public bool newGame = false;

        Texture2D background;
        static GraphicsDevice _graphicsDevice;
        static ContentManager _content;
        static SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;
        
        // Choose Character
        int characterCounter = 1;

        // Main menu
        int menuCounter = 1;
        bool exit;

        // Scoreboard
        int scoreboardCounter = 1;

        // Options
        int optionsCounter = 1;
        int settingsCounter = 1;
        int gameSettingsCounter = 1;
        int audioSettingsCounter = 1;
        int videoSettingsCounter = 1;

        int playerCounter = 1;
        int speedCounter = 1;
        int enemySpeedCounter = 1;

        int musicVolumeCounter = 1;
        int sfxVolumeCounter = 1;

        int brightnessCounter = 1;
        int displayModeCounter = 1;
        int screenResolutionCounter = 1;

        int enemySpeed = 2;

        Resolution resolution = Resolution.HD;

        public Resolution Resolution
        {
            get { return resolution; }
        }

        public bool IsFullscreen
        {
            get;
            set;
        }

        Difficulty difficulty;

        // Select Difficulty
        int diffCounter = 1;

        // Credits
        List<CreditsLine> credits;
        List<CreditsLine> inCredits;

        // Controls
        int controlCounter = 1;
        Texture2D controlsDiagram;

        // Gameplay
        internal Gameplay.Gameplay game;

        SpriteFont font;
        long score = 0;
        long highscore = 100000;
        ScoreboardControl scoreBoardControl;
        bool debugMode;
        bool gameWin = false; // TODO: Incorporate with Gamplay class

        public SoundEffect startUpSoundEffect;
        public SoundEffect gameplaySoundEffect;
        public SoundEffect menuAdvanceSoundEffect;
        public SoundEffect menuSelectSoundEffect;
        public SoundEffect backSoundEffect;
        public SoundEffectInstance backgroundMusic;
        public SoundEffectInstance soundEffect;

        // Pause
        int pauseCounter = 1;

        // End Game
        int endCounter = 1;
        #endregion

        public bool Exit
        {
            get { return exit; }
        }

        public UIControl(ref SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _graphicsDevice = graphicsDevice;
            UpdateAppSettings();

            scoreBoardControl = new ScoreboardControl();
            if (scoreBoardControl.Highscore != 0)
            {
                highscore = scoreBoardControl.Highscore;
            }
            else
            {
                highscore = 100000;
            }
            
            menuAdvanceSoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\SoundEffects\\Menu-Advance");
            menuSelectSoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\SoundEffects\\Menu-Select");
            backSoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\SoundEffects\\Back");
            
            debugMode = false;
    }
        

        #region TITLE METHODS
        public void InitializeTitle()
        {
            startUpSoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\Opening");
            PlayMusic(startUpSoundEffect);
        }

        public void TitleUpdate(ref GameState currentState, bool NewCharacter = false)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                PlaySoundEffect(menuSelectSoundEffect);

                currentState = GameState.MainMenu;
                menuCounter = 1;

                if (Properties.Settings.Default.NewCharacter || NewCharacter)
                    currentState = GameState.ChooseCharacter;
                
            }
            previousKeyboardState = keyboardState;
        }

        public void TitleDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            background = _content.Load<Texture2D>("Backgrounds\\TitleBackground");
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);
            
            _spriteBatch.End();
        }

        #endregion

        #region CHOOSE CHARACTER
        public void InitializeChooseCharacter()
        {
            
        }

        public void ChooseCharacterUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(menuSelectSoundEffect);
                // Respond to user input for menu selections, etc
                if (characterCounter == 1)
                {
                    Properties.Settings.Default.NewCharacter = false;
                    Properties.Settings.Default.Gender = "Female";
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.NewCharacter = false;
                    Properties.Settings.Default.Gender = "male";
                    Properties.Settings.Default.Save();
                }
                currentState = GameState.MainMenu;
                menuCounter = 1;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (characterCounter == 1)
                        characterCounter = 2;
                    else
                        characterCounter--;
                }
                if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (characterCounter == 2)
                        characterCounter = 1;
                    else
                        characterCounter++;
                }
            }
            previousKeyboardState = keyboardState;
        }

        public void ChooseCharacterDraw()
        {
            //MM: load the background?
            _spriteBatch.Begin();

            if (characterCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\ChooseCharacter\\Her");
            }
            else if (characterCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\ChooseCharacter\\Him");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region MAIN MENU METHODS
        public void MainMenuUpdate(ref GameState currentState)
        {
            /* Main Menu */
            keyboardState = Keyboard.GetState();
            
            if (keyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                debugMode = !debugMode;
            }

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                PlaySoundEffect(backSoundEffect);
                currentState = GameState.Title;
            }
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(menuSelectSoundEffect);
                // Respond to user input for menu selections, etc
                if (menuCounter == 1)
                {
                    InitializeDifficultyComponents();
                    currentState = GameState.SelectDifficulty;
                    InitializeDifficultyComponents();
                    KillMainMenuComponents();
                }
                else if (menuCounter == 2)
                {
                    //currentState = GameState.LoadGame;
                    currentState = GameState.MainMenu;
                    // TODO
                }
                else if (menuCounter == 3)
                {
                    currentState = GameState.Options;
                    KillMainMenuComponents();
                    optionsCounter = 1;
                }
                else if (menuCounter == 4)
                {
                    //currentState = GameState.LoadGame;
                    currentState = GameState.MainMenu;
                    // TODO
                }
                else if (menuCounter == 5)
                {
                    exit = true;
                    //scoreBoardControl.SaveScores();
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (menuCounter == 1)
                        menuCounter = 5;
                    else
                    {
                        menuCounter--;
                    }
                }

                if (((keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (menuCounter == 5)
                        menuCounter = 1;
                    else
                    {
                        menuCounter++;
                    }
                }
            }
            previousKeyboardState = keyboardState;
        }

        public void MainMenuDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            _spriteBatch.Begin();

            if (menuCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\StartGame");
            }
            else if (menuCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\LoadGame");
            }
            else if (menuCounter == 3)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options");
            }
            else if (menuCounter == 4)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Extras");
            }
            else if (menuCounter == 5)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Exit");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);
            if (debugMode)
            {
                // TODO: Vectorize this logic
                font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Fonts\\Score\\ScoreHD");
                _spriteBatch.DrawString(font, "Debug On", new Vector2(1100, 50), Microsoft.Xna.Framework.Color.White);
            }
            _spriteBatch.End();
        }

        private void KillMainMenuComponents()
        {
        }
        #endregion

        #region SCOREBOARD METHODS
        private void InitializeScoreboardComponents()
        {

        }

        public void ScoreboardUpdate(ref GameState currentState)
        {
            /* Scoreboard */
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.MainMenu;
            }
            previousKeyboardState = keyboardState;
        }

        public void ScoreboardDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            background = _content.Load<Texture2D>("Backgrounds\\ScoreboardBackground");
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region OPTIONS METHODS
        public void OptionsUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.MainMenu;
            }
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(menuSelectSoundEffect);
                // Respond to user input for menu selections, etc
                if (optionsCounter == 1)
                {
                    currentState = GameState.Story;
                }
                else if (optionsCounter == 2)
                {
                    currentState = GameState.Controls;
                    InitializeControlsComponents();
                }
                else if (optionsCounter == 3)
                {
                    currentState = GameState.Settings;
                    InitializeSettingsComponents();
                }
                else if (optionsCounter == 4)
                {
                    currentState = GameState.MainMenu;
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (optionsCounter == 1)
                        optionsCounter = 4;
                    else
                    {
                        optionsCounter--;
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (optionsCounter == 4)
                        optionsCounter = 1;
                    else
                    {
                        optionsCounter++;
                    }
                }

                // Respond to user input for menu selections, etc
                
            }
            previousKeyboardState = keyboardState;
        }

        public void OptionsDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            _spriteBatch.Begin();


            if (optionsCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Story");
            }
            else if (optionsCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Controls");
            }
            else if (optionsCounter == 3)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings");
            }
            else if (optionsCounter == 4)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\MainMenu");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region DIFFICULTY METHODS
        private void InitializeDifficultyComponents()
        {
            diffCounter = 1;
        }

        public Difficulty SelectDifficultyUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.MainMenu;
            }
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(menuSelectSoundEffect);
                // Respond to user input for menu selections, etc
                if (diffCounter == 1)
                {
                    difficulty = Difficulty.Easy;
                    currentState = GameState.Loading;
                    KillDifficultyComponents();
                }
                else if (diffCounter == 2)
                {
                    difficulty = Difficulty.Medium;
                    currentState = GameState.Loading;
                    KillDifficultyComponents();
                }
                else if (diffCounter == 3)
                {
                    difficulty = Difficulty.Hard;
                    currentState = GameState.Loading;
                    KillDifficultyComponents();
                }
                else if (diffCounter == 4)
                {
                    difficulty = Difficulty.Insane;
                    currentState = GameState.Loading;
                    KillDifficultyComponents();
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (diffCounter == 1)
                        diffCounter = 4;
                    else
                    {
                        diffCounter--;
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (diffCounter == 4)
                        diffCounter = 1;
                    else
                    {
                        diffCounter++;
                    }
                }

                previousKeyboardState = keyboardState;
            }
            return difficulty;
        }

        private void KillDifficultyComponents()
        {
        }

        public void SelectDifficultyDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            _spriteBatch.Begin();

            if (Properties.Settings.Default.Gender == "female")
            {
                if (diffCounter == 1)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\FemParagon");
                }
                else if (diffCounter == 2)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\FemSentinel");
                }
                else if (diffCounter == 3)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\FemVangaurd");
                }
                else if (diffCounter == 4)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\FemRenegade");
                }
                else { }
            }
            else
            {
                if (diffCounter == 1)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\MaleParagon");
                }
                else if (diffCounter == 2)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\MaleSentinel");
                }
                else if (diffCounter == 3)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\MaleVangaurd");
                }
                else if (diffCounter == 4)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Difficulty\\MaleRenegade");
                }
                else { }
            }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region HOW TO PLAY METHODS

        public void HowToPlayUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Options;
            }
            previousKeyboardState = keyboardState;
        }

        private void KillHowToPlayComponents()
        {
        }

        public void HowToPlayDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            if (Properties.Settings.Default.Gender == "Female")
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Story\HerStory");
            else
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Story\HisStory");

            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);
            

            _spriteBatch.End();
        }

        #endregion

        #region CONTROLS METHODS
        private void InitializeControlsComponents()
        {
        }

        public void ControlsUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Options;
            }
            previousKeyboardState = keyboardState;
        }

        private void KillControlsComponents()
        {

        }

        public void ControlsDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Controls\Controls");
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);
            _spriteBatch.End();
        }

        #endregion

        #region SETTINGS

        #region SETTINGS METHODS
        private void InitializeSettingsComponents()
        {
            //easyButton = _content.Load<Texture2D>(@"Menu Items\\Components\\Easy");
            //mediumButton = _content.Load<Texture2D>(@"Menu Items\\Components\\Medium");
            //hardButton = _content.Load<Texture2D>(@"Menu Items\\Components\\Hard");
            //insaneButton = _content.Load<Texture2D>(@"Menu Items\\Components\\Insane");
        }

        public void SettingsUpdate(ref GameState currentState)
        {
            /* Select Difficulty */
            //insaneButtonPosition = new Vector2((_graphicsDevice.PresentationParameters.BackBufferWidth / 2) - 150, _graphicsDevice.PresentationParameters.BackBufferHeight - 200);
            //hardButtonPosition = new Vector2((_graphicsDevice.PresentationParameters.BackBufferWidth / 2) - 150, _graphicsDevice.PresentationParameters.BackBufferHeight - 260);
            //mediumButtonPosition = new Vector2((_graphicsDevice.PresentationParameters.BackBufferWidth / 2) - 150, _graphicsDevice.PresentationParameters.BackBufferHeight - 325);
            //easyButtonPosition = new Vector2((_graphicsDevice.PresentationParameters.BackBufferWidth / 2) - 150, _graphicsDevice.PresentationParameters.BackBufferHeight - 385);

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Options;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                if (settingsCounter == 1)
                {
                    currentState = GameState.GameSettings;
                    gameSettingsCounter = 1;
                }
                else if (settingsCounter == 2)
                {
                    currentState = GameState.AudioSettings;
                    audioSettingsCounter = 1;
                }
                else if (settingsCounter == 3)
                {
                    currentState = GameState.VideoSettings;
                    videoSettingsCounter = 1;
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (settingsCounter == 1)
                        settingsCounter = 3;
                    else
                    {
                        settingsCounter--;
                    }
                }

                if (((keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (settingsCounter == 3)
                        settingsCounter = 1;
                    else
                    {
                        settingsCounter++;
                    }
                }
            }
            previousKeyboardState = keyboardState;
        }

        private void KillSettingsComponents()
        {
        }

        public void SettingsDraw()
        {
            _spriteBatch.Begin();

            if (settingsCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Game");
            }
            else if (settingsCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Audio");
            }
            else if (settingsCounter == 3)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Video");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region GAME SETTINGS METHODS

        public void GameSettingsUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Settings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                if (gameSettingsCounter == 1)
                {
                    currentState = GameState.PlayerSelect;
                    if (Properties.Settings.Default.Gender == "Female")
                        playerCounter = 1;
                    else
                        playerCounter = 2;
                }
                else if (gameSettingsCounter == 2)
                {
                    currentState = GameState.PlayerSpeed;
                    speedCounter = Properties.Settings.Default.PlayerSpeed;
                }
                else if (gameSettingsCounter == 3)
                {
                    currentState = GameState.EnemySpeed;
                    enemySpeedCounter = Properties.Settings.Default.EnemySpeed/enemySpeed;
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (gameSettingsCounter == 1)
                        gameSettingsCounter = 3;
                    else
                    {
                        gameSettingsCounter--;
                    }
                }

                if (((keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (gameSettingsCounter == 3)
                        gameSettingsCounter = 1;
                    else
                    {
                        gameSettingsCounter++;
                    }
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void GameSettingsDraw()
        {
            _spriteBatch.Begin();

            if (gameSettingsCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Game\\Player");
            }
            else if (gameSettingsCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Game\\PlayerSpeed");
            }
            else if (gameSettingsCounter == 3)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Game\\EnemySpeedFactor");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #region PLAYER SELECT
        public void PlayerSelectUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.GameSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                if (playerCounter == 1)
                    Properties.Settings.Default.Gender = "Female";
                else
                    Properties.Settings.Default.Gender = "Male";

                Properties.Settings.Default.Save();
                gameSettingsCounter = 1;
                currentState = GameState.GameSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (playerCounter == 2)
                        playerCounter = 1;
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (playerCounter == 1)
                        playerCounter = 2;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void PlayerSelectDraw()
        {
            _spriteBatch.Begin();

            if (playerCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\Player\Player-Selected-FemShep");
            }
            else if (playerCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\Player\Player-Selected-MaleShep");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #region PLAYER SPEED
        public void PlayerSpeedUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.GameSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                Properties.Settings.Default.PlayerSpeed = speedCounter;
                Properties.Settings.Default.Save();
                gameSettingsCounter = 2;
                currentState = GameState.GameSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (speedCounter > 1)
                        speedCounter--;
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (speedCounter < 10)
                       speedCounter++;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void PlayerSpeedDraw()
        {
            _spriteBatch.Begin();

            if (speedCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\1");
            }
            else if (speedCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\2");
            }
            else if (speedCounter == 3)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\3");
            }
            else if (speedCounter == 4)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\4");
            }
            else if (speedCounter == 5)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\5");
            }
            else if (speedCounter == 6)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\6");
            }
            else if (speedCounter == 7)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\7");
            }
            else if (speedCounter == 8)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\8");
            }
            else if (speedCounter == 9)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\9");
            }
            else if (speedCounter == 10)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\PlayerSpeed\Max");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #region ENEMY SPEED FACTOR
        public void EnemySpeedUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.GameSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                Properties.Settings.Default.EnemySpeed = enemySpeedCounter * enemySpeed;
                Properties.Settings.Default.Save();
                gameSettingsCounter = 3;
                currentState = GameState.GameSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (enemySpeedCounter > 1)
                        enemySpeedCounter--;
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (enemySpeedCounter < 5)
                        enemySpeedCounter++;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void EnemySpeedDraw()
        {
            _spriteBatch.Begin();

            if (enemySpeedCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\EnemySpeed\1");
            }
            else if (enemySpeedCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\EnemySpeed\2");
            }
            else if (enemySpeedCounter == 3)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\EnemySpeed\3");
            }
            else if (enemySpeedCounter == 4)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\EnemySpeed\4");
            }
            else if (enemySpeedCounter == 5)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Game\Selected\EnemySpeed\Max");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #endregion

        #region AUDIO SETTINGS METHODS
        
        public void AudioSettingsUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Settings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                
                if (audioSettingsCounter == 1)
                {
                    currentState = GameState.MusicVolume;
                    musicVolumeCounter = (int) (Properties.Settings.Default.MusicVolume * 10);
                }
                else if (audioSettingsCounter == 2)
                {
                    currentState = GameState.SFXVolume;
                    sfxVolumeCounter = (int) (Properties.Settings.Default.SFXVolume * 10);
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (audioSettingsCounter == 2)
                        audioSettingsCounter = 1;
                }

                if (((keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (audioSettingsCounter == 1)
                        audioSettingsCounter = 2;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void AudioSettingsDraw()
        {
            _spriteBatch.Begin();

            if (audioSettingsCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Audio\\Music");
            }
            else if (audioSettingsCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Audio\\SFX");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #region MUSIC VOLUME
        public void MusicVolumeUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.AudioSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                Properties.Settings.Default.MusicVolume = (float)((float)musicVolumeCounter / 10);
                Properties.Settings.Default.Save();
                audioSettingsCounter = 1;
                currentState = GameState.AudioSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (musicVolumeCounter > 0)
                        musicVolumeCounter--;

                    backgroundMusic.Volume = (float)((float)musicVolumeCounter / 10.0);
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (musicVolumeCounter < 10)
                        musicVolumeCounter++;

                    backgroundMusic.Volume = (float)((float)musicVolumeCounter / 10.0);
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void MusicVolumeDraw()
        {
            _spriteBatch.Begin();

            if (musicVolumeCounter == 0)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\Mute");
            }
            else if (musicVolumeCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\1");
            }
            else if (musicVolumeCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\2");
            }
            else if (musicVolumeCounter == 3)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\3");
            }
            else if (musicVolumeCounter == 4)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\4");
            }
            else if (musicVolumeCounter == 5)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\5");
            }
            else if (musicVolumeCounter == 6)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\6");
            }
            else if (musicVolumeCounter == 7)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\7");
            }
            else if (musicVolumeCounter == 8)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\8");
            }
            else if (musicVolumeCounter == 9)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\9");
            }
            else if (musicVolumeCounter == 10)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\Music\Max");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #region SFX VOLUME
        public void SFXVolumeUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.AudioSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                Properties.Settings.Default.SFXVolume = (float)((float)sfxVolumeCounter / 10);
                Properties.Settings.Default.Save();
                audioSettingsCounter = 2;
                currentState = GameState.AudioSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (sfxVolumeCounter > 0)
                        sfxVolumeCounter--;
                    
                    PlaySoundEffect(menuAdvanceSoundEffect, (float)((float)sfxVolumeCounter / 10.0));
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (sfxVolumeCounter < 10)
                        sfxVolumeCounter++;

                    PlaySoundEffect(menuAdvanceSoundEffect, (float)((float)sfxVolumeCounter / 10.0));
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void SFXVolumeDraw()
        {
            _spriteBatch.Begin();

            if (sfxVolumeCounter == 0)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\Mute");
            }
            else if (sfxVolumeCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\1");
            }
            else if (sfxVolumeCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\2");
            }
            else if (sfxVolumeCounter == 3)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\3");
            }
            else if (sfxVolumeCounter == 4)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\4");
            }
            else if (sfxVolumeCounter == 5)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\5");
            }
            else if (sfxVolumeCounter == 6)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\6");
            }
            else if (sfxVolumeCounter == 7)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\7");
            }
            else if (sfxVolumeCounter == 8)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\8");
            }
            else if (sfxVolumeCounter == 9)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\9");
            }
            else if (sfxVolumeCounter == 10)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Audio\Selected\SFX\Max");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #endregion

        #region VIDEO SETTINGS METHODS

        public void VideoSettingsUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Settings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                if (videoSettingsCounter == 1)
                {
                    currentState = GameState.Brightness;
                    brightnessCounter = Properties.Settings.Default.Brightness;
                }
                else if (videoSettingsCounter == 2)
                {
                    currentState = GameState.DisplayMode;

                    if (Properties.Settings.Default.DisplayMode == "Windowed")
                        displayModeCounter = 1;
                    else
                        displayModeCounter = 2;
                }
                else if (videoSettingsCounter == 3)
                {
                    currentState = GameState.ScreenResolution;

                    if (Properties.Settings.Default.Resolution == "Standard")
                        screenResolutionCounter = 1;
                    else if (Properties.Settings.Default.Resolution == "HD")
                        screenResolutionCounter = 2;
                    else if (Properties.Settings.Default.Resolution == "HDPlus")
                        screenResolutionCounter = 3;
                    else if (Properties.Settings.Default.Resolution == "FullHD")
                        screenResolutionCounter = 4;
                    else if (Properties.Settings.Default.Resolution == "FourK")
                        screenResolutionCounter = 5;
                    else { }
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (videoSettingsCounter == 1)
                        videoSettingsCounter = 3;
                    else
                        videoSettingsCounter--;
                }

                if (((keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))))
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (videoSettingsCounter == 3)
                        videoSettingsCounter = 1;
                    else
                        videoSettingsCounter++;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void VideoSettingsDraw()
        {
            _spriteBatch.Begin();

            if (videoSettingsCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Video\\Brightness");
            }
            else if (videoSettingsCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Video\\DisplayMode");
            }
            else if (videoSettingsCounter == 3)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\MainMenu\\Options\\Settings\\Video\\ScreenResolution");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #region SCREEN RESOLUTION
        public void ScreenResolutionUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.VideoSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                if (screenResolutionCounter == 1)
                {
                    Properties.Settings.Default.Resolution = "Standard";
                    resolution = SpaceBHGame.Resolution.Standard;
                }
                else if (screenResolutionCounter == 2)
                {
                    Properties.Settings.Default.Resolution = "HD";
                    resolution = SpaceBHGame.Resolution.HD;
                }
                else if (screenResolutionCounter == 3)
                {
                    Properties.Settings.Default.Resolution = "HDPlus";
                    resolution = SpaceBHGame.Resolution.HDPlus;
                }
                else if (screenResolutionCounter == 4)
                {
                    Properties.Settings.Default.Resolution = "FullHD";
                    resolution = SpaceBHGame.Resolution.FullHD;
                }
                else if (screenResolutionCounter == 5)
                {
                    Properties.Settings.Default.Resolution = "FourK";
                    resolution = SpaceBHGame.Resolution.FourK;
                }
                else { }
                
                Properties.Settings.Default.Save();
                audioSettingsCounter = 3;
                currentState = GameState.VideoSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (screenResolutionCounter > 1)
                        screenResolutionCounter--;
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (screenResolutionCounter < 5)
                        screenResolutionCounter++;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void ScreenResolutionDraw()
        {
            _spriteBatch.Begin();

            if (screenResolutionCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\ScreenResolution\Standard");
            }
            else if (screenResolutionCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\ScreenResolution\HD");
            }
            else if (screenResolutionCounter == 3)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\ScreenResolution\HDPlus");
            }
            else if (screenResolutionCounter == 4)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\ScreenResolution\FullHD");
            }
            else if (screenResolutionCounter == 5)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\ScreenResolution\FourK");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #region DISPLAY MODE
        public void DisplayModeUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.VideoSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                if (displayModeCounter == 1)
                {
                    Properties.Settings.Default.DisplayMode = "Windowed";
                    IsFullscreen = false;
                }
                else
                {
                    Properties.Settings.Default.DisplayMode = "FullScreen";
                    IsFullscreen = true;
                }

                Properties.Settings.Default.Save();
                videoSettingsCounter = 2;
                currentState = GameState.VideoSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (displayModeCounter == 2)
                        displayModeCounter = 1;
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (displayModeCounter == 1)
                        displayModeCounter = 2;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void DisplayModeDraw()
        {
            _spriteBatch.Begin();

            if (displayModeCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\DisplayMode\Windowed");
            }
            else if (displayModeCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\DisplayMode\Fullscreen");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #region BRIGHTNESS
        public void BrightnessUpdate(ref GameState currentState)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                // Respond to user input for menu selections, etc
                currentState = GameState.VideoSettings;
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                PlaySoundEffect(menuSelectSoundEffect);
                Properties.Settings.Default.Brightness = brightnessCounter;
                Properties.Settings.Default.Save();
                videoSettingsCounter = 1;
                currentState = GameState.VideoSettings;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    if (brightnessCounter > 1)
                        brightnessCounter--;
                }

                if (((keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))))
                {
                    if (brightnessCounter < 10)
                        brightnessCounter++;
                }
            }

            previousKeyboardState = keyboardState;
        }

        public void BrightnessDraw()
        {
            _spriteBatch.Begin();

            if (brightnessCounter == 1)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\1");
            }
            else if (brightnessCounter == 2)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\2");
            }
            else if (brightnessCounter == 3)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\3");
            }
            else if (brightnessCounter == 4)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\4");
            }
            else if (brightnessCounter == 5)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\5");
            }
            else if (brightnessCounter == 6)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\6");
            }
            else if (brightnessCounter == 7)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\7");
            }
            else if (brightnessCounter == 8)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\8");
            }
            else if (brightnessCounter == 9)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\9");
            }
            else if (brightnessCounter == 10)
            {
                background = _content.Load<Texture2D>(@"Menu Items\Components\MainMenu\Options\Settings\Video\Selected\Brightness\Max");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth,
                _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }

        #endregion

        #endregion

        #endregion

        #region CREDITS METHODS

        private void InitCredits()
        {
            credits = new List<CreditsLine>();
            inCredits = new List<CreditsLine>();

            credits.Add(new CreditsLine("MUSIC"));
            credits.Add(new CreditsLine(""));
            credits.Add(new CreditsLine("Song: Dragon's Lair"));
            credits.Add(new CreditsLine("Use: Starting Music"));
            credits.Add(new CreditsLine("Album: Magical Creatures"));
            credits.Add(new CreditsLine("Year: 2013"));
            credits.Add(new CreditsLine("Music Copyright: Brandon Fiechter"));
            credits.Add(new CreditsLine("Song: Main Theme"));
            credits.Add(new CreditsLine("Use: Easy Difficulty Music"));
            credits.Add(new CreditsLine("Album: Shovel Knight Original Soundtrack "));
            credits.Add(new CreditsLine("Year: 2014"));
            credits.Add(new CreditsLine("Music Copyright: Jake Kaufman"));
            credits.Add(new CreditsLine("Song: Main Title"));
            credits.Add(new CreditsLine("Use: Medium Difficulty Music"));
            credits.Add(new CreditsLine("Album: Game of Thrones"));
            credits.Add(new CreditsLine("Year: 2011"));
            credits.Add(new CreditsLine("Music Copyright: Ramin Djawadi"));
            credits.Add(new CreditsLine("Song: The Uruk-hai"));
            credits.Add(new CreditsLine("Use: Hard Difficulty Music"));
            credits.Add(new CreditsLine("Album: The Lord of the Rings: The Two Towers:"));
            credits.Add(new CreditsLine("       Original Motion Picture Soundtrack"));
            credits.Add(new CreditsLine("Year: 2002"));
            credits.Add(new CreditsLine("Music Copyright: Howard Shore"));
            credits.Add(new CreditsLine("Song: Dragonborn"));
            credits.Add(new CreditsLine("Use: Insane Difficulty Music"));
            credits.Add(new CreditsLine("Album: The Elder Scrolls V: Skyrim"));
            credits.Add(new CreditsLine("       (Original Game Soundtrack)"));
            credits.Add(new CreditsLine("Year: 2011"));
            credits.Add(new CreditsLine("Music Copyright: Jeremy Soule"));
            credits.Add(new CreditsLine(""));
            credits.Add(new CreditsLine("ARTWORK"));
            credits.Add(new CreditsLine(""));
            credits.Add(new CreditsLine("Image: Menu Backgrounds"));
            credits.Add(new CreditsLine("Source: http://fairytailfanon.wikia.com/wiki/File:Fant"));
            credits.Add(new CreditsLine("        asy-Castle-Wallpaper-11.jpg"));
            credits.Add(new CreditsLine("Image: Credits Background"));
            credits.Add(new CreditsLine("Source: http://www.creativeuncut.com/gallery-24/lotr-ice-"));
            credits.Add(new CreditsLine("        cave.html"));
            credits.Add(new CreditsLine("Image: Gameplay Background"));
            credits.Add(new CreditsLine("Source: Unknown"));
            credits.Add(new CreditsLine("Image: Title Page Background"));
            credits.Add(new CreditsLine("Source: http://www.cartoontreasure.com/download-flying"));
            credits.Add(new CreditsLine("        -fire-dragon-attack-free-desktop-wallpaper.html"));
            credits.Add(new CreditsLine("Image: Pause Background"));
            credits.Add(new CreditsLine("Source: http://sigbjornpedersen.deviantart.com/art/Dr"));
            credits.Add(new CreditsLine("        agon-attack-290460724"));
            credits.Add(new CreditsLine("Image: Lose Background"));
            credits.Add(new CreditsLine("Source: http://wings-of-fire-fanon-tribes.wikia.com/wiki"));
            credits.Add(new CreditsLine("        /File:Fantasy-dragon-dragons-27155012-2560-1600.jpg"));
            credits.Add(new CreditsLine("Image: Win Background"));
            credits.Add(new CreditsLine("Source: http://digital-art-gallery.com/picture/big/2491"));
            credits.Add(new CreditsLine("Image: Loading Background"));
            credits.Add(new CreditsLine("Source: http://www.pickywallpapers.com/1280x1024/fantasy/"));
            credits.Add(new CreditsLine("        art/dragon-fight-knight-wallpaper/download/"));
            credits.Add(new CreditsLine(""));
        }

        public void CreditsUpdate(ref GameState currentState, GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(backSoundEffect);
                // Respond to user input for menu selections, etc
                currentState = GameState.Options;
            }
            previousKeyboardState = keyboardState;

            if (credits.Count != 0)
            {
                if (gameTime.TotalGameTime.Milliseconds % 250 == 0)
                {
                    inCredits.Add(credits[0]);
                    credits.RemoveAt(0);
                }
            }

            foreach (CreditsLine cr in inCredits)
            {
                cr.Update();
            }

            previousKeyboardState = keyboardState;
        }

        public void CreditsDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            background = _content.Load<Texture2D>("Backgrounds\\CreditsBackground");
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            foreach (CreditsLine cr in inCredits)
            {
                cr.Draw();
            }

            _spriteBatch.End();
        }

        private class CreditsLine
        {
            string line;
            int y;
            SpriteFont font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Salutation");

            public CreditsLine(string str) 
            {
                line = str;
                y = 650;
            }

            public void Update()
            {
                y -= 2;
            }
            public void Draw()
            {
                _spriteBatch.DrawString(font, line, new Vector2(400, y), Microsoft.Xna.Framework.Color.Black);
            }
        }

        #endregion

        #region LOAD SCREEN METHODS
        public void LoadingUpdate(ref GameState currentState)
        {
            /* Loading */
            // TODO: Make load screens between music transistions to make transition cleaner
            initiateGameSettings();
            InitializeGameplayComponents();
            currentState = GameState.Gameplay;

            Thread.Sleep(1000);
        }

        public void LoadingDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            background = _content.Load<Texture2D>("Backgrounds\\LoadBackground");
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);

            _spriteBatch.End();
        }
        #endregion

        #region GAMEPLAY METHODS
        private void initiateGameSettings()
        {
            game = new Gameplay.Gameplay(ref _spriteBatch, _graphicsDevice, _content, difficulty, resolution, highscore, debugMode);
            if (difficulty == Difficulty.Easy)
            {
                gameplaySoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\ParagonGameplay");
                backgroundMusic.Stop();
                PlayMusic(gameplaySoundEffect);
            }
            else if (difficulty == Difficulty.Medium)
            {
                gameplaySoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\SentinalGameplay");
                backgroundMusic.Stop();
                PlayMusic(gameplaySoundEffect);
            }
            else if (difficulty == Difficulty.Hard)
            {
                gameplaySoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\VangaurdGameplay");
                backgroundMusic.Stop();
                PlayMusic(gameplaySoundEffect);
            }
            else if (difficulty == Difficulty.Insane)
            {
                gameplaySoundEffect = _content.Load<SoundEffect>("Gameplay Components\\Music\\RenegadeGameplay");
                backgroundMusic.Stop();
                PlayMusic(gameplaySoundEffect);
            }
            else { }

            game = new Gameplay.Gameplay(ref _spriteBatch, _graphicsDevice, _content, Difficulty.Easy, resolution, highscore, debugMode);
            newGame = true;
        }

        private void InitializeGameplayComponents()
        {
            game.Initialize();
        }

        private void KillGameplayComponents()
        {
            game.Kill();
        }

        public void GameplayUpdate(GameTime gameTime, ref GameState currentState)
        {
            currentState = game.Update(gameTime);

            switch(currentState)
            {
                case GameState.EndOfGame:
                    currentState = GameState.EndOfGame;
                    InitializeEndOfGameComponents();
                    break;
                case GameState.Pause:
                    InitializePauseComponents();
                    break;
                case GameState.Exit:
                    exit = true;
                    break;
                default:
                    break;
            }
        }

        public void GameplayDraw(ref GameState currentState)
        {
            currentState = game.Draw(ref currentState);
            switch (currentState)
            {
                default:
                    break;
            }
        }
        #endregion

        #region PAUSE METHODS
        private void InitializePauseComponents()
        {
            pauseCounter = 1;
        }

        public void PauseUpdate(ref GameState currentState)
        {
            /* Pause */
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(menuSelectSoundEffect);
                // Respond to user input for menu selections, etc
                if (pauseCounter == 1)
                {
                    currentState = GameState.Gameplay;
                }
                else if (pauseCounter == 2)
                {
                    currentState = GameState.Scoreboard;
                }
                else if (pauseCounter == 3)
                {
                    currentState = GameState.MainMenu;
                    backgroundMusic.Stop();
                    menuCounter = 1;
                    PlayMusic(startUpSoundEffect);
                }
                else if (pauseCounter == 4)
                {
                    exit = true;
                    scoreBoardControl.SaveScores();
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (pauseCounter == 1)
                        pauseCounter = 4;
                    else
                    {
                        pauseCounter--;
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (pauseCounter == 4)
                        pauseCounter = 1;
                    else
                    {
                        pauseCounter++;
                    }
                }
            }
            previousKeyboardState = keyboardState;
        }

        public void PauseDraw()
        {
            // Draw the main menu, any active selections, etc
            //MM: load the background?
            _spriteBatch.Begin();

            // Respond to user input for menu selections, etc
            if (pauseCounter == 1)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\Pause\\Resume");
            }
            else if (pauseCounter == 2)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\Pause\\Scoreboard");
            }
            else if (pauseCounter == 3)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\Pause\\MainMenu");
            }
            else if (pauseCounter == 4)
            {
                background = _content.Load<Texture2D>("Menu Items\\Components\\Pause\\Exit");
            }
            else { }

            _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);


            _spriteBatch.End();
        }
        #endregion

        #region END OF GAME
        private void InitializeEndOfGameComponents()
        {
            backgroundMusic.Stop();
            PlayMusic(startUpSoundEffect);
            endCounter = 1;
        }

        public void EndOfGameUpdate(ref GameState currentState)
        {
            /* End Game */
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                PlaySoundEffect(menuSelectSoundEffect);
                // Respond to user input for menu selections, etc
                if (endCounter == 1)
                {
                    currentState = GameState.SelectDifficulty;
                    InitializeDifficultyComponents();
                }
                else if (endCounter == 2)
                {
                    currentState = GameState.Scoreboard;
                }
                else if (endCounter == 3)
                {
                    currentState = GameState.MainMenu;
                    menuCounter = 1;
                }
                else if (endCounter == 4)
                {
                    exit = true;
                    scoreBoardControl.SaveScores();
                }
                else { }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)) // GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (endCounter == 1)
                        endCounter = 4;
                    else
                        endCounter--;
                }

                if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
                {
                    PlaySoundEffect(menuAdvanceSoundEffect);
                    if (endCounter == 4)
                        endCounter = 1;
                    else
                        endCounter++;
                }
            }
            previousKeyboardState = keyboardState;
        }

        public void EndOfGameDraw()
        {
            // Draw text and scores
            // Draw menu for restarting level or going back to main menu
            // Draw the main menu, any active selections, etc
            //MM: load the background?

            KillGameplayComponents();

            _spriteBatch.Begin();

            if (game.GameWin == true)
            {
                if (endCounter == 1)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Win\\PlayAgain");
                }
                else if (endCounter == 2)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Win\\Scoreboard");// Respond to user input for menu selections, etc
                }
                else if (endCounter == 3)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Win\\MainMenu");// Respond to user input for menu selections, etc
                }
                else if (endCounter == 4)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Win\\Exit");// Respond to user input for menu selections, etc
                }
                else { }
                _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                if (endCounter == 1)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Lose\\PlayAgain");
                }
                else if (endCounter == 2)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Lose\\Scoreboard");// Respond to user input for menu selections, etc
                }
                else if (endCounter == 3)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Lose\\MainMenu");// Respond to user input for menu selections, etc
                }
                else if (endCounter == 4)
                {
                    background = _content.Load<Texture2D>("Menu Items\\Components\\Lose\\Exit");// Respond to user input for menu selections, etc
                }
                else { }
                _spriteBatch.Draw(background, new Rectangle(0, 0, _graphicsDevice.PresentationParameters.BackBufferWidth, _graphicsDevice.PresentationParameters.BackBufferHeight), Microsoft.Xna.Framework.Color.White);
            }

            _spriteBatch.End();
        }
        #endregion

        #region UIHelper Functions

        public void PlayMusic(SoundEffect bgMusic)
        {
            backgroundMusic = bgMusic.CreateInstance();
            backgroundMusic.IsLooped = true;
            backgroundMusic.Play();
        }

        public void PlaySoundEffect(SoundEffect sfx, float volume = float.NaN)
        {
            soundEffect = sfx.CreateInstance();
            soundEffect.IsLooped = false;

            if (!float.IsNaN(volume))
                soundEffect.Volume = volume;
            else
                soundEffect.Volume = Properties.Settings.Default.SFXVolume;

            soundEffect.Play();
        }

        #endregion

        #region WORKFLOW METHODS

        private void UpdateAppSettings()
        {

        }

        private void UpdateBrightness()
        {

        }

        private void UpdateResolution()
        {

        }

        private void UpdateDisplayMode()
        {

        }

        #endregion


        /*public void GetInput()
        {
            KeyboardState keyboardState;
            KeyboardState prevKeyboardState = Keyboard.GetState();

            font = _content.Load<SpriteFont>(@"Gameplay Components\\Dashboard Components\\Score");

            TextBox textbox = new TextBox(font);
            string text = "";

            Rectangle backRect = new Rectangle(_graphicsDevice.PresentationParameters.BackBufferWidth / 2, _graphicsDevice.PresentationParameters.BackBufferHeight / 2, 300, 22);

            while (true)
            {
                keyboardState = Keyboard.GetState();
                text += textbox.OnKeyboardKeyPress(keyboardState);
                _spriteBatch.DrawString(font, text, new Vector2((_graphicsDevice.PresentationParameters.BackBufferWidth / 2) + 3, (_graphicsDevice.PresentationParameters.BackBufferHeight / 2) + 3), Microsoft.Xna.Framework.Color.Black);
                if (keyboardState.IsKeyDown(Keys.Enter) && !prevKeyboardState.IsKeyDown(Keys.Enter))
                {
                    break;
                }
                prevKeyboardState = keyboardState;
            }
        }*/
        
        #region DEBUG FUNCTIONS
        /* 
         * Press E to spawn enemy
         * Press K to increase power level
         * Press Space bar to shoot
         * Press L to lose lives
         * Press U to score
         * Press B to get bomb
         * Press T to get shield
         * Press V to use bomb
         * Press R to use shield
         */
        public void DebugFunct()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.L) && !previousKeyboardState.IsKeyDown(Keys.L)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                
            }
            else if (keyboardState.IsKeyDown(Keys.U) && !previousKeyboardState.IsKeyDown(Keys.U)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                
            }
            else if (keyboardState.IsKeyDown(Keys.R) && !previousKeyboardState.IsKeyDown(Keys.R)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                
            }
            else if (keyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S)) //GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || 
            {
                
            }
            else { }

            previousKeyboardState = keyboardState;
        }
        #endregion
    }
}