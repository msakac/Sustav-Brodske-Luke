using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msakac_zadaca_1.Aplikacija
{
    static class IspisPoruke
    {
        public static void Greska(string poruka)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(poruka);
            Console.ResetColor();
        }

        public static void Uspjeh(string poruka)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(poruka);
            Console.ResetColor();
        }

        public static void FatalnaGreska(string poruka)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(poruka);
            Console.ResetColor();
            Environment.Exit(0);
        }

        public static void PorukaKanala(string poruka)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(poruka);
            Console.ResetColor();
        }
    }
}
