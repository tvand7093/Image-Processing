using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    class ImageCompiler
    {
        public bool Compile(string programContents, string outputName)
        {
            //create source code AST from string read from OCR
            var tree = CSharpSyntaxTree.ParseText(programContents);

            //get global assembly cache directory so we can reference libraries
            var GAC = Path.GetDirectoryName(typeof(object).Assembly.Location);

            //create references to base libraries
            var baseReferences = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(Path.Combine(GAC, "mscorlib.dll")),
                MetadataReference.CreateFromFile(Path.Combine(GAC, "System.dll")),
                MetadataReference.CreateFromFile(Path.Combine(GAC, "System.Core.dll"))
            };

            //we have a program hopefully, so try to compile it.
            var result = CSharpCompilation.Create(outputName) //create a new assembly
                .AddSyntaxTrees(tree) //using the specified syntax trees
                .AddReferences(baseReferences) //and the set of specific references
                .Emit($"{outputName}.exe"); //then emit it to a file on disk.

            //now check if the binary built correctly...
            return result.Success;
        }
    }
}
