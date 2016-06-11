using Microsoft.ProjectOxford.Vision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class ImageReader
    {
        public async Task<string> GetText(string fileName)
        {
            const string key = "84cc0008b9614936ad5a7edfdec501d3";
            //create the OCR client
            VisionServiceClient client = new VisionServiceClient(key);

            //keep track of our program input from OCR image processing
            StringBuilder program = new StringBuilder();

            //pull our main program from a local image file.
            using (var stream = File.OpenRead(fileName))
            {
                //send the image for processing
                var ocrResult = await client.RecognizeTextAsync(stream);

                //now enumerate over results to build up program.
                foreach (var region in ocrResult.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            //print every word/symbol followed by a space to our string builder.
                            program.Append(word.Text + " ");
                        }
                    }
                }
            }
            return program.ToString();
        }
    }
}
