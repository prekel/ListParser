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

		public List<Direction> Directions { get; set; } = new List<Direction>();

		public Enrollee(string ln, string fn, string p, string e)
		{
			FirstName = fn;
			LastName = ln;
			Patronymic = p;
			EgeTest = e;
		}

		public override string ToString() => $"{LastName} {FirstName} {Patronymic} {EgeTest}";

		public int CompareTo(object obj)
		{
			var en = (Enrollee)obj;
			return ToString().CompareTo(en.ToString());
		}
	}
}
