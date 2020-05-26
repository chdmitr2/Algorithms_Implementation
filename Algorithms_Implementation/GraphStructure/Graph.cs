using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Implementation.GraphStructure
{
        public class Graph
        {
        public List<Vertice> vertices = new List<Vertice>();

        public Graph()
        {

        }

        public Graph(Graph graph)
        {
            vertices = graph.vertices.Select(x => new Vertice(x)).ToList();
        }

        public Graph(List<Vertice> vertices)
        {
            this.vertices = vertices;
        }

        public void AddVertice(Vertice vertice)
        {
            vertices.Add(vertice);
        }
        public void RemoveVertice(Vertice vertice)
        {
            vertices.Remove(vertice);
        }

        public void AddEdge(Vertice vertice1, Vertice vertice2)
        {
            vertice1.Neighbours.Add(vertice2.Number);
            vertice2.Neighbours.Add(vertice1.Number);
        }

        public void DeleteAllEdges()
        {
            for (int i = 0; i < Count(); i++)
            {
                vertices[i].Neighbours.Clear();
            }
        }

        public Vertice GetVertice(int number)
        {
            for (int i = 0; i < Count(); i++)
            {
                if (vertices[i].Number == number)
                    return vertices[i];
            }
            return null;
        }

        public int GetVerticeIndex(int number)
        {
            for (int i = 0; i < Count(); i++)
            {
                if (vertices[i].Number == number)
                    return i;
            }
            return -1;
        }

        public void ResetColors()
        {
            for (int i = 0; i < Count(); i++)
            {
                vertices[i].Color = 0;
            }
        }

        public List<Vertice> SortByNeighboursCountDescending()
        {
            return vertices.OrderByDescending(x => x.Neighbours.Count).ToList();
        }

        public List<Vertice> SortByNeighboursCountAscending()
        {
            return vertices.OrderBy(x => x.Neighbours.Count).ToList();
        }

        public List<Vertice> SortByNotColoredNeighboursCountDescending()
        {
            List<Tuple<Vertice, int>> notColoredNeighboursCount = new List<Tuple<Vertice, int>>();
            for (int i = 0; i < Count(); i++)
            {
                int notColoredNeighboursCounter = 0;
                for (int j = 0; j < vertices[i].Neighbours.Count; j++)
                {
                    if (GetVertice(vertices[i].Neighbours[j]).Color != 0)
                        notColoredNeighboursCounter++;
                }
                notColoredNeighboursCount.Add(new Tuple<Vertice, int>(vertices[i], notColoredNeighboursCounter));
            }
            notColoredNeighboursCount = notColoredNeighboursCount.OrderByDescending(x => x.Item2).ToList();

            List<Vertice> result = new List<Vertice>();
            for (int i = 0; i < Count(); i++)
            {
                result.Add(notColoredNeighboursCount[i].Item1);
            }

            return result;
        }

        public List<Vertice> SortByMostColoredNeighboursCountDescending()
        {
            List<Tuple<Vertice, int>> notColoredNeighboursCount = new List<Tuple<Vertice, int>>();
            for (int i = 0; i < Count(); i++)
            {
                List<int> uniqueNeighboursColorsCounter = new List<int>();
                for (int j = 0; j < vertices[i].Neighbours.Count; j++)
                {
                    if (!uniqueNeighboursColorsCounter.Contains(GetVertice(vertices[i].Neighbours[j]).Color))
                        uniqueNeighboursColorsCounter.Add(GetVertice(vertices[i].Neighbours[j]).Color);
                }
                notColoredNeighboursCount.Add(new Tuple<Vertice, int>(vertices[i], uniqueNeighboursColorsCounter.Count));
            }
            notColoredNeighboursCount = notColoredNeighboursCount.OrderByDescending(x => x.Item2).ToList();

            List<Vertice> result = new List<Vertice>();
            for (int i = 0; i < Count(); i++)
            {
                result.Add(notColoredNeighboursCount[i].Item1);
            }

            return result;
        }

        public List<int> GetNeighboursColors(Vertice vertice)
        {
            List<int> neighboursColors = new List<int>();
            foreach (int neighbours in vertice.Neighbours)
            {
                Vertice neighbourVertice = vertices.Find(x => x.Number == neighbours);
                if (neighbourVertice != null)
                    if (!neighboursColors.Contains(neighbourVertice.Color))
                        neighboursColors.Add(neighbourVertice.Color);
            }
            return neighboursColors;
        }

       
        public int ChromaticNumber()
        {
            List<int> colors = new List<int>();
            for (int i = 0; i < Count(); i++)
            {
                if (!colors.Contains(vertices[i].Color))
                    colors.Add(vertices[i].Color);
            }
            return colors.Count;
        }

        public int Count()
        {
            return vertices.Count;
        }

        public Graph Return()
        {
            return this;
        }
    }
}
