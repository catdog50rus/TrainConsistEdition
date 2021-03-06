﻿namespace TrainConsistEdition.UI.WF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel_Main = new System.Windows.Forms.Panel();
            this.button_Clear = new System.Windows.Forms.Button();
            this.panel_Consist = new System.Windows.Forms.Panel();
            this.groupBox_Consist = new System.Windows.Forms.GroupBox();
            this.button_Change = new System.Windows.Forms.Button();
            this.dataGridView_Consists = new System.Windows.Forms.DataGridView();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Coeff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_DeleteVehecle = new System.Windows.Forms.Button();
            this.groupBox_vehcle = new System.Windows.Forms.GroupBox();
            this.textBox_Coeff = new System.Windows.Forms.TextBox();
            this.button_AddVagon = new System.Windows.Forms.Button();
            this.label_LoaderCoeff = new System.Windows.Forms.Label();
            this.listBox_VagonName = new System.Windows.Forms.ListBox();
            this.textBox_VagonCount = new System.Windows.Forms.TextBox();
            this.label_VagonCount = new System.Windows.Forms.Label();
            this.label_VagonName = new System.Windows.Forms.Label();
            this.label_ConsistEdition = new System.Windows.Forms.Label();
            this.groupBox_Loco = new System.Windows.Forms.GroupBox();
            this.listBox_Loco = new System.Windows.Forms.ListBox();
            this.textBox_LocoCount = new System.Windows.Forms.TextBox();
            this.label_LocoCount = new System.Windows.Forms.Label();
            this.button_AddLoco = new System.Windows.Forms.Button();
            this.label_LocoName = new System.Windows.Forms.Label();
            this.panel_Options = new System.Windows.Forms.Panel();
            this.groupBox_Descr = new System.Windows.Forms.GroupBox();
            this.label_CabineInVehicle = new System.Windows.Forms.Label();
            this.label_Description = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.textBox_ConsistName = new System.Windows.Forms.TextBox();
            this.textBox_Description = new System.Windows.Forms.TextBox();
            this.textBox_CabineInVehicle = new System.Windows.Forms.TextBox();
            this.groupBox_TrainOptons = new System.Windows.Forms.GroupBox();
            this.label_ChargingPressure = new System.Windows.Forms.Label();
            this.label_InitMainResPressure = new System.Windows.Forms.Label();
            this.label_CouplingModule = new System.Windows.Forms.Label();
            this.label_NoAir = new System.Windows.Forms.Label();
            this.checkBox_NoAir = new System.Windows.Forms.CheckBox();
            this.listBox_CouplingType = new System.Windows.Forms.ListBox();
            this.textBox_ChargingPressure = new System.Windows.Forms.TextBox();
            this.textBox_InitMainResPressure = new System.Windows.Forms.TextBox();
            this.checkBox_TrainOptions = new System.Windows.Forms.CheckBox();
            this.label_TrainOptionsHide = new System.Windows.Forms.Label();
            this.label_ConsistOptions = new System.Windows.Forms.Label();
            this.button_Serialize = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_CreateNewConsist = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_OpenConsist = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_SetFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel_Main.SuspendLayout();
            this.panel_Consist.SuspendLayout();
            this.groupBox_Consist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Consists)).BeginInit();
            this.groupBox_vehcle.SuspendLayout();
            this.groupBox_Loco.SuspendLayout();
            this.panel_Options.SuspendLayout();
            this.groupBox_Descr.SuspendLayout();
            this.groupBox_TrainOptons.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Main
            // 
            this.panel_Main.AutoSize = true;
            this.panel_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_Main.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel_Main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Main.Controls.Add(this.button_Clear);
            this.panel_Main.Controls.Add(this.panel_Consist);
            this.panel_Main.Controls.Add(this.panel_Options);
            this.panel_Main.Controls.Add(this.button_Serialize);
            this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Main.Location = new System.Drawing.Point(0, 24);
            this.panel_Main.MinimumSize = new System.Drawing.Size(984, 637);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(1084, 637);
            this.panel_Main.TabIndex = 0;
            this.panel_Main.Visible = false;
            // 
            // button_Clear
            // 
            this.button_Clear.AutoSize = true;
            this.button_Clear.BackColor = System.Drawing.Color.IndianRed;
            this.button_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Clear.Enabled = false;
            this.button_Clear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Clear.Location = new System.Drawing.Point(591, 591);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(165, 39);
            this.button_Clear.TabIndex = 22;
            this.button_Clear.Text = "Очистить";
            this.button_Clear.UseVisualStyleBackColor = false;
            this.button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // panel_Consist
            // 
            this.panel_Consist.AutoSize = true;
            this.panel_Consist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Consist.Controls.Add(this.groupBox_Consist);
            this.panel_Consist.Controls.Add(this.groupBox_vehcle);
            this.panel_Consist.Controls.Add(this.label_ConsistEdition);
            this.panel_Consist.Controls.Add(this.groupBox_Loco);
            this.panel_Consist.Location = new System.Drawing.Point(7, 284);
            this.panel_Consist.MinimumSize = new System.Drawing.Size(1070, 275);
            this.panel_Consist.Name = "panel_Consist";
            this.panel_Consist.Size = new System.Drawing.Size(1070, 295);
            this.panel_Consist.TabIndex = 2;
            // 
            // groupBox_Consist
            // 
            this.groupBox_Consist.Controls.Add(this.button_Change);
            this.groupBox_Consist.Controls.Add(this.dataGridView_Consists);
            this.groupBox_Consist.Controls.Add(this.button_DeleteVehecle);
            this.groupBox_Consist.Enabled = false;
            this.groupBox_Consist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_Consist.Location = new System.Drawing.Point(717, 37);
            this.groupBox_Consist.Name = "groupBox_Consist";
            this.groupBox_Consist.Size = new System.Drawing.Size(346, 251);
            this.groupBox_Consist.TabIndex = 24;
            this.groupBox_Consist.TabStop = false;
            this.groupBox_Consist.Text = "Состав поезда";
            // 
            // button_Change
            // 
            this.button_Change.BackColor = System.Drawing.Color.Goldenrod;
            this.button_Change.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Change.Enabled = false;
            this.button_Change.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Change.Location = new System.Drawing.Point(20, 206);
            this.button_Change.Name = "button_Change";
            this.button_Change.Size = new System.Drawing.Size(137, 39);
            this.button_Change.TabIndex = 24;
            this.button_Change.Text = "Заменить";
            this.button_Change.UseVisualStyleBackColor = false;
            this.button_Change.Click += new System.EventHandler(this.Button_Change_Click);
            // 
            // dataGridView_Consists
            // 
            this.dataGridView_Consists.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView_Consists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Consists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Consists.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Name,
            this.Column_Count,
            this.Column_Coeff});
            this.dataGridView_Consists.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView_Consists.Location = new System.Drawing.Point(6, 19);
            this.dataGridView_Consists.Name = "dataGridView_Consists";
            this.dataGridView_Consists.RowHeadersVisible = false;
            this.dataGridView_Consists.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Consists.ShowEditingIcon = false;
            this.dataGridView_Consists.Size = new System.Drawing.Size(338, 138);
            this.dataGridView_Consists.TabIndex = 22;
            this.dataGridView_Consists.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_Consists_CellAndEdit);
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "Наименование";
            this.Column_Name.Name = "Column_Name";
            this.Column_Name.ReadOnly = true;
            this.Column_Name.Width = 175;
            // 
            // Column_Count
            // 
            this.Column_Count.HeaderText = "Количество";
            this.Column_Count.Name = "Column_Count";
            this.Column_Count.Width = 80;
            // 
            // Column_Coeff
            // 
            this.Column_Coeff.HeaderText = "Загрузка";
            this.Column_Coeff.Name = "Column_Coeff";
            this.Column_Coeff.Width = 80;
            // 
            // button_DeleteVehecle
            // 
            this.button_DeleteVehecle.BackColor = System.Drawing.Color.LightCoral;
            this.button_DeleteVehecle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_DeleteVehecle.Enabled = false;
            this.button_DeleteVehecle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_DeleteVehecle.Location = new System.Drawing.Point(188, 206);
            this.button_DeleteVehecle.Name = "button_DeleteVehecle";
            this.button_DeleteVehecle.Size = new System.Drawing.Size(137, 39);
            this.button_DeleteVehecle.TabIndex = 23;
            this.button_DeleteVehecle.Text = "Отцепить";
            this.button_DeleteVehecle.UseVisualStyleBackColor = false;
            this.button_DeleteVehecle.Click += new System.EventHandler(this.Button_DeleteVehecle_Click);
            // 
            // groupBox_vehcle
            // 
            this.groupBox_vehcle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox_vehcle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox_vehcle.Controls.Add(this.textBox_Coeff);
            this.groupBox_vehcle.Controls.Add(this.button_AddVagon);
            this.groupBox_vehcle.Controls.Add(this.label_LoaderCoeff);
            this.groupBox_vehcle.Controls.Add(this.listBox_VagonName);
            this.groupBox_vehcle.Controls.Add(this.textBox_VagonCount);
            this.groupBox_vehcle.Controls.Add(this.label_VagonCount);
            this.groupBox_vehcle.Controls.Add(this.label_VagonName);
            this.groupBox_vehcle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_vehcle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_vehcle.Location = new System.Drawing.Point(335, 37);
            this.groupBox_vehcle.Name = "groupBox_vehcle";
            this.groupBox_vehcle.Size = new System.Drawing.Size(376, 251);
            this.groupBox_vehcle.TabIndex = 19;
            this.groupBox_vehcle.TabStop = false;
            this.groupBox_vehcle.Text = "Вагоны";
            // 
            // textBox_Coeff
            // 
            this.textBox_Coeff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Coeff.Location = new System.Drawing.Point(304, 178);
            this.textBox_Coeff.Name = "textBox_Coeff";
            this.textBox_Coeff.Size = new System.Drawing.Size(49, 22);
            this.textBox_Coeff.TabIndex = 5;
            // 
            // button_AddVagon
            // 
            this.button_AddVagon.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button_AddVagon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_AddVagon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AddVagon.Location = new System.Drawing.Point(115, 206);
            this.button_AddVagon.Name = "button_AddVagon";
            this.button_AddVagon.Size = new System.Drawing.Size(160, 39);
            this.button_AddVagon.TabIndex = 22;
            this.button_AddVagon.Text = "Прицепить вагон";
            this.button_AddVagon.UseVisualStyleBackColor = false;
            this.button_AddVagon.Click += new System.EventHandler(this.Button_AddVagon_Click);
            // 
            // label_LoaderCoeff
            // 
            this.label_LoaderCoeff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_LoaderCoeff.Location = new System.Drawing.Point(6, 181);
            this.label_LoaderCoeff.Name = "label_LoaderCoeff";
            this.label_LoaderCoeff.Size = new System.Drawing.Size(292, 19);
            this.label_LoaderCoeff.TabIndex = 4;
            this.label_LoaderCoeff.Text = "Загруженность вагона (по умолчанию 1)";
            // 
            // listBox_VagonName
            // 
            this.listBox_VagonName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox_VagonName.FormattingEnabled = true;
            this.listBox_VagonName.ItemHeight = 15;
            this.listBox_VagonName.Location = new System.Drawing.Point(168, 19);
            this.listBox_VagonName.Name = "listBox_VagonName";
            this.listBox_VagonName.Size = new System.Drawing.Size(202, 124);
            this.listBox_VagonName.TabIndex = 3;
            this.listBox_VagonName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox_VagonName_MouseDoubleClick);
            // 
            // textBox_VagonCount
            // 
            this.textBox_VagonCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_VagonCount.Location = new System.Drawing.Point(304, 149);
            this.textBox_VagonCount.Name = "textBox_VagonCount";
            this.textBox_VagonCount.Size = new System.Drawing.Size(49, 22);
            this.textBox_VagonCount.TabIndex = 2;
            // 
            // label_VagonCount
            // 
            this.label_VagonCount.AutoSize = true;
            this.label_VagonCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_VagonCount.Location = new System.Drawing.Point(6, 152);
            this.label_VagonCount.Name = "label_VagonCount";
            this.label_VagonCount.Size = new System.Drawing.Size(143, 16);
            this.label_VagonCount.TabIndex = 1;
            this.label_VagonCount.Text = "Количество вагонов";
            // 
            // label_VagonName
            // 
            this.label_VagonName.AutoSize = true;
            this.label_VagonName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_VagonName.Location = new System.Drawing.Point(6, 28);
            this.label_VagonName.Name = "label_VagonName";
            this.label_VagonName.Size = new System.Drawing.Size(156, 16);
            this.label_VagonName.TabIndex = 0;
            this.label_VagonName.Text = "Наименование вагона";
            // 
            // label_ConsistEdition
            // 
            this.label_ConsistEdition.AutoSize = true;
            this.label_ConsistEdition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ConsistEdition.Location = new System.Drawing.Point(470, 10);
            this.label_ConsistEdition.Name = "label_ConsistEdition";
            this.label_ConsistEdition.Size = new System.Drawing.Size(119, 16);
            this.label_ConsistEdition.TabIndex = 17;
            this.label_ConsistEdition.Text = "Состав поезда";
            // 
            // groupBox_Loco
            // 
            this.groupBox_Loco.Controls.Add(this.listBox_Loco);
            this.groupBox_Loco.Controls.Add(this.textBox_LocoCount);
            this.groupBox_Loco.Controls.Add(this.label_LocoCount);
            this.groupBox_Loco.Controls.Add(this.button_AddLoco);
            this.groupBox_Loco.Controls.Add(this.label_LocoName);
            this.groupBox_Loco.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_Loco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_Loco.Location = new System.Drawing.Point(10, 37);
            this.groupBox_Loco.Name = "groupBox_Loco";
            this.groupBox_Loco.Size = new System.Drawing.Size(319, 251);
            this.groupBox_Loco.TabIndex = 18;
            this.groupBox_Loco.TabStop = false;
            this.groupBox_Loco.Text = "Локомотив";
            // 
            // listBox_Loco
            // 
            this.listBox_Loco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox_Loco.FormattingEnabled = true;
            this.listBox_Loco.ItemHeight = 15;
            this.listBox_Loco.Location = new System.Drawing.Point(193, 19);
            this.listBox_Loco.Name = "listBox_Loco";
            this.listBox_Loco.Size = new System.Drawing.Size(120, 124);
            this.listBox_Loco.TabIndex = 3;
            this.listBox_Loco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox_Loco_MouseClick);
            // 
            // textBox_LocoCount
            // 
            this.textBox_LocoCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_LocoCount.Location = new System.Drawing.Point(193, 149);
            this.textBox_LocoCount.Name = "textBox_LocoCount";
            this.textBox_LocoCount.Size = new System.Drawing.Size(49, 22);
            this.textBox_LocoCount.TabIndex = 2;
            // 
            // label_LocoCount
            // 
            this.label_LocoCount.AutoSize = true;
            this.label_LocoCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_LocoCount.Location = new System.Drawing.Point(-2, 152);
            this.label_LocoCount.Name = "label_LocoCount";
            this.label_LocoCount.Size = new System.Drawing.Size(176, 16);
            this.label_LocoCount.TabIndex = 1;
            this.label_LocoCount.Text = "Количество локомотивов";
            // 
            // button_AddLoco
            // 
            this.button_AddLoco.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button_AddLoco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_AddLoco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_AddLoco.Location = new System.Drawing.Point(71, 206);
            this.button_AddLoco.Name = "button_AddLoco";
            this.button_AddLoco.Size = new System.Drawing.Size(171, 39);
            this.button_AddLoco.TabIndex = 19;
            this.button_AddLoco.Text = "Добавить локомотив";
            this.button_AddLoco.UseVisualStyleBackColor = false;
            this.button_AddLoco.Click += new System.EventHandler(this.Button_AddLoco_Click);
            // 
            // label_LocoName
            // 
            this.label_LocoName.AutoSize = true;
            this.label_LocoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_LocoName.Location = new System.Drawing.Point(-2, 28);
            this.label_LocoName.Name = "label_LocoName";
            this.label_LocoName.Size = new System.Drawing.Size(189, 16);
            this.label_LocoName.TabIndex = 0;
            this.label_LocoName.Text = "Наименование локомотива";
            // 
            // panel_Options
            // 
            this.panel_Options.AutoSize = true;
            this.panel_Options.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Options.Controls.Add(this.groupBox_Descr);
            this.panel_Options.Controls.Add(this.groupBox_TrainOptons);
            this.panel_Options.Controls.Add(this.checkBox_TrainOptions);
            this.panel_Options.Controls.Add(this.label_TrainOptionsHide);
            this.panel_Options.Controls.Add(this.label_ConsistOptions);
            this.panel_Options.Location = new System.Drawing.Point(7, 3);
            this.panel_Options.MinimumSize = new System.Drawing.Size(970, 275);
            this.panel_Options.Name = "panel_Options";
            this.panel_Options.Size = new System.Drawing.Size(1070, 275);
            this.panel_Options.TabIndex = 1;
            // 
            // groupBox_Descr
            // 
            this.groupBox_Descr.Controls.Add(this.label_CabineInVehicle);
            this.groupBox_Descr.Controls.Add(this.label_Description);
            this.groupBox_Descr.Controls.Add(this.label_Title);
            this.groupBox_Descr.Controls.Add(this.textBox_ConsistName);
            this.groupBox_Descr.Controls.Add(this.textBox_Description);
            this.groupBox_Descr.Controls.Add(this.textBox_CabineInVehicle);
            this.groupBox_Descr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_Descr.Location = new System.Drawing.Point(10, 71);
            this.groupBox_Descr.Name = "groupBox_Descr";
            this.groupBox_Descr.Size = new System.Drawing.Size(401, 188);
            this.groupBox_Descr.TabIndex = 27;
            this.groupBox_Descr.TabStop = false;
            this.groupBox_Descr.Text = "Описание поезда";
            // 
            // label_CabineInVehicle
            // 
            this.label_CabineInVehicle.AutoSize = true;
            this.label_CabineInVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CabineInVehicle.Location = new System.Drawing.Point(7, 159);
            this.label_CabineInVehicle.Name = "label_CabineInVehicle";
            this.label_CabineInVehicle.Size = new System.Drawing.Size(299, 16);
            this.label_CabineInVehicle.TabIndex = 6;
            this.label_CabineInVehicle.Text = "Номер кабины управления (по умолчанию 0)";
            // 
            // label_Description
            // 
            this.label_Description.AutoSize = true;
            this.label_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Description.Location = new System.Drawing.Point(7, 70);
            this.label_Description.Name = "label_Description";
            this.label_Description.Size = new System.Drawing.Size(124, 16);
            this.label_Description.TabIndex = 5;
            this.label_Description.Text = "Описание поезда";
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Title.Location = new System.Drawing.Point(7, 41);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(125, 16);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "Название поезда";
            // 
            // textBox_ConsistName
            // 
            this.textBox_ConsistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_ConsistName.Location = new System.Drawing.Point(138, 37);
            this.textBox_ConsistName.Name = "textBox_ConsistName";
            this.textBox_ConsistName.Size = new System.Drawing.Size(250, 22);
            this.textBox_ConsistName.TabIndex = 1;
            // 
            // textBox_Description
            // 
            this.textBox_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Description.Location = new System.Drawing.Point(137, 64);
            this.textBox_Description.Multiline = true;
            this.textBox_Description.Name = "textBox_Description";
            this.textBox_Description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Description.Size = new System.Drawing.Size(251, 77);
            this.textBox_Description.TabIndex = 8;
            // 
            // textBox_CabineInVehicle
            // 
            this.textBox_CabineInVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_CabineInVehicle.Location = new System.Drawing.Point(316, 156);
            this.textBox_CabineInVehicle.Name = "textBox_CabineInVehicle";
            this.textBox_CabineInVehicle.Size = new System.Drawing.Size(72, 22);
            this.textBox_CabineInVehicle.TabIndex = 9;
            // 
            // groupBox_TrainOptons
            // 
            this.groupBox_TrainOptons.Controls.Add(this.label_ChargingPressure);
            this.groupBox_TrainOptons.Controls.Add(this.label_InitMainResPressure);
            this.groupBox_TrainOptons.Controls.Add(this.label_CouplingModule);
            this.groupBox_TrainOptons.Controls.Add(this.label_NoAir);
            this.groupBox_TrainOptons.Controls.Add(this.checkBox_NoAir);
            this.groupBox_TrainOptons.Controls.Add(this.listBox_CouplingType);
            this.groupBox_TrainOptons.Controls.Add(this.textBox_ChargingPressure);
            this.groupBox_TrainOptons.Controls.Add(this.textBox_InitMainResPressure);
            this.groupBox_TrainOptons.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_TrainOptons.Location = new System.Drawing.Point(418, 71);
            this.groupBox_TrainOptons.Name = "groupBox_TrainOptons";
            this.groupBox_TrainOptons.Size = new System.Drawing.Size(634, 188);
            this.groupBox_TrainOptons.TabIndex = 26;
            this.groupBox_TrainOptons.TabStop = false;
            this.groupBox_TrainOptons.Text = "Тормозные характеристики";
            this.groupBox_TrainOptons.Visible = false;
            // 
            // label_ChargingPressure
            // 
            this.label_ChargingPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ChargingPressure.Location = new System.Drawing.Point(6, 70);
            this.label_ChargingPressure.Name = "label_ChargingPressure";
            this.label_ChargingPressure.Size = new System.Drawing.Size(465, 26);
            this.label_ChargingPressure.TabIndex = 11;
            this.label_ChargingPressure.Text = "Зарядное давление тормозной магистрали (по умолчанию 0.5 МПа)";
            // 
            // label_InitMainResPressure
            // 
            this.label_InitMainResPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_InitMainResPressure.Location = new System.Drawing.Point(7, 110);
            this.label_InitMainResPressure.Name = "label_InitMainResPressure";
            this.label_InitMainResPressure.Size = new System.Drawing.Size(465, 32);
            this.label_InitMainResPressure.TabIndex = 2;
            this.label_InitMainResPressure.Text = "Начальное давление в главных резервуарах (по умолчанию 0.9 МПа)";
            // 
            // label_CouplingModule
            // 
            this.label_CouplingModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_CouplingModule.Location = new System.Drawing.Point(7, 149);
            this.label_CouplingModule.Name = "label_CouplingModule";
            this.label_CouplingModule.Size = new System.Drawing.Size(465, 39);
            this.label_CouplingModule.TabIndex = 4;
            this.label_CouplingModule.Text = "Тип поглощающего аппарата сцепного устройства (по умолчанию ef-coupling)";
            // 
            // label_NoAir
            // 
            this.label_NoAir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_NoAir.Location = new System.Drawing.Point(7, 34);
            this.label_NoAir.Name = "label_NoAir";
            this.label_NoAir.Size = new System.Drawing.Size(465, 36);
            this.label_NoAir.TabIndex = 12;
            this.label_NoAir.Text = "Поезд полностью без воздуха (по умолчанию, главный резервуар заполнен)";
            // 
            // checkBox_NoAir
            // 
            this.checkBox_NoAir.AutoSize = true;
            this.checkBox_NoAir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_NoAir.Location = new System.Drawing.Point(487, 41);
            this.checkBox_NoAir.Name = "checkBox_NoAir";
            this.checkBox_NoAir.Size = new System.Drawing.Size(15, 14);
            this.checkBox_NoAir.TabIndex = 13;
            this.checkBox_NoAir.UseVisualStyleBackColor = true;
            // 
            // listBox_CouplingType
            // 
            this.listBox_CouplingType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox_CouplingType.FormattingEnabled = true;
            this.listBox_CouplingType.ItemHeight = 16;
            this.listBox_CouplingType.Location = new System.Drawing.Point(484, 139);
            this.listBox_CouplingType.Name = "listBox_CouplingType";
            this.listBox_CouplingType.Size = new System.Drawing.Size(150, 36);
            this.listBox_CouplingType.TabIndex = 16;
            // 
            // textBox_ChargingPressure
            // 
            this.textBox_ChargingPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_ChargingPressure.Location = new System.Drawing.Point(487, 70);
            this.textBox_ChargingPressure.Name = "textBox_ChargingPressure";
            this.textBox_ChargingPressure.Size = new System.Drawing.Size(48, 22);
            this.textBox_ChargingPressure.TabIndex = 14;
            // 
            // textBox_InitMainResPressure
            // 
            this.textBox_InitMainResPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_InitMainResPressure.Location = new System.Drawing.Point(487, 107);
            this.textBox_InitMainResPressure.Name = "textBox_InitMainResPressure";
            this.textBox_InitMainResPressure.Size = new System.Drawing.Size(48, 22);
            this.textBox_InitMainResPressure.TabIndex = 15;
            // 
            // checkBox_TrainOptions
            // 
            this.checkBox_TrainOptions.AutoSize = true;
            this.checkBox_TrainOptions.Checked = true;
            this.checkBox_TrainOptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_TrainOptions.Location = new System.Drawing.Point(717, 43);
            this.checkBox_TrainOptions.Name = "checkBox_TrainOptions";
            this.checkBox_TrainOptions.Size = new System.Drawing.Size(15, 14);
            this.checkBox_TrainOptions.TabIndex = 25;
            this.checkBox_TrainOptions.UseVisualStyleBackColor = true;
            this.checkBox_TrainOptions.CheckedChanged += new System.EventHandler(this.CheckBox_TrainOptions_CheckedChanged);
            // 
            // label_TrainOptionsHide
            // 
            this.label_TrainOptionsHide.AutoSize = true;
            this.label_TrainOptionsHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_TrainOptionsHide.Location = new System.Drawing.Point(425, 41);
            this.label_TrainOptionsHide.Name = "label_TrainOptionsHide";
            this.label_TrainOptionsHide.Size = new System.Drawing.Size(263, 16);
            this.label_TrainOptionsHide.TabIndex = 24;
            this.label_TrainOptionsHide.Text = "Характеристики поезда по умолчанию";
            // 
            // label_ConsistOptions
            // 
            this.label_ConsistOptions.AutoSize = true;
            this.label_ConsistOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ConsistOptions.Location = new System.Drawing.Point(414, 9);
            this.label_ConsistOptions.Name = "label_ConsistOptions";
            this.label_ConsistOptions.Size = new System.Drawing.Size(196, 20);
            this.label_ConsistOptions.TabIndex = 0;
            this.label_ConsistOptions.Text = "Характеристики Поезда";
            // 
            // button_Serialize
            // 
            this.button_Serialize.AutoSize = true;
            this.button_Serialize.BackColor = System.Drawing.Color.SeaGreen;
            this.button_Serialize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Serialize.Enabled = false;
            this.button_Serialize.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_Serialize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Serialize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Serialize.Location = new System.Drawing.Point(344, 591);
            this.button_Serialize.Name = "button_Serialize";
            this.button_Serialize.Size = new System.Drawing.Size(172, 39);
            this.button_Serialize.TabIndex = 21;
            this.button_Serialize.Text = "Сохранить состав";
            this.button_Serialize.UseVisualStyleBackColor = false;
            this.button_Serialize.Click += new System.EventHandler(this.Button_Serialize_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.PaleTurquoise;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_CreateNewConsist,
            this.MenuItem_OpenConsist,
            this.MenuItem_SetFolders});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MenuItem_CreateNewConsist
            // 
            this.MenuItem_CreateNewConsist.Name = "MenuItem_CreateNewConsist";
            this.MenuItem_CreateNewConsist.Size = new System.Drawing.Size(304, 22);
            this.MenuItem_CreateNewConsist.Text = "Создать новый состав";
            this.MenuItem_CreateNewConsist.Click += new System.EventHandler(this.MenuItem_CreateNewConsist_Click);
            // 
            // MenuItem_OpenConsist
            // 
            this.MenuItem_OpenConsist.Name = "MenuItem_OpenConsist";
            this.MenuItem_OpenConsist.Size = new System.Drawing.Size(304, 22);
            this.MenuItem_OpenConsist.Text = "Открыть состав";
            this.MenuItem_OpenConsist.Click += new System.EventHandler(this.MenuItem_OpenConsist_Click);
            // 
            // MenuItem_SetFolders
            // 
            this.MenuItem_SetFolders.Name = "MenuItem_SetFolders";
            this.MenuItem_SetFolders.Size = new System.Drawing.Size(304, 22);
            this.MenuItem_SetFolders.Text = "Указать расположение папки с игрой RRS";
            this.MenuItem_SetFolders.Click += new System.EventHandler(this.MenuItem_SetFolders_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор составов RRS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel_Main.ResumeLayout(false);
            this.panel_Main.PerformLayout();
            this.panel_Consist.ResumeLayout(false);
            this.panel_Consist.PerformLayout();
            this.groupBox_Consist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Consists)).EndInit();
            this.groupBox_vehcle.ResumeLayout(false);
            this.groupBox_vehcle.PerformLayout();
            this.groupBox_Loco.ResumeLayout(false);
            this.groupBox_Loco.PerformLayout();
            this.panel_Options.ResumeLayout(false);
            this.panel_Options.PerformLayout();
            this.groupBox_Descr.ResumeLayout(false);
            this.groupBox_Descr.PerformLayout();
            this.groupBox_TrainOptons.ResumeLayout(false);
            this.groupBox_TrainOptons.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Panel panel_Options;
        private System.Windows.Forms.Label label_ConsistOptions;
        private System.Windows.Forms.Label label_CabineInVehicle;
        private System.Windows.Forms.Label label_Description;
        private System.Windows.Forms.Label label_CouplingModule;
        private System.Windows.Forms.Label label_InitMainResPressure;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_ChargingPressure;
        private System.Windows.Forms.TextBox textBox_CabineInVehicle;
        private System.Windows.Forms.TextBox textBox_Description;
        private System.Windows.Forms.TextBox textBox_ConsistName;
        private System.Windows.Forms.Label label_NoAir;
        private System.Windows.Forms.ListBox listBox_CouplingType;
        private System.Windows.Forms.TextBox textBox_InitMainResPressure;
        private System.Windows.Forms.TextBox textBox_ChargingPressure;
        private System.Windows.Forms.CheckBox checkBox_NoAir;
        private System.Windows.Forms.Panel panel_Consist;
        private System.Windows.Forms.GroupBox groupBox_Loco;
        private System.Windows.Forms.ListBox listBox_Loco;
        private System.Windows.Forms.TextBox textBox_LocoCount;
        private System.Windows.Forms.Label label_LocoCount;
        private System.Windows.Forms.Label label_LocoName;
        private System.Windows.Forms.Label label_ConsistEdition;
        private System.Windows.Forms.Button button_AddLoco;
        private System.Windows.Forms.Button button_Serialize;
        private System.Windows.Forms.GroupBox groupBox_vehcle;
        private System.Windows.Forms.ListBox listBox_VagonName;
        private System.Windows.Forms.TextBox textBox_VagonCount;
        private System.Windows.Forms.Label label_VagonCount;
        private System.Windows.Forms.Label label_VagonName;
        private System.Windows.Forms.DataGridView dataGridView_Consists;
        private System.Windows.Forms.Button button_AddVagon;
        private System.Windows.Forms.Label label_LoaderCoeff;
        private System.Windows.Forms.TextBox textBox_Coeff;
        private System.Windows.Forms.GroupBox groupBox_TrainOptons;
        private System.Windows.Forms.CheckBox checkBox_TrainOptions;
        private System.Windows.Forms.Label label_TrainOptionsHide;
        private System.Windows.Forms.Button button_DeleteVehecle;
        private System.Windows.Forms.GroupBox groupBox_Consist;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_SetFolders;
        private System.Windows.Forms.GroupBox groupBox_Descr;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_OpenConsist;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_CreateNewConsist;
        private System.Windows.Forms.Button button_Change;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Coeff;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

