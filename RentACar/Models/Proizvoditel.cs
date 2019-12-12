using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Proizvoditel
    {
        [Key]
        public int ProizvoditelId { get; set; }

        [Required(ErrorMessage = "Името е задолжително")]
        [StringLength(20, ErrorMessage = "Максималната големина на името треба да е 20 карактери")]
        [MinLength(2, ErrorMessage = "Минималната големина на името треба да е 2 карактери")]
        [Display(Name = "Име на производител")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Годината на производство на возилото е задолжителна")]
        [Range(1900,2019,ErrorMessage = "Годината на производство  треба да е измеѓу 1900 и 2019")]
        [Display(Name = "Година на производство")]
        public int Year { get; set; }

        public List<Vozilo> Vozila { get; set; }
  
        public Proizvoditel()
        {
            Vozila = new List<Vozilo>();
        }

    }
}