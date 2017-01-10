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
        public class LongBow : Weapon
        {
            private weaponType weapType;

            /**
            Constructs a LongBow weapon with stats: 7str, 8skill, 0int, and a range of 2-3 with name Long Bows
            */
            public LongBow()
            {
                modStr = 7;
                modSkill = 8;
                modInt = 0;
                range = new int[2] { 2, 3 };
                name = "Long Bow";
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



