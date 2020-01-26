using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Rezervacija
    {
        public Rezervacija()
        {
            total = 0;
            uspesnost = false;
            plateno = false;
        }

        [Key]
        public int RezervacijaId { get; set; }
        [Required(ErrorMessage = "Деновите на изнајмување на возилото  се задолжителни")]
        [Display(Name = "Денови на изнајмување")]
        public int denoviIznajmuvanje { get; set; }

        //koment
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
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "ДатумОд")]
        public DateTime DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "ДатумДо")]
        public DateTime DateTo { get; set; }

        //ova e primer kako mojsh da iskoristis geter vo kontroler 
        //za bilo kakva rabota i najdobro e vo modelot da nema logika nikakva vo konstruktor
        //najprakticno e da se prat vakvi geteri vo HomeController ima primer za upotreba na 
        //geterov
        public double Total
        {
            get
            {
                // ako rezervacijata zapochnala vchera
                if (DateFrom > DateTime.Now.AddDays(-1))
                {
                    return total + 1;
                }


                // ako ne vrati go total
                return total;
            }
        }
    }
}