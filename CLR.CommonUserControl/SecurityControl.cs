//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：自定义加密/解密控件
// 作    者：王义波
// 创建时间：2014/7/28 10:29:33
// CLR 版本：1.2
//=====================================================
using System.Windows.Forms;
using CLR.Security;

namespace CLR.CommonUserControl
{
    public partial class SecurityControl : UserControl
    {
        private SecurityUtility _securityUtility;
        private string _privateKey, _publicKey;

        public SecurityControl()
        {
            InitializeComponent();
            this.cboSecurityMode.SelectedIndex = 0;
            RSACryptionHelper _rSACryptionHelper = new RSACryptionHelper();
            _rSACryptionHelper.RSAKey(out _privateKey, out _publicKey);
        }

        private void btnDecrypt_Click(object sender, System.EventArgs e)
        {
            this.txtDestination.Text = this.Security(this.cboSecurityMode.SelectedItem.ToString(), SecurityMode.DECRYPT, this.txtSource.Text);
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
                    _securityUtility = new RSACryptionHelper(_privateKey, _publicKey);
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
