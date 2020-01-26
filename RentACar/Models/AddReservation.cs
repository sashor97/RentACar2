using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class AddReservation
    {
        //public int denoviIznajmuvanje { get; set; }

        public int VoziloId { get; set; }

        [Display(Name = "Датум од")]
        public string DateFrom { get; set; }

        [Display(Name = "Датум до")]
        public string DateTo { get; set; }

       
    }
}
