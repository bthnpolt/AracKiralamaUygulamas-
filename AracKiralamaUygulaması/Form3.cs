using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaUygulaması
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        //müşteri sınıfı çağırma
        MusteriSinifi musteri = new MusteriSinifi();
        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = musteri.Listele();
        }

        private void ekleBtn_Click(object sender, EventArgs e)
        {
            if(adTxt.Text != "" && soyadTxt.Text != "" && tcMsk.Text != "" && telefonMsk.Text !="" && mailTxt.Text != "" && adresRch.Text != "")
            {
                musteri.Ekle(adTxt.Text, soyadTxt.Text, tcMsk.Text, telefonMsk.Text, mailTxt.Text, adresRch.Text);
                dataGridView1.DataSource = musteri.Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        public void Temizle()
        {
            idTxt.Text = "";
            adTxt.Text = "";
            soyadTxt.Text = "";
            tcMsk.Text = "";
            telefonMsk.Text = "";
            mailTxt.Text = "";
            adresRch.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;

                idTxt.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                adTxt.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                soyadTxt.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                tcMsk.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                telefonMsk.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                mailTxt.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                adresRch.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Veri ekledikten sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void güncelleBtn_Click(object sender, EventArgs e)
        {
            if (adTxt.Text != "" && soyadTxt.Text != "" && tcMsk.Text != "" && telefonMsk.Text != "" && mailTxt.Text != "" && adresRch.Text != "")
            {
                musteri.Güncelle(Convert.ToInt32(idTxt.Text), adTxt.Text, soyadTxt.Text, tcMsk.Text, telefonMsk.Text, mailTxt.Text, adresRch.Text);
                dataGridView1.DataSource = musteri.Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void silBtn_Click(object sender, EventArgs e)
        {
            if (adTxt.Text != "" && soyadTxt.Text != "" && tcMsk.Text != "" && telefonMsk.Text != "" && mailTxt.Text != "" && adresRch.Text != "")
            {
                musteri.Sil(Convert.ToInt32(idTxt.Text));
                dataGridView1.DataSource = musteri.Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz!", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

          
        }
    }
}
