using System;
using System.ComponentModel.DataAnnotations;
using Windows.UI;

namespace GymTimerUWP
{
    public class GymTimerState
    {
        public TimeSpan TimerTickSpan { get { return TimeSpan.FromSeconds(1); } }
        public TimeSpan TimerTimeSpan { get; private set; }
        public TimeSpan TimerElapsed { get; private set; }
        public Color TimerColor { get; private set; }
        public TimerStateEnum TimerState { get; private set; }
        public int Iteration { get; private set; }
        private bool _started;

        public void Reset(GymTimerConfig config)
        {
            _started = false;
            TimerState = TimerStateEnum.GET_READY;
            Iteration = 1;
            TimerColor = config.InitialBufferTimeColor;
            TimerTimeSpan = config.InitialBufferTime;
            TimerElapsed = TimeSpan.Zero;
        }

        public void Start()
        {
            _started = true;
        }

        public void State_Tick(GymTimerConfig config)
        {
            if (!_started)
                return;

            TimerElapsed += TimerTickSpan;

            if(TimerState == TimerStateEnum.WORKOUT && (TimerTimeSpan - TimerElapsed) <= TimeSpan.FromSeconds(10))
            {
                TimerColor = config.WorkoutCloseToOverTimeColor;
            }

            if (TimerElapsed >= TimerTimeSpan)
            {
                UpdateForStateChange(config);
            }
        }

        private void UpdateForStateChange(GymTimerConfig config)
        {
            TimerElapsed = TimeSpan.Zero;
            if (TimerState == TimerStateEnum.GET_READY || TimerState == TimerStateEnum.BREAK)
            {
                if(TimerState == TimerStateEnum.BREAK)
                    Iteration++;
                
                TimerState = TimerStateEnum.WORKOUT;
                TimerColor = config.WorkoutTimeColor;
                TimerTimeSpan = config.WorkoutTime;
            }
            else if (TimerState == TimerStateEnum.WORKOUT)
            {
                if (Iteration < config.Iterations)
                {
                    TimerState = TimerStateEnum.BREAK;
                    TimerTimeSpan = config.BreakTime;
                    TimerColor = config.BreakTimeColor;
                }
                else
                {
                    TimerState = TimerStateEnum.END;
                    TimerTimeSpan = TimeSpan.Zero;
                    TimerColor = config.WorkoutOverTimeColor;
                    _started = false;
                }
            }
        }

        public enum TimerStateEnum
        {
            [Display(Name ="Get Ready")]
            GET_READY,
            [Display(Name = "Workout")]
            WORKOUT,
            [Display(Name = "Break")]
            BREAK,
            [Display(Name = "End")]
            END,
        }
    }
}
