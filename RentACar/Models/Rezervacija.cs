using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Rezervacija
    {
        [Key]
        public int RezervacijaId { get; set;}
        [Required(ErrorMessage = "Деновите на изнајмување на возилото  се задолжителни")]
        [Display(Name = "Денови на изнајмување")]
        public int denoviIznajmuvanje { get; set; }

        // comment
        [Display(Name = "Успешност на резервацијата")]
        public Boolean uspesnost { get; set; }
        [Display(Name = "Платено/Неплатено")]
        public Boolean plateno { get; set; }
        [Display(Name = "Вкупна цена за плаќање")]
        public double total { get; set; }

        public int VoziloId { get; set; }
        public Vozilo Vozilo { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public Rezervacija()
        {
            total = 0;
            uspesnost = false;
            plateno = false;
        }







    }
}