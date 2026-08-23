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
    }
}
