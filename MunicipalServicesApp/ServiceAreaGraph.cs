
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public class ServiceAreaGraph
    {
        private class Edge
        {
            public string Destination { get; set; }
            public int Weight { get; set; }

            public Edge(string destination, int weight)
            {
                Destination = destination;
                Weight = weight;
            }
        }

        private class GraphEdge
        {
            public string Source { get; set; }
            public string Destination { get; set; }
            public int Weight { get; set; }
        }

        private Dictionary<string, List<Edge>> adjacencyList;
        private List<GraphEdge> edges;

        public ServiceAreaGraph()
        {
            adjacencyList = new Dictionary<string, List<Edge>>();
            edges = new List<GraphEdge>();
        }

        public void AddVertex(string area)
        {
            if (!adjacencyList.ContainsKey(area))
            {
                adjacencyList.Add(area, new List<Edge>());
            }
        }

        public void AddEdge(string source, string destination, int weight)
        {
            if (weight < 0)
            {
                throw new ArgumentException("Distance cannot be negative.");
            }

            AddVertex(source);
            AddVertex(destination);

            adjacencyList[source].Add(new Edge(destination, weight));
            adjacencyList[destination].Add(new Edge(source, weight));

            edges.Add(new GraphEdge
            {
                Source = source,
                Destination = destination,
                Weight = weight
            });
        }

        public List<string> BreadthFirstSearch(string start)
        {
            List<string> result = new List<string>();

            if (!adjacencyList.ContainsKey(start))
            {
                return result;
            }

            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                result.Add(current);

                foreach (Edge edge in adjacencyList[current])
                {
                    if (!visited.Contains(edge.Destination))
                    {
                        visited.Add(edge.Destination);
                        queue.Enqueue(edge.Destination);
                    }
                }
            }

            return result;
        }

        public List<string> DepthFirstSearch(string start)
        {
            List<string> result = new List<string>();

            if (!adjacencyList.ContainsKey(start))
            {
                return result;
            }

            HashSet<string> visited = new HashSet<string>();

            DepthFirstSearchRecursive(start, visited, result);

            return result;
        }

        private void DepthFirstSearchRecursive(
            string current,
            HashSet<string> visited,
            List<string> result)
        {
            visited.Add(current);
            result.Add(current);

            foreach (Edge edge in adjacencyList[current])
            {
                if (!visited.Contains(edge.Destination))
                {
                    DepthFirstSearchRecursive(
                        edge.Destination,
                        visited,
                        result);
                }
            }
        }

        public List<string> MinimumSpanningTree()
        {
            List<string> result = new List<string>();

            Dictionary<string, string> parent =
                new Dictionary<string, string>();

            foreach (string vertex in adjacencyList.Keys)
            {
                parent[vertex] = vertex;
            }

            List<GraphEdge> sortedEdges = edges
                .OrderBy(edge => edge.Weight)
                .ToList();

            foreach (GraphEdge edge in sortedEdges)
            {
                string sourceRoot = FindRoot(parent, edge.Source);
                string destinationRoot = FindRoot(parent, edge.Destination);

                if (sourceRoot != destinationRoot)
                {
                    parent[sourceRoot] = destinationRoot;

                    result.Add(
                        edge.Source + " - " +
                        edge.Destination + " (" +
                        edge.Weight + " km)");
                }
            }

            return result;
        }

        private string FindRoot(
            Dictionary<string, string> parent,
            string vertex)
        {
            if (parent[vertex] != vertex)
            {
                parent[vertex] = FindRoot(parent, parent[vertex]);
            }

            return parent[vertex];
        }
    }
}
