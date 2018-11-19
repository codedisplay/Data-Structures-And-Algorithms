using DataStructuresAndAlgorithm.Shortest_Path.Dijkstra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresAndAlgorithmTests
{
    [TestClass]
    public class ShortestPathTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            IGraph graph1 = new AdjacencyMatrixGraph(4, GraphType.DIRECTED);
            graph1.AddEdge(0, 1, 4);
            graph1.AddEdge(1, 2, 4);
            graph1.AddEdge(0, 3, 1);
            graph1.AddEdge(3, 2, 1);

            Dijkstra.ShortestPath(graph1, 0, 2);

            graph1 = new AdjacencyMatrixGraph(8, GraphType.DIRECTED);
            graph1.AddEdge(2, 7, 4);
            graph1.AddEdge(0, 3, 2);
            graph1.AddEdge(0, 4, 2);
            graph1.AddEdge(0, 1, 1);
            graph1.AddEdge(2, 1, 3);
            graph1.AddEdge(1, 3, 2);
            graph1.AddEdge(3, 5, 1);
            graph1.AddEdge(3, 6, 3);
            graph1.AddEdge(4, 7, 2);
            graph1.AddEdge(7, 5, 4);

            Dijkstra.ShortestPath(graph1, 0, 5);

            //Console.WriteLine("------------------------------");

            graph1 = new AdjacencyMatrixGraph(4, GraphType.DIRECTED);
            graph1.AddEdge(0, 3, 20);
            graph1.AddEdge(0, 2, 3);
            graph1.AddEdge(0, 1, 24);
            graph1.AddEdge(2, 3, 12);

            Dijkstra.ShortestPath(graph1, 0, 1);

            Dijkstra.ShortestPath(graph1, 0, 2);

            Dijkstra.ShortestPath(graph1, 0, 3);
        }
    }
}
