using System;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace Diplom
{
    /// <summary>
    /// Выводит отчет о расчете схемыф
    /// </summary>
    public partial class ResultForm : Form
    {
        public ResultForm(Matrix<double> classic, Matrix<double> lu, double condNum, int nodesCount, int[] fixedNodes,
            bool truss)
        {
            InitializeComponent();
            if (truss)
            {
                int n = 1;
                int j = 0;
                int i;
                //Если узел был зафиксирован по оси, его изменения по этой оси пропущены в отчете, т.к. он не изменялся.
                for (i = 0; i < nodesCount * 2; i += 2)
                {
                    if (!fixedNodes.Contains(i))
                    {
                        dataGridView1.Rows.Add(n + "X", Math.Round(classic[j, 0], 5), Math.Round(lu[j, 0], 5),
                            Math.Round(Math.Abs(classic[j, 0] - lu[j, 0])), 5);
                        j++;
                    }

                    if (!fixedNodes.Contains(i + 1))
                    {
                        dataGridView1.Rows.Add(n + "Y", Math.Round(classic[j, 0], 5), Math.Round(lu[j, 0], 5),
                            Math.Round(Math.Abs(classic[j, 0] - lu[j, 0])), 5);
                        j++;
                    }

                    n++;
                }

                n = 1;
                for (i = j; i < classic.RowCount; i++)
                {
                    dataGridView1.Rows.Add(n+"N", Math.Round(classic[i, 0], 5), Math.Round(lu[i, 0], 5),
                        Math.Round(Math.Abs(classic[i, 0] - lu[i, 0])), 5);
                    n++;
                }

                label1.Text = @"Число обусловленности: " + Math.Round(condNum, 5);
            }
            else
            {
                int n = 1;
                int j = 0;
                int i;
                //Если узел был зафиксирован по оси, его изменения по этой оси пропущены в отчете, т.к. он не изменялся.
                for (i = 0; i < nodesCount * 3; i += 3)
                {
                    if (!fixedNodes.Contains(i))
                    {
                        dataGridView1.Rows.Add(n + "X", Math.Round(classic[j, 0], 5), Math.Round(lu[j, 0], 5),
                            Math.Round(Math.Abs(classic[j, 0] - lu[j, 0])), 5);
                        j++;
                    }

                    if (!fixedNodes.Contains(i + 1))
                    {
                        dataGridView1.Rows.Add(n + "Y", Math.Round(classic[j, 0], 5), Math.Round(lu[j, 0], 5),
                            Math.Round(Math.Abs(classic[j, 0] - lu[j, 0])), 5);
                        j++;
                    }

                    if (!fixedNodes.Contains(i + 2))
                    {
                        dataGridView1.Rows.Add(n + "UZ", Math.Round(classic[j, 0], 5), Math.Round(lu[j, 0], 5),
                            Math.Round(Math.Abs(classic[j, 0] - lu[j, 0])), 5);
                        j++;
                    }

                    n++;
                }

                n = 1;
                for (i = j; i < classic.RowCount; i += 3)
                {
                    dataGridView1.Rows.Add(n + "N", Math.Round(classic[i, 0], 5), Math.Round(lu[i, 0], 5),
                        Math.Round(Math.Abs(classic[i, 0] - lu[i, 0])), 5);
                    if (i+1==classic.RowCount)
                        continue;
                    dataGridView1.Rows.Add(n + "Qy", Math.Round(classic[i + 1, 0], 5), Math.Round(lu[i + 1, 0], 5),
                        Math.Round(Math.Abs(classic[i + 1, 0] - lu[i + 1, 0])), 5);
                    if (i+2 == classic.RowCount)
                        continue;
                    dataGridView1.Rows.Add(n + "Mz", Math.Round(classic[i + 2, 0], 5), Math.Round(lu[i + 2, 0], 5),
                        Math.Round(Math.Abs(classic[i + 2, 0] - lu[i + 2, 0])), 5);
                    n++;
                }

                label1.Text = @"Число обусловленности: " + Math.Round(condNum, 5);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}