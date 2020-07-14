using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Draw.src.Model
{
    [Serializable]
    public class GroupShape : Shape
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string nameGroup;
        public virtual string NameGroup
        {
            get { return nameGroup; }
            set { nameGroup = value; }
        }


        List<Shape> listShapes;
        public List<Shape> GetListShapes()
        {
            return listShapes;
        }

        public bool isEmpty()
        {
            return listShapes.Any();
        }
        public GroupShape()
        {
            listShapes = new List<Shape>();
        }


        public void addInGroup(Shape shape)
        {

            if (!listShapes.Contains(shape))
            {
                listShapes.Add(shape);
            }

        }

        public void removeFromGroup(Shape shape)
        {
            listShapes.Remove(shape);
        }


        public void changeFillColor(Color color)
        {
            foreach(Shape s in listShapes)
            {
                s.FillColor = color;
            }
        }

        public void changeBorderColor(Color color)
        {
            foreach(Shape s in listShapes)
            {
                s.BorderColor = color;
            }
        }

        public void changeBorderWidth(int BorderWidth)
        {
            foreach(Shape s in listShapes)
            {
                s.BorderWidth = BorderWidth;
            }
        }

        public void changeOpacity(int Opacity)
        {
            foreach(Shape s in listShapes)
            {
                s.Opacity = Opacity;
            }
        }

        public void clear()
        {
            listShapes.Clear();
        }


        /*        public override void DrawSelf(Graphics grfx)
                {
                    base.DrawSelf(grfx);

                    foreach (var s in listShapes)
                    {
                        s.DrawSelf(grfx);
                    }
                }*/


    }
}
