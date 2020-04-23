using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    class DotShape : Shape
    {
		public DotShape(RectangleF rect) : base(rect)
		{

		}

		public DotShape(EllipseShape ellipse) : base(ellipse)
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

			grfx.FillEllipse(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(Pens.Black, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

		}
	}
}
