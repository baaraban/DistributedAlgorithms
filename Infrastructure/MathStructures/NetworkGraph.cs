using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Infrastructure.MathStructures
{
    public class GraphNode
    {
        public GraphNode(string name)
        {
            this.Name = name;
        }
        public string Name { get; }
        public override int GetHashCode() => Name.GetHashCode();
        public override bool Equals(object obj) => (obj is GraphNode) ? ((GraphNode)obj).Name.Equals(this.Name) : false;
        public override string ToString() => String.Format("N: {0}", Name);
    }

    public class NetworkEdge
    {
        public NetworkEdge(GraphNode from, GraphNode to, int capacity, int flow = 0)
        {
            this.From = from;
            this.To = to;
            this.Capacity = capacity;
            this.Flow = flow;
        }
        public GraphNode From { get; }
        public GraphNode To { get; }
        public int Capacity { get; }
        public int Flow { get; set; }
        public int ResidualCapacity { get { return Capacity - Flow; } }
        public override string ToString() => String.Format("{0} -> {1}/{2} -> {3}", From, Flow, Capacity, To);
    }

    public class NetworkGraph
    {
        private const string SOURCE_NAME = "S";
        private const string SINK_NAME = "T";

        public GraphNode Source { get; }
        public GraphNode Sink { get; }
        public Dictionary<GraphNode, List<NetworkEdge>> NodesRelations { get; }

        public List<NetworkEdge> GetAugmentedPathBFS()
        {
            var nodesQueue = new Queue<GraphNode>();
            var visited = new HashSet<GraphNode>();
            var parentByEdges = new Dictionary<GraphNode, NetworkEdge>();
            nodesQueue.Enqueue(Source);
            visited.Add(Source);
            GraphNode current;
            do
            {
                current = nodesQueue.Dequeue();
                if (current.Equals(Sink)) break;
                var edges = this.NodesRelations[current];
                foreach (var edge in edges)
                {
                    if (edge.ResidualCapacity != 0 && !visited.Contains(edge.To))
                    {
                        nodesQueue.Enqueue(edge.To);
                        visited.Add(edge.To);
                        parentByEdges.Add(edge.To, edge);
                    }
                }
            } while(nodesQueue.Count > 0);

            var result = new List<NetworkEdge>();
            while(current != Source)
            {
                var edgeToAdd = parentByEdges[current];
                current = edgeToAdd.From;
                result.Add(edgeToAdd);
            }

            return result;
        }

        private NetworkGraph(Dictionary<GraphNode, List<NetworkEdge>> dict, GraphNode source, GraphNode sink)
        {
            this.NodesRelations = dict;
            this.Source = source;
            this.Sink = sink;
        }

        private static KeyValuePair<GraphNode, List<NetworkEdge>> getGraphElement(string s, HashSet<GraphNode> currentlyUsed)
        {
            var structElements = s.Split(' ');
            var nodeKey = new GraphNode(structElements[0]);
            if (!currentlyUsed.Contains(nodeKey)) currentlyUsed.Add(nodeKey);
            var amountOfEdges = structElements.Length - 1;
            var edges = new List<NetworkEdge>(amountOfEdges);
            for(var i = 0; i < amountOfEdges; ++i)
            {
                var splitted = structElements[1 + i].Split('-');
                var toNode = new GraphNode(splitted[0]);
                var capacity = Convert.ToInt32(splitted[1]);
                if (!currentlyUsed.Contains(toNode)) currentlyUsed.Add(toNode);
                edges.Add(new NetworkEdge(nodeKey, toNode, capacity));
            }
            return new KeyValuePair<GraphNode, List<NetworkEdge>>(nodeKey, edges);
        }

        public static NetworkGraph ComposeFromTextFile(string filepath)
        {
            using (var f = File.OpenRead(filepath))
            {
                using(var streamReader = new StreamReader(f, Encoding.UTF8))
                {
                    var nodesAmount = Convert.ToInt32(streamReader.ReadLine());
                    var line = String.Empty;
                    var lineRead = 0;
                    var dict = new Dictionary<GraphNode, List<NetworkEdge>>(nodesAmount);
                    var setOfNodes = new HashSet<GraphNode>();
                    while((line = streamReader.ReadLine()) != null && lineRead <= nodesAmount)
                    {
                        var temp = getGraphElement(line, setOfNodes);
                        dict.Add(temp.Key, temp.Value);
                        lineRead++;
                    }
                    return new NetworkGraph(dict, 
                        source: setOfNodes.First(x => x.Name == SOURCE_NAME), 
                        sink: setOfNodes.First(x => x.Name == SINK_NAME));
                }
            }
        }
    }
}
