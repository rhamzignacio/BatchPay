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

namespace BatchPay.View
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string serverResponse = "";

            LoginService.TryLogin(txtBoxUsername.Text, txtBoxPassword.Text, out serverResponse);

            if (serverResponse == "")
            {
                MainWindow form = new MainWindow();

                form.ShowDialog();
            }
            else
                MessageBox.Show(serverResponse, "Error");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
