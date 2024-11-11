using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaUygulaması
{
    internal class AracSinifi : VeriTabaniBaglantisi
    {
        //Araç Sınıfının Özellikleri
        int aracId;
        string aracMarka;
        string aracModel;
        int aracUretimYili;
        string aracPlaka;
        int aracKm;
        string aracRenk;
        string aracYakitTuru;
        int aracKiraUcreti;
        string aracDurum;
        byte[] aracResim;


        //Araç Sınıfı Kapsülleme

        public int AracId { get => aracId; set => aracId = value; }
        public string AracMarka { get => aracMarka; set => aracMarka = value; }
        public string AracModel { get => aracModel; set => aracModel = value; }
        public int AracUretimYili { get => aracUretimYili; set => aracUretimYili = value; }
        public string AracPlaka { get => aracPlaka; set => aracPlaka = value; }
        public int AracKm { get => aracKm; set => aracKm = value; }
        public string AracRenk { get => aracRenk; set => aracRenk = value; }
        public string AracYakitTuru { get => aracYakitTuru; set => aracYakitTuru = value; }
        public int AracKiraUcreti { get => aracKiraUcreti; set => aracKiraUcreti = value; }
        public string AracDurum { get => aracDurum; set => aracDurum = value; }
        public byte[] AracResim { get => aracResim; set => aracResim = value; }


        //Araç Listeleme
        public DataTable Listele()
        {
           
            //tablo oluşturma
            DataTable dt = new DataTable();

            try
            {
                
                //listeleme sorgusu
                string query = "Select * from Tbl_Araclar";
                //verileri saklama
                SqlDataAdapter data = new SqlDataAdapter(query,baglanti);
                //veriler ile oluşturulan tabloyu doldurma
                data.Fill(dt);
            }
            catch (Exception)
            {

                MessageBox.Show("Veriler listelenirken hata oluştu lütfen daha sonra tekrar deneyeniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
            }

            return dt;
      
        }

        public DataTable BosAraclariListele()
        {
            DataTable dt = new DataTable();

            try
            {
                string query = "Select * from Tbl_Araclar Where Durum = 'Boşta' ";
                SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
                da.Fill(dt);
            }
            catch (Exception)
            {
                MessageBox.Show("Veriler listelenirken hata oluştu lütfen daha sonra tekrar deneyeniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public void Ekle(string aracMarka, string aracModel, int aracUretimYili, string aracPlaka, int aracKm, string aracRenk, string aracYakitTuru, int aracKiraUcreti , string aracDurum, byte[] aracResim)
        {  
           

            try
            {
                //Sql ekle sorgusu
                string query = "Insert Into Tbl_Araclar (MARKA,MODEL,URETIMYILI,PLAKA,KM,RENK,YAKITTURU,KIRAUCRETI,DURUM,RESIM)  Values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10)";
                //sorguyu komuta aktarma
                SqlCommand komut = new SqlCommand(query, baglanti);
                //paramterelin nereden değer alacağını belirleme
                komut.Parameters.AddWithValue("@P1", aracMarka);
                komut.Parameters.AddWithValue("@P2", aracModel);
                komut.Parameters.AddWithValue("@P3", aracUretimYili);
                komut.Parameters.AddWithValue("@P4", aracPlaka);
                komut.Parameters.AddWithValue("@P5", aracKm);
                komut.Parameters.AddWithValue("@P6", aracRenk);
                komut.Parameters.AddWithValue("@P7", aracYakitTuru);
                komut.Parameters.AddWithValue("@P8", aracKiraUcreti);
                komut.Parameters.AddWithValue("@P9", aracDurum);
                komut.Parameters.AddWithValue("@P10", aracResim);
                //Sql bağlantısı açma
                baglanti.Open();
                //Komut Çalıştır
                komut.ExecuteNonQuery();
                //Sql bağlantı kapatma
                baglanti.Close();
                //Doğru bir şekilde çalışırsa bilgilendirme mesajı
                MessageBox.Show("Araç bilgileri başarılı bir şekilde eklendi!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Araç bilgileri eklenirken hata ile karşılaşıldı! Lütfen bilgileri uygun şekilde giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        public void Güncelle(int aracId ,string aracMarka, string aracModel, int aracUretimYili, string aracPlaka, int aracKm, string aracRenk, string aracYakitTuru, int aracKiraUcreti, string aracDurum, byte[] aracResim)
        {
            try
            {
                //Sql güncelleme sorgusu
                string query = "Update Tbl_Araclar set MARKA = @P1 , MODEL = @P2, URETIMYILI = @P3, PLAKA = @P4, KM = @P5, RENK = @P6, YAKITTURU = @P7, KIRAUCRETI = @P8 , DURUM = @P9, RESIM = @P10 Where ID = @P11";
                //sorguyu komuta aktarma
                SqlCommand komut = new SqlCommand(query, baglanti);
                //Parametreleri oluşturma
                komut.Parameters.AddWithValue("@P1", aracMarka);
                komut.Parameters.AddWithValue("@P2", aracModel);
                komut.Parameters.AddWithValue("@P3", aracUretimYili);
                komut.Parameters.AddWithValue("@P4", aracPlaka);
                komut.Parameters.AddWithValue("@P5", aracKm);
                komut.Parameters.AddWithValue("@P6", aracRenk);
                komut.Parameters.AddWithValue("@P7", aracYakitTuru);
                komut.Parameters.AddWithValue("@P8", aracKiraUcreti);
                komut.Parameters.AddWithValue("@P9", aracDurum);
                komut.Parameters.AddWithValue("@P10", aracResim);
                komut.Parameters.AddWithValue("@P11", aracId);

                //Sql bağlantısını açma
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Araç bilgileri güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Araç bilgileri güncellenemedi! Lütfen doğru bilgiler giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        public void Sil(int aracId)
        {
            try
            {
                string query = "Delete from Tbl_Araclar Where ID = @P1";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@P1", aracId);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Araç bilgileri başarılı bir şekilde silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Araç bilgileri silinemedi! Lütfen daha sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
