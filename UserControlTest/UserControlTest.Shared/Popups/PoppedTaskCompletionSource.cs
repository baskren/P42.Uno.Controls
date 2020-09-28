using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlTest.Popups
{
    internal class PoppedTaskCompletionSource : TaskCompletionSource<PopupPoppedEventArgs>
    {
        public PopupPoppedCause Cause { get; private set; }

        public object Trigger { get; private set; }

        public string CallerName { get; private set; }

        public PoppedTaskCompletionSource(PopupPoppedCause cause, object trigger, string callerName)
        {
            Cause = cause;
            Trigger = trigger;
            CallerName = callerName;
        }
    }
}
