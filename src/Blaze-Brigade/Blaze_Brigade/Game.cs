using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Form = System.Windows.Forms.Form;
using View;
using Model.UnitModule;
using Model.WeaponModule;
using Model;
using Model.MapModule;
using View.MenuModule;

/// <summary>
/// The controller in MVC. These classes will control how the Model is used, and how the 
/// View will be displayed to the user.
/// </summary>
namespace Controller
{
    /// <summary>
    /// Main Controller for game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region Variables
        public static Game Instance { get; set; }   // gets and sets singleton instance of Game
        GameMenuState currentGameState = GameMenuState.MainMenu;    // game starts in main menu screen
        MainMenu mMenu;             // main menu variable
        HowToPlay tut;              // instruction screen variable
        HowToPlay2 tut2;            // instruction 2 screen variable
        HowToPlay3 tut3;            // instruction 3 screen variable
        Graph graph;                // graph representing the map
        Player player1;
        Player player2;
        Camera camera;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backGround, moveableNode, attackableNode, gameOver, endTurnButton, player1Transition, player2Transition, player1Turn, player2Turn;
        SoundEffect swordAttack, bowAttack, fireAttack, footStep, menuSong, mapSong, gameOverSong;
        SoundEffectInstance MenuSong, MapSong, GameOverSong;
        private SpriteFont font; // custom font
        private SpriteFont largeFont; // custom font 2
        private SpriteFont largestFont; // custom font 3
        private float ElapsedTime = 0f;
        private float damageDealtTime1 = 0f;
        private float damageDealtTime2 = 0f;
        private float menuMusicCounter = 0;
        #endregion

        /**
         * Constructor for the game. Sets Instance to the current game along with initializing XNA features such as GraphicsDeviceManager, Height, Width,
         * Content Loader, and the different menu screens.
        */
        public Game()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameState.SCREEN_HEIGHT;
            graphics.PreferredBackBufferHeight = GameState.SCREEN_WIDTH;
            Content.RootDirectory = "Content";
            //instantiate the game windows
            mMenu = new MainMenu();
            tut = new HowToPlay();
            tut2 = new HowToPlay2();
            tut3 = new HowToPlay3();
        }

        /**
        Initializes the game. The Game screen is invisible until the Menu screen is closed.
        */
        protected override void Initialize()
        {
            Form MyGameForm = (Form)Form.FromHandle(Window.Handle); // creates handle
            MyGameForm.Opacity = 0; // hides the game window until it's set to visible
            base.Initialize();
        }

        /**
        This method Loads all GUI and Map Textures, fonts, and SoundEffect Wav files. An instance is created for the wave files
        that are a song, then isLooped is set to true. Volume for the songs are also adjusted to half the original value. The 
        method will then also call initializeGame() to set up the rest of the game.
        */
        protected override void LoadContent()
        {
            // creates a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load graphic textures
            backGround = Content.Load<Texture2D>("Game_Map");
            moveableNode = Content.Load<Texture2D>("moveableNode");
            attackableNode = Content.Load<Texture2D>("attackableNode");
            gameOver = Content.Load<Texture2D>("exit_game");
            endTurnButton = Content.Load<Texture2D>("End_Button");
            player1Transition = Content.Load<Texture2D>("player1turn");
            player2Transition = Content.Load<Texture2D>("player2turn");
            player1Turn = Content.Load<Texture2D>("player1turn_overlay");
            player2Turn = Content.Load<Texture2D>("player2turn_overlay");

            // load sounds
            swordAttack = Content.Load<SoundEffect>("Sword");
            bowAttack = Content.Load<SoundEffect>("Bow");
            fireAttack = Content.Load<SoundEffect>("Fireball");
            footStep = Content.Load<SoundEffect>("footStep");
            menuSong = Content.Load<SoundEffect>("MenuSong");
            MenuSong = menuSong.CreateInstance();
            MenuSong.Volume = 0.5f;
            MenuSong.IsLooped = true;
            Sounds.playMenuSong(true);
            mapSong = Content.Load<SoundEffect>("BattleTheme");
            MapSong = mapSong.CreateInstance();
            MapSong.Volume = 0.2f;
            MapSong.IsLooped = true;
            gameOverSong = Content.Load<SoundEffect>("MapTheme");
            GameOverSong = gameOverSong.CreateInstance();
            GameOverSong.IsLooped = true;

            // load fonts
            font = Content.Load<SpriteFont>("PixelFont");                   // loads font PixelFont
            largeFont = Content.Load<SpriteFont>("PixelFontLarge");         // loads font PixelFont
            largestFont = Content.Load<SpriteFont>("PixelFontLargest");     // loads font PixelFont

            //initialize rest of the starting game
            initializeGame();
        }

        //initializes players and units
        private void initializeGame()
        {
            // set screen dimensions
            graphics.PreferredBackBufferWidth = GameState.SCREEN_WIDTH;     // width of screen
            graphics.PreferredBackBufferHeight = GameState.SCREEN_HEIGHT;   // height of screen
            graphics.ApplyChanges();        // load images
            camera = new Camera();
            graph = new Graph(50, 32);  // create graph
            setObstacles(graph);
            IsMouseVisible = true;          // sets mouse visibility to true

            // set players
            player1 = new Player();
            player2 = new Player();
            GameState.currentPlayer = (player1);
            GameState.enemyPlayer = (player2);
            GameState.Player1 = player1;
            GameState.Player2 = player2;

            // load character sprite and set position
            player1.addUnit(getNewUnit(UnitType.Warrior, new Vector2(12 * 32f, 12 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Warrior, new Vector2(13 * 32f, 13 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Warrior, new Vector2(13 * 32f, 14 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Warrior, new Vector2(12 * 32f, 15 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Mage, new Vector2(12 * 32f, 13 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Mage, new Vector2(12 * 32f, 14 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Archer, new Vector2(12 * 32f, 11 * 32f), player1));
            player1.addUnit(getNewUnit(UnitType.Archer, new Vector2(12 * 32f, 16 * 32f), player1));
            player2.addUnit(getNewUnit(UnitType.Warrior, new Vector2(35 * 32f, 12 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Warrior, new Vector2(34 * 32f, 13 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Warrior, new Vector2(34 * 32f, 14 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Warrior, new Vector2(35 * 32f, 15 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Mage, new Vector2(35 * 32f, 13 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Mage, new Vector2(35 * 32f, 14 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Archer, new Vector2(35 * 32f, 11 * 32f), player2));
            player2.addUnit(getNewUnit(UnitType.Archer, new Vector2(35 * 32f, 16 * 32f), player2));
            GameFunction.startTurn(GameState.currentPlayer, camera);
        }

        /**
        Updates game in real time at 60 times per second. \n\n
            \b Update \b Components:
            - Checks if player clicks exit on the exit window \n
            - Calls MouseHandler to update mouse position if game is running \n
            - A switch case is used to update the game depending on what current Game State is. \n
            - During Game State Main Menu: \n
                -# Checks if start game button is clicked. In which case the main menu is closed, Menu Song is stopped, Map song is played, main game is set to visible, and Game State is set to Playing. \n
                -# Checks if get instruction button is clicked. In which case the gameState is switched to HowToPlay and instruction screen pops up. \n
                -# Checks if Exit Game is clicked, in which case the game closes. \n
            - During Game State HowToPlay: \n
                -# Checks if next button is clicked. In such case, change game state to HowToPlay2, set the 2nd instruction screen to show, and close the current window. \n
                -# Checks if back button is clicked. In such case, change game state to MainMenu, set the Main Menu to show, and close the current window. \n
                -# Checks if Exit Game is clicked, in which case the game closes. \n
            - During Game State HowToPlay2: \n
                -# Checks if next button is clicked. In such case, change game state to HowToPlay3, set the 2nd instruction screen to show, and close the current window. \n
                -# Checks if back button is clicked. In such case, change game state to HowToPlay1, set the 2nd instruction screen to show, and close the current window. \n
                -# Checks if Exit Game is clicked, in which case the game closes. \n
            - During Game State HowToPlay3: \n
                -# Checks if back button is clicked. In such case, change game state to HowToPlay2, set the 2nd instruction screen to show, and close the current window. \n
                -# Checks if Exit Game is clicked, in which case the game closes. \n
            - During Game State Playing: \n
                -# Loads the map texture \n
                -# If GameFunction isTurnOver is true, switch the current player and other player, set turn transition to true, and reset the camera to the new current player's turn. \n
                -# If transition turn is true, set it to false after 1.5 seconds. \n
                -# If currentPlayerDamagePopUp is true, set it to false after 2.5 seconds. \n
                -# If enemyPlayerDamagePopUp is true, set it to false after 2.5 seconds. \n
                -# Loops over all player1 and 2's units, and removes the ones that are dead. If either player has no units left, isGameOver is set to true, and Song changes from map theme to Game over theme. \n

            \param gameTime The current Game Time.
        */
        protected override void Update(GameTime gameTime)
        {
            #region Exiting Game
            // allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }
            if (IsActive && currentGameState == GameMenuState.Playing)
            {
                // call mouse handler if game window is active
                MouseHandler.updateMouse(graph, camera);
            }
            #endregion

            if (GameState.exitGameClicked)
            {
                Exit();
            }

            #region Game States
            MouseState mouse = Mouse.GetState();
            // lists possible game states
            switch (currentGameState)
            {
                case GameMenuState.MainMenu:        // if mouse is clicked... call method
                    mMenu.Show();                   // show main menu
                    if (mMenu.start == true)        //if New game is selected
                    {
                        mMenu.Hide();               // close Main Menu screen
                        tut.Hide();                 // close How To Play Menu
                        Form GameForm = (Form)Form.FromHandle(Window.Handle);
                        GameForm.Opacity = 100;     // make screen show
                        currentGameState = GameMenuState.Playing; // set game state to Playing
                        Sounds.playMenuSong(false);
                        Sounds.playMapSong(true);
                        break;
                    }
                    if (mMenu.getInstruct() == true)    // if How to Play is selected
                    {
                        mMenu.Hide();                   // hide visibility of menu window
                        currentGameState = GameMenuState.HowToPlay; // change game state to How To Play
                        break;
                    }
                    if (mMenu.quit == true) // if quit clicked
                    {
                        this.Exit();
                        break;
                    }
                    break;
                case GameMenuState.HowToPlay:   // if true.. load new image...
                    tut.Show();                 // set visibility of how to play window to true
                    mMenu.setInstructFalse();
                    if (tut.getNext() == true)  // if How to Play is selected
                    {
                        tut.Hide();             // hide visibility of menu window
                        currentGameState = GameMenuState.HowToPlay2; // change game state to How To Play
                        break;
                    }
                    if (tut.getQuit() == true)  // if quit clicked
                    {
                        tut.setQuitFalse();
                        tut.Hide();             // hide 
                        currentGameState = GameMenuState.MainMenu; // changes state back to main menu
                        break;
                    }
                    break;
                case GameMenuState.HowToPlay2:  // if true.. load new image...
                    tut2.Show();                // set visibility of how to play window to true
                    tut.setNextFalse();
                    if (tut2.getNext() == true) // if How to Play is selected
                    {
                        tut2.Hide();            // hide visibility of menu window
                        currentGameState = GameMenuState.HowToPlay3;    // change game state to How To Play
                        break;
                    }
                    if (tut2.getQuit() == true) // if quit clicked
                    {
                        tut2.setQuitFalse();
                        tut2.Hide();            // hide 
                        currentGameState = GameMenuState.HowToPlay;     // changes state back to main menu
                        break;
                    }
                    break;
                case GameMenuState.HowToPlay3:  // if true.. load new image...
                    tut3.Show();                // set visibility of how to play window to true
                    tut2.setNextFalse();
                    if (tut3.getQuit() == true) // if quit clicked
                    {
                        tut3.setQuitFalse();
                        tut3.Hide();            // hide 
                        currentGameState = GameMenuState.HowToPlay2; // changes state back to main menu
                        break;
                    }
                    break;
                case GameMenuState.Playing:     // if true.. load new image...
                    backGround = Content.Load<Texture2D>("Game_Map"); // load background
                    if (GameFunction.isTurnOver())
                    {
                        Player tempPlayer = GameState.currentPlayer;
                        GameState.currentPlayer = (GameState.enemyPlayer);
                        GameState.enemyPlayer = (tempPlayer);
                        GameState.transitionTurn = true;
                        GameFunction.startTurn(GameState.currentPlayer, camera);
                    }
                    if (GameState.transitionTurn)
                    {
                        ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (ElapsedTime > 1.5)
                        {
                            GameState.transitionTurn = false;
                            ElapsedTime = 0f;
                        }
                    }
                    if (GameState.currentPlayerDamagePopup)
                    {
                        damageDealtTime1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (damageDealtTime1 > 2.5)
                        {
                            GameState.currentPlayerDamagePopup = false;
                            damageDealtTime1 = 0f;
                        }
                    }
                    if (GameState.enemyPlayerDamagePopup)
                    {
                        damageDealtTime2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (damageDealtTime2 > 2.5)
                        {
                            GameState.enemyPlayerDamagePopup = false;
                            damageDealtTime2 = 0f;
                        }
                    }
                    // removes deceased units in Player 1
                    foreach (Unit unit in player1.getUnits())
                    {
                        if (!unit.Alive)
                        {
                            GameFunction.removeUnit(graph, player1, unit);
                            break;
                        }
                        if (GameFunction.isGameOver())
                        {
                            if (menuMusicCounter == 0)
                            {
                                Sounds.playGameOverSong();
                                Sounds.playMapSong(false);
                                menuMusicCounter++;
                            }
                            GameState.gameOver = true;
                            break;
                        }
                    }
                    // removes deceased units in Player 2
                    foreach (Unit unit in player2.getUnits())
                    {
                        if (!unit.Alive)
                        {
                            GameFunction.removeUnit(graph, player2, unit);
                            break;
                        }
                        if (GameFunction.isGameOver())
                        {
                            if (menuMusicCounter == 0)
                            {
                                Sounds.playGameOverSong();
                                Sounds.playMapSong(false);
                                menuMusicCounter++;
                            }
                            GameState.gameOver = true;
                            break;
                        }
                    }
                    break;
            }
            #endregion

            base.Update(gameTime); // repeatedly calls update
        }

        /**
        Draws the game as it updates at 60FPS. \n\n
            \b Draw \b Components \b that \b move \b with \camera:       *Note: Many draw methods although called here will not perform any action should the conditions to draw it not be met.
            - Start spriteBatch.begin, pass in camera transform matrix.
            - Draws background texture. \n
            - Draws all units for both players. \n
            - Draws damage popup. \n
            - If a unit is currently selected and isAnimating is false: \n
                -# draw highlightable nodes. \n
                -# draw dropDownMenu if dropDownMenuOpen is true. \n
                -# Draws inventory drop down menu if inventoryOpen is true. \n
            - redraws unit to be darker at gameOver (method won't redraw unless game is over). \n
            - Draws end turn confirmation button. \n
            \b Draw \b Components \b that \b are \b fixed \b to \game\ screen:      *Note: Many draw methods although called here will not perform any action should the conditions to draw it not be met.
            - If a unit is currently selectedand if attackConfirm is true, draw attackConfirm texture. \n
            - If enemy unit is selected, draw enemy unit info. \n
            - If turnTransition is true, draw the correct turn transition image. \n
            - If it is not game over, draw the label for the current player's turn. \n
            - If it is game over, draw the game over overlay image and buttons. \n
            \param gameTime The current Game Time.
        */
        protected override void Draw(GameTime gameTime)
        {
            // draw elements that are affected by camera (scrollable)
            #region Scrollable Elements
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.TransformMatrix);
            // only draw objects relevent to current game state

            // draw background
            spriteBatch.Draw(backGround, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
            // draws units for player 1 and 2
            DrawClass.DrawUnit(spriteBatch, player1);
            DrawClass.DrawUnit(spriteBatch, player2);
            //draws damage popup
            DrawClass.drawDamagePopup(spriteBatch, font);

            #region When Unit is selected
            // if unit is currently clicked on
            if (GameState.selectedUnit != null)
            {
                Unit unit = GameState.selectedUnit;
                if (!GameState.isAnimating)
                {
                    DrawClass.drawHighlightNodes(spriteBatch, graph, moveableNode, attackableNode);
                    if (GameState.dropDownMenuOpen) // if dropDowMenu should be opened, draw dropDownMenu
                    {
                        DrawClass.drawDropDownMenu(spriteBatch);
                    }
                    if (GameState.inventoryOpen)
                    {
                        DrawClass.drawInventoryMenu(spriteBatch, font);
                    }
                }
            }
            #endregion

            // redraws unit at game over to be darker
            if (GameState.gameOver)
            {
                DrawClass.drawUnitsAtGameOver(spriteBatch);
            }
            // draws end turn button 
            if (GameState.endTurnButton)
            {
                DrawClass.drawEndTurnButton(spriteBatch, endTurnButton);
            }

            spriteBatch.End();      // end spriteBatch
            #endregion

            // draw elements not affected by camera (fixed on window)
            #region Elements Unaffected by Camera
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null);   // Note: we don't pass in camera matrix
            // for when unit is selected
            if (GameState.selectedUnit != null)
            {
                // draw attack confirm menu
                if (GameState.attackConfirmOpen && !GameState.isAnimating)
                {
                    DrawClass.drawAttackConfirm(spriteBatch, font, largeFont, largestFont, graph);
                }
                // draw char info screen menu
                DrawClass.drawInfoScreen(spriteBatch, GameState.selectedUnit, font, largeFont);
            }
            if (GameState.selectedEnemyUnit != null)
            {
                DrawClass.drawInfoScreen(spriteBatch, GameState.selectedEnemyUnit, font, largeFont);
            }
            // draw game over menu
            if (GameState.gameOver)
            {
                DrawClass.drawGameOverMenu(spriteBatch, gameOver, backGround, largestFont);
            }
            // draw turn transition
            if (GameState.transitionTurn)
            {
                DrawClass.drawTurnTransition(spriteBatch, player1Transition, player2Transition);
            }
            if (!GameState.gameOver)
            {
                if (GameState.currentPlayer == player1)
                {
                    DrawClass.DrawPlayerTurn(spriteBatch, 1, player1Turn);
                }
                else
                {
                    DrawClass.DrawPlayerTurn(spriteBatch, 2, player2Turn);
                }
            }
            spriteBatch.End();
            #endregion

            base.Draw(gameTime);    // repeatedly calls draw
        }

        // method to return a unit: takes in unit type (warrior/mage/ranger), unit position, and which player's unit (since different model/colors)
        private Unit getNewUnit(UnitType unitType, Vector2 unitPosition, Player player)
        {
            Button[] unitButtons = new Button[9];
            unitButtons[0] = new Button(ButtonType.Attack, unitPosition, Content.Load<Texture2D>("attack"));
            unitButtons[1] = new Button(ButtonType.Move, unitPosition, Content.Load<Texture2D>("move"));
            unitButtons[2] = new Button(ButtonType.Items, unitPosition, Content.Load<Texture2D>("items")); ;
            unitButtons[3] = new Button(ButtonType.Wait, unitPosition, Content.Load<Texture2D>("wait")); ;
            unitButtons[4] = new Button(ButtonType.AttackConfirm, new Vector2(328, 130), Content.Load<Texture2D>("confirm_attack"));
            unitButtons[5] = new Button(ButtonType.Inventory1, unitPosition, Content.Load<Texture2D>("Inventory_Button"));
            unitButtons[6] = new Button(ButtonType.Inventory2, unitPosition, Content.Load<Texture2D>("Inventory_Button"));
            unitButtons[7] = new Button(ButtonType.Inventory3, unitPosition, Content.Load<Texture2D>("Inventory_Button"));
            unitButtons[8] = new Button(ButtonType.Inventory4, unitPosition, Content.Load<Texture2D>("Inventory_Button"));

            // creates human-like characters for current player
            if (player == GameState.currentPlayer)
            {
                if (unitType == UnitType.Warrior)
                {
                    Unit unit = new Warrior(Content.Load<Texture2D>("warrior"), unitButtons, Content.Load<Texture2D>("warrior_stats"), Content.Load<Texture2D>("warrior_attack"), unitPosition, Content.Load<Texture2D>("HealthBar1"));
                    Weapon startingWeap = new BronzeSword();
                    Weapon startingWeap2 = new IronSword();
                    unit.equippedWeapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory1).weapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory2).weapon = startingWeap2;
                    graph.getNode(unitPosition).unitOnNode = (unit);
                    return unit;
                }
                if (unitType == UnitType.Mage)
                {
                    Unit unit = new Mage(Content.Load<Texture2D>("mage"), unitButtons, Content.Load<Texture2D>("mage_stats"), Content.Load<Texture2D>("mage_attack"), unitPosition, Content.Load<Texture2D>("HealthBar1"));
                    Weapon startingWeap = new Fireball();
                    Weapon startingWeap2 = new Fireblast();
                    unit.equippedWeapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory1).weapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory2).weapon = startingWeap2;
                    graph.getNode(unitPosition).unitOnNode = (unit);
                    return unit;
                }
                if (unitType == UnitType.Archer)
                {
                    Unit unit = new Archer(Content.Load<Texture2D>("archer"), unitButtons, Content.Load<Texture2D>("archer_stats"), Content.Load<Texture2D>("archer_attack"), unitPosition, Content.Load<Texture2D>("HealthBar1"));
                    Weapon startingWeap = new ShortBow();
                    Weapon startingWeap2 = new LongBow();
                    unit.equippedWeapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory1).weapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory2).weapon = startingWeap2;
                    graph.getNode(unitPosition).unitOnNode = (unit);
                    return unit;
                }
            }

            // creates ghoul-like characters for enemy player
            if (player == GameState.enemyPlayer)
            {
                if (unitType == UnitType.Warrior)
                {
                    Unit unit = new Warrior(Content.Load<Texture2D>("2warrior"), unitButtons, Content.Load<Texture2D>("2warrior_stats"), Content.Load<Texture2D>("2warrior_attack"), unitPosition, Content.Load<Texture2D>("HealthBar2"));
                    Weapon startingWeap = new BronzeSword();
                    Weapon startingWeap2 = new IronSword();
                    unit.equippedWeapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory1).weapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory2).weapon = startingWeap2;
                    graph.getNode(unitPosition).unitOnNode = (unit);
                    return unit;
                }
                if (unitType == UnitType.Mage)
                {
                    Unit unit = new Mage(Content.Load<Texture2D>("2mage"), unitButtons, Content.Load<Texture2D>("2mage_stats"), Content.Load<Texture2D>("2mage_attack"), unitPosition, Content.Load<Texture2D>("HealthBar2"));
                    Weapon startingWeap = new Fireball();
                    Weapon startingWeap2 = new Fireblast();
                    unit.equippedWeapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory1).weapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory2).weapon = startingWeap2;
                    graph.getNode(unitPosition).unitOnNode = (unit);
                    return unit;
                }
                if (unitType == UnitType.Archer)
                {
                    Unit unit = new Archer(Content.Load<Texture2D>("2archer"), unitButtons, Content.Load<Texture2D>("2archer_stats"), Content.Load<Texture2D>("2archer_attack"), unitPosition, Content.Load<Texture2D>("HealthBar2"));
                    Weapon startingWeap = new ShortBow();
                    Weapon startingWeap2 = new LongBow();
                    unit.equippedWeapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory1).weapon = startingWeap;
                    unit.getButtonType(ButtonType.Inventory2).weapon = startingWeap2;
                    graph.getNode(unitPosition).unitOnNode = (unit);
                    return unit;
                }
            }
            return null;
        }

        // manually sets obstacle nodes for graph
        private void setObstacles(Graph graph)
        {
            Texture2D obstacleMap = Content.Load<Texture2D>("map1_obstacles");  // get the obstacle map
            Color[] pixelColor = new Color[1];                                  // holds the colour of the node
            Rectangle sourceRectangle;

            // set node as obstacle if the node on the obstacle map is black
            for (int x = 0; x < obstacleMap.Width; x += 32)
            {
                for (int y = 0; y < obstacleMap.Height; y += 32)
                {
                    sourceRectangle = new Rectangle(x, y, 1, 1);
                    obstacleMap.GetData<Color>(0, sourceRectangle, pixelColor, 0, 1);   // get colour of the node (on obstacle map)
                    if (pixelColor[0] == Color.Black)               // if black, set node as an obstacle
                    {
                        graph.getNode(x / 32, y / 32).isObstacle = true;
                    }
                }
            }
        }

        /**
         * This method takes in a string, and returns the correct sound effect corresponding to the string input. The sounds returned are Sword, Bow, and Fire.
        */
        public SoundEffect getSounds(string choice)
        {
            if (choice == "Sword")
            {
                return swordAttack;
            }
            else if (choice == "Bow")
            {
                return bowAttack;
            }
            else if (choice == "Fire")
            {
                return fireAttack;
            }
            else
            {
                return footStep;
            }
        }

        /**
         * This method takes in a string, and returns the song corresponding to the string input. The songs returned are Menu, Map, and gameOverSong.
        */
        public SoundEffectInstance getSong(string choice)
        {
            if (choice == "Menu")
            {
                return MenuSong;
            }
            else if (choice == "Map")
            {
                return MapSong;
            }
            else
            {
                return GameOverSong;
            }
        }
    }
}