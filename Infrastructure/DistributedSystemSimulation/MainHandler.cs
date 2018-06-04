using System;
using System.Collections.Generic;

namespace Infrastructure.DistributedSystemSimulation 
{
	public class MainHandler<NodeType> where NodeType: DistributedNode
	{
		private int amountOfNodes;
		private List<NodeType> nodes = new List<NodeType>();

		public int AmountOfNodes { get { return amountOfNodes; } }

		public MainHandler(int amountOfNodes, List<object> dataset, Func<object, object> jobForAll)
		{
			if(amountOfNodes != dataset.Count)
			{
				throw new ArgumentException("Input parameters mismatch");
			}
			foreach(var d in dataset)
			{
				nodes.Add((NodeType)Activator.CreateInstance(
					typeof(NodeType), 
					new object[] { d, jobForAll }
					));
			}
		}

		public MainHandler(int amountOfNodes, List<Tuple<object, Func<object, object>>> datasetWithJobs)
		{
			if (amountOfNodes != datasetWithJobs.Count)
			{
				throw new ArgumentException("Input parameters mismatch");
			}
			foreach (var dj in datasetWithJobs)
			{
				nodes.Add((NodeType)Activator.CreateInstance(
					typeof(NodeType),
					new object[] { dj.Item1, dj.Item2}
					));
			}
		}
	}
}
