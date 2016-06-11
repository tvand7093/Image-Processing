using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionTests
{
    class Program
    {
        static void Main(string[] args)
        {
            const string key = "84cc0008b9614936ad5a7edfdec501d3";
            VisionServiceClient client = new VisionServiceClient(key);

            using(var stream = File.OpenRead("img.jpeg"))
            {
                var ocrResult = client.RecognizeTextAsync(stream).Result;

                Rectangle kellysRow = null;

                foreach (var region in ocrResult.Regions)
                {
                    if (kellysRow != null)
                    {
                        //we are going column by column...
                        foreach (var line in region.Lines)
                        {
                            if (kellysRow.Top == line.Rectangle.Top)
                            {
                                Debugger.Break();
                            }
                        }
                    }
                    foreach(var line in region.Lines)
                    {
                        if(kellysRow == null)
                        {
                            var isKelly = line.Words.Any(w =>
                            {
                                var lower = w.Text.ToLower();
                                return lower.Contains("kelly");
                            });

                            if (isKelly)
                            {
                                kellysRow = line.Rectangle;
                            }
                        }
                    }
                }
            }
            Console.ReadKey();

        }
    }
}
