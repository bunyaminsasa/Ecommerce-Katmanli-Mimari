using Ecommerce.DAL.Entitites;
using Ecommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.ViewModels
{
    public class CartCheckOutVM
    {
        public Orders Orders { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
