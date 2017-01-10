using Model;
using Controller;
using System.Diagnostics;
using Model.UnitModule;
using Model.WeaponModule;

namespace View
{
    /// <summary>
    /// Sound class containing methods to play all different sounds to be used in the game
    /// </summary>
    public static class Sounds
    {
        /**
        This method takes in a unit, and plays an attacking sound corresponding to the unit's weapon. The weapon sounds include Sword, Bow, and Magic.
        /param unit The unit who's weapon sound will be played.
        */
        public static void attackSound(Unit unit)
        {
            if (unit.equippedWeapon.getWeapType() == weaponType.Sword)
            {
                Game.Instance.getSounds("Sword").Play();
            }
            else if (unit.equippedWeapon.getWeapType() == weaponType.Bow)
            {
                Game.Instance.getSounds("Bow").Play();
            }
            else
            {
                Game.Instance.getSounds("Fire").Play();
            }
        }

        /**
        This method plays a single walking step sound.
        */
        public static void walkingSound()
        {
            Game.Instance.getSounds("Walk").Play();
        }

        /**
        This method takes in the bool play, and either plays the instance of Menu song if true, or stops it from playing if false.
        \param play The Boolean determining if music should be played or stopped.
        */
        public static void playMenuSong(bool play)
        {
            if (play == true)
            {
                Game.Instance.getSong("Menu").Play();
            }
            else
            {
                Game.Instance.getSong("Menu").Dispose();
            }
        }

        /**
        This method takes in the bool play, and either plays the instance of Main Map Song if true, or stops it from playing if false.
        \param play The Boolean determining if music should be played or stopped.
        */
        public static void playMapSong(bool play)
        {
            Debug.WriteLine("playMapSong called");
            if (play == true)
            {
                Game.Instance.getSong("Map").Play();
            }
            else
            {
                Game.Instance.getSong("Map").Dispose();
            }
        }

        /**
        This method plays the Game Over Song.
        */
        public static void playGameOverSong()
        {
            Game.Instance.getSong("GameOver").Play();
        }
    }
}