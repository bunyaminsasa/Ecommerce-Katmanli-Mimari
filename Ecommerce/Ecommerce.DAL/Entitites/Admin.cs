using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Entitites
{
    [Table("Admin")]
    public class Admin
    {
        public int ID { get; set; }

        [StringLength(30),Column(TypeName ="varchar(30)"),Display(Name ="Adı")]
        public string Name { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Display(Name = "Soyadı")]
        public string Surname { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Display(Name = "Kullanıcı Adı"),Required(ErrorMessage ="Bir kullanıcı Adı Giriniz...")]
        public string UserName { get; set; }

        [StringLength(32), Column(TypeName = "varchar(32)"), Display(Name = "Şifre"), Required(ErrorMessage = "Bir Şifre Giriniz...")]
        public string Password { get; set; }

        [Display(Name="En Son Giriş Yaptığı Zaman")]
        public DateTime LastDate { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"),Display(Name = "En Son Giriş Yaptığı IP No")]
        public string LastIPNO{ get; set; }
    }
}
