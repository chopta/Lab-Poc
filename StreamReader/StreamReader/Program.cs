using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
			WhyUsingDispose();
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
			var fileLocation = @"d:\largeFile\largeFile3.txt";

			const int bufferSize = 20000;
			var buffer = new char[bufferSize];
			var count = bufferSize;
			var fs = File.OpenRead(fileLocation);
			var sr = new System.IO.StreamReader(fs, Encoding.UTF8);

			string value = "";
			var sb = new StringBuilder(bufferSize);
			while (count > 0)
			{
				count = sr.Read(buffer, 0, bufferSize);
				sb.Append(buffer, 0, count);
				Console.WriteLine(sb.ToString());
				sb.Clear();
			}


			Console.ReadKey();
		}

		static void CreateLargeFileIncrementally(int mb)
		{

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
		}

		static void GetRandomNumber()
		{

		}

		static void DirectoryBrowsing()
		{
		}
	}
}
