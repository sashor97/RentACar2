using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikId { get; set; }

        [Required(ErrorMessage = "Името е задолжително")]
        [StringLength(12, ErrorMessage = "Максималната големина на името треба да е 12 карактери")]
        [Display(Name = "Име на Корисник")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Презимето е задолжително")]
        [StringLength(20, ErrorMessage = "Максималната големина на презимето треба да е 20 карактери")]
        [Display(Name = "Презиме на Корисник")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Адресата е задолжителна")]
        //[RegularExpression(@"^[a-zA-Z]{2,20} \d{1,4}$", ErrorMessage = "ИмеАдреса број е правилниот формат кој треба да го внесете")]
        [Display(Name = "Адреса на сопственикот")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Годините се задолжителни")]
        [Display(Name = "Години")]
        [Range(18, 120, ErrorMessage = "Корисникот треба да биде полнолетен")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Емаилот е задолжителен")]
        [Display(Name = "Емаил")]
        public string email { get; set; }

        public List<Komentar> Komentari { get; set; }
        public List<Rezervacija> Rezervacii { get; set; }

        public Korisnik()
        {
            Komentari = new List<Komentar>();
            Rezervacii = new List<Rezervacija>();
        }

    }
}