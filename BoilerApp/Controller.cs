using Constants;

namespace BoilerApp
{
    public class Controller
    {
        private BoilerManager _boilerManager;

        private Task t1;

        public Controller(BoilerManager boilerManager)
        {
            _boilerManager = boilerManager;
        }

        public bool Run(MainMenuOptions userChoice)
        {
            switch (userChoice)
            {
                case MainMenuOptions.Start_Boiler:
                    t1 = _boilerManager.StartBoiler();
                    break;
                case MainMenuOptions.Stop_Boiler:
                    _boilerManager.StopBoiler();
                    break;
                case MainMenuOptions.Toggle_InterLock_Switch:
                    _boilerManager.ToggleInterLockSwitch();
                    break;
                case MainMenuOptions.Reset_Lockout:
                    _boilerManager.ResetLockOut();
                    break;
                case MainMenuOptions.Simulate_BoilerError:
                    _boilerManager.SimulateError();
                    break;
                case MainMenuOptions.View_EventLog:
                    _boilerManager.ViewEventLog();
                    break;
                default:
                    Console.WriteLine("Feature Not Implemented");
                    Console.Clear();
                    break;
                case MainMenuOptions.Exit:
                    return true;
            }
            Console.Clear();
            return false;
        }
    }
}
