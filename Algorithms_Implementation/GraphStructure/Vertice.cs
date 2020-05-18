using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Implementation.GraphStructure
{
    public  class Vertice
    {
        public float X { get; set; }
        public float Y { get; set; }

        public int Number { get; set; }
        public int Color { get; set; }
        public List<int> Neighbours { get; set; }

        public Vertice()
        {
            X = 0;
            Y = 0;
            Number = 0;
            Color = 0;
            Neighbours = new List<int>();
        }

        public Vertice(Vertice vertice)
        {
            this.X = vertice.X;
            this.Y = vertice.Y;
            this.Number = vertice.Number;
            this.Color = vertice.Color;
            this.Neighbours = vertice.Neighbours;
        }

        public Vertice(float X, float Y, int Number)
        {
            this.X = X;
            this.Y = Y;
            this.Number = Number;
            Color = 0;
            Neighbours = new List<int>();
        }

        public Vertice(float X, float Y, int Number, int Color)
        {
            this.X = X;
            this.Y = Y;
            this.Number = Number;
            this.Color = Color;
            Neighbours = new List<int>();
        }
    }
}
