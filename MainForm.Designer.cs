namespace GranadoEspadaHeadless
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_EventLog = new System.Windows.Forms.TabPage();
            this.textBox_LogArea = new System.Windows.Forms.TextBox();
            this.contextMenuStrip_LogArea = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_RawView = new System.Windows.Forms.TabPage();
            this.listView_RawView = new System.Windows.Forms.ListView();
            this.columnHeader_Direction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Opcode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Data = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_RawPackets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_Settings = new System.Windows.Forms.TabPage();
            this.checkBox_AutoSendMsgs = new System.Windows.Forms.CheckBox();
            this.checkBox_SendDBNotices = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_Port = new System.Windows.Forms.NumericUpDown();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.checkBox_AutoRelog = new System.Windows.Forms.CheckBox();
            this.textBox_TeamID = new System.Windows.Forms.TextBox();
            this.textBox_SteamToken = new System.Windows.Forms.TextBox();
            this.textBox_PIN = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.textBox_TeamHash = new System.Windows.Forms.TextBox();
            this.textBox_Pass = new System.Windows.Forms.TextBox();
            this.textBox_AcctName = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textBox_SendPacket = new System.Windows.Forms.TextBox();
            this.button_SendPacket = new System.Windows.Forms.Button();
            this.checkBox_SendAfterLogin = new System.Windows.Forms.CheckBox();
            this.textBox_PacketAfterLogin = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_EventLog.SuspendLayout();
            this.contextMenuStrip_LogArea.SuspendLayout();
            this.tabPage_RawView.SuspendLayout();
            this.contextMenuStrip_RawPackets.SuspendLayout();
            this.tabPage_Settings.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_EventLog);
            this.tabControl1.Controls.Add(this.tabPage_RawView);
            this.tabControl1.Controls.Add(this.tabPage_Settings);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(373, 353);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_EventLog
            // 
            this.tabPage_EventLog.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_EventLog.Controls.Add(this.textBox_LogArea);
            this.tabPage_EventLog.Location = new System.Drawing.Point(4, 22);
            this.tabPage_EventLog.Name = "tabPage_EventLog";
            this.tabPage_EventLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_EventLog.Size = new System.Drawing.Size(365, 327);
            this.tabPage_EventLog.TabIndex = 0;
            this.tabPage_EventLog.Text = "Event Log";
            // 
            // textBox_LogArea
            // 
            this.textBox_LogArea.ContextMenuStrip = this.contextMenuStrip_LogArea;
            this.textBox_LogArea.ForeColor = System.Drawing.Color.Gray;
            this.textBox_LogArea.Location = new System.Drawing.Point(5, 6);
            this.textBox_LogArea.Multiline = true;
            this.textBox_LogArea.Name = "textBox_LogArea";
            this.textBox_LogArea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_LogArea.Size = new System.Drawing.Size(356, 315);
            this.textBox_LogArea.TabIndex = 5;
            // 
            // contextMenuStrip_LogArea
            // 
            this.contextMenuStrip_LogArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem});
            this.contextMenuStrip_LogArea.Name = "contextMenuStrip_LogArea";
            this.contextMenuStrip_LogArea.Size = new System.Drawing.Size(125, 26);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // tabPage_RawView
            // 
            this.tabPage_RawView.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_RawView.Controls.Add(this.listView_RawView);
            this.tabPage_RawView.Location = new System.Drawing.Point(4, 22);
            this.tabPage_RawView.Name = "tabPage_RawView";
            this.tabPage_RawView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_RawView.Size = new System.Drawing.Size(365, 327);
            this.tabPage_RawView.TabIndex = 1;
            this.tabPage_RawView.Text = "Raw View";
            // 
            // listView_RawView
            // 
            this.listView_RawView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Direction,
            this.columnHeader_Length,
            this.columnHeader_Opcode,
            this.columnHeader_Data});
            this.listView_RawView.ContextMenuStrip = this.contextMenuStrip_RawPackets;
            this.listView_RawView.FullRowSelect = true;
            this.listView_RawView.GridLines = true;
            this.listView_RawView.HideSelection = false;
            this.listView_RawView.Location = new System.Drawing.Point(6, 6);
            this.listView_RawView.Name = "listView_RawView";
            this.listView_RawView.Size = new System.Drawing.Size(353, 315);
            this.listView_RawView.TabIndex = 0;
            this.listView_RawView.UseCompatibleStateImageBehavior = false;
            this.listView_RawView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_Direction
            // 
            this.columnHeader_Direction.Text = "In/Out";
            this.columnHeader_Direction.Width = 47;
            // 
            // columnHeader_Length
            // 
            this.columnHeader_Length.Text = "Length";
            this.columnHeader_Length.Width = 48;
            // 
            // columnHeader_Opcode
            // 
            this.columnHeader_Opcode.Text = "Opcode";
            this.columnHeader_Opcode.Width = 55;
            // 
            // columnHeader_Data
            // 
            this.columnHeader_Data.Text = "Data";
            this.columnHeader_Data.Width = 415;
            // 
            // contextMenuStrip_RawPackets
            // 
            this.contextMenuStrip_RawPackets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.sortToolStripMenuItem});
            this.contextMenuStrip_RawPackets.Name = "contextMenuStrip_RawPackets";
            this.contextMenuStrip_RawPackets.Size = new System.Drawing.Size(103, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // tabPage_Settings
            // 
            this.tabPage_Settings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_Settings.Controls.Add(this.textBox_PacketAfterLogin);
            this.tabPage_Settings.Controls.Add(this.checkBox_SendAfterLogin);
            this.tabPage_Settings.Controls.Add(this.checkBox_AutoSendMsgs);
            this.tabPage_Settings.Controls.Add(this.checkBox_SendDBNotices);
            this.tabPage_Settings.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Settings.Name = "tabPage_Settings";
            this.tabPage_Settings.Size = new System.Drawing.Size(365, 327);
            this.tabPage_Settings.TabIndex = 2;
            this.tabPage_Settings.Text = "Settings";
            // 
            // checkBox_AutoSendMsgs
            // 
            this.checkBox_AutoSendMsgs.AutoSize = true;
            this.checkBox_AutoSendMsgs.Location = new System.Drawing.Point(8, 34);
            this.checkBox_AutoSendMsgs.Name = "checkBox_AutoSendMsgs";
            this.checkBox_AutoSendMsgs.Size = new System.Drawing.Size(200, 17);
            this.checkBox_AutoSendMsgs.TabIndex = 1;
            this.checkBox_AutoSendMsgs.Text = "Auto-Send Player Messages from DB";
            this.checkBox_AutoSendMsgs.UseVisualStyleBackColor = true;
            this.checkBox_AutoSendMsgs.CheckedChanged += new System.EventHandler(this.checkBox_AutoSendMsgs_CheckedChanged);
            // 
            // checkBox_SendDBNotices
            // 
            this.checkBox_SendDBNotices.AutoSize = true;
            this.checkBox_SendDBNotices.Location = new System.Drawing.Point(8, 11);
            this.checkBox_SendDBNotices.Name = "checkBox_SendDBNotices";
            this.checkBox_SendDBNotices.Size = new System.Drawing.Size(131, 17);
            this.checkBox_SendDBNotices.TabIndex = 0;
            this.checkBox_SendDBNotices.Text = "Send Notices from DB";
            this.checkBox_SendDBNotices.UseVisualStyleBackColor = true;
            this.checkBox_SendDBNotices.CheckedChanged += new System.EventHandler(this.checkBox_SendDBNotices_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStripMainForm";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creditsToolStripMenuItem,
            this.donateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown_Port);
            this.groupBox1.Controls.Add(this.textBox_IP);
            this.groupBox1.Controls.Add(this.checkBox_AutoRelog);
            this.groupBox1.Controls.Add(this.textBox_TeamID);
            this.groupBox1.Controls.Add(this.textBox_SteamToken);
            this.groupBox1.Controls.Add(this.textBox_PIN);
            this.groupBox1.Controls.Add(this.buttonDisconnect);
            this.groupBox1.Controls.Add(this.textBox_TeamHash);
            this.groupBox1.Controls.Add(this.textBox_Pass);
            this.groupBox1.Controls.Add(this.textBox_AcctName);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(391, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 327);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // numericUpDown_Port
            // 
            this.numericUpDown_Port.Location = new System.Drawing.Point(5, 195);
            this.numericUpDown_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Port.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Port.Name = "numericUpDown_Port";
            this.numericUpDown_Port.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown_Port.TabIndex = 13;
            this.numericUpDown_Port.Value = new decimal(new int[] {
            7001,
            0,
            0,
            0});
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(5, 169);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(78, 20);
            this.textBox_IP.TabIndex = 12;
            this.textBox_IP.Text = "Enter IP...";
            // 
            // checkBox_AutoRelog
            // 
            this.checkBox_AutoRelog.AutoSize = true;
            this.checkBox_AutoRelog.Location = new System.Drawing.Point(5, 304);
            this.checkBox_AutoRelog.Name = "checkBox_AutoRelog";
            this.checkBox_AutoRelog.Size = new System.Drawing.Size(82, 17);
            this.checkBox_AutoRelog.TabIndex = 11;
            this.checkBox_AutoRelog.Text = "Re-connect";
            this.checkBox_AutoRelog.UseVisualStyleBackColor = true;
            this.checkBox_AutoRelog.CheckedChanged += new System.EventHandler(this.checkBox_AutoRelog_CheckedChanged);
            // 
            // textBox_TeamID
            // 
            this.textBox_TeamID.Location = new System.Drawing.Point(5, 143);
            this.textBox_TeamID.Name = "textBox_TeamID";
            this.textBox_TeamID.Size = new System.Drawing.Size(78, 20);
            this.textBox_TeamID.TabIndex = 10;
            this.textBox_TeamID.Text = "321819";
            // 
            // textBox_SteamToken
            // 
            this.textBox_SteamToken.Location = new System.Drawing.Point(5, 118);
            this.textBox_SteamToken.Name = "textBox_SteamToken";
            this.textBox_SteamToken.Size = new System.Drawing.Size(78, 20);
            this.textBox_SteamToken.TabIndex = 9;
            this.textBox_SteamToken.Text = "STEAM TOKEN";
            // 
            // textBox_PIN
            // 
            this.textBox_PIN.Location = new System.Drawing.Point(5, 94);
            this.textBox_PIN.Name = "textBox_PIN";
            this.textBox_PIN.Size = new System.Drawing.Size(78, 20);
            this.textBox_PIN.TabIndex = 8;
            this.textBox_PIN.Text = "Account PIN";
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(7, 275);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(77, 23);
            this.buttonDisconnect.TabIndex = 7;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // textBox_TeamHash
            // 
            this.textBox_TeamHash.Location = new System.Drawing.Point(7, 71);
            this.textBox_TeamHash.Name = "textBox_TeamHash";
            this.textBox_TeamHash.Size = new System.Drawing.Size(76, 20);
            this.textBox_TeamHash.TabIndex = 3;
            this.textBox_TeamHash.Text = "Team HASH";
            // 
            // textBox_Pass
            // 
            this.textBox_Pass.Location = new System.Drawing.Point(7, 45);
            this.textBox_Pass.Name = "textBox_Pass";
            this.textBox_Pass.Size = new System.Drawing.Size(76, 20);
            this.textBox_Pass.TabIndex = 1;
            this.textBox_Pass.Text = "password";
            // 
            // textBox_AcctName
            // 
            this.textBox_AcctName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox_AcctName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBox_AcctName.Location = new System.Drawing.Point(7, 19);
            this.textBox_AcctName.Name = "textBox_AcctName";
            this.textBox_AcctName.Size = new System.Drawing.Size(76, 20);
            this.textBox_AcctName.TabIndex = 0;
            this.textBox_AcctName.Text = "Name";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(7, 245);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(78, 24);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textBox_SendPacket
            // 
            this.textBox_SendPacket.Location = new System.Drawing.Point(12, 386);
            this.textBox_SendPacket.Name = "textBox_SendPacket";
            this.textBox_SendPacket.Size = new System.Drawing.Size(397, 20);
            this.textBox_SendPacket.TabIndex = 7;
            // 
            // button_SendPacket
            // 
            this.button_SendPacket.Location = new System.Drawing.Point(421, 383);
            this.button_SendPacket.Name = "button_SendPacket";
            this.button_SendPacket.Size = new System.Drawing.Size(63, 23);
            this.button_SendPacket.TabIndex = 6;
            this.button_SendPacket.Text = "Send";
            this.button_SendPacket.UseVisualStyleBackColor = true;
            this.button_SendPacket.Click += new System.EventHandler(this.button_SendPacket_Click);
            // 
            // checkBox_SendAfterLogin
            // 
            this.checkBox_SendAfterLogin.AutoSize = true;
            this.checkBox_SendAfterLogin.Location = new System.Drawing.Point(8, 57);
            this.checkBox_SendAfterLogin.Name = "checkBox_SendAfterLogin";
            this.checkBox_SendAfterLogin.Size = new System.Drawing.Size(142, 17);
            this.checkBox_SendAfterLogin.TabIndex = 2;
            this.checkBox_SendAfterLogin.Text = "Send Packet After Login";
            this.checkBox_SendAfterLogin.UseVisualStyleBackColor = true;
            this.checkBox_SendAfterLogin.CheckedChanged += new System.EventHandler(this.checkBox_SendAfterLogin_CheckedChanged);
            // 
            // textBox_PacketAfterLogin
            // 
            this.textBox_PacketAfterLogin.Location = new System.Drawing.Point(5, 80);
            this.textBox_PacketAfterLogin.Name = "textBox_PacketAfterLogin";
            this.textBox_PacketAfterLogin.Size = new System.Drawing.Size(354, 20);
            this.textBox_PacketAfterLogin.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 418);
            this.Controls.Add(this.button_SendPacket);
            this.Controls.Add(this.textBox_SendPacket);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Grando Espada NoClient";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_EventLog.ResumeLayout(false);
            this.tabPage_EventLog.PerformLayout();
            this.contextMenuStrip_LogArea.ResumeLayout(false);
            this.tabPage_RawView.ResumeLayout(false);
            this.contextMenuStrip_RawPackets.ResumeLayout(false);
            this.tabPage_Settings.ResumeLayout(false);
            this.tabPage_Settings.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_EventLog;
        private System.Windows.Forms.TabPage tabPage_RawView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_LogArea;
        private System.Windows.Forms.ListView listView_RawView;
        private System.Windows.Forms.ColumnHeader columnHeader_Direction;
        private System.Windows.Forms.ColumnHeader columnHeader_Length;
        private System.Windows.Forms.ColumnHeader columnHeader_Opcode;
        private System.Windows.Forms.ColumnHeader columnHeader_Data;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_RawPackets;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.TextBox textBox_TeamHash;
        private System.Windows.Forms.TextBox textBox_Pass;
        private System.Windows.Forms.TextBox textBox_AcctName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textBox_SendPacket;
        private System.Windows.Forms.Button button_SendPacket;
        private System.Windows.Forms.TextBox textBox_PIN;
        private System.Windows.Forms.TextBox textBox_SteamToken;
        private System.Windows.Forms.TextBox textBox_TeamID;
        private System.Windows.Forms.TabPage tabPage_Settings;
        private System.Windows.Forms.CheckBox checkBox_SendDBNotices;
        private System.Windows.Forms.CheckBox checkBox_AutoSendMsgs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_LogArea;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_AutoRelog;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.NumericUpDown numericUpDown_Port;
        private System.Windows.Forms.CheckBox checkBox_SendAfterLogin;
        private System.Windows.Forms.TextBox textBox_PacketAfterLogin;
    }
}

