using System;

namespace OOP_Odev {
    public class Warrior : Character {
        // Constructor: JSON'dan veriler gelene kadar başlangıçta 0 değerlerini gönderiyoruz
        public Warrior(string name) : base(name, 0, 0) { }

        public override int Damage(bool isPlayer = false) => base.Damage(isPlayer);
    }

    public class Mage : Character {
        public Mage(string name) : base(name, 0, 0) { }
        public override int Damage(bool isPlayer = false) => base.Damage(isPlayer);
    }

    public class BossWarrior : Character {
        public BossWarrior(string name) : base(name, 0, 0) { }
        public override int Damage(bool isPlayer = false) => base.Damage(isPlayer);
    }

    public class BossMage : Character {
        public BossMage(string name) : base(name, 0, 0) { }
        public override int Damage(bool isPlayer = false) => base.Damage(isPlayer);
    }

    public class Default : Character {
        public Default(string name) : base(name, 0, 0) { }
        public override int Damage(bool isPlayer = false) => base.Damage(isPlayer);
    }
}