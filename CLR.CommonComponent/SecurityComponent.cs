//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：加密/解密组件
// 作    者：王义波
// 创建时间：2014/7/30 10:29:33
// CLR 版本：1.2
//=====================================================
using System.ComponentModel;
using System.Windows.Forms;
using CLR.Security;

namespace CLR.CommonComponent
{
    public enum SecurityComponentMode
    {
        Base64,
        DES,
        MD5,
        RSACryption,
        TripleDES
    }

    public partial class SecurityComponent : Panel
    {
        private SecurityComponentMode _securityComponentMode = SecurityComponentMode.Base64;
        [Description("加密/解密的类型")]
        public SecurityComponentMode SecurityComponentMode
        {
            get { return _securityComponentMode; }
            set { _securityComponentMode = value; }
        }

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
            RSACryptionHelper _rSACryptionHelper = new RSACryptionHelper();
            _rSACryptionHelper.RSAKey(out _privateKey, out _publicKey);
        }

        void SecurityComponent_SizeChanged(object sender, System.EventArgs e)
        {
            this.txtDestination.Height = this.txtSource.Height = this.panel1.Location.Y;
        }

        void btnEncrypt_Click(object sender, System.EventArgs e)
        {
            this.txtDestination.Text = this.Security(SecurityMode.ENCRYPT, this.txtSource.Text);
        }

        void btnDecrypt_Click(object sender, System.EventArgs e)
        {
            this.txtDestination.Text = this.Security(SecurityMode.DECRYPT, this.txtSource.Text);
        }

        protected string Security(SecurityMode securityMode, string source)
        {
            switch (_securityComponentMode)
            {
                case SecurityComponentMode.Base64:
                    _securityUtility = new Base64Helper();
                    break;
                case SecurityComponentMode.DES:
                    _securityUtility = new DESHelper();
                    break;
                case SecurityComponentMode.MD5:
                    _securityUtility = new MD5Helper();
                    break;
                case SecurityComponentMode.RSACryption:
                    _securityUtility = new RSACryptionHelper(_privateKey, _publicKey);
                    break;
                case SecurityComponentMode.TripleDES:
                    _securityUtility = new TripleDESHelper();
                    break;
                default:
                    break;
            }
            return _securityUtility.SecurityUtilityInvoke(securityMode).Invoke(source);
        }
    }
}
