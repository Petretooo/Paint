using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

		private GroupShape selectGroup;
		public GroupShape SelectGroup 
		{
			get { return selectGroup; }
			set { selectGroup = value; }
		}
		public List<GroupShape> lastGroups = new List<GroupShape>();
		

		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}

		private bool isClicked;
		public bool IsClicked
		{
			get { return isClicked; }
			set { isClicked = value; }
		}

		private PointF current;
		public PointF Current
		{
			get { return current; }
			set { current = value; }
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
		public bool isTranslateActive = false;
		public bool isGroupActive = false;
		public bool isMultigroupActive = false;
		public GroupShape group = null;
		public List<GroupShape> multigroups = new List<GroupShape>();

		public int rotate = 45;
		public double scale = 1;

		public int Top;
		public int Bottom;
		public int Right;
		public int Left;

		public void addModelShape()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			ModelShape shape = new ModelShape(new RectangleF(x, y, 100, 100));

			ShapeList.Add(shape);
		}


		public void AddRandomRectangle()
		{

			Random rnd = new Random();
			float x = rnd.Next(100,1000);
			float y = rnd.Next(100,600);

			float z = rnd.Next(100, 600);
			float w = rnd.Next(100, 600);

			RectangleShape rect = new RectangleShape(new RectangleF(x, y, z, w));

			//RectangleShape rect = new RectangleShape(new RectangleF(x, y, 300, 150));

			ShapeList.Add(rect);
		}

		public void addRndomEllipse()
		{

			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			//EllipseShape ellipse = new EllipseShape(new RectangleF(x, y, 100, 100));

			EllipseShape ellipse = new EllipseShape(new RectangleF(x,y,100,200));



			ShapeList.Add(ellipse);
		}


		public void addRandomDot()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			DotShape dot = new DotShape(new RectangleF(x, y, 20, 20));

			ShapeList.Add(dot);
		}

		public void addRandomSquare()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			SquareShape square = new SquareShape(new RectangleF(x, y, 50, 50));

			ShapeList.Add(square);
		}

		public void addRandomLine()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			int z = rnd.Next(100, 1000);
			int w = rnd.Next(100, 600);

			LineShape line = new LineShape(new RectangleF(x, y, z, w));

			ShapeList.Add(line);
		}
		
		public void addRandomPolygon()
		{
			Random rnd = new Random();

			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);
			int z = rnd.Next(100, 600);
			int w = rnd.Next(100, 600);

			PolygonShape polygon = new PolygonShape(new RectangleF(x, y, z, w));

			ShapeList.Add(polygon);
		}

		public void addRandomTriangle()
		{
			Random rnd = new Random();

			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);
			int z = rnd.Next(100, 600);
			int w = rnd.Next(100, 600);

			TriangleShape triangle = new TriangleShape(new RectangleF(x,y,z,w));

			ShapeList.Add(triangle);

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

					Rotating(point);
					Scaling(point);
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
			if (isGroupActive && !isMultigroupActive)
			{
				if (group.isEmpty())
				{
					foreach(Shape s in group.GetListShapes())
					{
						s.Location = new PointF(s.Location.X + p.X - lastLocation.X, s.Location.Y + p.Y - lastLocation.Y);
						//lastLocation = p;
					}
					lastLocation = p;
				}
			}
			else if (isGroupActive && isMultigroupActive)
			{
				foreach(GroupShape group in multigroups)
				{
					foreach(Shape s in group.GetListShapes())
					{
						s.Location = new PointF(s.Location.X + p.X - lastLocation.X, s.Location.Y + p.Y - lastLocation.Y);
					}
				}
				lastLocation = p;
			}
			else if(selection != null) {
				selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
			}
		}

		public void Rotating(PointF p)
		{
			if (isGroupActive)
			{
				if (group.isEmpty())
				{
					foreach(Shape s in group.GetListShapes())
					{
						PointF[] matrixPoints = new PointF[]
						{
							s.Location,
							new PointF(s.Right, s.Top),
							new PointF(s.Right, s.Bottom),
							new PointF(s.Left, s.Bottom)
						};
						//s.matrix.RotateAt(rotate/**(float)Math.PI/180*/, new PointF(s.X + s.Width / 2, s.Top + s.Height / 2));
						s.Rectangle = new RectangleF(s.X, s.Y, s.Width, s.Height);
						//s.matrix.TransformPoints(matrixPoints);
					}
				}
			}
			else if(selection != null)
			{

				PointF[] matrixPoints = new PointF[]
				{
					selection.Location,
					new PointF(selection.Right, selection.Top),
					new PointF(selection.Right, selection.Bottom),
					new PointF(selection.Left, selection.Bottom)
				};


				//selection.matrix.RotateAt(rotate, new PointF(selection.X + selection.Width / 2, selection.Top + selection.Height / 2));
				selection.Rectangle = new RectangleF(selection.X, selection.Y, selection.Width, selection.Height);
				//selection.matrix.TransformPoints(matrixPoints);


			}


		}
		public void RotateAngle(float angle)
		{
			if(isMultigroupActive && isGroupActive)
			{
				foreach(GroupShape group in multigroups)
				{
					foreach (Shape s in group.GetListShapes())
					{
						s.Angle = angle;
						if (s.Angle >= 360)
						{
							s.Angle = 0;
						}

						if (angle == 45)
						{

							s.Angle += angle;

							if (s.Angle >= 360)
							{
								s.Angle = 0;
							}

						}
						else if (angle == 0)
						{
							s.Angle = angle;
						}
					}
				}
			}
			else if (isGroupActive && !isMultigroupActive)
			{
				if (group.isEmpty())
				{
					foreach (Shape s in group.GetListShapes())
					{
						s.Angle = angle;
						if (s.Angle >= 360)
						{
							s.Angle = 0;
						}

						if (angle == 45)
						{

							s.Angle += angle;

							if (s.Angle >= 360)
							{
								s.Angle = 0;
							}

						}
						else if (angle == 0)
						{
							s.Angle = angle;
						}
					}
				}
			}
			else if (selection != null)
			{
				selection.Angle = angle;
				if(selection.Angle >= 360)
				{
					selection.Angle = 0;
				}
				if (angle == 45)
				{

					selection.Angle += angle;

					if (selection.Angle >= 360)
					{
						selection.Angle = 0;
					}

				}
				else if (angle == 0)
				{
					selection.Angle = angle;
				}
			}
		}
		public override void Draw(Graphics grfx)
		{
				base.Draw(grfx);

			if (isMultigroupActive && isGroupActive)
			{
				foreach(GroupShape group in multigroups)
				{
					foreach(Shape s in group.GetListShapes())
					{
						s.RotateShape(grfx);
						grfx.DrawRectangle(Pens.Black, s.Location.X - 3 - (s.BorderWidth / 2), s.Location.Y - 3 - (s.BorderWidth / 2), s.Width + 6 + (s.BorderWidth), s.Height + 6 + (s.BorderWidth));
						grfx.ResetTransform();
					}
				}
			}
			else if (isGroupActive && !isMultigroupActive)
			{
				if (group.isEmpty() && group.GetListShapes().Count > 1)
				{
					foreach (Shape s in group.GetListShapes())
					{
						s.RotateShape(grfx);
						grfx.DrawRectangle(Pens.Black, s.Location.X - 3 - (s.BorderWidth / 2), s.Location.Y - 3 - (s.BorderWidth / 2), s.Width + 6 + (s.BorderWidth), s.Height + 6 + (s.BorderWidth));
						grfx.ResetTransform();
						
					}
				}
			}
			else if (selection != null)
			{
				Selection.RotateShape(grfx);
				grfx.DrawRectangle(Pens.Black, selection.Location.X - 3 - (selection.BorderWidth / 2), selection.Location.Y - 3 - (selection.BorderWidth / 2), selection.Width + 6 + (selection.BorderWidth), selection.Height + 6 + (selection.BorderWidth));
				grfx.ResetTransform();
				
			}

		}

		public void Scaling(PointF p)
		{
			isDragging = false;

			if(isGroupActive && isMultigroupActive)
			{
				foreach (GroupShape group in multigroups)
				{
					foreach (Shape s in group.GetListShapes())
					{
						PointF[] matrixPoints = new PointF[]
						{
							s.Location,
							new PointF(s.Right, s.Top),
							new PointF(s.Right, s.Bottom),
							new PointF(s.Left, s.Bottom),
							new PointF(s.Left, s.Top)
						};

						s.Width = s.Width * (float)scale;
						s.Height = s.Height * (float)scale;
						//s.matrix.Scale((float)scale, (float)scale);
						//s.matrix.TransformPoints(matrixPoints);
					}
					lastLocation = p;
				}
			}
			else if (isGroupActive && !isMultigroupActive)
			{
				if (group.isEmpty())
				{
					foreach(Shape s in group.GetListShapes())
					{
						PointF[] matrixPoints = new PointF[]
						{
							s.Location,
							new PointF(s.Right, s.Top),
							new PointF(s.Right, s.Bottom),
							new PointF(s.Left, s.Bottom),
							new PointF(s.Left, s.Top)
						};

						s.Width = s.Width * (float)scale;
						s.Height = s.Height * (float)scale;
						//s.matrix.Scale((float)scale, (float)scale);
						//s.matrix.TransformPoints(matrixPoints);
					}
					lastLocation = p;
				}
			}
			else if(selection != null)
			{
				PointF[] matrixPoints = new PointF[]
				{
					selection.Location,
					new PointF(selection.Right, selection.Top),
					new PointF(selection.Right, selection.Bottom),
					new PointF(selection.Left, selection.Bottom),
					new PointF(selection.Left, selection.Top)
				};

				/*				if (e.Button == MouseButtons.Left)
								{
									selection.matrix.Scale((float)1.1, (float)1.1);
									selection.matrix.TransformPoints(matrixPoints);
								}
								else if (e.Button == MouseButtons.Right)
								{
									selection.matrix.Scale((float)0.9, (float)0.9);
									selection.matrix.TransformPoints(matrixPoints);
								}*/
				selection.Width = selection.Width *(float)scale;
				selection.Height = selection.Height * (float)scale;
				//selection.matrix.Scale((float)scale, (float)scale);
				//selection.matrix.TransformPoints(matrixPoints);
			}

			lastLocation = p;
		}

		public void Delete()
		{
			if(isMultigroupActive && isGroupActive)
			{
				foreach(GroupShape group in multigroups)
				{
					foreach (Shape s in group.GetListShapes())
					{
						ShapeList.Remove(s);

					}
					group.clear();
				}
			}
			else if (isGroupActive && !isMultigroupActive)
			{
				if (group.isEmpty())
				{
					foreach(Shape s in group.GetListShapes())
					{
						ShapeList.Remove(s);
						
					}
					group.clear();
				}
			}
			else if (selection != null)
			{
				ShapeList.Remove(selection);
				selection = null;
			}

		}

		internal void Paste()
		{
			if (Current != null)
			{
				try
				{
					if (isGroupActive && !isMultigroupActive)
					{
						GroupShape gs = (GroupShape)DeSerialize();
						PointF p = gs.Location;
						TranslateTo(p);
						ShapeList.Add(gs);
					}
					else
					{
						Shape s = (Shape)DeSerialize();
						PointF p = s.Location;
						s.Location = Current;
						TranslateTo(p);
						ShapeList.Add(s);
					}
				}
				catch (Exception e)
				{

				}
			}
		}



		internal void CopySelect()
        {
			if(isMultigroupActive && isGroupActive)
			{
				Serialize(multigroups);
			}
			else if (isGroupActive && !isMultigroupActive)
			{
				if (group.isEmpty())
				{
					Serialize(group);
				}
			}
			else if(selection != null)
			{
				Serialize(selection);
			}
			
        }


		public void Serialize(object obj, string filePath = null)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream;
			if (filePath != null)
			{
				stream = new FileStream(filePath + ".bin",
								  FileMode.Create);
			}
			else
			{
				stream = new FileStream("MyFile.bin",
										FileMode.Create,
										FileAccess.Write, FileShare.None);
			}
			formatter.Serialize(stream, obj);
			stream.Close();
		}

		public object DeSerialize(string filePath = null)
		{
			object obj;
			IFormatter formatter = new BinaryFormatter();
			Stream stream;
			if (filePath != null)
			{
				stream = new FileStream(filePath,
									 FileMode.Open,
									 FileAccess.Read, FileShare.None);
			}
			else
			{
				stream = new FileStream("MyFile.bin",
									FileMode.Open);
			}
			obj = formatter.Deserialize(stream);
			stream.Close();
			return obj;
		}
	}
}
