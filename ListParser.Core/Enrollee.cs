using System;
using System.Collections.Generic;
using System.Text;

namespace ListParser.Core
{
    public class Enrollee : IComparable
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }
        public string EgeTest { get; private set; }

		public IDictionary<Direction, int> Directions { get; set; } = new SortedDictionary<Direction, int>();

		public Enrollee(string ln, string fn, string p, string e)
		{
			FirstName = fn;
			LastName = ln;
			Patronymic = p;
			EgeTest = e;
		}

		public override string ToString() => $"{LastName} {FirstName} {Patronymic} {EgeTest}";
		public string ToString1() => $"{LastName} {FirstName} {Patronymic}";

		public int CompareTo(object obj)
		{
			var en = (Enrollee)obj;
			return ToString1().CompareTo(en.ToString1());
		}
	}
}
