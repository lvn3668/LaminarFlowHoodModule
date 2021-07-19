using System;
// Author Lalitha Viswanathan
// LFHM V2.0 
using System.Windows.Forms;

namespace LaminarFlowHoodModule {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
