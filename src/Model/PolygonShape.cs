using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    class PolygonShape : Shape
    {

        PointF[] points;


        public PolygonShape(RectangleF rect) : base(rect)
		{

		}

		public PolygonShape(RectangleShape rectangle) : base(rectangle)
		{
		}

        public PolygonShape(List<PointF> PolygonPoints) : base(PolygonPoints)
        {
            
        }


        public override bool Contains(PointF point)
        {
            return CalculatePoint.IsPointInPolygon(points, point);
        }

        public override void DrawSelf(Graphics grfx)
		{
            Random rnd = new Random();

            points = base.PolygonPoints.ToArray();


            base.DrawSelf(grfx);

            grfx.FillPolygon(new SolidBrush(Color.FromArgb(Opacity, FillColor)), points);
            grfx.DrawPolygon(new Pen(Color.FromArgb(Opacity,BorderColor), BorderWidth), points);

		}
	}
}
