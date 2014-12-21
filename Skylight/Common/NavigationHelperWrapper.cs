using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Skylight.Common
{
    public sealed class NavigationHelperWrapper
    {
        public static Func<Page, NavigationHelperWrapper> Factory { get; set; }

        public event EventHandler<WrappedLoadStateEventArgs> LoadState;

        public void OnLoadState(WrappedLoadStateEventArgs e)
        {
            var handler = this.LoadState;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<WrappedSaveStateEventArgs> SaveState;

        public void OnSaveState(WrappedSaveStateEventArgs e)
        {
            var handler = this.SaveState;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public sealed class WrappedLoadStateEventArgs : EventArgs
    {
        public Object NavigationParameter { get; private set; }

        public IDictionary<string, object> PageState { get; private set; }

        public WrappedLoadStateEventArgs(object navigationParameter, IDictionary<string, object> pageState)
        {
            this.NavigationParameter = navigationParameter;
            this.PageState = pageState;
        }
    }

    public sealed class WrappedSaveStateEventArgs : EventArgs
    {
        public IDictionary<string, object> PageState { get; private set; }

        public WrappedSaveStateEventArgs(IDictionary<string, object> pageState)
        {
            this.PageState = pageState;
        }
    }
}
