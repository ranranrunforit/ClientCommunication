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
            this.ClientSideInfos = new System.Windows.Forms.GroupBox();
            this.ChecksumLabel = new System.Windows.Forms.Label();
            this.DataText = new System.Windows.Forms.TextBox();
            this.AddInfoLabel = new System.Windows.Forms.Label();
            this.AddComText = new System.Windows.Forms.MaskedTextBox();
            this.TestNumText = new System.Windows.Forms.MaskedTextBox();
            this.TotalNumText = new System.Windows.Forms.MaskedTextBox();
            this.SampleNumText = new System.Windows.Forms.MaskedTextBox();
            this.LengthText = new System.Windows.Forms.MaskedTextBox();
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
            this.Slabel18 = new System.Windows.Forms.Label();
            this.Slabel17 = new System.Windows.Forms.Label();
            this.Slabel16 = new System.Windows.Forms.Label();
            this.Slabel15 = new System.Windows.Forms.Label();
            this.Slabel14 = new System.Windows.Forms.Label();
            this.Slabel13 = new System.Windows.Forms.Label();
            this.Slabel12 = new System.Windows.Forms.Label();
            this.Slabel11 = new System.Windows.Forms.Label();
            this.Slabel8 = new System.Windows.Forms.Label();
            this.Slabel7 = new System.Windows.Forms.Label();
            this.Slabel6 = new System.Windows.Forms.Label();
            this.Slabel5 = new System.Windows.Forms.Label();
            this.Slabel4 = new System.Windows.Forms.Label();
            this.Slabel3 = new System.Windows.Forms.Label();
            this.Slabel2 = new System.Windows.Forms.Label();
            this.Slabel1 = new System.Windows.Forms.Label();
            this.ClientSideInfos.SuspendLayout();
            this.SBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.portLabel.Location = new System.Drawing.Point(692, 21);
            this.portLabel.Margin = new System.Windows.Forms.Padding(8, 0, 1, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(59, 20);
            this.portLabel.TabIndex = 27;
            this.portLabel.Text = "端口:";
            // 
            // localaddrLabel
            // 
            this.localaddrLabel.AutoSize = true;
            this.localaddrLabel.Location = new System.Drawing.Point(386, 21);
            this.localaddrLabel.Margin = new System.Windows.Forms.Padding(8, 0, 1, 0);
            this.localaddrLabel.Name = "localaddrLabel";
            this.localaddrLabel.Size = new System.Drawing.Size(89, 20);
            this.localaddrLabel.TabIndex = 26;
            this.localaddrLabel.Text = "IP 地址:";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(757, 18);
            this.portTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(89, 30);
            this.portTextBox.TabIndex = 25;
            this.portTextBox.TabStop = false;
            this.portTextBox.Text = "9000";
            this.portTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // localaddrMaskedTextBox
            // 
            this.localaddrMaskedTextBox.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.localaddrMaskedTextBox.Location = new System.Drawing.Point(480, 18);
            this.localaddrMaskedTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.localaddrMaskedTextBox.Mask = "990\\.990\\.990\\.990";
            this.localaddrMaskedTextBox.Name = "localaddrMaskedTextBox";
            this.localaddrMaskedTextBox.Size = new System.Drawing.Size(171, 30);
            this.localaddrMaskedTextBox.TabIndex = 24;
            this.localaddrMaskedTextBox.TabStop = false;
            this.localaddrMaskedTextBox.Text = "192168001102";
            this.localaddrMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(13, 602);
            this.logLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(89, 20);
            this.logLabel.TabIndex = 31;
            this.logLabel.Text = "对话记录";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logTextBox.Location = new System.Drawing.Point(13, 626);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(939, 114);
            this.logTextBox.TabIndex = 30;
            this.logTextBox.TabStop = false;
            // 
            // ClientSideInfos
            // 
            this.ClientSideInfos.Controls.Add(this.ChecksumLabel);
            this.ClientSideInfos.Controls.Add(this.DataText);
            this.ClientSideInfos.Controls.Add(this.AddInfoLabel);
            this.ClientSideInfos.Controls.Add(this.AddComText);
            this.ClientSideInfos.Controls.Add(this.TestNumText);
            this.ClientSideInfos.Controls.Add(this.TotalNumText);
            this.ClientSideInfos.Controls.Add(this.SampleNumText);
            this.ClientSideInfos.Controls.Add(this.LengthText);
            this.ClientSideInfos.Controls.Add(this.StepText);
            this.ClientSideInfos.Controls.Add(this.DataLabel);
            this.ClientSideInfos.Controls.Add(this.TestNumLabel);
            this.ClientSideInfos.Controls.Add(this.TotalTestLabel);
            this.ClientSideInfos.Controls.Add(this.SimpleNumLabel);
            this.ClientSideInfos.Controls.Add(this.TestLabel);
            this.ClientSideInfos.Controls.Add(this.LengthLabel);
            this.ClientSideInfos.Controls.Add(this.StepLabel);
            this.ClientSideInfos.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSideInfos.Location = new System.Drawing.Point(13, 75);
            this.ClientSideInfos.Name = "ClientSideInfos";
            this.ClientSideInfos.Size = new System.Drawing.Size(939, 256);
            this.ClientSideInfos.TabIndex = 35;
            this.ClientSideInfos.TabStop = false;
            this.ClientSideInfos.Text = "客户端信息";
            // 
            // ChecksumLabel
            // 
            this.ChecksumLabel.AutoSize = true;
            this.ChecksumLabel.Location = new System.Drawing.Point(804, 172);
            this.ChecksumLabel.Name = "ChecksumLabel";
            this.ChecksumLabel.Size = new System.Drawing.Size(29, 20);
            this.ChecksumLabel.TabIndex = 47;
            this.ChecksumLabel.Text = "FF";
            this.ChecksumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataText
            // 
            this.DataText.Location = new System.Drawing.Point(24, 162);
            this.DataText.Multiline = true;
            this.DataText.Name = "DataText";
            this.DataText.Size = new System.Drawing.Size(518, 78);
            this.DataText.TabIndex = 46;
            this.DataText.Text = "32.4897 51.2486 37.8932 48.9751 24.8637 89.3248 97.5124 86.3789 32.4897";
            // 
            // AddInfoLabel
            // 
            this.AddInfoLabel.AutoSize = true;
            this.AddInfoLabel.Location = new System.Drawing.Point(533, 134);
            this.AddInfoLabel.Name = "AddInfoLabel";
            this.AddInfoLabel.Size = new System.Drawing.Size(179, 20);
            this.AddInfoLabel.TabIndex = 44;
            this.AddInfoLabel.Text = "分析流程/信息交流";
            // 
            // AddComText
            // 
            this.AddComText.Location = new System.Drawing.Point(594, 162);
            this.AddComText.Mask = "&&";
            this.AddComText.Name = "AddComText";
            this.AddComText.Size = new System.Drawing.Size(44, 30);
            this.AddComText.TabIndex = 43;
            this.AddComText.Text = "71";
            this.AddComText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TestNumText
            // 
            this.TestNumText.Location = new System.Drawing.Point(683, 77);
            this.TestNumText.Mask = "&& &&";
            this.TestNumText.Name = "TestNumText";
            this.TestNumText.Size = new System.Drawing.Size(96, 30);
            this.TestNumText.TabIndex = 42;
            this.TestNumText.Text = "6100";
            this.TestNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TotalNumText
            // 
            this.TotalNumText.Location = new System.Drawing.Point(537, 77);
            this.TotalNumText.Mask = "&&";
            this.TotalNumText.Name = "TotalNumText";
            this.TotalNumText.Size = new System.Drawing.Size(44, 30);
            this.TotalNumText.TabIndex = 41;
            this.TotalNumText.Text = "51";
            this.TotalNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SampleNumText
            // 
            this.SampleNumText.Location = new System.Drawing.Point(249, 77);
            this.SampleNumText.Mask = "9999999";
            this.SampleNumText.Name = "SampleNumText";
            this.SampleNumText.Size = new System.Drawing.Size(162, 30);
            this.SampleNumText.TabIndex = 40;
            this.SampleNumText.Text = "4569837";
            this.SampleNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LengthText
            // 
            this.LengthText.Location = new System.Drawing.Point(129, 77);
            this.LengthText.Mask = "&&";
            this.LengthText.Name = "LengthText";
            this.LengthText.Size = new System.Drawing.Size(44, 30);
            this.LengthText.TabIndex = 38;
            this.LengthText.Text = "19";
            this.LengthText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StepText
            // 
            this.StepText.Location = new System.Drawing.Point(24, 77);
            this.StepText.Mask = "&&";
            this.StepText.Name = "StepText";
            this.StepText.Size = new System.Drawing.Size(44, 30);
            this.StepText.TabIndex = 36;
            this.StepText.Text = "01";
            this.StepText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.Location = new System.Drawing.Point(2, 134);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(529, 20);
            this.DataLabel.TabIndex = 6;
            this.DataLabel.Text = "分析数据(数据次序：Pr、Nd、Ti、Mo、W、Al、Si、Fe、C)";
            // 
            // TestNumLabel
            // 
            this.TestNumLabel.AutoSize = true;
            this.TestNumLabel.Location = new System.Drawing.Point(655, 41);
            this.TestNumLabel.Name = "TestNumLabel";
            this.TestNumLabel.Size = new System.Drawing.Size(149, 20);
            this.TestNumLabel.TabIndex = 5;
            this.TestNumLabel.Text = "状态和检测次数";
            // 
            // TotalTestLabel
            // 
            this.TotalTestLabel.AutoSize = true;
            this.TotalTestLabel.Location = new System.Drawing.Point(501, 41);
            this.TotalTestLabel.Name = "TotalTestLabel";
            this.TotalTestLabel.Size = new System.Drawing.Size(109, 20);
            this.TotalTestLabel.TabIndex = 4;
            this.TotalTestLabel.Text = "总检测次数";
            // 
            // SimpleNumLabel
            // 
            this.SimpleNumLabel.AutoSize = true;
            this.SimpleNumLabel.Location = new System.Drawing.Point(291, 41);
            this.SimpleNumLabel.Name = "SimpleNumLabel";
            this.SimpleNumLabel.Size = new System.Drawing.Size(69, 20);
            this.SimpleNumLabel.TabIndex = 3;
            this.SimpleNumLabel.Text = "样品号";
            // 
            // TestLabel
            // 
            this.TestLabel.Location = new System.Drawing.Point(718, 122);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(215, 44);
            this.TestLabel.TabIndex = 2;
            this.TestLabel.Text = "校验\r\n(无需输入，自动更新)";
            this.TestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LengthLabel
            // 
            this.LengthLabel.AutoSize = true;
            this.LengthLabel.Location = new System.Drawing.Point(97, 41);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(109, 20);
            this.LengthLabel.TabIndex = 1;
            this.LengthLabel.Text = "指令总长度";
            // 
            // StepLabel
            // 
            this.StepLabel.AutoSize = true;
            this.StepLabel.Location = new System.Drawing.Point(2, 41);
            this.StepLabel.Name = "StepLabel";
            this.StepLabel.Size = new System.Drawing.Size(89, 20);
            this.StepLabel.TabIndex = 0;
            this.StepLabel.Text = "操作步骤";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.Location = new System.Drawing.Point(774, 316);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(116, 28);
            this.btnSubmit.TabIndex = 43;
            this.btnSubmit.TabStop = false;
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
            this.SBox1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SBox1.Location = new System.Drawing.Point(14, 337);
            this.SBox1.Name = "SBox1";
            this.SBox1.Size = new System.Drawing.Size(939, 262);
            this.SBox1.TabIndex = 44;
            this.SBox1.TabStop = false;
            this.SBox1.Text = "服务器信息";
            // 
            // Slabel18
            // 
            this.Slabel18.AutoSize = true;
            this.Slabel18.Location = new System.Drawing.Point(689, 193);
            this.Slabel18.Name = "Slabel18";
            this.Slabel18.Size = new System.Drawing.Size(29, 20);
            this.Slabel18.TabIndex = 15;
            this.Slabel18.Text = "  ";
            this.Slabel18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel17
            // 
            this.Slabel17.AutoSize = true;
            this.Slabel17.Location = new System.Drawing.Point(462, 193);
            this.Slabel17.Name = "Slabel17";
            this.Slabel17.Size = new System.Drawing.Size(29, 20);
            this.Slabel17.TabIndex = 14;
            this.Slabel17.Text = "  ";
            this.Slabel17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel16
            // 
            this.Slabel16.AutoSize = true;
            this.Slabel16.Location = new System.Drawing.Point(36, 193);
            this.Slabel16.Name = "Slabel16";
            this.Slabel16.Size = new System.Drawing.Size(29, 20);
            this.Slabel16.TabIndex = 13;
            this.Slabel16.Text = "  ";
            this.Slabel16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel15
            // 
            this.Slabel15.AutoSize = true;
            this.Slabel15.Location = new System.Drawing.Point(724, 85);
            this.Slabel15.Name = "Slabel15";
            this.Slabel15.Size = new System.Drawing.Size(29, 20);
            this.Slabel15.TabIndex = 12;
            this.Slabel15.Text = "  ";
            this.Slabel15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel14
            // 
            this.Slabel14.AutoSize = true;
            this.Slabel14.Location = new System.Drawing.Point(539, 85);
            this.Slabel14.Name = "Slabel14";
            this.Slabel14.Size = new System.Drawing.Size(29, 20);
            this.Slabel14.TabIndex = 11;
            this.Slabel14.Text = "  ";
            this.Slabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel13
            // 
            this.Slabel13.AutoSize = true;
            this.Slabel13.Location = new System.Drawing.Point(330, 85);
            this.Slabel13.Name = "Slabel13";
            this.Slabel13.Size = new System.Drawing.Size(29, 20);
            this.Slabel13.TabIndex = 10;
            this.Slabel13.Text = "  ";
            this.Slabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel12
            // 
            this.Slabel12.AutoSize = true;
            this.Slabel12.Location = new System.Drawing.Point(165, 85);
            this.Slabel12.Name = "Slabel12";
            this.Slabel12.Size = new System.Drawing.Size(29, 20);
            this.Slabel12.TabIndex = 9;
            this.Slabel12.Text = "  ";
            this.Slabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel11
            // 
            this.Slabel11.AutoSize = true;
            this.Slabel11.Location = new System.Drawing.Point(6, 85);
            this.Slabel11.Name = "Slabel11";
            this.Slabel11.Size = new System.Drawing.Size(29, 20);
            this.Slabel11.TabIndex = 8;
            this.Slabel11.Text = "  ";
            this.Slabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Slabel8
            // 
            this.Slabel8.AutoSize = true;
            this.Slabel8.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel8.Location = new System.Drawing.Point(724, 145);
            this.Slabel8.Name = "Slabel8";
            this.Slabel8.Size = new System.Drawing.Size(69, 20);
            this.Slabel8.TabIndex = 7;
            this.Slabel8.Text = "控样数";
            // 
            // Slabel7
            // 
            this.Slabel7.AutoSize = true;
            this.Slabel7.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel7.Location = new System.Drawing.Point(479, 145);
            this.Slabel7.Name = "Slabel7";
            this.Slabel7.Size = new System.Drawing.Size(89, 20);
            this.Slabel7.TabIndex = 6;
            this.Slabel7.Text = "分析步骤";
            // 
            // Slabel6
            // 
            this.Slabel6.AutoSize = true;
            this.Slabel6.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel6.Location = new System.Drawing.Point(45, 145);
            this.Slabel6.Name = "Slabel6";
            this.Slabel6.Size = new System.Drawing.Size(149, 20);
            this.Slabel6.TabIndex = 5;
            this.Slabel6.Text = "状态和检测次数";
            // 
            // Slabel5
            // 
            this.Slabel5.AutoSize = true;
            this.Slabel5.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel5.Location = new System.Drawing.Point(739, 40);
            this.Slabel5.Name = "Slabel5";
            this.Slabel5.Size = new System.Drawing.Size(109, 20);
            this.Slabel5.TabIndex = 4;
            this.Slabel5.Text = "总检测次数";
            // 
            // Slabel4
            // 
            this.Slabel4.AutoSize = true;
            this.Slabel4.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel4.Location = new System.Drawing.Point(568, 40);
            this.Slabel4.Name = "Slabel4";
            this.Slabel4.Size = new System.Drawing.Size(69, 20);
            this.Slabel4.TabIndex = 3;
            this.Slabel4.Text = "样品号";
            // 
            // Slabel3
            // 
            this.Slabel3.AutoSize = true;
            this.Slabel3.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel3.Location = new System.Drawing.Point(378, 40);
            this.Slabel3.Name = "Slabel3";
            this.Slabel3.Size = new System.Drawing.Size(49, 20);
            this.Slabel3.TabIndex = 2;
            this.Slabel3.Text = "校验";
            // 
            // Slabel2
            // 
            this.Slabel2.AutoSize = true;
            this.Slabel2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel2.Location = new System.Drawing.Point(189, 40);
            this.Slabel2.Name = "Slabel2";
            this.Slabel2.Size = new System.Drawing.Size(109, 20);
            this.Slabel2.TabIndex = 1;
            this.Slabel2.Text = "指令总长度";
            // 
            // Slabel1
            // 
            this.Slabel1.AutoSize = true;
            this.Slabel1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Slabel1.Location = new System.Drawing.Point(36, 40);
            this.Slabel1.Name = "Slabel1";
            this.Slabel1.Size = new System.Drawing.Size(89, 20);
            this.Slabel1.TabIndex = 0;
            this.Slabel1.Text = "操作步骤";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.SBox1);
            this.Controls.Add(this.ClientSideInfos);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.localaddrLabel);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.localaddrMaskedTextBox);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
        private System.Windows.Forms.MaskedTextBox LengthText;
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
        private System.Windows.Forms.TextBox DataText;
        private System.Windows.Forms.Label ChecksumLabel;
    }
}

