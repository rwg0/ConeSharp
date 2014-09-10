using System;
using ASCOM.Interface;

namespace ConeSharp
{
    internal class DisplayResultStep : IStep
    {

        private State _state;

        public void Initialize(State state)
        {
            _state = state;

            _state.CalculateError();
        }

        public string Title { get { return "Step 7 : Measured Cone Error"; } }
        public string Description 
        {
            get
            {
                double coneError = _state.GetConeError();
                return string.Format("The measured cone error is {0}, with the front of the telescope pointing {1}.", Utils.FormatDegrees(Math.Abs(coneError)),
                    coneError < 0 ? "out away from the mount" : "in towards the mount");
            }
        }
        public string MoreInfo { get { return "Press 'Next' to begin correcting this error."; } }
        public IStep OnNext()
        {
            return new AdjustConeStep();
        }

        public void OnButton()
        {

        }

        public string ButtonText { get { return null; } }

        public bool TopMost
        {
            get { return true; }
        }

    }

    internal class Utils
    {
        public static string FormatDegrees(double angle)
        {
            var abs = Math.Abs(angle);
            var degrees = Math.Floor(abs);
            var frac = abs - degrees;
            var minutes = Math.Floor(frac*60);
            var bit = frac - (minutes/60);
            var seconds = Math.Floor(bit*3600);
                

            return string.Format("{3}{0}°{1}'{2}\"", degrees, minutes, seconds, angle >=0 ? "" : "-");
        }

        public static object FormatRA(double ra)
        {
            var abs = Math.Abs(ra);
            var degrees = Math.Floor(abs);
            var frac = abs - degrees;
            var minutes = Math.Floor(frac * 60);
            var bit = frac - (minutes / 60);
            var seconds = Math.Floor(bit * 3600);


            return string.Format("{3}{0}h{1}'{2}\"", degrees, minutes, seconds, ra >= 0 ? "" : "-");
        }
    }
}