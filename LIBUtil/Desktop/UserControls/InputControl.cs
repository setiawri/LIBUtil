using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIBUtil.Desktop.UserControls
{
    public partial class InputControl : UserControl
    {
        public virtual void reset() { }

        public virtual void focus() { }

        public virtual void setAsDefaultControl() { }
    }
}
