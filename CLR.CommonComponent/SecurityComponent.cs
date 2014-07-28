//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：加密/解密组件
// 作    者：王义波
// 创建时间：2014/7/28 10:29:33
// CLR 版本：1.1
//=====================================================
using System.ComponentModel;
using System.Windows.Forms;
using CLR.Security;

namespace CLR.CommonComponent
{
    public partial class SecurityComponent : Panel
    {
        private SecurityUtility _securityUtility;
        private string _privateKey, _publicKey;

        public SecurityComponent()
        {
            InitializeComponent();
        }

        public SecurityComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.cboSecurityMode.SelectedIndex = 0;
            RSACryptionHelper _rSACryptionHelper = new RSACryptionHelper();
            _rSACryptionHelper.RSAKey(out _privateKey, out _publicKey);
        }

        void SecurityComponent_SizeChanged(object sender, System.EventArgs e)
        {
            this.txtDestination.Height = this.txtSource.Height = this.panel1.Location.Y;
        }

        void btnEncrypt_Click(object sender, System.EventArgs e)
        {
            this.txtDestination.Text = this.Security(this.cboSecurityMode.SelectedItem.ToString(), SecurityMode.ENCRYPT, this.txtSource.Text);
        }

        void btnDecrypt_Click(object sender, System.EventArgs e)
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
