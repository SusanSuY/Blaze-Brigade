using Model.UnitModule;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Represents a Player in the game.
    /// </summary>

    /**
    * Holds information pertaining to a Player in the game.
    */
    public class Player
    {
        private LinkedList<Unit> ownedUnits;    // units that the player owns
                                                // if a unit dies, REMOVE from this list and put into a "dead" pile.
        /**
        Creates a player. Initializes a list of player owned units.
        */
        public Player()
        {
            ownedUnits = new LinkedList<Unit>();
        }

        /**
        Returns all player owned units.
        */
        public LinkedList<Unit> getUnits()
        {
            return ownedUnits;
        }

        /**
        Returns the total number of player owned units.
        */
        public int getNumOfUnits()
        {
            return ownedUnits.Count;
        }

        /**
        Indicates whether the player owns the specified unit.
            \param unit Specified unit.
        */
        public bool ownsUnit(Unit unit)
        {
            if (ownedUnits.Contains(unit))
            {
                return true;
            }
            return false;
        }

        /**
        Adds the specified unit to the player's units.
            \param unit Unit to be added.
        */
        public void addUnit(Unit unit)
        {
            ownedUnits.AddLast(unit);
        }

        /**
       Removes the specified unit from the player's units.
            \param unit Unit to be removed.
        */
        public void removeUnit(Unit unit)
        {
            ownedUnits.Remove(unit);
        }
    }
}

