using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static GymTimerUWP.GymTimerState;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GymTimerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GymTimerConfig _config;
        GymTimerState _state;

        private DispatcherTimer _dispatchTimer;

        public MainPage()
        {
            this.InitializeComponent();
            InitTimer();         
        }

        public void Bind()
        {
            this.Btn_Configure.Click += (o, e) =>
            {

            };
            this.Btn_StartStop.Click += (o, e) =>
            {
                if(_state.Running)
                {
                    _state.Stop();
                    Btn_StartStop.Content = "Start";
                }
                else
                {
                    _state.Start();
                    Btn_StartStop.Content = "Pause";
                }
            };
        }

        private void InitTimer()
        {
            _config = new GymTimerConfig();
            _state = new GymTimerState();

            _state.Reset(_config);

            _dispatchTimer = new DispatcherTimer();
            _dispatchTimer.Interval = _state.TimerTickSpan;
            _dispatchTimer.Tick += DispatcherTimer_Tick;

            _state.Start();
            _dispatchTimer.Start();
        }

        void DispatcherTimer_Tick(object sender, object e)
        {
            TimeRemaining.Text = string.Format("{0:mm\\:ss}", _state.TimerTimeSpan -_state.TimerElapsed);
            Iteration.Text = string.Format("Round: {0} of {1}", _state.Iteration, _config.Iterations);
            Message.Text = GetAttributeDisplayName(_state.TimerState);

            TimeRemaining.Foreground = new SolidColorBrush(_state.TimerColor);
            Iteration.Foreground = new SolidColorBrush(_state.TimerColor);
            Message.Foreground = new SolidColorBrush(_state.TimerColor);

            _state.State_Tick(_config);         
        }

        string GetAttributeDisplayName(TimerStateEnum tse)
        {
            var type = typeof(TimerStateEnum);
            var memInfo = type.GetMember(tse.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            return ((DisplayAttribute)attributes.First()).Name;
        }
    }
}
