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

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            string serverResponse = "";

            batchData = BatchPayService.GetBatch(dateTimeStart.Value.Date, dateTimeEnd.Value.Date, out serverResponse);

            batchData.ForEach(item =>
            {
                ListViewItem lvi = new ListViewItem(item.InvoiceNo);

                lvi.SubItems.Add(item.CardHolder);

                lvi.SubItems.Add(item.Reference);

                lvi.SubItems.Add(string.Format("{0:0.00}", item.GrossAmount));

                lstViewRecords.Items.Add(lvi);
            });
        }
    }
}
