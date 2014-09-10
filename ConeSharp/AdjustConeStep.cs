namespace ConeSharp
{
    internal class AdjustConeStep : IStep
    {
        private State _state;

        public void Initialize(State state)
        {
            _state = state;

            RADecl pos = _state.CalculateEastAdjustmentPoint();

            _state.Mount.SlewToCoordinates(pos.RA, pos.Declination);
        }

        public string Title
        {
            get { return "Step 8 : Adjust cone error."; }
        }

        public string Description
        {
            get { return "Using only the adjustment screws on the dovetail, put the star back in the centre of the camera or reticule."; }
        }

        public string MoreInfo
        {
            get { return ""; }
        }

        public IStep OnNext()
        {
            return new AllDoneStep();
        }

        public void OnButton()
        {
        }

        public string ButtonText
        {
            get { return null; }
        }

        public bool TopMost
        {
            get { return true; }
        }
    }
}