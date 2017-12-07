using System;
using System.ComponentModel;

namespace JsonLeeCMS.Control
{
    public sealed class PageChangingEventArgs : CancelEventArgs
    {
        private readonly int _newpageindex;

        public PageChangingEventArgs(int newPageIndex)
        {
            this._newpageindex = newPageIndex;
        }

        public int NewPageIndex
        {
            get
            {
                return this._newpageindex;
            }
        }
    }
}

