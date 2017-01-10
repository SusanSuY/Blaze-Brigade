namespace Model
{
    namespace WeaponModule
    {
        /// <summary>
        /// Melee Physical Weapon.
        /// </summary>

        /**
        * This class represents a melee weapon.
        * It implements the Weapon interface.
        */
        public class IronSword : Weapon
        {
            private weaponType weapType;

            /**
            Constructs a Iron Sword weapon with stats: 7str, 3skill, 0int, and a range of 1 with name Iron Sword.
            */
            public IronSword()
            {
                modStr = 7;
                modSkill = 3;
                modInt = 0;
                range = new int[2] { 1, 1 };
                name = "Iron Sword";
                weapType = weaponType.Sword;
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
