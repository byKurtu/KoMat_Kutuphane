using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeriErisimKatmani;

namespace KoMatKutuphaneApp
{
    public partial class YayinEviIslemleri : Form
    {
        VeriModel db = new VeriModel();
        int secilenID = -1;
        public YayinEviIslemleri()

        {
            InitializeComponent();
        }

        private void YayinEviIslemleri_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.YayinEviListele();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_isim.Text))
            {
                YayinEvleri model = new YayinEvleri();
                model.Isim = tb_isim.Text;
                if (db.YayinEviEkle(model))
                {
                    MessageBox.Show("YayinEvi eklendi", "Ekleme Başarılı");
                }
                else
                {
                    MessageBox.Show("Bir Hata Oluştu", "Ekleme Başarısız");
                }
                tb_isim.Text = "";
                dataGridView1.DataSource = db.YayinEviListele();
            }
            else
            {
                MessageBox.Show("YayinEvi Adı boş bırakılamaz", "Boş veri");
            }
        }

        private void TSMI_duzenle_Click(object sender, EventArgs e)
        {
            YayinEvleri y = db.YayinEviGetir(secilenID);
            tb_id.Text = y.ID.ToString();
            tb_isim.Text = y.Isim;
            btn_duzenle.Visible = true;
        }

        private void TSMI_sil_Click(object sender, EventArgs e)
        {
            db.YayinEviSil(secilenID);
            dataGridView1.DataSource = db.YayinEviListele();
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            YayinEvleri t = db.YayinEviGetir(secilenID);
            if (!string.IsNullOrEmpty(tb_isim.Text))
            {
                t.Isim = tb_isim.Text;
                if (db.YayinEviGuncelle(t))
                {
                    MessageBox.Show("Güncelleme Başarılı", "Başarılı");
                    tb_id.Text = tb_isim.Text = "";
                    btn_duzenle.Visible = false;
                    dataGridView1.DataSource = db.YayinEviListele();
                }
                else
                {
                    MessageBox.Show("YayinEvi güncellenirken bir hata oluştur", "Başarısız");
                }
            }
            else
            {
                MessageBox.Show("YayinEvi adi boş bırakılamaz", "Hata");
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int satir = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                dataGridView1.ClearSelection();
                if (satir != -1)
                {
                    contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                    dataGridView1.Rows[satir].Selected = true;
                    secilenID = Convert.ToInt32(dataGridView1.Rows[satir].Cells[0].Value);
                }
            }
        }
    }
}
