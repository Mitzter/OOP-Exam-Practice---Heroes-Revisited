using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private ICollection<IWeapon> models;

        public WeaponRepository()
        {
            models = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => (IReadOnlyCollection<IWeapon>)models;

        public void Add(IWeapon model) => models.Add(model);

        public IWeapon FindByName(string name) => this.models.FirstOrDefault(x => x.Name == name);

        public bool Remove(IWeapon model) => this.models.Remove(model);
    }
}
