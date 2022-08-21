using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;
        private bool isAlive = true;

        protected Hero(string name, int health, int armour)
        {
            this.Health = health;
            this.Name = name;
            this.Armour = armour;
        }
        public string Name
        {
            get => this.name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Hero name cannot be null or empty.");
                }
                this.name = value;
            }
        }

        public virtual int Health
        {
            get => this.health;
            protected set
            {

                if (value < 0)
                {
                    throw new ArgumentException("Hero health cannot be below 0");
                }
                this.health = value;
            }
        }

        public virtual int Armour
        {
            get => this.armour;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hero armour cannot be below 0");
                }
                this.armour = value;
            }
        }

        public IWeapon Weapon
        {
            get => this.weapon;
            protected set
            {
                if ( value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                this.weapon = value;
            }
        }

        public bool IsAlive
        {
            get => this.isAlive;
            protected set => this.isAlive = true;
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            int damageAfterArmor = this.Armour - points;
            if (this.Armour - points < 0)
            {
                this.Armour = 0;
            }
            else
            {
                this.Armour -= points;
            }
            
            if (this.Armour == 0)
            {
                if (this.Health + damageAfterArmor < 0)
                {
                    this.Health = 0;
                }
                else
                {
                    this.Health += damageAfterArmor;
                }
                
                
                if (this.Health == 0)
                {
                    
                    this.isAlive = false;
                }
            }
            
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {this.Name}");
            sb.AppendLine($"--Health: {this.Health}");
            sb.AppendLine($"--Armour: {this.Armour}");
            sb.AppendLine($"--Weapon: {(this.Weapon != null ? (this.Weapon.Name) : "Unarmed")}");

            return sb.ToString().TrimEnd();
                
        }
    }
}
