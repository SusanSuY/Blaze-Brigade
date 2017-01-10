using System;

namespace Model
{
    /// <summary>
    /// The module containing all weapon related classes and interfaces.
    /// </summary>
    namespace WeaponModule
    {
        /// <summary>
        /// Weapon Interface to be implemented when creating new weapons.
        /// </summary>

        /**
        * This is the interface to be used when creating new weapons.
        * The only differences in weapons will be their statistics.
        */
        public interface Weapon
        {
            /**
            Returns the name of the weapon.
            */
            String name { get; }

            /**
            Returns the weapon strength.
            */
            int modStr { get; }

            /**
            Returns the weapon intelligence.
            */
            int modInt { get; } //weapon int

            /**
            Returns the weapon skill.
            */
            int modSkill { get; }

            /**
            Return the range of the weapon, where range[minimum range, maximum range].
            */
            int[] range { get; }

            /**
            Returns the weapon type.
            */
            weaponType getWeapType();
        }

        /**
         * The enum for different weapon types.
         */
        public enum weaponType { Sword, Bow, Magic };
    }
}
