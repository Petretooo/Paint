using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Draw.src.Model
{
    class PolygonShape : Shape
    {

		public PolygonShape(RectangleF rect) : base(rect)
		{

		}

		public PolygonShape(RectangleShape rectangle) : base(rectangle)
		{
		}

        public PolygonShape(List<Point> PolygonPoints) : base(PolygonPoints)
        {
            
        }


        public override bool Contains(PointF point)
        {
            foreach(Point p in base.PolygonPoints){
                if (!base.Contains(p))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public override void DrawSelf(Graphics grfx)
		{
            Random rnd = new Random();

            Point[] points = base.PolygonPoints.ToArray();


            base.DrawSelf(grfx);

            grfx.FillPolygon(new SolidBrush(Color.FromArgb(Opacity, FillColor)), points);
            grfx.DrawPolygon(new Pen(Color.FromArgb(Opacity,BorderColor), BorderWidth), points);

		}
	}
}
