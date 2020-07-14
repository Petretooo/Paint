using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
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
			statusBar.Items[0].Text = "Drawing polygon";
			viewPort.Invalidate();
		}

		private void drawTriangleSpeedButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomTriangle();
			statusBar.Items[0].Text = "Drawing triangle";
			viewPort.Invalidate();
		}

		private void modelStripButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.addModelShape();
			statusBar.Items[0].Text = "Drawing MODEL";
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
			


			if (e.Button == MouseButtons.Right)
			{
				var containsCurrent = dialogProcessor.ContainsPoint(e.Location);

				if (containsCurrent != null)
				{

					if (dialogProcessor.ShapeList.Contains(containsCurrent))
					{
						contextMenuFirst.Show(this, new Point(e.X, e.Y));

						dialogProcessor.Selection = containsCurrent;

					}
				}
				else
				{
					if (dialogProcessor.Selection != null)
					{
						contextMenuSecond.Show(this, new Point(e.X, e.Y));
						if (dialogProcessor.Current != null)
						{
							dialogProcessor.Current = new PointF(e.X, e.Y);
						}
					}
				}
			}
			if (groupButton.Checked && !MultigroupButton1.Checked)
			{
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if(dialogProcessor.Selection != null)
				{
					if (e.Button == MouseButtons.Left)
					{
						dialogProcessor.group.addInGroup(dialogProcessor.Selection);
						
					}else if(e.Button == MouseButtons.Middle)
					{
						dialogProcessor.group.removeFromGroup(dialogProcessor.Selection);
					}
				}

				
			}
			if (MultigroupButton1.Checked && groupButton.Checked)
			{
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if(dialogProcessor.Selection != null)
				{
					if(e.Button == MouseButtons.Left)
					{

						/*bool found = false;
						GroupShape g = null;
						foreach (GroupShape groups in dialogProcessor.multigroups)
						{
							foreach (Shape s in groups.GetListShapes())
							{
								if (s == dialogProcessor.Selection)
								{
									found = true;
									g = groups;
								}
							}
						}
						if (found && !dialogProcessor.multigroups.Contains(g))
						{
							found = false;
							dialogProcessor.group = g;
							dialogProcessor.multigroups.Add(g);
						}
						else
						{
							foreach(GroupShape groups in dialogProcessor.LastGroups)
							{
								foreach(Shape s in groups.GetListShapes())
								{
									if(s == dialogProcessor.Selection)
									{
										dialogProcessor.multigroups.Add(groups);
									}
								}
							}
							//dialogProcessor.multigroups.Add(dialogProcessor.LastGroup);
						}
*/

						if (!dialogProcessor.multigroups.Contains(dialogProcessor.group) && !dialogProcessor.lastGroups.Contains(dialogProcessor.group))
						{
							dialogProcessor.multigroups.Add(dialogProcessor.group);
						}
						else
						{
							bool found = false;
							GroupShape g = null;
							if (dialogProcessor.lastGroups != null)
							{
								foreach (GroupShape groups in dialogProcessor.lastGroups)
								{
									foreach (Shape s in groups.GetListShapes())
									{
										if (s == dialogProcessor.Selection)
										{
											found = true;
											g = groups;
											//dialogProcessor.multigroups.Add(groups);
										}
										
									}
									//dialogProcessor.lastGroups.Remove(groups);
								}
								if (found)
								{
									dialogProcessor.multigroups.Add(g);
									dialogProcessor.lastGroups.Remove(g);
								}
							}
						}
					}
					else if(e.Button == MouseButtons.Middle)
					{
						bool found = false;
						GroupShape g = null;
						foreach(GroupShape groups in dialogProcessor.multigroups)
						{
							foreach(Shape s in groups.GetListShapes())
							{
								if(s == dialogProcessor.Selection)
								{
									found = true;
									g = groups;
								}
							}
						}
						if (found)
						{
							found = false;
							if (!dialogProcessor.lastGroups.Contains(g))
							{
								dialogProcessor.lastGroups.Add(g);
							}
							dialogProcessor.multigroups.Remove(g);
						}

/*						if (!dialogProcessor.multigroups.Contains(dialogProcessor.group))
						{
							dialogProcessor.multigroups.Remove(dialogProcessor.group);
						}*/
					}
				}
			}


			if (pickUpSpeedButton.Checked) {
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null) {
					
					//statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
					
				}
			}
			else if (rotateSpeedButton.Checked)
			{

				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null)
				{
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
				if (pickUpSpeedButton.Checked)
				{
					dialogProcessor.TranslateTo(e.Location);
				}
				else if (rotateSpeedButton.Checked)
				{
					if (e.Button == MouseButtons.Right)
					{
						dialogProcessor.Rotating(e.Location);
					}
				}
				
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
			dialogProcessor.IsClicked = false;
		}

		private void viewPort_Load(object sender, EventArgs e)
		{

		}

		private void rotateSpeedButton_Click(object sender, EventArgs e)
		{
			
		}

		private void fillColorDropDownMeny_Click(object sender, EventArgs e)
		{
			
		}

		private void rEDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);

			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Red);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Red);
			}
			else if(dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Red;
			}
			viewPort.Invalidate();
			//dialogProcessor.fillColor = Color.Red;
		}

		private void bLUEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);

			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Blue);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Blue);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Blue;
			}
			viewPort.Invalidate();
		}

		private void gREENToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Green);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Green);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Green;
			}
			viewPort.Invalidate();
		}

		private void yELLOWToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Yellow);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Yellow);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Yellow;
			}
			viewPort.Invalidate();
		}

		private void oRANGEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Orange);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Orange);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Orange;
			}
			viewPort.Invalidate();
		}

		private void bLACKToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Black);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Black);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Black;
			}
			viewPort.Invalidate();
		}

		private void borderColorDropDownMeny_Click(object sender, EventArgs e)
		{

		}

		private void rEDToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Red);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Red);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Red;
			}
			viewPort.Invalidate();
		}

		private void bLUEToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Blue);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Blue);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Blue;
			}
			viewPort.Invalidate();
		}

		private void gREENToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Green);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Green);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Green;
			}
			viewPort.Invalidate();
		}

		private void yELLOWToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Yellow);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Yellow);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Yellow;
			}
			viewPort.Invalidate();
		}

		private void oRANGEToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Orange);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Orange);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Orange;
			}
			viewPort.Invalidate();
		}

		private void bLACKToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Black);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Black);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Black;
			}
			viewPort.Invalidate();
		}

		private void opacityDropDownMenu_Click(object sender, EventArgs e)
		{

		}

		private void toolStripMenuItem14_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.05));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.05));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.05);
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.1));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.1));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.1);
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.2));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.2));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.2);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem6_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.3));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.3));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.3);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem7_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.4));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.4));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.4);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem8_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.5));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.5));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.5);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem9_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.6));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.6));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.6);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem10_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.7));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.7));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.7);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem11_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.8));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.8));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.8);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem12_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.9));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.9));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.9);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem13_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 1));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 1));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 1);
			}
			viewPort.Invalidate();
		}

		private void toolStripDropDownButton1_Click(object sender, EventArgs e)
		{

		}

		private void pxToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(0);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(0);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 0;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem3_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(1);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(1);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 1;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem4_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(3);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(3);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 3;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem5_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(6);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(6);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 6;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem6_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(9);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(9);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 9;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem7_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(12);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(12);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 12;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem8_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(15);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(15);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 15;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem9_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(18);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(18);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 18;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem10_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(21);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(21);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 21;
			}
			viewPort.Invalidate();
		}

		private void pxToolStripMenuItem11_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(24);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(24);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 24;
			}
			viewPort.Invalidate();
		}

		private void scaleSpeedButton_Click(object sender, EventArgs e)
		{
			
		}

		private void buttonToRotate_Click(object sender, EventArgs e)
		{

		}

		private void buttonToScale_Click(object sender, EventArgs e)
		{

		}

		private void degreeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.RotateAngle(45);
			statusBar.Items[0].Text = "Rotating";
			viewPort.Invalidate();
			dialogProcessor.rotate = 45;
			//todo 45 degree
		}

		private void degreeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.RotateAngle(0);
			statusBar.Items[0].Text = "Rotating";
			viewPort.Invalidate();
			dialogProcessor.rotate = 0;
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.scale = 1.1;
			viewPort.Invalidate();
			//todo+10
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.scale = 0.9;
			viewPort.Invalidate();
			//todo-10
		}



		private void sameShapeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.scale = 1;
			viewPort.Invalidate();
		}

		public void groupButton_Click(object sender, EventArgs e)
		{
			if (groupButton.Checked)
			{
				dialogProcessor.group = new GroupShape();
				dialogProcessor.isGroupActive = true;
			}else if (!groupButton.Checked)
			{
				dialogProcessor.group = null;
				dialogProcessor.isGroupActive = false;
			}
		}

		private void viewPort_Load_1(object sender, EventArgs e)
		{

		}

		private void toolStripTextBox1_Click(object sender, EventArgs e)
		{
			
			

			/*TextBox objText = (TextBox)sender;
			if (!String.IsNullOrEmpty(objText.Text))
			{
				dialogProcessor.Selection.Name = objText.Text;
			}*/
		}

		private void setNameButton1_Click(object sender, EventArgs e)
		{
			if (dialogProcessor.Selection != null)
			{
				if (!String.IsNullOrEmpty(toolStripTextBox1.Text))
				{
					dialogProcessor.Selection.Name = toolStripTextBox1.Text;
				}
			}

		}


		private void searchButton1_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(toolStripTextBox1.Text))
			{
				foreach (Shape item in dialogProcessor.ShapeList)
				{
					if (item.Name != null && item.Name.Equals(toolStripTextBox1.Text))
					{
						dialogProcessor.Selection.Contains(item.Location);
						dialogProcessor.Selection = item;
						viewPort.Invalidate();
					}
				}

			}
			
		}


		private void pickUpSpeedButton_Click(object sender, EventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				dialogProcessor.isTranslateActive = true;

			}else if (!pickUpSpeedButton.Checked)
			{
				dialogProcessor.isTranslateActive = false;
			}
		}

		private void speedMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.Delete();
			statusBar.Items[0].Text = "Deleting";
			viewPort.Invalidate();
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.CopySelect();
			statusBar.Items[0].Text = "Последно действие: Копиране на примитив";
			viewPort.Invalidate();
		}

		private void pasteButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.Paste();
			statusBar.Items[0].Text = "Последно действие: Поставяне";
			viewPort.Invalidate();
		}

		private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			viewPort.Invalidate();
		}

		private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRndomEllipse();
			viewPort.Invalidate();
		}

		private void squareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomSquare();
			viewPort.Invalidate();
		}

		private void dotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomDot();
			viewPort.Invalidate();
		}

		private void lineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomLine();
			viewPort.Invalidate();
		}

		private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomTriangle();
			viewPort.Invalidate();
		}

		private void polygonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.addRandomPolygon();
			viewPort.Invalidate();
		}

		private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.CopySelect();
			statusBar.Items[0].Text = "Последно действие: Копиране на примитив";
			viewPort.Invalidate();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.Delete();
			statusBar.Items[0].Text = "Deleting";
			viewPort.Invalidate();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.Paste();
			statusBar.Items[0].Text = "Последно действие: Поставяне";
			viewPort.Invalidate();
		}

		private void copySelectedItemToolStripMenuItem1_Click(object sender, EventArgs e)
		{

		}

		private void pasteSelectedItemToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void deleteSelectedItemToolStripMenuItem1_Click(object sender, EventArgs e)
		{

		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.isGroupActive = true;
			groupButton.Select();
			foreach(Shape s in dialogProcessor.ShapeList)
			{
				dialogProcessor.group.addInGroup(s);
			}

		}

		private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			dialogProcessor.Delete();
			statusBar.Items[0].Text = "Deleting";
			viewPort.Invalidate();
		}

		private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			dialogProcessor.CopySelect();
			statusBar.Items[0].Text = "Последно действие: Копиране на примитив";
			viewPort.Invalidate();
		}

		private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			dialogProcessor.Paste();
			statusBar.Items[0].Text = "Последно действие: Поставяне";
			viewPort.Invalidate();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if(dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Red);
				}
			}
			else if(dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Red);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Red;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Blue);
				}
			}
			else if(dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Blue);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Blue;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem5_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Green);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Green);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Green;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem15_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Yellow);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Yellow);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Yellow;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem16_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Orange);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Orange);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Orange;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem17_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeFillColor(Color.Black);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeFillColor(Color.Black);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.FillColor = Color.Black;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem18_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Red);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Red);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Red;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem19_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Blue);
				}
			}
			else if(dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Blue);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Blue;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem20_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Green);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Green);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Green;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem21_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Yellow);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Yellow);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Yellow;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem22_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Orange);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Orange);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Orange;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem23_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderColor(Color.Black);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderColor(Color.Black);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderColor = Color.Black;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem24_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.05));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.05));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.05);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem25_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.1));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.1));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.1);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem26_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.2));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.2));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.2);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem27_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.3));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.3));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.3);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem28_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.4));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.4));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.4);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem29_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.5));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.5));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.5);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem30_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.6));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.6));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.6);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem31_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.7));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.7));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.7);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem32_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.8));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.8));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.8);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem33_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 0.9));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 0.9));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 0.9);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem34_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity((int)(255 * 1));
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeOpacity((int)(255 * 1));
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = (int)(255 * 1);
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem35_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(0);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(0);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 0;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem36_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(1);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(1);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 1;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem37_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(3);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(3);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 3;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem38_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(6);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(6);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 6;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem39_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(9);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(9);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 9;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem40_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(12);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(12);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 12;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem41_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(15);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(15);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 15;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem42_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(18);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(18);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 18;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem43_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(21);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(21);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 21;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem44_Click(object sender, EventArgs e)
		{
			OnClick(e);
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(24);
				}
			}
			else if (dialogProcessor.isGroupActive)
			{
				dialogProcessor.group.changeBorderWidth(24);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = 24;
			}
			viewPort.Invalidate();
		}

		private void toolStripMenuItem45_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.RotateAngle(45);
			statusBar.Items[0].Text = "Rotating";
			viewPort.Invalidate();
			dialogProcessor.rotate = 45;
		}

		private void toolStripMenuItem46_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.RotateAngle(0);
			statusBar.Items[0].Text = "Rotating";
			viewPort.Invalidate();
			dialogProcessor.rotate = 0;
		}

		private void toolStripMenuItem47_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.scale = 1.1;
			viewPort.Invalidate();
		}

		private void toolStripMenuItem48_Click(object sender, EventArgs e)
		{
			OnClick(e);
			dialogProcessor.scale = 0.9;
			viewPort.Invalidate();
		}

		private void toolStripMenuItem49_Click(object sender, EventArgs e)
		{
			dialogProcessor.scale = 1;
			viewPort.Invalidate();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.Serialize(dialogProcessor.ShapeList, saveFile.FileName);
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.ShapeList = (List<Shape>)dialogProcessor.DeSerialize(openFile.FileName);
				viewPort.Invalidate();
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do you want to save changes?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if (saveFile.ShowDialog() == DialogResult.OK)
				{

					dialogProcessor.Serialize(dialogProcessor.ShapeList, saveFile.FileName);
				}
			}
			dialogProcessor.ShapeList.Clear();
			viewPort.Invalidate();
		}



		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (MessageBox.Show("Do you want to save changes?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if (saveFile.ShowDialog() == DialogResult.OK)
				{

					dialogProcessor.Serialize(dialogProcessor.ShapeList, saveFile.FileName);
				}
			}
			Close();
		}

		private void toolStripDropDownButton8_Click(object sender, EventArgs e)
		{
			if (colorWindow.ShowDialog() == DialogResult.OK)
			{
				if(dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
				{
					foreach(GroupShape g in dialogProcessor.multigroups)
					{
						g.changeFillColor(colorWindow.Color);
					}
				}
				else if (dialogProcessor.isGroupActive && !dialogProcessor.isMultigroupActive)
				{
					dialogProcessor.group.changeFillColor(colorWindow.Color);
				}
				else if (dialogProcessor.Selection != null)
				{
					dialogProcessor.Selection.FillColor = colorWindow.Color;
				}
				viewPort.Invalidate();
			}
		}

		private void toolStripDropDownButton9_Click(object sender, EventArgs e)
		{
			if (colorWindow.ShowDialog() == DialogResult.OK)
			{
				if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
				{
					foreach (GroupShape g in dialogProcessor.multigroups)
					{
						g.changeBorderColor(colorWindow.Color);
					}
				}
				else if (dialogProcessor.isGroupActive && !dialogProcessor.isMultigroupActive)
				{
					dialogProcessor.group.changeBorderColor(colorWindow.Color);
				}
				else if (dialogProcessor.Selection != null)
				{
					dialogProcessor.Selection.BorderColor = colorWindow.Color;
				}
				viewPort.Invalidate();
			}
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeOpacity(opacityTrack.Value);
				}
			}
			else if (dialogProcessor.isGroupActive && !dialogProcessor.isMultigroupActive)
			{
				dialogProcessor.group.changeOpacity(opacityTrack.Value);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.Opacity = opacityTrack.Value;
			}
			viewPort.Invalidate();
		}

		private void trackBar2_Scroll(object sender, EventArgs e)
		{
			if(dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
			{
				foreach (GroupShape g in dialogProcessor.multigroups)
				{
					g.changeBorderWidth(widthTrack.Value);
				}
			}
			else if (dialogProcessor.isGroupActive && !dialogProcessor.isMultigroupActive)
			{
				dialogProcessor.group.changeBorderWidth(widthTrack.Value);
			}
			else if (dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.BorderWidth = widthTrack.Value;
			}
			viewPort.Invalidate();
		}

		private void rotateTrack_Scroll(object sender, EventArgs e)
		{
			dialogProcessor.RotateAngle(rotateTrack.Value);
			statusBar.Items[0].Text = "Rotating";
			viewPort.Invalidate();
		}

		private void multigroupButton_Click(object sender, EventArgs e)
		{
			if (dialogProcessor.isGroupActive)
			{
				if (!dialogProcessor.multigroups.Contains(dialogProcessor.group))
				{
					int id = dialogProcessor.multigroups.Count;
					dialogProcessor.group.Id = ++id;
					dialogProcessor.multigroups.Add(dialogProcessor.group);
				}
			}
			else
			{
				//Nothing
			}
		}

		private void MultigroupButton1_Click(object sender, EventArgs e)
		{
			if (MultigroupButton1.Checked)
			{
				dialogProcessor.isMultigroupActive = true;
			}
			else if (!MultigroupButton1.Checked)
			{
				dialogProcessor.isMultigroupActive = false;
			}
		}

		private void toolStripDropDownButton10_Click(object sender, EventArgs e)
		{
			if (colorWindow.ShowDialog() == DialogResult.OK)
			{
				if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
				{
					foreach (GroupShape g in dialogProcessor.multigroups)
					{
						g.changeFillColor(colorWindow.Color);
					}
				}
				else if (dialogProcessor.isGroupActive && !dialogProcessor.isMultigroupActive)
				{
					dialogProcessor.group.changeFillColor(colorWindow.Color);
				}
				else if (dialogProcessor.Selection != null)
				{
					dialogProcessor.Selection.FillColor = colorWindow.Color;
				}
				viewPort.Invalidate();
			}
		}

		private void toolStripDropDownButton11_Click(object sender, EventArgs e)
		{
			if (colorWindow.ShowDialog() == DialogResult.OK)
			{
				if (dialogProcessor.isMultigroupActive && dialogProcessor.isGroupActive)
				{
					foreach (GroupShape g in dialogProcessor.multigroups)
					{
						g.changeBorderColor(colorWindow.Color);
					}
				}
				else if (dialogProcessor.isGroupActive && !dialogProcessor.isMultigroupActive)
				{
					dialogProcessor.group.changeBorderColor(colorWindow.Color);
				}
				else if (dialogProcessor.Selection != null)
				{
					dialogProcessor.Selection.BorderColor = colorWindow.Color;
				}
				viewPort.Invalidate();
			}
		}

		
	}
}
