using Algorithms_Implementation.GraphStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Implementation.ColoringGraphAlgorithms
{
    class AlgorithmWigderson
    {
        private Graph graph = new Graph();
        private List<int> colors = new List<int>();

        bool saveHistory = false;
        private List<Vertice> colorHistory = new List<Vertice>();

        public AlgorithmWigderson(Graph graph)
        {
            this.graph = graph;
        }

        public AlgorithmWigderson(Graph graph, bool saveHistory)
        {
            this.graph = graph;
            this.saveHistory = saveHistory;
        }

        public void StartColoring()
        {
            graph.ResetColors();
            InitializeColors();
            SortGraphByNeighboursCountDescending();

            for (int i = 0; i < graph.Count(); i++)
            {
                List<int> neighboursColors = graph.GetNeighboursColors(graph.vertices[i]);
                var enableColors = colors.Where(c => !neighboursColors.Contains(c));
                graph.vertices[i].Color = enableColors.Min();
                if (saveHistory)
                    colorHistory.Add(new Vertice(graph.vertices[i]));
            }
        }


        private void SortGraphByNeighboursCountDescending()
        {
            Graph sortedGraph = new Graph();
            sortedGraph.vertices = graph.SortByNeighboursCountDescending();
            graph = sortedGraph;
        }

        private void InitializeColors()
        {
            colors.Clear();
            for (int i = 1; i <= graph.Count(); i++)
            {
                colors.Add(i);
            }
        }

        public Graph ReturnGraph()
        {
            return graph;
        }

        public List<Vertice> ReturnColorHistory()
        {
            return colorHistory;
        }
    }
}

