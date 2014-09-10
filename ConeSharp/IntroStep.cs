using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConeSharp
{
    class IntroStep : IStep
    {
        private State _state;

        public void Initialize(State state)
        {
            _state = state;
        }

        public string Title { get { return "Introduction"; } }
        public string Description
        {
            get
            {
                return _state.AppName +
                       " will guide you through correcting cone error on your setup. Cone error describes the problem that occurs when the optical axis of the OTA is not parallel to the dovetail. It can usually be corrected by adjusting the bolts at each end of the dovetail.";
            }
        }
        public string MoreInfo { get
        {
            return _state.AppName + " requires a german equatorial style mount which can be controlled via ASCOM. You will also need a reticule eyepiece or a camera to precisely align the target stars. The mount must be at least roughly polar aligned.";
        }
        }
        public IStep OnNext()
        {
            return new SelectScopeStep();
        }

        public void OnButton()
        {

        }

        public string ButtonText { get { return null; } }

        public bool TopMost
        {
            get { return false; }
        }

    }
}
