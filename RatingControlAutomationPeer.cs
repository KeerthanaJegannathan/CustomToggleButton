using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace CustomToggleButton
{
    internal class RatingControlAutomationPeer : FrameworkElementAutomationPeer,
        IRangeValueProvider 
    {
        public RatingControlAutomationPeer(RatingControl owner) : base(owner)
        {
        }

        private RatingControl Rating => (RatingControl)Owner;

        protected override string GetClassNameCore() => nameof(RatingControl);

        protected override AutomationControlType GetAutomationControlTypeCore() => AutomationControlType.Slider;

        protected override string GetLocalizedControlTypeCore() => "rating";

        protected override string GetNameCore()
        {
            string name = base.GetNameCore();
            return string.IsNullOrEmpty(name) ? "Rating" : name;
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            if(patternInterface == PatternInterface.RangeValue)
            {
                return this;
            }
            return base.GetPattern(patternInterface);
        }
        //IRangeValue Provider
        public bool IsReadOnly => Rating.IsReadOnly;

        public double LargeChange => Rating.LargeChange;

        public double Maximum => Rating.Maximum;

        public double Minimum => Rating.Minimum;

        public double SmallChange => Rating.SmallChange;

        public double Value => Rating.Value;

        public void SetValue(double value)
        {
           if(!IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            if (IsReadOnly)
            {
                throw new InvalidOperationException("Control is read-only.");
            }
            if (value < Minimum || value > Maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value is out of range.");
            }
            Rating.Value = value;   
        }

        internal void RaiseValueChanged(double oldValue, double newValue)
        {
            throw new NotImplementedException();
        }

        internal void RaiseIsReadOnlyChanged(bool oldValue, bool newValue)
        {
            throw new NotImplementedException();
        }
    }   
}
