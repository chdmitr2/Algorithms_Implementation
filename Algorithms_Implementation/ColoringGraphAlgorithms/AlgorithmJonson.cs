using Algorithms_Implementation.GraphStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Implementation.ColoringGraphAlgorithms
{
    class AlgorithmJonson
    {
        private Graph graph = new Graph();
        private List<int> colors = new List<int>();
     

        bool saveHistory = false;
        private List<Vertice> colorHistory = new List<Vertice>();

        public AlgorithmJonson(Graph graph)
        {
            this.graph = graph;
        }

        public AlgorithmJonson(Graph graph, bool saveHistory)
        {
            this.graph = graph;
            this.saveHistory = saveHistory;
        }

      
        public int StartColoring()
        {

            graph.ResetColors();
            InitializeColors();
          
            int CountColors = 0;

     

                SortGraphByNeighboursCountAscending();

            for (int i = 0; i < graph.Count(); i++)
            {
                List<int> neighboursColors = graph.GetNeighboursColors(graph.vertices[i]);
                var enableColors = colors.Where(c => !neighboursColors.Contains(c));
                graph.vertices[i].Color = enableColors.Min();
                if (saveHistory)
                {
                    colorHistory.Add(new Vertice(graph.vertices[i]));
                    CountColors++;
                }
                else
                    if(!saveHistory) colorHistory.Add(new Vertice(graph.vertices[i]));
                
             }

            return CountColors;
        }

        private void SortGraphByNeighboursCountAscending()
        {
            Graph sortedGraph = new Graph();
            sortedGraph.vertices = graph.SortByNeighboursCountAscending();
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
