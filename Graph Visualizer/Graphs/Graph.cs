using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Graph<T> : IGraph<T>
    {
        public IList<Edge> Edges { get; }
        public IList<IList<int>> Neighbors { get; }
        public IList<IList<int>> AdjacencyMatrix { get; }
        public int VertexCount { get => Vertices.Count; }
        public IList<T> Vertices { get; }

        public Graph(IList<T> vertices, IList<Edge> edges)
        {
            Vertices = vertices;

            Neighbors = new List<IList<int>>();
            Edges = edges;
            AdjacencyMatrix = new List<IList<int>>();


            for (int i = 0; i < vertices.Count; i++)
            {
                AdjacencyMatrix.Add(new List<int>());
                for (int j = 0; j < vertices.Count; j++)
                    AdjacencyMatrix[i].Add(0);
                Neighbors.Add(new List<int>());
            }

            foreach (var edge in edges)
            {
                int from = edge.From;
                int to = edge.To;
                AdjacencyMatrix[from][to] = 1;
                Neighbors[from].Add(to);
            }

        }

        public T GetVertex(int index)
        {
            if (index >= VertexCount) throw new IndexOutOfRangeException("Should be between zero and vertex count.");

            return Vertices[index];
        }

        public int GetDegree(int vertexIndex)
        {
            if (vertexIndex < 0 || vertexIndex >= VertexCount) throw new InvalidOperationException();
            return Neighbors[vertexIndex].Count;
        }

        public int GetDegree(T vertex)
        {
            if (vertex == null) throw new InvalidOperationException("Vertex should not be null.");
            return Neighbors[GetIndex(vertex)].Count;
        }

        public int GetIndex(T vertex)
        {
            if (vertex == null) throw new InvalidOperationException("Vertex should not be null.");
            return Vertices.IndexOf(vertex);
        }

        public IList<T> GetNeighbors(int vertexIndex)
        {
            if (vertexIndex < 0 || vertexIndex >= VertexCount) throw new InvalidOperationException();
            var neighbors = new List<T>();
            var tmp = Neighbors[vertexIndex];
            foreach (int i in tmp) neighbors.Add(GetVertex(i));

            return neighbors;
        }

        public IList<int> GetNeighborsByIndex(int vertexIndex)
        {
            if (vertexIndex < 0 || vertexIndex >= VertexCount) throw new InvalidOperationException();
            return Neighbors[vertexIndex];
        }
        public void PrintAdjacencyList()
        {
            for (int i =0; i < Vertices.Count; i++)
            {
                var neighborCount = Neighbors[i].Count;
                var vertex = GetVertex(i);
                Console.Write($"{vertex} | ");
                for(int j = 0; j < neighborCount; j++)
                {
                    vertex = GetVertex(Neighbors[i][j]);
                    Console.Write($"{vertex}, ");
                }

                Console.WriteLine();
            }
        }
       
        public Path GetShortestPath(int startingVertexIndex)
        {
            // stores the previous vertex of v in the path (index form)
            var prev = new List<int>();
            // stores the costs of vertex v from the starting vertex. AKA currDist
            var costs = new List<float>();
            // stores the sequence of v that was processed.
            var searchOrder = new List<int>();

            var toBeChecked = new List<int>();

            for (int i = 0; i < VertexCount; i++)
            {
                costs.Add(float.MaxValue);
                toBeChecked.Add(i);
                // initially, all vertices do not have predecessors, hence the -1 value
                prev.Add(-1);
            }

            costs[startingVertexIndex] = 0;

            while (toBeChecked.Count > 0)
            {
                // v = a vertex in toBeChecked with minimal currDist(v);
                int vertexWithMinimalCost = -1;
                float minimalCost = float.MaxValue;
                foreach (int i in toBeChecked)
                {
                    float value = costs[i];
                    if (value <= minimalCost)
                    {
                        minimalCost = value;
                        vertexWithMinimalCost = i;
                    }
                }

                // remove v from toBeChecked
                toBeChecked.Remove(vertexWithMinimalCost);
                bool searchOrderAdded = false;

		        //for all vertices u adjacent to v and in toBeChecked
                var neighbors = Neighbors[vertexWithMinimalCost];
                foreach (int neighbor in neighbors)
                {
                    if (toBeChecked.Contains(neighbor))
                    {
                        // search for edge (vu)
                        Edge edgeVu = null;
                        foreach (var edge in Edges)
                        {
                            if (edge.From == vertexWithMinimalCost && edge.To == neighbor)
                            {
                                edgeVu = edge;
                                break;
                            }
                        }

                        // currDist(u)
                        float costU = costs[neighbor];
                        // currDist(v)
                        float costV = costs[vertexWithMinimalCost];
                        // weight (edge(vu))
                        float weight = edgeVu.Weight;

                        // if currDist(u) > currDist(v) + weight (edge(vu))
                        if (costU > costV + weight)
                        {
                            costs[neighbor] = costV + weight;

                            // predecessor(u) = v;
                            prev[neighbor] = vertexWithMinimalCost;

                            if (!searchOrderAdded)
                            {
                                searchOrder.Add(vertexWithMinimalCost);
                                searchOrderAdded = true; 
                            }
                        }
                        
                    }
                }

            }
            return new Path(costs, prev, searchOrder);
        }

        public void AddVertex(T vertex, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            
            foreach (var vertexInGraph in Vertices)
            {
                if (comparer.Compare(vertex, vertexInGraph) == 0)
                {
                    throw new InvalidOperationException("Cannot have two vertices with same name");
                }
            }
            Vertices.Add(vertex);
            
            for (int i = 0; i < VertexCount; i++)
            {
                if (i < VertexCount - 1)
                    AdjacencyMatrix[i].Add(0);
                else
                {
                    Neighbors.Add(new List<int>());
                    AdjacencyMatrix.Add(new List<int>());
                    for (int j = 0; j < VertexCount; j++)
                        AdjacencyMatrix[i].Add(0);
                }
            }
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
            Neighbors[edge.From].Add(edge.To);

            AdjacencyMatrix[edge.From][edge.To] = 1;

        }
    }
}
