using Algorithms_Implementation.GraphStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Implementation.ColoringGraphAlgorithms
{
    class AlgorithmBlum
    {
        private Graph graph = new Graph();
        private List<int> colors = new List<int>();

        bool saveHistory = false;
        private List<Vertice> colorHistory = new List<Vertice>();

        public AlgorithmBlum(Graph graph)
        {
            this.graph = graph;
        }

        public AlgorithmBlum(Graph graph, bool saveHistory)
        {
            this.graph = graph;
            this.saveHistory = saveHistory;
        }

        public int CheckChromaticNumber(Graph graph)
        {

            for (int i = 0; i < graph.Count(); i++)
            {
                List<int> neighboursColors = graph.GetNeighboursColors(graph.vertices[i]);
                var enableColors = colors.Where(c => !neighboursColors.Contains(c));
                graph.vertices[i].Color = enableColors.Min();
            }

            int ChromaticNumber = graph.ChromaticNumber();
            graph.ResetColors();
            return ChromaticNumber;

        }
        public void StartColoring()
        {

            graph.ResetColors();
            InitializeColors();
            Graph coloredGraph = new Graph();
            int ChrNum = CheckChromaticNumber(graph);



            for (int i = 0; i < graph.Count(); i++)
            {

                SortGraphByNeighboursCountDescending();
                SortGraphByMostColoredNeighboursCountDescending();


                bool coloredStatus = false;
                int j = 0;

                while (!coloredStatus)
                {

                    if (coloredGraph.GetVertice(graph.vertices[j].Number) == null)
                    {
                        if (graph.vertices[j].Neighbours.Count < Math.Pow(graph.Count(), 1 / (ChrNum - 1)))
                        {

                            List<int> neighboursColors = graph.GetNeighboursColors(graph.vertices[j]);

                            var enableColors = colors.Where(c => !neighboursColors.Contains(c));
                            graph.vertices[j].Color = enableColors.Min();
                            coloredGraph.vertices.Add(graph.vertices[j]);
                            coloredStatus = true;
                            if (saveHistory)
                                colorHistory.Add(new Vertice(graph.vertices[j]));
                        }

                        else

                        {

                            List<int> neighboursColors = graph.GetNeighboursColors(graph.vertices[j]);
                            var enableColors = colors.Where(c => !neighboursColors.Contains(c));
                            graph.vertices[j].Color = enableColors.Min();
                            coloredGraph.vertices.Add(graph.vertices[j]);
                            coloredStatus = true;
                            if (saveHistory)
                                colorHistory.Add(new Vertice(graph.vertices[j]));

                        }


                    }

                    j++;
                }

            }
            graph = coloredGraph;
        }

        private void SortGraphByNeighboursCountDescending()
        {
            Graph sortedGraph = new Graph();
            sortedGraph.vertices = graph.SortByNeighboursCountDescending();
            graph = sortedGraph;
        }

        private void SortGraphByNeighboursCountAscending()
        {
            Graph sortedGraph = new Graph();
            sortedGraph.vertices = graph.SortByNeighboursCountAscending();
            graph = sortedGraph;
        }

        private void SortGraphByMostColoredNeighboursCountDescending()
        {
            Graph sortedGraph = new Graph();
            sortedGraph.vertices = graph.SortByMostColoredNeighboursCountDescending();
            graph = sortedGraph;
        }

        private void SortGraphByNotColoredNeighboursCountDescending()
        {
            Graph sortedGraph = new Graph();
            sortedGraph.vertices = graph.SortByNotColoredNeighboursCountDescending();
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
