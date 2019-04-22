namespace Diplom
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьСхемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фермыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рамыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.coresGrid = new System.Windows.Forms.DataGridView();
            this.deleteCore = new System.Windows.Forms.DataGridViewButtonColumn();
            this.coreId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coreStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coreEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coreE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coreF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coreI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCoreBtn = new System.Windows.Forms.Button();
            this.editCoresBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nodesGrid = new System.Windows.Forms.DataGridView();
            this.deleteNode = new System.Windows.Forms.DataGridViewButtonColumn();
            this.nodeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nodeFixX = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nodeFixY = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nodeFixA = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nodePx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodePy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodePa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reactionY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addNodeBtn = new System.Windows.Forms.Button();
            this.editNodesBtn = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.resetScalingButton = new System.Windows.Forms.Button();
            this._schemeGraphic = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рамаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.двухсторойСвязьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.однойсторойСвязьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coresGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._schemeGraphic)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьСхемуToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьСхемуToolStripMenuItem
            // 
            this.создатьСхемуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фермыToolStripMenuItem,
            this.рамыToolStripMenuItem});
            this.создатьСхемуToolStripMenuItem.Name = "создатьСхемуToolStripMenuItem";
            this.создатьСхемуToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.создатьСхемуToolStripMenuItem.Text = "Создать схему";
            // 
            // фермыToolStripMenuItem
            // 
            this.фермыToolStripMenuItem.Name = "фермыToolStripMenuItem";
            this.фермыToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.фермыToolStripMenuItem.Text = "Фермы";
            this.фермыToolStripMenuItem.Click += new System.EventHandler(this.CreateTruss_Click);
            // 
            // рамыToolStripMenuItem
            // 
            this.рамыToolStripMenuItem.Name = "рамыToolStripMenuItem";
            this.рамыToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.рамыToolStripMenuItem.Text = "Рамы";
            this.рамыToolStripMenuItem.Click += new System.EventHandler(this.CreateFrame_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.открытьToolStripMenuItem.Text = "Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Enabled = false;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Enabled = false;
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(362, 487);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(354, 461);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Стержни";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.coresGrid, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.addCoreBtn, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.editCoresBtn, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(348, 455);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // coresGrid
            // 
            this.coresGrid.AllowUserToAddRows = false;
            this.coresGrid.AllowUserToDeleteRows = false;
            this.coresGrid.AllowUserToResizeRows = false;
            this.coresGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.coresGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.coresGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteCore,
            this.coreId,
            this.coreStart,
            this.coreEnd,
            this.coreE,
            this.coreF,
            this.coreI});
            this.coresGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coresGrid.Location = new System.Drawing.Point(3, 3);
            this.coresGrid.Name = "coresGrid";
            this.coresGrid.ReadOnly = true;
            this.coresGrid.RowHeadersVisible = false;
            this.coresGrid.Size = new System.Drawing.Size(342, 395);
            this.coresGrid.TabIndex = 1;
            this.coresGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CoresGrid_CellContentClick);
            this.coresGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CoresGrid_CellValueChanged);
            this.coresGrid.CurrentCellChanged += new System.EventHandler(this.coresGrid_Click);
            this.coresGrid.Click += new System.EventHandler(this.coresGrid_Click);
            // 
            // deleteCore
            // 
            this.deleteCore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.deleteCore.HeaderText = "";
            this.deleteCore.MinimumWidth = 10;
            this.deleteCore.Name = "deleteCore";
            this.deleteCore.ReadOnly = true;
            this.deleteCore.Text = "✖";
            this.deleteCore.UseColumnTextForButtonValue = true;
            this.deleteCore.Visible = false;
            // 
            // coreId
            // 
            this.coreId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coreId.HeaderText = "Номер";
            this.coreId.Name = "coreId";
            this.coreId.ReadOnly = true;
            this.coreId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // coreStart
            // 
            this.coreStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coreStart.HeaderText = "Начало";
            this.coreStart.Name = "coreStart";
            this.coreStart.ReadOnly = true;
            this.coreStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // coreEnd
            // 
            this.coreEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coreEnd.HeaderText = "Конец";
            this.coreEnd.Name = "coreEnd";
            this.coreEnd.ReadOnly = true;
            this.coreEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // coreE
            // 
            this.coreE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coreE.HeaderText = "E";
            this.coreE.Name = "coreE";
            this.coreE.ReadOnly = true;
            this.coreE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // coreF
            // 
            this.coreF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coreF.HeaderText = "F";
            this.coreF.Name = "coreF";
            this.coreF.ReadOnly = true;
            this.coreF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // coreI
            // 
            this.coreI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coreI.HeaderText = "J";
            this.coreI.Name = "coreI";
            this.coreI.ReadOnly = true;
            this.coreI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // addCoreBtn
            // 
            this.addCoreBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCoreBtn.Location = new System.Drawing.Point(3, 404);
            this.addCoreBtn.Name = "addCoreBtn";
            this.addCoreBtn.Size = new System.Drawing.Size(342, 21);
            this.addCoreBtn.TabIndex = 2;
            this.addCoreBtn.Text = "Добавить...";
            this.addCoreBtn.UseVisualStyleBackColor = true;
            this.addCoreBtn.Click += new System.EventHandler(this.AddCoreBtn_Click);
            // 
            // editCoresBtn
            // 
            this.editCoresBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editCoresBtn.Location = new System.Drawing.Point(3, 431);
            this.editCoresBtn.Name = "editCoresBtn";
            this.editCoresBtn.Size = new System.Drawing.Size(342, 21);
            this.editCoresBtn.TabIndex = 4;
            this.editCoresBtn.Text = "Редактировать";
            this.editCoresBtn.UseVisualStyleBackColor = true;
            this.editCoresBtn.Click += new System.EventHandler(this.EditCoresBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(354, 461);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Узлы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.nodesGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.addNodeBtn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.editNodesBtn, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(348, 455);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // nodesGrid
            // 
            this.nodesGrid.AllowUserToAddRows = false;
            this.nodesGrid.AllowUserToDeleteRows = false;
            this.nodesGrid.AllowUserToResizeRows = false;
            this.nodesGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.nodesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nodesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deleteNode,
            this.nodeId,
            this.nodeX,
            this.nodeY,
            this.nodeType,
            this.nodeFixX,
            this.nodeFixY,
            this.nodeFixA,
            this.nodePx,
            this.nodePy,
            this.nodePa,
            this.reactionY});
            this.nodesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesGrid.Location = new System.Drawing.Point(3, 3);
            this.nodesGrid.Name = "nodesGrid";
            this.nodesGrid.ReadOnly = true;
            this.nodesGrid.RowHeadersVisible = false;
            this.nodesGrid.Size = new System.Drawing.Size(342, 395);
            this.nodesGrid.TabIndex = 0;
            this.nodesGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.NodesGrid_CellContentClick);
            this.nodesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.NodesGrid_CellValueChanged);
            // 
            // deleteNode
            // 
            this.deleteNode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.deleteNode.HeaderText = "";
            this.deleteNode.MinimumWidth = 10;
            this.deleteNode.Name = "deleteNode";
            this.deleteNode.ReadOnly = true;
            this.deleteNode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteNode.Text = "✖";
            this.deleteNode.UseColumnTextForButtonValue = true;
            this.deleteNode.Visible = false;
            // 
            // nodeId
            // 
            this.nodeId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeId.FillWeight = 95.64265F;
            this.nodeId.HeaderText = "Номер";
            this.nodeId.MinimumWidth = 20;
            this.nodeId.Name = "nodeId";
            this.nodeId.ReadOnly = true;
            this.nodeId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nodeX
            // 
            this.nodeX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeX.FillWeight = 59.97492F;
            this.nodeX.HeaderText = "X";
            this.nodeX.MaxInputLength = 15;
            this.nodeX.Name = "nodeX";
            this.nodeX.ReadOnly = true;
            this.nodeX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nodeY
            // 
            this.nodeY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeY.FillWeight = 59.97492F;
            this.nodeY.HeaderText = "Y";
            this.nodeY.MaxInputLength = 15;
            this.nodeY.Name = "nodeY";
            this.nodeY.ReadOnly = true;
            this.nodeY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nodeType
            // 
            this.nodeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeType.FillWeight = 81.29626F;
            this.nodeType.HeaderText = "Тип";
            this.nodeType.Items.AddRange(new object[] {
            "Шарнир",
            "Жесткий"});
            this.nodeType.MinimumWidth = 75;
            this.nodeType.Name = "nodeType";
            this.nodeType.ReadOnly = true;
            this.nodeType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // nodeFixX
            // 
            this.nodeFixX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeFixX.FillWeight = 114.7712F;
            this.nodeFixX.HeaderText = "Закр. X";
            this.nodeFixX.MinimumWidth = 2;
            this.nodeFixX.Name = "nodeFixX";
            this.nodeFixX.ReadOnly = true;
            // 
            // nodeFixY
            // 
            this.nodeFixY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeFixY.FillWeight = 114.7712F;
            this.nodeFixY.HeaderText = "Закр. Y";
            this.nodeFixY.MinimumWidth = 2;
            this.nodeFixY.Name = "nodeFixY";
            this.nodeFixY.ReadOnly = true;
            // 
            // nodeFixA
            // 
            this.nodeFixA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeFixA.FillWeight = 114.7712F;
            this.nodeFixA.HeaderText = "Закр. угол";
            this.nodeFixA.Name = "nodeFixA";
            this.nodeFixA.ReadOnly = true;
            this.nodeFixA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // nodePx
            // 
            this.nodePx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodePx.HeaderText = "Нагр. X";
            this.nodePx.MaxInputLength = 15;
            this.nodePx.Name = "nodePx";
            this.nodePx.ReadOnly = true;
            this.nodePx.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nodePy
            // 
            this.nodePy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodePy.HeaderText = "Нагр. Y";
            this.nodePy.MaxInputLength = 15;
            this.nodePy.Name = "nodePy";
            this.nodePy.ReadOnly = true;
            this.nodePy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nodePa
            // 
            this.nodePa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodePa.HeaderText = "Сосред. момент";
            this.nodePa.MaxInputLength = 15;
            this.nodePa.Name = "nodePa";
            this.nodePa.ReadOnly = true;
            this.nodePa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // reactionY
            // 
            this.reactionY.HeaderText = "Рабочее направление одно связи";
            this.reactionY.Name = "reactionY";
            this.reactionY.ReadOnly = true;
            this.reactionY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // addNodeBtn
            // 
            this.addNodeBtn.BackColor = System.Drawing.Color.Transparent;
            this.addNodeBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addNodeBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addNodeBtn.Location = new System.Drawing.Point(3, 404);
            this.addNodeBtn.Name = "addNodeBtn";
            this.addNodeBtn.Size = new System.Drawing.Size(342, 21);
            this.addNodeBtn.TabIndex = 3;
            this.addNodeBtn.Text = "Добавить...";
            this.addNodeBtn.UseVisualStyleBackColor = false;
            this.addNodeBtn.Click += new System.EventHandler(this.addNodeBtn_Click);
            // 
            // editNodesBtn
            // 
            this.editNodesBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editNodesBtn.Location = new System.Drawing.Point(3, 431);
            this.editNodesBtn.Name = "editNodesBtn";
            this.editNodesBtn.Size = new System.Drawing.Size(342, 21);
            this.editNodesBtn.TabIndex = 5;
            this.editNodesBtn.Text = "Редактировать";
            this.editNodesBtn.UseVisualStyleBackColor = true;
            this.editNodesBtn.Click += new System.EventHandler(this.EditNodesBtn_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.resetScalingButton);
            this.splitContainer1.Panel1.Controls.Add(this._schemeGraphic);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 487);
            this.splitContainer1.SplitterDistance = 662;
            this.splitContainer1.TabIndex = 4;
            // 
            // resetScalingButton
            // 
            this.resetScalingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetScalingButton.Enabled = false;
            this.resetScalingButton.Location = new System.Drawing.Point(584, 464);
            this.resetScalingButton.Name = "resetScalingButton";
            this.resetScalingButton.Size = new System.Drawing.Size(75, 23);
            this.resetScalingButton.TabIndex = 4;
            this.resetScalingButton.Text = "Сброс";
            this.resetScalingButton.UseVisualStyleBackColor = true;
            this.resetScalingButton.Click += new System.EventHandler(this.ResetScalingButton_Click);
            // 
            // _schemeGraphic
            // 
            this._schemeGraphic.BackColor = System.Drawing.SystemColors.HighlightText;
            this._schemeGraphic.Dock = System.Windows.Forms.DockStyle.Fill;
            this._schemeGraphic.Location = new System.Drawing.Point(0, 0);
            this._schemeGraphic.Name = "_schemeGraphic";
            this._schemeGraphic.Size = new System.Drawing.Size(662, 487);
            this._schemeGraphic.TabIndex = 3;
            this._schemeGraphic.TabStop = false;
            this._schemeGraphic.Click += new System.EventHandler(this._schemeGraphic_Click);
            this._schemeGraphic.Paint += new System.Windows.Forms.PaintEventHandler(this.SchemeGraphic_Paint);
            this._schemeGraphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SchemeGraphic_MouseDown);
            this._schemeGraphic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SchemeGraphic_MouseMove);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(176, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cToolStripMenuItem,
            this.рамаToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::ConstructionCalculator.Properties.Resources.icons8_создать_новый_26;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "Создать схему";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.cToolStripMenuItem.Text = "Ферма";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.CreateTruss_Click);
            // 
            // рамаToolStripMenuItem
            // 
            this.рамаToolStripMenuItem.Name = "рамаToolStripMenuItem";
            this.рамаToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.рамаToolStripMenuItem.Text = "Рама";
            this.рамаToolStripMenuItem.Click += new System.EventHandler(this.CreateFrame_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::ConstructionCalculator.Properties.Resources.icons8_документ_50;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Открыть схему";
            this.toolStripButton4.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Enabled = false;
            this.toolStripButton5.Image = global::ConstructionCalculator.Properties.Resources.icons8_сохранить_50;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Сохранить";
            this.toolStripButton5.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = global::ConstructionCalculator.Properties.Resources.icons8_калькулятор_40;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Рассчитать";
            this.toolStripButton1.Click += new System.EventHandler(this.CalculateToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = global::ConstructionCalculator.Properties.Resources.icons8_спрятать_30;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Скрыть схему";
            this.toolStripButton2.Click += new System.EventHandler(this.hideSchemeCheckBox_CheckedChanged);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.Image = global::ConstructionCalculator.Properties.Resources.icons8_спрятать_32;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "Скрыть расчет";
            this.toolStripButton3.Click += new System.EventHandler(this.hideResultCheckBox_CheckedChanged);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.двухсторойСвязьToolStripMenuItem,
            this.однойсторойСвязьToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Click += new System.EventHandler(this.toolStripDropDownButton2_Click);
            // 
            // двухсторойСвязьToolStripMenuItem
            // 
            this.двухсторойСвязьToolStripMenuItem.Name = "двухсторойСвязьToolStripMenuItem";
            this.двухсторойСвязьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.двухсторойСвязьToolStripMenuItem.Text = "Двухсторой связь";
            this.двухсторойСвязьToolStripMenuItem.Click += new System.EventHandler(this.двухсторойСвязьToolStripMenuItem_Click);
            // 
            // однойсторойСвязьToolStripMenuItem
            // 
            this.однойсторойСвязьToolStripMenuItem.Name = "однойсторойСвязьToolStripMenuItem";
            this.однойсторойСвязьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.однойсторойСвязьToolStripMenuItem.Text = "Однойсторой связь";
            this.однойсторойСвязьToolStripMenuItem.Click += new System.EventHandler(this.однойсторойСвязьToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStripContainer2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1028, 512);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 24);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1028, 537);
            this.toolStripContainer1.TabIndex = 9;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(1028, 487);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(1028, 512);
            this.toolStripContainer2.TabIndex = 10;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 561);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(572, 430);
            this.Name = "MainForm";
            this.Text = "Расчет плоских стержневых систем";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.coresGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nodesGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._schemeGraphic)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьСхемуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фермыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рамыToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox _schemeGraphic;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView coresGrid;
        private System.Windows.Forms.Button addCoreBtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView nodesGrid;
        private System.Windows.Forms.Button addNodeBtn;
        private System.Windows.Forms.Button editCoresBtn;
        private System.Windows.Forms.Button editNodesBtn;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рамаToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.Button resetScalingButton;
        private System.Windows.Forms.DataGridViewButtonColumn deleteCore;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreId;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreE;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreF;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreI;
        private System.Windows.Forms.DataGridViewButtonColumn deleteNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeX;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeY;
        private System.Windows.Forms.DataGridViewComboBoxColumn nodeType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn nodeFixX;
        private System.Windows.Forms.DataGridViewCheckBoxColumn nodeFixY;
        private System.Windows.Forms.DataGridViewCheckBoxColumn nodeFixA;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodePx;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodePy;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodePa;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem двухсторойСвязьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem однойсторойСвязьToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn reactionY;
    }
}

