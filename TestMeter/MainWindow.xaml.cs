using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TestMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer ourTimer;
        double[] meterValues = { 10.123d, 22d, 91.456d, 99d, 100d, 130.678d, 140d, 111.243d, 99.024d, 112.908d, 99.456d, 95.003d, 91.098d };
        //        double[] meterValues = { 102.5d };
        int meterIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            ourTimer = new DispatcherTimer();		                    // runs on same UI thread
            ourTimer.Tick += OurTimer_Tick;
            ourTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);      //every 1 second
            ourTimer.Start();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OurTimer_Tick(object sender, EventArgs e)
        {
            if (meterIndex < meterValues.Count())
            {
                Meter2.SM_MeterValue = meterValues[meterIndex];
                ++meterIndex;
            }
        }

        private void Meter2_SM_Alarm(object o, DllMeter.SmartMeter.AlarmRoutedEventArgs e)
        {
            Meter2.SM_Alarm -= Meter2_SM_Alarm;

            string msg = $"Alarm from {e.MeterDescription}, value is {e.MeterValue}";

            //MessageBox.Show(msg);
        }

        private void Meter2_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Left mouse down on meter 2");
            e.Handled = true;
        }

    }
}
