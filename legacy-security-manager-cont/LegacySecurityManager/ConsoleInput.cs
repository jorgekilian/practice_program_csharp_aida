using System;

namespace LegacySecurityManager;

public class ConsoleInput : Input {
    public string Request(string requestMessage)
    {
        Console.WriteLine(requestMessage);
        return Console.ReadLine();
    }
}