using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> barbarians = new List<IHero>();
            List<IHero> knights = new List<IHero>();

            foreach (var hero in players)
            {
                if (hero.IsAlive == true)
                {
                    if (hero.GetType().Name == "Barbarian")
                    {
                        barbarians.Add(hero);
                    } 
                    else if (hero.GetType().Name == "Knight")
                    {
                        knights.Add(hero);
                    }
                }
            }
            int knightsDead = 0;
            int barbariansDead = 0;
            while (true)
            {
                
                foreach (var knight in knights)
                {
                    if (knight.IsAlive == true)
                    {
                        foreach (var barbarian in barbarians)
                        {
                            if (barbarian.IsAlive == true)
                            {
                                barbarian.TakeDamage(knight.Weapon.DoDamage());
                                if (barbarian.IsAlive == false)
                                {
                                    barbariansDead++;
                                    if (barbariansDead == barbarians.Count)
                                    {
                                        return $"The knights took {knightsDead} casualties but won the battle.";
                                    }
                                }
                            }
                            
                        }
                    }
                }
                foreach (var barbarian in barbarians)
                {
                    if (barbarian.IsAlive == true)
                    {

                        foreach (var knight in knights)
                        {
                            knight.TakeDamage(barbarian.Weapon.DoDamage());
                            if (knight.IsAlive == false)
                            {
                                knightsDead++;
                                if (knightsDead == knights.Count)
                                {
                                    return $"The barbarians took {barbariansDead} casualties but won the battle.";
                                }
                            }

                        }
                    }
                }
                
                
            }
        }
    }
}
