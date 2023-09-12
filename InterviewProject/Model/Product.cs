using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProject.Model
{
    public class Product
    {
        public Guid Id { get; }
        public double PlnPrice { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        

        internal Product(double plnPrice, string name, string? description, DateTime created)
        {
            PlnPrice = plnPrice;
            Name = name;
            Description = description;
            Created = created;
            Id= Guid.NewGuid();
        }
    }
}
