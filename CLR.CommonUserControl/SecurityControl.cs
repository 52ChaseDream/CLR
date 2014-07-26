using System.Windows.Forms;
using CLR.Security;
using System;

namespace CLR.CommonUserControl
{
    public partial class SecurityControl : UserControl
    {
        private SecurityUtility _securityUtility;

        public SecurityControl()
        {
            InitializeComponent();
            this.cboSecurityMode.SelectedIndex = 0;
        }

        private void btnDecrypt_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.txtDestination.Text = this.Security(this.cboSecurityMode.SelectedItem.ToString(), SecurityMode.DECRYPT, this.txtSource.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("该功能暂时还未实现！！！");
            }

        }

        protected string Security(string securityControlMode, SecurityMode securityMode, string source)
        {
            switch (securityControlMode)
            {
                case "Base64":
                    _securityUtility = new Base64Helper();
                    break;
                case "DES":
                    _securityUtility = new DESHelper();
                    break;
                case "MD5":
                    _securityUtility = new MD5Helper();
                    break;
                case "RSACryption":
                    _securityUtility = new RSACryptionHelper();
                    break;
                case "TripleDES":
                    _securityUtility = new TripleDESHelper();
                    break;
                default:
                    break;
            }
            return _securityUtility.SecurityUtilityInvoke(securityMode).Invoke(source);
        }
    }
}
