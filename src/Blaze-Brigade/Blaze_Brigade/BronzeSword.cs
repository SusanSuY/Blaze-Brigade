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
        public class BronzeSword : Weapon
        {
            private weaponType weapType; // weapon type

            /**
            Constructs a bronze sword weapon with stats: str, 5skill, 0int, and a range of 1 with name Bronze Sword.
            */
            public BronzeSword()
            {
                modStr = 5;
                modSkill = 5;
                modInt = 0;
                range = new int[2] { 1, 1 };
                name = "Bronze Sword";
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
