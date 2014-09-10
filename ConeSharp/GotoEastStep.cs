using System;
using System.Windows.Forms;
using ASCOM.Interface;

namespace ConeSharp
{
    internal class GotoEastStep : BaseStep
    {
        public override string Title
        {
            get { return "Step 5 : Goto star on east side of meridian"; }
        }

        public override string Description
        {
            get
            {
                return "In your planetarium program, select and goto a bright star near zero declination and just to the east of the meridian.\r\n\r\n" +
                    "Ideally, choose a star within +/- 10 degrees declination and within 20 degrees of the meridian.";
            }
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

            if (lat > 0 && ( az < 150 || az > 180))
            {
                State.ShowMessage("You need to GoTo a star within 30 degrees of the meridian (on the east side).");
                return false;
            }

            if (lat < 0 && (az > 30))
            {
                State.ShowMessage("You need to GoTo a star within 30 degrees of the meridian (on the east side).");
                return false;
            }


            if (State.Mount.SideOfPier == State.SideOfPierOnGotoWest)
            {
                State.ShowMessage("At this step, the mount should have move to the *other* side of the pier.\r\n\r\nSome ASCOM drivers cannot accurately report the correct side of pier, so this could be a false alarm.\r\n\r\nIf you are sure the mount has swapped sides then it is OK to continue.");
        
            }
            State.SideOfPierOnGotoEast = State.Mount.SideOfPier;

            return true;

        }

        protected override IStep CreateNextStep()
        {
            return new AlignEastStep();
        }

        protected override PositionId GetPositionId()
        {
            return PositionId.EastGoto;
        }

    }
}