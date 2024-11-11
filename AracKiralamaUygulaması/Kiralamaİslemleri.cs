using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaUygulaması
{
    internal class Kiralamaİslemleri : VeriTabaniBaglantisi
    {
        //kiralama işlemi tamamlandıktan sonra boşta olan aracın kiralanmış olarak ayarlama

        public void AracDurumGuncelleme(int aracİd)
        {
            try
            {
                string query = "Update Tbl_Araclar set Durum = 'Kiralanmış' Where ID = @P1";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@P1", aracİd);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Araç başarılı bir şekilde kiralandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Araç kiralama işlemi yapılırken bir sorun ile karşılaşıldı! Lütfen daha sonra tekrar deneyiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
    }
}
