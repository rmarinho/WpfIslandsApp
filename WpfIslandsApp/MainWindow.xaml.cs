using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Threading;
using Windows.UI;
using WindowsDispatcher = System.Windows.Threading.Dispatcher;

namespace WpfIslandsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        //internal sealed class Wpf1Dispatcher : Dispatcher
        //{
        //    private readonly WindowsDispatcher _windowsDispatcher;

        //    public Wpf1Dispatcher(WindowsDispatcher windowsDispatcher)
        //    {
        //        _windowsDispatcher = windowsDispatcher ?? throw new ArgumentNullException(nameof(windowsDispatcher));
        //    }

        //    private static Action<Exception> RethrowException = exception =>
        //        ExceptionDispatchInfo.Capture(exception).Throw();

        //    public override bool CheckAccess()
        //        => _windowsDispatcher.CheckAccess();

        //    public override async Task InvokeAsync(Action workItem)
        //    {
        //        try {
        //            if (_windowsDispatcher.CheckAccess()) {
        //                workItem();
        //            } else {
        //                await _windowsDispatcher.InvokeAsync(workItem);
        //            }
        //        }
        //        catch (Exception ex) {
        //            // TODO: Determine whether this is the right kind of rethrowing pattern
        //            // You do have to do something like this otherwise unhandled exceptions
        //            // throw from inside Dispatcher.InvokeAsync are simply lost.
        //            _ = _windowsDispatcher.BeginInvoke(RethrowException, ex);
        //            throw;
        //        }
        //    }

        //    public override async Task InvokeAsync(Func<Task> workItem)
        //    {
        //        try {
        //            if (_windowsDispatcher.CheckAccess()) {
        //                await workItem();
        //            } else {
        //                await _windowsDispatcher.InvokeAsync(workItem);
        //            }
        //        }
        //        catch (Exception ex) {
        //            // TODO: Determine whether this is the right kind of rethrowing pattern
        //            // You do have to do something like this otherwise unhandled exceptions
        //            // throw from inside Dispatcher.InvokeAsync are simply lost.
        //            _ = _windowsDispatcher.BeginInvoke(RethrowException, ex);
        //            throw;
        //        }
        //    }

        //    public override async Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        //    {
        //        try {
        //            if (_windowsDispatcher.CheckAccess()) {
        //                return workItem();
        //            } else {
        //                return await _windowsDispatcher.InvokeAsync(workItem);
        //            }
        //        }
        //        catch (Exception ex) {
        //            // TODO: Determine whether this is the right kind of rethrowing pattern
        //            // You do have to do something like this otherwise unhandled exceptions
        //            // throw from inside Dispatcher.InvokeAsync are simply lost.
        //            _ = _windowsDispatcher.BeginInvoke(RethrowException, ex);
        //            throw;
        //        }
        //    }

        //    public override async Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        //    {
        //        try {
        //            if (_windowsDispatcher.CheckAccess()) {
        //                return await workItem();
        //            } else {
        //                return await _windowsDispatcher.InvokeAsync(workItem).Task.Unwrap();
        //            }
        //        }
        //        catch (Exception ex) {
        //            // TODO: Determine whether this is the right kind of rethrowing pattern
        //            // You do have to do something like this otherwise unhandled exceptions
        //            // throw from inside Dispatcher.InvokeAsync are simply lost.
        //            _ = _windowsDispatcher.BeginInvoke(RethrowException, ex);
        //            throw;
        //        }
        //    }
        //}

        class WPFDispatcher : IDispatcher
        {

            public bool IsDispatchRequired => !System.Windows.Application.Current.Dispatcher.CheckAccess();

            public IDispatcherTimer CreateTimer()
            {
                return new WPFDispatcherTimer(new DispatcherTimer());
            }

            public bool Dispatch(Action action)
            {
                System.Windows.Application.Current?.Dispatcher.BeginInvoke(action);
                return true;
            }

            public bool DispatchDelayed(TimeSpan delay, Action action)
            {
                var timer = new DispatcherTimer();
                timer.Interval = delay;
                timer.Tick += OnTimerTick;
                timer.Start();
                return true;

                void OnTimerTick(object? sender, EventArgs e)
                {
                    action();
                    timer.Tick -= OnTimerTick;
                }
            }

        }

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

        MauiContext _mauiContext;
        Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource _xamlSource;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            builder.Services.AddScoped<IDispatcher, WPFDispatcher>();
            MauiApp mauiApp = builder.Build();
            _mauiContext = new MauiContext(mauiApp.Services);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var mauiPage = new Microsoft.Maui.Controls.ContentPage();
            var wih = new System.Windows.Interop.WindowInteropHelper(this);
            var id = new Microsoft.UI.WindowId((ulong)wih.Handle);
            var controller = Microsoft.UI.Dispatching.DispatcherQueueController.CreateOnCurrentThread();
            _xamlSource = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
            _xamlSource.Initialize(id);
            _xamlSource.Content =( new Microsoft.Maui.Controls.Label() {
                Text = "Hello, world!",
             //   FontSize = 32,
            }).ToPlatform(_mauiContext);
            //_xamlSource.Content = new Microsoft.UI.Xaml.Controls.TextBlock() {
            //    Text = "Hello, world!",
            //    FontSize = 32,
            //};
            _xamlSource.SiteBridge.ResizePolicy = Microsoft.UI.Content.ContentSizePolicy.ResizeContentToParentWindow;
            _xamlSource.SiteBridge.Show();
        }
    }
}