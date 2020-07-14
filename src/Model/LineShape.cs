using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	public class LineShape : Shape
    {

		public LineShape(RectangleF rect) : base(rect)
		{
		}

		public LineShape(LineShape rectangle) : base(rectangle)
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
			base.RotateShape(grfx);
			//grfx.DrawLine(Pens.Black, Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

			Pen pen = new Pen(Color.FromArgb(Opacity, FillColor), BorderWidth);
			pen.StartCap = LineCap.RoundAnchor;
			pen.EndCap = LineCap.RoundAnchor;

			Point point1 = new Point((int)Rectangle.X + ((int)Rectangle.Width), (int)Rectangle.Y);
			Point point2 = new Point((int)Rectangle.X, (int)(Rectangle.Y + Rectangle.Height));
			//grfx.Transform = matrix;
			grfx.DrawLine(pen, point1, point2);

			grfx.ResetTransform();
		}
	}
}
