using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresAndAlgorithm.Shortest_Path.Dijkstra
{
    public enum GraphType
    {
        DIRECTED,
        UNDIRECTED
    }

    public interface IGraph
    {
        GraphType TypeofGraph();

        void AddEdge(int v1, int v2);

        void AddEdge(int v1, int v2, int weight);

        int GetWeightedEdge(int v1, int v2);

        List<int> GetAdjacentVertices(int v);

        int GetNumVertices();

        int GetIndegree(int v);
    }

    public class AdjacencyMatrixGraph : IGraph
    {
        private int[,] adjacencyMatrix;
        private GraphType graphType = GraphType.DIRECTED;
        private readonly int numVertices = 0;

        public AdjacencyMatrixGraph(int numVertices, GraphType graphType)
        {
            this.graphType = graphType;
            this.numVertices = numVertices;
            adjacencyMatrix = new int[numVertices, numVertices];

            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    adjacencyMatrix[i, j] = 0;
                }
            }
        }

        public GraphType TypeofGraph()
        {
            return graphType;
        }

        public void AddEdge(int v1, int v2)
        {
            if (v1 >= numVertices || v2 >= numVertices || v1 < 0 || v2 < 0)
            {
                throw new Exception("Vertex number is not valid");
            }
            adjacencyMatrix[v1, v2] = 1;
            if (graphType == GraphType.UNDIRECTED)
            {
                adjacencyMatrix[v2, v1] = 1;
            }
        }

        public void AddEdge(int v1, int v2, int weight)
        {
            if (v1 >= numVertices || v2 >= numVertices || v1 < 0 || v2 < 0)
            {
                throw new Exception("Vertex number is not valid");
            }
            adjacencyMatrix[v1, v2] = weight;
            if (graphType == GraphType.UNDIRECTED)
            {
                adjacencyMatrix[v2, v1] = weight;
            }
        }

        public int GetWeightedEdge(int v1, int v2)
        {
            return adjacencyMatrix[v1, v2];
        }

        public List<int> GetAdjacentVertices(int v)
        {
            if (v < 0 || v >= numVertices)
            {
                throw new Exception("Vertex number is not valid");
            }
            List<int> integerList = new List<int>();
            for (int i = 0; i < numVertices; i++)
            {
                if (adjacencyMatrix[v, i] != 0)
                {
                    integerList.Add(i);
                }
            }
            return integerList;
        }

        public int GetIndegree(int v)
        {
            if (v < 0 || v >= numVertices)
            {
                throw new Exception("Vertex number is not valid");
            }
            int indegree = 0;
            for (int i = 0; i < GetNumVertices(); i++)
            {
                if (adjacencyMatrix[i, v] != 0)
                {
                    indegree++;
                }
            }
            return indegree;
        }

        public int GetNumVertices()
        {
            return numVertices;
        }
    }

    public class KeyComparer<TSource, TKey> : Comparer<TSource>
    {
        private readonly Func<TSource, TKey> _keySelector;
        private readonly IComparer<TKey> _innerComparer;

        protected internal KeyComparer(
            Func<TSource, TKey> keySelector,
            IComparer<TKey> innerComparer = null)
        {
            _keySelector = keySelector;
            _innerComparer = innerComparer ?? Comparer<TKey>.Default;
        }

        public override int Compare(TSource x, TSource y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            TKey xKey = _keySelector(x);
            TKey yKey = _keySelector(y);
            return _innerComparer.Compare(xKey, yKey);
        }
    }

    public static class KeyComparer
    {
        public static KeyComparer<TSource, TKey> Create<TSource, TKey>(
            Func<TSource, TKey> keySelector,
            IComparer<TKey> innerComparer = null)
        {
            return new KeyComparer<TSource, TKey>(keySelector, innerComparer);
        }
    }

    public class Dijkstra
    {
        public static Dictionary<int, DistanceInfo> BuildDistanceTable(IGraph graph, int source)
        {
            Dictionary<int, DistanceInfo> distanceTable = new Dictionary<int, DistanceInfo>();

            //SortedSet<VertexInfo> queue = new SortedSet<VertexInfo>(
            //    KeyComparer.Create((VertexInfo p) => p.GetDistance()));

            PriorityQueue<VertexInfo> queue = new PriorityQueue<VertexInfo>();

            Dictionary<int, VertexInfo> vertexInfoMap = new Dictionary<int, VertexInfo>();

            for (int j = 0; j < graph.GetNumVertices(); j++)
            {
                distanceTable.Add(j, new DistanceInfo());
            }

            distanceTable[(source)].SetDistance(0);
            distanceTable[(source)].SetLastVertex(source);

            VertexInfo sourceVertexInfo = new VertexInfo(source, 0);
            queue.Add(sourceVertexInfo);
            vertexInfoMap.Add(source, sourceVertexInfo);

            while (queue.Count > 0)
            {
                VertexInfo vertexInfo = queue.Take();

                int currentVertex = vertexInfo.GetVertexId();

                foreach (int neighbour in graph.GetAdjacentVertices(currentVertex))
                {
                    // Get the new distance, account for the weighted edge.
                    int distance = distanceTable[(currentVertex)].GetDistance()
                            + graph.GetWeightedEdge(currentVertex, neighbour);

                    // If we find a new shortest path to the neighbour update
                    // the distance and the last vertex.
                    if (distanceTable[(neighbour)].GetDistance() > distance)
                    {
                        distanceTable[(neighbour)].SetDistance(distance);
                        distanceTable[(neighbour)].SetLastVertex(currentVertex);

                        // We've found a new short path to the neighbour so remove
                        // the old node from the priority queue.

                        VertexInfo neighbourVertexInfo = vertexInfoMap.ContainsKey(neighbour) ? vertexInfoMap[(neighbour)] : null;
                        if (neighbourVertexInfo != null)
                        {
                            queue.Remove(neighbourVertexInfo);
                        }

                        // Add the neighbour back with a new updated distance.
                        neighbourVertexInfo = new VertexInfo(neighbour, distance);
                        queue.Add(neighbourVertexInfo);

                        if (!vertexInfoMap.ContainsKey(neighbour))
                            vertexInfoMap.Add(neighbour, neighbourVertexInfo);
                        else
                            vertexInfoMap[(neighbour)] = neighbourVertexInfo;
                    }
                }
            }
            return distanceTable;
        }

        public static void ShortestPath(IGraph graph, int source, int destination)
        {
            Dictionary<int, DistanceInfo> distanceTable = BuildDistanceTable(graph, source);
            Stack<int> stack = new Stack<int>();
            stack.Push(destination);

            int previousVertex = distanceTable[(destination)].GetLastVertex();
            while (previousVertex != -1 && previousVertex != source)
            {
                stack.Push(previousVertex);
                previousVertex = distanceTable[(previousVertex)].GetLastVertex();
            }

            if (previousVertex == -1)
            {
                Console.WriteLine("There is no path from node: " + source
                        + " to node: " + destination);
            }
            else
            {
                Console.Write("Smallest Path is " + source);

                while (stack.Any())
                {
                    Console.Write(" -> " + stack.Pop());
                }

                Console.WriteLine(" Dijkstra  DONE!");
            }
        }

        /**
         * A class which holds the distance information of any vertex.
         * The distance specified is the distance from the source node
         * and the last vertex is the last vertex just before the current
         * one while traversing from the source node.
         */
        public class DistanceInfo
        {
            private int distance;
            private int lastVertex;

            public DistanceInfo()
            {
                // The initial distance to all nodes is assumed
                // infinite.
                distance = int.MaxValue;
                lastVertex = -1;
            }

            public int GetDistance()
            {
                return distance;
            }

            public int GetLastVertex()
            {
                return lastVertex;
            }

            public void SetDistance(int distance)
            {
                this.distance = distance;
            }

            public void SetLastVertex(int lastVertex)
            {
                this.lastVertex = lastVertex;
            }
        }


        /**
         * A simple class which holds the vertex id and the weight of
         * the edge that leads to that vertex from its neighbour
         */
        public class VertexInfo : IComparable
        {
            private int vertexId;
            private int distance;

            public VertexInfo(int vertexId, int distance)
            {
                this.vertexId = vertexId;
                this.distance = distance;
            }

            public int GetVertexId()
            {
                return vertexId;
            }

            public int GetDistance()
            {
                return distance;
            }

            public int CompareTo(object other)
            {
                return GetDistance();
            }
        }
    }
}
