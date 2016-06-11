using System;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Threading.Tasks;

namespace ImageProcessing
{
	class MainClass
	{
		public static void Main (string[] args)
        {
            ImageReader reader = new ImageReader();
            ImageCompiler compiler = new ImageCompiler();

            //build HI console app
            BuildAndRunHiImage(reader, compiler).Wait();

            //build Hi from Roslyn and OCR app
            BuildAndRunHiFromOCRImage(reader, compiler).Wait();

            Console.Write("\n");
            Console.WriteLine("Done Processing...");
            Console.ReadKey();
        }

        private static async Task BuildAndRunHiImage(ImageReader reader, ImageCompiler compiler)
        {
            //process main image.
            var hiProgram = await reader.GetText("../../../main.png");
            var compileResult = compiler.Compile(hiProgram, "Hi");

            if (compileResult)
            {
                //call the library
                var running = Process.Start("Hi.exe");
                running.WaitForExit();
            }
        }

        private static async Task BuildAndRunHiFromOCRImage(ImageReader reader, ImageCompiler compiler)
        {
            //process main image.
            var ocrProgram = await reader.GetText("../../../ocr.png");
            var compileResult = compiler.Compile(ocrProgram, "OCRAndRoslyn");

            if (compileResult)
            {
                //call the library
                var running = Process.Start("OCRAndRoslyn.exe");
                running.WaitForExit();
            }
        }
    }
}


