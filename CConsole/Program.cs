using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codex;

namespace CConsole {
    class Program {
        static void Main(string[] args) {
            if (args.Length == 1) {
                Anatomizer nlp = new Anatomizer(args[0]);   //, Char.MinValue);

                Console.WriteLine("\nInput:\n" + nlp.Input);
                Console.WriteLine("\nFormatted:\n" + nlp.FormattedText);

                Console.WriteLine("\nMax Words: " + nlp.GetMaxWordCountOfSentences().ToString());
                Console.WriteLine("Total Words: " + nlp.GetTotalWordCount().ToString());
                Console.WriteLine("Longest Word Length: " + nlp.GetMaxWordLength().ToString());

                Console.WriteLine("\nLongest Words: " + String.Join<string>(", ", nlp.GetLongestWords(true)));

                Console.WriteLine("");
            }
        }
    }
}
