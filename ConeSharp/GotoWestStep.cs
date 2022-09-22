using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using ASCOM.DeviceInterface;

namespace ConeSharp
{
    class GotoWestStep : BaseStep
    {
        public override string Title
        {
            get { return "Step 3 : Goto star on west side of meridian"; }
        }

        public override string Description
        {
            get { return "In your planetarium program, select and goto a bright star near zero declination and just to the west of the meridian.\r\n\r\n" + 
                "Ideally, choose a star within +/- 10 degrees declination and within 20 degrees of the meridian."; }
        }

        public override string MoreInfo
        {
            get { return "Once the goto is complete, press the 'Next' button."; }
        }

        protected override bool IsOKPosition()
        {
            var lat = State.Mount.SiteLatitude;


            var decl = State.Mount.Declination;
            var az = State.Mount.Azimuth;

            if (Math.Abs(decl) > 20)
            {
                State.ShowMessage("You need to GoTo a star with a declination between -20 and +20.");
                return false;    
            }

            if (lat > 0 && (az < 180 || az > 210))
            {
                State.ShowMessage("You need to GoTo a star within 30 degrees of the meridian (on the west side).");
                return false;
            }

            if (lat == 0)
            {
                State.ShowMessage("Your mount reports a site latitude of zero - this isn't going to work without a correct latitude because the calculation is different in the southern hemisphere.");
                return false;
            }

            if (lat < 0 && (az < 330))
            {
                State.ShowMessage("You need to GoTo a star within 30 degrees of the meridian (on the west side).");
                return false;
            }

            State.SideOfPierOnGotoWest = State.Mount.SideOfPier;

            return true;
        }

        protected override IStep CreateNextStep()
        {
            return new AlignWestStep();
        }

        protected override PositionId GetPositionId()
        {
            return PositionId.WestGoto;
        }
    }
}
