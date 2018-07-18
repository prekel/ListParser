using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ListParser.Core
{
	public class Base : List<Direction>
	{
		public IDictionary<Enrollee, SortedDictionary<Direction, int>> Enrollers { get; } = new SortedDictionary<Enrollee, SortedDictionary<Direction, int>>();

		public TextReader Reader { get; set; }

		public void Add(TextReader r, EduForm form)
		{
			var nl = 0; Direction.Code c;
			Reader = r;
			Direction dir;
			try
			{
				while (true)
				{
					var l = Reader.ReadLine();
					if (l.Length < 8 || !Direction.Code.TryParse(l.Substring(0, 8), out c)) continue;
					dir = new Direction(l, form);
					Add(dir);
					break;
				}
				while (true)
				{
					var l = Reader.ReadLine();
					if (l.Length >= 8 && Direction.Code.TryParse(l.Substring(0, 8), out c))
					{
						dir = new Direction(l, form);
						Add(dir);
						continue;
					}
					if (Int32.TryParse(l.Split()[0], out nl))
					{
						var ls = l.Split();
						var n = Int32.Parse(ls[0]);
						var ln = ls[1];
						var fn = ls[2];
						var p = ls[3];
						Enrollee enr;
						if (p == "ЕГЭ" || p == "ВИ")
						{
							enr = new Enrollee(ln, fn, "", p);
						}
						else if (ls.Length == 4)
						{
							enr = new Enrollee(ln, fn, p, "");
						}
						else
						{
							enr = new Enrollee(ln, fn, p, ls[4]);
						}
						if (Enrollers.ContainsKey(enr))
						{
							if (!Enrollers[enr].ContainsKey(dir)) Enrollers[enr].Add(dir, n);
							enr.Directions = Enrollers[enr];
						}
						else
						{
							Enrollers.Add(enr, (SortedDictionary<Direction, int>)enr.Directions);
							enr.Directions.Add(dir, n);
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

		public void Init(DirectoryInfo dir)
		{
			foreach (var i in EduForm.Forms)
			{
				Add(new StreamReader(Path.Combine(dir.FullName, i.Code + ".txt")), i);
			}
		}

		public void Init(IEnumerable<(TextReader, EduForm)> rs)
		{
			foreach (var i in rs)
			{
				Add(i.Item1, i.Item2);
			}
		}
	}
}
