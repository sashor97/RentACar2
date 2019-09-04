using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class Kategorija
    {
        [Key]
        public int KategorijaId { get; set; }
        [Required(ErrorMessage = "Името е задолжително")]
        [StringLength(20,ErrorMessage = "Максималната големина на името треба да е 20 карактери")]       
        [Display(Name ="Име на категоријата")]
        public string Name { get; set; }
   
        [RegularExpression(@"(B|C|D)",ErrorMessage ="Внеси категорија од тип B,C или D")]
        public string Type { get; set; }

        public List<Vozilo> Vozila { get; set; }

        public Kategorija()
        {
            Vozila = new List<Vozilo>();
        }




    }
}