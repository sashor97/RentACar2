using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.Models
{
    public class AddComment
    {
        //
        public int VoziloId { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}