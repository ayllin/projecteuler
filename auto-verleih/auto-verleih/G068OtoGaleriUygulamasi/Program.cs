using System;
using System.Data;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace G068OtoGaleriUygulamasi
{

    internal class Program
    {

        static Galeri otogaleri = new Galeri();

        static void Main(string[] args)
        {
            SahteVeri();
            Uygulama();
        }
        static void Uygulama()
        {
            Menu();
            int sayac = 0;
            while (true)
            {
                string secim = SecimAl();

                switch (secim)
                {
                    case "1":
                    case "K":
                        ArabaKirala();
                        break;
                    case "2":
                    case "T":
                        ArabaTeslimAl();
                        break;
                    case "3":
                    case "R":
                        ArabalariListele("Kirada");
                        break;
                    case "4":
                    case "M":
                        ArabalariListele("Galeride");
                        break;
                    case "5":
                    case "A":
                        TumArabalariListele();
                        break;
                    case "6":
                    case "I":
                        Kiralamaİptali();
                        break;
                    case "7":
                    case "Y":
                        ArabaEkle();
                        break;
                    case "8":
                    case "S":
                        ArabaSil();
                        break;
                    case "9":
                    case "G":
                        BilgileriGoster();
                        break;
                    case "X":
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine();
            }
        }
        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Galeri Otomasyon                   ");
            Console.WriteLine("1- Araba Kirala (K)                ");
            Console.WriteLine("2- Araba Teslim Al (T)             ");
            Console.WriteLine("3- Kiradaki Arabaları Listele (R)  ");
            Console.WriteLine("4- Galerideki Arabaları Listele (M)");
            Console.WriteLine("5- Tüm Arabaları Listele (A)       ");
            Console.WriteLine("6- Kiralama İptali (I)             ");
            Console.WriteLine("7- Araba Ekle (Y)                  ");
            Console.WriteLine("8- Araba Sil (S)                   ");
            Console.WriteLine("9- Bilgileri Göster (G)            ");
            Console.WriteLine();
        }
        static void ArabaKirala()
        {
            Console.WriteLine("-Araba Kirala-");
            Console.WriteLine();
            if (otogaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride hiç araba yok.");
            }
            if (otogaleri.GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Tüm araçlar kirada.");
            }
            string plaka;
            while (true)
            {
                plaka = PlakaSorgula("Kiralanacak arabanın plakası: ");
                if (plaka != "X")
                {
                    switch (otogaleri.DurumSorgula(plaka))
                    {
                        case "Kirada":
                            Console.WriteLine("Araba şu anda kirada. Farklı araba seçiniz.");
                            break;
                        case "ArabaYok":
                            Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                            break;
                        default:
                            goto label122;
                    }
                }
                else
                    break;
            }
        label122:
            Console.Write("Kiralama suresi: ");
            int sure = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(plaka + " plakalı araba " + sure + " saatliğine kiralandı.");
            otogaleri.ArabaKirala(plaka, sure);
        }
        static void ArabaSil()
        {
            Console.WriteLine("-Araba Sil-");
            Console.WriteLine();

            if (otogaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride silinecek araba yok.");
            }
            string plaka;
            while (true)
            {
                plaka = PlakaSorgula("Silmek istediğiniz arabanın plakasını giriniz: ");
                if (!(plaka == "X"))
                {
                    if (otogaleri.DurumSorgula(plaka) == "ArabaYok")
                        Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    else if (otogaleri.DurumSorgula(plaka) == "Kirada")
                        Console.WriteLine("Araba kirada olduğu için silme işlemi gerçekleştirilemedi.");
                    else
                        goto label_177;
                }
                else
                    break;
            }
        label_177:
            otogaleri.ArabaSil(plaka);
            Console.WriteLine();
            Console.WriteLine("Araba silindi.");
        }
        static void ArabaTeslimAl()
        {
            Console.WriteLine("-Araba Teslim Al-");
            Console.WriteLine();
            if (otogaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride hiç araba yok.");
            }
            if (otogaleri.GaleridekiAracSayisi == 0)
            {
                Console.WriteLine("Kirada hiç araba yok.");
            }
            string plaka;
            while (true)
            {
                plaka = PlakaSorgula("Teslim edilecek arabanın plakası: ");
                if (plaka != "X")
                {
                    switch (otogaleri.DurumSorgula(plaka))
                    {
                        case "Galeride":
                            Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                            break;
                        case "ArabaYok":
                            Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                            break;
                        default:
                            goto AltaGit;
                    }
                }
                else
                    break;
            }
        AltaGit:
            otogaleri.ArabaTeslimAl(plaka);
            Console.WriteLine();
            Console.WriteLine("Araba galeride beklemeye alındı.");
        }
        static void ArabalariListele(string durum)
        {
            switch (durum)
            {
                case "Kirada":
                    Console.WriteLine("-Kiradaki Arabalar-");
                    break;
                case "Galeride":
                    Console.WriteLine("-Galerideki Arabalar-");
                    break;
                default:
                    Console.WriteLine("-Tüm Arabalar-");
                    break;
            }
            Console.WriteLine();
            ArabaListele(otogaleri.ArabaListesiGetir(durum));
        }
        static void ArabaListele(List<Araba> liste)
        {
            if (liste.Count == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
            }
            else
            {
                Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum");
                Console.WriteLine("".PadRight(70, '-'));
                foreach (Araba araba in liste)
                    Console.WriteLine(araba.Plaka.PadRight(14) + araba.Marka.PadRight(12) + araba.KiralamaBedeli.ToString().PadRight(12) + araba.AracTipi.ToString().PadRight(12) + araba.ToplamKiralamaSayisi.ToString().PadRight(12) + araba.Durum);
            }
        }
        static void TumArabalariListele()
        {
            Console.WriteLine("-Tüm Arabalar-");
            Console.WriteLine();
            Console.WriteLine("Plaka".PadRight(13) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum".PadRight(8));
            Console.WriteLine("----------------------------------------------------------------------");

            otogaleri.TumArabaListesi();
        }
        static void Kiralamaİptali()
        {
            Console.WriteLine("-Kiralama İptali-");
            Console.WriteLine();
            if (otogaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada araba yok.");
            }
            string plaka;
            while (true)
            {
                plaka = PlakaSorgula("Kiralaması iptal edilecek arabanın plakası: ");
                if (plaka != "X")
                {
                    switch (otogaleri.DurumSorgula(plaka))
                    {
                        case "Galeride":
                            Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                            break;
                        case "ArabaYok":
                            Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                            break;
                        default:
                            goto AltaGit1;
                    }
                }
                else
                    break;
            }
        AltaGit1:
            otogaleri.Kiralamaİptali(plaka);
            Console.WriteLine();
            Console.WriteLine("İptal gerçekleştirildi.");
        }

        static void ArabaEkle()
        {
            Console.WriteLine("-Araba Ekle-");
            Console.WriteLine();
            string plaka;
            while (true)
            {
                plaka = PlakaSorgula("Plaka: ");
                if (!(plaka == "X"))
                {
                    if (otogaleri.DurumSorgula(plaka) == "Kirada" || otogaleri.DurumSorgula(plaka) == "Galeride")
                        Console.WriteLine("Aynı plakada araba mevcut. Girdiğiniz plakayı kontrol edin.");
                    else
                        goto BurayaDon5;
                }
                else
                    break;
            }
        BurayaDon5:
            string marka;
            while (true)
            {
                int sayi;
                Console.Write("Marka: ");
                marka = Console.ReadLine().ToUpper();
                bool sonuc = int.TryParse(marka, out sayi);
                if (sonuc == true)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                }
                else
                {
                    break;
                }
            }
            float kiralamaBedeli;
            while (true)
            {
                Console.Write("Kiralama bedeli: ");
                string giris = Console.ReadLine();
                bool sonuc = float.TryParse(giris, out kiralamaBedeli);
                if (sonuc != true)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin. ");
                }
                else
                {
                    break;
                }

            }
            Console.WriteLine("Araç tipi: ");
            string aTipi = " ";
            Console.WriteLine("SUV için 1");
            Console.WriteLine("Hatchback için 2");
            Console.WriteLine("Sedan için 3");
            while (true)
            {
            Secim:
                Console.Write("Araç tipi: ");
                string arabaTipiSecim = Convert.ToString(Console.ReadLine());

                if (arabaTipiSecim == "1")
                {
                    aTipi = "SUV";

                }
                else if (arabaTipiSecim == "2")
                {
                    aTipi = "Hatchback";

                }
                else if (arabaTipiSecim == "3")
                {
                    aTipi = "Sedan";

                }
                else
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    goto Secim;
                }
                Console.WriteLine("Araba başarılı bir şekilde eklendi.");
                break;

            }
            otogaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aTipi);

        }

        static string SecimAl()
        {
            string karakterler = "123456789KTRMAIYSGX";
            int sayac = 0;
            while (true)
            {
                sayac++;
                Console.Write("Seçiminiz: ");
                string giris = Console.ReadLine().ToUpper();
                int index = karakterler.IndexOf(giris);

                Console.WriteLine();

                if (giris.Length == 1 && index >= 0)
                {
                    return giris;
                }
                else
                {
                    if (sayac == 10)
                    {
                        Environment.Exit(0);
                    }
                    Console.WriteLine("Hatalı işlem gerçekleştirildi. Tekrar deneyin.");

                }
                Console.WriteLine();
            }
        }
        static void BilgileriGoster()
        {
            Console.WriteLine("-Galeri Bilgileri-");
            Console.WriteLine("Toplam araba sayısı:" + otogaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki araba sayısı:" + otogaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen araba sayısı:" + otogaleri.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araba kiralama süresi:" + otogaleri.ToplamAracKiralamaSuresi);
            Console.WriteLine("Toplam araba kiralama adedi:" + otogaleri.ToplamAracKiralamaAdedi);
            Console.WriteLine("Ciro:" + otogaleri.Ciro);

        }
        public static string PlakaSorgula(string mesaj)
        {
            while (true)
            {
                try
                {
                    Console.Write(mesaj);
                    string upper = Console.ReadLine().ToUpper();
                    if (upper == "X")
                        return "X";
                    return PlakaMi(upper) ? upper : throw new Exception("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static bool PlakaMi(string veri)
        {
            int result;
            return veri.Length > 6 && veri.Length < 10 && int.TryParse(veri.Substring(0, 2), out result) && HarfMi(veri.Substring(2, 1)) && (veri.Length == 7 && int.TryParse(veri.Substring(3), out result) || veri.Length < 9 && HarfMi(veri.Substring(3, 1)) && int.TryParse(veri.Substring(4), out result) || HarfMi(veri.Substring(3, 2)) && int.TryParse(veri.Substring(5), out result));
        }
        static bool HarfMi(string veri)
        {
            veri = veri.ToUpper();
            for (int index = 0; index < veri.Length; ++index)
            {
                int num = (int)veri[index];
                if (num < 65 || num > 90)
                    return false;
            }
            return true;
        }
        static void SahteVeri()
        {
            otogaleri.ArabaEkle("09ADT252", "FİAT", 500, "SEDAN");
            otogaleri.ArabaEkle("34ACT400", "OPEL", 100, "SUV");
            otogaleri.ArabaEkle("35ADT250", "VOLSWAGEN", 1000, "HATCHBACK");
            otogaleri.ArabaEkle("81CN1881", "VOLVO", 100, "HATCHBACK");
        }
    }
}





