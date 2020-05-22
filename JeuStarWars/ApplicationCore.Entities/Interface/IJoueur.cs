using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public  interface IJoueur
    {
         int? PointsMagie { get; set; }
         int? Portee { get; set; }
         int? Degat { get; set; }
    }
}
