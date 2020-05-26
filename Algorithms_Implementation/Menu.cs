using System.Collections.Generic;
using System.Windows.Forms;
using System;
using Algorithms_Implementation.GraphUI;
using Algorithms_Implementation.GraphStructure;
using Algorithms_Implementation.ColoringGraphAlgorithms;

namespace Algorithms_Implementation
{
    public partial class Menu : Form
    {

        private DrawGraph gui;
        private Graph graph = new Graph();
        int ColorCount = 0;
       

        private bool enableVerticePaint = true;
        private bool enableEdgePaint = false;
        private int verticeNumber = 0;
        private Vertice firstVertice = null;

        private string sortAlgorithm = "";
        private int stepNumber = 0;
        private int stepCount = 0;
        private List<Vertice> colorHistory = new List<Vertice>();
        public Menu()
        {

            InitializeComponent();
            gui = new DrawGraph(PictureBox);

            StepByStepAdvencedInfoLabel.Text = "";
            StepLabel.Text = "";
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {

            if (enableVerticePaint)
            {
                Vertice vertice = new Vertice(e.X, e.Y, verticeNumber);
                graph.AddVertice(vertice);
                verticeNumber++;
                AddVerticeOrEdgeReset();
                gui.DrawVertice(vertice);
            }

            if (enableEdgePaint)
            {
                if (graph.Count() >= 1 && firstVertice == null)
                {
                    firstVertice = GetClosestVertice(e);
                    gui.DrawVerticeGold(firstVertice);
                }
                else if (graph.Count() >= 1)
                {
                    Vertice secondVertice = GetClosestVertice(e);
                    if (firstVertice != secondVertice)
                    {
                        graph.AddEdge(firstVertice, secondVertice);
                        gui.DrawEdge(firstVertice, secondVertice);
                        ResetClickedVertice();
                        AddVerticeOrEdgeReset();
                    }
                }
            }
        }
        private void ResetClickedVertice()
        {
            if (firstVertice != null)
            {
                gui.DrawVertice(firstVertice);
                firstVertice = null;
            }
        }

        private void AddVerticeOrEdgeReset()
        {
            if (sortAlgorithm != "")
            {
                sortAlgorithm = "";
                ResetStepByStepOptions();
                SetColorCount(0);
                SetColorCount2(ColorCount);

                graph.ResetColors();
                gui.DrawAllVertice(graph);
            }
        }

        public Vertice GetClosestVertice(MouseEventArgs mouse)
        {
            double bestDistance = double.MaxValue;
            int bestIndex = -1;

            for (int i = 0; i < graph.Count(); i++)
            {
                if (bestDistance > CalculateDistance(mouse, graph.vertices[i]))
                {
                    bestDistance = CalculateDistance(mouse, graph.vertices[i]);
                    bestIndex = i;
                }
            }

            return graph.vertices[bestIndex];
        }

        public double CalculateDistance(MouseEventArgs mouse, Vertice vertice)
        {
            return Math.Sqrt(Math.Pow(vertice.X - mouse.X, 2) + Math.Pow(vertice.Y - mouse.Y, 2));
        }

        private void AddVerticeButton_Click(object sender, EventArgs e)
        {
            enableVerticePaint = !enableVerticePaint;

            if (enableVerticePaint)
            {
                AddVerticeButton.Text = "Print Vertice Enabled";

                enableEdgePaint = false;
                AddEdgeButton.Text = "Add Edge Disabled";
            }
            else
            {
                AddVerticeButton.Text = "Print Vertice Disabled";
            }
        }

        private void AddEdgeButton_Click(object sender, EventArgs e)
        {
            enableEdgePaint = !enableEdgePaint;

            if (enableEdgePaint)
            {
                AddEdgeButton.Text = "Add Edge Enabled";

                enableVerticePaint = false;
                AddVerticeButton.Text = "Add Vertice Disabled";
            }
            else
            {
                AddEdgeButton.Text = "Add Edge Disabled";
            }
        }

        private void StepByStepAlgotythmLogic()
        {
            ResetClickedVertice();

            if (stepNumber == stepCount)
            {
                graph.ResetColors();
                gui.DrawAllVertice(graph);
            }
            else if (stepNumber == stepCount + 1)
            {
                stepNumber = 0;
                gui.DrawVertice(colorHistory[stepNumber]);
            }
            else
                gui.DrawVertice(colorHistory[stepNumber]);

            if (stepCount > 0)
                stepNumber++;

            if (stepNumber < stepCount + 1)
                StepInfoUpdate();
            else
            {
                stepNumber = 0;
                StepInfoUpdate();
            }
        }
        
        private void StepInfoUpdate()
        {
            if (StepByStepCheckBox.Checked)
            {
                StepLabel.Text = "Step: " + stepNumber + " / " + stepCount;

                if (stepNumber > stepCount)
                {
                    StepLabel.Text = "Step: Result";
                }
            }
        }

        private void StepByStepCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (StepByStepCheckBox.Checked)
            {
                StepByStepAdvencedInfoLabel.Text = "Step By Step Advenced Info:";
                StepLabel.Text = "Step: " + stepNumber + " / " + stepCount;
            }
            else
            {
                StepByStepAdvencedInfoLabel.Text = "";
                StepLabel.Text = "";
            }
        }


        private void AlgorithmJonson_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (StepByStepCheckBox.Checked)
            {
                if (sortAlgorithm != "Algorithm Jonson: Step By Step" || stepNumber == 0)
                {
                    AlgorithmJonson graphColoring = new AlgorithmJonson(graph, true);
                  count =   graphColoring.StartColoring();
                    SetColorCount(graphColoring.ReturnGraph().ChromaticNumber());
                    SetColorCount2(count);



                    colorHistory = graphColoring.ReturnColorHistory();
                    stepNumber = 0;
                    stepCount = colorHistory.Count;

                    graph.ResetColors();
                    gui.DrawAllVertice(graph);
                }

                sortAlgorithm = "Algorithm Jonson: Step By Step";
                StepByStepAlgotythmLogic();
            }
            else
            {
                AlgorithmJonson graphColoring = new AlgorithmJonson(graph);
                count = graphColoring.StartColoring();
                gui.DrawAllVertice(graphColoring.ReturnGraph());
                SetColorCount(graphColoring.ReturnGraph().ChromaticNumber());
                SetColorCount2(count);


                if (sortAlgorithm != "Algorithm Jonson")
                {
                    ResetStepByStepOptions();
                }
                sortAlgorithm = "Algorithm Jonson";
            }
        }

        private void AlgorithmWigderson_Click(object sender, EventArgs e)
        {
            if (StepByStepCheckBox.Checked)
            {
                if (sortAlgorithm != "Algorithm Wigderson: Step By Step" || stepNumber == 0)
                {
                    AlgorithmWigderson graphColoring = new AlgorithmWigderson(graph, true);
                    graphColoring.StartColoring();
                    SetColorCount(graphColoring.ReturnGraph().ChromaticNumber());
                   

                    colorHistory = graphColoring.ReturnColorHistory();
                    stepNumber = 0;
                    stepCount = colorHistory.Count;

                    graph.ResetColors();
                    gui.DrawAllVertice(graph);
                }

                sortAlgorithm = "Algorithm Wigderson: Step By Step";
                StepByStepAlgotythmLogic();
            }
            else
            {
                AlgorithmWigderson graphColoring = new AlgorithmWigderson(graph);
                graphColoring.StartColoring();
                gui.DrawAllVertice(graphColoring.ReturnGraph());
                SetColorCount(graphColoring.ReturnGraph().ChromaticNumber());
                
                if (sortAlgorithm != "Algorithm Wigderson")
                {
                    ResetStepByStepOptions();
                }
                sortAlgorithm = "Algorithm Wigderson";
            }
        }

        private void AlgorithmBlum_Click(object sender, EventArgs e)
        {
            if (StepByStepCheckBox.Checked)
            {
                if (sortAlgorithm != "Algorithm Blum: Step By Step" || stepNumber == 0)
                {
                    AlgorithmWigderson graphColoring = new AlgorithmWigderson(graph, true);
                    graphColoring.StartColoring();
                    SetColorCount(graphColoring.ReturnGraph().ChromaticNumber());
                   

                    colorHistory = graphColoring.ReturnColorHistory();
                    stepNumber = 0;
                    stepCount = colorHistory.Count;

                    graph.ResetColors();
                    gui.DrawAllVertice(graph);
                }

                sortAlgorithm = "Algorithm Blum: Step By Step";
                StepByStepAlgotythmLogic();
            }
            else
            {
                AlgorithmBlum graphColoring = new AlgorithmBlum(graph);
                graphColoring.StartColoring();
                gui.DrawAllVertice(graphColoring.ReturnGraph());
                SetColorCount(graphColoring.ReturnGraph().ChromaticNumber());
               
                if (sortAlgorithm != "Algorithm Blum")
                {
                    ResetStepByStepOptions();
                }
                sortAlgorithm = "Algorithm Blum";
            }
        }
        private void SetColorCount(int number)
        {
            ColorCountLabel.Text = number.ToString();
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            gui.Clear();
            graph = new Graph();
            verticeNumber = 0;
            firstVertice = null;
            SetColorCount(0);
            SetColorCount2(0);
            ResetStepByStepOptions();
        }
        private void ClearEdgesButton_Click(object sender, EventArgs e)
        {
            gui.Clear();
            graph.DeleteAllEdges();
            graph.ResetColors();
            ResetClickedVertice();
            gui.DrawAllVertice(graph);
            SetColorCount(0);
            ResetStepByStepOptions();
        }

       
        private void ResetStepByStepOptions()
        {
            sortAlgorithm = "";
            stepNumber = 0;
            stepCount = 0;
            colorHistory.Clear();
            StepInfoUpdate();
        }

        private void SetColorCount2(int number)
        {
            CountColorLabel2.Text = number.ToString();
        }
    }
}
