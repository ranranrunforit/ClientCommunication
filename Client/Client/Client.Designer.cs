namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButton = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.localaddrLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.localaddrMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.logLabel = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.sendLabel = new System.Windows.Forms.Label();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.ClientSideInfos = new System.Windows.Forms.GroupBox();
            this.AddInfoLabel = new System.Windows.Forms.Label();
            this.AddComText = new System.Windows.Forms.MaskedTextBox();
            this.TestNumText = new System.Windows.Forms.MaskedTextBox();
            this.TotalNumText = new System.Windows.Forms.MaskedTextBox();
            this.SampleNumText = new System.Windows.Forms.MaskedTextBox();
            this.TestingText = new System.Windows.Forms.MaskedTextBox();
            this.LengthText = new System.Windows.Forms.MaskedTextBox();
            this.DataText = new System.Windows.Forms.MaskedTextBox();
            this.StepText = new System.Windows.Forms.MaskedTextBox();
            this.DataLabel = new System.Windows.Forms.Label();
            this.TestNumLabel = new System.Windows.Forms.Label();
            this.TotalTestLabel = new System.Windows.Forms.Label();
            this.SimpleNumLabel = new System.Windows.Forms.Label();
            this.TestLabel = new System.Windows.Forms.Label();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.StepLabel = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SBox1 = new System.Windows.Forms.GroupBox();
            this.Slabel8 = new System.Windows.Forms.Label();
            this.Slabel7 = new System.Windows.Forms.Label();
            this.Slabel6 = new System.Windows.Forms.Label();
            this.Slabel5 = new System.Windows.Forms.Label();
            this.Slabel4 = new System.Windows.Forms.Label();
            this.Slabel3 = new System.Windows.Forms.Label();
            this.Slabel2 = new System.Windows.Forms.Label();
            this.Slabel1 = new System.Windows.Forms.Label();
            this.Slabel11 = new System.Windows.Forms.Label();
            this.Slabel12 = new System.Windows.Forms.Label();
            this.Slabel13 = new System.Windows.Forms.Label();
            this.Slabel14 = new System.Windows.Forms.Label();
            this.Slabel15 = new System.Windows.Forms.Label();
            this.Slabel16 = new System.Windows.Forms.Label();
            this.Slabel17 = new System.Windows.Forms.Label();
            this.Slabel18 = new System.Windows.Forms.Label();
            this.ClientSideInfos.SuspendLayout();
            this.SBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(13, 13);
            this.connectButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 10);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(116, 28);
            this.connectButton.TabIndex = 28;
            this.connectButton.TabStop = false;
            this.connectButton.Text = "连接";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(600, 21);
            this.portLabel.Margin = new System.Windows.Forms.Padding(8, 0, 1, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(45, 15);
            this.portLabel.TabIndex = 27;
            this.portLabel.Text = "端口:";
            // 
            // localaddrLabel
            // 
            this.localaddrLabel.AutoSize = true;
            this.localaddrLabel.Location = new System.Drawing.Point(386, 21);
            this.localaddrLabel.Margin = new System.Windows.Forms.Padding(8, 0, 1, 0);
            this.localaddrLabel.Name = "localaddrLabel";
            this.localaddrLabel.Size = new System.Drawing.Size(69, 15);
            this.localaddrLabel.TabIndex = 26;
            this.localaddrLabel.Text = "IP 地址:";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(650, 18);
            this.portTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(132, 25);
            this.portTextBox.TabIndex = 25;
            this.portTextBox.TabStop = false;
            this.portTextBox.Text = "9000";
            this.portTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // localaddrMaskedTextBox
            // 
            this.localaddrMaskedTextBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.localaddrMaskedTextBox.Location = new System.Drawing.Point(460, 18);
            this.localaddrMaskedTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.localaddrMaskedTextBox.Mask = "990\\.990\\.990\\.990";
            this.localaddrMaskedTextBox.Name = "localaddrMaskedTextBox";
            this.localaddrMaskedTextBox.Size = new System.Drawing.Size(132, 25);
            this.localaddrMaskedTextBox.TabIndex = 24;
            this.localaddrMaskedTextBox.TabStop = false;
            this.localaddrMaskedTextBox.Text = "127000000001";
            this.localaddrMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(10, 650);
            this.logLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(67, 15);
            this.logLabel.TabIndex = 31;
            this.logLabel.Text = "对话记录";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logTextBox.Location = new System.Drawing.Point(13, 680);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(769, 100);
            this.logTextBox.TabIndex = 30;
            this.logTextBox.TabStop = false;
            // 
            // sendLabel
            // 
            this.sendLabel.AutoSize = true;
            this.sendLabel.Location = new System.Drawing.Point(10, 800);
            this.sendLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sendLabel.Name = "sendLabel";
            this.sendLabel.Size = new System.Drawing.Size(37, 15);
            this.sendLabel.TabIndex = 33;
            this.sendLabel.Text = "发送";
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(13, 820);
            this.sendTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sendTextBox.Size = new System.Drawing.Size(769, 25);
            this.sendTextBox.TabIndex = 32;
            this.sendTextBox.TabStop = false;
            this.sendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendTextBox_KeyDown);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(660, 640);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(116, 28);
            this.clearButton.TabIndex = 34;
            this.clearButton.TabStop = false;
            this.clearButton.Text = "清空对话";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ClientSideInfos
            // 
            this.ClientSideInfos.Controls.Add(this.AddInfoLabel);
            this.ClientSideInfos.Controls.Add(this.AddComText);
            this.ClientSideInfos.Controls.Add(this.TestNumText);
            this.ClientSideInfos.Controls.Add(this.TotalNumText);
            this.ClientSideInfos.Controls.Add(this.SampleNumText);
            this.ClientSideInfos.Controls.Add(this.TestingText);
            this.ClientSideInfos.Controls.Add(this.LengthText);
            this.ClientSideInfos.Controls.Add(this.DataText);
            this.ClientSideInfos.Controls.Add(this.StepText);
            this.ClientSideInfos.Controls.Add(this.DataLabel);
            this.ClientSideInfos.Controls.Add(this.TestNumLabel);
            this.ClientSideInfos.Controls.Add(this.TotalTestLabel);
            this.ClientSideInfos.Controls.Add(this.SimpleNumLabel);
            this.ClientSideInfos.Controls.Add(this.TestLabel);
            this.ClientSideInfos.Controls.Add(this.LengthLabel);
            this.ClientSideInfos.Controls.Add(this.StepLabel);
            this.ClientSideInfos.Location = new System.Drawing.Point(13, 75);
            this.ClientSideInfos.Name = "ClientSideInfos";
            this.ClientSideInfos.Size = new System.Drawing.Size(833, 228);
            this.ClientSideInfos.TabIndex = 35;
            this.ClientSideInfos.TabStop = false;
            this.ClientSideInfos.Text = "客户端信息";
            // 
            // AddInfoLabel
            // 
            this.AddInfoLabel.AutoSize = true;
            this.AddInfoLabel.Location = new System.Drawing.Point(654, 41);
            this.AddInfoLabel.Name = "AddInfoLabel";
            this.AddInfoLabel.Size = new System.Drawing.Size(135, 15);
            this.AddInfoLabel.TabIndex = 44;
            this.AddInfoLabel.Text = "分析流程/信息交流";
            // 
            // AddComText
            // 
            this.AddComText.Location = new System.Drawing.Point(683, 77);
            this.AddComText.Mask = "9&";
            this.AddComText.Name = "AddComText";
            this.AddComText.Size = new System.Drawing.Size(66, 25);
            this.AddComText.TabIndex = 43;
            this.AddComText.Text = "71";
            this.AddComText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TestNumText
            // 
            this.TestNumText.Location = new System.Drawing.Point(539, 77);
            this.TestNumText.Mask = "9&";
            this.TestNumText.Name = "TestNumText";
            this.TestNumText.Size = new System.Drawing.Size(109, 25);
            this.TestNumText.TabIndex = 42;
            this.TestNumText.Text = "61";
            this.TestNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TotalNumText
            // 
            this.TotalNumText.Location = new System.Drawing.Point(438, 77);
            this.TotalNumText.Mask = "99";
            this.TotalNumText.Name = "TotalNumText";
            this.TotalNumText.Size = new System.Drawing.Size(79, 25);
            this.TotalNumText.TabIndex = 41;
            this.TotalNumText.Text = "51";
            this.TotalNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SampleNumText
            // 
            this.SampleNumText.Location = new System.Drawing.Point(254, 77);
            this.SampleNumText.Mask = "9& 9&";
            this.SampleNumText.Name = "SampleNumText";
            this.SampleNumText.Size = new System.Drawing.Size(155, 25);
            this.SampleNumText.TabIndex = 40;
            this.SampleNumText.Text = "3248";
            this.SampleNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TestingText
            // 
            this.TestingText.Location = new System.Drawing.Point(200, 77);
            this.TestingText.Mask = "9&";
            this.TestingText.Name = "TestingText";
            this.TestingText.Size = new System.Drawing.Size(34, 25);
            this.TestingText.TabIndex = 39;
            this.TestingText.Text = "21";
            this.TestingText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LengthText
            // 
            this.LengthText.Location = new System.Drawing.Point(102, 77);
            this.LengthText.Mask = "9&";
            this.LengthText.Name = "LengthText";
            this.LengthText.Size = new System.Drawing.Size(79, 25);
            this.LengthText.TabIndex = 38;
            this.LengthText.Text = "19";
            this.LengthText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DataText
            // 
            this.DataText.Location = new System.Drawing.Point(25, 168);
            this.DataText.Mask = "9& 9& 9& 9& 9& 9& 9& 9& 9&";
            this.DataText.Name = "DataText";
            this.DataText.Size = new System.Drawing.Size(744, 25);
            this.DataText.TabIndex = 37;
            this.DataText.Text = "010203040506070809";
            // 
            // StepText
            // 
            this.StepText.Location = new System.Drawing.Point(22, 77);
            this.StepText.Mask = "99";
            this.StepText.Name = "StepText";
            this.StepText.Size = new System.Drawing.Size(64, 25);
            this.StepText.TabIndex = 36;
            this.StepText.Text = "01";
            this.StepText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.Location = new System.Drawing.Point(22, 134);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(420, 15);
            this.DataLabel.TabIndex = 6;
            this.DataLabel.Text = "分析数据（数据次序：Pr、Nd、Ti、Mo、W、Al、Si、Fe、C）";
            // 
            // TestNumLabel
            // 
            this.TestNumLabel.AutoSize = true;
            this.TestNumLabel.Location = new System.Drawing.Point(536, 41);
            this.TestNumLabel.Name = "TestNumLabel";
            this.TestNumLabel.Size = new System.Drawing.Size(105, 15);
            this.TestNumLabel.TabIndex = 5;
            this.TestNumLabel.Text = "状态/检测次数";
            // 
            // TotalTestLabel
            // 
            this.TotalTestLabel.AutoSize = true;
            this.TotalTestLabel.Location = new System.Drawing.Point(435, 41);
            this.TotalTestLabel.Name = "TotalTestLabel";
            this.TotalTestLabel.Size = new System.Drawing.Size(82, 15);
            this.TotalTestLabel.TabIndex = 4;
            this.TotalTestLabel.Text = "总检测次数";
            // 
            // SimpleNumLabel
            // 
            this.SimpleNumLabel.AutoSize = true;
            this.SimpleNumLabel.Location = new System.Drawing.Point(303, 41);
            this.SimpleNumLabel.Name = "SimpleNumLabel";
            this.SimpleNumLabel.Size = new System.Drawing.Size(52, 15);
            this.SimpleNumLabel.TabIndex = 3;
            this.SimpleNumLabel.Text = "样品号";
            // 
            // TestLabel
            // 
            this.TestLabel.AutoSize = true;
            this.TestLabel.Location = new System.Drawing.Point(197, 41);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(37, 15);
            this.TestLabel.TabIndex = 2;
            this.TestLabel.Text = "校验";
            // 
            // LengthLabel
            // 
            this.LengthLabel.AutoSize = true;
            this.LengthLabel.Location = new System.Drawing.Point(99, 41);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(82, 15);
            this.LengthLabel.TabIndex = 1;
            this.LengthLabel.Text = "指令总长度";
            // 
            // StepLabel
            // 
            this.StepLabel.AutoSize = true;
            this.StepLabel.Location = new System.Drawing.Point(19, 41);
            this.StepLabel.Name = "StepLabel";
            this.StepLabel.Size = new System.Drawing.Size(67, 15);
            this.StepLabel.TabIndex = 0;
            this.StepLabel.Text = "操作步骤";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(666, 295);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(116, 28);
            this.btnSubmit.TabIndex = 43;
            this.btnSubmit.Text = "发送";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // SBox1
            // 
            this.SBox1.Controls.Add(this.Slabel18);
            this.SBox1.Controls.Add(this.Slabel17);
            this.SBox1.Controls.Add(this.Slabel16);
            this.SBox1.Controls.Add(this.Slabel15);
            this.SBox1.Controls.Add(this.Slabel14);
            this.SBox1.Controls.Add(this.Slabel13);
            this.SBox1.Controls.Add(this.Slabel12);
            this.SBox1.Controls.Add(this.Slabel11);
            this.SBox1.Controls.Add(this.Slabel8);
            this.SBox1.Controls.Add(this.Slabel7);
            this.SBox1.Controls.Add(this.Slabel6);
            this.SBox1.Controls.Add(this.Slabel5);
            this.SBox1.Controls.Add(this.Slabel4);
            this.SBox1.Controls.Add(this.Slabel3);
            this.SBox1.Controls.Add(this.Slabel2);
            this.SBox1.Controls.Add(this.Slabel1);
            this.SBox1.Location = new System.Drawing.Point(13, 342);
            this.SBox1.Name = "SBox1";
            this.SBox1.Size = new System.Drawing.Size(833, 228);
            this.SBox1.TabIndex = 44;
            this.SBox1.TabStop = false;
            this.SBox1.Text = "服务器信息";
            // 
            // Slabel8
            // 
            this.Slabel8.AutoSize = true;
            this.Slabel8.Location = new System.Drawing.Point(398, 184);
            this.Slabel8.Name = "Slabel8";
            this.Slabel8.Size = new System.Drawing.Size(52, 15);
            this.Slabel8.TabIndex = 7;
            this.Slabel8.Text = "控样数";
            // 
            // Slabel7
            // 
            this.Slabel7.AutoSize = true;
            this.Slabel7.Location = new System.Drawing.Point(33, 184);
            this.Slabel7.Name = "Slabel7";
            this.Slabel7.Size = new System.Drawing.Size(67, 15);
            this.Slabel7.TabIndex = 6;
            this.Slabel7.Text = "分析步骤";
            // 
            // Slabel6
            // 
            this.Slabel6.AutoSize = true;
            this.Slabel6.Location = new System.Drawing.Point(384, 114);
            this.Slabel6.Name = "Slabel6";
            this.Slabel6.Size = new System.Drawing.Size(105, 15);
            this.Slabel6.TabIndex = 5;
            this.Slabel6.Text = "状态/检测次数";
            // 
            // Slabel5
            // 
            this.Slabel5.AutoSize = true;
            this.Slabel5.Location = new System.Drawing.Point(33, 114);
            this.Slabel5.Name = "Slabel5";
            this.Slabel5.Size = new System.Drawing.Size(82, 15);
            this.Slabel5.TabIndex = 4;
            this.Slabel5.Text = "总检测次数";
            // 
            // Slabel4
            // 
            this.Slabel4.AutoSize = true;
            this.Slabel4.Location = new System.Drawing.Point(610, 40);
            this.Slabel4.Name = "Slabel4";
            this.Slabel4.Size = new System.Drawing.Size(52, 15);
            this.Slabel4.TabIndex = 3;
            this.Slabel4.Text = "样品号";
            // 
            // Slabel3
            // 
            this.Slabel3.AutoSize = true;
            this.Slabel3.Location = new System.Drawing.Point(413, 40);
            this.Slabel3.Name = "Slabel3";
            this.Slabel3.Size = new System.Drawing.Size(37, 15);
            this.Slabel3.TabIndex = 2;
            this.Slabel3.Text = "校验";
            // 
            // Slabel2
            // 
            this.Slabel2.AutoSize = true;
            this.Slabel2.Location = new System.Drawing.Point(209, 40);
            this.Slabel2.Name = "Slabel2";
            this.Slabel2.Size = new System.Drawing.Size(82, 15);
            this.Slabel2.TabIndex = 1;
            this.Slabel2.Text = "指令总长度";
            // 
            // Slabel1
            // 
            this.Slabel1.AutoSize = true;
            this.Slabel1.Location = new System.Drawing.Point(33, 40);
            this.Slabel1.Name = "Slabel1";
            this.Slabel1.Size = new System.Drawing.Size(67, 15);
            this.Slabel1.TabIndex = 0;
            this.Slabel1.Text = "操作步骤";
            // 
            // Slabel11
            // 
            this.Slabel11.AutoSize = true;
            this.Slabel11.Location = new System.Drawing.Point(36, 77);
            this.Slabel11.Name = "Slabel11";
            this.Slabel11.Size = new System.Drawing.Size(0, 15);
            this.Slabel11.TabIndex = 8;
            this.Slabel11.Text = "  ";
            // 
            // Slabel12
            // 
            this.Slabel12.AutoSize = true;
            this.Slabel12.Location = new System.Drawing.Point(212, 77);
            this.Slabel12.Name = "Slabel12";
            this.Slabel12.Size = new System.Drawing.Size(0, 15);
            this.Slabel12.TabIndex = 9;
            this.Slabel12.Text = "  ";
            // 
            // Slabel13
            // 
            this.Slabel13.AutoSize = true;
            this.Slabel13.Location = new System.Drawing.Point(401, 77);
            this.Slabel13.Name = "Slabel13";
            this.Slabel13.Size = new System.Drawing.Size(0, 15);
            this.Slabel13.TabIndex = 10;
            this.Slabel13.Text = "  ";
            // 
            // Slabel14
            // 
            this.Slabel14.AutoSize = true;
            this.Slabel14.Location = new System.Drawing.Point(610, 77);
            this.Slabel14.Name = "Slabel14";
            this.Slabel14.Size = new System.Drawing.Size(55, 15);
            this.Slabel14.TabIndex = 11;
            this.Slabel14.Text = "  ";

            // 
            // Slabel15
            // 
            this.Slabel15.AutoSize = true;
            this.Slabel15.Location = new System.Drawing.Point(36, 144);
            this.Slabel15.Name = "Slabel15";
            this.Slabel15.Size = new System.Drawing.Size(55, 15);
            this.Slabel15.TabIndex = 12;
            this.Slabel15.Text = "  ";
            // 
            // Slabel16
            // 
            this.Slabel16.AutoSize = true;
            this.Slabel16.Location = new System.Drawing.Point(404, 144);
            this.Slabel16.Name = "Slabel16";
            this.Slabel16.Size = new System.Drawing.Size(55, 15);
            this.Slabel16.TabIndex = 13;
            this.Slabel16.Text = "  ";
            // 
            // Slabel17
            // 
            this.Slabel17.AutoSize = true;
            this.Slabel17.Location = new System.Drawing.Point(129, 183);
            this.Slabel17.Name = "Slabel17";
            this.Slabel17.Size = new System.Drawing.Size(55, 15);
            this.Slabel17.TabIndex = 14;
            this.Slabel17.Text = "  ";
            // 
            // Slabel18
            // 
            this.Slabel18.AutoSize = true;
            this.Slabel18.Location = new System.Drawing.Point(480, 183);
            this.Slabel18.Name = "Slabel18";
            this.Slabel18.Size = new System.Drawing.Size(55, 15);
            this.Slabel18.TabIndex = 15;
            this.Slabel18.Text = "  ";
            // 
            // Client
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(858, 853);
            this.Controls.Add(this.SBox1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.ClientSideInfos);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.sendLabel);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.localaddrLabel);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.localaddrMaskedTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Client";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.ClientSideInfos.ResumeLayout(false);
            this.ClientSideInfos.PerformLayout();
            this.SBox1.ResumeLayout(false);
            this.SBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label localaddrLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.MaskedTextBox localaddrMaskedTextBox;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Label sendLabel;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox ClientSideInfos;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.Label TestNumLabel;
        private System.Windows.Forms.Label TotalTestLabel;
        private System.Windows.Forms.Label SimpleNumLabel;
        private System.Windows.Forms.Label TestLabel;
        private System.Windows.Forms.Label LengthLabel;
        private System.Windows.Forms.Label StepLabel;
        private System.Windows.Forms.MaskedTextBox TestNumText;
        private System.Windows.Forms.MaskedTextBox TotalNumText;
        private System.Windows.Forms.MaskedTextBox SampleNumText;
        private System.Windows.Forms.MaskedTextBox TestingText;
        private System.Windows.Forms.MaskedTextBox LengthText;
        private System.Windows.Forms.MaskedTextBox DataText;
        private System.Windows.Forms.MaskedTextBox StepText;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label AddInfoLabel;
        private System.Windows.Forms.MaskedTextBox AddComText;
        private System.Windows.Forms.GroupBox SBox1;
        private System.Windows.Forms.Label Slabel8;
        private System.Windows.Forms.Label Slabel7;
        private System.Windows.Forms.Label Slabel6;
        private System.Windows.Forms.Label Slabel5;
        private System.Windows.Forms.Label Slabel4;
        private System.Windows.Forms.Label Slabel3;
        private System.Windows.Forms.Label Slabel2;
        private System.Windows.Forms.Label Slabel1;
        private System.Windows.Forms.Label Slabel18;
        private System.Windows.Forms.Label Slabel17;
        private System.Windows.Forms.Label Slabel16;
        private System.Windows.Forms.Label Slabel15;
        private System.Windows.Forms.Label Slabel14;
        private System.Windows.Forms.Label Slabel13;
        private System.Windows.Forms.Label Slabel12;
        private System.Windows.Forms.Label Slabel11;
    }
}

