namespace EMS.Shared
{
    public class HandledException : Exception
    {
        public HandledException(string message) : base(message) { }

        public HandledException(string message, Exception ex) : base(message, ex) { }
    }
}
