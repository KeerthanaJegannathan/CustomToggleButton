using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace CustomToggleButton
{
    public class ToggleSwitchAutomationPeer : FrameworkElementAutomationPeer
    {
        public ToggleSwitchAutomationPeer(ToggleSwitch owner) : base(owner)
        {
        }

        protected override string GetClassNameCore() => nameof(ToggleSwitch);

        //The AutomationControlType for a ToggleSwitch is set to Button. This is because the ToggleSwitch behaves like a button that can be toggled on or off.  
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Button;
        }   
    }
}
