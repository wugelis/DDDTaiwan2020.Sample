using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanArchitectureCQRSTemplate.Forms
{
    public partial class frmApplicationLayerWizard : Form
    {
        public frmApplicationLayerWizard()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            bool result = Validation();
            if(result)
            {
                Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private bool Validation()
        {
            string errMsg = string.Empty;

            if(chkQueryCommand.Checked)
            {
                if ((errMsg = (ValidatorControl(txtQueryCommandName, "請輸入 Query Command 的名稱"))) != string.Empty)
                {
                    MessageBox.Show(errMsg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                    
                if ((errMsg = ValidatorControl(txtQueryDtoName, "請輸入 DTO 的名稱！")) != string.Empty)
                {
                    MessageBox.Show(errMsg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            bool result = chkQueryCommand.Checked | chkCreateCommand.Checked | chkUpdateCommand.Checked | chkDeleteCommand.Checked;
            if(!result)
            {
                MessageBox.Show("請選擇任一個要建立的 CQRS Command 項目！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return string.IsNullOrEmpty(errMsg);
        }

        private string ValidatorControl(Control control, string msg)
        {
            string resultMsg = string.Empty;

            if(control is TextBox)
            {
                TextBox textBox = control as TextBox;
                if(textBox.Text.Trim() =="")
                {
                    resultMsg = $"{msg}";
                    textBox.Focus();
                }
                
            }

            return resultMsg;
        }

        #region 公開的屬性
        /// <summary>
        /// Query Command 的 CheckBox 選擇
        /// </summary>
        public bool ChkQueryCommandState
        {
            get
            {
                return this.chkQueryCommand.Checked;
            }
        }
        /// <summary>
        /// Create Command 的 CheckBox 選擇
        /// </summary>
        public bool ChkCreateCommandState
        {
            get
            {
                return this.chkCreateCommand.Checked;
            }
        }
        /// <summary>
        /// Delete Command 的 CheckBox 選擇
        /// </summary>
        public bool ChkDeleteCommandState
        {
            get
            {
                return this.chkDeleteCommand.Checked;
            }
        }
        /// <summary>
        /// Update Command 的 CheckBox 選擇
        /// </summary>
        public bool ChkUpdateCommandState
        {
            get
            {
                return this.chkUpdateCommand.Checked;
            }
        }
        /// <summary>
        /// 取得 Query Command 名稱
        /// </summary>
        public string GetQueryCommandName
        {
            get
            {
                return this.txtQueryCommandName.Text;
            }
        }
        /// <summary>
        /// 取得 Query DTO 名稱
        /// </summary>
        public string GetQueryDtoName
        {
            get
            {
                return this.txtQueryDtoName.Text;
            }
        }
        #endregion

        private void btnQueryDTO_Click(object sender, EventArgs e)
        {
            frmGetDtoModel frmDto = new frmGetDtoModel();
            GlobalVar.GetDtoModelForm = frmDto;
            Tuple<DialogResult, string> result = frmDto.ShowDialog();
            if(result.Item1 == DialogResult.OK)
            {
                txtQueryDtoName.Text = result.Item2;
            }
        }
    }
}
