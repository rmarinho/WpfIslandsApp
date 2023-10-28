using Microsoft.UI.Dispatching;

namespace WpfIslandsApp
{
    public partial class MainWindow
    {
        class WinUIWPFDispatcher : IDispatcher
        {
            readonly DispatcherQueue _dispatcherQueue;

            public bool IsDispatchRequired => IsDispatchRequiredImplementation();

            internal WinUIWPFDispatcher(DispatcherQueue dispatcherQueue)
            {
                _dispatcherQueue = dispatcherQueue ?? throw new ArgumentNullException(nameof(dispatcherQueue));
            }

            bool IsDispatchRequiredImplementation() =>
                !_dispatcherQueue.HasThreadAccess;

            bool DispatchImplementation(Action action) =>
                _dispatcherQueue.TryEnqueue(() => action());

            bool DispatchDelayedImplementation(TimeSpan delay, Action action)
            {
                var timer = _dispatcherQueue.CreateTimer();
                timer.Interval = delay;
                timer.Tick += OnTimerTick;
                timer.Start();
                return true;

                void OnTimerTick(DispatcherQueueTimer sender, object args)
                {
                    action();
                    timer.Tick -= OnTimerTick;
                }
            }

            public bool Dispatch(Action action) => DispatchImplementation(action);

            public bool DispatchDelayed(TimeSpan delay, Action action) => DispatchDelayedImplementation(delay, action);

            public IDispatcherTimer CreateTimer()
            {
                return new WinUIDispatcherTimer(_dispatcherQueue.CreateTimer());
            }
        }
    }
}