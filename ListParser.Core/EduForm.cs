using System;
using System.Collections.Generic;
using System.Text;

namespace ListParser.Core
{
	public class EduForm : IComparable
	{
		private static string[] Codes = new string[] { "OB", "OP", "OsobOB", "BezOB", "cel", "ZB", "celZ", "OsobZB", "ZP", "OZP" };
		private static string[] Names = new string[] { "Очная форма, бюджетная основа обучения", "Очная форма, платная основа обучения", "Очная форма, бюджетная основа обучения, особое право", "Очная форма, бюджетная основа обучения, без вступительных испытаний", "Очная форма, бюджетная основа обучения, целевой набор", "Заочная форма, бюджетная основа обучения", "Заочная форма, бюджетная основа обучения, целевой набор", "Заочная форма, бюджетная основа обучения, особое право", "Заочная форма, платная основа обучения", "Очно-заочная форма, платная основа обучения" };

		public static EduForm[] Forms { get; set; }

		static EduForm()
		{
			Forms = new EduForm[Codes.Length];
			for (var i = 0; i < Codes.Length; i++)
			{
				Forms[i] = new EduForm(Names[i], Codes[i]);
			}
		}

		public string Name { get; set; }
		public string Code { get; set; }

		public EduForm(string name, string code)
		{
			Name = name;
			Code = code;
		}

		public int CompareTo(object obj) => Array.IndexOf(Codes, Code).CompareTo(Array.IndexOf(Codes, ((EduForm)obj).Code));

		public override string ToString() => Name;
	}
}
