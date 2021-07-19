// Author Lalitha Viswanathan
// LFHM V2.0
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace LaminarFlowHoodModule {
	class TwoDScanner {

		private NameValueCollection appSettings = ConfigurationManager.AppSettings;

		private static TcpClient tcpClient = new TcpClient();
		private static NetworkStream clientSockStream { get; set; } = null;
		private StreamWriter streamWriter { get; set; } = null;
		private StreamReader streamReader { get; set; } = null;

		private string acknowledgeText = "OK";
		private string changeProduct = "P";
		private string getCurrentProduct = "C";
		private string getDecodeTime = "K";
		private string getExpectedNumberOfTubes = "A";
		private string getHardwareStatus = "Q";
		private string getStringStatus = "J";
		private string getRackOrientation = "O";
		private string getNumberOfNoReads = "G";
		private string getNumberOfTubes = "H";
		private string getNumberOfReads = "F";
		private string getRackID = "B";
		private string getRackPresent = "R";
		private string getScanData = "D";
		private string getScannerStatus = "L";
		private string setExpectedNumberOfTubes = "A";
		private string setRackID = "I";
		private string startScan = "S";
		private string enabledExpectedTubes = "N";
		private string reset = "Z";
		private string noTubeText = "No Tube";
		private string noReadText = "No Read";

		private string previousResult = String.Empty;
		//private bool ignorePreviousResultOnce = false;


		/// <summary>
		/// TwoDScanner constructor
		/// </summary>
		public TwoDScanner() {

		}

		/// <summary>
		/// connect to the server at given port
		/// </summary>
		/// <param name="hostname">hostname of the scanner server</param>
		/// <param name="port">post to connect to the server on</param>
		/// <returns></returns>
		internal bool Connect(string hostname, int port) {

			try {
				Debug.WriteLine("Before connecting, status of TCP Connection:" + tcpClient.Connected);

				bool isListening = false;

				// Check to see if the scanner is running on localhost
				// currently the only supported method
				if (!tcpClient.Connected) {
					List<IPEndPoint> listenersList = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().ToList();
					foreach (IPEndPoint listener in listenersList) {
						if (listener.Port.Equals(port)) {
							isListening = true;
							break;
						}
					}
					if (!isListening) {
						return false;
					}

					tcpClient = new TcpClient(hostname, port);
					//get a network stream from server   
					clientSockStream = tcpClient.GetStream();
					streamWriter = new StreamWriter(clientSockStream, System.Text.Encoding.ASCII);
					streamWriter.AutoFlush = true;
					streamWriter.NewLine = "\r";
					streamReader = new StreamReader(clientSockStream, System.Text.Encoding.ASCII);
					Debug.WriteLine("Connected to VisionMate server");
					return true;
				}

			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
			}

			return false;
		}

		/// <summary>
		/// Get the status of the scanner
		/// </summary>
		/// <returns>ScannerStatus object</returns>
		internal ScannerStatus GetStatus() {
			try {

				Debug.WriteLine("Getting scanner status");
				string response = communicateWithServer(getScannerStatus);
				Debug.WriteLine("Response from GetStatus: " + response);

				// Convert the decimal number to a bit array
				BitArray scannerStatusArray = new BitArray(System.BitConverter.GetBytes(Int32.Parse(response, CultureInfo.InvariantCulture)));

				ScannerStatus status = new ScannerStatus(scannerStatusArray);
				return status;
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
				throw e;
			}
		}

		/// <summary>
		/// Sends a command to the server and returns the response
		/// </summary>
		/// <param name="command">Server Command</param>
		/// <returns>Server response</returns>
		private string communicateWithServer(string command) {
			try {
				writeToServer(command);
				return readFromServer();
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
				throw e;
			}

		}

		/// <summary>
		/// Reads from the server socket
		/// </summary>
		/// <returns>Server response minus acknowledge header</returns>
		private string readFromServer() {
			try {
				StringBuilder response = new StringBuilder();
				streamReader = new StreamReader(clientSockStream, System.Text.Encoding.ASCII);

				while (streamReader.Peek() != -1) {
					string chunk = Char.ConvertFromUtf32(streamReader.Read());
					response.Append(chunk);
				}

				Debug.WriteLine("Response from server " + response);
				response.Remove(0, acknowledgeText.Length);
				return response.ToString();
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
				throw e;
			}
		}

		/// <summary>
		/// Sends a command to the server
		/// </summary>
		/// <param name="command">Server command</param>
		private void writeToServer(string command) {
			Debug.WriteLine("Command to write to server is " + command);
			try {
				// send command to visionmate
				streamWriter.WriteLine(command);
				streamWriter.Flush();
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				Debug.WriteLine(e.StackTrace);
				throw e;
			}
		}

		/// <summary>
		/// Starts a 2D Scan
		/// </summary>
		internal void StartScan() {
			Debug.WriteLine("Issuing Start Scan command");
			communicateWithServer(startScan);
		}

		/// <summary>
		/// Gets the name of the rack product
		/// </summary>
		/// <returns>product name</returns>
		internal string GetCurrentProduct() {
			return communicateWithServer(getCurrentProduct);
		}

		/// <summary>
		/// Gets the unique rack ID
		/// </summary>
		/// <returns>Rack ID</returns>
		internal string GetRackID() {
			return communicateWithServer(getRackID);
		}

		/// <summary>
		/// Gets the number of unsuccessful reads
		/// </summary>
		/// <returns>Number of reads</returns>
		internal int GetNumberOfNoReads() {
			string reads = communicateWithServer(getNumberOfNoReads);
			return Int32.Parse(reads);
		}

		/// <summary>
		/// Gets the number of expected tubes
		/// </summary>
		/// <returns>Number of tubes</returns>
		internal int GetNumberOfTubes() {
			string tubes = communicateWithServer(getNumberOfTubes);
			return Int32.Parse(tubes);
		}

		/// <summary>
		/// Gets the number of successful reads
		/// </summary>
		/// <returns>Number of reads</returns>
		internal int GetNumberOfReads() {
			string reads = communicateWithServer(getNumberOfReads);
			return Int32.Parse(reads);
		}

		/// <summary>
		/// Gets the time it took to scan the rack
		/// </summary>
		/// <returns>Scan Time</returns>
		internal string GetDecodeTime() {
			return communicateWithServer(getDecodeTime);
		}

		/// <summary>
		/// Gets the bar code data from a 2d Scan
		/// </summary>
		/// <returns>Dictionary Object of barcode data</returns>
		internal Dictionary<string, string> GetBarcodeData() {
			Dictionary<String, String> scanData = new Dictionary<String, String>();
			Debug.WriteLine("Get scan data");

			string responseFromServer = communicateWithServer(getScanData);

			// Split up the results into tubes and their locations
			string[] responses = Regex.Split(responseFromServer, @"\,?([A-Z]\d\d)\,(No Tube|\d+)\,?");
			int i = 1;

			while (i < responses.Length) {
				if (!(responses[i + 1].Trim().StartsWith(noTubeText, StringComparison.InvariantCultureIgnoreCase))) {
					// add the location and tube ID
					scanData.Add(responses[i].Trim(), responses[i + 1].Trim());
				}
				i += 3;
			}
			return scanData;
		}

		/// <summary>
		/// Sets the number of tubes the scanner should expect to be in the rack
		/// </summary>
		/// <param name="numberOfTubes"></param>
		internal void SetNumberOfTubes(string numberOfTubes) {
			communicateWithServer(setExpectedNumberOfTubes + numberOfTubes);
		}

		internal void SetRackID(string id) {
			communicateWithServer(setRackID + id);
		}

		/// <summary>
		/// Determines if a rack is present.
		/// Will always return true after a Start Scan has been issued and the
		/// scan is not complete.
		/// </summary>
		/// <returns></returns>
		internal bool RackPresent() {
			string response = communicateWithServer(getRackPresent);
			if (response == "True") {
				Debug.WriteLine("Rack Found");
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Do not use, server support isn't good
		/// </summary>
		/// <param name="rackType"></param>
		internal void SetRackType(string rackType) {
			writeToServer(changeProduct + rackType);
		}

		/// <summary>
		/// Do not use, server support isn't good
		/// </summary>
		internal void EnableExpectedTubes() {
			Debug.WriteLine("Enable Expected Tubes");
			communicateWithServer(enabledExpectedTubes);
		}

		/// <summary>
		/// Do not use, server support isn't good
		/// </summary>
		/// <returns></returns>
		internal string GetHardwareStatus() {
			return communicateWithServer(getHardwareStatus);
		}
	}
}
