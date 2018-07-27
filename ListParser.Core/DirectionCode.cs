using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ListParser.Core
{
	public class DirectionCode : IComparable
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

		public DirectionCode(int first, int second, int third)
		{
			First = first;
			Second = second;
			Third = third;
		}

		public static DirectionCode Parse(string s)
		{
			var ss = (from i in s.Split('.') select Int32.Parse(i)).ToArray();
			var c = new DirectionCode(ss[0], ss[1], ss[2]);
			if (!c.Check()) throw new ApplicationException("Неправильный код");
			return c;
		}

		public static bool TryParse(string s, out DirectionCode c)
		{
			c = null;
			if (s.Length != 8 || s[2] != '.' || s[5] != '.') return false;
			try
			{
				var ss = (from i in s.Split('.') select Int32.Parse(i)).ToArray();
				c = new DirectionCode(ss[0], ss[1], ss[2]);
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
			var c = (DirectionCode)obj;
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

		public override int GetHashCode() => First * 10000 + Second * 100 + Third;
	}
}
