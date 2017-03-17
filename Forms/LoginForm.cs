using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressBase.Studio.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            SetSplashScreen();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthenticateResponse authResponse = null;

            try
            {
                var authClient = new JsonServiceClient("http://localhost:53125/");

                authResponse = authClient.Send(new Authenticate
                {
                    provider = "jwt",
                    UserName = txtUserName.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Meta = new Dictionary<string, string> { { "cid", txtClientId.Text.Trim() } },
                    UseTokenCookie = true
                });
            }
            catch (WebServiceException wse)
            {
            }

            if (authResponse != null && authResponse.ResponseStatus != null
                && authResponse.ResponseStatus.ErrorCode != "EbUnauthorized")
            {
                this.Hide();
                MainForm _mf = new MainForm(authResponse.BearerToken);
                _mf.Show();
            }
            else
                MessageBox.Show("Invalid UserName and/or Password!");

        }

        private SplashScreen _splashScreen;

        private void SetSplashScreen()
        {
            _splashScreen = new SplashScreen();

            //ResizeSplash();
            _splashScreen.Visible = true;
            _splashScreen.TopMost = true;

            Timer _timer = new Timer();
            _timer.Tick += (sender, e) =>
            {
                _splashScreen.Visible = false;
                _timer.Enabled = false;
            };
            _timer.Interval = 4000;
            _timer.Enabled = true;
        }
    }
}
