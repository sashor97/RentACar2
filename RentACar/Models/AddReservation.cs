using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class AddReservation
    {
        //public int denoviIznajmuvanje { get; set; }

        public int VoziloId { get; set; }
     
        public string DateFrom { get; set; }
        
        public string DateTo { get; set; }

       
    }
}
