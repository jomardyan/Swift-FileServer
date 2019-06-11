using System;

namespace CommonFeatures
{

    public class CoreEventArgs : EventArgs
    {
        public string OriginName;
        public string OriginMesage;
    }
    public class EngineEventArgs : CoreEventArgs
    {
        public string LogMesage { get; set; }
    }

    class AddAccessControlEventArgs : CoreEventArgs
    {

    }
    public static class Common 
    {

    }
}
