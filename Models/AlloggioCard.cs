using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Nest.Models
{
    public class AlloggioCard
    {
        public Alloggi Alloggio { get; set; }
        public List<ImmaginiAlloggi> Immagini { get; set; }
    }
}
