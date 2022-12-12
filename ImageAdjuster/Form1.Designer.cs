namespace ImageAdjuster
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("ここにファイルをドラッグ＆ドロップ");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.checkBox_FlipHorizontal = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_SelectedExec = new System.Windows.Forms.Button();
            this.label_ResizeVirtical = new System.Windows.Forms.Label();
            this.label_ResizeHorizontal = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_ResizeVirtical = new System.Windows.Forms.TextBox();
            this.checkBox_ResizeVirtical = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_ResizeHorizontal = new System.Windows.Forms.TextBox();
            this.checkBox_ResizeHorizontal = new System.Windows.Forms.CheckBox();
            this.label_Progress = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBar_ThuresholdAlpha = new System.Windows.Forms.TrackBar();
            this.textBox_ThuresholdAlpha = new System.Windows.Forms.TextBox();
            this.button_ExecClear = new System.Windows.Forms.Button();
            this.label_AlignRightOrder = new System.Windows.Forms.Label();
            this.label_AlignLeftOrder = new System.Windows.Forms.Label();
            this.label_AlignTopOrder = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_AlignRight = new System.Windows.Forms.TextBox();
            this.checkBox_AlignRight = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_AlignLeft = new System.Windows.Forms.TextBox();
            this.checkBox_AlignLeft = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_AlignTop = new System.Windows.Forms.TextBox();
            this.checkBox_AlignTop = new System.Windows.Forms.CheckBox();
            this.label_AlignBottomOrder = new System.Windows.Forms.Label();
            this.label_MarginAlignOrder = new System.Windows.Forms.Label();
            this.label_FlipVirticalOrder = new System.Windows.Forms.Label();
            this.label_Status = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button_Exec = new System.Windows.Forms.Button();
            this.checkBox_FlipVirtical = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_MarginAdjust = new System.Windows.Forms.TextBox();
            this.checkBox_MarginAdjust = new System.Windows.Forms.CheckBox();
            this.label_FlipHorizontalOrder = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_AlignBottom = new System.Windows.Forms.TextBox();
            this.checkBox_AlignBottom = new System.Windows.Forms.CheckBox();
            this.pictureBox_Before = new System.Windows.Forms.PictureBox();
            this.pictureBox_After = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listView_FileList = new System.Windows.Forms.ListView();
            this.columnHeader_File = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_State = new System.Windows.Forms.ColumnHeader();
            this.columnHeader_Path = new System.Windows.Forms.ColumnHeader();
            this.label_FileName = new System.Windows.Forms.Label();
            this.label_BeforeSizeTitle = new System.Windows.Forms.Label();
            this.label_BeforeSize = new System.Windows.Forms.Label();
            this.label_AfterSizeTitle = new System.Windows.Forms.Label();
            this.label_AfterSize = new System.Windows.Forms.Label();
            this.button_StateReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ThuresholdAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Before)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_After)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_FlipHorizontal
            // 
            this.checkBox_FlipHorizontal.AutoSize = true;
            this.checkBox_FlipHorizontal.Location = new System.Drawing.Point(66, 50);
            this.checkBox_FlipHorizontal.Name = "checkBox_FlipHorizontal";
            this.checkBox_FlipHorizontal.Size = new System.Drawing.Size(74, 19);
            this.checkBox_FlipHorizontal.TabIndex = 0;
            this.checkBox_FlipHorizontal.Text = "左右反転";
            this.checkBox_FlipHorizontal.UseVisualStyleBackColor = true;
            this.checkBox_FlipHorizontal.CheckedChanged += new System.EventHandler(this.checkBox_FlipHorizontal_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_Status);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.button_SelectedExec);
            this.groupBox1.Controls.Add(this.label_ResizeVirtical);
            this.groupBox1.Controls.Add(this.label_ResizeHorizontal);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox_ResizeVirtical);
            this.groupBox1.Controls.Add(this.checkBox_ResizeVirtical);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBox_ResizeHorizontal);
            this.groupBox1.Controls.Add(this.checkBox_ResizeHorizontal);
            this.groupBox1.Controls.Add(this.label_Progress);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.trackBar_ThuresholdAlpha);
            this.groupBox1.Controls.Add(this.textBox_ThuresholdAlpha);
            this.groupBox1.Controls.Add(this.button_ExecClear);
            this.groupBox1.Controls.Add(this.label_AlignRightOrder);
            this.groupBox1.Controls.Add(this.label_AlignLeftOrder);
            this.groupBox1.Controls.Add(this.label_AlignTopOrder);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_AlignRight);
            this.groupBox1.Controls.Add(this.checkBox_AlignRight);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox_AlignLeft);
            this.groupBox1.Controls.Add(this.checkBox_AlignLeft);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_AlignTop);
            this.groupBox1.Controls.Add(this.checkBox_AlignTop);
            this.groupBox1.Controls.Add(this.label_AlignBottomOrder);
            this.groupBox1.Controls.Add(this.label_MarginAlignOrder);
            this.groupBox1.Controls.Add(this.label_FlipVirticalOrder);
            this.groupBox1.Controls.Add(this.button_Exec);
            this.groupBox1.Controls.Add(this.checkBox_FlipVirtical);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_MarginAdjust);
            this.groupBox1.Controls.Add(this.checkBox_MarginAdjust);
            this.groupBox1.Controls.Add(this.label_FlipHorizontalOrder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_AlignBottom);
            this.groupBox1.Controls.Add(this.checkBox_AlignBottom);
            this.groupBox1.Controls.Add(this.checkBox_FlipHorizontal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 506);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // button_SelectedExec
            // 
            this.button_SelectedExec.Location = new System.Drawing.Point(94, 466);
            this.button_SelectedExec.Name = "button_SelectedExec";
            this.button_SelectedExec.Size = new System.Drawing.Size(94, 31);
            this.button_SelectedExec.TabIndex = 41;
            this.button_SelectedExec.Text = "選択実行";
            this.button_SelectedExec.UseVisualStyleBackColor = true;
            this.button_SelectedExec.Click += new System.EventHandler(this.button_SelectedExec_Click);
            // 
            // label_ResizeVirtical
            // 
            this.label_ResizeVirtical.AutoSize = true;
            this.label_ResizeVirtical.Location = new System.Drawing.Point(26, 308);
            this.label_ResizeVirtical.Name = "label_ResizeVirtical";
            this.label_ResizeVirtical.Size = new System.Drawing.Size(13, 15);
            this.label_ResizeVirtical.TabIndex = 40;
            this.label_ResizeVirtical.Text = "1";
            // 
            // label_ResizeHorizontal
            // 
            this.label_ResizeHorizontal.AutoSize = true;
            this.label_ResizeHorizontal.Location = new System.Drawing.Point(26, 276);
            this.label_ResizeHorizontal.Name = "label_ResizeHorizontal";
            this.label_ResizeHorizontal.Size = new System.Drawing.Size(13, 15);
            this.label_ResizeHorizontal.TabIndex = 39;
            this.label_ResizeHorizontal.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(265, 308);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 15);
            this.label14.TabIndex = 38;
            this.label14.Text = "pix";
            // 
            // textBox_ResizeVirtical
            // 
            this.textBox_ResizeVirtical.Location = new System.Drawing.Point(194, 304);
            this.textBox_ResizeVirtical.MaxLength = 5;
            this.textBox_ResizeVirtical.Name = "textBox_ResizeVirtical";
            this.textBox_ResizeVirtical.Size = new System.Drawing.Size(65, 23);
            this.textBox_ResizeVirtical.TabIndex = 37;
            this.textBox_ResizeVirtical.Text = "10";
            this.textBox_ResizeVirtical.TextChanged += new System.EventHandler(this.textBox_ResizeVirtical_TextChanged);
            // 
            // checkBox_ResizeVirtical
            // 
            this.checkBox_ResizeVirtical.AutoSize = true;
            this.checkBox_ResizeVirtical.Location = new System.Drawing.Point(66, 306);
            this.checkBox_ResizeVirtical.Name = "checkBox_ResizeVirtical";
            this.checkBox_ResizeVirtical.Size = new System.Drawing.Size(98, 19);
            this.checkBox_ResizeVirtical.TabIndex = 36;
            this.checkBox_ResizeVirtical.Text = "リサイズ縦指定";
            this.checkBox_ResizeVirtical.UseVisualStyleBackColor = true;
            this.checkBox_ResizeVirtical.CheckedChanged += new System.EventHandler(this.checkBox_ResizeVirtical_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(265, 276);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 15);
            this.label15.TabIndex = 35;
            this.label15.Text = "pix";
            // 
            // textBox_ResizeHorizontal
            // 
            this.textBox_ResizeHorizontal.Location = new System.Drawing.Point(194, 272);
            this.textBox_ResizeHorizontal.MaxLength = 5;
            this.textBox_ResizeHorizontal.Name = "textBox_ResizeHorizontal";
            this.textBox_ResizeHorizontal.Size = new System.Drawing.Size(65, 23);
            this.textBox_ResizeHorizontal.TabIndex = 34;
            this.textBox_ResizeHorizontal.Text = "10";
            this.textBox_ResizeHorizontal.TextChanged += new System.EventHandler(this.textBox_ResizeHorizontal_TextChanged);
            // 
            // checkBox_ResizeHorizontal
            // 
            this.checkBox_ResizeHorizontal.AutoSize = true;
            this.checkBox_ResizeHorizontal.Location = new System.Drawing.Point(66, 274);
            this.checkBox_ResizeHorizontal.Name = "checkBox_ResizeHorizontal";
            this.checkBox_ResizeHorizontal.Size = new System.Drawing.Size(98, 19);
            this.checkBox_ResizeHorizontal.TabIndex = 33;
            this.checkBox_ResizeHorizontal.Text = "リサイズ横指定";
            this.checkBox_ResizeHorizontal.UseVisualStyleBackColor = true;
            this.checkBox_ResizeHorizontal.CheckedChanged += new System.EventHandler(this.checkBox_ResizeHorizontal_CheckedChanged);
            // 
            // label_Progress
            // 
            this.label_Progress.Location = new System.Drawing.Point(78, 451);
            this.label_Progress.Name = "label_Progress";
            this.label_Progress.Size = new System.Drawing.Size(74, 15);
            this.label_Progress.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 377);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "透明判定アルファ しきい値";
            // 
            // trackBar_ThuresholdAlpha
            // 
            this.trackBar_ThuresholdAlpha.LargeChange = 1;
            this.trackBar_ThuresholdAlpha.Location = new System.Drawing.Point(6, 395);
            this.trackBar_ThuresholdAlpha.Maximum = 255;
            this.trackBar_ThuresholdAlpha.Name = "trackBar_ThuresholdAlpha";
            this.trackBar_ThuresholdAlpha.Size = new System.Drawing.Size(182, 45);
            this.trackBar_ThuresholdAlpha.TabIndex = 30;
            this.trackBar_ThuresholdAlpha.TickFrequency = 16;
            this.trackBar_ThuresholdAlpha.ValueChanged += new System.EventHandler(this.trackBar_ThuresholdAlpha_ValueChanged);
            // 
            // textBox_ThuresholdAlpha
            // 
            this.textBox_ThuresholdAlpha.Location = new System.Drawing.Point(194, 395);
            this.textBox_ThuresholdAlpha.MaxLength = 5;
            this.textBox_ThuresholdAlpha.Name = "textBox_ThuresholdAlpha";
            this.textBox_ThuresholdAlpha.Size = new System.Drawing.Size(65, 23);
            this.textBox_ThuresholdAlpha.TabIndex = 29;
            this.textBox_ThuresholdAlpha.Text = "10";
            this.textBox_ThuresholdAlpha.TextChanged += new System.EventHandler(this.textBox_ThuresholdAlpha_TextChanged);
            // 
            // button_ExecClear
            // 
            this.button_ExecClear.Location = new System.Drawing.Point(65, 341);
            this.button_ExecClear.Name = "button_ExecClear";
            this.button_ExecClear.Size = new System.Drawing.Size(75, 23);
            this.button_ExecClear.TabIndex = 28;
            this.button_ExecClear.Text = "クリア";
            this.button_ExecClear.UseVisualStyleBackColor = true;
            this.button_ExecClear.Click += new System.EventHandler(this.button_ExecClear_Click);
            // 
            // label_AlignRightOrder
            // 
            this.label_AlignRightOrder.AutoSize = true;
            this.label_AlignRightOrder.Location = new System.Drawing.Point(26, 244);
            this.label_AlignRightOrder.Name = "label_AlignRightOrder";
            this.label_AlignRightOrder.Size = new System.Drawing.Size(13, 15);
            this.label_AlignRightOrder.TabIndex = 27;
            this.label_AlignRightOrder.Text = "1";
            // 
            // label_AlignLeftOrder
            // 
            this.label_AlignLeftOrder.AutoSize = true;
            this.label_AlignLeftOrder.Location = new System.Drawing.Point(26, 212);
            this.label_AlignLeftOrder.Name = "label_AlignLeftOrder";
            this.label_AlignLeftOrder.Size = new System.Drawing.Size(13, 15);
            this.label_AlignLeftOrder.TabIndex = 26;
            this.label_AlignLeftOrder.Text = "1";
            // 
            // label_AlignTopOrder
            // 
            this.label_AlignTopOrder.AutoSize = true;
            this.label_AlignTopOrder.Location = new System.Drawing.Point(26, 148);
            this.label_AlignTopOrder.Name = "label_AlignTopOrder";
            this.label_AlignTopOrder.Size = new System.Drawing.Size(13, 15);
            this.label_AlignTopOrder.TabIndex = 25;
            this.label_AlignTopOrder.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(265, 244);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 15);
            this.label11.TabIndex = 24;
            this.label11.Text = "pix";
            // 
            // textBox_AlignRight
            // 
            this.textBox_AlignRight.Location = new System.Drawing.Point(194, 240);
            this.textBox_AlignRight.MaxLength = 5;
            this.textBox_AlignRight.Name = "textBox_AlignRight";
            this.textBox_AlignRight.Size = new System.Drawing.Size(65, 23);
            this.textBox_AlignRight.TabIndex = 23;
            this.textBox_AlignRight.Text = "10";
            this.textBox_AlignRight.TextChanged += new System.EventHandler(this.textBox_AlignRight_TextChanged);
            // 
            // checkBox_AlignRight
            // 
            this.checkBox_AlignRight.AutoSize = true;
            this.checkBox_AlignRight.Location = new System.Drawing.Point(66, 242);
            this.checkBox_AlignRight.Name = "checkBox_AlignRight";
            this.checkBox_AlignRight.Size = new System.Drawing.Size(86, 19);
            this.checkBox_AlignRight.TabIndex = 22;
            this.checkBox_AlignRight.Text = "右余白調整";
            this.checkBox_AlignRight.UseVisualStyleBackColor = true;
            this.checkBox_AlignRight.CheckedChanged += new System.EventHandler(this.checkBox_AlignRight_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(265, 212);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 15);
            this.label10.TabIndex = 21;
            this.label10.Text = "pix";
            // 
            // textBox_AlignLeft
            // 
            this.textBox_AlignLeft.Location = new System.Drawing.Point(194, 208);
            this.textBox_AlignLeft.MaxLength = 5;
            this.textBox_AlignLeft.Name = "textBox_AlignLeft";
            this.textBox_AlignLeft.Size = new System.Drawing.Size(65, 23);
            this.textBox_AlignLeft.TabIndex = 20;
            this.textBox_AlignLeft.Text = "10";
            this.textBox_AlignLeft.TextChanged += new System.EventHandler(this.textBox_AlignLeft_TextChanged);
            // 
            // checkBox_AlignLeft
            // 
            this.checkBox_AlignLeft.AutoSize = true;
            this.checkBox_AlignLeft.Location = new System.Drawing.Point(66, 210);
            this.checkBox_AlignLeft.Name = "checkBox_AlignLeft";
            this.checkBox_AlignLeft.Size = new System.Drawing.Size(86, 19);
            this.checkBox_AlignLeft.TabIndex = 19;
            this.checkBox_AlignLeft.Text = "左余白調整";
            this.checkBox_AlignLeft.UseVisualStyleBackColor = true;
            this.checkBox_AlignLeft.CheckedChanged += new System.EventHandler(this.checkBox_AlignLeft_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "pix";
            // 
            // textBox_AlignTop
            // 
            this.textBox_AlignTop.Location = new System.Drawing.Point(194, 144);
            this.textBox_AlignTop.MaxLength = 5;
            this.textBox_AlignTop.Name = "textBox_AlignTop";
            this.textBox_AlignTop.Size = new System.Drawing.Size(65, 23);
            this.textBox_AlignTop.TabIndex = 17;
            this.textBox_AlignTop.Text = "10";
            this.textBox_AlignTop.TextChanged += new System.EventHandler(this.textBox_AlignTop_TextChanged);
            // 
            // checkBox_AlignTop
            // 
            this.checkBox_AlignTop.AutoSize = true;
            this.checkBox_AlignTop.Location = new System.Drawing.Point(66, 146);
            this.checkBox_AlignTop.Name = "checkBox_AlignTop";
            this.checkBox_AlignTop.Size = new System.Drawing.Size(86, 19);
            this.checkBox_AlignTop.TabIndex = 16;
            this.checkBox_AlignTop.Text = "上余白調整";
            this.checkBox_AlignTop.UseVisualStyleBackColor = true;
            this.checkBox_AlignTop.CheckedChanged += new System.EventHandler(this.checkBox_AlignTop_CheckedChanged);
            // 
            // label_AlignBottomOrder
            // 
            this.label_AlignBottomOrder.AutoSize = true;
            this.label_AlignBottomOrder.Location = new System.Drawing.Point(26, 180);
            this.label_AlignBottomOrder.Name = "label_AlignBottomOrder";
            this.label_AlignBottomOrder.Size = new System.Drawing.Size(13, 15);
            this.label_AlignBottomOrder.TabIndex = 15;
            this.label_AlignBottomOrder.Text = "1";
            // 
            // label_MarginAlignOrder
            // 
            this.label_MarginAlignOrder.AutoSize = true;
            this.label_MarginAlignOrder.Location = new System.Drawing.Point(26, 116);
            this.label_MarginAlignOrder.Name = "label_MarginAlignOrder";
            this.label_MarginAlignOrder.Size = new System.Drawing.Size(13, 15);
            this.label_MarginAlignOrder.TabIndex = 14;
            this.label_MarginAlignOrder.Text = "1";
            // 
            // label_FlipVirticalOrder
            // 
            this.label_FlipVirticalOrder.AutoSize = true;
            this.label_FlipVirticalOrder.Location = new System.Drawing.Point(26, 84);
            this.label_FlipVirticalOrder.Name = "label_FlipVirticalOrder";
            this.label_FlipVirticalOrder.Size = new System.Drawing.Size(13, 15);
            this.label_FlipVirticalOrder.TabIndex = 13;
            this.label_FlipVirticalOrder.Text = "1";
            // 
            // label_Status
            // 
            this.label_Status.AutoSize = true;
            this.label_Status.Location = new System.Drawing.Point(78, 433);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(74, 15);
            this.label_Status.TabIndex = 12;
            this.label_Status.Text = "何もしてないよ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 433);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "ステータス";
            // 
            // button_Exec
            // 
            this.button_Exec.Location = new System.Drawing.Point(194, 466);
            this.button_Exec.Name = "button_Exec";
            this.button_Exec.Size = new System.Drawing.Size(94, 31);
            this.button_Exec.TabIndex = 10;
            this.button_Exec.Text = "一括実行";
            this.button_Exec.UseVisualStyleBackColor = true;
            this.button_Exec.Click += new System.EventHandler(this.button_Exec_Click);
            // 
            // checkBox_FlipVirtical
            // 
            this.checkBox_FlipVirtical.AutoSize = true;
            this.checkBox_FlipVirtical.Location = new System.Drawing.Point(66, 82);
            this.checkBox_FlipVirtical.Name = "checkBox_FlipVirtical";
            this.checkBox_FlipVirtical.Size = new System.Drawing.Size(74, 19);
            this.checkBox_FlipVirtical.TabIndex = 9;
            this.checkBox_FlipVirtical.Text = "上下反転";
            this.checkBox_FlipVirtical.UseVisualStyleBackColor = true;
            this.checkBox_FlipVirtical.CheckedChanged += new System.EventHandler(this.checkBox_FlipVirtical_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "pix";
            // 
            // textBox_MarginAdjust
            // 
            this.textBox_MarginAdjust.Location = new System.Drawing.Point(194, 112);
            this.textBox_MarginAdjust.MaxLength = 5;
            this.textBox_MarginAdjust.Name = "textBox_MarginAdjust";
            this.textBox_MarginAdjust.Size = new System.Drawing.Size(65, 23);
            this.textBox_MarginAdjust.TabIndex = 7;
            this.textBox_MarginAdjust.Text = "10";
            this.textBox_MarginAdjust.TextChanged += new System.EventHandler(this.textBox_MarginAdjust_TextChanged);
            // 
            // checkBox_MarginAdjust
            // 
            this.checkBox_MarginAdjust.AutoSize = true;
            this.checkBox_MarginAdjust.Location = new System.Drawing.Point(66, 114);
            this.checkBox_MarginAdjust.Name = "checkBox_MarginAdjust";
            this.checkBox_MarginAdjust.Size = new System.Drawing.Size(122, 19);
            this.checkBox_MarginAdjust.TabIndex = 6;
            this.checkBox_MarginAdjust.Text = "上下左右余白調整";
            this.checkBox_MarginAdjust.UseVisualStyleBackColor = true;
            this.checkBox_MarginAdjust.CheckedChanged += new System.EventHandler(this.checkBox_MarginAdjust_CheckedChanged);
            // 
            // label_FlipHorizontalOrder
            // 
            this.label_FlipHorizontalOrder.AutoSize = true;
            this.label_FlipHorizontalOrder.Location = new System.Drawing.Point(26, 52);
            this.label_FlipHorizontalOrder.Name = "label_FlipHorizontalOrder";
            this.label_FlipHorizontalOrder.Size = new System.Drawing.Size(13, 15);
            this.label_FlipHorizontalOrder.TabIndex = 5;
            this.label_FlipHorizontalOrder.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "処理順序";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "pix";
            // 
            // textBox_AlignBottom
            // 
            this.textBox_AlignBottom.Location = new System.Drawing.Point(194, 176);
            this.textBox_AlignBottom.MaxLength = 5;
            this.textBox_AlignBottom.Name = "textBox_AlignBottom";
            this.textBox_AlignBottom.Size = new System.Drawing.Size(65, 23);
            this.textBox_AlignBottom.TabIndex = 2;
            this.textBox_AlignBottom.Text = "10";
            this.textBox_AlignBottom.TextChanged += new System.EventHandler(this.textBox_AlignBottom_TextChanged);
            // 
            // checkBox_AlignBottom
            // 
            this.checkBox_AlignBottom.AutoSize = true;
            this.checkBox_AlignBottom.Location = new System.Drawing.Point(66, 178);
            this.checkBox_AlignBottom.Name = "checkBox_AlignBottom";
            this.checkBox_AlignBottom.Size = new System.Drawing.Size(86, 19);
            this.checkBox_AlignBottom.TabIndex = 1;
            this.checkBox_AlignBottom.Text = "下余白調整";
            this.checkBox_AlignBottom.UseVisualStyleBackColor = true;
            this.checkBox_AlignBottom.CheckedChanged += new System.EventHandler(this.checkBox_AlignBottom_CheckedChanged);
            // 
            // pictureBox_Before
            // 
            this.pictureBox_Before.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Before.Location = new System.Drawing.Point(651, 39);
            this.pictureBox_Before.Name = "pictureBox_Before";
            this.pictureBox_Before.Size = new System.Drawing.Size(400, 400);
            this.pictureBox_Before.TabIndex = 2;
            this.pictureBox_Before.TabStop = false;
            // 
            // pictureBox_After
            // 
            this.pictureBox_After.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_After.Location = new System.Drawing.Point(1072, 39);
            this.pictureBox_After.Name = "pictureBox_After";
            this.pictureBox_After.Size = new System.Drawing.Size(400, 400);
            this.pictureBox_After.TabIndex = 3;
            this.pictureBox_After.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(651, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "処理前";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1072, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "処理後サンプル";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(341, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "ファイルリスト";
            // 
            // listView_FileList
            // 
            this.listView_FileList.AllowDrop = true;
            this.listView_FileList.AutoArrange = false;
            this.listView_FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_File,
            this.columnHeader_State,
            this.columnHeader_Path});
            this.listView_FileList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_FileList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView_FileList.Location = new System.Drawing.Point(341, 39);
            this.listView_FileList.Name = "listView_FileList";
            this.listView_FileList.Size = new System.Drawing.Size(280, 479);
            this.listView_FileList.TabIndex = 11;
            this.listView_FileList.UseCompatibleStateImageBehavior = false;
            this.listView_FileList.View = System.Windows.Forms.View.Details;
            this.listView_FileList.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.listView_FileList_ColumnWidthChanged);
            this.listView_FileList.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_FileList_DrawColumnHeader);
            this.listView_FileList.SelectedIndexChanged += new System.EventHandler(this.listView_FileList_SelectedIndexChanged);
            this.listView_FileList.Click += new System.EventHandler(this.listView_FileList_Click);
            this.listView_FileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_FileList_DragDrop);
            this.listView_FileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_FileList_DragEnter);
            this.listView_FileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_FileList_KeyDown);
            this.listView_FileList.Layout += new System.Windows.Forms.LayoutEventHandler(this.listView_FileList_Layout);
            // 
            // columnHeader_File
            // 
            this.columnHeader_File.Text = "ファイル";
            this.columnHeader_File.Width = 200;
            // 
            // columnHeader_State
            // 
            this.columnHeader_State.Width = 90;
            // 
            // columnHeader_Path
            // 
            this.columnHeader_Path.Text = "パス";
            this.columnHeader_Path.Width = 0;
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(700, 21);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(0, 15);
            this.label_FileName.TabIndex = 12;
            // 
            // label_BeforeSizeTitle
            // 
            this.label_BeforeSizeTitle.AutoSize = true;
            this.label_BeforeSizeTitle.Location = new System.Drawing.Point(880, 21);
            this.label_BeforeSizeTitle.Name = "label_BeforeSizeTitle";
            this.label_BeforeSizeTitle.Size = new System.Drawing.Size(35, 15);
            this.label_BeforeSizeTitle.TabIndex = 13;
            this.label_BeforeSizeTitle.Text = "サイズ";
            // 
            // label_BeforeSize
            // 
            this.label_BeforeSize.AutoSize = true;
            this.label_BeforeSize.Location = new System.Drawing.Point(921, 21);
            this.label_BeforeSize.Name = "label_BeforeSize";
            this.label_BeforeSize.Size = new System.Drawing.Size(0, 15);
            this.label_BeforeSize.TabIndex = 14;
            // 
            // label_AfterSizeTitle
            // 
            this.label_AfterSizeTitle.AutoSize = true;
            this.label_AfterSizeTitle.Location = new System.Drawing.Point(1173, 21);
            this.label_AfterSizeTitle.Name = "label_AfterSizeTitle";
            this.label_AfterSizeTitle.Size = new System.Drawing.Size(35, 15);
            this.label_AfterSizeTitle.TabIndex = 15;
            this.label_AfterSizeTitle.Text = "サイズ";
            // 
            // label_AfterSize
            // 
            this.label_AfterSize.AutoSize = true;
            this.label_AfterSize.Location = new System.Drawing.Point(1214, 21);
            this.label_AfterSize.Name = "label_AfterSize";
            this.label_AfterSize.Size = new System.Drawing.Size(0, 15);
            this.label_AfterSize.TabIndex = 16;
            // 
            // button_StateReset
            // 
            this.button_StateReset.Location = new System.Drawing.Point(514, 10);
            this.button_StateReset.Name = "button_StateReset";
            this.button_StateReset.Size = new System.Drawing.Size(107, 26);
            this.button_StateReset.TabIndex = 41;
            this.button_StateReset.Text = "処理済リセット";
            this.button_StateReset.UseVisualStyleBackColor = true;
            this.button_StateReset.Click += new System.EventHandler(this.button_StateReset_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 530);
            this.Controls.Add(this.button_StateReset);
            this.Controls.Add(this.label_AfterSize);
            this.Controls.Add(this.label_AfterSizeTitle);
            this.Controls.Add(this.label_BeforeSize);
            this.Controls.Add(this.label_BeforeSizeTitle);
            this.Controls.Add(this.label_FileName);
            this.Controls.Add(this.listView_FileList);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox_After);
            this.Controls.Add(this.pictureBox_Before);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ImageAdjuster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ThuresholdAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Before)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_After)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox checkBox_FlipHorizontal;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox textBox_AlignBottom;
        private CheckBox checkBox_AlignBottom;
        private Label label_Status;
        private Label label8;
        private Button button_Exec;
        private CheckBox checkBox_FlipVirtical;
        private Label label6;
        private TextBox textBox_MarginAdjust;
        private CheckBox checkBox_MarginAdjust;
        private Label label_FlipHorizontalOrder;
        private Label label2;
        private PictureBox pictureBox_Before;
        private PictureBox pictureBox_After;
        private Label label3;
        private Label label4;
        private Label label7;
        private ListView listView_FileList;
        private ColumnHeader columnHeader_File;
        private ColumnHeader columnHeader_Path;
        private Label label_FlipVirticalOrder;
        private Label label_AlignBottomOrder;
        private Label label_MarginAlignOrder;
        private Label label11;
        private TextBox textBox_AlignRight;
        private CheckBox checkBox_AlignRight;
        private Label label10;
        private TextBox textBox_AlignLeft;
        private CheckBox checkBox_AlignLeft;
        private Label label5;
        private TextBox textBox_AlignTop;
        private CheckBox checkBox_AlignTop;
        private Label label_AlignRightOrder;
        private Label label_AlignLeftOrder;
        private Label label_AlignTopOrder;
        private Button button_ExecClear;
        private ColumnHeader columnHeader_State;
        private TrackBar trackBar_ThuresholdAlpha;
        private TextBox textBox_ThuresholdAlpha;
        private Label label9;
        private Label label_FileName;
        private Label label_Progress;
        private Label label_BeforeSizeTitle;
        private Label label_BeforeSize;
        private Label label_AfterSizeTitle;
        private Label label_AfterSize;
        private Label label_ResizeVirtical;
        private Label label_ResizeHorizontal;
        private Label label14;
        private TextBox textBox_ResizeVirtical;
        private CheckBox checkBox_ResizeVirtical;
        private Label label15;
        private TextBox textBox_ResizeHorizontal;
        private CheckBox checkBox_ResizeHorizontal;
        private Button button_StateReset;
        private Button button_SelectedExec;
    }
}