using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace CustomToggleButton
{
    public class ToggleSwitchAutomationPeer : FrameworkElementAutomationPeer, IToggleProvider   
    {
        public ToggleSwitchAutomationPeer(ToggleSwitch owner) : base(owner)
        {
        }

        private ToggleSwitch Switch => (ToggleSwitch)Owner;


        protected override string GetClassNameCore() => nameof(ToggleSwitch);

        //The AutomationControlType for a ToggleSwitch is set to Button. This is because the ToggleSwitch behaves like a button that can be toggled on or off.  
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }
        protected override string GetNameCore()
        {
            string name= base.GetNameCore();
            return string.IsNullOrEmpty(name) ? Switch.Label : name;    
        }

        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Toggle)
            {
                return this;
            }
            return base.GetPattern(patternInterface);
        }

        // Implement the Toggle method from the IToggleProvider interface   
        public ToggleState ToggleState => Switch.IsOn ? ToggleState.On : ToggleState.Off;
        public void Toggle()
        {
            if (!IsEnabled())
            {
                throw new ElementNotEnabledException();

            }
            Switch.Toggle();
        }

        //Called by the control when IsOn property changes

        internal void RaiseToggleStateChanged(bool oldValue, bool newValue)
        {
            RaisePropertyChangedEvent(
                TogglePatternIdentifiers.ToggleStateProperty,
                oldValue ? ToggleState.On : ToggleState.Off,
                newValue ? ToggleState.On : ToggleState.Off);
        }   
    }
}
