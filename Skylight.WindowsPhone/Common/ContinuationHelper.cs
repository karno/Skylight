using System;
using Skylight.Annotations;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Skylight.Common
{
    /// <summary>
    /// <see cref="ContinuationHelper"/> is used to detect if the most recent activation was due
    /// to a continuation such as the FileOpenPicker or WebAuthenticationBroker
    /// </summary>
    public static class ContinuationHelper
    {
        /// <summary>
        /// Execute continue with specified <see cref="IContinuationActivatedEventArgs"/>.
        /// </summary>
        /// <param name="args">activation args</param>
        public static void Continue(IContinuationActivatedEventArgs args)
        {
            Continue(args, Window.Current.Content as Frame);
        }

        /// <summary>
        /// Execute continue with specified <see cref="IContinuationActivatedEventArgs"/>.
        /// </summary>
        /// <param name="args">activation args</param>
        /// <param name="targetFrame">The frame control that contains the current page</param>
        public static void Continue([NotNull] IContinuationActivatedEventArgs args, [CanBeNull] Frame targetFrame)
        {
            if (args == null) throw new ArgumentNullException("args");
            if (targetFrame == null)
            {
                // target frame is not specified.
                return;
            }
            switch (args.Kind)
            {
                case ActivationKind.PickFileContinuation:
                    ContinueTo<FileOpenPickerContinuationEventArgs>(targetFrame, args);
                    break;
                case ActivationKind.PickSaveFileContinuation:
                    ContinueTo<FileSavePickerContinuationEventArgs>(targetFrame, args);
                    break;
                case ActivationKind.PickFolderContinuation:
                    ContinueTo<FolderPickerContinuationEventArgs>(targetFrame, args);
                    break;
                case ActivationKind.WebAuthenticationBrokerContinuation:
                    ContinueTo<WebAuthenticationBrokerContinuationEventArgs>(targetFrame, args);
                    break;
            }
        }

        /// <summary>
        /// Continue to target frame (or DataContext of that).
        /// </summary>
        /// <typeparam name="T">event type</typeparam>
        /// <param name="targetFrame">target frame</param>
        /// <param name="args">event args</param>
        private static void ContinueTo<T>(Frame targetFrame, IContinuationActivatedEventArgs args)
            where T : class, IContinuationActivatedEventArgs
        {
            var typedArgs = args as T;
            var continuable = GetContinuationTargetAs<IContinuable<T>>(targetFrame);
            if (continuable != null && typedArgs != null)
            {
                continuable.Continue(typedArgs);
            }
        }

        /// <summary>
        /// Get continuation target from frame or that DataContext.
        /// </summary>
        /// <typeparam name="T">interface type</typeparam>
        /// <param name="rootFrame">target frame</param>
        /// <returns>continuation target</returns>
        private static T GetContinuationTargetAs<T>(Frame rootFrame) where T : class
        {
            // if Page implements T, return immediately. 
            var typed = rootFrame.Content as T;
            if (typed != null)
            {
                return typed;
            }
            var page = rootFrame.Content as Page;
            // page == null -> return null
            // page != null && page.DataContext is T -> return T
            // page != null && page.DataContext is not T -> return null
            return page != null ? page.DataContext as T : null;
        }
    }

    /// <summary>
    /// Implements this interface for accepts continuing.
    /// </summary>
    /// <typeparam name="T">continuation event args</typeparam>
    public interface IContinuable<in T> where T : IContinuationActivatedEventArgs
    {
        /// <summary>
        /// This method is invoked when returned from SomethingAndContinue methods.
        /// </summary>
        /// <param name="args">Activated event args that contains specific information.</param>
        void Continue(T args);
    }
}
