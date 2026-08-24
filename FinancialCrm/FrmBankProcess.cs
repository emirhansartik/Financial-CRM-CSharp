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
    public partial class FrmBankProcess : Form
    {
        public FrmBankProcess()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities1 db = new FinancialCrmDbEntities1();
        private void FrmBankProcess_Load(object sender, EventArgs e)
        {
            var values = db.BankProcesses.Select(x => new
            {
                x.BankProcessId,
                x.Description,
                x.ProcessDate,
                x.ProcessType,
                x.Amount,
                BankaAdi = x.Banks.BankTitle // İşte o sihirli dokunuş! Yabancı anahtar üzerinden gidip bankanın gerçek adını alıyoruz.
            }).ToList();

            dataGridView1.DataSource = values;
            var banks = db.Banks.ToList();
            cmbBankOption.DataSource = banks;
            cmbBankOption.DisplayMember = "BankTitle";
            cmbBankOption.ValueMember = "BankId";
        }

        private void btnBankProcessList_Click(object sender, EventArgs e)
        {
            var values = db.BankProcesses.Select(x => new
            {
                x.BankProcessId,
                x.Description,
                x.ProcessDate,
                x.ProcessType,
                x.Amount,
                BankaAdi = x.Banks.BankTitle // İşte o sihirli dokunuş! Yabancı anahtar üzerinden gidip bankanın gerçek adını alıyoruz.
            }).ToList();

            dataGridView1.DataSource = values;
        }

        private void btnCreateBankProcess_Click(object sender, EventArgs e)
        {
            string bankProcessDescription = txtBankDescription.Text;
            DateTime bankProcessesDate = DateTime.Parse(txtBankDate.Text);
            string bankProcessOption = txtBankOption.Text;
            decimal bankProcessAmount = decimal.Parse(txtBankAmount.Text);
            int selectedBankId = int.Parse(cmbBankOption.SelectedValue.ToString());

            BankProcesses bankProcesses = new BankProcesses();
            bankProcesses.Description = bankProcessDescription;
            bankProcesses.ProcessDate = bankProcessesDate;
            bankProcesses.ProcessType = bankProcessOption;
            bankProcesses.Amount = bankProcessAmount;
            bankProcesses.BankId = selectedBankId;


            db.BankProcesses.Add(bankProcesses);
            db.SaveChanges();
            MessageBox.Show("Banka Hareketleri Sisteme Başarılı Bir Şekilde Entegre Edildi", "Banka Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values = db.BankProcesses.ToList();
            dataGridView1.DataSource = values;
        }

        private void BtnRemoveBankProcess_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBankProcessId.Text);
            var removeValue = db.BankProcesses.Find(id);
            db.BankProcesses.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Silme İşlemi Başarılı Bir Şekilde Gerçekleştirildi.", "Banka Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var values = db.BankProcesses.Select(x => new
            {
                x.BankProcessId,
                x.Description,
                x.ProcessDate,
                x.ProcessType,
                x.Amount,
                BankaAdi = x.Banks.BankTitle // İşte o sihirli dokunuş! Yabancı anahtar üzerinden gidip bankanın gerçek adını alıyoruz.
            }).ToList();

            dataGridView1.DataSource = values;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdateBankProcess_Click(object sender, EventArgs e)
        {
            int bankProcessId = int.Parse(txtBankProcessId.Text);
            string bankProcessDescription = txtBankDescription.Text;
            DateTime bankProcessesDate = DateTime.Parse(txtBankDate.Text);
            string bankProcessOption = txtBankOption.Text;
            decimal bankProcessAmount = decimal.Parse(txtBankAmount.Text);
            int selectedBankId = int.Parse(cmbBankOption.SelectedValue.ToString());

            var values = db.BankProcesses.Find(bankProcessId);

            values.Description = bankProcessDescription;
            values.ProcessDate = bankProcessesDate;
            values.ProcessType = bankProcessOption;
            values.Amount = bankProcessAmount;
            values.BankId = selectedBankId;


            db.SaveChanges();
            MessageBox.Show("Banka Hareketi Başarılı Bir Şekilde Güncellendi", "Banka Hareketleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var values2 = db.BankProcesses.ToList();
            dataGridView1.DataSource = values2;
        }
    }
}
