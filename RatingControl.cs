using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomToggleButton
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomToggleButton"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomToggleButton;assembly=CustomToggleButton"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:RatingControl/>
    ///
    /// </summary>
    public class RatingControl : Control
    {
        static RatingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingControl), new FrameworkPropertyMetadata(typeof(RatingControl)));
            FocusableProperty.OverrideMetadata(typeof(RatingControl), new FrameworkPropertyMetadata(true));
            IsTabStopProperty.OverrideMetadata(typeof(RatingControl), new FrameworkPropertyMetadata(true));
        }

        public static readonly DependencyProperty ValueProperty =
             DependencyProperty.Register(
                 nameof(Value), typeof(double), typeof(RatingControl),
                 new FrameworkPropertyMetadata(
                     0.0,
                     FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                     OnValueChanged,
                     CoerceValue));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum), typeof(double), typeof(RatingControl),
                new FrameworkPropertyMetadata(0.0, OnRangeChanged));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum), typeof(double), typeof(RatingControl),
                new FrameworkPropertyMetadata(5.0, OnRangeChanged));

        public static readonly DependencyProperty IsReadOnlyProperty =  
            DependencyProperty.Register(
                nameof(IsReadOnly), typeof(bool), typeof(RatingControl),
                new FrameworkPropertyMetadata(false, OnIsReadOnlyChanged));

        public static readonly DependencyProperty SmallChangeProperty=
            DependencyProperty.Register(
                nameof(SmallChange), typeof(double), typeof(RatingControl),
                new FrameworkPropertyMetadata(1.0));
        public static readonly DependencyProperty LargeChangeProperty =
           DependencyProperty.Register(
               nameof(LargeChange), typeof(double), typeof(RatingControl),
               new FrameworkPropertyMetadata(1.0));


        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        public double LargeChange
        {
            get => (double)GetValue(LargeChangeProperty);
            set => SetValue(LargeChangeProperty, value);
        }
    

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }   

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            var control = (RatingControl)d;
            double value = (double)baseValue;
            return Math.Max(control.Minimum, Math.Min(control.Maximum, value)); 
        }   
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RatingControl)d;
            var peer = (RatingControlAutomationPeer)UIElementAutomationPeer.FromElement(control);
            peer?.RaiseValueChanged((double)e.OldValue, (double)e.NewValue);
        }
        private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new RatingControlAutomationPeer(this);
        }
    }
}
