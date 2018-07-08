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

		public TextReader Reader { get; set; }

		public void Init1(TextReader r)
		{
			Reader = r;
			var nl = 0;
			var rcode = new Regex("[0-9][0-9].[0-9][0-9].[0-9][0-9]");
			var dir = new Direction("");
			try
			{
				while (true)
				{
					var l = Reader.ReadLine();
					if (l.Length < 8 || !rcode.IsMatch(l.Substring(0, 8))) continue;
					dir = new Direction(l);
					Add(dir);
					break;
				}
				while (true)
				{
					var l = Reader.ReadLine();
					if (l.Length >= 8 && rcode.IsMatch(l.Substring(0, 8)))
					{
						dir = new Direction(l);
						Add(dir);
						continue;
					}
					if (Int32.TryParse(l, out nl))
					{
						var ln = Reader.ReadLine();
						var fn = Reader.ReadLine();
						var p = Reader.ReadLine();
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
			catch (IOException)
			{

			}
		}

		public void Init(TextReader r)
		{
			Reader = r;
			var nl = 0;
			var rcode = new Regex("[0-9][0-9].[0-9][0-9].[0-9][0-9]");
			var dir = new Direction("");
			try
			{
				while (true)
				{
					var l = Reader.ReadLine();
					if (l.Length < 8 || !rcode.IsMatch(l.Substring(0, 8))) continue;
					dir = new Direction(l);
					Add(dir);
					break;
				}
				while (true)
				{
					var l = Reader.ReadLine();
					if (l.Length >= 8 && rcode.IsMatch(l.Substring(0, 8)))
					{
						dir = new Direction(l);
						Add(dir);
						continue;
					}
					if (Int32.TryParse(l.Split()[0], out nl))
					{
						var ls = l.Split();
						var ln = ls[1];
						var fn = ls[2];
						var p = ls[3];
						Enrollee enr;
						if (p == "ЕГЭ" || p == "ВИ")
						{
							enr = new Enrollee(ln, fn, "", p);
						}
						else
						{
							enr = new Enrollee(ln, fn, p, ls[4]);
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
			catch (IOException)
			{

			}
			catch (NullReferenceException)
			{

			}
		}
	}
}
