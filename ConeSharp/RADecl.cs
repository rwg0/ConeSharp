using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConeSharp
{
    class RADecl
    {
        public double RA { get; set; }
        public double RADegrees { get { return RA/24*360; } }
        public double Declination { get; set; }

        static public RADecl operator -(RADecl left, RADecl right)
        {
            return new RADecl() {RA = left.RA - right.RA, Declination = left.Declination - right.Declination};
        }
    }
}
