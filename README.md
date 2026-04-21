---
# 🛡️ OOP Proje Ödevi: Teknik Mimari Dokümantasyonu

Bu proje, harici bir veri kaynağından (`Data.json`) beslenen, tamamen **Nesne Yönelimli Programlama (OOP)** prensipleriyle geliştirilmiş bir RPG savaş simülasyonudur.

## 🏗️ Sınıf Yapıları ve İlişkiler

Projedeki sınıflar, "Sorumlulukların Ayrılması" (Separation of Concerns) ilkesine göre tasarlanmıştır:

### 1. [Karakter.cs](./Karakter.cs) - Temel Soyut Sınıf (Base Class)
Projenin temel iskeletidir. `abstract` olarak tanımlanmıştır, yani doğrudan bir "Karakter" oluşturulamaz; mutlaka bir alt sınıfa türetilmelidir.

* **Encapsulation (Kapsülleme):** Karakter istatistikleri `private` alanlarda (`_health`, `_name` vb.) tutulur ve kontrollü `Property` yapılarıyla dışarı açılır.
* **Polymorphism (Çok Biçimlilik):** `Damage()` metodu `virtual` tanımlanarak alt sınıfların bu davranışı değiştirmesine olanak tanır.
* **Bellek Yönetimi:** `static Random RNG` kullanımıyla, her karakter için yeni bir rastgele sayı üreticisi oluşturulması engellenmiş, sistem kaynağı ve "seed" tutarlılığı optimize edilmiştir.

```csharp
public abstract class Character {
    private int _health;
    public int Health { get => _health; set => _health = Math.Max(0, value); } // Negatif canı engeller
    public virtual int Damage(bool isPlayer = false) { ... }
}
```
2. Race.cs - Türetilmiş Sınıflar (Inheritance)
Character sınıfından miras alan somut sınıflardır.

Özelleşmiş Yapılar: Warrior, Mage ve Boss sınıfları, karakter hiyerarşisini oluşturur.

Constructor Chaining: Alt sınıflar, base() anahtar kelimesiyle üst sınıfın yapıcı metodunu tetikler.

```csharp
public class Warrior : Character {
    public Warrior(string name) : base(name, 0, 0) { } // Veri JSON'dan yüklenecek
}
```
3. DataManager.cs - Veri ve Nesne Fabrikası
JSON verilerini işleyen ve Reflection kullanarak nesne üreten yönetim birimidir.

Dinamik Nesne Üretimi: Activator.CreateInstance kullanılarak, JSON'daki metin değerinden gerçek bir C# nesnesi oluşturulur.

Data Mapping: Harici JSON verileri, LoadPlayerStats ve Setup metodlarıyla karakter nesnelerine enjekte edilir.

4. Program.cs - Oyun Motoru ve Kontrol Akışı
Oyunun ana döngüsünü, kullanıcı etkileşimini ve "Wave" mantığını yönetir.

🧠 Uygulanan Nesne Tabanlı Programlama (OOP) Kavramları


💎 1. Kalıtım (Inheritance)

Tüm karakter tipleri Character sınıfından miras alır. Bu sayede TakeDamage veya IsAlive gibi ortak metotlar her sınıf için tekrar yazılmamış, merkezi bir yerden yönetilmiştir.

💎 2. Çok Biçimlilik (Polymorphism)

Damage(bool isPlayer) metodu, vuranın kim olduğuna göre farklı davranışlar sergiler.

```csharp
// Character.cs içindeki Polymorphic yapı
if (isPlayer) {
    Printer.Type("!!! KRİTİK VURDUN !!!"); // Oyuncuya özel geri bildirim
} else {
    Printer.Type("!!! KRİTİK VURUŞ YEDİN !!!"); // Düşmana özel geri bildirim
}
```

💎 3. Kapsülleme (Encapsulation)

Math.Max(0, value) kontrolü ile can değerinin negatif bir sayıya düşmesi Property seviyesinde engellenmiştir. Veri güvenliği sağlanmıştır.

💎 4. Soyutlama (Abstraction)

Program.cs içindeki foreach (var enemy in enemies) döngüsü, düşmanın teknik detaylarıyla ilgilenmez. Sadece soyutlanmış Damage() ve TakeDamage() arayüzlerini kullanarak savaşı yönetir.


⚙️ Teknik Detaylar

Seed Sistemi: Character.RNG = new Random(seed); satırı ile rastgelelik kontrol altına alınabilir ve savaşlar tekrarlanabili.

Typewriter Effect: Printer.cs sınıfı, metinlerin kullanıcıya daha organik bir şekilde sunulmasını sağlar.

Data-Driven Design: Oyunun zorluk ayarları, dalga sayısı ve bitiş mesajları tamamen Data.json içinden yönetilmektedir.

Bu proje, OOP mimarisinin gücünü ve verinin koddan ayrıştırılmasının avantajlarını göstermek için hazırlanmıştır.


---
