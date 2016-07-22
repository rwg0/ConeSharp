using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ASCOM.Interface;


namespace ConeSharp
{
    internal class ProxyBase : IDisposable
    {
        protected object _impl;

        protected ProxyBase(object impl)
        {
            _impl = impl;
        }

        private T GetProperty<T>(string name)
        {
            try
            {
                object result = _impl.GetType().InvokeMember(name, BindingFlags.GetProperty, null, _impl, new object[0]);
                return (T)result;
            }
            catch (Exception e)
            {
                TraceException("Getting IDispatch property : " + name, e);
                if (e.InnerException != null)
                    throw e.InnerException;
                throw;
            }
        }

        private void SetProperty<T>(string name, T value)
        {
            try
            {
                _impl.GetType().InvokeMember(name, BindingFlags.SetProperty, null, _impl, new object[] { value });
            }
            catch (Exception e)
            {
                TraceException("Setting IDispatch property : " + name, e);
                if (e.InnerException != null)
                    throw e.InnerException;
                throw;
            }
        }

        private void TraceException(string s, Exception exception)
        {
            Trace.WriteLine(s);
            Trace.WriteLine(exception.ToString());
        }

        private void CallMethod(string name, params object[] oParams)
        {
            try
            {
                _impl.GetType().InvokeMember(name, BindingFlags.InvokeMethod, null, _impl, oParams);
            }
            catch (Exception e)
            {
                TraceException("Calling IDispatch method : " + name, e);
                if (e.InnerException != null)
                    throw e.InnerException;
                throw;
            }
        }

        protected T CallMethodT<T>(string name, params object[] oParams)
        {
            try
            {
                var result = _impl.GetType().InvokeMember(name, BindingFlags.InvokeMethod, null, _impl, oParams);
                if (result is T)
                    return (T)result;

                if (typeof(T) == typeof(IAxisRates))
                {
                    result = new AxisRatesProxy(result);
                    return (T)result;
                }

                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                TraceException("Calling IDispatch method : " + name, e);
                if (e.InnerException != null)
                    throw e.InnerException;
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected T Get<T>()
        {
            var caller = new StackFrame(1, true).GetMethod().Name;
            return GetProperty<T>(caller.Remove(0, 4));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void Set<T>(T value)
        {
            var caller = new StackFrame(1, true).GetMethod().Name;
            SetProperty(caller.Remove(0, 4), value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void Call(params object[] oParams)
        {
            var caller = new StackFrame(1, true).GetMethod().Name;
            CallMethod(caller, oParams);

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected T CallT<T>(params object[] oParams)
        {
            var caller = new StackFrame(1, true).GetMethod().Name;
            return CallMethodT<T>(caller, oParams);

        }

        public void Dispose()
        {
            if (_impl != null && Marshal.IsComObject(_impl))
            {
                Marshal.ReleaseComObject(_impl);
            }
            _impl = null;
        }
    }

    internal class MountDispatchProxy : ProxyBase, ITelescope
    {
        public MountDispatchProxy(object oDispObj) : base(oDispObj)
        {
        }


        public void AbortSlew()
        {
            Call();
        }

        public IAxisRates AxisRates(TelescopeAxes Axis)
        {
            return CallT<IAxisRates>(Axis);
        }

        public bool CanMoveAxis(TelescopeAxes Axis)
        {
            return CallT<bool>(Axis);
        }

        public PierSide DestinationSideOfPier(double RightAscension, double Declination)
        {
            return CallT<PierSide>(RightAscension, Declination);
        }

        public void FindHome()
        {
            Call();
        }

        public void MoveAxis(TelescopeAxes Axis, double Rate)
        {
            Call(Axis, Rate);
        }

        public void Park()
        {
            Call();
        }

        public void PulseGuide(GuideDirections Direction, int Duration)
        {
            Call(Direction, Duration);
        }

        public void SetPark()
        {
            Call();
        }

        public void SetupDialog()
        {
            Call();
        }

        public void SlewToAltAz(double azimuth, double altitude)
        {
            Call(azimuth, altitude);
        }

        public void SlewToAltAzAsync(double azimuth, double altitude)
        {
            Call(azimuth, altitude);
        }

        public void SlewToCoordinates(double rightAscension, double declination)
        {
            Call(rightAscension, declination);
        }

        public void SlewToCoordinatesAsync(double rightAscension, double declination)
        {
            Call(rightAscension, declination);
        }

        public void SlewToTarget()
        {
            Call();
        }

        public void SlewToTargetAsync()
        {
            Call();
        }

        public void SyncToAltAz(double azimuth, double altitude)
        {
            Call(azimuth, altitude);

        }

        public void SyncToCoordinates(double rightAscension, double declination)
        {
            Call(rightAscension, declination);

        }

        public void SyncToTarget()
        {
            Call();
        }

        public void Unpark()
        {
            Call();
        }

        public void CommandBlind(string Command, bool Raw = false)
        {
            Call(Command, Raw);
        }

        public bool CommandBool(string Command, bool Raw = false)
        {
            return CallT<bool>(Command, Raw);
        }

        public string CommandString(string Command, bool Raw = false)
        {
            return CallT<string>(Command, Raw);
        }

        public AlignmentModes AlignmentMode => Get<AlignmentModes>();

        public double Altitude => Get<double>();

        public double ApertureArea => Get<double>();

        public double ApertureDiameter => Get<double>();

        public bool AtHome => Get<bool>();

        public bool AtPark => Get<bool>();

        public double Azimuth => Get<double>();

        public bool CanFindHome => Get<bool>();

        public bool CanPark => Get<bool>();

        public bool CanPulseGuide => Get<bool>();

        public bool CanSetDeclinationRate => Get<bool>();

        public bool CanSetGuideRates => Get<bool>();

        public bool CanSetPark => Get<bool>();

        public bool CanSetRightAscensionRate => Get<bool>();

        public bool CanSetPierSide => Get<bool>();

        public bool CanSetTracking => Get<bool>();

        public bool CanSlew => Get<bool>();

        public bool CanSlewAltAz => Get<bool>();

        public bool CanSlewAltAzAsync => Get<bool>();

        public bool CanSlewAsync => Get<bool>();

        public bool CanSync => Get<bool>();

        public bool CanSyncAltAz => Get<bool>();

        public bool CanUnpark => Get<bool>();

        public bool Connected
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public double Declination => Get<double>();

        public double DeclinationRate
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public string Description => Get<string>();

        public bool DoesRefraction
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public string DriverInfo => Get<string>();

        public string DriverVersion => Get<string>();

        public EquatorialCoordinateType EquatorialSystem => Get<EquatorialCoordinateType>();

        public double FocalLength => Get<double>();

        public double GuideRateDeclination
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double GuideRateRightAscension
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public short InterfaceVersion => Get<short>();

        public bool IsPulseGuiding => Get<bool>();

        public string Name => Get<string>();

        public double RightAscension => Get<double>();

        public double RightAscensionRate
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public PierSide SideOfPier
        {
            get { return Get<PierSide>(); }
            set { Set(value); }
        }

        public double SiderealTime => Get<double>();

        public double SiteElevation
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double SiteLatitude
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double SiteLongitude
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public bool Slewing => Get<bool>();

        public short SlewSettleTime
        {
            get { return Get<short>(); }
            set { Set(value); }
        }

        public double TargetDeclination
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public double TargetRightAscension
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

        public bool Tracking
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        public DriveRates TrackingRate
        {
            get { return Get<DriveRates>(); }
            set { Set(value); }
        }

        public ITrackingRates TrackingRates => Get<ITrackingRates>();

        public DateTime UTCDate
        {
            get { return Get<DateTime>(); }
            set { Set(value); }
        }
    }

    internal class AxisRatesProxy : ProxyBase, IAxisRates
    {
        public AxisRatesProxy(object oDispObj) : base(oDispObj)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return CallMethodT<IEnumerator>("GetEnumerator");

        }

        public int Count => Get<int>();

        public IRate this[int Index]
        {
            get
            {
                dynamic dyn = _impl;
                dynamic item = dyn[Index];
                if (item is IRate)
                    return item;

                return new RateProxy(item);
            }
        }

        IEnumerator IAxisRates.GetEnumerator()
        {
            return CallMethodT<IEnumerator>("GetEnumerator");
        }
    }

    internal class RateProxy : ProxyBase, IRate
    {
        public RateProxy(object impl) : base(impl)
        {
        }

        public double Maximum
        {
            get { return Get<double>(); }
            set { Set(value); }
        }
        public double Minimum
        {
            get { return Get<double>(); }
            set { Set(value); }
        }

    }
}