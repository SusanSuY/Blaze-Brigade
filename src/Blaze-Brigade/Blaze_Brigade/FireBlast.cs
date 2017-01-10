namespace Model
{
    namespace WeaponModule
    {
        /// <summary>
        /// Ranged Magical Weapon.
        /// </summary>

        /**
        * This class represents a magic based weapon.
        * It implements the Weapon interface.
        */
        public class Fireblast : Weapon
        {
            private weaponType weapType;

            /**
            Constructs a Fireblast weapon with stats: 1str, 3skill, 7int, and a range of 1-2 with name Fireblast tome.
            */
            public Fireblast()
            {
                modStr = 1;
                modSkill = 3;
                modInt = 7;
                range = new int[2] { 1, 2 };
                name = "Fireblast Tome";
                weapType = weaponType.Magic;
            }

            /**
            Returns the weapon strength.
            */
            public int modStr { get; }

            /**
            Returns the weapon intelligence.
            */
            public int modInt { get; }

            /**
            Returns the weapon skill.
            */
            public int modSkill { get; }

            /**
            Returns the name of the weapon.
            */
            public string name { get; }

            /**
            Return the range of the weapon, where range[minimum range, maximum range].
            */
            public int[] range { get; }

            /**
            Returns the weapon type.
            */
            public weaponType getWeapType()
            {
                return weapType;
            }
        }
    }
}



