using System.Windows.Forms;
using ASCOM.DeviceInterface;

namespace ConeSharp
{
    abstract class BaseStep : IStep
    {
        protected State State;
        

        public void Initialize(State state)
        {
            State = state;

        }

        public abstract string Title { get; }
        public abstract string Description { get; }
        public abstract string MoreInfo { get; }
        
        public IStep OnNext()
        {
            if (State.Mount.Slewing)
            {
                State.ShowMessage("Wait until all mount movement is complete before pressing next.");
                return null;    
            }

            if (!State.Mount.Tracking)
            {
                State.ShowMessage("The mount must be tracking to perform the cone error adjustment.");
                return null;    
            }

            if (State.Mount.TrackingRate != DriveRates.driveSidereal)
            {
                State.ShowMessage("The mount must be tracking at sidereal rate to perform the cone error adjustment.");
                return null;
            }


            if (!IsOKPosition())
                return null;

            State.AddPosition(GetPositionId(), State.Mount.RightAscension, State.Mount.Declination);
            return CreateNextStep();
        }

        protected abstract bool IsOKPosition();

        protected abstract IStep CreateNextStep();
        protected abstract PositionId GetPositionId();

        public void OnButton()
        {
            
        }

        public string ButtonText { get { return null; } }

        public bool TopMost
        {
            get { return true; }
        }

    }
}