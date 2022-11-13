using FastShare.CLI.Utils;
using FastShare.Core;

Console.WriteLine("FastShare CLI");
Console.WriteLine();

var parsed = CommandLineParser.Parse(args);

long fileLength = -1;

switch(parsed.Mode)
{
    case ShareMode.RECEIVE:
        Console.WriteLine("Entering receive mode...");

        var code = FastShareCore.Instance.RequestCode().Result;

        Console.WriteLine("\nCode: " + code + "\n");
        Console.WriteLine("Waiting for file...");

        FastShareCore.Instance.DownloadStarted += Instance_DownloadStarted;
        FastShareCore.Instance.DownloadProgress += Instance_DownloadProgress;

        var success = FastShareCore.Instance.ReceiveFile(parsed.FilePath).Result;

        if(!success)
        {
            Console.WriteLine("\nError ! Something wrong happened. Please try again");
        }
        else
        {
            Console.WriteLine("\nSuccess !");
        }

        break;
    case ShareMode.SEND:
        Console.WriteLine("Entering send mode...");

        FastShareCore.Instance.SendStarted += Instance_SendStarted;
        FastShareCore.Instance.SendProgress += Instance_SendProgress;

        fileLength = new FileInfo(parsed.FilePath).Length;

        var result = FastShareCore.Instance.SendFile(parsed.FilePath, parsed.Code).Result;

        if(result)
        {
            Console.WriteLine("\nFile sent successfully !");
        }
        else
        {
            Console.WriteLine("\nError: somthing went wrong. Please verify the code and try again");
        }
        break;
}

void Instance_DownloadProgress(int progress)
{
    Console.Write($"\rProgress: {progress * 100 / fileLength}%");
}

void Instance_DownloadStarted(FastShare.Core.Model.FastShareFileInfo obj)
{
    fileLength = obj.Length;
    Console.WriteLine($"Receiving \"{obj.Title}\" of size {obj.Length}...");
}

void Instance_SendProgress(int progress)
{
    Console.Write($"\rProgress: {progress * 100 / fileLength}%");
}

void Instance_SendStarted()
{
    Console.WriteLine($"Sending {new FileInfo(parsed.FilePath).Name} of size {fileLength}...");
}