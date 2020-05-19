using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.services
{
   public static class Helpers
    {
        public static bool Between(this int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
    }
}
