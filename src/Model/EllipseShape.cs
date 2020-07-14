using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	class EllipseShape : Shape
    {

		public EllipseShape(RectangleF rect):base(rect)
		{

		}
		
		public EllipseShape(EllipseShape ellipse) : base(ellipse)
		{

		}

		
		public override bool Contains(PointF point)
		{
			GraphicsPath path = new GraphicsPath();
			path.AddEllipse(new RectangleF(Rectangle.X, Rectangle.Y, 100, 100));
			bool a = path.IsVisible(point);
			return a;

		}



		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			base.RotateShape(grfx);
			grfx.FillEllipse(new SolidBrush(Color.FromArgb(Opacity, FillColor)), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(Color.FromArgb(Opacity,BorderColor), BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			



			grfx.ResetTransform();
		}
	}
}
