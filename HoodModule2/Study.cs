// Author Lalitha Viswanathan
// LFHM V2.0
using System;
using System.Globalization;

namespace LaminarFlowHoodModule {
	public class Study {
		private string rackManufacturer;
		private string product;
		private int numRows;
		private int numCols;
		private string studyName;
		private int rackSize;
		private int expectedNumberOfBuffyTubes;
		private int expectedNumberOfPlasmaTubes;
		private int expectedNumberOfTotalTubes;
		private string buffyLocation;
		private string plasma1Location;
		private string plasma2Location;


		public string BuffyLocation {
			get { return buffyLocation; }
			set { buffyLocation = value; }
		}

		public string Plasma1Location {
			get { return plasma1Location; }
			set { plasma1Location = value; }
		}

		public string Plasma2Location {
			get { return plasma2Location; }
			set { plasma2Location = value; }
		}
		public String StudyName {
			get { return studyName; }
			set { studyName = value; }
		}

		public Study() {
		}

		public Study(string studyname) {
			studyName = studyname;
		}

		public Study(string study, string productName, string newbuffyLocation, string newplasma1Location, string newplasma2Location) {
			studyName = study;
			switch (productName.Substring(0, 1)) {
				case "A":
					rackManufacturer = "Abgene";
					break;
				case "N":
					rackManufacturer = "Nunc";
					break;
				case "M":
					rackManufacturer = "Matrix";
					break;
			}
			numRows = Int32.Parse(productName.Substring(1, 2), CultureInfo.InvariantCulture);
			numCols = Int32.Parse(productName.Substring(3, 2), CultureInfo.InvariantCulture);
			rackSize = numCols * numRows;
			product = productName;

			buffyLocation = newbuffyLocation;

			plasma1Location = newplasma1Location;
			plasma2Location = newplasma2Location;

			expectedNumberOfBuffyTubes = 1;
			expectedNumberOfPlasmaTubes = 2;
			expectedNumberOfTotalTubes = expectedNumberOfBuffyTubes + expectedNumberOfPlasmaTubes;
		}

		public int ExpectedNumberOfTubes {
			get { return expectedNumberOfTotalTubes; }
			set { expectedNumberOfTotalTubes = value; }
		}

		public string CurrentProduct {
			get { return rackManufacturer; }
			set { rackManufacturer = value; }
		}

		public int ExpectedNumberOfBuffy {
			get { return expectedNumberOfBuffyTubes; }
			set { expectedNumberOfBuffyTubes = value; }
		}

		public int ExpectedNumberOfPlasma {
			get { return expectedNumberOfPlasmaTubes; }
			set { expectedNumberOfPlasmaTubes = value; }
		}
	}
}
