using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ASCOM.DeviceInterface;

namespace ConeSharp
{
    internal class State
    {
        public State(IWin32Window form)
        {
            Form = form;
        }

        readonly Dictionary<PositionId, RADecl> _positions = new Dictionary<PositionId, RADecl>();
        private RADecl _westError;
        private RADecl _eastError;
        private double _coneError;


        public void AddPosition(PositionId positionId, double rightAscension, double declination)
        {
            _positions[positionId] = new RADecl() {RA = rightAscension, Declination = declination};
        }

        public RADecl CalculateEastAdjustmentPoint()
        {
            var algined = _positions[PositionId.EastAlign];

            var offsetRA = _coneError/360*24;

            return new RADecl() {Declination = algined.Declination, RA = algined.RA + offsetRA};

        }

        public void CalculateError()
        {
            _westError = _positions[PositionId.WestAlign] - _positions[PositionId.WestGoto];
            _eastError = _positions[PositionId.EastAlign] - _positions[PositionId.EastGoto];

            double outWest = _westError.RADegrees;
            double outEast = _eastError.RADegrees * -1;

            _coneError = (outWest + outEast)/2;

        }

        public double GetConeError()
        {
            return _coneError;
        }

        public ITelescopeV3 Mount { get; set; }

        public IWin32Window Form { get; private set; }

        public string AppName
        {
            get { return "ConeSharp"; }
        }

        public PierSide SideOfPierOnGotoWest { get; set; }

        public PierSide SideOfPierOnGotoEast { get; set; }

        public void ShowMessage(string s)
        {
            MessageBox.Show(Form, s, AppName);
        }

        public RADecl GetPosition(PositionId positionId)
        {
            RADecl result = null;
            _positions.TryGetValue(positionId, out result);
            return result;
        }
    }
}