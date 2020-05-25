using Algorithms_Implementation.GraphStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Algorithms_Implementation.GraphUI
{
    public class DrawGraph
    {
        PictureBox pictureBox;
        private int verticeSize = 25;

        public DrawGraph()
        {

        }

        public DrawGraph(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        public DrawGraph(PictureBox pictureBox, int verticeSize)
        {
            this.pictureBox = pictureBox;
            this.verticeSize = verticeSize;
        }

        public void DrawVertice(Vertice vertice)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.FillEllipse(SetColorBrush(vertice), vertice.X - verticeSize / 2, vertice.Y - verticeSize / 2, verticeSize, verticeSize);
            g.DrawString(vertice.Number.ToString(), new Font("Arial", 14), Brushes.White, vertice.X - verticeSize / 2, vertice.Y - verticeSize / 2);
        }

        public void DrawVerticeGold(Vertice vertice)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.FillEllipse(Brushes.Goldenrod, vertice.X - verticeSize / 2, vertice.Y - verticeSize / 2, verticeSize, verticeSize);
            g.DrawString(vertice.Number.ToString(), new Font("Arial", 14), Brushes.White, vertice.X - verticeSize / 2, vertice.Y - verticeSize / 2);
        }

        public void DrawAllVertice(Graph graph)
        {
            foreach (Vertice vertice in graph.vertices)
            {
                DrawVertice(vertice);
            }
        }

        public void DrawEdge(Vertice vertice1, Vertice vertice2)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.DrawLine(Pens.Black, vertice1.X, vertice1.Y, vertice2.X, vertice2.Y);
        }

        public void Clear()
        {
            Graphics g = pictureBox.CreateGraphics();
            g.Clear(Color.White);
        }

        private Brush SetColorBrush(Vertice vertice)
        {
            switch (vertice.Color)
            {
                case 0:
                    return Brushes.Black;
                case 1:
                    return Brushes.Red;
                case 2:
                    return Brushes.Blue;
                case 3:
                    return Brushes.Green;
                case 4:
                    return Brushes.Orange;
                case 5:
                    return Brushes.Gray;
                case 6:
                    return Brushes.Brown;
                case 7:
                    return Brushes.Aqua;
                case 8:
                    return Brushes.Olive;
                case 9:
                    return Brushes.BlueViolet;
                case 10:
                    return Brushes.DarkGoldenrod;
                case 11:
                    return Brushes.DeepPink;
                case 12:
                    return Brushes.LimeGreen;
                case 13:
                    return Brushes.Pink;
                case 14:
                    return Brushes.OrangeRed;
                case 15:
                    return Brushes.LemonChiffon;
                case 16:
                    return Brushes.AliceBlue;
                case 17:
                    return Brushes.BurlyWood;
                case 18:
                    return Brushes.Moccasin;
                case 19:
                    return Brushes.MistyRose;
                case 20:
                    return Brushes.OliveDrab;
                default:
                    return Brushes.Black;
            }
        }
    }
}
