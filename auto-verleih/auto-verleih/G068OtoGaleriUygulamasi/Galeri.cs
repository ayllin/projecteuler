using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace G068OtoGaleriUygulamasi
{
    internal class Galeri
    {
        public List<Araba> Arabalar = new List<Araba>();

        public int ToplamAracSayisi
        {
            get
            {
                return this.Arabalar.Count;
            }
        }
        public int KiradakiAracSayisi
        {
            get
            {
                int adet = 0;

                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Kirada")
                    {
                        adet++;
                    }
                }
                return adet;
            }
        }
        public int GaleridekiAracSayisi
        {
            get
            {
                int adet = 0;

                foreach (Araba item in Arabalar)
                {
                    if (item.Durum == "Galeride")
                    {
                        adet++;
                    }
                }
                return adet;
            }
        }

        public int ToplamAracKiralamaSuresi
        {
            get
            {
                int toplam = 0;

                foreach (Araba item in Arabalar)
                {
                    toplam += item.ToplamKiralamaSuresi;
                }
                return toplam;
            }
        }
        public int ToplamAracKiralamaAdedi
        {
            get
            {
                int toplam = 0;

                foreach (Araba item in Arabalar)
                {
                    toplam += item.ToplamKiralamaSayisi;
                }
                return toplam;
            }
        }
        public float Ciro => this.Arabalar.Sum<Araba>((Func<Araba, float>)(a => (float)a.ToplamKiralamaSuresi * a.KiralamaBedeli));

        public void ArabaKirala(string plaka, int sure)
        {

            Araba a = null;

            foreach (Araba item in Arabalar)
            {
                if (plaka == item.Plaka)
                {
                    a = item;
                }
            }
            if (a != null)
            {
                a.Durum = "Kirada";
                a.KiralamaSureleri.Add(sure);

            }
        }
        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, string aTipi)
        {

            //parametreden alinan bilgiler ile yeni bir araba nesnemiz olusturulacak
            //bu olusan arabayi arabalar listesine ekleyecegiz.

            Araba a = new Araba(plaka, marka, kiralamaBedeli, aTipi);
            this.Arabalar.Add(a);


        }
        public void TumArabaListesi()
        {

            if (Arabalar.Count == 0)
            {
                Console.WriteLine("Gösterilecek Araba Yok");
            }
            else
            {
                foreach (Araba item in Arabalar)
                {
                    Console.WriteLine(item.Plaka.PadRight(13) + item.Marka.PadRight(12) + item.KiralamaBedeli.ToString().PadRight(12) + item.AracTipi.PadRight(12) + item.ToplamKiralamaSayisi.ToString().PadRight(12) + item.Durum.PadRight(8));

                }
            }
        }
        public List<Araba> ArabaListesiGetir(string durum)
        {
            List<Araba> arabaList = this.Arabalar;
            if (durum == "Kirada" || durum == "Galeride")
                arabaList = this.Arabalar.Where<Araba>((Func<Araba, bool>)(a => a.Durum == durum)).ToList<Araba>();
            return arabaList;
        }
        public void ArabaSil(string plaka)
        {
            Araba araba = this.Arabalar.Where<Araba>((Func<Araba, bool>)(x => x.Plaka == plaka.ToUpper())).FirstOrDefault<Araba>();
            if (araba == null || !(araba.Durum == "Galeride"))
                return;
            this.Arabalar.Remove(araba);
        }
        public string DurumSorgula(string plaka)
        {
            Araba araba = this.Arabalar.Where<Araba>((Func<Araba, bool>)(a => a.Plaka == plaka.ToUpper())).FirstOrDefault<Araba>();
            return araba != null ? araba.Durum : "ArabaYok";
        }
        public void ArabaTeslimAl(string plaka)
        {
            Araba araba = this.Arabalar.Where<Araba>((Func<Araba, bool>)(a => a.Plaka == plaka.ToUpper())).FirstOrDefault<Araba>();
            if (araba == null)
            {
                Console.WriteLine("Bu plakada bir araç yok.");
            }
            else if(araba.Durum == "Galeride")
            {
                Console.WriteLine("Zaten galeride");
            }
        }
        public void Kiralamaİptali(string plaka)
        {
            Araba araba = this.Arabalar.Where<Araba>((Func<Araba, bool>)(a => a.Plaka == plaka.ToUpper())).FirstOrDefault<Araba>();
            if (araba == null)
                return;
            araba.Durum = "Galeride";
            araba.KiralamaSureleri.RemoveAt(araba.KiralamaSureleri.Count - 1);

        }
        

    }       
}
