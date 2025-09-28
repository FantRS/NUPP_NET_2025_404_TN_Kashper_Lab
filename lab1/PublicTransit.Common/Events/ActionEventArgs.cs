using System;

namespace PublicTransit.Common.Events
{
    public class ActionEventArgs(string message) : EventArgs
    {
        public string Message { get; } = message;
    }
}
