using FinancialCrm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialCrm.Models;

namespace FinancialCrm
{
    public partial class FrmCategories : Form
    {
        public FrmCategories()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities1 db = new FinancialCrmDbEntities1();
        private void FrmCategories_Load(object sender, EventArgs e)
        {
            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string categorieName = txtCategoriesName.Text;

            Categories categories = new Categories();
            categories.CategoryName = categorieName;
            db.Categories.Add(categories);
            db.SaveChanges();
            MessageBox.Show("Kategori Ekleme İşlemi Başarılı Bir Şekilde Sisteme Entegre Edildi.", "Kategoriler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        private void BtnRemoveCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoriesId.Text);
            var removeValue = db.Categories.Find(id);
            db.Categories.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Silme İşlemi Başarılı Bir Şekilde Gerçekleştirildi.", "Kategoriler", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var values = db.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        // Güncelleme Kısmı
        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string categorieName = txtCategoriesName.Text;
            int id = int.Parse(txtCategoriesId.Text);

            var values = db.Categories.Find(id);

            values.CategoryName = categorieName;
            db.SaveChanges();
            MessageBox.Show("Kategori Başarılı Bir Şekilde Sistemde Güncellendi.", "Kategoriler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values2 = db.Categories.ToList();
            dataGridView1.DataSource = values2;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDashboardForm_Click_1(object sender, EventArgs e)
        {
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
            this.Hide();
        }

        private void btnBanksForm_Click_1(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnBankProcessesForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
            frm.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.Show();
            this.Hide();
        }
    }
}
