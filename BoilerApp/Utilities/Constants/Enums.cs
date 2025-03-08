namespace Constants
{
    public enum MainMenuOptions
    {
        Start_Boiler = 1,
        Stop_Boiler,
        Toggle_InterLock_Switch,
        Reset_Lockout,
        View_EventLog,
        Simulate_BoilerError,
        Exit
    }

    public enum SwitchStates
    {
        Open,
        Close
    }

    public enum BoilerStates
    {
        Lockout,
        Ready,
        PrePurge,
        Ignition,
        Operational
    }
}