using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Vozilo
    {
        [Key]
        public int VoziloId { get; set; }

        [Required(ErrorMessage = "Моделот е задолжителен")]
        [StringLength(12, ErrorMessage = "Максималната големина на името моделот треба да е 12 карактери")]
        [Display(Name = "Име на Моделот")]
        public string ModelName { get; set; }

        [Required(ErrorMessage = "Сликата е задолжителна")]
        [Display(Name = "Слика од возилото")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Локацијата е задолжителна")]
        [StringLength(20, ErrorMessage = "Максималната големина на името локацијата треба да е 20 карактери")]
        [Display(Name = "Локација на возилото")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Цената по ден за возилото е задолжителна")]
        [Display(Name = "Цена по ден")]
        public double PriceDay { get; set; }

        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }

        public int ProizvoditelId { get; set; }
        public Proizvoditel Proizvoditel { get; set; }

        public int SopstvenikId { get; set; }
        public Sopstvenik Sopstvenik { get; set; }

        public List<Komentar> Komentari { get; set; }
        public List<Rezervacija> Rezervacija { get; set; }

        public Vozilo()
        {
            Komentari = new List<Komentar>();
            Rezervacija = new List<Rezervacija>();
        }
    }
}