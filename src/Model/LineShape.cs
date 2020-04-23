using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    class LineShape : Shape
    {

		public LineShape(RectangleF rect) : base(rect)
		{
		}

		public LineShape(RectangleShape rectangle) : base(rectangle)
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
			//grfx.DrawLine(Pens.Black, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			
			Pen pen = new Pen(Color.Black, 6);
			pen.StartCap = LineCap.RoundAnchor;
			pen.EndCap = LineCap.RoundAnchor;
			grfx.DrawLine(pen, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);


		}
	}
}
