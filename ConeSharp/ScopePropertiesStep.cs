using System;

namespace ConeSharp
{
    internal class ScopePropertiesStep : IStep
    {
        private State _state;

        public void Initialize(State state)
        {
            _state = state;
        }

        public string Title { get { return "Step 2 : Set Mount properties"; } }
        public string Description { get { return "IMPORTANT: \r\n\r\nYou *must* turn off any align/sync enhancements in the telescope mount driver. In EQMOD this means checking that the Align point count is at most 1. Click the 'Clear Align Data' button in EQMOD setup to get rid of existing alignment data."; } }
        public string MoreInfo { get { return "Press 'Next' when you have completed setting telescope mount properties"; } }
        public IStep OnNext()
        {

            return new GotoWestStep();
        }

        public void OnButton()
        {
            _state.Mount.Connected = false;
            _state.Mount.SetupDialog();
            _state.Mount.Connected = true;

        }

        public string ButtonText { get { return "Properties"; } }

        public bool TopMost
        {
            get { return true; }
        }

    }
}