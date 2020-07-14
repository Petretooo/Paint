using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
    public class ModelShape : Shape
    {
		public ModelShape(RectangleF rect) : base(rect)
		{
		}

		public ModelShape(RectangleShape rectangle) : base(rectangle)
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

			grfx.FillEllipse(new SolidBrush(Color.FromArgb(Opacity, FillColor)), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(Color.FromArgb(Opacity, BorderColor), BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);


			//Exam task

			PointF CE = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);

			PointF RightUp = new PointF(Rectangle.X, Rectangle.Y + Rectangle.Height / 2);
			PointF LeftDown = new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height / 2);

			PointF LeftUp = new PointF((float)(CE.X + Rectangle.Width / 2 / Math.Sqrt(2)), (float)(CE.Y + Rectangle.Height / -2 / Math.Sqrt(2)));
			PointF RightDown = new PointF((float)(CE.X + Rectangle.Width / -2 / Math.Sqrt(2)), (float)(CE.Y + Rectangle.Height / 2 / Math.Sqrt(2)));

			

			grfx.DrawLine(new Pen(Color.FromArgb(Opacity, BorderColor), BorderWidth), RightUp, LeftUp);
			grfx.DrawLine(new Pen(Color.FromArgb(Opacity, BorderColor), BorderWidth), RightDown, LeftDown);

			//Exam task end

			grfx.ResetTransform();
		}
	}
}
