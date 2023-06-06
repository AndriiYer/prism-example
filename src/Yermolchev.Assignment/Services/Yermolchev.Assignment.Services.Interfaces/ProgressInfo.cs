using System;

namespace Yermolchev.Assignment.Services.Interfaces
{
    public class ProgressInfoEventArgs : EventArgs
    {
        public long TotallyToProcess { get; set; }

        public long CurrentlyProcessed { get; set; }
    }
}
