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
                Anatomizer nlp = new Anatomizer(args[0], '\0');
                
                Console.WriteLine(nlp.FormattedText);
            }
        }
    }
}
