using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class InsertProgressEventArgs : EventArgs
    {
        private int _current;
        private int _total;

        public InsertProgressEventArgs(int current, int total)
        {
            this._current = current;
            this._total = total;
        }

        public int CurrentValue { get { return _current; } }
        public int TotalValue { get { return _total; } }

    }
}
