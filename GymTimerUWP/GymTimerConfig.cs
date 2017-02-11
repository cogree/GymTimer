using System;
using Windows.UI;

namespace GymTimerUWP
{
    public class GymTimerConfig
    {
        public GymTimerConfig()
        {
            WorkoutTime = TimeSpan.FromSeconds(20);
            WorkoutTimeColor = Colors.Black;
            WorkoutCloseToOverTimeColor = Colors.DarkGoldenrod;
            WorkoutOverTimeColor = Colors.Red;

            InitialBufferTime = TimeSpan.FromSeconds(10);
            InitialBufferTimeColor = Colors.Blue;

            BreakTime = TimeSpan.FromSeconds(15);
            BreakTimeColor = Colors.Blue;

            Iterations = 3;
        }


        public TimeSpan WorkoutTime { get; set; }
        public Color WorkoutTimeColor { get; set; }
        public Color WorkoutCloseToOverTimeColor { get; set; }
        public Color WorkoutOverTimeColor { get; set; }

        public TimeSpan InitialBufferTime { get; set; }
        public Color InitialBufferTimeColor { get; set; }

        public TimeSpan BreakTime { get; set; }
        public Color BreakTimeColor { get; set; }

        public int Iterations { get; set; }
    }
}
