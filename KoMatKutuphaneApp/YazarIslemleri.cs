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
    public partial class YazarIslemleri : Form
    {
        VeriModel db = new VeriModel();
        int rowindex = -1;
        public YazarIslemleri()
        {
            InitializeComponent();
        }

        private void YazarIslemleri_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.YazarListele();
        }

        private void btn_yazarekle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_isim.Text))
            {
                Yazar model = new Yazar();
                model.Isim = tb_isim.Text;
                model.SoyIsim = tb_soyisim.Text;
                if (db.YazarEkle(model))
                {
                    dataGridView1.DataSource = db.YazarListele();
                    tb_isim.Text = tb_soyisim.Text = "";
                    MessageBox.Show("Yazar ekleme başarılı","Başarılı");
                }
                else
                {
                    MessageBox.Show("Yazar eklenirken bir hata oluştu","Hata");
                }
            }
            else
            {
                MessageBox.Show("Yazar ismi boş bırakılamaz", "Boş veri");
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            if (e.Button == MouseButtons.Right)
            {
                rowindex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (rowindex != -1)
                {
                    contextMenuStrip1.Show(dataGridView1,e.X,e.Y);
                    dataGridView1.Rows[rowindex].Selected = true;
                }
            }
        }

        private void TSMI_duzenle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
            Yazar model = db.YazarGetir(id);
            tb_id.Text = model.ID.ToString();
            tb_isim.Text = model.Isim;
            tb_soyisim.Text = model.SoyIsim;
            btn_duzenle.Visible = true;
        }

        private void TSMI_sil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[0].Value);
            int kitapsayi = db.YazarKitapSayi(id);
            if (kitapsayi == 0)
            {
                Yazar model = db.YazarGetir(id);
                DialogResult sonuc = MessageBox.Show($"{model.Isim} {model.SoyIsim} yazarı silenecektir.\nOnaylıyor musunuz?", "Silme işlemini onaylayın", MessageBoxButtons.YesNo);
                if (sonuc == DialogResult.Yes)
                {
                    db.YazarSil(id);
                    dataGridView1.DataSource = db.YazarListele();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi", "İptal");
                }
            }
            else
            {
                MessageBox.Show($"Bu yazarın sistemde kayıtlı {kitapsayi} adet kitabı olduğu için bu yazar silinemez...", "Yazar Silinemez");
            }
        }

        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_isim.Text))
            {
                Yazar model = new Yazar();
                model.ID = Convert.ToInt32(tb_id.Text);
                model.Isim = tb_isim.Text;
                model.SoyIsim = tb_soyisim.Text;
                if (db.YazarGuncelle(model))
                {
                    MessageBox.Show("Yazar Güncellendi", "Başarılı");
                    dataGridView1.DataSource = db.YazarListele();
                    tb_isim.Text = tb_soyisim.Text = tb_id.Text = "";
                    btn_duzenle.Visible = false;

                }
                else
                {
                    MessageBox.Show("Yazar Güncellenirken bir hata oluştu");
                }
            }
            else
            {
                MessageBox.Show("Yazar ismi boş bırakılamaz", "Boş veri");
            }
        }
    }
}
