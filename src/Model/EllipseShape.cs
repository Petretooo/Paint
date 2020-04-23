using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
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
			if (base.Contains(point))
				// Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
				// В случая на правоъгълник - директно връщаме true
				return true;
			else
				// Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
				return false;
		}

		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.FillEllipse(new SolidBrush(Color.FromArgb(Opacity, FillColor)), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.DrawEllipse(new Pen(Color.FromArgb(Opacity,BorderColor), BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

		}
	}
}
