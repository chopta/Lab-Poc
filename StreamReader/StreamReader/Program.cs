using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace StreamReader
{
	class Program
	{
		static void Main(string[] args)
		{
			//CreateLargeFile();
			//CreateLargeFileWithEncoding();
			//LoadLargeFile();
			//PartialLoadLargeFile();
			//WhyUsingDispose();
			//CreateLargeFileMb();
			//SocketMain();
			//PingASite();
			GetRandomNumber();
			//NotRandom();
			//Stream vs reader vs File vs Buffer vs start, end, flush, 
			//Socket
			//Ping
			//How to get random
		}

		static void CreateLargeFile()
		{
			var fileLocation = @"d:\largeFile\largeFile.txt";

			//Don't use stringBuilder
			var sb = new StringBuilder();
			for (var i = 0; i < 100; i++)
			{
				sb.Append("The Stream type exposes the following members.");
			}

			//StringWriter is for text manipulation within memory
			//var ms = new StringWriter(sb); 
			//ms.Encoding = Encoding.UTF8;

			//TextWriter can write to file
			using (var streamWriter = File.CreateText(fileLocation))
			{
				streamWriter.WriteLine(sb.ToString());
			}

			//FileStream: bytes that will be writen to a file. To write from text to stream, one will need StreamWriter which takes a sequence of chars with encoding and write.
			//TextWriter: Abstract class that handles writing text. An interface for all text as of //Stream: abstract to byte
			//TextReader: Abstract class that handles all the read text operation. 
			//StreamReader: read text from a stream of bytes in a special encoding format.

			//Uses the streamWriter
		}

		static void CreateLargeFileWithEncoding()
		{
			var fileLocation = @"d:\largeFile\largeFile3.txt";

			var sb = new StringBuilder();
			for (var i = 0; i < 200000; i++)
			{
				//sb.Append("3The Stream type exposes the following members."); //Unicode 10kb, UTF32 19Kb, UTF8 5Kb, ASCII 5kb
				sb.Append("字由石器時代記示演化而來，傳說係黃帝史官倉頡所造，但不能作準。現時考古發現，"); //Unicode 8, UTF32 15, UTF8 12, ASCII 4 (couldn't see)
			}
			using (var w = new StreamWriter(new FileStream(fileLocation, FileMode.OpenOrCreate, FileAccess.Write), Encoding.Unicode))
			{
				w.WriteLine(sb.ToString());
			}

		}

		static void WhyUsingDispose()
		{
			//for (var i = 0; i < 10; i++)
			//{
			//    var fileLocation = @"d:\largeFile\largeFile3.txt";

			//    const int bufferSize = 200000;
			//    var buffer = new char[bufferSize];
			//    var count = bufferSize;
			//    var fs = File.OpenRead(fileLocation);
			//    var sr = new System.IO.StreamReader(fs, Encoding.UTF8);

			//    string value = "";
			//    var sb = new StringBuilder(bufferSize);
			//    //while (count > 0)
			//    //{
			//    //    count = sr.Read(buffer, 0, bufferSize);
			//    //    sb.Append(buffer, 0, count);
			//    //    Console.WriteLine(sb.ToString());
			//    //    sb.Clear();
			//    //}



			//}

			for (var i = 0; i < 100; i++)
			{
				var result = ReadAll();
			}
			Console.ReadKey();
		}


		public static string ReadAll()
		{
			var filePath = @"d:\largeFile\largeFile2.txt";
			var sr = new System.IO.StreamReader(filePath);
			return sr.ReadToEnd();
		}

		public static string ReadAllDispose()
		{
			var filePath = @"d:\largeFile\largeFile2.txt";
			using (var sr = new System.IO.StreamReader(filePath))
			{
				return sr.ReadToEnd();
			}
		}

		static void CreateLargeFileMb()
		{
			int mb = 0;
			var input = Console.ReadLine();
			if (int.TryParse(input, out mb))
			{
				var fileLocation = @"d:\largeFile\" + mb + "MbCreation.txt";
				using (var ss = File.OpenWrite(fileLocation))
				using (var sw = new StreamWriter(ss))
				{
					var kbbuilder = new StringBuilder(1024);
					for (var i = 0; i < 1024; i++)
					{
						kbbuilder.Append("A");
					}
					var mbbuilder = new StringBuilder(1024, 1024 * 1000);
					for (var i = 0; i < 1000; i++)
					{
						mbbuilder.Append(kbbuilder.ToString());
					}
					var resultbuilder = new StringBuilder(1024 * 1000 * mb);
					for (var i = 0; i < mb; i++)
					{
						resultbuilder.Append(mbbuilder.ToString());
					}
					sw.Write(resultbuilder.ToString());
				}
			}
			Console.ReadKey();
		}

		static void LoadLargeFile()
		{
			var fileLocation = @"d:\largeFile\largeFile2.txt";

			using (var fs = File.OpenRead(fileLocation))
			using (var sr = new System.IO.StreamReader(fs, Encoding.UTF8))
			{
				string value = "";
				while ((value = sr.ReadToEnd()) != null)
				{
					Console.WriteLine(value);
				}
			}

			Console.ReadKey();
		}

		static void PartialLoadLargeFile()
		{
			var fileLocation = @"d:\largeFile\largeFile3.txt";

			const int bufferSize = 20000;
			var buffer = new char[bufferSize];
			var count = bufferSize;

			using (var fs = File.OpenRead(fileLocation))
			using (var sr = new System.IO.StreamReader(fs, Encoding.UTF8))
			{
				string value = "";
				var sb = new StringBuilder(bufferSize);
				while (count > 0)
				{
					count = sr.Read(buffer, 0, bufferSize);
					sb.Append(buffer, 0, count);
					Console.WriteLine(sb.ToString());
					sb.Clear();
				}
			}

			Console.ReadKey();
		}

		static void SendANetworkSocketGettingSiteData()
		{

		}

		static void PingASite()
		{
			var entry = Dns.GetHostEntry("www.google.com");
			if (entry.AddressList.Length > 0)
			{
				IPAddress address = entry.AddressList.First();
				var pingVar = new System.Net.NetworkInformation.Ping ();
				var reply = pingVar.Send(address);

				Console.WriteLine(reply.Status);
			}
		}

		static void GetRandomNumber()
		{
			//simple random
			var random = new Random(5);
			for(var i =0; i < 10; i++)
			{
				Console.WriteLine(random.Next(0, 100));
			}
			Console.ReadKey();
			//complex algorithm to calculate random RandomNumberGenerator
		}

		static void DirectoryBrowsing()
		{
		}

		static void MultiThreading()
		{
			//https://msdn.microsoft.com/en-us/library/system.random.aspx
			//http://www.codeproject.com/Articles/26148/Beginners-Guide-to-Threading-in-NET-Part-of-n
		}

		static void WebCrawler()
		{
			
		}

		static void ManipulateWithByte()
		{
			
		}

		static void NotRandom()
		{
			byte[] bytes1 = new byte[100];
			byte[] bytes2 = new byte[100];
			Random rnd1 = new Random();
			Random rnd2 = new Random();

			rnd1.NextBytes(bytes1);
			rnd2.NextBytes(bytes2);

			Console.WriteLine("First Series:");
			for (int ctr = bytes1.GetLowerBound(0);
				 ctr <= bytes1.GetUpperBound(0);
				 ctr++)
			{
				Console.Write("{0, 5}", bytes1[ctr]);
				if ((ctr + 1) % 10 == 0) Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine("Second Series:");
			for (int ctr = bytes2.GetLowerBound(0);
				 ctr <= bytes2.GetUpperBound(0);
				 ctr++)
			{
				Console.Write("{0, 5}", bytes2[ctr]);
				if ((ctr + 1) % 10 == 0) Console.WriteLine();
			}
			Console.ReadKey();
		}


		private static Socket ConnectSocket(string server, int port)
		{
			Socket s = null;
			IPHostEntry hostEntry = null;

			// Get host related information.
			hostEntry = Dns.GetHostEntry(server);

			// Loop through the AddressList to obtain the supported AddressFamily. This is to avoid 
			// an exception that occurs when the host IP Address is not compatible with the address family 
			// (typical in the IPv6 case). 
			foreach (IPAddress address in hostEntry.AddressList)
			{
				IPEndPoint ipe = new IPEndPoint(address, port);
				Socket tempSocket =
					new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

				tempSocket.Connect(ipe);

				if (tempSocket.Connected)
				{
					s = tempSocket;
					break;
				}
				else
				{
					continue;
				}
			}
			return s;
		}

		// This method requests the home page content for the specified server. 
		private static string SocketSendReceive(string server, int port)
		{
			string request = "GET / HTTP/1.1\r\nHost: " + server +
				"\r\nConnection: Close\r\n\r\n";
			Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
			Byte[] bytesReceived = new Byte[256];

			// Create a socket connection with the specified server and port.
			Socket s = ConnectSocket(server, port);

			if (s == null)
				return ("Connection failed");

			// Send request to the server.
			s.Send(bytesSent, bytesSent.Length, 0);

			// Receive the server home page content. 
			int bytes = 0;
			string page = "Default HTML page on " + server + ":\r\n";

			// The following will block until te page is transmitted. 
			do
			{
				bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
				page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
			}
			while (bytes > 0);

			return page;
		}

		public static void SocketMain()
		{
			string host = "www.google.ca";
			int port = 80;

			//if (args.Length == 0)
			//    // If no server name is passed as argument to this program,  
			//    // use the current host name as the default.
			//    host = "google.com";// Dns.GetHostName();
			//else
			//    host = args[0];

			string result = SocketSendReceive(host, port);
			Console.WriteLine(result);
			Console.ReadKey();
		}
	}
}
