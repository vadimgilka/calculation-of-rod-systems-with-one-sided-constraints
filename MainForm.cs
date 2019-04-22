using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace Diplom
{
    /// <summary>
    /// Основная форма программы
    /// </summary>
    public partial class MainForm : Form
    {
        private Presenter _presenter;
        private AddNodeForm _addNodeForm;   //форма редактирования узла
        private AddCoreForm _addCoreForm;   //форма добавления стержня
        private bool _coresEditing; //определяет, активен ли режим редактирования стержней
        private bool _nodesEditing; //определяет, активен ли режим редактирования узлов
        private bool _readFromFile; //определяет, производится ли чтение из файла
        private bool _hideResult;   //определяет, скрывать ли визуализацию результата расчета
        private bool _hideScheme;   //определяет, скрывать ли визуализацию схемы
        public bool Truss { get; set; } //определяет тип схемы
        private bool _displayingResult; //определяет, отрисовывать ли новый вид схемы
        private float _scaling = 100;   //коэффициент масштабирования
        private int _cx, _cy;           // суммарные смещения по осям
        private int _shiftX;            //текущие смещения по осям
        private int _shiftY;
        private List<int> _editedNodesIds = new List<int>(); //идентификаторы элементов, которые были отредактированы
        private List<int> _editedCoresIds = new List<int>();
        public string FileName { get; set; }
        private bool _unsavedChanges;   //определяет, имеются ли несохраненные изменения в схеме
        private bool _firstErrorShown;  //определяет, была ли показана ошибка при чтении из файла (во избежание отображения большого количества ошибок)
        private bool _onewayConnect = false;  //определяет активен режим строиной связь(одностороиной связь)
        private bool _twowayConnect = false;  //определяет активен режим строиной связь(двухстороиной связь)

        public MainForm()
        {
            InitializeComponent();
            //привязываем для масштабирования визуализации колесиком:
            _schemeGraphic.MouseWheel += SchemeGraphic_MouseWheel;
            _schemeGraphic.Select();
            _schemeGraphic.Image = new Bitmap(1000, 1000);
        }

        private void addNodeBtn_Click(object sender, EventArgs e)
        {
            _addNodeForm.Owner = this;
            _addNodeForm.Show();
        }

        //Методы для добавления и редактирования элементов схемы
        public void AddNodeToModel(double x, double y, NodeType type, bool fixX, bool fixY, bool fixA, double px,
            double py, double pa, string re)
        {
            if (
                _presenter.AddNode(x, y, type, fixX, fixY, fixA, px, py, pa,re))
            {
                if (!_unsavedChanges)
                {
                    _unsavedChanges = true;
                    Text += @" *";
                }
            }
        }

        public bool ChangeNodeInModel(int id, double x, double y, NodeType type, bool fixX, bool fixY, bool fixA,
            double px,
            double py, double pa, string re)
        {
            if (_presenter.ChangeNode(id, x, y, type, fixX, fixY, fixA, px, py, pa,re))
            {
                if (!_unsavedChanges)
                {
                    _unsavedChanges = true;
                    Text += @" *";
                }

                return true;
            }

            return false;
        }

        public void AddCoreToModel(int start, int end, double e, double f, double i)
        {
            if (_presenter.AddCore(start, end, e, f, i))
            {
                if (!_unsavedChanges)
                {
                    _unsavedChanges = true;
                    Text += @" *";
                }
            }
        }

        public bool ChangeCoreInModel(int start, int end, double e, double f, double i, int id)
        {
            if (_presenter.ChangeCore(start, end, e, f, i, id))
            {
                if (!_unsavedChanges)
                {
                    _unsavedChanges = true;
                    Text += @" *";
                }

                return true;
            }

            return false;
        }

        //Методы для отображения узлов и стержней в таблицах справа
        public void DisplayCoreInGrid(int id, int start, int end, double e, double f, double i)
        {
            coresGrid.Rows.Add(null, id, start, end, e, f, i);
            _schemeGraphic.Invalidate();
        }

        public void DisplayNodeInGrid(int id, double x, double y, NodeType type, bool fixX, bool fixY, bool fixA,
            double px,
            double py, double pa, string re)
        {
            int nType = 1;
            if (type == NodeType.Hinge)
                nType = 0;
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(nodesGrid);
            nodesGrid.Rows.Add(null, id, x, y, (row.Cells[4] as DataGridViewComboBoxCell).Items[nType], fixX, fixY,
                fixA, px, py, pa,re);
            if (type == NodeType.Hinge)
                row.Cells[7].ReadOnly = true;
            _schemeGraphic.Invalidate();
        }

        private void SchemeGraphic_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0 && _scaling < 100000)
            {
                _scaling *= 1.1F;
            }
            else if (e.Delta < 0 && _scaling > 0.001)
            {
                _scaling *= 0.9F;
            }

            _schemeGraphic.Invalidate();
        }

        private void SchemeGraphic_MouseDown(object sender, MouseEventArgs e)
        {
            _shiftX = e.X;
            _shiftY = e.Y;
        }

        private void SchemeGraphic_MouseMove(object sender, MouseEventArgs e)
        {
            if (_schemeGraphic.Capture)
            {
                int dx = e.X - _shiftX;
                int dy = e.Y - _shiftY;
                _cx += dx;
                _cy += dy;
                _shiftX = e.X;
                _shiftY = e.Y;
                _schemeGraphic.Invalidate();
            }
        }

        private void AddCoreBtn_Click(object sender, EventArgs e)
        {
            _addCoreForm.Owner = this;
            _addCoreForm.Show();
        }

        private void CalculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_coresEditing)
            {
                ErrorMsg("Завершите редактирование стержней", "Невозможно начать расчет");
                return;
            }

            if (_nodesEditing)
            {
                ErrorMsg("Завершите редактирование узлов", "Невозможно начать расчет");
                return;
            }

            //try
            //{
                //Для Двухстроной связь

                    _presenter.Calculate();
 
            //}
            //catch (Exception ex)
            //{
            //    ErrorMsg("Проверьте корректность схемы.", "Произошла ошибка при расчете");
            //}
        }

        private void EditCoresBtn_Click(object sender, EventArgs e)
        {
            _displayingResult = false;
            _hideScheme = false;
            if (_nodesEditing)
            {
                ErrorMsg("Завершите редактирование узлов", "Невозможно начать редактирование стержней");
                return;
            }

            if (!_coresEditing)
            {
                coresGrid.Columns[0].Visible = true;
                editCoresBtn.Text = @"Сохранить";
                coresGrid.ReadOnly = false;
                coresGrid.Columns[1].ReadOnly = true;
                _coresEditing = true;
            }
            else
            {
                SaveCoresFromGrid();
            }
        }

        /// <summary>
        /// Сохраняет изменения в таблице стержней в модель
        /// </summary>
        private void SaveCoresFromGrid()
        {
            for (var current = 0; current < coresGrid.RowCount; current++)
                if (_editedCoresIds.Contains(Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value)))
                {
                    int start = 0;
                    int end = 0;
                    double ee = 0;
                    double f = 0;
                    double i = 0;
                    bool ok = false;
                    try
                    {
                        if (double.TryParse(coresGrid.Rows[current].Cells["coreE"].Value.ToString().Replace(".", ","),
                                out ee) && double.TryParse(
                                coresGrid.Rows[current].Cells["coreI"].Value.ToString().Replace(".", ","),
                                out i) && int.TryParse(coresGrid.Rows[current].Cells["coreStart"].Value.ToString(),
                                out start) && int.TryParse(coresGrid.Rows[current].Cells["coreEnd"].Value.ToString(),
                                out end))
                        {
                            if (start == end)
                            {
                                ErrorMsg(@"Начало и конец стержня не могут совпадать",
                                    @"Ошибка при изменении стержня " +
                                    Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value));
                                continue;
                            }

                            if (start == end)
                            {
                                ErrorMsg(@"Начальный и конечный узлы должны существовать",
                                    @"Ошибка при изменении стержня " +
                                    Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value));
                                continue;
                            }

                            if (double.TryParse(
                                coresGrid.Rows[current].Cells["coreF"].Value.ToString().Replace(".", ","), out f))
                                ok = true;
                            else
                                ErrorMsg(@"Заполните все поля корректно",
                                    @"Ошибка при изменении стержня " +
                                    Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value));
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        ErrorMsg(@"Заполните все поля",
                            @"Ошибка при изменении стержня " +
                            Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value));
                    }

                    if (!ok) continue;
                    if (
                        ChangeCoreInModel(start, end, ee, f, i,
                            Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value)))
                        _editedCoresIds.RemoveAll(id =>
                            id == Convert.ToInt32(coresGrid.Rows[current].Cells["coreId"].Value));
                }

            if (_editedCoresIds.Count == 0)
            {
                coresGrid.Columns[0].Visible = false;
                editCoresBtn.Text = @"Редактировать";
                coresGrid.ReadOnly = true;
                _coresEditing = false;
            }

            _schemeGraphic.Invalidate();
        }

        private void EditNodesBtn_Click(object sender, EventArgs e)
        {
            _displayingResult = false;
            _hideScheme = false;
            if (_coresEditing)
            {
                ErrorMsg("Завершите редактирование стержней", "Невозможно начать редактирование узлов");
                return;
            }

            if (!_nodesEditing)
            {
                nodesGrid.Columns[0].Visible = true;
                editNodesBtn.Text = @"Сохранить";
                nodesGrid.ReadOnly = false;
                nodesGrid.Columns[1].ReadOnly = true;
                if (Truss)
                {
                    nodesGrid.Columns[4].ReadOnly = true;
                    nodesGrid.Columns[7].ReadOnly = true;
                }

                _nodesEditing = true;
            }
            else
            {
                SaveNodesFromGrid();
            }
        }

        /// <summary>
        /// Сохраняет изменения в таблице узлов в модель
        /// </summary>
        private void SaveNodesFromGrid()
        {
            for (var current = 0; current < nodesGrid.RowCount; current++)
                if (_editedNodesIds.Contains(Convert.ToInt32(nodesGrid.Rows[current].Cells["nodeId"].Value)))
                {

                    var fixX = (bool)nodesGrid.Rows[current].Cells["nodeFixX"].Value;
                    var fixY = (bool)nodesGrid.Rows[current].Cells["nodeFixY"].Value;
                    var fixA = (bool)nodesGrid.Rows[current].Cells["nodeFixA"].Value;
                    var re = (string)nodesGrid.Rows[current].Cells["reactionY"].Value;
                    try
                    {

                        if (double.TryParse(nodesGrid.Rows[current].Cells["nodeX"].Value.ToString().Replace(".", ","),
                                out var x) && double.TryParse(
                                nodesGrid.Rows[current].Cells["nodeY"].Value.ToString().Replace(".", ","),
                                out var y) && double.TryParse(
                                nodesGrid.Rows[current].Cells["nodePx"].Value.ToString().Replace(".", ","),
                                out var px) && double.TryParse(
                                nodesGrid.Rows[current].Cells["nodePy"].Value.ToString().Replace(".", ","),
                                out var py) && double.TryParse(
                                nodesGrid.Rows[current].Cells["nodePa"].Value.ToString().Replace(".", ","),
                                out var pa) && ((string)nodesGrid.Rows[current].Cells["reactionY"].Value =="1" || (string)nodesGrid.Rows[current].Cells["reactionY"].Value == "2"|| (string)nodesGrid.Rows[current].Cells["reactionY"].Value == "0")
                                )
                        {
                            var type = NodeType.Rigid;
                            var ttii = (string)nodesGrid.Rows[current].Cells["reactionY"].Value;
                            //re = node;


                            if ((string) nodesGrid.Rows[current].Cells["nodeType"].Value == "Шарнир")
                                type = NodeType.Hinge;
                            if (
                                ChangeNodeInModel(Convert.ToInt32(nodesGrid.Rows[current].Cells["nodeId"].Value), x, y,
                                    type, fixX, fixY, fixA, px, py, pa, re))
                                _editedNodesIds.RemoveAll(id =>
                                    id == Convert.ToInt32(nodesGrid.Rows[current].Cells["nodeId"].Value));
                        }
                        else
                        {
                            ErrorMsg(@"Заполните все поля корректно",
                                @"Ошибка при изменении узла " +
                                Convert.ToInt32(nodesGrid.Rows[current].Cells["nodeId"].Value));
                        }


                    }
                    catch (NullReferenceException ex)
                    {
                        ErrorMsg(@"Заполните все поля",
                            @"Ошибка при изменении узла " +
                            Convert.ToInt32(nodesGrid.Rows[current].Cells["nodeId"].Value));
                    }
                }

            if (_editedNodesIds.Count == 0)
            {
                nodesGrid.Columns[0].Visible = false;
                editNodesBtn.Text = @"Редактировать";
                nodesGrid.ReadOnly = true;
                _nodesEditing = false;
            }

            _schemeGraphic.Invalidate();
        }

        /// <summary>
        /// Удаляет строку с узлом при клике на крестик
        /// </summary>
        private void NodesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView) sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (_presenter.RemoveNode(Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[1].Value)))
                {
                    nodesGrid.Rows.RemoveAt(e.RowIndex);
                    _editedNodesIds.RemoveAll(id => id == Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[1].Value));
                    for (int i = 0; i < _editedNodesIds.Count; i++)
                    {
                        if (_editedNodesIds[i] > Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[1].Value))
                            _editedNodesIds[i]--;
                    }

                    for (int i = e.RowIndex; i < senderGrid.RowCount; i++)
                    {
                        senderGrid.Rows[i].Cells[1].Value = Convert.ToInt32(senderGrid.Rows[i].Cells[1].Value) - 1;
                    }

                    for (int i = 0; i < coresGrid.RowCount; i++)
                    {
                        if (Convert.ToInt32(coresGrid.Rows[i].Cells[2].Value) > e.RowIndex)
                            coresGrid.Rows[i].Cells[2].Value = Convert.ToInt32(coresGrid.Rows[i].Cells[2].Value) - 1;
                        if (Convert.ToInt32(coresGrid.Rows[i].Cells[3].Value) > e.RowIndex)
                            coresGrid.Rows[i].Cells[3].Value = Convert.ToInt32(coresGrid.Rows[i].Cells[3].Value) - 1;
                    }

                    if (!_unsavedChanges)
                    {
                        _unsavedChanges = true;
                        Text += @" *";
                    }

                    _schemeGraphic.Invalidate();
                }
            }
        }

        /// <summary>
        /// Удаляет строку со стержнем при клике на крестик
        /// </summary>
        private void CoresGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView) sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                _presenter.RemoveCore(Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[1].Value));
                coresGrid.Rows.RemoveAt(e.RowIndex);
                _editedCoresIds.RemoveAll(id => id == Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[1].Value));
                for (int i = 0; i < _editedCoresIds.Count; i++)
                {
                    if (_editedCoresIds[i] > Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells[1].Value))
                        _editedCoresIds[i]--;
                }

                for (int i = e.RowIndex; i < senderGrid.RowCount; i++)
                {
                    senderGrid.Rows[i].Cells[1].Value = Convert.ToInt32(senderGrid.Rows[i].Cells[1].Value) - 1;
                }

                if (!_unsavedChanges)
                {
                    _unsavedChanges = true;
                    Text += @" *";
                }

                _schemeGraphic.Invalidate();
            }
        }

        /// <summary>
        /// Запоминает, что стержень был изменен
        /// </summary>
        private void CoresGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Если режим редактирования выключен или ивент возник в результате удаления строки - выходим, не запоминая элемент
            if (!_coresEditing || e.ColumnIndex == 1) return;
            var senderGrid = (DataGridView) sender;
            var editedId = (int) senderGrid.Rows[e.RowIndex].Cells[1].Value;
            if (!_editedCoresIds.Contains(editedId))
                _editedCoresIds.Add(editedId);
        }

        /// <summary>
        /// Запоминает, что узел был изменен
        /// </summary>
        private void NodesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Если режим редактирования выключен или ивент возник в результате удаления строки - выходим, не запоминая элемент
            if (!_nodesEditing || e.ColumnIndex == 1) return;
            var senderGrid = (DataGridView) sender;
            var editedId = (int) senderGrid.Rows[e.RowIndex].Cells[1].Value;
            if (!_editedNodesIds.Contains(editedId))
                _editedNodesIds.Add(editedId);
        }

        /// <summary>
        /// Отображает окно об ошибке
        /// </summary>
        public void ErrorMsg(string text, string title)
        {
            if (_readFromFile && !_firstErrorShown)
            {
                MessageBox.Show(
                    text,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                _firstErrorShown = true;
            }
            else
            {
                MessageBox.Show(
                    text,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Подготавливает элементы для работы с фермой
        /// </summary>
        private void InitializeTruss()
        {
            toolStripButton5.Enabled = true;
            coresGrid.Rows.Clear();
            nodesGrid.Rows.Clear();
            coresGrid.Columns[6].Visible = false;
            nodesGrid.Columns[7].Visible = false;
            nodesGrid.Columns[10].Visible = false;
            nodesGrid.Columns[4].ReadOnly = true;
            _displayingResult = false;
            _presenter = new Presenter(this, new SchemeModel(SchemeModel.SchemeType.Truss));
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem.Enabled = true;
            tabControl1.Visible = true;
            Truss = true;
            if (_addNodeForm != null)
                _addNodeForm.Close();
            if (_addCoreForm != null)
                _addCoreForm.Close();
            _addNodeForm = new AddNodeForm(true);
            _addCoreForm = new AddCoreForm(true);
            _unsavedChanges = false;
            resetScalingButton.Enabled = true;
            ResetScaling();
        }

        private void CreateTruss_Click(object sender, EventArgs e)
        {
            if (!CheckUnsavedChanges()) return;
            Text = @"Расчет плоских стержневых систем - Новая ферма";
            FileName = null;
            InitializeTruss();
        }

        private void TwoWay_Click(object sender,EventArgs e)
        {


        }

        /// <summary>
        /// Подготавливает элементы для работы с рамой
        /// </summary>
        private void InitializeFrame()
        {
            toolStripButton5.Enabled = true;
            coresGrid.Rows.Clear();
            nodesGrid.Rows.Clear();
            coresGrid.Columns[6].Visible = true;
            nodesGrid.Columns[7].Visible = true;
            nodesGrid.Columns[10].Visible = true;
            _displayingResult = false;
            _presenter = new Presenter(this, new SchemeModel(SchemeModel.SchemeType.Frame));
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            сохранитьToolStripMenuItem.Enabled = true;
            сохранитьКакToolStripMenuItem.Enabled = true;
            tabControl1.Visible = true;
            Truss = false;
            _unsavedChanges = false;
            if (_addNodeForm != null)
                _addNodeForm.Close();
            if (_addCoreForm != null)
                _addCoreForm.Close();
            _addNodeForm = new AddNodeForm(false);
            _addCoreForm = new AddCoreForm(false);
            resetScalingButton.Enabled = true;
            ResetScaling();
        }

        private void CreateFrame_Click(object sender, EventArgs e)
        {
            if (!CheckUnsavedChanges()) return;
            Text = @"Расчет плоских стержневых систем - Новая рама";
            FileName = null;
            toolStripButton5.Enabled = true;
            InitializeFrame();
        }

        /// <summary>
        /// Открывает схему из файла
        /// </summary>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (!CheckUnsavedChanges()) return;
            _displayingResult = false;
            _readFromFile = true;
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = @"Файл схемы|*.sch";
            openFileDialog1.Title = @"Выберите файл схемы";
            bool isTruss = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                _readFromFile = false;
                return;
            }

            _unsavedChanges = false;
            Text = @"Расчет плоских стержневых систем - " + openFileDialog1.FileName;
            toolStripButton5.Enabled = true;
            coresGrid.Rows.Clear();
            nodesGrid.Rows.Clear();
            _displayingResult = false;
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            сохранитьToolStripMenuItem.Enabled = false;
            сохранитьКакToolStripMenuItem.Enabled = false;
            tabControl1.Visible = false;
            if (_addNodeForm != null)
                _addNodeForm.Close();
            if (_addCoreForm != null)
                _addCoreForm.Close();
            _unsavedChanges = false;
            resetScalingButton.Enabled = false;
            List<string> nodesStrings = new List<string>();
            List<string> coresStrings = new List<string>();
            List<string> rredStrings = new List<string>();
            List<string> pStrings = new List<string>();
            List<string> eStrings = new List<string>();
            List<string> fStrings = new List<string>();
            List<string> iStrings = new List<string>();
            double e1 = 0;
            double f1 = 0;
            double i1 = 0;
            List<Node> nodes = new List<Node>();
            FileName = openFileDialog1.FileName;
            var fileLines = File.ReadAllLines(openFileDialog1.FileName).ToList();
            if (fileLines.Count < 7)
            {
                ErrorMsg("Входной файл не соответствует формату", "Ошибка при чтении файла");
                return;
            }

            foreach (var str in fileLines)
            {
                if (fileLines[0].IndexOf("Truss") == 0)
                    isTruss = true;
                else if (fileLines[0].IndexOf("Frame") == 0)
                {
                    isTruss = false;
                    if (fileLines.Count < 8)
                    {
                        ErrorMsg("Входной файл рамы не соответствует формату", "Ошибка при чтении файла");
                        return;
                    }
                }
                else
                {
                    ErrorMsg("В строке 1 должен быть указан корректный тип конструкции", "Ошибка при чтении файла");
                    return;
                }

                if (fileLines[1].IndexOf("Cores=") == 0 && fileLines[1].Length > 8)
                    coresStrings = new List<string>(fileLines[1].Substring(7, fileLines[1].Length - 8).Split(';'));
                else
                {
                    ErrorMsg("В строке 2 должны быть указаны стержни", "Ошибка при чтении файла");
                    return;
                }

                if (fileLines[2].IndexOf("Nodes=") == 0)
                    nodesStrings = new List<string>(fileLines[2].Substring(7, fileLines[2].Length - 8).Split(';'));
                else
                {
                    ErrorMsg("В строке 3 должны быть указаны узлы", "Ошибка при чтении файла");
                    return;
                }

                if (fileLines[3].IndexOf("E1=") == 0 && fileLines[3].Length > 3)
                {
                    if (!double.TryParse(fileLines[3].Substring(3, fileLines[3].Length - 3).Replace(".", ","),
                        out e1))
                    {
                        ErrorMsg("В строке 4 должны быть указаны модули упругости стержней",
                            "Ошибка при чтении файла");
                        return;
                    }
                }
                else if (fileLines[3].IndexOf("E=") == 0 && fileLines[3].Length > 4)
                    eStrings = new List<string>(fileLines[3].Substring(3, fileLines[3].Length - 4).Split(';'));
                else
                {
                    ErrorMsg("В строке 4 должны быть указаны модули упругости стержней",
                        "Ошибка при чтении файла");
                    return;
                }

                if (fileLines[4].IndexOf("F1=") == 0 && fileLines[4].Length > 3)
                {
                    if (!double.TryParse(fileLines[4].Substring(3, fileLines[4].Length - 3).Replace(".", ","),
                        out f1))
                    {
                        ErrorMsg("В строке 5 должны быть указаны площади стержней", "Ошибка при чтении файла");
                        return;
                    }
                }
                else if (fileLines[4].IndexOf("F=") == 0 && fileLines[4].Length > 4)
                    fStrings = new List<string>(fileLines[4].Substring(3, fileLines[4].Length - 4).Split(';'));
                else
                {
                    ErrorMsg("В строке 5 должны быть указаны площади стержней", "Ошибка при чтении файла");
                    return;
                }

                if (fileLines[5].IndexOf("Fix=") == 0 && fileLines[5].Length > 5)
                    rredStrings = new List<string>(fileLines[5].Substring(5, fileLines[5].Length - 6).Split(';'));
                else
                {
                    ErrorMsg("В строке 6 должны быть указаны фиксации узлов", "Ошибка при чтении файла");
                    return;
                }

                if (fileLines[6].IndexOf("P=") == 0 && fileLines[6].Length > 3)
                    pStrings = new List<string>(fileLines[6].Substring(3, fileLines[6].Length - 4).Split(';'));
                else
                {
                    ErrorMsg("В строке 7 должны быть указаны силы, действующие на узлы", "Ошибка при чтении файла");
                    return;
                }

                if (isTruss == false)
                {
                    if (fileLines[7].IndexOf("I1=") == 0 && fileLines[7].Length > 3)
                    {
                        if (!double.TryParse(fileLines[7].Substring(3, fileLines[7].Length - 3).Replace(".", ","),
                            out i1))
                        {
                            ErrorMsg("В строке 8 должны быть указаны моменты инерции стержней",
                                "Ошибка при чтении файла");
                            return;
                        }
                    }
                    else if (fileLines[7].IndexOf("I=") == 0 && fileLines[7].Length > 4)
                        iStrings = new List<string>(fileLines[7].Substring(3, fileLines[7].Length - 4).Split(';'));
                    else
                    {
                        ErrorMsg("В строке 8 должны быть указаны моменты инерции стержней", "Ошибка при чтении файла");
                        return;
                    }
                }
            }

            string typeText;
            if (isTruss)
            {
                InitializeTruss();
                typeText = @"[Ферма]";
            }
            else
            {
                InitializeFrame();
                typeText = @"[Рама]";
            }

            Text = typeText + @" Расчет плоских стержневых систем - " + openFileDialog1.FileName;
            try
            {
                foreach (var str in nodesStrings)
                {
                    NodeType nType;
                    if (isTruss)
                        nType = NodeType.Hinge;
                    else
                        nType = str.Split(' ')[2] == "h" ? NodeType.Hinge : NodeType.Rigid;
                    double.TryParse(str.Split(' ')[0].Replace(".", ","), out var x);
                    double.TryParse(str.Split(' ')[1].Replace(".", ","), out var y);
                    nodes.Add(new Node(0, x, y, nType, false, false, false, 0, 0, 0,""));
                }
            }
            catch (Exception exception)
            {
                ErrorMsg("Строка с узлами задана некорректно", "Ошибка при чтении файла");
                return;
            }

            try
            {
                if (rredStrings[0] != "")
                {
                    foreach (var str in rredStrings)
                    {
                        int.TryParse(str.Split(' ')[0], out var n);
                        if (str.Split(' ')[1] == "x")
                            nodes[n - 1].FixX = true;
                        else if (str.Split(' ')[1] == "y")
                            nodes[n - 1].FixY = true;
                        else if (str.Split(' ')[1] == "a")
                            nodes[n - 1].FixA = true;
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorMsg("Строка с фиксациями задана некорректно", "Ошибка при чтении файла");
                return;
            }

            try
            {
                if (pStrings[0] != "")
                {
                    foreach (var str in pStrings)
                    {
                        int.TryParse(str.Split(' ')[0], out var n);
                        double.TryParse(str.Split(' ')[2].Replace(".", ","), out var value);
                        if (str.Split(' ')[1] == "x")
                            nodes[n - 1].Px = value;
                        else if (str.Split(' ')[1] == "y")
                            nodes[n - 1].Py = value;
                        else if (str.Split(' ')[1] == "a")
                            nodes[n - 1].Pa = value;
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorMsg("Строка с силами задана некорректно", "Ошибка при чтении файла");
                return;
            }

            foreach (var n in nodes)
                _presenter.AddNodeOnLoad(n);
            for (int i = 0; i < coresStrings.Count; i++)
            {
                double ee = 0;
                double ff = 0;
                double ii = 0;
                int start = 0;
                int end = 0;
                try
                {
                    if (!(int.TryParse(coresStrings[i].Split(' ')[0], out start) &&
                          int.TryParse(coresStrings[i].Split(' ')[1], out end)))
                    {
                        ErrorMsg("Строка со стержнями задана некорректно", "Ошибка при чтении файла");
                        return;
                    }
                }
                catch (Exception exception)
                {
                    ErrorMsg("Строка со стержнями задана некорректно", "Ошибка при чтении файла");
                    return;
                }

                try
                {
                    if (eStrings.Count > 0)
                        double.TryParse(eStrings[i], out ee);
                    else
                        ee = e1;
                }
                catch (Exception exception)
                {
                    ErrorMsg("Строка с модулями упругости задана некорректно", "Ошибка при чтении файла");
                    return;
                }

                try
                {
                    if (fStrings.Count > 0)
                        double.TryParse(fStrings[i], out ff);
                    else
                        ff = f1;
                }
                catch (Exception exception)
                {
                    ErrorMsg("Строка с площадями задана некорректно", "Ошибка при чтении файла");
                    return;
                }

                if (isTruss == false)
                {
                    try
                    {
                        if (fStrings.Count > 0)
                            double.TryParse(iStrings[i], out ii);
                        else
                            ii = i1;
                    }
                    catch (Exception exception)
                    {
                        ErrorMsg("Строка с моментами инерции задана некорректно", "Ошибка при чтении файла");
                        return;
                    }
                }

                _presenter.AddCore(start, end, ee, ff, ii);
            }

            _readFromFile = false;
            _firstErrorShown = false;
            _schemeGraphic.Invalidate();
        }

        /// <summary>
        /// Визуализирует схему
        /// </summary>
        private void SchemeGraphic_Paint(object sender, PaintEventArgs e)
        {
            if (_readFromFile || !tabControl1.Visible) return;
            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(e.Graphics, _schemeGraphic.DisplayRectangle);
            const float baseWidth = 4;
            var pen = new Pen(Color.Black, baseWidth / 4);
            var resultPen = new Pen(Color.Red, baseWidth / 4);
            myBuffer.Graphics.Clear(Color.White);
            Brush hingeBrush = Brushes.Green;
            Brush rigidBrush = Brushes.Blue;
            Brush nodeBrush = Brushes.Blue;
            Brush coreBrush = Brushes.Black;
            Brush resultNodeBrush = Brushes.Red;
            Brush resultCoreBrush = Brushes.Orange;
            if (!_hideScheme)
            {
                DrawNodes(myBuffer, hingeBrush, baseWidth, rigidBrush, ref pen, nodeBrush);
                DrawCores(pen, myBuffer, coreBrush);
            }

            if (_displayingResult && !_hideResult)
            {
                DrawResult(myBuffer, hingeBrush, baseWidth, rigidBrush, resultNodeBrush, resultPen, resultCoreBrush);
            }

            myBuffer.Render(e.Graphics);
        }

        /// <summary>
        /// Визуализирует результат расчета
        /// </summary>
        private void DrawResult(BufferedGraphics myBuffer, Brush hingeBrush, float baseWidth, Brush rigidBrush,
            Brush resultNodeBrush, Pen resultPen, Brush resultCoreBrush)
        {
            try
            {
                foreach (var n in _presenter.OffsetNodes)
                {
                    switch (n.Type)
                    {
                        case NodeType.Hinge:
                            myBuffer.Graphics.FillEllipse(hingeBrush, _scaling * (float) n.X + _cx - baseWidth,
                                _schemeGraphic.Height - _scaling * (float) n.Y + _cy - baseWidth, baseWidth, baseWidth);
                            break;
                        case NodeType.Rigid:
                            myBuffer.Graphics.FillRectangle(rigidBrush, _scaling * (float) n.X + _cx - baseWidth / 2,
                                _schemeGraphic.Height - _scaling * (float) n.Y + _cy - baseWidth / 2, baseWidth,
                                baseWidth);
                            break;
                    }

                    myBuffer.Graphics.DrawString(n.Id + "'", new Font("Arial", 10), resultNodeBrush,
                        new Point((int) (_scaling * (float) n.X + _cx - baseWidth + 5),
                            (int) (_schemeGraphic.Height - _scaling * (float) n.Y + _cy - baseWidth)));
                }

                foreach (var c in _presenter.OffsetCores)
                {
                    myBuffer.Graphics.DrawLine(resultPen,
                        new Point((int) (c.Start.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * c.Start.Y + _cy)),
                        new Point((int) (c.End.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * c.End.Y + _cy)));
                    myBuffer.Graphics.DrawString(c.Id + "'", new Font("Arial", 10), resultCoreBrush,
                        new Point((int) (c.Start.X * _scaling + _cx + c.End.X * _scaling + _cx) / 2,
                            (int) (_schemeGraphic.Height - _scaling * c.Start.Y + _cy + _schemeGraphic.Height -
                                   _scaling * c.End.Y + _cy) / 2));
                }
            }
            catch (OverflowException ex)
            {
                ErrorMsg("Координаты элементов результирующей схемы находятся вне допустимых границ",
                    "Ошибка при визуализации результата");
            }
        }

        private void DrawNodes(BufferedGraphics myBuffer, Brush hingeBrush, float baseWidth, Brush rigidBrush,
            ref Pen pen,
            Brush nodeBrush)
        {
            foreach (var n in _presenter.Nodes)
            {
                switch (n.Type)
                {
                    case NodeType.Hinge:
                        myBuffer.Graphics.FillEllipse(hingeBrush, _scaling * (float) n.X + _cx - baseWidth,
                            _schemeGraphic.Height - _scaling * (float) n.Y + _cy - baseWidth, baseWidth, baseWidth);
                        break;
                    case NodeType.Rigid:
                        myBuffer.Graphics.FillRectangle(rigidBrush, _scaling * (float) n.X + _cx - baseWidth / 2,
                            _schemeGraphic.Height - _scaling * (float) n.Y + _cy - baseWidth / 2, baseWidth,
                            baseWidth);
                        break;
                }

                //Отрисовка действующих сил
                //Для сил карандаш шире и со стрелкой на конце
                pen = new Pen(Color.Blue, baseWidth * 1.2F);
                pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                if (n.Px > 0)
                    myBuffer.Graphics.DrawLine(pen,
                        new Point((int) ((n.X - 0.15) * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * n.Y + _cy)),
                        new Point((int) (n.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * n.Y + _cy)));
                if (n.Px < 0)
                    myBuffer.Graphics.DrawLine(pen,
                        new Point((int) ((n.X + 0.15) * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * n.Y + _cy)),
                        new Point((int) (n.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * n.Y + _cy)));
                if (n.Py > 0)
                    myBuffer.Graphics.DrawLine(pen,
                        new Point((int) (n.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * (n.Y - 0.15) + _cy)),
                        new Point((int) (n.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * n.Y + _cy)));
                if (n.Py < 0)
                    myBuffer.Graphics.DrawLine(pen,
                        new Point((int) (n.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * (n.Y + 0.15) + _cy)),
                        new Point((int) (n.X * _scaling) + _cx,
                            (int) (_schemeGraphic.Height - _scaling * n.Y + _cy)));
                if (n.Pa > 0)
                    myBuffer.Graphics.DrawArc(pen,
                        (int) ((n.X - 0.1) * _scaling) + _cx,
                        (int) (_schemeGraphic.Height - (_scaling * (n.Y + 0.1)) + _cy), 0.2F * _scaling,
                        0.2F * _scaling, -90,
                        270);
                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                if (n.Pa < 0)
                    myBuffer.Graphics.DrawArc(pen,
                        (int) ((n.X - 0.1) * _scaling) + _cx,
                        (int) (_schemeGraphic.Height - (_scaling * (n.Y + 0.1)) + _cy), 0.2F * _scaling,
                        0.2F * _scaling, 0,
                        270);
                //Возвращаем изначальный вид карандаша
                pen = new Pen(Color.Black, baseWidth / 4);
                //Конец отрисовки сил
                //Отрисовка номера узла
                myBuffer.Graphics.DrawString(n.Id.ToString(), new Font("Arial", 10), nodeBrush,
                    new Point((int) (_scaling * (float) n.X + _cx - baseWidth + 5),
                        (int) (_schemeGraphic.Height - _scaling * (float) n.Y + _cy - baseWidth - 15)));
            }
        }

        private void DrawCores(Pen pen, BufferedGraphics myBuffer, Brush coreBrush)
        {
            foreach (var c in _presenter.Cores)
            {
                if (coresGrid.CurrentCell != null && c.Id == coresGrid.CurrentCell.RowIndex + 1)
                    pen.Color = Color.Aqua;
                myBuffer.Graphics.DrawLine(pen,
                    new Point((int) (c.Start.X * _scaling) + _cx,
                        (int) (_schemeGraphic.Height - _scaling * c.Start.Y + _cy)),
                    new Point((int) (c.End.X * _scaling) + _cx,
                        (int) (_schemeGraphic.Height - _scaling * c.End.Y + _cy)));
                myBuffer.Graphics.DrawString(c.Id.ToString(), new Font("Arial", 10), coreBrush,
                    new Point((int) ((c.Start.X * _scaling + _cx + (c.End.X * _scaling + _cx) * 0.7) / 1.7),
                        (int) ((_schemeGraphic.Height - _scaling * c.Start.Y + _cy + (_schemeGraphic.Height -
                                                                                      _scaling * c.End.Y + _cy) *
                                0.7) / 1.7)));
                if (coresGrid.CurrentCell != null && c.Id == coresGrid.CurrentCell.RowIndex + 1)
                    pen.Color = Color.Black;
            }
        }

        private void hideResultCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            toolStripButton3.Checked = !toolStripButton3.Checked;
            _hideResult = toolStripButton3.Checked;
            _schemeGraphic.Invalidate();
        }

        private void hideSchemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            toolStripButton2.Checked = !toolStripButton2.Checked;
            _hideScheme = toolStripButton2.Checked;
            _schemeGraphic.Invalidate();
        }

        private void coresGrid_Click(object sender, EventArgs e)
        {
            _schemeGraphic.Invalidate();
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            if (FileName == null)
                SaveSchemeAs();
            else
            {
                File.WriteAllText(FileName, String.Join(String.Empty, CreateFileContent()));
                string typeText;
                if (Truss)
                {
                    typeText = @"[Ферма]";
                }
                else
                {
                    typeText = @"[Рама]";
                }

                Text = typeText + @" Расчет плоских стержневых систем - " + FileName;
                _unsavedChanges = false;
            }
        }

        public void SaveSchemeAs()
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = @"Файл схемы|*.sch";
            saveFileDialog1.Title = @"Сохранить схему как";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            FileName = saveFileDialog1.FileName;
            string typeText;
            if (Truss)
            {
                typeText = @"[Ферма]";
            }
            else
            {
                typeText = @"[Рама]";
            }

            Text = typeText + @" Расчет плоских стержневых систем - " + FileName;
            // сохраняем текст в файл
            File.WriteAllText(FileName, String.Join(String.Empty, CreateFileContent()));
            _unsavedChanges = false;
        }

        private List<string> CreateFileContent()
        {
            var content = new List<string>();
            content.Add(Truss ? "Truss\n" : "Frame\n");

            var sb = new StringBuilder();
            sb.Append("Cores=[");
            foreach (var c in _presenter.Cores)
            {
                sb.Append(c.Start.Id + " ");
                sb.Append(c.End.Id + ";");
            }

            if (sb.Length > 7)
                sb.Length--;
            sb.Append("]\n");
            content.Add(sb.ToString());
            sb.Clear();

            sb.Append("Nodes=[");
            foreach (var n in _presenter.Nodes)
            {
                sb.Append(n.X + " ");
                sb.Append(n.Y + " ");
                sb.Append(n.Type == NodeType.Hinge ? "h;" : "r;");
            }

            if (sb.Length > 7)
                sb.Length--;
            sb.Append("]\n");
            content.Add(sb.ToString());

            sb.Clear();

            sb.Append("E=[");
            foreach (var c in _presenter.Cores)
                sb.Append(c.E + ";");
            if (sb.Length > 3)
                sb.Length--;
            sb.Append("]\n");
            content.Add(sb.ToString());

            sb.Clear();

            sb.Append("F=[");
            foreach (var c in _presenter.Cores)
                sb.Append(c.F + ";");
            if (sb.Length > 3)
                sb.Length--;
            sb.Append("]\n");
            content.Add(sb.ToString());

            sb.Clear();

            sb.Append("Fix=[");
            foreach (var n in _presenter.Nodes)
            {
                if (n.FixX)
                    sb.Append(n.Id + " x;");

                if (n.FixY)
                    sb.Append(n.Id + " y;");

                if (n.FixA)
                    sb.Append(n.Id + " a;");
            }

            if (sb.Length > 5)
                sb.Length--;
            sb.Append("]\n");
            content.Add(sb.ToString());

            sb.Clear();

            sb.Append("P=[");
            foreach (var n in _presenter.Nodes)
            {
                if (Math.Abs(n.Px) > 0.00000001)
                    sb.Append(n.Id + " x " + n.Px + ";");

                if (Math.Abs(n.Py) > 0.00000001)
                    sb.Append(n.Id + " y " + n.Py + ";");

                if (Math.Abs(n.Pa) > 0.00000001)
                    sb.Append(n.Id + " a " + n.Pa + ";");
            }

            if (sb.Length > 3)
                sb.Length--;
            sb.Append("]\n");
            content.Add(sb.ToString());

            sb.Clear();

            if (!Truss)
            {
                sb.Append("I=[");
                foreach (var c in _presenter.Cores)
                    sb.Append(c.J + ";");
                if (sb.Length > 3)
                    sb.Length--;
                sb.Append("]\n");
                content.Add(sb.ToString());

                sb.Clear();
            }

            return content;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile_Click(sender, e);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSchemeAs();
        }

        private void ResetScalingButton_Click(object sender, EventArgs e)
        {
            ResetScaling();
        }

        private void ResetScaling()
        {
            _scaling = 100;
            _cx = 0;
            _cy = 0;
            _shiftX = 0;
            _shiftY = 0;
            _schemeGraphic.Invalidate();
        }

        public void DisplayResult(Matrix<double> classic, Matrix<double> lu, double condNum, int[] fixedNodes)
        {
            _displayingResult = true;
            var result = new ResultForm(classic, lu, condNum, _presenter.Nodes.Count, fixedNodes, Truss) {Owner = this};
            result.Show();
            _schemeGraphic.Invalidate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckUnsavedChanges())
                e.Cancel = true;
        }

        private void двухсторойСвязьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void двухсторойСвязьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _twowayConnect = true;
            _onewayConnect = false;
        }

        private void однойсторойСвязьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _onewayConnect = true;
            _twowayConnect = false;
        }

        private void _schemeGraphic_Click(object sender, EventArgs e)
        {

        }

        private bool CheckUnsavedChanges()
        {
            if (_unsavedChanges)
            {
                var result = MessageBox.Show(@"Сохранить изменения?", @"Имеются несохраненные изменения",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (FileName == null)
                        SaveSchemeAs();
                    else
                    {
                        File.WriteAllText(FileName, String.Join(String.Empty, CreateFileContent()));
                    }

                    return true;
                }

                if (result == DialogResult.No)
                {
                    return true;
                }

                if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }
    }
}