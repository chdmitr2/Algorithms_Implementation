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
            int ChromaticNumber;
            graph.ResetColors();
            InitializeColors();
            int ChromaticNumber1 = AlgorithmC(graph);
            int ChromaticNumber2 = AlgorithmD(graph);
            if (ChromaticNumber1 < ChromaticNumber2)
            {
                ChromaticNumber = ChromaticNumber1;
                WigdersonColoring(graph,ChromaticNumber,1);
            }
            else
            {
                ChromaticNumber = ChromaticNumber2;
                JonsonColoring(graph, ChromaticNumber,1);
            }
        }

        public int AlgorithmC(Graph graph)
        {
            int ChromaticNumber = 2;
            while (ChromaticNumber <= graph.Count())
            {
                int AlgB = AlgorithmB(ChromaticNumber, graph, 1);
                if (AlgB == -1)
                    ChromaticNumber++;

                else
                     return ChromaticNumber;

            }

            return graph.Count();
        }

        public int AlgorithmB(int ChromaticNumber, Graph graph, int number)
        {
            int number1;
            int constant = 1 - (1 / (ChromaticNumber - 1));
            if (ChromaticNumber = 2 && TwoSetColoring(graph))
                return 2;
            if (ChromaticNumber >= Math.Log(graph.Count()))
                return graph.Count();
            SortGraphByNeighboursCountDescending();
            for (int i = 0; i < graph.Count(); i++)
            {
                if (graph.vertices[i].Neighbours.Count >= Math.Pow(graph.Count(), constant))
                {
                    List<Vertice> Neighbours = graph.vertices[i].Neighbours;
                    Graph BackTrackingGraph = new Graph(Neighbours);
                    number1 = AlgorithmB(ChromaticNumber - 1, BackTrackingGraph, 1);
                    number += number1;
                    graph.vertices[i].Neighbours.Clear();
                    graph.vertices[i].Clear();





                }
                




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

