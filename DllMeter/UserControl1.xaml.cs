using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DllMeter
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	//public partial class SmartMeter : UserControl
	public partial class SmartMeter : ButtonBase
	{
		private const string _defaultDescription = @"SM__Description not set";
        private DoubleAnimation _alarmAnimation = null;
        private SolidColorBrush _meterFrameBrush = null;
        private SolidColorBrush _meterReadingBrush = null;

		#region Constructor
		public SmartMeter()
        {
            InitializeComponent();

            labDescription.Content = _defaultDescription;

            _meterFrameBrush = (SolidColorBrush)this.Resources["MeterFrameBrush"];
            _meterReadingBrush = (SolidColorBrush)this.Resources["MeterReadingBrush"];
		}
		#endregion

		#region Provide a RoutedUICommand
		private static readonly RoutedUICommand SM_UICommand = new RoutedUICommand("Smart Meter Command", "SM_UICommand", typeof(SmartMeter));

		public static RoutedUICommand SM_Command
		{
			get { return SM_UICommand; }
		}
		#endregion

		#region SM_AlarmEvent when meter reading goes above the 'SM_Threshold'
		public class AlarmRoutedEventArgs : RoutedEventArgs
        {
            public double MeterValue { get; set; }
            public string MeterDescription { get; set; }

            public AlarmRoutedEventArgs(RoutedEvent e)
            {
                base.RoutedEvent = e;
            }
        }

        public delegate void AlarmEventHandler(object o, AlarmRoutedEventArgs e);

        private static readonly RoutedEvent SM_AlarmEvent = EventManager.RegisterRoutedEvent("SM_Alarm", RoutingStrategy.Direct,
            typeof(AlarmEventHandler), typeof(SmartMeter));

        public event AlarmEventHandler SM_Alarm
        {
            add { this.AddHandler(SM_AlarmEvent, value); }
            remove { this.RemoveHandler(SM_AlarmEvent, value); }
        }

        public void RaiseAlarmEvent(double inMeterValue, string inMeterDescription)
        {
            AlarmRoutedEventArgs NewEvent = new AlarmRoutedEventArgs(SmartMeter.SM_AlarmEvent);
            NewEvent.MeterDescription = inMeterDescription;
            NewEvent.MeterValue = inMeterValue;
            RaiseEvent(NewEvent);
        }

        #endregion

        #region SM_Background1 Property (for the main grid)

        private static readonly DependencyProperty SM_Background1Property =
            DependencyProperty.Register("SM_Background1", typeof(Color), typeof(SmartMeter),
            new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_Background1PropertyChanged)));

        private static void SM_Background1PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_Background1PropertyChanged(e);
        }

        private void OnSM_Background1PropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            MainGrid.Background = new SolidColorBrush((Color)e.NewValue);
        }

        // Allow access via code.
        public static Color GetSM_Background1Property(UIElement e)
        {
            return (Color)e.GetValue(SM_Background1Property);
        }

        public static void SetSM_Background1Property(UIElement e, Color value)
        {
            e.SetValue(SM_Background1Property, value);
        }

        // Allow access via XAML.
        public Color SM_Background1
        {
            get { return (Color)GetValue(SM_Background1Property); }

            set { SetValue(SM_Background1Property, value); }
        }
        #endregion

        #region SM_Background2 Property (for the meter's face / semi-cirle and the Description Label)

        private static readonly DependencyProperty SM_Background2Property =
            DependencyProperty.Register("SM_Background2", typeof(Color), typeof(SmartMeter),
            new FrameworkPropertyMetadata(Colors.Gray, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_Background2PropertyChanged)));

        private static void SM_Background2PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_Background2PropertyChanged(e);
        }

        private void OnSM_Background2PropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            MeterFace.Fill = new SolidColorBrush((Color)e.NewValue);
            labDescription.Background = new SolidColorBrush((Color)e.NewValue);
        }

        // Allow access via code.
        public static Color GetSM_Background2Property(UIElement e)
        {
            return (Color)e.GetValue(SM_Background2Property);
        }

        public static void SetSM_Background2Property(UIElement e, Color value)
        {
            e.SetValue(SM_Background2Property, value);
        }

        // Allow access via XAML.
        public Color SM_Background2
        {
            get { return (Color)GetValue(SM_Background2Property); }

            set { SetValue(SM_Background2Property, value); }
        }
        #endregion

        #region SM_Background3 Property (for the meter's pointer)

        private static readonly DependencyProperty SM_Background3Property =
            DependencyProperty.Register("SM_Background3", typeof(Color), typeof(SmartMeter),
            new FrameworkPropertyMetadata(Colors.Yellow, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_Background3PropertyChanged)));

        private static void SM_Background3PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_Background3PropertyChanged(e);
        }

        private void OnSM_Background3PropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            MeterPointer.Fill = new SolidColorBrush((Color)e.NewValue);
        }

        // Allow access via code.
        public static Color GetSM_Background3Property(UIElement e)
        {
            return (Color)e.GetValue(SM_Background3Property);
        }

        public static void SetSM_Background3Property(UIElement e, Color value)
        {
            e.SetValue(SM_Background3Property, value);
        }

        // Allow access via XAML.
        public Color SM_Background3
        {
            get { return (Color)GetValue(SM_Background3Property); }

            set { SetValue(SM_Background3Property, value); }
        }
        #endregion

        #region SM_Foreground1 Property (for the meter's numbers, meter's hash marks, and the Description Label)

        private static readonly DependencyProperty SM_Foreground1Property =
            DependencyProperty.Register("SM_Foreground1", typeof(Color), typeof(SmartMeter),
            new FrameworkPropertyMetadata(Colors.Blue, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_Foreground1PropertyChanged)));

        private static void SM_Foreground1PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_Foreground1PropertyChanged(e);
        }

        private void OnSM_Foreground1PropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            SolidColorBrush newColor = new SolidColorBrush((Color)e.NewValue);
            labMinimum.Foreground = newColor;
            labMiddle.Foreground = newColor;
            labMaximum.Foreground = newColor;
            labDescription.Foreground = newColor;
            HashMark1.Stroke = newColor;
            HashMark2.Stroke = newColor;
            HashMark3.Stroke = newColor;
            HashMark4.Stroke = newColor;
            HashMark5.Stroke = newColor;
            HashMark6.Stroke = newColor;
            HashMark7.Stroke = newColor;
            HashMark8.Stroke = newColor;
            HashMark9.Stroke = newColor;
        }

        // Allow access via code.
        public static Color GetSM_Foreground1Property(UIElement e)
        {
            return (Color)e.GetValue(SM_Foreground1Property);
        }

        public static void SetSM_Foreground1Property(UIElement e, Color value)
        {
            e.SetValue(SM_Foreground1Property, value);
        }

        // Allow access via XAML.
        public Color SM_Foreground1
        {
            get { return (Color)GetValue(SM_Foreground1Property); }

            set { SetValue(SM_Foreground1Property, value); }
        }
        #endregion

        #region SM_MeterValue Property (for the meter's pointer angle and label)

        private static readonly DependencyProperty SM_MeterValueProperty =
            DependencyProperty.Register("SM_MeterValue", typeof(Double), typeof(SmartMeter),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_MeterValuePropertyChanged)));

        private static void SM_MeterValuePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_MeterValuePropertyChanged(e);
        }

        private void OnSM_MeterValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            // Round to 1 decimal place
            double newReading = Math.Round((double)e.NewValue, 1);
            double rawReading = newReading;
            double oldReading = Math.Round((double)e.OldValue, 1);

            // Adjust if invalid value given.
            if (newReading < SM_Minimum)
                newReading = SM_Minimum;

            if (newReading > SM_Maximum)
                newReading = SM_Maximum;

            // Update label to show new meter value.
            labReading.Content = rawReading.ToString("N1");

            // Now convert the readings into angles for the meter pointer.
            double oldMeterAngle = CalculateMeterPointerAngle(oldReading);
            double newMeterAngle = CalculateMeterPointerAngle(newReading);

            // Animate the meter's pointer so it rotates to its new position.
            RotateTransform _meterPointer = (RotateTransform)MeterPointer.RenderTransform;

            DoubleAnimation aAnimation = new DoubleAnimation();
            aAnimation.From = oldMeterAngle;
            aAnimation.To = newMeterAngle;
            aAnimation.Duration = new Duration(new TimeSpan(0, 0, 1));
            aAnimation.SpeedRatio = 1.4d;                                  // Go faster
            aAnimation.FillBehavior = FillBehavior.HoldEnd;

            _meterPointer.BeginAnimation(RotateTransform.AngleProperty, aAnimation);

            // If the new meter reading exceeds the threshold (if set)...
            if (this.SM_Threshold != 0d && newReading >= this.SM_Threshold)
            {
                // Raise the alarm event in case the user is listening to it.
                RaiseAlarmEvent(SM_MeterValue, SM_Description);

                if (_alarmAnimation == null)
                {
                    MeterFrame.Stroke = new SolidColorBrush(Colors.Red);

                    // Blink the meter reading quickly for visual attention.
                    labReading.Background = new SolidColorBrush(Colors.Red);
                    _alarmAnimation = new DoubleAnimation();
                    _alarmAnimation.Completed += _alarmAnimation_Completed;
                    _alarmAnimation.AutoReverse = true;
                    _alarmAnimation.Duration = new Duration(new TimeSpan(0, 0, 1));  // Duration = 1 second
                    _alarmAnimation.From = 0d;
                    _alarmAnimation.To = 1d;
                    _alarmAnimation.FillBehavior = FillBehavior.Stop;
                    _alarmAnimation.SpeedRatio = 8;                                  // Go faster
                    _alarmAnimation.RepeatBehavior = new RepeatBehavior(4d);         // Repeat 4 times

                    labReading.BeginAnimation(Label.OpacityProperty, _alarmAnimation);
                }
            }
            else
            {
                labReading.Background = _meterReadingBrush;
            }
        }

        private void _alarmAnimation_Completed(object sender, EventArgs e)
        {
            _alarmAnimation = null;
        }

        /// <summary>
        /// For the given meter 'reading', calculate the angle the meter 
        /// pointer should be. Note that:
        /// An angle of -60 degrees points to the bottom left for a minimum reading.
        /// An angle of +60 degrees points to the bottom right for a maximum reading.
        /// 
        /// Also note the user can specify any values for SM_Minimum and SM_Maximum.
        /// </summary>
        /// <param name="inReading">A meter reading</param>
        /// <returns>Angle for the meter pointer</returns>
        private double CalculateMeterPointerAngle(double inReading)
        {
            if (inReading <= SM_Minimum)
            {
                return -60d;
            }
            if (inReading >= SM_Maximum)
            {
                return 60d;
            }

            // Calculate fraction relative to max and min
            double temp = (inReading - SM_Minimum) / (SM_Maximum - SM_Minimum);

            // Calculate fraction relative to 120 degrees
            temp = 120d * temp;

            // Calculate final rotation amount, rounded to 1 decimal place.
            temp = temp - 60d;
            temp = Math.Round(temp, 1);

            return temp;
        }

        // Allow access via code.
        public static Double GetSM_MeterValueProperty(UIElement e)
        {
            return (Double)e.GetValue(SM_MeterValueProperty);
        }

        public static void SetSM_MeterValueProperty(UIElement e, Double value)
        {
            e.SetValue(SM_MeterValueProperty, value);
        }

        // Allow access via XAML.
        public Double SM_MeterValue
        {
            get { return (Double)GetValue(SM_MeterValueProperty); }

            set { SetValue(SM_MeterValueProperty, value); }
        }
        #endregion

        #region SM_Minimum Property (for the meter's minimum reading)

        private static readonly DependencyProperty SM_MinimumProperty =
            DependencyProperty.Register("SM_Minimum", typeof(Double), typeof(SmartMeter),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_MinimumPropertyChanged)));

        private static void SM_MinimumPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_MinimumPropertyChanged(e);
        }

        private void OnSM_MinimumPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            // Round to 0 decimal places
            double newMinimum = Math.Round((double)e.NewValue, 0);
            labMinimum.Content = newMinimum.ToString("N0");

            // Set middle number for the meter
            SetMiddle();
        }

        // Allow access via code.
        public static Double GetSM_MinimumProperty(UIElement e)
        {
            return (Double)e.GetValue(SM_MinimumProperty);
        }

        public static void SetSM_MinimumProperty(UIElement e, Double value)
        {
            e.SetValue(SM_MinimumProperty, value);
        }

        // Allow access via XAML.
        public Double SM_Minimum
        {
            get { return (Double)GetValue(SM_MinimumProperty); }

            set { SetValue(SM_MinimumProperty, value); }
        }
        #endregion

        #region SM_Maximum Property (for the meter's maximum reading)

        private static readonly DependencyProperty SM_MaximumProperty =
            DependencyProperty.Register("SM_Maximum", typeof(Double), typeof(SmartMeter),
            new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_MaximumPropertyChanged)));

        private static void SM_MaximumPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_MaximumPropertyChanged(e);
        }

        private void OnSM_MaximumPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            // Round to 0 decimal places
            double newMaximum = Math.Round((double)e.NewValue, 0);
            labMaximum.Content = newMaximum.ToString("N0");

            // Set middle number for the meter
            SetMiddle();
        }

        private void SetMiddle()
        {
            if (SM_Minimum < SM_Maximum)
            {
                // Round to 0 decimal places
                double minimum = Math.Round((double)SM_Minimum, 0);
                double maximum = Math.Round((double)SM_Maximum, 0);

                // Compute middle value
                double middle = (maximum + minimum) / 2;

                // If middle has no fractions
                if ((maximum + minimum) % 2 == 0)
                {
                    labMiddle.Content = middle.ToString("N0");
                }

                // Else show fraction to 1 decimal place
                else
                {
                    middle = Math.Round(middle, 1);
                    labMiddle.Content = middle.ToString("N1");
                }
            }
        }

        // Allow access via code.
        public static Double GetSM_MaximumProperty(UIElement e)
        {
            return (Double)e.GetValue(SM_MaximumProperty);
        }

        public static void SetSM_MaximumProperty(UIElement e, Double value)
        {
            e.SetValue(SM_MaximumProperty, value);
        }

        // Allow access via XAML.
        public Double SM_Maximum
        {
            get { return (Double)GetValue(SM_MaximumProperty); }

            set { SetValue(SM_MaximumProperty, value); }
        }
        #endregion

        #region SM_Description Property (for the label description)

        private static readonly DependencyProperty SM_DescriptionProperty =
            DependencyProperty.Register("SM_Description", typeof(string), typeof(SmartMeter),
            new FrameworkPropertyMetadata(_defaultDescription, FrameworkPropertyMetadataOptions.AffectsRender,
            new PropertyChangedCallback(SM_DescriptionPropertyChanged)));

        private static void SM_DescriptionPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_DescriptionPropertyChanged(e);
        }

        private void OnSM_DescriptionPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            labDescription.Content = (String)e.NewValue;
        }

        // Allow access via code.
        public static String GetSM_DescriptionProperty(UIElement e)
        {
            return (String)e.GetValue(SM_DescriptionProperty);
        }

        public static void SetSM_DescriptionProperty(UIElement e, String value)
        {
            e.SetValue(SM_DescriptionProperty, value);
        }

        // Allow access via XAML.
        public String SM_Description
        {
            get { return (String)GetValue(SM_DescriptionProperty); }

            set { SetValue(SM_DescriptionProperty, value); }
        }
        #endregion

        #region SM_Threshold Property (optional level that when exceeded, user can be notified via SM_Alarm event)

        private static readonly DependencyProperty SM_ThresholdProperty =
            DependencyProperty.Register("SM_Threshold", typeof(double), typeof(SmartMeter),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(SM_ThresholdPropertyChanged)));

        private static void SM_ThresholdPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_ThresholdPropertyChanged(e);
        }

        private void OnSM_ThresholdPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (SM_Threshold > SM_Minimum && SM_Threshold < SM_Maximum)
            {
                // Show a pieslice on the meter to show where the warning section is.
                ThresholdSlice.StartAngle = CalculateMeterPointerAngle(SM_Threshold);
                ThresholdSlice.SweepAngle = 60d - ThresholdSlice.StartAngle;
            }
        }

        // Allow access via code.
        public static double GetSM_ThresholdProperty(UIElement e)
        {
            return (double)e.GetValue(SM_ThresholdProperty);
        }

        public static void SetSM_ThresholdProperty(UIElement e, double value)
        {
            e.SetValue(SM_ThresholdProperty, value);
        }

        // Allow access via XAML.
        public double SM_Threshold
        {
            get { return (double)GetValue(SM_ThresholdProperty); }

            set { SetValue(SM_ThresholdProperty, value); }
        }
        #endregion

        #region SM_Scale Property (for changing the entire meter's size)

        private static readonly DependencyProperty SM_ScaleProperty =
            DependencyProperty.Register("SM_Scale", typeof(Double), typeof(SmartMeter),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender | 
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | 
                FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure,
            new PropertyChangedCallback(SM_ScalePropertyChanged)));

        private static void SM_ScalePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as SmartMeter).OnSM_ScalePropertyChanged(e);
        }

        private void OnSM_ScalePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            ScaleMeter.ScaleX = SM_Scale;
            ScaleMeter.ScaleY = SM_Scale;
        }

        // Allow access via code.
        public static Double GetSM_ScaleProperty(UIElement e)
        {
            return (Double)e.GetValue(SM_ScaleProperty);
        }

        public static void SetSM_ScaleProperty(UIElement e, Double value)
        {
            e.SetValue(SM_ScaleProperty, value);
        }

        // Allow access via XAML.
        public Double SM_Scale
        {
            get { return (Double)GetValue(SM_ScaleProperty); }

            set { SetValue(SM_ScaleProperty, value); }
        }
        #endregion

        #region Context Menu click events

        /// <summary>
        /// Restore the normal color around the meter. This is handy after
        /// a meter has alarmed and this area's color has turned red.
        /// </summary>
        private void mnuAlarmColor_Click(object sender, RoutedEventArgs e)
        {
            MeterFrame.Stroke = _meterFrameBrush;
        }

        #endregion

        private void MainGrid_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            MeterFrame.Stroke = _meterFrameBrush;
        }
    }
}
