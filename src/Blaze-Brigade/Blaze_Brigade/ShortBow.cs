namespace Model
{
    namespace WeaponModule
    {
        /// <summary>
        /// Ranged physical Weapon.
        /// </summary>

        /**
        * This class represents a ranged weapon.
        * It implements the Weapon interface.
        */
        public class ShortBow : Weapon
        {
            private weaponType weapType;

            /**
            Constructs a Fireball weapon with stats: 7str, 10skill, 0int, and a range of 1-2 with name Short Bows
            */
            public ShortBow()
            {
                modStr = 7;
                modSkill = 10;
                modInt = 0;
                range = new int[2] { 2, 2 };
                name = "Short Bow";
                weapType = weaponType.Bow;
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



