using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
	[Serializable]
	public abstract class Shape
	{
		#region Constructors


		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
		
		

		public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;

			

			this.BorderWidth = shape.borderWidth;
			this.BorderColor = shape.BorderColor;
			this.Opacity = shape.Opacity;
			this.FillColor =  shape.FillColor;
		}

		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}


		
		public virtual float Top
		{
			get { return Rectangle.Top; }
		}
		public virtual float Bottom
		{
			get { return Rectangle.Bottom; }
		}
		public virtual float Right
		{
			get { return Rectangle.Right; }
		}
		public virtual float Left
		{
			get { return Rectangle.Left; }
		}
		public virtual float X
		{
			get { return Rectangle.X; }
		}
		public virtual float Y
		{
			get { return Rectangle.Y; }
		}
		
		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		private string name;
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}



		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}

		private Color borderColor = Color.Black;
		public virtual Color BorderColor {
			get { return borderColor; }
			set { borderColor = value; }
		}

		private int borderWidth = 5;
		public virtual int BorderWidth {
			get { return borderWidth; }
			set { borderWidth = value; }
		}

		private int opacity = 255;
		public virtual int Opacity {
			get { return opacity; }
			set { opacity = value; }
		}


		#endregion
		private float rotate;
		public virtual float Rotate
		{
			get { return rotate; }
			set { this.rotate = value; }
		}

		private float scale;
		public virtual float Scale 
		{
			get { return scale; }
			set { this.scale = value; }
		}

		public virtual void Rotation(Graphics grfx)
		{

		}

		public virtual void Scaling(Graphics grfx)
		{

		}

/*		public virtual void RotateShape(Matrix m)
		{
			
		}

		public virtual void ScaleShape(Matrix m)
		{

		}*/
		
		/// <summary>
		/// Проверка дали точка point принадлежи на елемента.
		/// </summary>
		/// <param name="point">Точка</param>
		/// <returns>Връща true, ако точката принадлежи на елемента и
		/// false, ако не пренадлежи</returns>
		public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}

		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}

		private float angle;
		public virtual float Angle
		{
			get { return angle; }
			set { angle = value; }
		}
		public virtual void RotateShape(Graphics grfx)
		{

			grfx.TranslateTransform(Rectangle.X + Rectangle.Width / 2, Rectangle.Y + Rectangle.Height / 2);
			grfx.RotateTransform(Angle);

			grfx.TranslateTransform(-(Rectangle.X + Rectangle.Width / 2), -(Rectangle.Y + Rectangle.Height / 2));

		}

	}
}
