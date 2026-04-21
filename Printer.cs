using System;
using System.Threading;

namespace OOP_Odev {
    public static class Printer {
        // 144Hz bir monitörde her frame arası ~7ms'dir. 
        // 60Hz bir monitörde ~16ms'dir.
        // akıcılık için hızı 5-7ms civarına çekelim (saniyede ~150 karakter)
        public static int fluencySpeed = 10;

        public static void Type(string text) {
            foreach (char c in text) {
                Console.Write(c);

                // idk if it's better than thread.sleep im new to c#
                var sw = System.Diagnostics.Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < fluencySpeed) {
                    // spin-wait logic
                }
            }
            Console.WriteLine();
        }
    }
}
