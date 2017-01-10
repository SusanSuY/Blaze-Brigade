using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Model;
using View;
using Model.UnitModule;
using Model.MapModule;

namespace Controller
{
    /// <summary>
    /// Handles all user mouse input.
    /// </summary>

    /**
    * This class performs appropriate actions in response to user mouse input by updating the Model. \n
    * It does so by calling GameFunction (which is also in the Controller) to update the Model (state of the game) in reaction to user input.
    * Additionally, it updates GameState (in the Model) in order to reflect an update in the game's current status based upon specific conditions in addition to mouse input. \n
    * All of the mouse handling is done within one function, the updateMouse() function, as mouse handling only consists of checking conditionals to perform calls to the GameFunction (which performs further actions) or an update in the GameState.
    */
    public static class MouseHandler
    {
        private static MouseState lastMouseState;          // this state allows for detection of a single click (not click and hold)
        private static MouseState currentMouseState;

        /**
        Performs appropriate actions in response to mouse input and current state of the game. These actions include updating the GameState and calling the GameFunction to perform further actions. \n\n
            \b Mouse \b Handling \b Actions:
            - No reaction to mouse input if the game currently has an animation on the screen (condition checked using GameState.isAnimating). \n
            - No reaction if mouse clicks occur outside of the game window (invalid mouse click coordinates). \n
            - Scrolling of map corresponding to the position of the mouse relative to the window. Scrolling occurs through calling GameFunction's scrollMap(). \n
            - If any unit is selected and a right-click occurs, deselect it. \n
            - If no unit is selected and a right-click occurs, display the 'End Turn' button. \n
            - If 'End Turn' button is displayed and a left-click occurs on it, call GameFunction's endTurn() to end the current player's turn. \n
            - If screen currently displays 'Game Over', check if a left-click occurs on the 'Exit Game' button. If so, set GameState's exitGameClicked to true. \n
            - If an enemy unit is selected and a left-click occurs, deselect it. \n
            - If a playable unit is selected and a left-click occurs: \n
                -# Check if the click occured on a menu button; if so, update GameState's TurnState. \n
                -# Check if the click occured on a valid moveable node (by checking if GameFunction's pathFinder() function does not return null) and if GameState's TurnState is currently Move. If so, call GameFunction's updateUnitPosition() to move the unit. Else, deselect the unit. \n
                -# Check if the click occured on an enemy unit within attack range (by using GameFunction's isEnemyUnitInRange()) and if GameState's TurnState is currently Attack. If so, prompt the Attack Confirm (by updating GameState's attackConfirmOpen) and set GameState's unitToAttack. Else, deselect the unit. \n
            - If no unit is selected and a left-click occurs, check if the user clicked on a unit. If the user clicked on a playable unit, set GameState's selectedUnit. If the user clicked on an enemy unit, set GameState's selectedEnemyUnit. Else, do nothing.
            \param graph The Graph representing the current game map (see Graph class). Requires the graph in order to verify conditions that may lead to a change in the Model.
            \param camera The camera of the game. Requires the camera in order to scroll the map upon satisfactory circumstances (see Mouse Handling Actions bullet 3).
        */
        public static void updateMouse(Graph graph, Camera camera)
        {
            // do not react to any mouse input if animation is on screen
            if (GameState.isAnimating)
            {
                return;
            }

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // valid coordinates for the mouse (ie. must be inside the game window)
            bool validX = currentMouseState.X < GameState.SCREEN_WIDTH && currentMouseState.X > 0;
            bool validY = currentMouseState.Y < GameState.SCREEN_HEIGHT && currentMouseState.Y > 0;

            // enable scrolling around the map
            GameFunction.scrollMap(camera, currentMouseState.X, currentMouseState.Y);

            // if unit is selected when right-clicked, deselect it
            if (lastMouseState.RightButton == ButtonState.Released
                && currentMouseState.RightButton == ButtonState.Pressed
                && validX && validY && (GameState.selectedUnit != null || GameState.selectedEnemyUnit != null))
            {
                GameFunction.deselectUnit();
                GameState.inventoryOpen = false;
            }

            // if no unit is selected when right-click, display end turn button
            else if (lastMouseState.RightButton == ButtonState.Released
               && currentMouseState.RightButton == ButtonState.Pressed
               && validX && validY)
            {
                GameState.endTurnButton = true;
                GameState.endTurnButtonLocation = new Vector2(currentMouseState.X - camera.Position.X, currentMouseState.Y - camera.Position.Y);
            }

            // checks for single mouse left click inside the game window
            if (lastMouseState.LeftButton == ButtonState.Released
            && currentMouseState.LeftButton == ButtonState.Pressed
            && validX && validY)
            {
                // Note: currentMouseState is the pixel coordinates relative to screen; need to get it relative to map, so need offset based on camera position
                Vector2 mouseClickCoordinates = new Vector2(currentMouseState.X - camera.Position.X, currentMouseState.Y - camera.Position.Y);
                Node nodeClickedOn = graph.getNode(mouseClickCoordinates);

                //if end turn is clicked, ends the current player's turn
                if (GameState.endTurnButton)
                {
                    if (mouseClickCoordinates.X > GameState.endTurnButtonLocation.X &&
                        mouseClickCoordinates.X < GameState.endTurnButtonLocation.X + 128 &&
                        mouseClickCoordinates.Y > GameState.endTurnButtonLocation.Y &&
                        mouseClickCoordinates.Y < GameState.endTurnButtonLocation.Y + 32)
                    {
                        GameFunction.endTurn(camera);
                    }
                    else
                    {
                        GameState.endTurnButton = false;
                    }
                }

                // upon game over screen, detect click on exit game button
                if (GameState.gameOver)
                {
                    if (currentMouseState.X > 370 && currentMouseState.X < 556 &&
                        currentMouseState.Y > 300 && currentMouseState.Y < 396)
                    {
                        GameState.exitGameClicked = true;
                    }
                }

                #region Options when unit is clicked

                // if player clicks when enemy unit is selected, deselect it
                if (GameState.selectedEnemyUnit != null)
                {
                    GameFunction.deselectUnit();
                }

                // if player clicks after unit is already selected ...
                if (GameState.selectedUnit != null)
                {
                    // determine if player has clicked on a menu button; if so, change TurnState
                    if (GameFunction.getMenuButtonClicked(mouseClickCoordinates, camera) != null)
                    {
                        Button menuButton = GameFunction.getMenuButtonClicked(mouseClickCoordinates, camera);
                        GameFunction.buttonAction(menuButton, graph);
                        return;
                    }

                    // if user clicks on a valid end node, move to it
                    if (GameState.TurnState == TurnState.Move)
                    {
                        Game.Instance.Tick();

                        // set variables for path finding
                        Node startNode = graph.getNode(GameState.selectedUnit.Position);
                        Node endNode = graph.getNode(mouseClickCoordinates);
                        LinkedList<Node> path = GameFunction.pathFinder(graph, GameState.selectedUnit, startNode, endNode);

                        // if path finder returns a non-null path, then end node is valid
                        if (path != null)
                        {
                            GameState.selectedUnit.getButtonType(ButtonType.Move).Active = false;   // move button no longer active
                            GameFunction.updateUnitPosition(graph, path);
                            GameState.beforeMove = false;
                            GameState.TurnState = TurnState.Wait;
                        }
                        else
                        {
                            return;    // if user clicks on invalid end node, do nothing
                        }
                    }

                    if (GameState.TurnState == TurnState.Attack) // if a unit is clicked after attack is clicked
                    {
                        // gets enemy unit where user clicked
                        Unit unit = GameFunction.getUnitOnNodeClicked(graph.getNode(mouseClickCoordinates), mouseClickCoordinates, GameState.enemyPlayer);
                        GameState.attackSelect = false;
                        if (unit != null && GameFunction.isEnemyUnitInRange(graph, GameState.selectedUnit, unit))
                        {
                            GameState.unitToAttack = unit;      // set state of attacked unit
                            GameState.attackConfirmOpen = true; // opens attack confirmation
                        }
                        else
                        {
                            GameState.attackSelect = true;
                            return; // does nothing if no enemy unit is clicked
                        }
                    }
                }

                // if player clicks when a unit is not selected ...
                else
                {
                    // if there is a playable, player-owned unit on position clicked, set selected unit status
                    Unit unit = GameFunction.getUnitOnNodeClicked(graph.getNode(mouseClickCoordinates), mouseClickCoordinates, GameState.currentPlayer);
                    if (unit != null && !GameFunction.hasUnitFinishedActions(unit))
                    {
                        GameState.selectedUnit = unit;
                        GameFunction.setMovableNodes(graph, unit);
                        GameState.dropDownMenuOpen = true;
                        if (unit.getButtonType(ButtonType.Move).Active)
                        {
                            GameState.beforeMove = true;
                        }
                    }

                    // if there is an enemy unit on the clicked position, show info screen
                    Unit enemyUnit = GameFunction.getUnitOnNodeClicked(graph.getNode(mouseClickCoordinates), mouseClickCoordinates, GameState.enemyPlayer);
                    if (enemyUnit != null)
                    {
                        GameState.selectedEnemyUnit = enemyUnit;
                    }
                }

                #endregion
            }
        }
    }
}