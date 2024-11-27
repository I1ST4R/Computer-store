using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_store.Domain.Entities
{
    public class Provider : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Provider(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
