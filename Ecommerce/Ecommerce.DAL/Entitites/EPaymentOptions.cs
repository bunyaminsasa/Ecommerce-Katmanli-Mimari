using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Entitites
{
    public enum EPaymentOptions
    {
        [Display(Name ="Kredi Kartı İle Ödeme")]
        KrediKartı=1,

        [Display(Name = "Havale / EFT İle Ödeme")]
        HavaleEFT,

        [Display(Name = "Kapıda Ödeme")]
        KapıdaOdeme
    }
}
