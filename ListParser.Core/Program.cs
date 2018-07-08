using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ListParser.Core
{
	class Program
	{
		static void Main(string[] args)
		{
			var reader = new PdfReader(new Uri(@"http://admissions.sfu-kras.ru/files/admissions/2018/lists/0707/OB.pdf"));
			var s = "";
			for (int i = 1; i <= reader.NumberOfPages; ++i)
			{
				var strategy = new SimpleTextExtractionStrategy();
				var text = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
				s += text;
			}
			reader.Close();


			var b1 = new Base();
			b1.Init(new StringReader(s));


			//using (var r = new StreamReader(args[0]))
			//{
			//	b1. 
			//}
			//var b2 = new Base();
			//using (var r = new StreamReader(args[1]))
			//{
			//	b2.FromStreamReader(r);
			//}

			//var q = (from i in b2.Enrollers.Keys where !b1.Enrollers.ContainsKey(i) select i).ToList();

			//var qeq4 = (from i in b.Enrollers where i.Value.Count == 4 select i).ToList();
			//var qlt3 = (from i in b.Enrollers where i.Value.Count < 3 select i).ToList();
			//var qeq2 = (from i in b.Enrollers where i.Value.Count == 2 select i).ToList();
			//var qeq1 = (from i in b.Enrollers where i.Value.Count == 1 select i).ToList();
		}

		public static IEnumerable<Enrollee> LastNameSearch(Base b, string ln)
		{
			return from i in b.Enrollers.Keys where i.LastName == ln select i;
		}
	}
}
