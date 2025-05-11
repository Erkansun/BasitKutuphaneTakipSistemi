using Helper;
using System.Net.Sockets;

namespace BasitKutuphaneTakipSistemi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Kitap> kitaps = new List<Kitap>();
            List<Kullanici> kullanicis = new List<Kullanici>();
            List<KitapOduncVerme> kitapOduncVermes = new List<KitapOduncVerme>();

            int IdKitap = 1;
            int IdKullanici = 1;

            ProgramAcilis();

        BAS:
            Console.WriteLine();
            Console.WriteLine("Menüden bir seçim yapın...");
            Console.WriteLine();
            Console.WriteLine("[1] Kitap Ekle\n[2] Kullanıcı Ekle\n[3] Ödünç Ver\n[4] Listele\n[0] Çıkış");
            Console.WriteLine();
            Console.Write("Seçiminiz: ");
            string? menuSecilen = Console.ReadLine();

            if (!string.IsNullOrEmpty(menuSecilen))
            {
                switch (menuSecilen)
                {
                    case "1":
                        KitapEkle();
                        Console.WriteLine();
                        Console.WriteLine($"{IdKitap} ID'li kitap eklemiştir.");
                        IdKitap++;
                        goto BAS;
                    case "2":
                        KullaniciEkle();
                        Console.WriteLine();
                        Console.WriteLine($"{IdKullanici} ID'li kullanıcı eklenmiştir.");
                        IdKullanici++;
                        goto BAS;
                    case "3":
                        if (kitaps.Count > 0 && kullanicis.Count > 0)
                        {
                            OduncVer();
                        }
                        else
                        {
                            Console.WriteLine("Ödünç verme işlem için öncelikle kitap ve/veya kullanıcı tanımlamanız gerekiyor...");
                        }
                        goto BAS;
                    case "4":
                        Console.Clear();
                        if (kitapOduncVermes.Count != 0)
                        {
                            Listele();
                        }
                        else
                        {
                            Console.WriteLine("Ödünç verilen kitap olmadı");
                        }
                        goto BAS;
                    case "0":
                        Console.WriteLine("Güle Güle...");
                        goto SON;
                    default:
                        Console.WriteLine("Yanlış bir seçim yaptınız, yeninden deneyin...");
                        goto BAS;
                }
            }
            else
            {
                Console.WriteLine("Boş seçim yapılamaz. Lütfen tekrar deneyin...");
                goto BAS;
            }



            string Sor(string soru)
            {
                Console.Write(soru);
                return Console.ReadLine();
            }



            void KitapEkle()
            {
                Kitap kitap = new Kitap();
                kitap.Id = IdKitap;
                kitap.KitapAdi = Sor("Kitap Adı: ");
                kitap.YazarAdi = Sor("Yazar Adı: ");
                kitap.YayinYili = Convert.ToInt32(Sor("Yayın Yılı: "));

                kitaps.Add(kitap);

            }



            void KullaniciEkle()
            {
                Kullanici kullanici = new Kullanici();
                kullanici.Id = IdKullanici;
                kullanici.AdiSoyadi = Sor("Kullanıcı Adı: ");

                kullanicis.Add(kullanici);
            }



            void OduncVer()
            {
                KitapOduncVerme kitapOduncVer = new KitapOduncVerme();

            KITAPODUNC:
                int kitapID = Convert.ToInt32(Sor("Ödünç verilecek kitap ID: "));

                Kitap? arananKitap = KitapBul(kitapID);
                if (arananKitap != null)
                {
                    kitapOduncVer.KitapId = kitapID;
                }
                else
                {
                    Console.WriteLine($"{kitapID} ID'li kitap sistemde kayıtlı değil...");
                    goto KITAPODUNC;
                }

            KULLANICIODUNC:
                int kullaniciID = Convert.ToInt32(Sor("Ödünç verilecek kullanıcı ID: "));

                Kullanici? arananKullanici = KullaniciBul(kullaniciID);
                if (arananKullanici != null)
                {
                    kitapOduncVer.KullaniciId = kullaniciID;
                }
                else
                {
                    Console.WriteLine($"{kullaniciID} ID'li kullanıcı sistemde kayıtlı değil...");
                    goto KULLANICIODUNC;
                }

                kitapOduncVermes.Add(kitapOduncVer);
            }

            Kitap? KitapBul(int ID)
            {
                Kitap? kitapSonuc = null;

                foreach (var item in kitaps)
                {
                    if (ID == item.Id)
                    {
                        kitapSonuc = item;
                        break;
                    }
                }

                return kitapSonuc;
            }

            Kullanici? KullaniciBul(int ID)
            {
                Kullanici? kullaniciSonuc = null;

                foreach (var item in kullanicis)
                {
                    if (ID == item.Id)
                    {
                        kullaniciSonuc = item;
                        break;
                    }
                }

                return kullaniciSonuc;
            }

            void Listele()
            {
                Console.WriteLine("Eklenen Kitaplar");
                Console.WriteLine("----------------");
                foreach (var item in kitaps)
                {
                    Console.WriteLine($"{item.Id}. {item.KitapAdi} - {item.YazarAdi} - {item.YayinYili}");
                }

                Console.WriteLine();
                Console.WriteLine("Eklenen Kullanıcılar");
                Console.WriteLine("--------------------");
                foreach (var item in kullanicis)
                {
                    Console.WriteLine($"{item.Id}. {item.AdiSoyadi}");
                }

                Console.WriteLine();
                Console.WriteLine("Ödünç Verilen Kitaplar");
                Console.WriteLine("----------------------");
                foreach (var item in kitapOduncVermes)
                {
                    Kitap? kitap = KitapBul(item.KitapId);
                    Kullanici? kullanici = KullaniciBul(item.KullaniciId);
                    Console.WriteLine($"{kitap.KitapAdi} ({kitap.YazarAdi} - {kitap.YayinYili}) => {kullanici.AdiSoyadi}");
                }
            }


            void ProgramAcilis()
            {
                Console.WriteLine("***************************");
                Console.WriteLine("* KÜTÜPHANE TAKİP SİSTEMİ *");
                Console.WriteLine("***************************");
            }


        SON:
            Console.ReadKey();
        }
    }
}
