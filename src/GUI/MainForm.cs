using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}


		void drawEllipseSpeedButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRndomEllipse();
			statusBar.Items[0].Text = "Drawing ellipse";
			viewPort.Invalidate();

		}

		void drawDotSpeedButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomDot();
			statusBar.Items[0].Text = "Drawing dot";
			viewPort.Invalidate();
		}

		void drawSquareSpeedButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomSquare();
			statusBar.Items[0].Text = "Drawing square";
			viewPort.Invalidate();
		}

		void drawLineSpeedButton_Click_1(object sender, EventArgs e)
		{
			dialogProcessor.addRandomLine();
			statusBar.Items[0].Text = "Drawing line";
			viewPort.Invalidate();
		}

		void drawPolygonSpeedButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomPolygon();
			statusBar.Items[0].Text = "Drawing polygin";
			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked) {
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null) {
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

		
	}
}
