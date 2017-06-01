using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DllMeter.Helpers
{
    public class PieSlice : Shape
    {
        public static readonly DependencyProperty CenterProperty =
            EllipseGeometry.CenterProperty.AddOwner(typeof(PieSlice),
                new FrameworkPropertyMetadata(OnGeometryPropertyChanged));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius",
                typeof(double),
                typeof(PieSlice),
                new FrameworkPropertyMetadata(OnGeometryPropertyChanged));

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle",
                typeof(double),
                typeof(PieSlice),
                new FrameworkPropertyMetadata(OnGeometryPropertyChanged));

        public static readonly DependencyProperty SweepAngleProperty =
            DependencyProperty.Register("SweepAngle",
                typeof(double),
                typeof(PieSlice),
                new FrameworkPropertyMetadata(OnGeometryPropertyChanged));

        static readonly DependencyPropertyKey CenterAngleKey =
            DependencyProperty.RegisterReadOnly("CenterAngle",
                typeof(double),
                typeof(PieSlice),
                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty CenterAngleProperty =
            CenterAngleKey.DependencyProperty;

        PathGeometry pathGeometry = new PathGeometry();
        PathFigure pathFigure = new PathFigure();
        LineSegment lineSegment = new LineSegment();
        ArcSegment arcSegment = new ArcSegment();

        public PieSlice()
        {
            pathFigure.IsClosed = true;
            pathFigure.Segments.Add(lineSegment);
            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);
        }

        public Point Center
        {
            set { SetValue(CenterProperty, value); }
            get { return (Point)GetValue(CenterProperty); }
        }

        public double Radius
        {
            set { SetValue(RadiusProperty, value); }
            get { return (double)GetValue(RadiusProperty); }
        }

        public double StartAngle
        {
            set { SetValue(StartAngleProperty, value); }
            get { return (double)GetValue(StartAngleProperty); }
        }

        public double SweepAngle
        {
            set { SetValue(SweepAngleProperty, value); }
            get { return (double)GetValue(SweepAngleProperty); }
        }

        public double CenterAngle
        {
            protected set { SetValue(CenterAngleKey, value); }
            get { return (double)GetValue(CenterAngleProperty); }
        }

        static void OnGeometryPropertyChanged(DependencyObject obj,
                                              DependencyPropertyChangedEventArgs args)
        {
            (obj as PieSlice).OnGeometryPropertyChanged(args);
        }

        void OnGeometryPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            pathFigure.StartPoint = Center;

            double angle = Math.PI * StartAngle / 180;
            double x = Center.X + Radius * Math.Sin(angle);
            double y = Center.Y - Radius * Math.Cos(angle);
            lineSegment.Point = new Point(x, y);

            angle = Math.PI * (StartAngle + SweepAngle) / 180;
            x = Center.X + Radius * Math.Sin(angle);
            y = Center.Y - Radius * Math.Cos(angle);
            arcSegment.Point = new Point(x, y);
            arcSegment.Size = new Size(Radius, Radius);
            arcSegment.IsLargeArc = SweepAngle > 180;
            arcSegment.SweepDirection = SweepDirection.Clockwise;

            CenterAngle = StartAngle + SweepAngle / 2;

            InvalidateMeasure();
        }

        protected override System.Windows.Media.Geometry DefiningGeometry
        {
            get
            {
                return pathGeometry;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return new Size(Center.X + Radius, Center.Y + Radius);
        }
    }
}
