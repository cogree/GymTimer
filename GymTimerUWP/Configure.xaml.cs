using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GymTimerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Configure : Page
    {
        public GymTimerConfig _config { get; set; }

        public Configure()
        {
            this.InitializeComponent();

            if(_config != null)
            {
                BindConfigToPage();
            }
        }

        private void BindConfigToPage()
        {
            this.PageWorkoutTime.Text = _config.WorkoutTime.ToString();
            this.PageBreakTime.Text = _config.BreakTime.ToString();
            this.PageNumberOfRounds.Text = _config.Iterations.ToString();
        }
    }
}
