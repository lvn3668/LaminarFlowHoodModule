using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace LaminarFlowHoodModule {
	public partial class MainForm : Form {

		private NameValueCollection appSettings = ConfigurationManager.AppSettings;
		private Color okColor = Color.Green;
		private Color errorColor = Color.Red;
		private Color black = Color.Black;
		private string bloodSampleID = String.Empty;
		private ArrayList scannedBloodSamples = new ArrayList();
		private ArrayList aliquotScans = new ArrayList();
		private TextWriter error_log;
		private TwoDScanner scanner;
		private TwoDScan twoDscan = null;
		private Study study;
		private MavericDatabaseInterface mdb;

		/// <summary>
		/// Constructor
		/// </summary>
		public MainForm() {
			InitializeComponent();

			TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText("Debug.txt"));
			Debug.Listeners.Add(tr2);

			// clear messages left from designer
			lbl_Status.Text = String.Empty;
			lbl_Instructions.Text = String.Empty;
			tb_1DScanner.Text = String.Empty;
			lbl_Progress.Text = String.Empty;

			// instantiate a new 2D scanner
			scanner = new TwoDScanner();

			// connect to the scanner server
			bool connected = scanner.Connect(appSettings["2dScannerHostname"], Int32.Parse(appSettings["2dScannerPort"]));
			if (!connected) {
				MessageBox.Show("Visionmate Server is not running!");
			}

			// create new database interface
			mdb = new MavericDatabaseInterface();

			// set up error log
			if (File.Exists(appSettings["ErrorLog"])) {
				error_log = new StreamWriter(appSettings["ErrorLog"], true);
			} else {
				error_log = new StreamWriter(appSettings["ErrorLog"], true);
				error_log.WriteLine(appSettings["ErrorLogHeader"]);
				error_log.Flush();
			}


			/***************************
			 * Temporary section because MVP is the default for now
			 * 
			 * *************************/
			combo_Study.SelectedIndex = 0;
			gb_Study.Text = String.Empty;
			study = new Study(combo_Study.SelectedItem.ToString(), appSettings["MVP_RackType"], appSettings["BuffyCoatCoordinate"], appSettings["Plasma1Coordinate"], appSettings["Plasma2Coordinate"]);
			// Set up 1d scanner GUI
			setGUIForOneDScan();
			// Wait for 1D input
			/***************************
			 * End Temporary Section
			 * *************************/
		}

		#region events

		/// <summary>
		/// Handles study selection changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void combo_Study_SelectedIndexChanged(object sender, EventArgs e) {
			gb_Study.Text = String.Empty;
			study = new Study(combo_Study.SelectedItem.ToString(), appSettings["MVP_RackType"], appSettings["BuffyCoatCoordinate"], appSettings["Plasma1Coordinate"], appSettings["Plasma2Coordinate"]);

			// Set up 1d scanner GUI
			setGUIForOneDScan();
			// Wait for 1D input

		}

		/// <summary>
		/// Waits for the EOL sequence from the 1D Scanner and evaluates the input,
		/// kicking off the 2D scan if the rackID is valid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tb_1DScanner_KeyDown(object sender, KeyEventArgs e) {
			// Check to see if we have unsaved study data and save, if so
			saveTwoDScanData();

			if (e.KeyCode == Keys.Enter) {
				Debug.WriteLine("Blood sample ID: " + tb_1DScanner.Text);
				if (validate1DScan(tb_1DScanner.Text)) {
					bloodSampleID = tb_1DScanner.Text;

					// set gui to aliquot scan state
					setGUIForTwoDScan();
					start2Dscan();

				}
			}
		}

		/// <summary>
		/// Handles volume up changes for plasma
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_plasma1Up_Click(object sender, EventArgs e) {
			int defaultValue = Int32.Parse(appSettings["plasma1Default"]);
			if (twoDscan != null) {
				twoDscan.Plasma1Volume += 100;
				if (twoDscan.Plasma1Volume > defaultValue) {
					twoDscan.Plasma1Volume = defaultValue;
				}
				label_Plasma1.Text = twoDscan.Plasma1Volume.ToString() + " ml";
			}
			tb_1DScanner.Focus();

		}

		/// <summary>
		/// Handles volume down changes for plasma
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_plasma1Down_Click(object sender, EventArgs e) {
			if (twoDscan != null) {
				twoDscan.Plasma1Volume -= 100;
				if (twoDscan.Plasma1Volume < 0) {
					twoDscan.Plasma1Volume = 0;
				}
				label_Plasma1.Text = twoDscan.Plasma1Volume.ToString() + " ml";
			}
			tb_1DScanner.Focus();

		}

		/// <summary>
		/// Handles volume up changes for plasma
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_plasma2Up_Click(object sender, EventArgs e) {
			int defaultValue = Int32.Parse(appSettings["plasma2Default"]);
			if (twoDscan != null) {
				twoDscan.Plasma2Volume += 100;
				if (twoDscan.Plasma2Volume > defaultValue) {
					twoDscan.Plasma2Volume = defaultValue;
				}
				label_Plasma2.Text = twoDscan.Plasma2Volume.ToString() + " ml";
			}
			tb_1DScanner.Focus();

		}

		/// <summary>
		/// Handles volume down changes for plasma
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_plasma2Down_Click(object sender, EventArgs e) {
			if (twoDscan != null) {
				twoDscan.Plasma2Volume -= 100;
				if (twoDscan.Plasma2Volume < 0) {
					twoDscan.Plasma2Volume = 0;
				}
				label_Plasma2.Text = twoDscan.Plasma2Volume.ToString() + " ml";
			}
			tb_1DScanner.Focus();

		}

		/// <summary>
		/// Handles volume up changes for buffycoat
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_buffycoatUp_Click(object sender, EventArgs e) {
			int defaultValue = Int32.Parse(appSettings["buffyCoatDefault"]);
			if (twoDscan != null) {
				twoDscan.BuffyCoatVolume += 100;
				if (twoDscan.BuffyCoatVolume > defaultValue) {
					twoDscan.BuffyCoatVolume = defaultValue;
				}
				label_BuffyCoat.Text = twoDscan.BuffyCoatVolume.ToString() + " ml";
			}
			tb_1DScanner.Focus();

		}

		/// <summary>
		/// Handles volume down changes for buffycoat
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_buffycoatDown_Click(object sender, EventArgs e) {
			if (twoDscan != null) {
				twoDscan.BuffyCoatVolume -= 100;
				if (twoDscan.BuffyCoatVolume < 0) {
					twoDscan.BuffyCoatVolume = 0;
				}
				label_BuffyCoat.Text = twoDscan.BuffyCoatVolume.ToString() + " ml";
			}
			tb_1DScanner.Focus();

		}

		/// <summary>
		/// When the Export button is clicked, the contents of the Export folder are
		/// copied to a special folder on another drive, most likely a thumb drive.
		/// They are then copied to a backup folder in the application space, and deleted
		/// from the current export folder.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Export_Click(object sender, EventArgs e) {
			saveTwoDScanData();
			// Write contents of CVS file to thumb drive
			try {
				// check for data to be exported
				string[] cvsList = Directory.GetFiles(appSettings["ExportFolder"]);
				if (cvsList.Length < 1) {
					lbl_Status.Visible = true;
					lbl_Status.ForeColor = errorColor;
					lbl_Status.Text = appSettings["NoExportData"];
					tb_1DScanner.Focus();
					return;
				}

				string destPath = Path.Combine(appSettings["ExportDrive"], appSettings["ExportFolder"]);

				// Check for thumb drive
				if (!Directory.Exists(appSettings["ExportDrive"])) {
					lbl_Status.Visible = true;
					lbl_Status.ForeColor = errorColor;
					lbl_Status.Text = String.Format(appSettings["NoThumbDrive"], appSettings["ExportDrive"]);
					tb_1DScanner.Focus();
					return;

				}

				// Check to see if destination path exists, create the folder if it doesn't.
				if (!Directory.Exists(destPath)) {
					try {
						Directory.CreateDirectory(destPath);
					} catch (IOException error) {
						Debug.WriteLine(error.Message);
						lbl_Status.Visible = true;
						lbl_Status.ForeColor = errorColor;
						lbl_Status.Text = appSettings["CantCreateExportFolder"];
						tb_1DScanner.Focus();
						return;
					}
				}

				// Check for backup folder, create one if missing
				if (!Directory.Exists(appSettings["BackupFolder"])) {
					try {
						Directory.CreateDirectory(appSettings["BackupFolder"]);
					} catch (IOException error) {
						Debug.WriteLine(error.Message);
						lbl_Status.Visible = true;
						lbl_Status.ForeColor = errorColor;
						lbl_Status.Text = appSettings["CantCreateBackupFolder"];
						tb_1DScanner.Focus();
						return;
					}

				}

				// move each file to thumbdrive and backup folder
				// then delete
				foreach (string f in cvsList) {
					string fName = f.Substring(appSettings["ExportFolder"].Length + 1);
					File.Copy(f, Path.Combine(destPath, fName), true);
					File.Copy(f, appSettings["BackupFolder"] + "\\" + fName, true);
					File.Delete(f);
				}

				lbl_Status.Visible = true;
				lbl_Status.ForeColor = okColor;
				lbl_Status.Text = appSettings["DataExportSuccess"];
				tb_1DScanner.Focus();

			} catch (IOException copyError) {
				lbl_Status.Visible = true;
				lbl_Status.ForeColor = errorColor;
				lbl_Status.Text = copyError.Message;
				tb_1DScanner.Focus();
			}

		}

		/// <summary>
		/// Handles reset button clicks.
		/// Disregards any current 1d or 2d scans
		/// Resets the UI for 1d scan
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Reset_Click(object sender, EventArgs e) {
			add_error("reset");
			try {
				if (TwoDScannerBackgroundWorker.IsBusy) {
					TwoDScannerBackgroundWorker.CancelAsync();
				}

				twoDscan = null;
				bloodSampleID = null;
				setGUIForOneDScan();

			} catch (Exception error) {
				Debug.WriteLine(error.Message);
				Debug.WriteLine(error.StackTrace);
			}

		}

		/// <summary>
		/// Handles exit button clicks
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Exit_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		/// <summary>
		/// Intercepts a form exit
		/// Saves any unsaved 2d scans if exit confirmation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			string msg = appSettings["ExitMsg"];
			var result = MessageBox.Show(msg,
														"Close Application",
														MessageBoxButtons.YesNo,
														MessageBoxIcon.Question);
			if (result == DialogResult.No) {
				e.Cancel = true;
				tb_1DScanner.Focus();
			} else {
				saveTwoDScanData();
				error_log.Close();
				error_log.Dispose();
			}

		}

		#endregion

		#region Background worker

		/// <summary>
		/// Asynchornous thread for polling the 2d scanner while allowing the UI to function
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TwoDScannerBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;

			try {

				worker.ReportProgress(0, appSettings["PrepareScan"]);

				// Set a unique rack ID
				scanner.SetRackID(System.DateTime.Now.ToString().Replace('/', '1').Replace(':', '1').Replace(' ', '1'));
				// Sets the expected number of tubes, more efficient scanning
				scanner.SetNumberOfTubes(appSettings["ExpectedTubes"]);

				// Pause if no rack
				while (!scanner.RackPresent()) {
					Debug.WriteLine("Rack is not present");
					worker.ReportProgress(-1, appSettings["RackNotPresent"]);
				    Thread.Sleep(2000);
				}

				scanner.StartScan();
				// Poor scanner needs some time to reset itself
				// this seems to be the key to getting accurate scans.
				Thread.Sleep(Int32.Parse(appSettings["ScannerSleep"]));

				worker.ReportProgress(1, appSettings["Scanning"]);

				ScannerStatus status = scanner.GetStatus();

				while (!status.FinishedScan) {

					// This will actually return true in the middle of a scan
					// despite a rack not being present
					while (!scanner.RackPresent()) {
						Debug.WriteLine("Rack is not present");
						worker.ReportProgress(-1, appSettings["RackNotPresent"]);
						Thread.Sleep(2000);
					}

					status = scanner.GetStatus();
					worker.ReportProgress(2, appSettings["Scanning"]);
					Debug.WriteLine("Scanning.");
					Thread.Sleep(500);
				}

				worker.ReportProgress(3, appSettings["WaitingForResults"]);
				// scan has finished
				twoDscan = new TwoDScan(scanner);


			} catch (Exception error) {
				Debug.WriteLine(error.Message);
				Debug.WriteLine(error.StackTrace);
				worker.ReportProgress(0, appSettings["FatalError"]);
			}

		}

		/// <summary>
		/// Runs when the polling is complete for a specific rack
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TwoDScannerBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Debug.WriteLine("TwoDScannerBackgroundWorker_RunWorkerCompleted");

			// check for duplicate aliquots in recently completed scan
			if (duplicateAliquots(twoDscan)) {
				string loc = duplicateAliquotLocation(twoDscan);
				lbl_Status.Visible = true;
				lbl_Status.ForeColor = errorColor;
				lbl_Status.Text = String.Format(appSettings["RepeatAliquot"], loc);
				// User will need to fix the rack, we can afford to sleep this much
				Thread.Sleep(2000);
				start2Dscan();
			} else if (!validate2DScanData(twoDscan)) {
				// errors are checked and Status posted in validate2DScanData

				// User will need to fix the rack, we can afford to sleep this much
				Thread.Sleep(2000);
				start2Dscan();

			} else {
				// Allow user to adjust volumes
				setGuiForTwoDScanSuccess();
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TwoDScannerBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if (e.ProgressPercentage < 0) {
				lbl_Instructions.ForeColor = errorColor;
			} else {
				lbl_Instructions.ForeColor = black;
			}
			lbl_Instructions.Text = e.UserState.ToString();
		}

		#endregion

		/// <summary>
		/// Sets up the GUI and kicks off a 2d scan thread
		/// </summary>
		private void start2Dscan() {
			if (!TwoDScannerBackgroundWorker.IsBusy) {
				btn_Exit.Visible = false;
				btn_Export.Visible = false;
				btn_Reset.Visible = false;
				TwoDScannerBackgroundWorker.RunWorkerAsync();
			}
		}

		/// <summary>
		/// Validates whether or not a rack ID is in the correct format
		/// </summary>
		/// <param name="candidate"></param>
		/// <returns>True if the rack ID is in the correct format</returns>
		private bool validate1DScan(string candidate) {
			// 1D barcode is checked against a regular expression
			Regex r = new Regex(appSettings["1DRegex"]);
			if (!r.IsMatch(candidate)) {
				lbl_Status.Visible = true;
				lbl_Status.ForeColor = errorColor;
				lbl_Status.Text = appSettings["InvalidBloodSampleID"];
				tb_1DScanner.Text = String.Empty;
				tb_1DScanner.Focus();
				return false;
			}

			// This should be handled by the database
			// Check to see if the blood sample has already been saved with a
			// 2D scan
			if (scannedBloodSamples.Contains(candidate)) {
				lbl_Status.Visible = true;
				lbl_Status.ForeColor = errorColor;
				lbl_Status.Text = appSettings["RepeatBloodSampleID"];
				tb_1DScanner.Text = String.Empty;
				tb_1DScanner.Focus();
				return false;
			}

			return true;
		}

		/// <summary>
		/// Returns true for the first detected duplicate barcode
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		private bool duplicateAliquots(TwoDScan t) {
			foreach (KeyValuePair<string, string> kv in t.Barcodes) {
				if (aliquotScans.Contains(kv.Value)) {
					return true;
				}
			}
			return false;

		}

		/// <summary>
		/// Returns the first duplicate barcode
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		private string duplicateAliquotLocation(TwoDScan t) {
			foreach (KeyValuePair<string, string> kv in t.Barcodes) {
				if (aliquotScans.Contains(kv.Value)) {
					add_error("duplicate aliquot ID", kv.Key + ":" + kv.Value);
					return kv.Key;
				}
			}
			return String.Empty;
		}

		/// <summary>
		/// Validate whether scanned barcodes are in the expected location or not
		/// </summary>
		/// <param name="scan"></param>
		/// <returns>true or false validating 2D scan</returns>
		private bool validate2DScanData(TwoDScan scan) {
			try {
				// Check if expected number of tubes match
				if (scan.Barcodes.Keys.Count != study.ExpectedNumberOfTubes) {
					add_error("unexpected number of aliquots");
					lbl_Status.ForeColor = errorColor;
					lbl_Status.Text = String.Format(appSettings["WrongNumberAliquots"], study.ExpectedNumberOfTubes, scan.Barcodes.Keys.Count);
					return false;
				}

				// For each 2D barcode, check if it's a valid location
				foreach (string location in scan.Barcodes.Keys) {
					if (!checkIfValid2DBarcodeLocation(location)) {
						lbl_Status.ForeColor = errorColor;
						lbl_Status.Text = appSettings["AliquotsIncorrectPosition"];
						add_error("aliquot in wrong position", location);
						return false;
					}
				}
				return true;
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
			}

			return false;
		}

		/// <summary>
		/// checks if given location matches expected buffy or plasma
		/// </summary>
		/// <param name="location">row and column of 2d barcode scanned</param>
		/// <returns>true or false indicating whether barcode is at expected location or not</returns>
		private bool checkIfValid2DBarcodeLocation(string location) {
				if (location == study.BuffyLocation) {
					return true;
				}

				if (location == study.Plasma1Location) {
					return true;
				}

				if (location == study.Plasma2Location) {
					return true;
				}

				return false;

		}

		/// <summary>
		/// Sets up the GUI for a 1D scan
		/// </summary>
		private void setGUIForOneDScan() {
			gb_VolumeControls.Visible = false;
			gb_Scanner.Visible = true;
			label_Plasma1.Text = "0 ml";
			label_Plasma2.Text = "0 ml";
			label_BuffyCoat.Text = "0 ml";
			lbl_Instructions.Text = appSettings["BloodSampleScanText"];
			lbl_Status.Text = String.Empty;
			tb_1DScanner.Text = String.Empty;
			tb_1DScanner.Enabled = true;
			tb_1DScanner.Focus();
			btn_Export.Visible = true;
			btn_Exit.Visible = true;
			btn_Reset.Visible = false;
		}

		/// <summary>
		/// Sets up the GUI for a 2d Scan
		/// </summary>
		private void setGUIForTwoDScan() {
			lbl_Status.ForeColor = okColor;
			lbl_Status.Text = appSettings["BloodSampleScanSuccess"];
			lbl_Instructions.ForeColor = black;
			lbl_Instructions.Text = appSettings["ReadyToScanRack"];
			tb_1DScanner.Enabled = false;
		}

		/// <summary>
		/// Sets up the GUI for 2D Scan success and volume adjustment
		/// </summary>
		private void setGuiForTwoDScanSuccess() {
			lbl_Status.ForeColor = okColor;
			lbl_Status.Text = appSettings["AliquotScanSuccess"];
			label_Plasma1.Text = twoDscan.Plasma1Volume.ToString() + " ml";
			label_Plasma2.Text = twoDscan.Plasma2Volume.ToString() + " ml";
			label_BuffyCoat.Text = twoDscan.BuffyCoatVolume.ToString() + " ml";
			gb_VolumeControls.Visible = true;
			tb_1DScanner.Text = String.Empty;
			tb_1DScanner.Enabled = true;
			tb_1DScanner.Focus();
			lbl_Instructions.Text = appSettings["BloodSampleScanText"];
			btn_Exit.Visible = true;
			btn_Export.Visible = true;
			btn_Reset.Visible = true;
		}

		/// <summary>
		/// Saves a 2D Scan to the database
		/// Records blood sample ID for dupe checking
		/// </summary>
		private void saveTwoDScanData() {
			if (twoDscan != null) {
				mdb.SaveLFHMAliquots(study.StudyName, bloodSampleID, twoDscan);
				foreach (KeyValuePair<string, string> kv in twoDscan.Barcodes) {
					aliquotScans.Add(kv.Value);
				}
				scannedBloodSamples.Add(bloodSampleID);
				twoDscan = null;
				setGUIForOneDScan();
			}
		}

		/// <summary>
		/// Writes the message to the error log.
		/// </summary>
		/// <param name="error_msg">error description</param>
		private void add_error(string error_msg) {
			add_error(error_msg, String.Empty);
		}

		/// <summary>
		/// Writes a message to the error log
		/// </summary>
		/// <param name="error_msg">error description</param>
		/// <param name="details">error details</param>
		private void add_error(string error_msg, string details) {
			string error = (bloodSampleID != null ? bloodSampleID : String.Empty)
				+ "," + (study.StudyName != null ? study.StudyName : String.Empty)
				+ "," + System.DateTime.Now.ToString("MM/dd/yyyy")
				+ "," + System.DateTime.Now.ToString("t")
				+ "," + System.Environment.UserName
				+ "," + System.Environment.MachineName
				+ "," + error_msg
				+ "," + details;
			Debug.WriteLine(error);
			error_log.WriteLine(error);
			error_log.Flush();
		}

	}
}
