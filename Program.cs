using System;
using System.Collections.Generic;
using System.Threading;

namespace OOP_Odev {
    internal class Program {
        static void Main(string[] args) {
            // seed
            Printer.Type("=== OOP Proje ===");
            Console.Write("Seed (Boşsa rastgele): ");
            string sIn = Console.ReadLine();
            int seed = int.TryParse(sIn, out int s) ? s : Guid.NewGuid().GetHashCode();
            Character.RNG = new Random(seed);
            Printer.Type($"[KADER] Seed: {seed}\n");

            // difficulty
            Printer.Type("1)Kolay 2)Orta 3)Zor 4)Cehennem");
            Console.Write("Seçim: ");
            var diff = DataManager.GetDifficulty(Console.ReadLine());

            // player creation
            Console.Write("\nAdın: ");
            string pName = Console.ReadLine();
            Printer.Type("1)Savaşçı 2)Büyücü");
            string pType = (Console.ReadLine() == "2") ? "Mage" : "Warrior";

            Character player = (pType == "Mage") ? (Character)new Mage(pName) : new Warrior(pName);
            DataManager.LoadPlayerStats(player, pType);

            // load enemies
            var enemies = DataManager.PrepareEnemies(diff.Waves);
            if (enemies.Count == 0) return;

            Console.Clear();
            Printer.Type($"{player.Name} maceraya başlıyor! Zorluk: {diff.Name}");

            // war loop
            foreach (var enemy in enemies) {
                if (!player.IsAlive()) break;
                Printer.Type($"\n--- RAKİP: {enemy.Name} ({enemy.GetType().Name}) ---");

                while (player.IsAlive() && enemy.IsAlive()) {
                    Printer.Type($"Durum -> Sen: {player.Health} HP | Rakip: {enemy.Health} HP");
                    Console.ReadLine(); // Hamle bekle

                    enemy.TakeDamage(player.Damage(true));
                    if (enemy.IsAlive()) {
                        Thread.Sleep(600);
                        player.TakeDamage(enemy.Damage());
                    }
                }
                if (player.IsAlive()) Printer.Type("[ZAFER] Rakip düştü, ilerliyorsun...");
            }

            Console.Clear();
            string finalMsg = player.IsAlive() ? diff.WinMsg : diff.LoseMsg;
            Printer.Type(player.IsAlive() ? "********** ARENA TEMİZLENDİ **********" : "---------- ELENDİN ----------");
            Printer.Type(finalMsg);

            Console.ReadKey();
        }
    }
}