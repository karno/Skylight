using System;
using Microsoft.Xaml.Interactivity;
using Skylight.Internals;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Skylight.Common.Behaviors
{
    public class NavigationHelperBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty StateProviderProperty = DependencyProperty.Register(
            "StateProvider", typeof(INavigationStateProvider), typeof(NavigationHelperBehavior),
            new PropertyMetadata(null));

        public INavigationStateProvider StateProvider
        {
            get { return (INavigationStateProvider)this.GetValue(StateProviderProperty); }
            set { this.SetValue(StateProviderProperty, value); }
        }

        private Page _associatedPage;

        private NavigationHelperWrapper _navigationHelperWrapper;

        public DependencyObject AssociatedObject { get { return this._associatedPage; } }

        public void Attach(DependencyObject associatedObject)
        {
            if (this.AssociatedObject == associatedObject || DesignMode.DesignModeEnabled)
            {
                return;
            }
            if (this.AssociatedObject != null)
            {
                throw new InvalidOperationException(
                    "Could not attach the behavior for multiple times.");
            }
            associatedObject.GetPage();
            var page = associatedObject as Page;
            if (page == null && associatedObject != null)
            {
                throw new ArgumentException("NavigationHelperBehavior allows attach to Page instance only.");
            }
            this._associatedPage = page;
            if (page != null)
            {
                this.AttachToPage(page);
            }
            else
            {
                this.DetachFromPage();
            }
        }

        public void Detach()
        {
            this._associatedPage = null;
            this.DetachFromPage();
        }

        public void AttachToPage(Page page)
        {
            if (this._navigationHelperWrapper != null)
            {
                this.DetachFromPage();
            }
            if (NavigationHelperWrapper.Factory == null)
            {
                // wrapper fuctory is not assigned
                throw new InvalidOperationException(
                    "Factory of NavigationHelperWrapper is not assigned yet." + Environment.NewLine +
                    "You should assign to NavigationHelperWrapper.Factory with conversion code" +
                    " from NavigationHelper in your solution.");
            }
            this._navigationHelperWrapper = NavigationHelperWrapper.Factory(page);
            this._navigationHelperWrapper.LoadState += this.LoadState;
            this._navigationHelperWrapper.SaveState += this.SaveState;
        }

        public void DetachFromPage()
        {
            if (this._navigationHelperWrapper == null) return;
            var wrapper = this._navigationHelperWrapper;
            this._navigationHelperWrapper = null;
            wrapper.LoadState -= this.LoadState;
            wrapper.SaveState -= this.SaveState;
        }

        private void SaveState(object sender, WrappedSaveStateEventArgs e)
        {
            var stateProvider = this.StateProvider;
            if (stateProvider != null)
            {
                stateProvider.SaveNavigationState(e);
            }
        }

        private void LoadState(object sender, WrappedLoadStateEventArgs e)
        {
            var stateConsumer = this.StateProvider;
            if (stateConsumer != null)
            {
                stateConsumer.LoadNavigationState(e);
            }
        }
    }
}
