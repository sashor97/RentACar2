using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Komentar
    {
        [Key]
        public int SopstvenikId { get; set; }

        [Required(ErrorMessage = "Коментарот е задолжителен")]
        [StringLength(100, ErrorMessage = "Максималната големина на описот треба да е 100 карактери")]
        [Display(Name = "Опис на коментарот")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Рејтингот е задолжителен")]
        [Range(1, 10, ErrorMessage = "Максимален рејтинг е 10")]
        [Display(Name = "Рејтинг")]
        public double Rating { get; set; }

        public int VoziloId { get; set; }
        public Vozilo Vozilo { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

    }
}