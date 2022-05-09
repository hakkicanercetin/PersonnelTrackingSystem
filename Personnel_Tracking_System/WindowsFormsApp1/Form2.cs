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
using System.Text.RegularExpressions; // Güvenli parola için
using System.IO; // Giriş-çıkış işlemleri fotoğraf için
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.Ace.OleDb.12.0; Data Source=personel.accdb");

        private void yoneticileri_goster()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter listele = new OleDbDataAdapter("select tc AS[TC KİMLİK NO],isim AS[ADI],soyisim AS" +
                    "[SOYADI],kullaniciadi AS[KULLANCIADI],sifre AS[ŞİFRE] from kullanicilar " +
                    "Order By isim ASC", baglanti);
                DataSet dshafiza = new DataSet();
                listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                throw;
            }
        }
        private void personelleri_goster()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter listele2 = new OleDbDataAdapter("select tc AS[TC KİMLİK NO],ad AS[ADI],soyad AS" +
                    "[SOYADI],cinsiyet AS[CİNSİYET],mezuniyet AS[MEZUNİYET],dogumtarihi AS[DOĞUM TARİHİ]," +
                    "gorev AS[ÇALIŞMA ALANI],departman AS[DEPARTMAN],maas AS[MAAŞI] from personeller Order By ad ASC", baglanti);
                DataSet dshafiza = new DataSet();
                listele2.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            yoneticileri_goster();
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciprofil\\" + Form1.isim + ".jpg");
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciprofil\\fotoğrafyok.jpg");
            }
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label9.ForeColor = Color.DarkRed;
            label9.Text = Form1.isim + " " + Form1.soyisim;
            textBox1.MaxLength = 11;
            toolTip1.SetToolTip(this.textBox1, "TC Kimlik Numarası 11 Karakter Olmalıdır!");
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100; pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000"; //Zorunlu Sayısal Giriş
            maskedTextBox2.Mask = "LL??????????????????"; //En az iki karakter en fazla 20 karakter
            maskedTextBox3.Mask = "LL??????????????????";
            maskedTextBox4.Mask = "0000"; //En az 4 haneli sayı girmek zorunda
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();
            comboBox1.Items.Add("İlkÖğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");
            comboBox2.Items.Add("İşçi");
            comboBox2.Items.Add("Teknisyen");
            comboBox2.Items.Add("Tekniker");
            comboBox2.Items.Add("Mühendis");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Yönetim");
            comboBox3.Items.Add("Muhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Montaj");
            comboBox3.Items.Add("Bakım Onarım");
            comboBox3.Items.Add("Kaynakhane");
            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gün = int.Parse(zaman.ToString("dd"));
            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gün);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            radioButton3.Checked = true;
            personelleri_goster();
        }















        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {//11 karakterden az girilirse
            if (textBox1.Text.Length < 11)
                errorProvider1.SetError(textBox1, "TC Kimlik Numarası 11 karakter olmalıdır!");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {//klavyeden sadece rakam girmek için ASCII sayılarıyla if yapısı ayrıca backspace basılabilir.
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {//isim kısmına sadece harf, backspace yada boşluk tuşlarına basılabilir hale getirmek.
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 8)
                errorProvider1.SetError(textBox4, "Kullanıcı Adı en az 8 karakter olmalıdır!");
            else
                errorProvider1.Clear();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {//harf, backspace ve sayı tuşlarına basılabilir.
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }












        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Şifreler uyuşmuyor!");
            else
                errorProvider1.Clear();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length < 10)
                errorProvider1.SetError(textBox5, "Şifre en az 10 karakterden oluşmalıdır!");
            else
                errorProvider1.Clear();
        }
        private void Page1_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }
        private void Page2_temizle()
        {
            pictureBox2.Image = null; maskedTextBox1.Clear(); maskedTextBox2.Clear(); maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1; comboBox3.SelectedIndex = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool kayit = false;
            baglanti.Open();
            // girilen tc tabloda mevcut mu
            OleDbCommand slctsorgu = new OleDbCommand("select * from kullanicilar where tc='" + textBox1.Text + "'", baglanti);
            OleDbDataReader reader = slctsorgu.ExecuteReader();
            while (reader.Read())
            {//aynı tc ile kayıt varsa
                kayit = true;
                break;
            }
            baglanti.Close();
            if (kayit == false)
            {
                //TC Kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                //İsim Kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                //Soyisim Kontrolü
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                //Kullanıcı Adı Kontrolü
                if (textBox4.Text.Length < 5)
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                //Şifre Kontrolü
                if (textBox5.Text.Length < 10 || textBox5.Text == "")
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                //Şifre Tekrar Kontrolü
                if (textBox6.Text != textBox6.Text || textBox6.Text == "")
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;
                
                if (textBox1.Text.Length == 11 && textBox1.Text != "" &&
                    textBox2.Text.Length > 2 && textBox2.Text != "" &&
                    textBox3.Text.Length > 2 && textBox3.Text != "" &&
                    textBox4.Text.Length > 5 && textBox4.Text != "" &&
                    textBox5.Text != "" && textBox6.Text != "" &&
                    textBox5.Text == textBox6.Text)
                {//Koşullar sağlandığında ekle
                    try
                    {
                        baglanti.Open();
                        OleDbCommand insrt = new OleDbCommand("INSERT INTO kullanicilar VALUES('" + textBox1.Text + "'," +
                            "'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')",
                            baglanti);
                        insrt.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı eklendi!", "Personel Takip Sistemi", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        Page1_temizle();
                        yoneticileri_goster();
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message);
                        baglanti.Close();
                    }
                }
                else
                    MessageBox.Show("Kırmızı renkli alanları tekrar kontrol ediniz!", "Personel Takip Sistemi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Girilen TC kimlik numarası sistemde zaten kayıtlı!", "Personel Takip Sistemi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool arama = false;
            if (textBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand slct = new OleDbCommand("select * from kullanicilar where tc='" + textBox1.Text + "'", baglanti);
                OleDbDataReader reader = slct.ExecuteReader();
                while (reader.Read())
                {
                    arama = true;
                    textBox2.Text = reader.GetValue(1).ToString();
                    textBox3.Text = reader.GetValue(2).ToString();
                    textBox4.Text = reader.GetValue(3).ToString();
                    textBox5.Text = reader.GetValue(4).ToString();
                    textBox6.Text = reader.GetValue(4).ToString();
                    break;
                }
                if (arama == false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı!", "Personel Takip Sistemi", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli TC kimlik numarası giriniz!", "Personel Takip Sistemi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Page1_temizle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TC Kontrolü
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;
            //İsim Kontrolü
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
            //Soyisim Kontrolü
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;
            //Kullanıcı Adı Kontrolü
            if (textBox4.Text.Length < 5)
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;
            //Şifre Kontrolü
            if (textBox5.Text.Length < 10 || textBox5.Text == "")
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;
            //Şifre Tekrar Kontrolü
            if (textBox6.Text != textBox6.Text || textBox6.Text == "")
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" &&
                textBox2.Text.Length > 1 && textBox2.Text != "" &&
                textBox3.Text.Length > 1 && textBox3.Text != "" &&
                textBox4.Text.Length > 5 && textBox4.Text != "" &&
                textBox5.Text != "" && textBox6.Text != "" &&
                textBox5.Text == textBox6.Text)
            {
                try
                {
                    baglanti.Open();
                    OleDbCommand update = new OleDbCommand("update kullanicilar set isim='" + textBox2.Text + "'," +
                        "soyisim='" + textBox3.Text + "',kullaniciadi='" + textBox4.Text + "',sifre='" + textBox5.Text + "' " +
                        "where tc='" + textBox1.Text + "'", baglanti);
                    update.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kullanıcı bilgileri güncellendi!", "Personel Takip Sistemi", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    yoneticileri_goster();
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message, "Personel Takip Sistemi", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    baglanti.Close();
                }
            }
            else
                MessageBox.Show("Kırmızı renkli alanları tekrar kontrol ediniz!", "Personel Takip Sistemi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool aramadurum = false;
                baglanti.Open();
                OleDbCommand slct = new OleDbCommand("select * from kullanicilar where tc='" + textBox1.Text + "'", baglanti);
                OleDbDataReader reader = slct.ExecuteReader();
                while (reader.Read())
                {
                    aramadurum = true;
                    OleDbCommand delete = new OleDbCommand("delete from kullanicilar where tc='" + textBox1.Text + "'", baglanti);
                    delete.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla silindi!", "Personel Takip Sistemi", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    yoneticileri_goster();
                    Page1_temizle();
                    break;
                }
                if (aramadurum = false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "Personel Takip Sistemi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                    Page1_temizle();
                }
            }
            else
            {
                MessageBox.Show("Lütfen 11 karakterden oluşan TC kimlik numarası giriniz!", "Personel Takip Sistemi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Page1_temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog profilfoto = new OpenFileDialog();
            profilfoto.Title = "Eklemek İstediğiniz Profil Fotoğrafı Seçiniz!";
            profilfoto.Filter = "JPG Dosyalar (*.jpg) | *.jpg";
            if (profilfoto.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(profilfoto.OpenFile());
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayit = false;
            baglanti.Open();
            OleDbCommand slct = new OleDbCommand("select * from personeller where tc='" + maskedTextBox1.Text + "'", baglanti);
            OleDbDataReader reader = slct.ExecuteReader();
            while (reader.Read())
            {
                kayit = true;
                break;
            }
            baglanti.Close();
            if (kayit == false)
            {
                if (pictureBox2.Image == null)
                {
                    button6.ForeColor = Color.Red;
                }
                else
                    button6.ForeColor = Color.Black;
                if (maskedTextBox1.MaskCompleted == false)
                {
                    label10.ForeColor = Color.Red;
                }
                else
                    label10.ForeColor = Color.Black;
                if (maskedTextBox2.MaskCompleted == false)
                {
                    label11.ForeColor = Color.Red;
                }
                else
                    label11.ForeColor = Color.Black;
                if (maskedTextBox3.MaskCompleted == false)
                {
                    label12.ForeColor = Color.Red;
                }
                else
                    label12.ForeColor = Color.Black;
                if (comboBox1.Text == "")
                {
                    label14.ForeColor = Color.Red;
                }
                else
                    label14.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                {
                    label16.ForeColor = Color.Red;
                }
                else
                    label16.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                {
                    label17.ForeColor = Color.Red;
                }
                else
                    label17.ForeColor = Color.Black;
                if (maskedTextBox4.MaskCompleted == false)
                {
                    label18.ForeColor = Color.Red;
                }
                else
                    label18.ForeColor = Color.Black;
                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label18.ForeColor = Color.Red;
                else
                    label18.ForeColor = Color.Black;
                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false &&
                    maskedTextBox3.MaskCompleted != false && maskedTextBox4.MaskCompleted != false && comboBox1.Text != "" &&
                    comboBox2.Text != "" && comboBox3.Text != "")
                {
                    if (radioButton3.Checked == true)
                    {
                        cinsiyet = "Erkek";
                    }
                    else if (radioButton4.Checked == true)
                    {
                        cinsiyet = "Kadın";
                    }
                    try
                    {
                        baglanti.Open();
                        OleDbCommand insrt = new OleDbCommand("INSERT INTO personeller VALUES('" + maskedTextBox1.Text + "', '" + 
                            maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + 
                            dateTimePicker1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + maskedTextBox4.Text + "')", baglanti);
                        insrt.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelprofil"))//klasör var mı
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\personelprofil");//yoksa oluştur
                        }
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelprofil\\" + maskedTextBox2.Text + ".jpg");
                        MessageBox.Show("Yeni Personel Kaydı Oluşturuldu.", "Personel Takip Programı", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        Page2_temizle();
                        maskedTextBox4.Text = "0";
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show(hata.Message, "Personel Takip Programı", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                else
                    MessageBox.Show("Kırmızı renkli alanları tekrar kontrol ediniz!", "Personel Takip Sistemi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Girdiğiniz TC kimlik numarası ait bilgiler zaten kayıtlı!", "Personel Takip Sistemi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool kayitara = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglanti.Open();
                OleDbCommand slct = new OleDbCommand("select * from personeller where tc='" + maskedTextBox1.Text + "'", baglanti);
                OleDbDataReader reader = slct.ExecuteReader();
                while (reader.Read())
                {
                    kayitara = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelprofil\\" + reader.GetValue(1).ToString() +
                            ".jpg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelprofil\\fotoğrafyok.jpg");
                    }
                    maskedTextBox2.Text = reader.GetValue(1).ToString();
                    maskedTextBox3.Text = reader.GetValue(2).ToString();
                    if (reader.GetValue(3) == "Erkek")
                    {
                        radioButton3.Checked = true;
                    }
                    else
                    {
                        radioButton4.Checked = true;
                    }
                    comboBox1.Text = reader.GetValue(4).ToString();
                    dateTimePicker1.Text = reader.GetValue(5).ToString();
                    comboBox2.Text = reader.GetValue(6).ToString();
                    comboBox3.Text = reader.GetValue(7).ToString();
                    maskedTextBox4.Text = reader.GetValue(8).ToString();
                    break;
                }
                if (kayitara == false)
                {
                    MessageBox.Show("Aranan Kayıt Bulunamadı!", "Personel Takip Sistemi",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli TC kimlik numarası giriniz!", "Personel Takip Sistemi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Page2_temizle();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (pictureBox2.Image == null)
                button6.ForeColor = Color.Red;
            else
                button6.ForeColor = Color.Black;
            if (maskedTextBox1.MaskCompleted == false)
                label10.ForeColor = Color.Red;
            else
                label10.ForeColor = Color.Black;
            if (maskedTextBox2.MaskCompleted == false)
                label11.ForeColor = Color.Red;
            else
                label11.ForeColor = Color.Black;
            if (maskedTextBox3.MaskCompleted == false)
                label12.ForeColor = Color.Red;
            else
                label12.ForeColor = Color.Black;
            if (comboBox1.Text == "")
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.Black;
            if (comboBox2.Text == "")
                label16.ForeColor = Color.Red;
            else
                label16.ForeColor = Color.Black;
            if (comboBox3.Text == "")
                label17.ForeColor = Color.Red;
            else
                label17.ForeColor = Color.Black;
            if (maskedTextBox4.MaskCompleted == false)
                label18.ForeColor = Color.Red;
            else
                label18.ForeColor = Color.Black;
            if (int.Parse(maskedTextBox4.Text) < 1000)
                label18.ForeColor = Color.Red;
            else
                label18.ForeColor = Color.Black;
            if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false &&
                maskedTextBox3.MaskCompleted != false && maskedTextBox4.MaskCompleted != false && comboBox1.Text != "" &&
                comboBox2.Text != "" && comboBox3.Text != "")
            {
                if (radioButton3.Checked == true)
                {
                    cinsiyet = "Erkek";
                }
                else if (radioButton4.Checked == true)
                {
                    cinsiyet = "Kadın";
                }
                try
                {
                    baglanti.Open();
                    OleDbCommand update = new OleDbCommand("UPDATE personeller set ad='" + maskedTextBox2.Text + "',soyad='" + 
                        maskedTextBox3.Text + "',cinsiyet='" + cinsiyet + "',mezuniyet='" + comboBox1.Text + "',dogumtarihi='" + 
                        dateTimePicker1.Text + "',gorev='" + comboBox2.Text + "',departman='" + comboBox3.Text + "',maas='" + 
                        maskedTextBox4.Text + "' where tc='" + maskedTextBox1.Text + "'", baglanti);
                    update.ExecuteNonQuery();
                    baglanti.Close();
                    personelleri_goster();
                    MessageBox.Show("Personel bilgisi güncellendi!", "Personel Takip Sistemi", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    Page2_temizle();
                    maskedTextBox4.Text = "0";
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message, "Personel Takip Programı", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    baglanti.Close();
                }
            }
            else
                MessageBox.Show("Kırmızı renkli alanları tekrar kontrol ediniz!", "Personel Takip Sistemi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted == true)
            {
                bool kayitara = false;
                baglanti.Open();
                OleDbCommand search = new OleDbCommand("select * from personeller where tc='" + maskedTextBox1.Text + "'", baglanti);
                OleDbDataReader reader = search.ExecuteReader();
                while (reader.Read())
                {
                    kayitara = true;
                    OleDbCommand delete = new OleDbCommand("DELETE FROM personeller where tc='" + maskedTextBox1.Text + "'", baglanti);
                    delete.ExecuteNonQuery();
                    break;
                }
                if (kayitara == false)
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "Personel Takip Sistemi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
                personelleri_goster();
                Page2_temizle();
                maskedTextBox4.Text = "0";
            }
            else
                MessageBox.Show("Lütfen 11 haneli TC kimlik numarası giriniz!", "Personel Takip Sistemi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            Page2_temizle();
            maskedTextBox4.Text = "0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Page2_temizle();
        }
    }
}
