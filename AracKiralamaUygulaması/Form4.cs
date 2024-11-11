using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaUygulaması
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        AracSinifi arac = new AracSinifi();
        MusteriSinifi musteri = new MusteriSinifi();
        Kiralamaİslemleri kiralama = new Kiralamaİslemleri(); 
        OpenFileDialog openFileDialog = new OpenFileDialog();
        byte[] resim;
        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = musteri.Listele();
            dataGridView2.DataSource = arac.BosAraclariListele();
            dataGridView2.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView2_CellFormatting);
        }

        // DataGridView'in CellFormatting olayını kullanarak renkleri değiştirme
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "DURUM" && e.RowIndex >= 0)
            {
                if (dataGridView2.Rows[e.RowIndex].Cells["DURUM"].Value != null)
                {
                    string durum = dataGridView2.Rows[e.RowIndex].Cells["DURUM"].Value.ToString();

                    if (durum == "Boşta")
                    {
                        e.CellStyle.BackColor = Color.Green;
                        e.CellStyle.ForeColor = Color.White;
                    }
                    else if (durum == "Kiralanmış")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                }
                else
                {

                    e.CellStyle.BackColor = Color.White;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;

                musIdTxt.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                adTxt.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                soyadTxt.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                tcTxt.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                telefonMsk.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                mailTxt.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                adresRch.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Veri ekledikten sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //seçilen satırı alma
                int secilen = dataGridView2.SelectedCells[0].RowIndex;
                //seçilen satırı gerekli yerlere aktarma
                aracIdTxt.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
                markaTxt.Text = dataGridView2.Rows[secilen].Cells[1].Value.ToString();
                modelTxt.Text = dataGridView2.Rows[secilen].Cells[2].Value.ToString();
                uretimTxt.Text = dataGridView2.Rows[secilen].Cells[3].Value.ToString();
                plakaTxt.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
                kmTxt.Text = dataGridView2.Rows[secilen].Cells[5].Value.ToString();
                renkTxt.Text = dataGridView2.Rows[secilen].Cells[6].Value.ToString();
                yakitTxt.Text = dataGridView2.Rows[secilen].Cells[7].Value.ToString();
                kiraTxt.Text = dataGridView2.Rows[secilen].Cells[8].Value.ToString();
                durumTxt.Text = dataGridView2.Rows[secilen].Cells[9].Value.ToString();
                resim = (byte[])dataGridView2.Rows[secilen].Cells[10].Value;
                MemoryStream ms = new MemoryStream(resim);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch (Exception)
            {

                MessageBox.Show("Veri ekledikten sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void KiralamaUcretiHesaplama()
        {
            //günlük ücret textboxtan çekiliyor
            int günlükUcret = Convert.ToInt32(kiraTxt.Text);
            // başlangıç ve bitiş günleri alınıyor.
            DateTime baslangic = baslangicDate.Value;
            DateTime bitis = bitisDate.Value;
            //gün sayısını hesaplama
            int günSayisi = (bitis - baslangic).Days;
            //kontrol
            if (günSayisi <=0)
            {
                MessageBox.Show("Lütfen geçerli bir tarih aralığı seçiniz!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //toplam ücret hesaplama ve yazdırma
            int toplamUcret = günSayisi * günlükUcret;
            bedelTxt.Text = toplamUcret.ToString();

        }

        private void bitisDate_ValueChanged(object sender, EventArgs e)
        {
            KiralamaUcretiHesaplama();
        }

        private void kaydetBtn_Click(object sender, EventArgs e)
        {
            kiralama.AracDurumGuncelleme(Convert.ToInt32(aracIdTxt.Text));
            dataGridView2.DataSource = arac.BosAraclariListele();
        }
    }
}
