using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;
        private IMap map;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
            
        }
        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = heroes.FindByName(name);
            if (hero == null)
            {
                if (type == "Knight")
                {
                    hero = new Knight(name, health, armour);
                    heroes.Add(hero);
                    return $"Successfully added Sir {name} to the collection.";
                }
                else if (type == "Barbarian")
                {
                    hero = new Barbarian(name, health, armour);
                    heroes.Add(hero);
                    return $"Successfully added Barbarian {name} to the collection.";
                }
                else
                {
                    throw new InvalidOperationException("Invalid hero type.");
                }
            }
            else
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
        }
        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = weapons.FindByName(name);
            if (weapon == null)
            {
                if (type == "Mace")
                {
                    weapon = new Mace(name, durability);
                    weapons.Add(weapon);
                    return $"A {type.ToLower()} {name} is added to the collection.";
                }
                else if (type == "Claymore")
                {
                    weapon = new Claymore(name, durability);
                    weapons.Add(weapon);
                    return $"A {type.ToLower()} {name} is added to the collection.";
                }
                else
                {
                    throw new InvalidOperationException("Invalid weapon type.");
                }
            }
            else
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }
        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }
            else if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }
            else if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {hero.Name} is well-armoed.");
            }
            else
            {
                hero.AddWeapon(weapon);
                return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
            }
        }

        public string StartBattle()
        {
            map = new Map();
            ICollection<IHero> heroesToFight = new List<IHero>();
            foreach (var hero in heroes.Models)
            {
                if (hero.IsAlive == true && hero.Weapon != null)
                {
                    heroesToFight.Add(hero);
                }
            }
            
            return map.Fight(heroesToFight);
        }



        public string HeroReport()
        {
            List<IHero> heroesList = heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name).ToList();
            var sb = new StringBuilder();
            foreach (var hero in heroesList)
            {
                sb.AppendLine(hero.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        
    }
}
