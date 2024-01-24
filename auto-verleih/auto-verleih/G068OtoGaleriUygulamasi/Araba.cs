using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace G068OtoGaleriUygulamasi
{
    //Bu sinifta araba ile ilgili ozellik ve islemler olmali
    internal class Araba
    {
        public string Plaka { get; set; }

        public string Marka { get; set; }

        public float KiralamaBedeli { get; set; }

        public string AracTipi { get; set; }

        public string Durum { get; set; }
        
        public List<int> KiralamaSureleri = new List<int>();

        public int ToplamKiralamaSayisi
        {
            get
            {
                return this.KiralamaSureleri.Count;
            }
        }

        public int ToplamKiralamaSuresi
        {
            get
            {
                return this.KiralamaSureleri.Sum();
            }

        }

        public Araba(string plaka, string marka, float kiralamaBedeli, string aracTipi)
        {
            this.Plaka = plaka;
            this.Marka = marka;
            this.KiralamaBedeli = kiralamaBedeli;
            this.AracTipi = aracTipi;
            this.Durum = "Galeride";
        }
        //ctor:  kurucu metot olusturmak icin kullanilan kisaltma.

    }
}
