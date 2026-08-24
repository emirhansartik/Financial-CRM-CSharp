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
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities1 db = new FinancialCrmDbEntities1();
        int count = 0;
        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            var totalBalance = db.Banks.Sum(x=>x.BankBalance);
            lblTotalBalance.Text = totalBalance.ToString() + " ₺";
            timer1.Start();

            var lastBankProcessAmount = db.BankProcesses.OrderByDescending(x => x.BankProcessId).Take(1).Select(y => y.Amount).FirstOrDefault();
            lblLastBankProcessAmount.Text = lastBankProcessAmount.ToString() + " ₺";


            // Chart 1 Kodları
            var bankData = db.Banks.Select(x => new
            {
                x.BankTitle,
                x.BankBalance
            }).ToList();
            chart1.Series.Clear();
            var series = chart1.Series.Add("Series1");
            foreach(var item in bankData)
            {
                series.Points.AddXY(item.BankTitle, item.BankBalance);
            }

            // Chart 2 Kodları
            var billData = db.Bills.Select(x => new 
            {
                
                x.BillTitle,
                x.BillAmount
            
            }).ToList();
            chart2.Series.Clear();
            var series2 = chart2.Series.Add("Faturalar");
            series2.ChartType=System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            foreach (var item in billData) 
            {
                series2.Points.AddXY(item.BillTitle, item.BillAmount);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if (count % 4 == 1)
            {
                var elektirikFaturasi = db.Bills.Where(x=>x.BillTitle == "Elektirik Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Elektirik Faturası";
                lblBillAmount.Text = elektirikFaturasi.ToString() + " ₺"; 
            }


            if (count % 4 == 2)
            {
                var elektirikFaturasi = db.Bills.Where(x => x.BillTitle == "Doğalgaz Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Doğalgaz Faturası";
                lblBillAmount.Text = elektirikFaturasi.ToString() + " ₺";
            }

            if (count % 4 == 3)
            {
                var elektirikFaturasi = db.Bills.Where(x => x.BillTitle == "Su Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Su Faturası";
                lblBillAmount.Text = elektirikFaturasi.ToString() + " ₺";
            }

            if (count % 4 == 0)
            {
                var elektirikFaturasi = db.Bills.Where(x => x.BillTitle == "İnternet Faturası").Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "İnternet Faturası";
                lblBillAmount.Text = elektirikFaturasi.ToString() + " ₺";
            }
        }

        private void frmBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.Show();
            this.Hide();
        }

        private void btnBillForm_Click(object sender, EventArgs e)
        {
            FrmBilling frm = new FrmBilling();
            frm.Show();
            this.Hide();
        }

        private void btnCategoriesForm_Click(object sender, EventArgs e)
        {
            FrmCategories frm = new FrmCategories();
            frm.Show();
            this.Hide();
        }

        private void btnBankProcessForm_Click(object sender, EventArgs e)
        {
            FrmBankProcess frm = new FrmBankProcess();
            frm.Show();
            this.Hide();

        }
    }
}
