using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaUygulaması
{
    internal class MusteriSinifi : VeriTabaniBaglantisi
    {
        //Müşteri Sınıfı Özellikleri
        int musteriId;
        string musteriAd;
        string musteriSoyad;
        string musteriTc;
        string musteriTelefon;
        string musteriMail;
        string musteriAdres;


        //Müşteri Sınıfı Kapsülleme

        public int MusteriId { get => musteriId; set => musteriId = value; }
        public string MusteriAd { get => musteriAd; set => musteriAd = value; }
        public string MusteriSoyad { get => musteriSoyad; set => musteriSoyad = value; }
        public string MusteriTc { get => musteriTc; set => musteriTc = value; }
        public string MusteriTelefon { get => musteriTelefon; set => musteriTelefon = value; }
        public string MusteriMail { get => musteriMail; set => musteriMail = value; }
        public string MusteriAdres { get => musteriAdres; set => musteriAdres = value; }


        public DataTable Listele()
        {

            //tablo oluşturma
            DataTable dt = new DataTable();

            try
            {

                //listeleme sorgusu
                string query = "Select * from Tbl_Musteriler";
                //verileri saklama
                SqlDataAdapter data = new SqlDataAdapter(query, baglanti);
                //veriler ile oluşturulan tabloyu doldurma
                data.Fill(dt);
            }
            catch (Exception)
            {

                MessageBox.Show("Veriler listelenirken hata oluştu lütfen daha sonra tekrar deneyeniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return dt;

        }

        public void Ekle(string musteriAd, string musteriSoyad, string musteriTc, string musteriTelefon, string musteriMail, string musteriAdres)
        {
            try
            {
                //ekle sorgusu
                string query = "Insert Into Tbl_Musteriler (AD,SOYAD,TC,TELEFON,MAIL,ADRES) Values (@P1,@P2,@P3,@P4,@P5,@P6)";
                //sorguyu komuta aktarma
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@P1", musteriAd);
                komut.Parameters.AddWithValue("@P2", musteriSoyad);
                komut.Parameters.AddWithValue("@P3", musteriTc);
                komut.Parameters.AddWithValue("@P4", musteriTelefon);
                komut.Parameters.AddWithValue("@P5", musteriMail);
                komut.Parameters.AddWithValue("@P6", musteriAdres);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Müşteri başarılı bir şekilde eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Müşteri bilgileri eklenirken hata ile karşılaşıldı! Lütfen bilgileri uygun şekilde giriniz.", "Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           

        }

        public void Güncelle(int musteriId, string musteriAd, string musteriSoyad, string musteriTc, string musteriTelefon, string musteriMail, string musteriAdres)
        {
            try
            {
                //güncelleme sorgusu
                string query = "Update Tbl_Musteriler set AD = @P1, SOYAD = @P2, TC = @P3, TELEFON = @P4, MAIL = @P5, ADRES = @P6 Where ID = @P7";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@P1", musteriAd);
                komut.Parameters.AddWithValue("@P2", musteriSoyad);
                komut.Parameters.AddWithValue("@P3", musteriTc);
                komut.Parameters.AddWithValue("@P4", musteriTelefon);
                komut.Parameters.AddWithValue("@P5", musteriMail);
                komut.Parameters.AddWithValue("@P6", musteriAdres);
                komut.Parameters.AddWithValue("@P7", musteriId);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Müşteri bilgileri başarılı bir şekilde güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Müşteri bilgileri güncellenemedi! Lütfen bilgileri kontrol ediniz.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           

        }

        public void Sil(int musteriId)
        {
            try
            {
                string query = "Delete from Tbl_Musteriler Where ID = @P1";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@P1", musteriId);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Müşteri bilgileri başarılı bir şekilde silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Müşteri bilgileri silinemedi! Lütfen daha sonra tekrar deneyiniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
