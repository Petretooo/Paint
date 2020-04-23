using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    class SquareShape : Shape
    {
		public SquareShape(RectangleF rect) : base(rect)
		{

		}

		public SquareShape(RectangleShape rectangle) : base(rectangle)
		{
		}



		public override bool Contains(PointF point)
		{
			if (base.Contains(point))

				return true;
			else
				return false;
		}

		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);


			grfx.FillRectangle(new SolidBrush(Color.FromArgb(Opacity, FillColor)), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawRectangle(new Pen(Color.FromArgb(Opacity, BorderColor), BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

		}
	}
}
