using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIMusicApp
{
    public class CustomTrackBar : TrackBar
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle trackRect = new Rectangle(0, this.Height / 2 - 2, this.Width, 4);
            int thumbX = (int)((this.Value - this.Minimum) * 1.0 / (this.Maximum - this.Minimum) * this.Width);
            Rectangle thumbRect = new Rectangle(thumbX - 7, this.Height / 2 - 7, 14, 14);
            e.Graphics.FillRectangle(Brushes.Gray, trackRect);
            e.Graphics.FillRectangle(Brushes.Green, new Rectangle(trackRect.X, trackRect.Y, thumbX, trackRect.Height));
            e.Graphics.FillEllipse(Brushes.White, thumbRect);
            e.Graphics.DrawEllipse(Pens.Black, thumbRect);
        }
    }
}
