using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ListParser.Core
{
	public class Base : List<Direction>
	{
		public IDictionary<Enrollee, List<Direction>> Enrollers { get; } = new SortedDictionary<Enrollee, List<Direction>>();

		public void FromStreamReader(StreamReader r)
		{
			var nl = 0;
			var rcode = new Regex("[0-9][0-9].[0-9][0-9].[0-9][0-9]");
			var dir = new Direction("");
			while (!r.EndOfStream)
			{
				var l = r.ReadLine();
				if (l.Length < 8 || !rcode.IsMatch(l.Substring(0, 8))) continue;
				dir = new Direction(l);
				Add(dir);
				break;
			}
			while (!r.EndOfStream)
			{
				var l = r.ReadLine();
				if (l.Length >= 8 && rcode.IsMatch(l.Substring(0, 8)))
				{
					dir = new Direction(l);
					Add(dir);
					continue;
				}
				if (Int32.TryParse(l, out nl))
				{
					var ln = r.ReadLine();
					var fn = r.ReadLine();
					var p = r.ReadLine();
					Enrollee enr;
					if (p == "ЕГЭ" || p == "ВИ")
					{
						enr = new Enrollee(ln, fn, "", p);
					}
					else
					{
						enr = new Enrollee(ln, fn, p, r.ReadLine());
					}
					if (Enrollers.ContainsKey(enr))
					{
						Enrollers[enr].Add(dir);
						enr.Directions = Enrollers[enr];
					}
					else
					{
						Enrollers.Add(enr, enr.Directions);
						enr.Directions.Add(dir);
					}
					dir.Enrollers.Add(enr);
				}
			}
		}
	}
}
