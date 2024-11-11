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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //Araç sınıfını çağırma
        AracSinifi arac = new AracSinifi();
        //Kullanıcın araç fotoğrafı seçebilmesi için OpenFileDialog sınıfı çağırma
        OpenFileDialog openFileDialog= new OpenFileDialog();
        byte[] resim;

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //lisele metodunu çağırarak tablomuza verileri aktarıyoruz.
            dataGridView1.DataSource = arac.Listele();
            // form yüklendiğinde durum sütununu ayarlama
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
        }

        private void ekleBtn_Click(object sender, EventArgs e)
        {
            //Araç ekleme işlemi
            arac.Ekle(markaTxt.Text, modelTxt.Text, (int)uretimNmr.Value, plakaTxt.Text, (int)kmNmr.Value, renkTxt.Text, yakitCmb.Text, (int)kiraNmr.Value, durumCmb.Text, resim);
            dataGridView1.DataSource = arac.Listele();
            Temizle();
        }

        private void resimBtn_Click(object sender, EventArgs e)
        {
            //eklenebilecek resimlerin uzantı filremesi
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                resim = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //seçilen satırı alma
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                //seçilen satırı gerekli yerlere aktarma
                idTxt.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                markaTxt.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                modelTxt.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                uretimNmr.Value = (int)dataGridView1.Rows[secilen].Cells[3].Value;
                plakaTxt.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                kmNmr.Value = (int)dataGridView1.Rows[secilen].Cells[5].Value;
                renkTxt.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
                yakitCmb.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
                kiraNmr.Value = (int)dataGridView1.Rows[secilen].Cells[8].Value;
                durumCmb.Text = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
                resim = (byte[])dataGridView1.Rows[secilen].Cells[10].Value;
                MemoryStream ms = new MemoryStream(resim);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch (Exception)
            {

                MessageBox.Show("Veri ekledikten sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        // DataGridView'in CellFormatting olayını kullanarak renkleri değiştirme
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DURUM" && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["DURUM"].Value != null)
                {
                    string durum = dataGridView1.Rows[e.RowIndex].Cells["DURUM"].Value.ToString();

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



        private void güncelleBtn_Click(object sender, EventArgs e)
        {
            //Araç Güncelleme işlemi
            if(idTxt.Text != "")
            {
                arac.Güncelle(Convert.ToInt32(idTxt.Text), markaTxt.Text, modelTxt.Text, (int)uretimNmr.Value, plakaTxt.Text, (int)kmNmr.Value, renkTxt.Text, yakitCmb.Text, (int)kiraNmr.Value, durumCmb.Text, resim);
                dataGridView1.DataSource = arac.Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        // boxları temizleme
        public void Temizle()
        {
            idTxt.Text = "";
            markaTxt.Text = "";
            modelTxt.Text = "";
            uretimNmr.Value = 1980;
            plakaTxt.Text = "";
            kmNmr.Value = 0;
            renkTxt.Text = "";
            yakitCmb.Text = "";
            kiraNmr.Value = 0;
            durumCmb.Text = "";
            pictureBox1.Image = null;

        }

        private void silBtn_Click(object sender, EventArgs e)
        {
            if(idTxt.Text != "")
            {
                arac.Sil(Convert.ToInt32(idTxt.Text));
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                resim = (byte[])dataGridView1.Rows[secilen].Cells[10].Value;
                resim = null;
                dataGridView1.DataSource = arac.Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
    
}
