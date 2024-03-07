using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BookingApp.Global
{
    public static class ToolTipSettings
    {
        private static Visibility toolTipVis = Visibility.Visible;
        public static Visibility ToolTipVis
        {
            get { return toolTipVis; }
            set { toolTipVis = value; }
        }

    }
}
