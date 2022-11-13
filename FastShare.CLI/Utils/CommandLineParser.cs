using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastShare.CLI.Utils
{
    internal class CommandLineParser
    {
        private CommandLineParser() { }

        public ShareMode Mode { get; private set; }

        public int Code { get; private set; }

        public string? FilePath { get; private set; } = null;

        public static CommandLineParser Parse(string[] args)
        {
            CommandLineParser parser = new CommandLineParser();

            if(args.Length == 0)
            {
                throw new ArgumentException("No argument supplied");
            }

            if (args[0] == "--recv")
            {
                if(args.Length == 3)
                {
                    if (args[1] == "--out")
                    {
                        var dirPath = args[2];
                        if(!Directory.Exists(dirPath))
                        {
                            throw new ArgumentException("Error: specified directory does not exist. Please create it.");
                        }

                        parser.FilePath = dirPath;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Error: Argument 2 must be --out. Use fastshare --help to see usage");
                    }
                }

                parser.Mode = ShareMode.RECEIVE;
            }
            else if (args[0] == "--send")
            {
                if(args.Length != 5)
                {
                    throw new ArgumentException("Send mode requires 5 arguments. Use fastshare --help to see usage");
                }

                for(int i = 1; i < args.Length; i++)
                {
                    switch(args[i])
                    {
                        case "--file":
                            if (!File.Exists(args[i+1]))
                            {
                                throw new FileNotFoundException();
                            }
                            parser.FilePath = args[i+1];
                            i++;
                            break;
                        case "--code":
                            string code = args[i+1];
                            if(code.Length != 4)
                            {
                                throw new ArgumentException("Code must be 5 characters length");
                            }
                            parser.Code = int.Parse(code);
                            i++;
                            break;
                        default:
                            throw new ArgumentException("unrecognized argument: " + args[i]);
                    }
                }

                parser.Mode = ShareMode.SEND;
            }
            else if (args[0] == "--help")
            {
                ShowUsage();
                Environment.Exit(0);
            }
            else
            {
                throw new ArgumentException("Error: argument at position 0 must be either --recv or --send. Use fastshare --help to see usage");
            }

            return parser;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("" +
                "FastShare CLI Usage\n\n" +
                "fastshare --recv [--out <output path>]\n" +
                "fastshare --send --code code --file <path to file>\n\n" +
                "  --recv\t\t\tEnter receive mode: shows up a code to give to your sender. No more arguments required\n" +
                "  --send\t\t\tEnter send mode. Please provide the code and the path to the file to send.\n" +
                "  --code code\t\t\tProvide the code that your receiver got.\n" +
                "  --file <path to file>\t\tEnter the file path." +
                "\n");
        }
    }
}
