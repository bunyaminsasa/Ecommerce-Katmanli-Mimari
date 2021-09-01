using Ecommerce.DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Product> LastProducts { get; set; }
        public IEnumerable<Product> SalesProducts { get; set; }
    }
}
