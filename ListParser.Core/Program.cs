using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Net;
using Newtonsoft.Json;

namespace ListParser.Core
{
	class Program
	{
		private static string TextExtract(PdfReader reader)
		{
			var s = "";
			for (int i = 1; i <= reader.NumberOfPages; ++i)
			{
				var strategy = new SimpleTextExtractionStrategy();
				var text = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
				s += text;
			}
			reader.Close();
			return s;
		}

		private static Uri CraftUrl(DateTime date, string code)
		{
			var l1 = @"http://admissions.sfu-kras.ru/files/admissions/";
			var l2 = @"/lists/";
			return new Uri($"{l1}{date.Year}{l2}{date.Day:D2}{date.Month:D2}/{code}.pdf");
		}

		private static void Download(DirectoryInfo wd, DateTime date)
		{
			wd.CreateSubdirectory(date.ToShortDateString());
			var web = new WebClient();
			foreach (var i in EduForm.Forms)
			{
				var reader = new PdfReader(CraftUrl(date, i.Code));
				using (var sw = new StreamWriter(System.IO.Path.Combine(date.ToShortDateString(), i.Code + ".txt")))
				{
					var s = TextExtract(reader).Split('\n');
					foreach (var j in s)
					{
						sw.WriteLine(j);
					}
				}
			}
		}

		static void Main(string[] args)
		{
			//var date = new DateTime(year: 2018, day: Int32.Parse(args[0].Substring(0, 2)), month: Int32.Parse(args[0].Substring(2, 2)));
			var date = DateTime.Parse(args[0]);
			var wd = new DirectoryInfo(".");
			if (!(from i in wd.GetDirectories() where i.Name == date.ToShortDateString() select i).Any())
			{
				try
				{
					Download(wd, date);
				}
				catch (WebException)
				{
					new DirectoryInfo(date.ToShortDateString()).Delete();
					Environment.Exit(1);
				}
			}

			//var d = new DirectoryInfo(date.ToShortDateString()); 
			Directory.SetCurrentDirectory(date.ToShortDateString());
			var d = new DirectoryInfo(".");

			var b = new Base();
			b.Init(d);

			using (var sw1 = new StreamWriter("1.json"))
			using (var sw2 = new StreamWriter("2.json"))
			{
				sw1.Write(JsonConvert.SerializeObject(b, Formatting.Indented));
				sw2.Write(JsonConvert.SerializeObject(b.Enrollers, Formatting.Indented));
			}
		}

		//public static IEnumerable<Enrollee> LastNameSearch(Base b, string ln)
		//{
		//	return from i in b.Enrollers.Keys where i.LastName == ln select i;
		//}
	}
}
