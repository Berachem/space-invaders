using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using System.Runtime.CompilerServices;
using SpaceInvaders.GameObjects.Decorations;
using SpaceInvaders.GameObjects.Utils;
using SpaceInvaders.GameObjects.Bonus;
using SpaceInvaders.GameObjects.Enemies;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the entire game, it implements the singleton pattern
    /// </summary>
    class Game
    {

        #region GameObjects management

        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }


        #endregion

        #region game technical elements

            #region general paramenters
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// ID of the current game (3 capital letters)
        /// </summary>
        public string gameID;

        ///<summary>
        /// boolean use to see if it's single or multiplayer
        ///</summarry>
        private Boolean multiplayer;

        /// <summary>
        ///an enum of differents gamestates
        /// </summary>
        enum GameState { PLAY, PAUSE, WIN, LOSE, STARTMENU}

        /// <summary>
        ///actual state of the game
        /// </summary>
        private GameState state = GameState.STARTMENU;
            #endregion

            #region managers
        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        /// <summary>
        /// Sound manager for the game
        /// </summary>
        public static SoundManager soundManager = new SoundManager();

        /// <summary>
        ///random manager
        /// </summary>
        public Random random = new Random();
            #endregion

            #region visuals
        /// <summary>
        /// The dynamic background of the game
        /// </summary>
        private readonly Background gameBackground = new Background();

        /// <summary>
        /// Menu used to start the game
        /// </summary>
        private GameObjects.Decorations.Menu startMenu;

        /// <summary>
        /// Menu used to start the game
        /// </summary>
        private GameObjects.Decorations.Menu endMenu;

        /// <summary>
        /// Menu used when pause is on
        /// </summary>
        private GameObjects.Decorations.Menu pauseMenu;
            #endregion

            #region ennemies
        /// <summary>
        ///enemies
        /// </summary>
        private EnemyBlock enemies;

        /// <summary>
        /// boss
        /// </summary>
        private Boss boss;
            #endregion

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// Shared brushes
        /// </summary>
        public static Brush blackBrush = new SolidBrush(Color.Black);
        public static Brush whiteBrush = new SolidBrush(Color.White);
        public static Brush redBrush = new SolidBrush(Color.Red);
        public static Brush greenBrush = new SolidBrush(Color.Green);
        public static Brush blueBrush = new SolidBrush(Color.Blue);

        /// <summary
        /// Default pens
        /// </summary>
        public static Pen blackPen = new Pen(Color.Black);
        public static Pen whitePen = new Pen(Color.White);


        /// <summary>
        /// A shared simple font
        /// </summary>
        public static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion

        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null){game = new Game(gameSize); }
           

            //we can't create a game before we know if it's multiplayer
            if(game.state == GameState.PLAY)
            {
                soundManager.StopBackgroundMusic();
                //reset gameObjects
                game.gameObjects.Clear();
                game.pendingNewGameObjects.Clear();
                game.boss = null;
                GameConfig(game);

            }
            else 
            {
                soundManager.PlayBackgroundMusic();
                if (game.startMenu == null){game.startMenu = CreateStartMenu();}
                if (game.endMenu == null){game.endMenu = CreateEndMenu(); }
                if (game.pauseMenu == null) { game.pauseMenu = CreatePauseMenu(); }
            }

            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;

        }

        /// <summary>
        /// Return a random string, it is used to give an ID to the game 
        /// </summary>
        /// <param name="lenght">lenght of the game</param>
        /// <returns></returns>
        private string RandomString(int lenght)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        #region methods

            #region configuration
        /// <summary>
        /// Configure the game for a one player game (1 spaceship, 4 bunkers, 6 enemie lines)
        /// </summary>  
        /// <param name="gameSize">Size of the game area</param>"
        private static void GameConfig(Game game)
        {
            // reset gameID and load scores
            game.gameID = game.RandomString(3);
            HighScoreManager.LoadScores();

            //add players
            AddPlayers(game);

            // add bunkers
            AddBunkers(game, 3);

            //add enemies
            AddEnnemies(game);    
        }

        /// <summary>
        /// Add bunkers to the game
        /// </summary>  
        /// <param name="game">game where we add something</param>"
        /// <param name="numberBunker">numbers of bunkers</param>
        private static void AddBunkers(Game game, int numberBunker)
        {
            Bitmap BunkerImage = SpaceInvaders.Properties.Resources.bunker;
            double marge = (game.gameSize.Width - (numberBunker * BunkerImage.Width)) / (1 + numberBunker);
            double y = 4 * game.gameSize.Height / 5;
            for (int i = 0; i < numberBunker; i++)
            {
                game.AddNewGameObject(new Bunker(new Vecteur2D(BunkerImage.Width / 2 + i * BunkerImage.Width + (i + 1) * marge, y)));
            }
        }
        
        /// <summary>
        /// Add an ennemy block to the game
        /// </summary>  
        /// <param name="game">game where we add something</param>"
        private static void AddEnnemies(Game game)
        {
            game.enemies = new EnemyBlock(game.gameSize);
            game.AddNewGameObject(game.enemies);
            
            game.enemies.AddLine(8, 1, SpaceInvaders.Properties.Resources.ship3);
            game.enemies.AddLine(8, 1, SpaceInvaders.Properties.Resources.ship5);
            game.enemies.AddLine(7, 1, SpaceInvaders.Properties.Resources.ship8);
            game.enemies.AddLine(7, 1, SpaceInvaders.Properties.Resources.ship2);
            game.enemies.AddLine(8, 1, SpaceInvaders.Properties.Resources.ship1);
            

            // add more enemies (2 times more if multiplayer)
            for (int i = 0; i < 2 * (game.multiplayer ? 2 : 1); i++)
            {
                game.enemies.AddLine(5, 1, SpaceInvaders.Properties.Resources.ship1);
            }
        }

        /// <summary>
        /// Add player(s) to the game
        /// </summary>  
        /// <param name="game">game where we add something</param>"
        private static void AddPlayers(Game game)
        {
            // Create the default player
            PlayerSpaceship playerDefault = PlayerSpaceship.GetPlayerDefault(game.gameSize, game.gameID+"-1");
            game.AddNewGameObject(playerDefault);

            // If multiplayer is on, create the alternative player
            if (game.multiplayer)
            {
                PlayerSpaceship playerAlternative = PlayerSpaceship.GetPlayerAlternative(game.gameSize, game.gameID+"-2");  
                game.AddNewGameObject(playerAlternative);
            }
        }

        /// <summary>
        /// Add a boss to the game
        /// </summary>  
        /// <param name="game">game where we add something</param>"
        private static void AddBoss(Game game)
        {
            game.boss = new Boss(new Vecteur2D(game.gameSize.Width / 2, game.gameSize.Height / 4), 5, SpaceInvaders.Properties.Resources.spaceship);
            game.AddNewGameObject(game.boss);
        }

        /// <summary>
        /// generate the starting menu
        /// </summary>  
        private static GameObjects.Decorations.Menu CreateStartMenu()
        {
            List<MenuButton> buttons = new List<MenuButton>
                {
                    new MenuButton(
                                        SpaceInvaders.Properties.Resources.Player1White,
                                        (game) =>
                                        {
                                            game.state = GameState.PLAY;
                                            game.multiplayer = false;
                                            CreateGame(game.gameSize);
                                        }
                                    ),
                    new MenuButton(
                                        SpaceInvaders.Properties.Resources.Player2White,
                                        (game) =>
                                        {
                                            game.state = GameState.PLAY;
                                            game.multiplayer = true;
                                            CreateGame(game.gameSize);
                                        }
                                    )
                };
            return new GameObjects.Decorations.Menu(buttons);
        }

        /// <summary>
        /// generate the end menu
        /// </summary>  
        private static GameObjects.Decorations.Menu CreateEndMenu()
        {
            List<MenuButton> buttons = new List<MenuButton>
                {
                    new MenuButton(
                                        SpaceInvaders.Properties.Resources.RestartWhite,
                                        (game) =>
                                        {
                                            soundManager.PlayBackgroundMusic();
                                            game.state = GameState.STARTMENU;
                                        }
                                    ),
                    new MenuButton(
                                        SpaceInvaders.Properties.Resources.ExitWhite,
                                        (game) =>
                                        {
                                            Application.Exit();
                                        }
                                    )
                };
            return new GameObjects.Decorations.Menu(buttons);

        }

        /// <summary>
        /// generate the pause menu
        /// </summary>  
        private static GameObjects.Decorations.Menu CreatePauseMenu()
        {
            List<MenuButton> buttons = new List<MenuButton>
                {

                new MenuButton(
                                        SpaceInvaders.Properties.Resources.ResumeWhite,
                                        (game) =>
                                        {
                                            soundManager.StopBackgroundMusic();
                                            game.state=GameState.PLAY;
                                        }
                                    ),
                new MenuButton(
                                        SpaceInvaders.Properties.Resources.RestartWhite,
                                        (game) =>
                                        {
                                            soundManager.PlayBackgroundMusic();
                                            game.state = GameState.STARTMENU;
                                        }
                                    ),
                new MenuButton(
                                        SpaceInvaders.Properties.Resources.ExitWhite,
                                        (game) =>
                                        {
                                            Application.Exit();
                                        }
                                    )
                };
            return new GameObjects.Decorations.Menu(buttons);

        }

        #endregion

            #region running
        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            // draw background first
            gameBackground.Draw(this, g); 

            // draw game id when the game is launched
            if (state != GameState.STARTMENU) { g.DrawString("GameID: " + gameID, new Font("Arial", 7), whiteBrush, new PointF(0, gameSize.Height - 15)); } // draw gameID
            
            // draw the game
            switch (state)
            {
                case GameState.STARTMENU:
                    startMenu.Draw(this, g);
                    break;
                   
                case GameState.PLAY:

                    foreach (GameObject gameObject in gameObjects)
                    {
                        gameObject.Draw(this, g);
                    }
                    break;
                case GameState.PAUSE:
                    g.DrawString("PAUSE", defaultFont, whiteBrush, gameSize.Width / 2 - 40, 50);
                    pauseMenu.Draw(this, g);
                    break;
                case GameState.WIN:
                    g.DrawString("WIN", defaultFont, whiteBrush, gameSize.Width / 2 - 25, 50);
                    HighScoreManager.DrawHighScore(g, gameSize);
                    endMenu.Draw(this, g);
                    break;
                case GameState.LOSE:
                    g.DrawString("LOSE", defaultFont, whiteBrush, gameSize.Width / 2 - 25, 50);
                    HighScoreManager.DrawHighScore(g, gameSize);
                    endMenu.Draw(this, g);
                    break;
                 
            }
                 
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            
            // emergency exit
            if (keyPressed.Contains(Keys.Escape)) { Application.Exit(); }

            // state gestion (no switch because wa have the same action for multiple states)
            if (state == GameState.PLAY && keyPressed.Contains(Keys.P)) { state = GameState.PAUSE; ReleaseKey(Keys.P); soundManager.PlayBackgroundMusic(); }

            if (state == GameState.PAUSE) { pauseMenu.Update(this, deltaT);  }

            if (state == GameState.WIN || state == GameState.LOSE) { endMenu.Update(this, deltaT); }

            if (state == GameState.PLAY){ GamePlaying(deltaT); }

            if (state == GameState.STARTMENU) { startMenu.Update(this,deltaT); }
        }

        ///<summary
        /// Mannage the dead objects 
        ///</summary>
        public void ManageTheDead()
        {
            // prepare to remove dead objects
            HashSet<GameObject> toRemove = new HashSet<GameObject>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (!gameObject.IsAlive())
                {
                    // if a player dies, update score
                    if (gameObject is PlayerSpaceship player) HighScoreManager.UpdateScore(player.playerID, player.Score);

                    toRemove.Add(gameObject);
                }

            }

            // check if all the players are dead
            if (gameObjects.Count > 0 && GetPlayersAliveSpaceship().Count == 0)
            {
                state = GameState.LOSE;
                soundManager.PlayLoseSound();
                return;
            }

            //remove dead object
            foreach (GameObject gameObject in toRemove)
            {
                gameObjects.Remove(gameObject);
            }
        }

        /// <summary>
        /// Update and manage the state/objects of the game 
        /// </summary>
        public void UpdateAndManage(double deltaT)
        {
            // update each game object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }

            //manage the dead objects 
            ManageTheDead();

            //add bonuses in game
            AddBonusRandomlyInGame();
        }
        
        /// <summary>
        /// Manage the game when it is in PLAY state
        /// </summary>  
        private void GamePlaying(double deltaT)
        {
            
            //l'ajout d'enemis etant géré au début de la partie il n'est pas nécéssaire de les ajouter
            if (enemies.IsAlive())
            {
                UpdateAndManage(deltaT);
            }
            //une fois les ennemis vaincu ajout du boss et la partie continue
            else if (boss==null || boss.IsAlive())
            {
                if (boss == null)
                {
                    AddBoss(game);
                }
                UpdateAndManage(deltaT);
            }
            //une fois le boss vaincu fin de la partie 
            else 
            {
                state = GameState.WIN; 
                soundManager.PlayWinSound(); 
                foreach (PlayerSpaceship player in GetPlayersAliveSpaceship()) 
                { 
                    player.Score += 1000;
                    HighScoreManager.UpdateScore(player.playerID, player.Score);
                }
                return;
            }
        }

        /// <summary>
        /// Add bonus in the game 
        /// </summary> 
        private void AddBonusRandomlyInGame()
        {
            if (random.NextDouble() < 0.00020)
            {
                bool isFromLeft = random.NextDouble() < 0.5;

                double tirage = random.NextDouble();
                Vecteur2D vec = new Vecteur2D(isFromLeft ? 0 : gameSize.Width, random.NextDouble() / 2 * gameSize.Height);
                if (tirage > 0.6)
                {
                    gameObjects.Add(new HealingBonus(vec, isFromLeft));
                }
                else if (tirage > 0.3)
                {
                    gameObjects.Add(new ScoreBonus(vec, isFromLeft));
                }
                else
                {
                    gameObjects.Add(new ShieldBonus(vec, isFromLeft));
                }


            }
        }
            #endregion

            #region utils
        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        
        /// <summary>
        /// Return the player spaceship if it exists, null otherwise
        /// </summary>  
        public List<PlayerSpaceship> GetPlayersAliveSpaceship()
        {
            List<PlayerSpaceship> players = new List<PlayerSpaceship>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is PlayerSpaceship spaceship && spaceship.IsAlive()){ players.Add(spaceship); }
            }
            return players;
        }
            #endregion
        
        #endregion

    }
}
