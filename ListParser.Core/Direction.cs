using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ListParser.Core
{
	public class Direction : IComparable
	{
		public string Name { get; private set; }
		public DirectionCode Code { get; private set; }
		public EduForm Form { get; private set; }

		public List<Enrollee> Enrollers { get; } = new List<Enrollee>();

		public Direction(string name, EduForm form)
		{
			Name = name;
			Code = DirectionCode.Parse(Name.Substring(0, 8));
			Form = form;
		}

		public Direction(string name, DirectionCode code, EduForm form)
		{
			Name = name;
			Code = code;
			Form = form;
		}

		public override string ToString() => $"{Name} — {Form}";

		public int CompareTo(object obj)
		{
			var d = (Direction)obj;
			var c = d.Code;
			if (Code.CompareTo(c) == 0)
			{
				if (Name.CompareTo(d.Name) == 0)
				{
					return Form.CompareTo(d.Form);
				}
				else
				{
					return Name.CompareTo(d.Name);
				}
			}
			return Code.CompareTo(c);
		}
	}
}
