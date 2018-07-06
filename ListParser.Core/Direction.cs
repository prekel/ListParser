using System;
using System.Collections.Generic;
using System.Text;

namespace ListParser.Core
{
	public class Direction
	{
		public string Name { get; private set; }

		public List<Enrollee> Enrollers { get; } = new List<Enrollee>();

		public Direction(string name)
		{
			Name = name;
		}
		
		public override string ToString() => $"{Name}";
	}
}
