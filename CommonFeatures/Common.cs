using System;

namespace CommonFeatures
{
    /// <summary>
    /// Common EventArgs Interface
    /// </summary>
    public class CoreEventArgs : EventArgs
    {
        public string OriginName { get; set; }
        public string OriginMessage { get; set; }
    }
    public class EngineEventArgs : CoreEventArgs
    {
        public string LogMessage { get; set; }
    }

    public class LoggerEventArgs : CoreEventArgs
    {
        public string LogMessage { get; set; }
    }


    class AddAccessControlEventArgs : CoreEventArgs
    {

    }
    public static class Common 
    {

    }
}
