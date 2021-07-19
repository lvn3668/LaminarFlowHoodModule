// Author Lalitha Viswanathan
// LFHM V2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;


namespace LaminarFlowHoodModule {
	internal class TwoDScan {
		private string currentProduct;
		private int numRowsOnRack;
		private int numColsOnRack;
		private int rackSize;
		private int totalNumberOfTubesRead;
		private int numberOfNoReads;
		private int tubesRead;
		private string decodeTime;
		private bool rackOrientation { get; set; }
		private bool rackPresent { get; set; }
		private Dictionary<String, String> barcodes;
		private int[,] rackMatrix;
		private string rackId;
		private static NameValueCollection appSettings = ConfigurationManager.AppSettings;
		private TwoDScanner scanner;

		private int plasma1Volume = Int32.Parse(appSettings["plasma1Default"]);
		private int plasma2Volume = Int32.Parse(appSettings["plasma2Default"]);
		private int buffyCoatVolume = Int32.Parse(appSettings["buffyCoatDefault"]);


		public TwoDScan() {
			barcodes = new Dictionary<String, String>();
		}

		public TwoDScan(TwoDScanner scanner) {
			try {
				this.scanner = scanner;
				Debug.WriteLine("Get current product");
				currentProduct = scanner.GetCurrentProduct();
				Debug.WriteLine("Get RackID");
				rackId = scanner.GetRackID();
				numRowsOnRack = Int32.Parse(currentProduct.Substring(1, 2));
				numColsOnRack = Int32.Parse(currentProduct.Substring(3, 2));
				rackSize = numRowsOnRack * numColsOnRack;
				Debug.WriteLine("Get number of No reads");
				numberOfNoReads = scanner.GetNumberOfNoReads();
				Debug.WriteLine("Get number of tubes");
				totalNumberOfTubesRead = scanner.GetNumberOfTubes();
				Debug.WriteLine("Get number of reads");
				tubesRead = scanner.GetNumberOfReads();
				Debug.WriteLine("Get decode time");
				decodeTime = scanner.GetDecodeTime();
				rackOrientation = true;
				rackPresent = true;
				Debug.WriteLine("Get bar code data");
				barcodes = scanner.GetBarcodeData();
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
			}
		}

		public int[,] RackMatrix {
			get { return rackMatrix; }
			set {
				rackMatrix = new int[NumRows, NumCols];
			}
		}

		public Dictionary<String, String> Barcodes {
			get { return barcodes; }
			set {
				barcodes = new Dictionary<String, String>();
				barcodes = value;
			}
		}

		public string RackId {
			get { return rackId; }
			set { rackId = value; }
		}

		public int TubesRead {
			get { return tubesRead; }
			set { tubesRead = value; }
		}
		public string CurrentProduct {
			get { return currentProduct; }
			set { currentProduct = value; }
		}

		public int RackSize {
			get { return rackSize; }
			set { rackSize = value; }
		}

		public int NumRows {
			get { return numRowsOnRack; }
			set { numRowsOnRack = value; }
		}

		public int NumCols {
			get { return numColsOnRack; }
			set { numColsOnRack = value; }
		}

		public int NoTubesRead {
			get { return totalNumberOfTubesRead; }
			set { totalNumberOfTubesRead = value; }
		}

		public int NoNoReads {
			get { return numberOfNoReads; }
			set { numberOfNoReads = value; }
		}

		public string DecodeTime {
			get { return decodeTime; }
			set { decodeTime = value; }
		}

		public bool RackPresent {
			get { return rackPresent; }
			set { rackPresent = value; }
		}

		public bool RackOrientation {
			get { return rackOrientation; }
			set { rackOrientation = value; }
		}

		public int Plasma1Volume {
			get { return plasma1Volume; }
			set { plasma1Volume = value; }
		}

		public int Plasma2Volume {
			get { return plasma2Volume; }
			set { plasma2Volume = value; }
		}

		public int BuffyCoatVolume {
			get { return buffyCoatVolume; }
			set { buffyCoatVolume = value; }
		}
	}
}
