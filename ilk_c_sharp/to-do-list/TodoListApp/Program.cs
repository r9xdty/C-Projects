using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // NuGet: Install-Package Newtonsoft.Json

// VS Code Console To-Do List Uygulaması
public class TodoItem
{
    public string Gorev { get; set; }
    public bool Tamamlandi { get; set; }
    
    public TodoItem(string gorev)
    {
        Gorev = gorev;
        Tamamlandi = false;
    }
}

public class TodoListApp
{
    private static List<TodoItem> gorevler = new List<TodoItem>();
    private static string dosyaYolu = "todolist.json";
    
    public static void Main()
    {
        // Kayıtlı görevleri yükle
        GorevleriYukle();
        
        // Ana menü döngüsü
        while (true)
        {
            Console.Clear();
            MenuGoster();
            
            Console.Write("\nSeçiminiz (1-6): ");
            string secim = Console.ReadLine();
            
            switch (secim)
            {
                case "1":
                    GorevleriListele();
                    break;
                case "2":
                    YeniGorevEkle();
                    break;
                case "3":
                    GorevTamamla();
                    break;
                case "4":
                    GorevSil();
                    break;
                case "5":
                    TamamlananlariSil();
                    break;
                case "6":
                    GorevleriKaydet();
                    Console.WriteLine("👋 Görüşürüz!");
                    return;
                default:
                    Console.WriteLine("❌ Geçersiz seçim!");
                    Console.ReadKey();
                    break;
            }
        }
    }
    
    static void MenuGoster()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║      📝 TO-DO LİSTESİ        ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.ResetColor();
        
        Console.WriteLine();
        Console.WriteLine("1️⃣  Görevleri Listele");
        Console.WriteLine("2️⃣  Yeni Görev Ekle");
        Console.WriteLine("3️⃣  Görevi Tamamla/Geri Al");
        Console.WriteLine("4️⃣  Görev Sil");
        Console.WriteLine("5️⃣  Tamamlananları Temizle");
        Console.WriteLine("6️⃣  Çıkış");
        
        // İstatistik göster
        int toplam = gorevler.Count;
        int tamamlanan = gorevler.Count(g => g.Tamamlandi);
        int kalan = toplam - tamamlanan;
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"📊 Toplam: {toplam} | ✅ Tamamlanan: {tamamlanan} | ⏳ Kalan: {kalan}");
        Console.ResetColor();
    }
    
    static void GorevleriListele()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("═══ 📋 GÖREV LİSTESİ ═══");
        Console.ResetColor();
        Console.WriteLine();
        
        if (gorevler.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📭 Henüz görev eklenmemiş!");
            Console.ResetColor();
        }
        else
        {
            for (int i = 0; i < gorevler.Count; i++)
            {
                TodoItem gorev = gorevler[i];
                string checkbox = gorev.Tamamlandi ? "[✓]" : "[ ]";
                
                // Tamamlanan görevler gri, aktifler beyaz
                if (gorev.Tamamlandi)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1:D2}. {checkbox} {gorev.Gorev}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{i + 1:D2}. {checkbox} {gorev.Gorev}");
                }
                Console.ResetColor();
            }
        }
        
        Console.WriteLine("\n⌨️  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
    
    static void YeniGorevEkle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("═══ ➕ YENİ GÖREV EKLE ═══");
        Console.ResetColor();
        Console.WriteLine();
        
        Console.Write("Görev adını girin: ");
        string gorevAdi = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(gorevAdi))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Boş görev eklenemez!");
            Console.ResetColor();
        }
        else
        {
            gorevler.Add(new TodoItem(gorevAdi));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ '{gorevAdi}' başarıyla eklendi!");
            Console.ResetColor();
            GorevleriKaydet(); // Otomatik kaydet
        }
        
        Console.WriteLine("\n⌨️  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
    
    static void GorevTamamla()
    {
        if (gorevler.Count == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📭 Görev bulunamadı!");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("═══ ✓ GÖREV TAMAMLA ═══");
        Console.ResetColor();
        Console.WriteLine();
        
        // Görevleri numaralı listele
        for (int i = 0; i < gorevler.Count; i++)
        {
            TodoItem gorev = gorevler[i];
            string durum = gorev.Tamamlandi ? "[✓]" : "[ ]";
            Console.WriteLine($"{i + 1}. {durum} {gorev.Gorev}");
        }
        
        Console.Write($"\nTamamlamak/Geri almak istediğiniz görev numarası (1-{gorevler.Count}): ");
        
        if (int.TryParse(Console.ReadLine(), out int secim) && 
            secim >= 1 && secim <= gorevler.Count)
        {
            TodoItem secilenGorev = gorevler[secim - 1];
            secilenGorev.Tamamlandi = !secilenGorev.Tamamlandi;
            
            string durum = secilenGorev.Tamamlandi ? "tamamlandı" : "aktif hale getirildi";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Görev {durum}!");
            Console.ResetColor();
            
            GorevleriKaydet(); // Otomatik kaydet
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Geçersiz numara!");
            Console.ResetColor();
        }
        
        Console.WriteLine("\n⌨️  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
    
    static void GorevSil()
    {
        if (gorevler.Count == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📭 Silinecek görev bulunamadı!");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("═══ 🗑️ GÖREV SİL ═══");
        Console.ResetColor();
        Console.WriteLine();
        
        // Görevleri listele
        for (int i = 0; i < gorevler.Count; i++)
        {
            TodoItem gorev = gorevler[i];
            string durum = gorev.Tamamlandi ? "[✓]" : "[ ]";
            Console.WriteLine($"{i + 1}. {durum} {gorev.Gorev}");
        }
        
        Console.Write($"\nSilmek istediğiniz görev numarası (1-{gorevler.Count}): ");
        
        if (int.TryParse(Console.ReadLine(), out int secim) && 
            secim >= 1 && secim <= gorevler.Count)
        {
            TodoItem silinecekGorev = gorevler[secim - 1];
            
            Console.Write($"'{silinecekGorev.Gorev}' görevini silmek istediğinizden emin misiniz? (E/H): ");
            string onay = Console.ReadLine()?.ToUpper();
            
            if (onay == "E" || onay == "EVET")
            {
                gorevler.RemoveAt(secim - 1);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Görev başarıyla silindi!");
                Console.ResetColor();
                GorevleriKaydet(); // Otomatik kaydet
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("❎ Silme işlemi iptal edildi.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Geçersiz numara!");
            Console.ResetColor();
        }
        
        Console.WriteLine("\n⌨️  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
    
    static void TamamlananlariSil()
    {
        var tamamlananlar = gorevler.Where(g => g.Tamamlandi).ToList();
        
        if (tamamlananlar.Count == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📭 Tamamlanmış görev bulunamadı!");
            Console.ResetColor();
            Console.ReadKey();
            return;
        }
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("═══ 🧹 TEMİZLE ═══");
        Console.ResetColor();
        Console.WriteLine();
        
        Console.WriteLine("Tamamlanmış görevler:");
        foreach (var gorev in tamamlananlar)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"• [✓] {gorev.Gorev}");
            Console.ResetColor();
        }
        
        Console.Write($"\n{tamamlananlar.Count} tamamlanmış görevi silmek istiyor musunuz? (E/H): ");
        string onay = Console.ReadLine()?.ToUpper();
        
        if (onay == "E" || onay == "EVET")
        {
            gorevler.RemoveAll(g => g.Tamamlandi);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ {tamamlananlar.Count} görev temizlendi!");
            Console.ResetColor();
            GorevleriKaydet(); // Otomatik kaydet
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("❎ Temizleme iptal edildi.");
            Console.ResetColor();
        }
        
        Console.WriteLine("\n⌨️  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
    
    static void GorevleriKaydet()
    {
        try
        {
            string json = JsonConvert.SerializeObject(gorevler, Formatting.Indented);
            File.WriteAllText(dosyaYolu, json);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Kaydetme hatası: {ex.Message}");
            Console.ResetColor();
        }
    }
    
    static void GorevleriYukle()
    {
        try
        {
            if (File.Exists(dosyaYolu))
            {
                string json = File.ReadAllText(dosyaYolu);
                gorevler = JsonConvert.DeserializeObject<List<TodoItem>>(json) ?? new List<TodoItem>();
            }
            else
            {
                // İlk çalıştırmada örnek görevler ekle
                gorevler.Add(new TodoItem("VS Code ile C# öğrenmek"));
                gorevler.Add(new TodoItem("To-Do List uygulaması yapmak"));
                gorevler.Add(new TodoItem("Git öğrenmek"));
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Yükleme hatası: {ex.Message}");
            Console.ResetColor();
            gorevler = new List<TodoItem>();
        }
    }
}

/* 
VS CODE KURULUM ADLARI:

1. .NET SDK'yı yükle: https://dotnet.microsoft.com/download
2. VS Code'da C# extensionı yükle
3. Terminal'de:
   dotnet new console -n TodoListApp
   cd TodoListApp
   dotnet add package Newtonsoft.Json
   
4. Program.cs'i bu kodla değiştir
5. Çalıştır:
   dotnet run
*/