﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
	[Serializable]
	public class PolygonShape : Shape
    {


        public PolygonShape(RectangleF rect) : base(rect)
		{

		}

		public PolygonShape(PolygonShape rectangle) : base(rectangle)
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


		public override void DrawSelf(Graphics grfx)
		{
            base.DrawSelf(grfx);
			base.RotateShape(grfx);

			Random rnd = new Random();

			Point[] p = {

				new Point( (int)Rectangle.X + ((int)Rectangle.Width/2), (int)Rectangle.Y ),
				new Point( (int)Rectangle.X, (int)(Rectangle.Y + Rectangle.Height) ),
				new Point( (int)(Rectangle.X + Rectangle.Width), (int)(Rectangle.Y + Rectangle.Height) ),
				new Point( (int)Rectangle.X + ((int)Rectangle.Width/2) + 50, (int)Rectangle.Y ),
				new Point( (int)Rectangle.X + ((int)Rectangle.Width/2), (int)Rectangle.Y + 100 ),
				new Point( (int)Rectangle.X + ((int)Rectangle.Width/2 - 150), (int)Rectangle.Y + 50 ),
				new Point( (int)Rectangle.X+400, (int)(Rectangle.Y + Rectangle.Height) )
			};
			//grfx.Transform = matrix;
			grfx.FillPolygon(new SolidBrush(Color.FromArgb(Opacity, FillColor)), p);
            grfx.DrawPolygon(new Pen(Color.FromArgb(Opacity,BorderColor), BorderWidth), p);
			grfx.ResetTransform();
		}
	}
}
