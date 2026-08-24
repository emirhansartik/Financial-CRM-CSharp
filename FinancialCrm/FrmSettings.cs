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
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities1 db = new FinancialCrmDbEntities1();
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            var values = db.Users.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnUserList_Click(object sender, EventArgs e)
        {
            var values = db.Users.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtUserPassword.Text;

            Users users = new Users(); 

            users.Username = username;
            users.Password = password;  
            
            db.Users.Add(users);
            db.SaveChanges();
            MessageBox.Show("Kullanıcı Başarılı Bir Şekilde Sisteme Entegre Edildi", "Kullanıcı Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values = db.Users.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtUserId.Text);
            var removeValue = db.Users.Find(id);
            db.Users.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Kullanıcı Silme İşlemi Başarıyla Gerçekleştirildi", "Kullanıcı Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var values = db.Users.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;    
            string password = txtUserPassword.Text;
            int id = int.Parse(txtUserId.Text);

            var values = db.Users.Find(id);

            values.Username = username;
            values.Password = password;
            db.SaveChanges();
            MessageBox.Show("Kullanıcı Başarılı Bir Şekilde Güncelleştirildi", "Kullanıcı Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values2 = db.Users.ToList();
            dataGridView1.DataSource = values2;
        }
    }
}
