using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	public class TriangleShape : Shape
	{

		Point[] points1;

		public TriangleShape(RectangleF rect) : base(rect)
		{

		}

		public TriangleShape(TriangleShape triangle) : base(triangle)
		{

		}


		public override bool Contains(PointF point)
		{
/*			GraphicsPath path = new GraphicsPath();
			path.AddPolygon(points1);
			bool a = path.IsVisible(point);
			return a;*/


			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}


		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);
			base.RotateShape(grfx);

			Random rnd = new Random();


			Point[] points = { 

				new Point( (int)Rectangle.X + ((int)Rectangle.Width/2), (int)Rectangle.Y ),
				new Point( (int)Rectangle.X, (int)(Rectangle.Y + Rectangle.Height) ),
				new Point( (int)(Rectangle.X + Rectangle.Width), (int)(Rectangle.Y + Rectangle.Height) )

			};

			points1 = points;

			//grfx.Transform = matrix;
			
			grfx.FillPolygon(new SolidBrush(Color.FromArgb(Opacity, FillColor)), points);
			grfx.DrawPolygon(new Pen(Color.FromArgb(Opacity, BorderColor), BorderWidth), points);
			grfx.ResetTransform();
		}


	}
}