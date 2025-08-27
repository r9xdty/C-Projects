//Öğrencinin not ortalamasını hesaplayan program
using System;
//Öğrencinin adını, soyadını ve numarasını al -string ve int
//3 Farklı dersin notunu al -double
//Not ortalamasını hesapla -double
//Harf notunu belirle -char Harf notu sistemi: 90-100 A, 80-89 B, 65-79 C, 50-64 D, 0-49 F
//Sonucu ekrana yazdır - bool
class Program
{
    static void Main()
    {
        //Öğrencinin adını, soyadını ve numarasını al
        Console.Write("Öğrencinin Adı: ");
        string ad = Console.ReadLine();
        Console.Write("Öğrencinin Soyadı: ");
        string soyad = Console.ReadLine();
        Console.Write("Öğrencinin Numarası: ");
        int numara = int.Parse(Console.ReadLine());

        //3 Farklı dersin notunu al
        Console.Write("1. Ders Notu: ");
        double not1 = double.Parse(Console.ReadLine());
        Console.Write("2. Ders Notu: ");
        double not2 = double.Parse(Console.ReadLine());
        Console.Write("3. Ders Notu: ");
        double not3 = double.Parse(Console.ReadLine());

        //Not ortalamasını hesapla
        double ortalama = (not1 + not2 + not3) / 3;

        //Harf notunu belirle
        char harfNotu;
        if (ortalama >= 90)
            harfNotu = 'A';
        else if (ortalama >= 80)
            harfNotu = 'B';
        else if (ortalama >= 65)
            harfNotu = 'C';
        else if (ortalama >= 50)
            harfNotu = 'D';
        else
            harfNotu = 'F';

        bool gectiMi = harfNotu != 'F';
        //Sonucu ekrana yazdır
        Console.WriteLine($"\nÖğrenci Bilgileri:");
        Console.WriteLine($"Adı: {ad}");
        Console.WriteLine($"Soyadı: {soyad}");
        Console.WriteLine($"Numarası: {numara}");
        Console.WriteLine($"Ortalama Notu: {ortalama:F2}");
        Console.WriteLine($"Harf Notu: {harfNotu}");
        Console.WriteLine($"Geçti Mi: {(gectiMi ? "Evet" : "Hayır")}");
    }
}