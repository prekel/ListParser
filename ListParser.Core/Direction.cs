using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ListParser.Core
{
	public class Direction : IComparable
	{
		public string Name { get; private set; }
		public Code DCode { get; private set; }
		public EduForm Form { get; private set; }

		public List<Enrollee> Enrollers { get; } = new List<Enrollee>();

		public class Code : IComparable
		{
			public int First { get; set; }
			public int Second { get; set; }
			public int Third { get; set; }

			private bool Check()
			{
				if (0 <= First && First < 100 ||
					0 <= Second && Second < 100 ||
					0 <= Third && Third < 100)
				{
					return true;
				}
				return false;
			}

			public Code(int first, int second, int third)
			{
				First = first;
				Second = second;
				Third = third;
			}

			public static Code Parse(string s)
			{
				var ss = (from i in s.Split('.') select Int32.Parse(i)).ToArray();
				var c = new Code(ss[0], ss[1], ss[2]);
				if (!c.Check()) throw new ApplicationException("Неправильный код");
				return c;
			}

			public static bool TryParse(string s, out Code c)
			{
				c = null;
				if (s.Length != 8 || s[2] != '.' || s[5] != '.') return false;
				try
				{
					var ss = (from i in s.Split('.') select Int32.Parse(i)).ToArray();
					c = new Code(ss[0], ss[1], ss[2]);
				}
				catch
				{
					return false;
				}
				if (c.Check()) return true;
				return false;
			}

			public override string ToString() => $"{First:D2}.{Second:D2}.{Third:D2}";

			public int CompareTo(object obj)
			{
				var c = (Code)obj;
				if (First.CompareTo(c.First) == 0)
				{
					if (Second.CompareTo(c.Second) == 0)
					{
						return Third.CompareTo(c.Third);
					}
					else
					{
						return Second.CompareTo(c.Second);
					}
				}
				else
				{
					return First.CompareTo(c.First);
				}
			}
		}

		public Direction(string name, EduForm form)
		{
			Name = name;
			DCode = Code.Parse(Name.Substring(0, 8));
			Form = form;
		}

		public Direction(string name, Code code, EduForm form)
		{
			Name = name;
			DCode = code;
			Form = form;
		}

		public override string ToString() => $"{Name} — {Form}";

		public int CompareTo(object obj)
		{
			var d = (Direction)obj;
			var c = d.DCode;
			if (DCode.CompareTo(c) == 0)
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
			return DCode.CompareTo(c);
		}
	}
}
