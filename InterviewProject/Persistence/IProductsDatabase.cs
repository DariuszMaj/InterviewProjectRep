using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewProject.Model;

namespace InterviewProject.Persistence
{
    public interface IProductsDatabase
    {
        Product GetByName(string name,string OperationType);
        Product GetTheCheapest ();
        Product GetTheMostExpensive ();
        Product GetTheNewest ();
        void Add(Product product);
        void Update(string Newname,string price, string description, string name);
        void Delete(Guid id);
    }
}
