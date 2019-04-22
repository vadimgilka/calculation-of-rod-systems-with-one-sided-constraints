using System;
using System.Text;
using System.Windows.Forms;

namespace Diplom
{
    /// <summary>
    /// Форма для добавления стержня
    /// </summary>
    public partial class AddCoreForm : Form
    {
        public AddCoreForm(bool isTruss)
        {
            InitializeComponent();
            if (isTruss)
            {
                label8.Enabled = false;
                label5.Enabled = false;
                textBox5.Enabled = false;
            }

            numericUpDown1.Maximum = 5000;
            numericUpDown2.Maximum = 5000;
        }

        private void AddCoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Activate();
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;
            e.Cancel = true;
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = (MainForm) Owner;
            var start = Convert.ToInt32(numericUpDown1.Value);
            var end = Convert.ToInt32(numericUpDown2.Value);
            if (double.TryParse(textBox1.Text.Replace(".", ","), out var ee) &&
                double.TryParse(textBox5.Text.Replace(".", ","), out var i) &&
                double.TryParse(textBox4.Text.Replace(".", ","), out var f))
                if (start > 0 && end > 0 && ee > 0.0000000001 && f > 0.0000000001 && i > 0.0000000001)
                    frm.AddCoreToModel(start, end, ee, f, i);
                else
                {
                    var sb = new StringBuilder("Заполните свойства корректно:");
                    if (start == end)
                        sb.Append("\nначало и конец стержня должны находиться в разных узлах;");
                    if (i< 0.0000000001)
                        sb.Append("\nмомент инерции должен быть больше 0;");
                    if (f< 0.0000000001)
                        sb.Append("\nплощадь должна быть больше 0;");
                    if (ee< 0.0000000001)
                        sb.Append("\nмодуль упругости должен быть больше 0;");
                    frm.ErrorMsg(
                        sb.ToString(),
                        "Ошибка при создании стержня");
                }
            else
            {
                frm.ErrorMsg("Заполните поля корректно", "Ошибка при создании стержня");
            }
        }
    }
}