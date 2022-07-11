

using System.Drawing;
using Pastel;

namespace verCode.console
{

    internal static class Logger
    {
       private static readonly Color VERB = Color.Gray;
       private static readonly Color INFO = Color.LightBlue;
       private static readonly Color WARN = Color.Yellow;
       private static readonly Color ERROR = Color.DarkRed;
    
       public static void Info(string className, string methodName, string message, object? args = null)
        {
            Console.WriteLine($"[Info] [{className}] [{methodName}] {message}".Pastel(INFO), args);
        }

        public static void Warn(string className, string methodName, string message, object? args = null)
        {
            Console.WriteLine($"[Warn] [{className}] [{methodName}] {message}".Pastel(WARN), args);
        }

        public static void Error(string className, string methodName, string message, object? args = null)
        {
            Console.WriteLine($"[Error] [{className}] [{methodName}] {message}".Pastel(ERROR), args = null);
        }

        public static void Verb(string className, string methodName, string message, object? args = null)
        {
            Console.WriteLine($"[Verb] [{className}] [{methodName}] {message}".Pastel(VERB), args);
        }
    }
}
