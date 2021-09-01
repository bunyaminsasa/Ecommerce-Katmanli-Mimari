using Ecommerce.DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.Areas.admin.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
