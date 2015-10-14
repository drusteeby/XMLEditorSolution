using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachineTagEditor.Infrastructure.Controls
{
    public static class InputMessageBox
    {
        public static KeyValuePair<string,string> ShowMyDialogBox()
        {
            AttributeInputForm testDialog = new AttributeInputForm();

            string nameText = null;
            string valueText = null;
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog() == DialogResult.OK)
            {
                nameText  = testDialog.nameTxtBx.Text;
                valueText = testDialog.valueTxtBx.Text;
                testDialog.Dispose();
                
            }

            return new KeyValuePair<string, string>(nameText, valueText);
        }


    }

}
