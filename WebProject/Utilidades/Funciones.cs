using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Utilidades
{
    public static class Funciones
    {
        public static void LimpiarCampos(List<Control> _iControls)
        {
            if (_iControls != null)
            {
                foreach (Control _control in _iControls)
                {
                    string strType = _control.GetType().Name;
                    switch (strType)
                    {
                        case "HiddenField":
                            HiddenField oHiddenField = (HiddenField)_control;
                            oHiddenField.Value = (float.TryParse(oHiddenField.Value.ToString(), out _)) ? Convert.ToString(0) : string.Empty;
                            break;
                        case "Label":
                            Label oLabel = (Label)_control;
                            oLabel.Text = string.Empty;
                            break;
                        case "TextBox":
                            TextBox oTextBox = (TextBox)_control;
                            oTextBox.Text = string.Empty;
                            break;
                        case "CheckBox":
                            CheckBox oCheckBox = (CheckBox)_control;
                            oCheckBox.Checked = false;
                            break;
                    }
                    if (strType != "HiddenField")
                    {
                        string _success = "is-valid";
                        string _error = "is-invalid";
                        WebControl _webControl = (WebControl)_control;
                        _webControl.CssClass = _webControl.CssClass.Replace(_error, string.Empty).Replace(_success, string.Empty);
                    }
                }
            }
        }

        public static bool ValidarCamposConRetorno(List<WebControl> _iWebControl, ref string _message)
        {
            bool r = true;
            bool _internal = true;
            string _returnMessage = "Debe completar los siguientes campos:<br/>";
            try
            {
                if (_iWebControl != null)
                {
                    foreach (WebControl _webControl in _iWebControl)
                    {
                        var oType = _webControl.GetType();

                        switch (oType.Name)
                        {
                            case "Label":
                                Label oLabel = (Label)_webControl;
                                if (oLabel.Text.Length == 0)
                                {
                                    r = false;
                                    _internal = false;
                                    _returnMessage += string.Format("{0}<br/>", oLabel.Attributes["data-name"].ToString());
                                }
                                else
                                {
                                    _internal = true;
                                }
                                break;
                            case "TextBox":
                                TextBox oTextBox = (TextBox)_webControl;
                                if (oTextBox.Text.Length == 0)
                                {
                                    r = false;
                                    _internal = false;
                                    _returnMessage += string.Format("{0}<br/>", oTextBox.Attributes["data-name"].ToString());
                                }
                                else
                                {
                                    _internal = true;
                                }
                                break;
                            case "DropDownList":
                                DropDownList oDropDownList = (DropDownList)_webControl;
                                if (oDropDownList.SelectedValue == string.Empty || oDropDownList.SelectedValue == Convert.ToString(0))
                                {
                                    r = false;
                                    _internal = false;
                                    _returnMessage += string.Format("{0}<br/>", oDropDownList.Attributes["data-name"].ToString());
                                }
                                else
                                {
                                    _internal = true;
                                }
                                break;

                            case "CheckBox":
                                CheckBox oCheckBox = (CheckBox)_webControl;
                                _internal = oCheckBox.Checked;
                                if (!_internal)
                                {
                                    r = false;
                                    _returnMessage += string.Format("{0}<br/>", oCheckBox.Attributes["data-name"].ToString());
                                }
                                break;
                        }
                        HacerValido(_webControl, _internal);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error ValidateFields: {0}.", ex.Message));
            }
            _message = _returnMessage;
            return r;
        }

        public static string GenerarQR(string strValor)
        {
            string r = string.Empty;
            try
            {
                QRCodeGenerator _qrCode = new QRCodeGenerator();
                QRCodeData _qrCodeData = _qrCode.CreateQrCode(strValor, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(_qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(30, Color.Goldenrod, Color.OrangeRed, true);

                using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, ImageFormat.Png);
                    byte[] byteImage = stream.ToArray();
                    r = Convert.ToBase64String(byteImage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        }

        public static void HacerValido(WebControl _control, bool _valid)
        {
            string _success = "is-valid";
            string _error = "is-invalid";
            string _class = _control.CssClass.Replace(_error, string.Empty).Replace(_success, string.Empty);
            if (_valid)
                _control.CssClass = string.Format("{0} {1}", _class, _success);
            else
                _control.CssClass = string.Format("{0} {1}", _class, _error);
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "P4$$w0Rd1nY0uRh34R7";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "P4$$w0Rd1nY0uRh34R7";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
