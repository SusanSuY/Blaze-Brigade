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
        public class Fireball : Weapon
        {
            private weaponType weapType;

            /**
            Constructs a Fireball weapon with stats: 1str, 5skill, 5int, and a range of 1-2 with name Fireball tome.
            */
            public Fireball()
            {
                modStr = 1;
                modSkill = 5;
                modInt = 5;
                range = new int[2] { 1, 2 };
                name = "Fireball Tome";
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



