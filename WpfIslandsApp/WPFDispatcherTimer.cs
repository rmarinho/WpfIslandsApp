using Microsoft.UI.Dispatching;
using System.Windows.Threading;

namespace WpfIslandsApp
{
    public partial class MainWindow
    {
        partial class WPFDispatcherTimer : IDispatcherTimer
        {
            readonly DispatcherTimer _timer;

            /// <summary>
            /// Initializes a new instance of the <see cref="DispatcherTimer"/> class.
            /// </summary>
            /// <param name="timer">An instance of <see cref="DispatcherQueueTimer"/> that will be used for this <see cref="DispatcherTimer"/> instance.</param>
            public WPFDispatcherTimer(DispatcherTimer timer)
            {
                _timer = timer;
            }

            /// <inheritdoc/>
            public TimeSpan Interval
            {
                get => _timer.Interval;
                set => _timer.Interval = value;
            }

            /// <inheritdoc/>
            public bool IsRepeating
            {
                get;
                set;
            }

            /// <inheritdoc/>
            public bool IsRunning
            {
                get; private set;
            }

            /// <inheritdoc/>
            public event EventHandler? Tick;

            /// <inheritdoc/>
            public void Start()
            {
                if (IsRunning)
                    return;

                IsRunning = true;

                _timer.Tick += OnTimerTick;

                _timer.Start();
            }

            /// <inheritdoc/>
            public void Stop()
            {
                if (!IsRunning)
                    return;

                IsRunning = false;

                _timer.Tick -= OnTimerTick;

                _timer.Stop();
            }

            void OnTimerTick(object? sender, EventArgs e)
            {
                Tick?.Invoke(this, e);

                if (!IsRepeating)
                    Stop();
            }
        }
    }
}