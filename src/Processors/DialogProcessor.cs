using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Избран елемент.
		/// </summary>
		private Shape selection;
		public Shape Selection {
			get { return selection; }
			set { selection = value; }
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		/// 



		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);

			int z = rnd.Next(100, 600);
			int w = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, z, w));
			rect.FillColor = Color.Blue;
			rect.BorderColor = Color.Black;
			rect.BorderWidth = 25;
			rect.Opacity = 20;
			ShapeList.Add(rect);
		}

		public void addRndomEllipse()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShape ellipse = new EllipseShape(new RectangleF(x,y,100,200));
			ellipse.FillColor = Color.Green;
			ellipse.Opacity = 100;

			ShapeList.Add(ellipse);
		}


		public void addRandomDot()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			DotShape dot = new DotShape(new RectangleF(x, y, 20, 20));
			dot.FillColor = Color.Black;
			ShapeList.Add(dot);
		}

		public void addRandomSquare()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			SquareShape square = new SquareShape(new Rectangle(x, y, 50, 50));
			square.FillColor = Color.White;
			ShapeList.Add(square);
		}

		public void addRandomLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			int z = rnd.Next(100, 1000);
			int w = rnd.Next(100, 600);

			LineShape line = new LineShape(new Rectangle(x, y, z, w));
			ShapeList.Add(line);
		}
		
		public void addRandomPolygon()
		{
			Random rnd = new Random();
			Point point1 = new Point(rnd.Next(100, 700), rnd.Next(100, 700));
			Point point2 = new Point(rnd.Next(100, 700), rnd.Next(100, 700));
			Point point3 = new Point(rnd.Next(100, 700), rnd.Next(100, 700));
			Point point4 = new Point(rnd.Next(100, 700), rnd.Next(100, 700));
			Point point5 = new Point(rnd.Next(100, 700), rnd.Next(100, 700));

			List<Point> points = new List<Point>();
			points.Add(point1);
			points.Add(point2);
			points.Add(point3);
			points.Add(point4);
			points.Add(point5);

			PolygonShape polygon = new PolygonShape(points);
			polygon.FillColor = Color.White;
			polygon.BorderWidth = 6;
			polygon.BorderColor = Color.Red;
			polygon.Opacity = 100;
			ShapeList.Add(polygon);
		}

		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					ShapeList[i].FillColor = Color.Red;
						
					return ShapeList[i];
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			if (selection != null) {
				selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
			}
		}
	}
}
