using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace OOP_Odev {
    public class DifficultySetting {
        public string Name { get; set; }
        public int Waves { get; set; }
        public string WinMsg { get; set; }
        public string LoseMsg { get; set; }
    }

    public class GameSettings {
        public Dictionary<string, DifficultySetting> Difficulties { get; set; }
    }

    public class StatsInfo {
        public int Health { get; set; }
        public int CritChance { get; set; }
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
    }

    public class EnemyInfo {
        public string Name { get; set; }
        public string Race { get; set; }
        public int Health { get; set; }
        public int CritChance { get; set; }
        public int MinDmg { get; set; }
        public int MaxDmg { get; set; }
    }

    public class EnemyData {
        public GameSettings Settings { get; set; }
        public Dictionary<string, StatsInfo> PlayerStats { get; set; }
        public List<EnemyInfo> Enemies { get; set; }
        public List<EnemyInfo> Bosses { get; set; }
    }

    public static class DataManager {
        private static string FilePath = "Data.json";

        private static EnemyData GetRawData() {
            try {
                if (!File.Exists(FilePath)) return null;
                string json = File.ReadAllText(FilePath);
                return new JavaScriptSerializer().Deserialize<EnemyData>(json);
            } catch { return null; }
        }

        public static DifficultySetting GetDifficulty(string choice) {
            var data = GetRawData();
            if (data?.Settings?.Difficulties != null && data.Settings.Difficulties.ContainsKey(choice))
                return data.Settings.Difficulties[choice];

            return new DifficultySetting { Name = "Normal", Waves = 1, WinMsg = "Kazandın!", LoseMsg = "Öldün." };
        }

        public static void LoadPlayerStats(Character player, string raceName) {
            var data = GetRawData();
            if (data?.PlayerStats != null && data.PlayerStats.ContainsKey(raceName)) {
                var s = data.PlayerStats[raceName];
                player.Health = s.Health;
                player.CritChance = s.CritChance;
                player.MinDamage = s.MinDmg;
                player.MaxDamage = s.MaxDmg;
            }
        }

        public static List<Character> PrepareEnemies(int waveAmount) {
            List<Character> list = new List<Character>();
            var data = GetRawData();
            if (data == null) return list;

            for (int w = 0; w < waveAmount; w++) {
                for (int i = 0; i < 2; i++)
                    list.Add(Setup(data.Enemies[Character.RNG.Next(data.Enemies.Count)]));

                list.Add(Setup(data.Bosses[Character.RNG.Next(data.Bosses.Count)]));
            }
            return list;
        }

        private static Character Setup(EnemyInfo info) {
            Type t = Type.GetType("OOP_Odev." + info.Race);
            Character c = (t != null) ? (Character)Activator.CreateInstance(t, info.Name) : new Default(info.Name);
            c.Health = info.Health;
            c.CritChance = info.CritChance;
            c.MinDamage = info.MinDmg;
            c.MaxDamage = info.MaxDmg;
            return c;
        }
    }
}