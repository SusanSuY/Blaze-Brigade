using Model.UnitModule;
using System;

namespace Controller
{
    /// <summary>
    /// This class calculates all damage related calculations
    /// </summary>
    public static class DamageCalculations
    {
        /**
        This method takes in the 2 units, and a boolean on whether attack is physical (false). Damage is then calculated by taking attacker's Str/int, 
        and defender's def/res (where str-def is for physical, and int-res is for magic). If the defending stat is higher, final damage is 0
        as an attack cannot heal.
        *@param attacker The unit performing the attack.
        *@param defender The unit defending against the attack.
        *@param physOrMagic Boolean that tells the controller if it's physical or magical damage to be calculated.
        */
        public static int getDamageDealt(Unit attacker, Unit defender, bool physOrMagic)
        {
            if (physOrMagic == false)
            {
                int damage = attacker.Str - defender.Def;
                if (damage < 0)// to prevent attacks from healing if defense is higher then attack
                {
                    damage = 0;
                }
                return damage; //return physical damage dealt
            }
            else
            {
                return attacker.Int - defender.Res; //else return magical damage dealt
            }
        }

        /**
        This method takes in the 2 units, and returns the hit rate as a percentage out of 100 by taking into account both unit's skill. \n
        This calculation is found according to the equation  [((attackerSkill/10) - (defenderSkill/10) +1) *0.8] *100. \n
        A negative hitrate will be changed to 0.
        *@param attacker The unit performing the attack.
        *@param defender The unit defending against the attack.
        */
        public static int getHitRate(Unit attacker, Unit defender)
        {
            int hitRate = (int)Math.Round((((attacker.Skill / 10.0) - (defender.Skill / 10.0) + 1.0) * 0.8) * 100.0);
            if (hitRate > 100) //to prevent hit rate over 100%
            {
                hitRate = 100;
            }
            else if (hitRate < 0) //to prevent hit rate under 0%
            {
                hitRate = 0;
            }
            return hitRate;
        }

        /**
        This method takes in the 2 units, and returns the crit rate as a percentage out of 100 by taking into account both unit's skill. \n
        This calculation is found according to the equation  [((attackerSkill/10) - (defenderSkill/10) +1) *0.1] *100. \n
        A negative hitrate will be changed to 0.
        *@param attacker The unit performing the attack.
        *@param defender The unit defending against the attack.
        */
        public static int getCritRate(Unit attacker, Unit defender)
        {

            int critRate = (int)Math.Round((((attacker.Skill / 3.0) - (defender.Skill / 3.0) + 1.0) * 0.1) * 100.0);
            if (critRate > 100) //to prevent crit rate over 100%
            {
                critRate = 100;
            }
            else if (critRate < 0) //to prevent crit rate under 0%
            {
                critRate = 0;
            }
            return critRate;
        }

        /**
        This method takes in the 2 units, and determines how many attacks the attacker makes by factoring in both unit's relative speed. 
        If one unit's speed is 4 or more higher then the other unit, hitCount will return 2.
        *@param attacker The unit performing the attack.
        *@param defender The unit defending against the attack.
        */
        public static int getHitCount(Unit attacker, Unit defender)
        {
            if (attacker.Speed > (defender.Speed + 4))
            {
                return 2; //return 2 attack if speed of attacker if 4 higher
            }
            else
            {
                return 1; //else return 1
            }
        }
        /**
        This method factors in damage dealt, hit rate, crit rate, and number of attacks (as in how above functions were calculated) to calculate actual damage dealt.
        Hit and crit is factored in by creating a random number between 0-100, and see if that random number is within the range of the crit or hit rate.
        If it is, then the unit hits with the attack and/or crits. \n
        If an attack misses, damage dealt is 0, otherwise damage is dealt normally. \n
        If an attack crits, damage dealt is x2 regular normal damage. \n
        If numOfAttacks is 1, damage is dealt once. Else if 2, damage is dealt twice.
        *@param attacker The unit performing the attack.
        *@param defender The unit defending against the attack.
        *@param physOrMagic The boolean that tells the controller if it's physical or magical damage to be calculated.
        */
        public static int finalDamage(Unit attacker, Unit defender, bool physOrMagic)
        {
            int rawDamage = getDamageDealt(attacker, defender, physOrMagic);
            int hitRate = getHitRate(attacker, defender);
            int critRate = getCritRate(attacker, defender);
            int numOfAttacks = getHitCount(attacker, defender);
            Random rnd = new Random();
            int hitOrMiss = rnd.Next(0, 101); // creates a number between 0-100
            int critOrNot = rnd.Next(0, 101); // creates a number between 0-100

            if (hitOrMiss > hitRate) //if the random number is greater then the hitrate, the attack misses and 0 is returned
            {
                return 0;
            }
            else
            {
                if (critOrNot > critRate) //if attack doesn't crit
                {
                    return rawDamage * numOfAttacks; //return the damage * number of attacks calculated from above
                }
                else //else if attack crits
                {
                    return rawDamage * numOfAttacks * 2; //returns the damage * number of attacks *2 for critical for damage dealt elsewise
                }
            }
        }
    }
}
