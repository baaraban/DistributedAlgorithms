using System;
using System.Threading.Tasks;

namespace Infrastructure.DistributedSystemSimulation
{
	public class DistributedNode
	{
		protected object data;
		protected Func<object, object> job;

		public DistributedNode(object data, Func<object, object> job)
		{
			this.data = data;
			this.job = job;
		}

		public object DoWork(object parameter)
		{
			var returnTask = new Task<object>(() => this.job(parameter));
			returnTask.Start();
			return returnTask.Result;
		}
	}
}
