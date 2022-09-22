using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using ASCOM.DriverAccess;

namespace ConeSharp
{
    class SelectScopeStep : IStep
    {
        private State _state;

        public void Initialize(State state)
        {
            _state = state;
        }

        public string Title
        {
            get { return "Step 1 : Select Telescope"; }
        }
        public string Description { get { return "Select an ASCOM Telescope mount and then press 'Next'."; } }
        public string MoreInfo { get { return "Currently Selected : " + GetMountName(); } }

        private static string GetMountName()
        {
            string lastMount = Settings1.Default.LastMount;
            return string.IsNullOrEmpty(lastMount) ? "None" : lastMount;
        }

        public IStep OnNext()
        {
            ITelescopeV3 scope = null;

            if (string.IsNullOrEmpty(Settings1.Default.LastMountId))
            {
                _state.ShowMessage("No telescope mount driver selected.");
                return null;
            }

            try
            {
                scope = CreateScope(Settings1.Default.LastMountId);
            }
            catch (Exception e)
            {
                _state.ShowMessage("Cannot create ASCOM mount driver " + Settings1.Default.LastMount + "\r\nBecause : " + e.Message);
                return null;
            }

            if (scope.AlignmentMode == AlignmentModes.algAltAz)
            {
                _state.ShowMessage(_state.AppName + " will only work with a German Equatorial style mount (as it needs the meridian flip to happen when swapping sides of the meridian).");
                return null;
            }

            try
            {
                scope.Connected = true;
                _state.Mount = scope;
            }
            catch (Exception ex)
            {
                _state.ShowMessage("Could not connect to scope driver : " + ex.Message);
                return null;
            }

            return new ScopePropertiesStep();
        }

        public void OnButton()
        {
            var ch = new Chooser {DeviceType = "Telescope"};
            var device = ch.Choose();

            if (string.IsNullOrEmpty(device))
                return;

            try
            {
                var scope = CreateScope(device);
                Settings1.Default.LastMountId = device;
                Settings1.Default.LastMount = scope.Name;
                Settings1.Default.Save();
            }
            catch (Exception e)
            {
                _state.ShowMessage("Cannot create ASCOM mount driver " + device + "\r\nBecause : " + e.Message);
            }
        }

        private static ITelescopeV3 CreateScope(string device)
        {
            return new Telescope(device);
        }

        public string ButtonText { get { return "Select Mount"; } }

        public bool TopMost
        {
            get { return false; }
        }

    }
}
