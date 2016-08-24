using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shiva.Shared.Interfaces;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace GnosisControls
{
    public  class GnosisTabHeaderButton : GnosisToggleButton, IGnosisTabHeaderButtonImplementation
    {
        public GnosisTabHeaderButton() :base()
        {
            Style style;
            if (this.Style == null)
            {
                style = new Style(this.GetType());
            }
            else
            {
                style = new Style(this.GetType(), this.Style);
            }

            Setter setter1 = new Setter(BorderThicknessProperty, new Thickness(0));
            style.Setters.Add(setter1);

            Setter setter2 = new Setter(MarginProperty, new Thickness(0));
            style.Setters.Add(setter2);

            Setter setter3 = new Setter(PaddingProperty, new Thickness(5));
            style.Setters.Add(setter3);

            this.Style = style;

        }
    }
}
