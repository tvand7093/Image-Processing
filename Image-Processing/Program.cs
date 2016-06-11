using System;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Diagnostics;
using System.Linq;

namespace ImageProcessing
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			const string key = "84cc0008b9614936ad5a7edfdec501d3";
			VisionServiceClient client = new VisionServiceClient(key);

			using(var stream = File.OpenRead("../../../main.png"))
			{
				var ocrResult = client.RecognizeTextAsync(stream).Result;

				foreach (var region in ocrResult.Regions)
				{
					foreach(var line in region.Lines)
					{
						foreach (var word in line.Words) {
							Console.Write (word.Text + " ");
						}
					}
				}
			}
			Console.Write ("\n");

			Console.WriteLine ("Done Processing...");
			Console.ReadKey();

		}
	}
}


