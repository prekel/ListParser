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
		public static string TextExtract(PdfReader reader)
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

		static void Main(string[] args)
		{
			var date = args[0];
			var wd = new DirectoryInfo(".");
			if (!(from i in wd.GetDirectories() where i.Name == date select i).Any())
			{
				wd.CreateSubdirectory(date);
				var web = new WebClient();
				Uri url(string dat, string code) => new Uri(new Uri(@"http://admissions.sfu-kras.ru/files/admissions/2018/lists/"), dat + @"/" + code + ".pdf");
				foreach (var i in EduForm.Forms)
				{
					var reader = new PdfReader(url(date, i.Code));
					using (var sw = new StreamWriter(System.IO.Path.Combine(date, i.Code + ".txt")))
					{
						var s = TextExtract(reader).Split('\n');
						foreach (var j in s)
						{
							sw.WriteLine(j);
						}
					}
				}
			}
			var d = new DirectoryInfo(date); 

			var b = new Base();
			b.Init(d);

			using (var sw1 = new StreamWriter(System.IO.Path.Combine(date, "1.json")))
			using (var sw2 = new StreamWriter(System.IO.Path.Combine(date, "2.json")))
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
