using System;

namespace OOP_Odev {
    public abstract class Character {
        public static Random RNG = new Random();

        // ENCAPSULATION
        private string _name;
        private int _health;
        private int _critChance;
        private int _minDamage;
        private int _maxDamage;

        // ENCAPSULATION
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = Math.Max(0, value); }
        public int CritChance { get => _critChance; set => _critChance = value; }
        public int MinDamage { get => _minDamage; set => _minDamage = value; }
        public int MaxDamage { get => _maxDamage; set => _maxDamage = value; }

        public Character(string name, int health, int critChance) {
            this.Name = name;
            this.Health = health;
            this.CritChance = critChance;
        }

        protected int CritControl(int baseDamage, bool isPlayer) {
            // static RNG kullanarak bellek optimizasyonu sağladık
            if (RNG.Next(1, 101) <= CritChance) {
                if (isPlayer) {
                    Printer.Type("!!! KRİTİK VURDUN !!!");
                } else {
                    Printer.Type("!!! KRİTİK VURUŞ YEDİN !!!");
                }
                return baseDamage * 2;
            }
            return baseDamage;
        }

        public virtual int Damage(bool isPlayer = false) {
            int baseDamage = RNG.Next(MinDamage, MaxDamage + 1);
            return CritControl(baseDamage, isPlayer);
        }

        public void TakeDamage(int amount) {
            this.Health -= amount;
            Printer.Type($"{Name} | {amount} hasar aldı! Kalan Can: {Health}");
        }

        public bool IsAlive() => Health > 0;
    }
}