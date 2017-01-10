using Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Model;
using Model.MapModule;
using Model.UnitModule;
using System.Collections.Generic;
using System.Linq;

namespace View
{
    /// <summary>
    /// Draw Class containing all the different draw methods
    /// </summary>
    public static class DrawClass
    {
        /**
        This method draws the unit sprites, by taking in spriteBatch, and the Player who's units are to be drawn along with their healthbar. All the player's units will
        then be looped through, and drawn to screen if such unit is alive. The healthbar location is directly above the character x coord of unit +1. 
        The healthbar will be 30 pixels wide, and be scaled in accordance with the unit's current HP by using a rectangle.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param player The player's unit to draw.
        */
        public static void DrawUnit(SpriteBatch spriteBatch, Player player) // draw it all to the screen.
        {
            for (int i = 0; i < player.getNumOfUnits(); i++)
            {
                Unit unit = player.getUnits().ElementAt(i); // gets unit at i
                if (unit.Alive)
                {
                    if ((GameFunction.hasUnitFinishedActions(unit)) && (GameState.currentPlayer == player))
                    {
                        spriteBatch.Draw(unit.getSpriteImage(), unit.PixelCoordinates,
                            unit.getCurrentFrame(), Color.Gray, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                    }
                    else
                    {
                        spriteBatch.Draw(unit.getSpriteImage(), unit.PixelCoordinates,
                            unit.getCurrentFrame(), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                    }
                    Vector2 healthLocation = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y - 4);
                    //draw healthbar
                    spriteBatch.Draw(unit.getHealthBar(), new Rectangle((int)healthLocation.X + 1,
                        (int)healthLocation.Y, (int)(30 * ((double)unit.Hp / unit.getMaxHp())), 5), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.7f);
                }
            }
        }

        /**
        This method draws who's player turn it currently is. The method takes in the texture containing that info, spriteBatch and the int for the player's whose turn it is.
        The method will print the texture for player 1 on left side of screen if it is currently player 1's turn, otherwise it will print it on right side.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param player The current player.
        \param turnInfo The Texture2D containing the text on which player's turn it currently is.
        */
        public static void DrawPlayerTurn(SpriteBatch spriteBatch, int player, Texture2D playerTurn)
        {
            if (player == 1)
            {
                spriteBatch.Draw(playerTurn, new Vector2(50, 50), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, 0.15f);
            }
            else
            {
                spriteBatch.Draw(playerTurn, new Vector2(740, 50), null, Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, 0.15f);
            }
        }

        /**
        Draws the Damage pop up numbers from attacking. If GameState currentPlayerDamagePopup is true, draw the damage dealt by attacking player on top of the enemy unit.
        If GameState enemyPlayerDamagePopup is true, draw the damage received by defender on top of the recipient.
        \n \b Exceptions: \n
            - This function assumes that the last time damage calculation was calculated and stored corresponds to the last attacking and defending unit
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param font the font to be used.
        */
        public static void drawDamagePopup(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (GameState.currentPlayerDamagePopup)
            {
                spriteBatch.DrawString(font, GameState.CurrentPlayerDamageDealt, GameState.lastDefendingUnit.PixelCoordinates + (new Vector2(12, -35)), Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            if (GameState.enemyPlayerDamagePopup)
            {
                spriteBatch.DrawString(font, GameState.EnemyPlayerDamageDealt, GameState.lastAttackingUnit.PixelCoordinates + (new Vector2(12, -35)), Color.White, 0,
                    Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        /**
        Draws the highlighted nodes. If a unit has yet to move, and unit is selected, all moveable nodes are highlighted blue, with the max attack range nodes
        highlighted red. Otherwise if a unit is selected, and has finished moving, only display the attackable nodes from the unit's current position.
        \n \b Exceptions: \n
            - If a unit has no moveable nodes, no squares will be highlighted blue
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param graph The current game graph.
        \param moveableNode The texture for moveableNode.
        \param attackableNode The texture for attackableNode.
        */
        public static void drawHighlightNodes(SpriteBatch spriteBatch, Graph graph, Texture2D moveableNode, Texture2D attackableNode)
        {
            Unit unit = GameState.selectedUnit;
            if (GameState.beforeMove && !GameState.attackSelect) // if unit has yet to move, display the overall move and attack range of unit
            {
                // Highlight movable nodes in blue
                foreach (Node move in GameState.moveableNodes)
                {
                    spriteBatch.Draw(moveableNode, move.getPosition(), null, Color.White * 0.2f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                }
                // Highlight attackable nodes in red
                LinkedList<Node> attackableNodes = GameFunction.getAttackableNodes(graph, unit);

                foreach (Node attack in attackableNodes)
                {
                    if ((!GameState.moveableNodes.Contains(attack)) && (attack.unitOnNode != unit) && (!attack.isObstacle))
                    {
                        spriteBatch.Draw(attackableNode, attack.getPosition(), null, Color.White * 0.2f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                    }
                }
            }
            else // else if unit has already moved, only display the attack range
            {
                LinkedList<Node> attackableNodes = GameFunction.getAttackRangeAfterMoving(graph, unit);
                foreach (Node attack in attackableNodes)
                {
                    spriteBatch.Draw(attackableNode, attack.getPosition(), null, Color.White * 0.2f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
                }
            }
        }

        /**
        Draws all active buttons for the currently selected unit. The 4 possible active button is Attack, Move, Item and Wait.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        */
        public static void drawDropDownMenu(SpriteBatch spriteBatch)
        {
            Unit unit = GameState.selectedUnit;
            unit.setButtonCoordinates(unit.PixelCoordinates);
            Button[] unitButtons = unit.getButtons();
            for (int i = 0; i < 4; i++)
            {
                if (unitButtons[i].Active)
                {
                    spriteBatch.Draw(unitButtons[i].getImage(), unitButtons[i].getPixelCoordinates(), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                }
            }
        }

        /**
        Draws the Inventory Menu for the selected unit. This method will loop through all possible inventory slots, and display them if the
        slot if the button for that slot is both active, and has an item. The name of the item is drawn to screen along with the button.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param font The font used to draw the text.
        */
        public static void drawInventoryMenu(SpriteBatch spriteBatch, SpriteFont font)
        {
            Unit unit = GameState.selectedUnit;
            unit.setButtonCoordinates(unit.PixelCoordinates); // update button positions
            Button[] unitButtons = unit.getButtons(); // for each inventory button
            for (int i = 5; i < 9; i++)
            {
                if (unitButtons[i].Active) // if inventory menu buttons are active
                {
                    if (unitButtons[i].hasItem) // if current menu button actually has an item stored in it
                    {
                        spriteBatch.DrawString(font, unitButtons[i].weapon.name.ToString(), unitButtons[i].getPixelCoordinates() + (new Vector2(15, 5)), Color.Black, 0,
                            Vector2.Zero, 1f, SpriteEffects.None, 0f); // draws item stored in unitButtons[i]
                        spriteBatch.Draw(unitButtons[i].getImage(), unitButtons[i].getPixelCoordinates(), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
                    }
                }
            }
        }

        /**
        Redraws units for game over state. This method loops through all of player 1 and 2's units, 
        and redraws them with a darker shade to match the darker game over screen.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        */
        public static void drawUnitsAtGameOver(SpriteBatch spriteBatch)
        {
            // draws faded units
            #region Player 1 Units
            // redraws units for player 1 in darker shade for game over
            for (int i = 0; i < GameState.Player1.getNumOfUnits(); i++)
            {
                Unit unit = GameState.Player1.getUnits().ElementAt(i); //gets unit at i
                if (unit.Alive)
                {
                    spriteBatch.Draw(unit.getSpriteImage(), unit.PixelCoordinates,
                        unit.getCurrentFrame(), Color.Black * 0.5f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                }
            }
            #endregion

            #region Player 2 Units
            // redraws units for player 2 in darker shade for game over
            for (int i = 0; i < GameState.Player2.getNumOfUnits(); i++)
            {
                Unit unit = GameState.Player2.getUnits().ElementAt(i);
                if (unit.Alive)
                {
                    spriteBatch.Draw(unit.getSpriteImage(), unit.PixelCoordinates,
                        unit.getCurrentFrame(), Color.Black * 0.5f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                }
            }
            #endregion
        }

        /**
        Draws end turn button.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param endTurnButton End turn button texture2D.
        */
        public static void drawEndTurnButton(SpriteBatch spriteBatch, Texture2D endTurnButton)
        {
            spriteBatch.Draw(endTurnButton, GameState.endTurnButtonLocation, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        /**
        Draws the attack confirmation screen. All the damage calculations, 1 set each for each player: AttackType, damageDealt, hitCOunt, hitRate, critRate, HP and equipped weapons 
        are all printed to the screen. To make sure the damage numbers are properly displayed for which player's unit is attacking, and which is defending, the method will
        check for whose player's turn it currently is. The method will also draw the attack confirm button. Negative numbers for stats are handled in DamageClass.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param font The small font to be used.
        \param largeFont The larger font to be used.
        \param largestFont The largest font to be used.
        \param graph The current game graph.
        */
        public static void drawAttackConfirm(SpriteBatch spriteBatch, SpriteFont font, SpriteFont largeFont, SpriteFont largestFont, Graph graph)
        {
            Unit unit = GameState.selectedUnit;
            Unit attackedUnit = GameState.unitToAttack;
            Button confirmButton = unit.getButtonType(ButtonType.AttackConfirm);
            Vector2 attackInfoLocation2 = new Vector2(519, 0);

            // get attack stats
            bool attackType = GameFunction.isMagicalAttack(unit);
            int damageDealt = DamageCalculations.getDamageDealt(unit, attackedUnit, attackType);
            int hitCount = DamageCalculations.getHitCount(unit, attackedUnit);
            int hitRate = DamageCalculations.getHitRate(unit, attackedUnit);
            int critRate = DamageCalculations.getCritRate(unit, attackedUnit);
            // get enemy counter attack stats
            attackType = GameFunction.isMagicalAttack(attackedUnit);
            int damageDealt2 = DamageCalculations.getDamageDealt(attackedUnit, unit, attackType);
            int hitCount2 = DamageCalculations.getHitCount(attackedUnit, unit);
            int hitRate2 = DamageCalculations.getHitRate(attackedUnit, unit);
            int critRate2 = DamageCalculations.getCritRate(attackedUnit, unit);

            #region If player 1's turn
            if (GameState.currentPlayer == GameState.Player1)
            {
                spriteBatch.Draw(unit.getCharAttackInfo(), Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); //draw charAttackInfoBackground texture for current character
                spriteBatch.DrawString(largeFont, damageDealt.ToString(), new Vector2(180, 458), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws damage dealt
                spriteBatch.DrawString(font, " x " + hitCount.ToString(), new Vector2(195, 459), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws hit count
                spriteBatch.DrawString(largeFont, hitRate.ToString() + " %", new Vector2(170, 488), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws hit rate
                spriteBatch.DrawString(largeFont, critRate.ToString() + " %", new Vector2(170, 518), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws crit rate
                spriteBatch.DrawString(largestFont, unit.Hp.ToString(), new Vector2(342, 475), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit health
                spriteBatch.DrawString(largeFont, unit.equippedWeapon.name.ToString(), new Vector2(40, 348), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit's weapon

                spriteBatch.Draw(attackedUnit.getCharAttackInfo(), attackInfoLocation2, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); //draw charAttackInfoBackground for unit being attacked
                spriteBatch.DrawString(largestFont, attackedUnit.Hp.ToString(), new Vector2(862, 475), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws enemy unit health
                spriteBatch.DrawString(largeFont, attackedUnit.equippedWeapon.name.ToString(), new Vector2(715, 348), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws enemy unit's weapon

                if (GameFunction.isEnemyUnitInRange(graph, attackedUnit, unit))
                {
                    spriteBatch.DrawString(largeFont, damageDealt2.ToString(), new Vector2(700, 458), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack damage
                    spriteBatch.DrawString(font, " x " + hitCount2.ToString(), new Vector2(715, 459), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack hit count
                    spriteBatch.DrawString(largeFont, hitRate2.ToString() + " %", new Vector2(690, 488), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack hit rate
                    spriteBatch.DrawString(largeFont, critRate2.ToString() + " %", new Vector2(690, 518), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack crit rate
                }
            }
            #endregion

            #region If player 2's turn
            else
            {
                spriteBatch.Draw(attackedUnit.getCharAttackInfo(), Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); //draw charAttackInfoBackground texture for current character
                spriteBatch.DrawString(largestFont, attackedUnit.Hp.ToString(), new Vector2(342, 475), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws enemy unit health
                spriteBatch.DrawString(largeFont, attackedUnit.equippedWeapon.name.ToString(), new Vector2(40, 348), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit's weapon
                if (GameFunction.isEnemyUnitInRange(graph, attackedUnit, unit))
                {
                    spriteBatch.DrawString(largeFont, damageDealt2.ToString(), new Vector2(180, 458), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack damage 
                    spriteBatch.DrawString(font, " x " + hitCount2.ToString(), new Vector2(195, 459), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack hit count
                    spriteBatch.DrawString(largeFont, hitRate2.ToString() + " %", new Vector2(170, 488), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack hit rate
                    spriteBatch.DrawString(largeFont, critRate2.ToString() + " %", new Vector2(170, 518), Color.DarkBlue, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws counterattack crit rate
                }

                spriteBatch.Draw(unit.getCharAttackInfo(), attackInfoLocation2, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); //draw charAttackInfoBackground for current character
                spriteBatch.DrawString(largestFont, unit.Hp.ToString(), new Vector2(862, 475), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit health 
                spriteBatch.DrawString(largeFont, unit.equippedWeapon.name.ToString(), new Vector2(715, 348), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit's weapon
                spriteBatch.DrawString(largeFont, damageDealt.ToString(), new Vector2(700, 458), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws damage
                spriteBatch.DrawString(font, " x " + hitCount.ToString(), new Vector2(715, 459), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws hit count
                spriteBatch.DrawString(largeFont, hitRate.ToString() + " %", new Vector2(690, 488), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws hit rate
                spriteBatch.DrawString(largeFont, critRate.ToString() + " %", new Vector2(690, 518), Color.DarkRed, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws crit rate

            }
            #endregion

            spriteBatch.Draw(confirmButton.getImage(), confirmButton.getPixelCoordinates(), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
        }

        /**
        Draws character information popup. If it is player 1's turn, the popup will be on the bottom left screen. Otherwise it will show up on right side of the screen.
        The stats Level, Strength, Int, Skill, Speed, Defense, Resistance, HP and charInfoBackground texture are all drawn to screen.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param unit The unit to print information of.
        \param font The small font to be used.
        \param largeFont The larger font to be used.
        */
        public static void drawInfoScreen(SpriteBatch spriteBatch, Unit unit, SpriteFont font, SpriteFont largeFont)
        {
            // draws info screen on the left side of the screen
            if (GameState.Player1.ownsUnit(unit) && !GameState.attackConfirmOpen)
            {
                Vector2 statLocation = new Vector2(170, 535);   // starting location for first stat
                Vector2 statLocation2 = new Vector2(235, 535);  // starting location for first stat
                Vector2 increment = new Vector2(0, 20);         // increment downwards for each stat
                Vector2 infoLocation = new Vector2(20, 513);
                int[] stats = unit.getStats();

                for (int k = 0; k < 4; k++) // for stats - level, str, int, skill,
                {
                    spriteBatch.DrawString(font, stats[k].ToString(), statLocation, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    statLocation = statLocation + increment; //increment downwards
                }
                for (int t = 4; t < 7; t++) // for stats - speed, defense, resistance 
                {
                    spriteBatch.DrawString(font, stats[t].ToString(), statLocation2, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                    statLocation2 = statLocation2 + increment; // increment downwards
                }

                spriteBatch.DrawString(largeFont, unit.Hp.ToString(), new Vector2(278, 532), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit hp
                spriteBatch.Draw(unit.getCharInfo(), infoLocation, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); // draw charInfoBackground texture
            }
            // draws info window on the right side of the screen
            else
            {
                if (!GameState.attackConfirmOpen)
                {
                    Vector2 statLocation = new Vector2(795, 533);   // starting location for first stat
                    Vector2 statLocation2 = new Vector2(860, 533);  // starting location for first stat
                    Vector2 increment = new Vector2(0, 20);         // increment downwards for each stat
                    Vector2 infoLocation = new Vector2(635, 513);
                    int[] stats = unit.getStats();

                    for (int k = 0; k < 4; k++) // for stats - level, str, int, skill,
                    {
                        spriteBatch.DrawString(font, stats[k].ToString(), statLocation, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                        statLocation = statLocation + increment; // increment downwards
                    }
                    for (int t = 4; t < 7; t++) // for stats - speed, defense, resistance 
                    {
                        spriteBatch.DrawString(font, stats[t].ToString(), statLocation2, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
                        statLocation2 = statLocation2 + increment; // increment downwards
                    }

                    spriteBatch.DrawString(largeFont, unit.Hp.ToString(), new Vector2(893, 532), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f); //draws unit HP
                    spriteBatch.Draw(unit.getCharInfo(), infoLocation, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); // draw charInfoBackground texture
                }
            }
        }

        /**
        Draw the Game over menu. A game over button texture, the string "Game Over", which player won, and a darkened background is drawn to screen.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param gameOver The game over button Texture2D.
        \param background The background Texture2D.
        \param largestFont The largest font to be used.
        */
        public static void drawGameOverMenu(SpriteBatch spriteBatch, Texture2D gameOver, Texture2D backGround, SpriteFont largestFont)
        {
            Vector2 gameOverLocation = new Vector2(-370, -300);
            spriteBatch.DrawString(largestFont, "Game Over", new Vector2(350, 150), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f); //draws Game Over Text
            spriteBatch.DrawString(largestFont, "Player " + GameState.winningPlayer + " wins", new Vector2(280, 220), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f); //draws Game Over Text
            spriteBatch.Draw(gameOver, Vector2.Zero, null, Color.White, 0, gameOverLocation, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(backGround, Vector2.Zero, null, Color.Black * 0.5f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.2f);
        }

        /**
        Draws a turn transition image to screen. player1Transition is if GameState is player1, otherwise player2Transition is drawn.
        \param spriteBatch The spitebatch used to draw 2D bitmap to screen.
        \param player1Transition The player 1 transition texture2D.
        \param player2Transition The player 2 transition texture2D.
        */
        public static void drawTurnTransition(SpriteBatch spriteBatch, Texture2D player1Transition, Texture2D player2Transition)
        {
            if (GameState.currentPlayer == GameState.Player1)
            {
                spriteBatch.Draw(player1Transition, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); //draw turn transition 
            }
            else
            {
                spriteBatch.Draw(player2Transition, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.7f); //draw turn transition 
            }
        }
    }
}