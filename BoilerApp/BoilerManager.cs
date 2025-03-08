using System.Runtime.CompilerServices;
using BoilerApp.Utilities;
using Constants;

namespace BoilerApp
{
    public class BoilerManager
    {
        private SwitchStates _interLockSwitchState;

        private BoilerStates _boilerState;

        private Logger _logger;

        private SwitchStates InterLockSwitchState
        {
            get => _interLockSwitchState;
            set
            {
                _interLockSwitchState = value;
                _logger.EventOccurred($"InterLock Switch is {value} now");
            }
        }

        private BoilerStates BoilerState
        {
            get => _boilerState;
            set
            {
                _boilerState = value;
                _logger.EventOccurred($"Boiler state changed to {value}");
            }
        }

        public BoilerManager(Logger logger)
        {
            _logger = logger;
            _interLockSwitchState = SwitchStates.Open;
            _boilerState = BoilerStates.Lockout;
        }

        public void ToggleInterLockSwitch()
        {
            InterLockSwitchState = SwitchStates.Close;
            IOHandlerUtility.PrintActionSuccess(" InterLock Switch Toggle");
        }

        public async Task StartBoiler()
        {
            if (InterLockSwitchState == SwitchStates.Open)
            {
                IOHandlerUtility.PrintActionFailure(" The InterLockSwitch is open , close InterLockSwitch and Start Boiler.\n StartBoiler action");
                return;
            }
            if (InterLockSwitchState == SwitchStates.Close && BoilerState != BoilerStates.Lockout)
            {
                IOHandlerUtility.PrintActionFailure(" Boiler is already running.\n StartBoiler action");
                return;
            }
            if (InterLockSwitchState == SwitchStates.Close && BoilerState == BoilerStates.Lockout)
            {
                await Task.Run(() =>
                {
                    BoilerState = BoilerStates.Ready;
                    while (BoilerState != BoilerStates.Operational && InterLockSwitchState == SwitchStates.Close)
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            Thread.Sleep(1000);
                            Console.Write(i + " ");
                        }
                        BoilerState += 1;
                    }
                    IOHandlerUtility.PrintActionSuccess(" Boiler Run");
                });
            }
        }

        public void StopBoiler()
        {
            if (InterLockSwitchState == SwitchStates.Close)
            {
                InterLockSwitchState = SwitchStates.Open;
                BoilerState = BoilerStates.Lockout;
                IOHandlerUtility.PrintActionSuccess(" Stop Boiler");
            }
            else
            {
                IOHandlerUtility.PrintActionFailure("The Boiler is already off.\n Stop Boiler action");
            }
        }

        public void ResetLockOut()
        {
                InterLockSwitchState = SwitchStates.Open;
                BoilerState = BoilerStates.Lockout;
                IOHandlerUtility.PrintActionSuccess(" Boiler Reset");
        }

        public void SimulateError()
        {
            if(BoilerState == BoilerStates.Operational)
            {
                throw new Exception("Simulating Error");
            }
            else
            {
                IOHandlerUtility.PrintActionFailure("The Boiler should be in operational state to Simulate Error.\n Simulate Error");
            }
        }

        public void ViewEventLog()
        {
            _logger.viewLog();
            IOHandlerUtility.PrintActionSuccess(" Log Display");
        }
    }
}
