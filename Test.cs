using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace FileSort
{
	class Test
	{
		private static void MainNONGUI()
		{
			//testing the full function..

			Console.WriteLine("string directory, string title = \"\", int index = 1, bool space = true, bool padding = false");
			string arg0 = Console.ReadLine();
			string arg1 = Console.ReadLine();
			int arg2 = int.Parse(Console.ReadLine());
			bool arg3 = Console.ReadLine().ToLower().StartsWith("t");
			bool arg4 = Console.ReadLine().ToLower().StartsWith("t");

			R.Rename(arg0, arg1, arg2, arg3, arg4);
		}

		private static void MainGUI()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new GUI());
		}

		[STAThread]
		public static void Main(String[] args)
		{
			MainGUI();
		}
	}
}
