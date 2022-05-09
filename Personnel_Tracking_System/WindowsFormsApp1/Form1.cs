using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand slctsorgu = new OleDbCommand("select * from kullanicilar", baglanti);
            OleDbDataReader reader = slctsorgu.ExecuteReader();
            while (reader.Read())
            {
                if (reader["kullaniciadi"].ToString() == textBox1.Text &&
                        reader["sifre"].ToString() == textBox2.Text)
                {
                    durum = true;
                    tc = reader.GetValue(0).ToString();
                    isim = reader.GetValue(1).ToString();
                    soyisim = reader.GetValue(2).ToString();
                    this.Hide(); //Form1'in gizlenmesi
                    Form2 frm2 = new Form2();
                    frm2.Show();
                }
                else
                {
                    MessageBox.Show("Yanlış bilgiler girildi!Lütfen kontrol ediniz.", "Personel Takip Sistemi", 
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
                }
            }
            baglanti.Close();
        }
        
        
        OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.Ace.OleDb.12.0;Data Source = personel.accdb");
        public static string tc, isim, soyisim;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        bool durum = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Personel Takip Sistemi";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
    }
}
