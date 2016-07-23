using System;
using System.Collections;
using ASCOM.DeviceInterface;
using ASCOM.Interface;
using AlignmentModes = ASCOM.Interface.AlignmentModes;
using DriveRates = ASCOM.Interface.DriveRates;
using EquatorialCoordinateType = ASCOM.Interface.EquatorialCoordinateType;
using GuideDirections = ASCOM.Interface.GuideDirections;
using IAxisRates = ASCOM.Interface.IAxisRates;
using IRate = ASCOM.Interface.IRate;
using ITrackingRates = ASCOM.Interface.ITrackingRates;
using PierSide = ASCOM.Interface.PierSide;
using TelescopeAxes = ASCOM.Interface.TelescopeAxes;

namespace ConeSharp
{
    class TelescopeV3Proxy  : ITelescope
    {
        private readonly ITelescopeV3 _impl;

        public TelescopeV3Proxy(ITelescopeV3 impl)
        {
            _impl = impl;
        }

        public void AbortSlew()
        {
            _impl.AbortSlew();
        }

        public IAxisRates AxisRates(TelescopeAxes axis)
        {
            return new AxisRatesV3Proxy(_impl.AxisRates((ASCOM.DeviceInterface.TelescopeAxes) axis));
        }

        public bool CanMoveAxis(TelescopeAxes axis)
        {
            return _impl.CanMoveAxis((ASCOM.DeviceInterface.TelescopeAxes) axis);
        }

        public PierSide DestinationSideOfPier(double rightAscension, double declination)
        {
            return (PierSide) _impl.DestinationSideOfPier(rightAscension, declination);
        }

        public void FindHome()
        {
            _impl.FindHome();
        }

        public void MoveAxis(TelescopeAxes axis, double rate)
        {
            _impl.MoveAxis((ASCOM.DeviceInterface.TelescopeAxes) axis, rate);
        }

        public void Park()
        {
            _impl.Park();
        }

        public void PulseGuide(GuideDirections direction, int duration)
        {
            _impl.PulseGuide((ASCOM.DeviceInterface.GuideDirections) direction, duration);
        }

        public void SetPark()
        {
            _impl.SetPark();
        }

        public void SetupDialog()
        {
            _impl.SetupDialog();
        }

        public void SlewToAltAz(double azimuth, double altitude)
        {
            _impl.SlewToAltAz(azimuth, altitude);
        }

        public void SlewToAltAzAsync(double azimuth, double altitude)
        {
            _impl.SlewToAltAzAsync(azimuth, altitude);
        }

        public void SlewToCoordinates(double rightAscension, double declination)
        {
            _impl.SlewToCoordinates(rightAscension, declination);
        }

        public void SlewToCoordinatesAsync(double rightAscension, double declination)
        {
            _impl.SlewToCoordinatesAsync(rightAscension, declination);
        }

        public void SlewToTarget()
        {
            _impl.SlewToTarget();
        }

        public void SlewToTargetAsync()
        {
            _impl.SlewToTargetAsync();
        }

        public void SyncToAltAz(double azimuth, double altitude)
        {
            _impl.SyncToAltAz(azimuth, altitude);
        }

        public void SyncToCoordinates(double rightAscension, double declination)
        {
            _impl.SyncToCoordinates(rightAscension, declination);
        }

        public void SyncToTarget()
        {
            _impl.SyncToTarget();
        }

        public void Unpark()
        {
            _impl.Unpark();
        }

        public void CommandBlind(string command, bool raw = false)
        {
            _impl.CommandBlind(command, raw);
        }

        public bool CommandBool(string command, bool raw = false)
        {
            return _impl.CommandBool(command, raw);
        }

        public string CommandString(string command, bool raw = false)
        {
            return _impl.CommandString(command, raw);
        }

        public AlignmentModes AlignmentMode => (AlignmentModes) _impl.AlignmentMode;

        public double Altitude => _impl.Altitude;

        public double ApertureArea => _impl.ApertureArea;

        public double ApertureDiameter => _impl.ApertureDiameter;

        public bool AtHome => _impl.AtHome;

        public bool AtPark => _impl.AtPark;

        public double Azimuth => _impl.Azimuth;

        public bool CanFindHome => _impl.CanFindHome;

        public bool CanPark => _impl.CanPark;

        public bool CanPulseGuide => _impl.CanPulseGuide;

        public bool CanSetDeclinationRate => _impl.CanSetDeclinationRate;

        public bool CanSetGuideRates => _impl.CanSetGuideRates;

        public bool CanSetPark => _impl.CanSetPark;

        public bool CanSetRightAscensionRate => _impl.CanSetRightAscensionRate;

        public bool CanSetPierSide => _impl.CanSetPierSide;

        public bool CanSetTracking => _impl.CanSetTracking;

        public bool CanSlew => _impl.CanSlew;

        public bool CanSlewAltAz => _impl.CanSlewAltAz;

        public bool CanSlewAltAzAsync => _impl.CanSlewAltAzAsync;

        public bool CanSlewAsync => _impl.CanSlewAsync;

        public bool CanSync => _impl.CanSync;

        public bool CanSyncAltAz => _impl.CanSyncAltAz;

        public bool CanUnpark => _impl.CanUnpark;

        public bool Connected
        {
            get { return _impl.Connected; }
            set { _impl.Connected = value; }
        }

        public double Declination => _impl.Declination;

        public double DeclinationRate
        {
            get { return _impl.DeclinationRate; }
            set { _impl.DeclinationRate = value; }
        }

        public string Description => _impl.Description;

        public bool DoesRefraction
        {
            get { return _impl.DoesRefraction; }
            set { _impl.DoesRefraction = value; }
        }

        public string DriverInfo => _impl.DriverInfo;

        public string DriverVersion => _impl.DriverVersion;

        public EquatorialCoordinateType EquatorialSystem => (EquatorialCoordinateType) _impl.EquatorialSystem;

        public double FocalLength => _impl.FocalLength;

        public double GuideRateDeclination
        {
            get { return _impl.GuideRateDeclination; }
            set { _impl.GuideRateDeclination = value; }
        }

        public double GuideRateRightAscension
        {
            get { return _impl.GuideRateRightAscension; }
            set { _impl.GuideRateRightAscension = value; }
        }

        public short InterfaceVersion => _impl.InterfaceVersion;

        public bool IsPulseGuiding => _impl.IsPulseGuiding;

        public string Name => _impl.Name;

        public double RightAscension => _impl.RightAscension;

        public double RightAscensionRate
        {
            get { return _impl.RightAscensionRate; }
            set { _impl.RightAscensionRate = value; }
        }

        public PierSide SideOfPier
        {
            get { return (PierSide) _impl.SideOfPier; }
            set { _impl.SideOfPier = (ASCOM.DeviceInterface.PierSide) value; }
        }

        public double SiderealTime => _impl.SiderealTime;

        public double SiteElevation
        {
            get { return _impl.SiteElevation; }
            set { _impl.SiteElevation = value; }
        }

        public double SiteLatitude
        {
            get { return _impl.SiteLatitude; }
            set { _impl.SiteLatitude = value; }
        }

        public double SiteLongitude
        {
            get { return _impl.SiteLongitude; }
            set { _impl.SiteLongitude = value; }
        }

        public bool Slewing => _impl.Slewing;

        public short SlewSettleTime
        {
            get { return _impl.SlewSettleTime; }
            set { _impl.SlewSettleTime = value; }
        }

        public double TargetDeclination
        {
            get { return _impl.TargetDeclination; }
            set { _impl.TargetDeclination = value; }
        }

        public double TargetRightAscension
        {
            get { return _impl.TargetRightAscension; }
            set { _impl.TargetRightAscension = value; }
        }

        public bool Tracking
        {
            get { return _impl.Tracking; }
            set { _impl.Tracking = value; }
        }

        public DriveRates TrackingRate
        {
            get { return (DriveRates) _impl.TrackingRate; }
            set { _impl.TrackingRate = (ASCOM.DeviceInterface.DriveRates) value; }
        }

        public ITrackingRates TrackingRates => new TrackingRateV3Proxy( _impl.TrackingRates);

        public DateTime UTCDate
        {
            get { return _impl.UTCDate; }
            set { _impl.UTCDate = value; }
        }
    }

    internal class TrackingRateV3Proxy : ITrackingRates
    {
        private readonly ASCOM.DeviceInterface.ITrackingRates _trackingRatesImplementation;

        public TrackingRateV3Proxy(ASCOM.DeviceInterface.ITrackingRates trackingRates)
        {
            _trackingRatesImplementation = trackingRates;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _trackingRatesImplementation).GetEnumerator();
        }

        public int Count => _trackingRatesImplementation.Count;

        public DriveRates this[int index] => (DriveRates) _trackingRatesImplementation[index];

        IEnumerator ITrackingRates.GetEnumerator()
        {
            return _trackingRatesImplementation.GetEnumerator();
        }
    }

    internal class AxisRatesV3Proxy : IAxisRates
    {
        private readonly ASCOM.DeviceInterface.IAxisRates _axisRatesImplementation;

        public AxisRatesV3Proxy(ASCOM.DeviceInterface.IAxisRates impl)
        {
            _axisRatesImplementation = impl;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _axisRatesImplementation.GetEnumerator();
        }

        public int Count => _axisRatesImplementation.Count;

        public IRate this[int index] => new RateV3Proxy(_axisRatesImplementation[index]);

        IEnumerator IAxisRates.GetEnumerator()
        {
            return _axisRatesImplementation.GetEnumerator();
        }
    }

    internal class RateV3Proxy : IRate
    {
        private readonly ASCOM.DeviceInterface.IRate _rateImplementation;

        public RateV3Proxy(ASCOM.DeviceInterface.IRate rate)
        {
            _rateImplementation = rate;
        }

        public double Maximum
        {
            get { return _rateImplementation.Maximum; }
            set { _rateImplementation.Maximum = value; }
        }

        public double Minimum
        {
            get { return _rateImplementation.Minimum; }
            set { _rateImplementation.Minimum = value; }
        }
    }
}
