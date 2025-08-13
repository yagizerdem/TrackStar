using System;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace TrackStar.Utils
{
    internal static class ShowToast
    {
        private static Notifier notifier;
        private static bool initialized;

        private static void EnsureInitialized()
        {
            if (initialized) return;

            // Schedule creation on the UI thread when it's ready
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var parentWindow = Application.Current.MainWindow
                   ?? (Application.Current.Windows.Count > 0
                       ? Application.Current.Windows[0] as Window
                       : new Window());

                notifier = new Notifier(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: parentWindow,
                        corner: Corner.TopRight,
                        offsetX: 10,
                        offsetY: 10);

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(5),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                    cfg.Dispatcher = Application.Current.Dispatcher;
                });

                Application.Current.Exit += (s, e) => notifier.Dispose();
                initialized = true;
            }), DispatcherPriority.ApplicationIdle); // Initialize when WPF is ready
        }

        private static void RunOnUI(Action action)
        {
            EnsureInitialized();

            // If not ready yet, queue the action
            if (!initialized)
            {
                Application.Current.Dispatcher.BeginInvoke(action, DispatcherPriority.ApplicationIdle);
                return;
            }

            if (Application.Current.Dispatcher.CheckAccess())
                action();
            else
                Application.Current.Dispatcher.BeginInvoke(action);
        }

        public static void ShowSuccess(string message) => RunOnUI(() => notifier.ShowSuccess(message));
        public static void ShowInformation(string message) => RunOnUI(() => notifier.ShowInformation(message));
        public static void ShowWarning(string message) => RunOnUI(() => notifier.ShowWarning(message));
        public static void ShowError(string message) => RunOnUI(() => notifier.ShowError(message));
    }
}
