using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom
{
    /// <summary>
    /// Форма для добавления узла
    /// </summary>
    public partial class AddNodeForm : Form
    {
        private string re;

        public AddNodeForm(bool isTruss)
        {
            InitializeComponent();
            if (isTruss)
            {
                radioButton1.Checked = true;
                groupBox2.Enabled = false;
                checkBox3.Enabled = false;
                textBox5.Enabled = false;
                label6.Enabled = false;
            }
            else
                radioButton2.Checked = true;
        }

        private void AddNodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Activate();
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;
            e.Cancel = true;
            Visible = false;
        }

        private void AddNodeBtn_Click(object sender, EventArgs e)
        {
            var frm = (MainForm)Owner;
           
            var fixX = checkBox1.Checked;
            var fixY = checkBox2.Checked;
            var fixA = checkBox3.Checked;
            if (double.TryParse(textBox1.Text.Replace(".", ","), out var x) &&
                double.TryParse(textBox2.Text.Replace(".", ","), out var y) )
            {
                if (double.TryParse(textBox3.Text.Replace(".", ","), out var px) &&
                    double.TryParse(textBox4.Text.Replace(".", ","), out var py) &&
                    double.TryParse(textBox5.Text.Replace(".", ","), out var pa))
                {
                    var type = NodeType.Rigid;
                    if (radioButton1.Checked)
                        type = NodeType.Hinge;
                    frm.AddNodeToModel(x, y, type, fixX, fixY, fixA, px, py, pa, re);
                }


                else
                {
                    frm.ErrorMsg("Введите корректные нагрузки", "Ошибка при создании узла");
                }
            }
            else
            {
                frm.ErrorMsg("Введите корректные координаты", "Ошибка при создании узла");
            }
        }
        
        private void CheckValidControl()
        {
            addNodeBtn.Enabled = textBox1.Text != "" && textBox2.Text != "";
            if (radioButton1.Checked)
            {
                checkBox3.Checked = false;
                checkBox3.Enabled = false;
                textBox5.Enabled = false;
                label6.Enabled = false;
            }
            else
            {
                checkBox3.Enabled = true;
                textBox5.Enabled = true;
                label6.Enabled = true;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            CheckValidControl();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            CheckValidControl();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CheckValidControl();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CheckValidControl();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            CheckValidControl();
        }
    }
}