using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatchPay.Service;
using BatchPay.Model;

namespace BatchPay.View
{
    public partial class MainWindow : Form
    {
        List<BatchPayModel> batchData = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Status (string _message)
        {
            lblStatus.Text = _message;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            decimal total = 0;

            lstViewRecords.Items.Clear();

            if (cmbBoxCurrency.Text == "")
                lblStatus.Text = "ERROR -> Curreny is required";
            else if (dateTimeStart.Value.Date >= DateTime.Now)
                lblStatus.Text = "ERRPR -> Date must not be greater that today's date";
            else
            {
                Status("Retrieving Data");

                string serverResponse = "";

                batchData = BatchPayService.GetBatch(dateTimeStart.Value.Date, cmbBoxCurrency.Text, out serverResponse);

                if (batchData != null)
                {
                    batchData.ForEach(item =>
                    {
                        ListViewItem lvi = new ListViewItem(item.InvoiceNo);

                        if (item.CorpCardHolder != "" && item.CorpCardHolder != null)
                            lvi.SubItems.Add(item.CorpCardHolder);
                        else
                            lvi.SubItems.Add(item.IndividualCardHolder);

                        if (item.CorpCCMask != "" && item.CorpCCMask != null)
                            lvi.SubItems.Add(item.CorpCCMask);
                        else
                            lvi.SubItems.Add(item.IndividualCCMask);

                        lvi.SubItems.Add(string.Format("{0:0.00}", item.GrossAmount));

                        total += item.GrossAmount;

                        lstViewRecords.Items.Add(lvi);
                    });

                    btnSave.Enabled = true;

                    Status("Done total of " + batchData.Count.ToString() + " record(s)");

                    lblTotalAmount.Text = cmbBoxCurrency.Text + " " + string.Format("{0:0.00}", total);
                }
                else
                {
                    btnSave.Enabled = false;

                    Status("No record Found");
                }
            }
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            string serverResponse = "";
       
            if(dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                string mid = "";

                string rps = "";

                if (cmbBoxCurrency.Text == "PHP")
                {
                    mid = "09181924564";

                    rps = "GG";
                }
                else if (cmbBoxCurrency.Text == "USD")
                {
                    mid = "09181924572";

                    rps = "GH";
                }

                BatchPayService.Save(dialog.SelectedPath, mid, rps, batchData, out serverResponse);

                if (serverResponse != "")
                    lblStatus.Text = "Error - " + serverResponse;
                else
                    lblStatus.Text = "Successfully Saved";
            }
        }
    }
}