using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Model;
using View;
using Microsoft.Xna.Framework;
using Model.UnitModule;
using Model.MapModule;

namespace Controller
{
    /// <summary>
    /// Contains functions that update the Model.
    /// </summary>

    /**
    * This class holds useable functions in the scope of the entire gameplay. Such functions include updates to the Model. This is done through updating of the GameState or parameters passed into a function.
    */
    public static class GameFunction
    {
        /**
        Returns a boolean value indicating whether or not the specified enemy unit is within attack range of the specified unit. \n\n
            \b Exceptions: \n
            - The function does not check whether or not the units specified are actually on opposing sides, and simply assumes so.
            - The function currently does not support null parameters, and requires that the parameters be non-null in order to operate correctly.
            - The function assumes that both units specified have positions, and that the graph is made up of nodes. It also assumes that the positions of both units are valid positions on the graph.
            \param graph Graph representing the current game map. Required in order to determine which Node the units occupy and to give context on the range of the units.
            \param unit Specified playable unit. Assume unit is non-null and has a position that is valid on the graph.
            \param enemyUnit Specified enemy unit that holds interest in whether or not it is within attack range. Assume enemyUnit is non-null and has a position that is valid on the graph.
        */
        public static bool isEnemyUnitInRange(Graph graph, Unit unit, Unit enemyUnit)
        {
            Node attackNode = graph.getNode(enemyUnit.Position.Item1, enemyUnit.Position.Item2);
            return isNodeAttackable(unit, unit.Position.Item1, unit.Position.Item2, attackNode);
        }

        /**
        Must be called upon the start of a new turn. Resets all of the player's units' menu actions (by setting each unit's buttons to Active) and sets up game states.
            It does so by setting Game State's selectedUnit, unitToAttack, dropDownMenuOpen, attackConfirmOpen, and beforeMove. \n\n
            \b Exceptions: \n
            - The function does not support non-null values and assumes that each unit owned by the player has menu buttons.
            \param player Player of the new turn. Assume player is a valid player within the current game and is non-null.
            \param camera The camera of the game. Used to set the camera to show a unit in the current player's turn.
        */
        public static void startTurn(Player player, Camera camera)
        {
            // set all buttons to active
            foreach (Unit unit in player.getUnits())
            {
                Button[] buttons = unit.getButtons();
                for (int i = 0; i < buttons.Count(); i++)
                {
                    if (buttons[i].getButtonType() != ButtonType.Attack
                        || buttons[i].getButtonType() != ButtonType.AttackConfirm)
                    {
                        buttons[i].Active = true;
                    }
                }
            }

            // sets all game states
            GameState.selectedUnit = null;
            GameState.unitToAttack = null;
            GameState.dropDownMenuOpen = false;
            GameState.attackConfirmOpen = false;
            GameState.beforeMove = true;

            if (!isGameOver())
            {
                updateCamera(camera);
            }
        }

        /**
        Returns a boolean value indicating whether or not the specified unit can perform actions. It does so by checking if the unit's AttackConfirm or Wait button is no longer Active. If so, then the unit can no longer perform actions. \n\n
            \b Exceptions: \n
            - The function does not support null parameters.
            - The function requires that the unit has an AttackConfirm and Wait button, and does not behave correctly if these buttons are not available.
            \param unit Specified unit. Assumes unit is non-null and has an AttackConfirm and Wait button.
        */
        public static bool hasUnitFinishedActions(Unit unit)
        {
            if (!unit.isButtonActive(ButtonType.AttackConfirm))
            {
                return true;
            }
            if (!unit.isButtonActive(ButtonType.Wait))
            {
                return true;
            }
            return false;
        }

        /**
        Returns a boolean value indicating whether or not the current turn is over. It does so by checking if each unit in the current turn's player has already performed all actions. \n\n
            \b Exceptions: \n
            - The function assumes that the current player has live units left in the game, and does not behave properly if this is not the case.
        */
        public static bool isTurnOver()
        {
            foreach (Unit unit in GameState.currentPlayer.getUnits())
            {
                if (!hasUnitFinishedActions(unit))
                {
                    return false;
                }
            }
            return true;
        }

        /**
        Returns a boolean value indicating whether or not the game is over, based off win conditions. These conditions include whether or not one of the players in the game has any live units left. \n\n
            \b Exceptions: \n
            - The function requires that Player1 and Player2 stored in GameState are non-null.
            - The function assumes that the two players stored in GameState are both different objects.
        */
        public static bool isGameOver()
        {
            // if player1 does not have anymore units, game is over
            if (GameState.Player1.getNumOfUnits() <= 0)
            {
                GameState.winningPlayer = 2;
                return true;
            }

            // if player2 does not have anymore units, game is over
            if (GameState.Player2.getNumOfUnits() <= 0)
            {
                GameState.winningPlayer = 1;
                return true;
            }

            return false;
        }

        /**
        Returns a boolean value indicating whether or not a specified node is attackable based on the unit's equipped weapon's range. \n
            It does so by first check that the specified node is not an obstacle, then checks that the attackNode is within range. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the unit have an equipped weapon with a range, and does not behave correctly otherwise.
            - The function requires that the attackNode have a position on the game's graph, and does not behave correctly otherwise.
            - The function assumes that unitX and unitY are correspondant to the unit's current or prospective position, and does not behave correctly otherwise.
            \param unit Unit to be checked for attackable nodes.
            \param unitX The x coordinate of the unit (by nodes).
            \param unitY The y coordinate of the unit (by nodes).
            \param attackNode The specified attack node.
        */
        public static bool isNodeAttackable(Unit unit, int unitX, int unitY, Node attackNode)
        {
            int attackX = attackNode.getPositionX();    // coordinates of attack node
            int attackY = attackNode.getPositionY();
            int[] range = unit.equippedWeapon.range;    // range of unit's equipped weapon

            if (attackNode.isObstacle)
            {
                return false;   // if attack node is an obstacle, automatically non-attackable
            }

            // if attack node is within weapon's min and max attack range, then node is attackable
            if (Math.Abs(unitX - attackX) + Math.Abs(unitY - attackY) >= range[0] &&
                Math.Abs(unitX - attackX) + Math.Abs(unitY - attackY) <= range[1])
            {
                return true;
            }

            return false;
        }

        /**
        Returns a LinkedList of Node that the unit can move onto. \n
            It does so by iterating through all nodes on the graph and checking if a path exists from the unit's current position to that node. If a path exists (pathFinder() does not return null), then it adds that node to the LinkedList of moveable nodes. \n\n
            \b Exceptions: \n
            - The function does not handle non-null values.
            - The function requires that the specified unit has a valid position on the graph, and does not behave correctly otherwise.
            - The function requires that the graph be initialized with nodes corresponding to the map of the game, and does not behave correctly otherwise.
            \param graph Graph representing the current game map.
            \param Specified unit.
        */
        public static LinkedList<Node> setMovableNodes(Graph graph, Unit unit)
        {
            LinkedList<Node> moveableNodes = new LinkedList<Node>();
            Node currentNode = graph.getNode(unit.Position);

            // iterate through all nodes in the graph
            for (int x = 0; x < graph.Width; x++)
            {
                for (int y = 0; y < graph.Height; y++)
                {
                    // if a path exists to that node, add it to list of moveable nodes
                    if (pathFinder(graph, unit, currentNode, graph.getNode(x, y)) != null && !graph.getNode(x, y).isOccupied())
                    {
                        moveableNodes.AddLast(graph.getNode(x, y));
                    }
                }
            }

            GameState.moveableNodes = moveableNodes;
            return moveableNodes;
        }

        /**
        Returns a LinkedList of Node that the specified unit can perform an attack on. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the unit has not yet moved, else the function does not behave correctly.
            - The function requires that the graph be initialized with nodes corresponding to the map of the game, and does not behave correctly otherwise.
            \param graph Graph representing the current game map.
            \param unit Specfied unit.
        */
        public static LinkedList<Node> getAttackableNodes(Graph graph, Unit unit)
        {
            LinkedList<Node> attackableNodes = new LinkedList<Node>();

            // determine attackable nodes for each moveable node
            foreach (Node moveableNode in GameState.moveableNodes)
            {
                // iterate through entire grid to determine attackable nodes for moveable node
                for (int x = 0; x < graph.Width; x++)
                {
                    for (int y = 0; y < graph.Height; y++)
                    {
                        if (isNodeAttackable(unit, moveableNode.getPositionX(), moveableNode.getPositionY(), graph.getNode(x, y)) &&
                            !attackableNodes.Contains(graph.getNode(x, y)))
                        {
                            // if node is attackable, add it to list of attackable nodes
                            attackableNodes.AddLast(graph.getNode(x, y));
                        }
                    }
                }
            }

            return attackableNodes;
        }

        /**
        Returns a boolean value indicating whether or not the specified enemy unit is within attack range of the specified unit. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the unit has already moved, else the function does not behave correctly.
            - The function requires that the graph be initialized with nodes corresponding to the map of the game, and does not behave correctly otherwise.
            \param graph Graph representing the current game map.
            \param Specified unit.
        */
        public static LinkedList<Node> getAttackRangeAfterMoving(Graph graph, Unit unit)
        {
            LinkedList<Node> attackableNodes = new LinkedList<Node>();
            Tuple<int, int> currentPosition = unit.Position;
            for (int x = 0; x < graph.Width; x++)
            {
                for (int y = 0; y < graph.Height; y++)
                {
                    if (isNodeAttackable(unit, currentPosition.Item1, currentPosition.Item2, graph.getNode(x, y)))
                    {
                        // if node is attackable, add it to list of attackable nodes
                        attackableNodes.AddLast(graph.getNode(x, y));
                    }
                }
            }

            return attackableNodes;
        }

        /**
        Returns a LinkedList of Node representing the path from the start node to the end node, with a maximum number of moves corresponding to the unit's movability. If no such path is valid, return null. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the graph be initialized with nodes corresponding to the map of the game, and does not behave correctly otherwise.
            - The function requires that the unit have a movabiliy value, else the function cannot function correctly.
            - The function requires that both the start and end Node are valid nodes on the Graph, else the function will not behave correctly.
            \param graph Graph representing the current game map.
            \param unit Unit to move.
            \param start Start Node of the path.
            \param end End Node of the path
        */
        public static LinkedList<Node> pathFinder(Graph graph, Unit unit, Node start, Node end)
        {
            // TODO: (OPTIONAL, IGNORE FOR NOW) incorporate movability of nodes in the calculation

            // if end node is not movable, then there cannot be a path
            if (end.isObstacle || end.isOccupied())
            {
                return null;
            }

            // uses a variation of breadth-first search
            Queue<LinkedList<Node>> paths = new Queue<LinkedList<Node>>();  // queue of paths

            // sets up queue of paths
            LinkedList<Node> startPath = new LinkedList<Node>();
            startPath.AddFirst(start);      // adds the starting node to starting path
            paths.Enqueue(startPath);

            while (paths.Count != 0)    // while queue isn't empty
            {
                LinkedList<Node> path = paths.Dequeue();    // get the first path from the queue
                Node lastNode = path.Last();                // get the last node from the path
                if (lastNode == end)                        // if last node is desired end node, return path
                {
                    return path;
                }
                if (path.Count <= unit.getMovability())     // only consider paths within the unit's movability
                {
                    // add paths with adjacent nodes of last node to queue
                    foreach (Node adjacentNode in getAdjacentNodes(graph, lastNode))
                    {
                        if (!path.Contains(adjacentNode))   // if adjacent node doesn't already exist in the path
                        {
                            LinkedList<Node> newPath = new LinkedList<Node>();
                            // copies path to new path
                            foreach (Node node in path)
                            {
                                newPath.AddLast(node);
                            }
                            // add adjacent node to new path that will be enqueued
                            newPath.AddLast(adjacentNode);
                            paths.Enqueue(newPath);
                        }
                    }
                }
            }

            return null;
        }


        //returns moveable adjacent nodes of the specified node. Used for BFS path finding.
        private static LinkedList<Node> getAdjacentNodes(Graph graph, Node node)
        {
            LinkedList<Node> adjacentNodes = new LinkedList<Node>();
            Node upNode = graph.getNode(node.getPositionX(), node.getPositionY() + 1);
            Node downNode = graph.getNode(node.getPositionX(), node.getPositionY() - 1);
            Node rightNode = graph.getNode(node.getPositionX() + 1, node.getPositionY());
            Node leftNode = graph.getNode(node.getPositionX() - 1, node.getPositionY());

            // up node
            if (!upNode.isOccupied() && !upNode.isObstacle)
            {
                adjacentNodes.AddLast(upNode);
            }

            // down node
            if (!downNode.isOccupied() && !downNode.isObstacle)
            {
                adjacentNodes.AddLast(downNode);
            }

            // left node
            if (!leftNode.isOccupied() && !leftNode.isObstacle)
            {
                adjacentNodes.AddLast(leftNode);
            }

            // right node
            if (!rightNode.isOccupied() && !rightNode.isObstacle)
            {
                adjacentNodes.AddLast(rightNode);
            }

            return adjacentNodes;
        }

        /**
        Removes the specified unit from the game. It does so by removing the unit from the Player player's list of ownedUnits. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the graph be initialized with nodes corresponding to the map of the game, and does not behave correctly otherwise.
            - The function requires that the unit have a position, else the function cannot execute.
            - The function does perform the specified action if the specified unit does not previously belong in the player's list of ownedUnits.
            \param graph Graph representing the current game map.
            \param player The player that owns the unit to be removed.
            \param unit unit to remove from the game.
        */
        public static void removeUnit(Graph graph, Player player, Unit unit)
        {
            graph.getNode(unit.Position).unitOnNode = null;
            player.removeUnit(unit);
        }

        /**
        Deselects any selected unit. The function does so by updating the GameState's selectedUnit and selectedEnemyUnit.
            It also sets the GameState's TurnState, unitToAttack, attackConfirmOpen, dropDownMenuOpen, attackSelect, and inventoryOpen as a secondary effect. \n\n
        */
        public static void deselectUnit()
        {
            GameState.selectedUnit = null;
            GameState.selectedEnemyUnit = null;
            GameState.TurnState = TurnState.Wait;
            GameState.unitToAttack = null;
            GameState.attackConfirmOpen = false;
            GameState.dropDownMenuOpen = false;
            GameState.attackSelect = false;
            GameState.inventoryOpen = false;
        }

        /**
        If an unit exists where user clicked (that belongs to player), return it; else, return null. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the clickedNode be a valid node in the game.
            - The function requires that the positionClicked be a valid coordinate on the game map.
            - The function requires that the player be a valid player in the current game.
            \param clickedNode Node where user has clicked.
            \param positionClicked position (by node) of where the user has clicked.
            \param player Player that is currently moving.
        */
        public static Unit getUnitOnNodeClicked(Node clickedNode, Vector2 positionClicked, Player player)
        {
            for (int i = 0; i < player.getNumOfUnits(); i++)
             {
                Unit unit = player.getUnits().ElementAt(i);
                int unitX = unit.Position.Item1;
                int unitY = unit.Position.Item2;
                int clickedX = (int)Math.Floor(positionClicked.X / 32);
                int clickedY = (int)Math.Floor(positionClicked.Y / 32);
                if (unitX == clickedX && unitY == clickedY)
                {
                    return unit;
                }
            }
            return null;
        }

        /**
        Moves the selected unit's position to the clicked position. \n
            Note that the function also sets GameState's isAnimating during the movement animation. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that the graph be initialized with nodes corresponding to the map of the game, and does not behave correctly otherwise.
            - The function requires that the path consist of valid nodes on the graph.
            \param graph Graph representing the current game map.
            \param path Path to move the unit along.
        */
        public static void updateUnitPosition(Graph graph, LinkedList<Node> path)
        {
            Unit unit = GameState.selectedUnit;
            GameState.isAnimating = true;

            // updates the unit's position to each node in the path (node by node)
            foreach (Node node in path)
            {
                Animation.animateUnitPosition(graph, unit, node);
                unit.Position = (new Tuple<int, int>(node.getPositionX(), node.getPositionY()));
                node.unitOnNode = (unit);
                GameState.dropDownMenuOpen = true;
            }

            GameState.isAnimating = false;
        }

        /**
        Ends the current player's turn and starts the enemy player's turn. It does so by updating the GameState's currentPlayer and enemyPlayer.
            It also calls startTurn() and updates the GameState's endTurnButton and transitionTurn to activate the animation of turn transitioning.
            \param camera The camera of the game. Used to call startTurn after performing all actions to end the current turn.
        */
        public static void endTurn(Camera camera)
        {
            Player tempPlayer = GameState.currentPlayer;
            GameState.currentPlayer = (GameState.enemyPlayer);
            GameState.enemyPlayer = (tempPlayer);
            startTurn(GameState.currentPlayer, camera);
            GameState.endTurnButton = false;
            GameState.transitionTurn = true;
        }

        /**
        Updates the Model in correspondence to unit menu button clicks. It does so by updating GameState's TurnState to the button that was clicked. \n
            In addition, specific actions for each button include:
                - If Attack is clicked, updates GameState's dropDownMenuOpen and attackSelect.
                - If AttackConfirm is clicked, updates the unit's Active button states, unit stats (including damage to HP), as well as several GameState variables. 
                - If Move is clicked, updates GameState's dropDownMenuOpen.
                - If Items is clicked, updates GameState's inventoryOppen.
                - If Wait is clicked, deselects the unit and updates the Wait button's Active status.
                - If any of the items in the inventory are clicked, causes the unit to equip the item and update the GameState's inventoryOpen. \n\n
            \b Exceptions: \n
                - The function does not handle non-null parameters.
                - The function assumes that the GameState's selectedUnit is non-null, else changes made by this function will cause other functions to behave incorrectly.
            \param button Button that was clicked.
            \param graph Graph of the map.
        */
        public static void buttonAction(Button button, Graph graph)
        {
            Unit unit = GameState.selectedUnit;
            Unit enemyUnit = GameState.unitToAttack;

            switch (button.getButtonType())
            {
                #region Attack Button
                // if attack clicked
                case ButtonType.Attack:
                    GameState.TurnState = TurnState.Attack;
                    GameState.dropDownMenuOpen = false; // close the dropdown menu when selecting who to attack
                    GameState.attackSelect = true;
                    GameState.inventoryOpen = false;    // closes secondary inventory menu
                    break;
                #endregion

                #region Attack Confirm Button
                // if confirm attack clicked
                case ButtonType.AttackConfirm:
                    unit.getButtonType(ButtonType.Move).Active = false;     // move button is no longer active
                    unit.getButtonType(ButtonType.Attack).Active = false;   // attack button is no longer active

                    //Gets attack animation direction
                    #region Attack Animation Direction
                    Direction attackDir = Direction.Up;
                    Direction counterAttackDir = Direction.Up;
                    if (unit.Position.Item1 < enemyUnit.Position.Item1) // attack right
                    {
                        attackDir = Direction.Right;
                        counterAttackDir = Direction.Left;
                        unit.currentFrame = 7;
                        enemyUnit.currentFrame = 4;
                    }
                    else if (unit.Position.Item1 > enemyUnit.Position.Item1) // attack left
                    {
                        attackDir = Direction.Left;
                        counterAttackDir = Direction.Right;
                        unit.currentFrame = 4;
                        enemyUnit.currentFrame = 7;
                    }
                    else if (unit.Position.Item2 < enemyUnit.Position.Item2) // attack down
                    {
                        attackDir = Direction.Down;
                        counterAttackDir = Direction.Up;
                        unit.currentFrame = 1;
                        enemyUnit.currentFrame = 10;
                    }
                    else if (unit.Position.Item2 > enemyUnit.Position.Item2) // attack up
                    {
                        attackDir = Direction.Up;
                        counterAttackDir = Direction.Down;
                        unit.currentFrame = 10;
                        enemyUnit.currentFrame = 1;
                    }
                    #endregion

                    bool magicalAttack = isMagicalAttack(unit); // find what attack type unit is making
                    int damageDealt = DamageCalculations.finalDamage(unit, enemyUnit, magicalAttack); // gets damage dealt
                    GameState.lastAttackingUnit = unit;
                    GameState.lastDefendingUnit = enemyUnit;
                    GameState.CurrentPlayerDamageDealt = damageDealt.ToString();
                    enemyUnit.Hp = enemyUnit.Hp - damageDealt; // updates hp
                    GameState.currentPlayerDamagePopup = true;
                    Sounds.attackSound(unit);
                    Animation.attackAnimation(attackDir, unit);

                    // if enemy unit is still alive, perform a counter attack
                    if (enemyUnit.Alive && isEnemyUnitInRange(graph, enemyUnit, unit))
                    {
                        Thread.Sleep(750);
                        magicalAttack = isMagicalAttack(enemyUnit); // gets attack type 
                        int damageTaken = DamageCalculations.finalDamage(enemyUnit, unit, magicalAttack); // damage dealt
                        GameState.EnemyPlayerDamageDealt = damageTaken.ToString();
                        unit.Hp = unit.Hp - damageTaken; // update hp
                        GameState.enemyPlayerDamagePopup = true;
                        Sounds.attackSound(enemyUnit);
                        Animation.attackAnimation(counterAttackDir, enemyUnit);
                    }
                    deselectUnit();
                    button.Active = false;
                    break;
                #endregion

                #region Move Button
                // if Move is clicked
                case ButtonType.Move:
                    if (button.Active)          // if unit hasn't already moved
                    {
                        GameState.TurnState = TurnState.Move;
                        GameState.dropDownMenuOpen = false;   // close the dropdownmenu when selecting where to move
                        GameState.inventoryOpen = false;    // closes secondary inventory menu
                    }
                    break;
                #endregion

                #region Item Button
                case ButtonType.Items:          // if item is clicked
                    GameState.TurnState = TurnState.Items;
                    GameState.inventoryOpen = true;
                    break;
                #endregion

                #region Wait Button
                // if wait is clicked
                case ButtonType.Wait:
                    GameState.TurnState = TurnState.Wait;
                    deselectUnit();
                    button.Active = false;
                    break;
                #endregion

                #region Inventory Buttons
                case ButtonType.Inventory1:
                    if ((GameState.inventoryOpen) && (button.hasItem))
                    {
                        GameState.inventoryOpen = false;
                        unit.equippedWeapon = button.weapon;
                    }
                    break;
                case ButtonType.Inventory2:
                    if ((GameState.inventoryOpen) && (button.hasItem))
                    {
                        GameState.inventoryOpen = false;
                        unit.equippedWeapon = button.weapon;
                    }
                    break;
                case ButtonType.Inventory3:
                    if ((GameState.inventoryOpen) && (button.hasItem))
                    {
                        GameState.inventoryOpen = false;
                        unit.equippedWeapon = button.weapon;
                    }
                    break;
                case ButtonType.Inventory4:
                    if ((GameState.inventoryOpen) && (button.hasItem))
                    {
                        GameState.inventoryOpen = false;
                        unit.equippedWeapon = button.weapon;
                    }
                    break;
                #endregion

                default:
                    break;
            }
        }

        /**
        Returns a boolean value indicating whether or not the unit performed a magical attack. This value is dependent on the unit's UnitType. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters, else the behaviour of the function will not be correct.
           \param unit Unit to check.
       */
        // TODO: change this function to factor the weapon type rather than unit type
        public static bool isMagicalAttack(Unit unit)
        {
            if ((unit.getClass() == UnitType.Warrior) || (unit.getClass() == UnitType.Archer))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /**
        Returns the menu button that was clicked; if no menu button was clicked, returns null. \n\n
            \b Exceptions: \n
            - The function does not handle non-null parameters.
            - The function requires that mouseCoordinates be valid coordinates within the game window, else the function does not perform as expected.
            \param mouseCoordinates Coordinates of the mouse click.
            \param camera The camera of the game.
        */
        public static Button getMenuButtonClicked(Vector2 mouseCoordinates, Camera camera)
        {
            Unit unit = GameState.selectedUnit;
            Button[] menuButtons = unit.getButtons();
            int clickX = (int)mouseCoordinates.X;   // coordinates relative to map
            int clickY = (int)mouseCoordinates.Y;
            int stationaryClickX = (int)mouseCoordinates.X + (int)camera.Position.X;    // coordinates relative to window
            int stationaryClickY = (int)mouseCoordinates.Y + (int)camera.Position.Y;

            for (int i = 0; i < menuButtons.Length; i++)
            {
                int buttonX = (int)menuButtons[i].getPixelCoordinates().X;
                int buttonY = (int)menuButtons[i].getPixelCoordinates().Y;
                if (buttonX <= clickX && clickX < buttonX + 128 && buttonY <= clickY && clickY < buttonY + 32)
                {
                    if (GameState.dropDownMenuOpen)
                    {
                        return menuButtons[i];
                    }
                }
            }

            int ButtonX = (int)unit.getButtonType(ButtonType.AttackConfirm).getPixelCoordinates().X;
            int ButtonY = (int)unit.getButtonType(ButtonType.AttackConfirm).getPixelCoordinates().Y;
            if (ButtonX + 90 <= stationaryClickX && stationaryClickX < ButtonX + 214 &&
                ButtonY + 127 <= stationaryClickY && stationaryClickY < ButtonY + 172)
            {
                if (GameState.attackConfirmOpen)
                {
                    return unit.getButtonType(ButtonType.AttackConfirm);
                }
            }
            return null;
        }

        /**
        Must be called upon the start of a new turn. Updates the camera position to focus on a unit of the current turn's player.
            \param camera The camera of the game.
        */
        private static void updateCamera(Camera camera)
        {
            const int boundaryX = -640;
            const int boundaryY = -320;
            Unit unit = GameState.currentPlayer.getUnits().First();     // get first unit in current turn's player units

            // set camera to coordinates of the unit
            camera.Position = new Vector2((int)(-(unit.PixelCoordinates.X / 2.5f + (unit.PixelCoordinates.X / 2.5f - 320))), (int)(-(unit.PixelCoordinates.Y / 3 + (unit.PixelCoordinates.Y / 3 - 160))));

            // keep camera within map boundaries
            if (camera.Position.X > 0)
            {
                camera.Position = new Vector2(0, camera.Position.Y);
            }

            if (camera.Position.X < boundaryX)
            {
                camera.Position = new Vector2(boundaryX, camera.Position.Y);
            }

            if (camera.Position.Y > 0)
            {
                camera.Position = new Vector2(camera.Position.X, 0);
            }

            if (camera.Position.Y < boundaryY)
            {
                camera.Position = new Vector2(camera.Position.X, boundaryY);
            }
        }

        /**
        Enables scrolling of the map based on mouse position. The map scrolls in the direction of the mouse position if the mouse position is at the edge/outside of the window and if the scrolling direction has not yet reached the end of the map.
            \param camera The camera of the game.
            \param mouseX x-coordinate of the mouse position.
            \param mouseY y-coordinate of the mouse position.
        */
        public static void scrollMap(Camera camera, int mouseX, int mouseY)
        {
            const int scrollSpeed = 3;      // change in pixels while scrolling
            const int boundaryX = -640;
            const int boundaryY = -320;

            if (Game.Instance.IsActive)     // if game window is in focus
            {
                // scroll map to the left
                if (mouseX <= 0 && camera.Position.X < 0)
                {
                    camera.Position = new Vector2(camera.Position.X + scrollSpeed, camera.Position.Y);
                }

                // scroll map to the right
                if (mouseX >= GameState.SCREEN_WIDTH && camera.Position.X > boundaryX)
                {
                    camera.Position = new Vector2(camera.Position.X - scrollSpeed, camera.Position.Y);
                }

                // scroll map downwards
                if (mouseY >= GameState.SCREEN_HEIGHT && camera.Position.Y > boundaryY)
                {
                    camera.Position = new Vector2(camera.Position.X, camera.Position.Y - scrollSpeed);
                }

                // scroll map upwards
                if (mouseY <= 0 && camera.Position.Y < 0)
                {
                    camera.Position = new Vector2(camera.Position.X, camera.Position.Y + scrollSpeed);
                }
            }
        }
    }
}