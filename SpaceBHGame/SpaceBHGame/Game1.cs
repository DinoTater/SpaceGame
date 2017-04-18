﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceBHGame.UI_Control;
using System.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Media;
using SpaceBHGame.Gameplay;

namespace SpaceBHGame
{
    public enum GameState
    {
        Title,
        ChooseCharacter,
        MainMenu,
        SelectDifficulty,
        Options,
        Scoreboard,
        Credits,
        Story,
        Settings,
        GameSettings,
        PlayerSelect,
        PlayerSpeed,
        EnemySpeed,
        AudioSettings,
        MusicVolume,
        SFXVolume,
        VideoSettings,
        Brightness,
        DisplayMode,
        ScreenResolution,
        Controls,
        LoadGame,
        Loading,
        Gameplay,
        Pause,
        EndOfGame,
        Exit
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Insane
    }

    public enum Resolution
    {
        Standard,
        HD,
        HDPlus,
        FullHD,
        FourK,
        FullScreen
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        /*********************ALERT *********************/
        /* IF YOU WANT TO JUST CODE THE GAME WITH NO UI */
        /* TOGGLE KEEP THESE VARIABLES FALSE!!! WE WILL */
        /* REMOVE IT WHEN WE INTGRATE LATER             */
        /************************************************/
        bool CammisCoding = false;
        bool NewCharacter = false;

        /* Gameplay Member Variables */
        SpriteBatch _spriteBatch;

        #region UI Stuff
        /* Game State Member Variables */
        GameState _state;
        UI_Control.UIControl _gameScreen;
        Difficulty difficulty;

        #endregion
        /* End Game State Member Variables */

        /* Game Graphics Member Variables */
        GraphicsDeviceManager graphics;
        string dataFolder = AppDomain.CurrentDomain.BaseDirectory + "\\Game Data";
        Resolution game_resolution;
        bool IsFullscreen = false;
        

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Space Bullet Hell Game - It's Bit, Fam";
            this.Window.AllowUserResizing = false;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20);

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
        }
            
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //this.Window.AllowUserResizing = true;
            //this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

            //set the position of the buttons

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameScreen = new UI_Control.UIControl(ref _spriteBatch, GraphicsDevice, Content);
            game_resolution = _gameScreen.Resolution;

            ChangeResolution(game_resolution);
            if (CammisCoding)
            {
                #region UI Stuff
                _state = GameState.Title;
                _gameScreen.InitializeTitle();
                #endregion
            }
            else
            {
                _gameScreen.game = new Gameplay.Gameplay(ref _spriteBatch, GraphicsDevice, Content, Difficulty.Easy, _gameScreen.Resolution, 100000);
                _gameScreen.game.Initialize();
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.


            // TODO: use this.Content to load your game content here
            // Load the player resources
           
            //load the buttonimages into the content pipeline

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (CammisCoding)
            {
                Resolution now = game_resolution;
                game_resolution = _gameScreen.Resolution;

                if (now != game_resolution)
                    ChangeResolution(game_resolution);

                #region UI Stuff
                switch (_state)
                {
                    case GameState.MainMenu:
                        _gameScreen.MainMenuUpdate(ref _state);
                        break;
                    case GameState.Title:
                        _gameScreen.TitleUpdate(ref _state, NewCharacter);
                        break;
                    case GameState.ChooseCharacter:
                        _gameScreen.ChooseCharacterUpdate(ref _state);
                        break;
                    case GameState.Story:
                        _gameScreen.HowToPlayUpdate(ref _state);
                        break;
                    case GameState.Controls:
                        _gameScreen.ControlsUpdate(ref _state);
                        break;
                    case GameState.Settings:
                        _gameScreen.SettingsUpdate(ref _state);
                        break;
                    case GameState.VideoSettings:
                        _gameScreen.VideoSettingsUpdate(ref _state);
                        break;
                    case GameState.Brightness:
                        _gameScreen.BrightnessUpdate(ref _state);
                        break;
                    case GameState.DisplayMode:
                        _gameScreen.DisplayModeUpdate(ref _state);
                        // TODO: Fix this
                        if (_gameScreen.IsFullscreen != IsFullscreen)
                        {
                            IsFullscreen = _gameScreen.IsFullscreen;

                            if (IsFullscreen)
                                if (!graphics.IsFullScreen)
                                    graphics.ToggleFullScreen();
                            else
                                if (graphics.IsFullScreen)
                                    graphics.ToggleFullScreen();
                        }
                        break;
                    case GameState.ScreenResolution:
                        _gameScreen.ScreenResolutionUpdate(ref _state);
                        if (_gameScreen.Resolution != game_resolution)
                            ChangeResolution(_gameScreen.Resolution);
                        break;
                    case GameState.AudioSettings:
                        _gameScreen.AudioSettingsUpdate(ref _state);
                        break;
                    case GameState.MusicVolume:
                        _gameScreen.MusicVolumeUpdate(ref _state);
                        break;
                    case GameState.SFXVolume:
                        _gameScreen.SFXVolumeUpdate(ref _state);
                        break;
                    case GameState.GameSettings:
                        _gameScreen.GameSettingsUpdate(ref _state);
                        break;
                    case GameState.PlayerSelect:
                        _gameScreen.PlayerSelectUpdate(ref _state);
                        break;
                    case GameState.PlayerSpeed:
                        _gameScreen.PlayerSpeedUpdate(ref _state);
                        break;
                    case GameState.EnemySpeed:
                        _gameScreen.EnemySpeedUpdate(ref _state);
                        break;
                    case GameState.Gameplay:
                        _gameScreen.GameplayUpdate(gameTime, ref _state);
                        break;
                    case GameState.EndOfGame:
                        _gameScreen.EndOfGameUpdate(ref _state);
                        break;
                    case GameState.Credits:
                        _gameScreen.CreditsUpdate(ref _state, gameTime);
                        break;
                    case GameState.Loading:
                        _gameScreen.LoadingUpdate(ref _state);
                        break;
                    case GameState.Options:
                        _gameScreen.OptionsUpdate(ref _state);
                        break;
                    case GameState.Pause:
                        _gameScreen.PauseUpdate(ref _state);
                        break;
                    case GameState.Scoreboard:
                        _gameScreen.ScoreboardUpdate(ref _state);
                        break;
                    case GameState.SelectDifficulty:
                        difficulty = _gameScreen.SelectDifficultyUpdate(ref _state);
                        break;
                }

                if (_gameScreen.Exit == true)
                    Exit();
                #endregion
            }
            else
            {
                
                _gameScreen.GameplayUpdate(gameTime, ref _state);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (CammisCoding)
            {
                #region UI Stuff
                switch (_state)
                {
                    case GameState.MainMenu:
                        _gameScreen.MainMenuDraw();
                        break;
                    case GameState.Title:
                        _gameScreen.TitleDraw();
                        break;
                    case GameState.ChooseCharacter:
                        _gameScreen.ChooseCharacterDraw();
                        break;
                    case GameState.Story:
                        _gameScreen.HowToPlayDraw();
                        break;
                    case GameState.Controls:
                        _gameScreen.ControlsDraw();
                        break;
                    case GameState.Settings:
                        _gameScreen.SettingsDraw();
                        break;
                    case GameState.VideoSettings:
                        _gameScreen.VideoSettingsDraw();
                        break;
                    case GameState.Brightness:
                        _gameScreen.BrightnessDraw();
                        break;
                    case GameState.DisplayMode:
                        _gameScreen.DisplayModeDraw();
                        break;
                    case GameState.ScreenResolution:
                        _gameScreen.ScreenResolutionDraw();
                        break;
                    case GameState.AudioSettings:
                        _gameScreen.AudioSettingsDraw();
                        break;
                    case GameState.MusicVolume:
                        _gameScreen.MusicVolumeDraw();
                        break;
                    case GameState.SFXVolume:
                        _gameScreen.SFXVolumeDraw();
                        break;
                    case GameState.GameSettings:
                        _gameScreen.GameSettingsDraw();
                        break;
                    case GameState.PlayerSelect:
                        _gameScreen.PlayerSelectDraw();
                        break;
                    case GameState.PlayerSpeed:
                        _gameScreen.PlayerSpeedDraw();
                        break;
                    case GameState.EnemySpeed:
                        _gameScreen.EnemySpeedDraw();
                        break;
                    case GameState.Gameplay:
                        _gameScreen.GameplayDraw(ref _state);
                        break;
                    case GameState.EndOfGame:
                        _gameScreen.EndOfGameDraw();
                        break;
                    case GameState.Credits:
                        _gameScreen.CreditsDraw();
                        break;
                    case GameState.Loading:
                        _gameScreen.LoadingDraw();
                        break;
                    case GameState.Options:
                        _gameScreen.OptionsDraw();
                        break;
                    case GameState.Pause:
                        _gameScreen.PauseDraw();
                        break;
                    case GameState.Scoreboard:
                        _gameScreen.ScoreboardDraw();
                        break;
                    case GameState.SelectDifficulty:
                        _gameScreen.SelectDifficultyDraw();
                        break;
                }
                #endregion
            }
            else
            {
                _gameScreen.GameplayDraw(ref _state);
            }

            base.Draw(gameTime);
        }

        #region UI Stuff
        private void ChangeResolution(Resolution resolution)
        {
            if (resolution == Resolution.Standard)
            {
                graphics.PreferredBackBufferWidth = 960;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 540;   // set this value to the desired height of your window
                graphics.ApplyChanges();
                return;
            }
            else if (resolution == Resolution.HD)
            {
                graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
                graphics.ApplyChanges();
                return;
            }
            else if (resolution == Resolution.HDPlus)
            {
                graphics.PreferredBackBufferWidth = 1600;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 900;   // set this value to the desired height of your window
                graphics.ApplyChanges();
                return;
            }
            else if (resolution == Resolution.FullHD)
            {
                graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
                graphics.ApplyChanges();
                return;
            }
            else if (resolution == Resolution.FourK)
            {
                graphics.PreferredBackBufferWidth = 4096;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 2304;   // set this value to the desired height of your window
                graphics.ApplyChanges();
                return;
            }
            else if (resolution == Resolution.FullScreen)
            {
                if (!graphics.IsFullScreen)
                {
                    graphics.ToggleFullScreen();
                    graphics.ApplyChanges();
                }
                return;
            }
            else
            {
                if (graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
                graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
                graphics.ApplyChanges();
                return;
            }
        }
        #endregion
    }
}
