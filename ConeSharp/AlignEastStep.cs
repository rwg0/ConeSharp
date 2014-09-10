using System;
using System.Windows.Forms;
using ASCOM.Interface;

namespace ConeSharp
{
    internal class AlignEastStep : BaseStep
    {
        public override string Title
        {
            get { return "Step 6 : Align star on east side of meridian"; }
        }

        public override string Description
        {
            get
            {
                return "Using a camera or reticule eyepiece, exactly center the star that you selected to goto in step 3. Note that if the mount performs a meridian flip during adjustment, you will need to go back and choose a star further from the meridian.";
            }
        }

        public override string MoreInfo
        {
            get { return "Once alignment is complete, press the 'Next' button."; }
        }

        protected override bool IsOKPosition()
        {
            if (State.Mount.SideOfPier != State.SideOfPierOnGotoEast)
            {
                State.ShowMessage("At this step, the mount must still be on the same side of the pier as in the previous GOTO step.");
                return false;

            }

            return true;
        }

        protected override IStep CreateNextStep()
        {
            return new DisplayResultStep();
        }

        protected override PositionId GetPositionId()
        {
            return PositionId.EastAlign;
        }
    }
}