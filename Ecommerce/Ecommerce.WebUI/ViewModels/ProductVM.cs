using Ecommerce.DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
    }
}
