namespace KiwisCoOpMod
{
    partial class UserInterface
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uPnPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientPlayerCollisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientPrintVConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientHostModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientAutomaticallyReconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverDisableUserVConsoleInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamemodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOptionsOnExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamWorkshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelOutput = new System.Windows.Forms.Label();
            this.splitConfig = new System.Windows.Forms.SplitContainer();
            this.buttonClientRandomizeAuthId = new System.Windows.Forms.Button();
            this.numericUpDownClientPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxClientMemo = new System.Windows.Forms.TextBox();
            this.labelClientMemo = new System.Windows.Forms.Label();
            this.textBoxClientAuthId = new System.Windows.Forms.TextBox();
            this.labelClientAuthId = new System.Windows.Forms.Label();
            this.textBoxClientUsername = new System.Windows.Forms.TextBox();
            this.textBoxClientPassword = new System.Windows.Forms.TextBox();
            this.labelClientPassword = new System.Windows.Forms.Label();
            this.labelClientUsername = new System.Windows.Forms.Label();
            this.labelClientPort = new System.Windows.Forms.Label();
            this.textBoxClientIpAddress = new System.Windows.Forms.TextBox();
            this.labelClientIpAddress = new System.Windows.Forms.Label();
            this.checkBoxClientEnabled = new System.Windows.Forms.CheckBox();
            this.labelClient = new System.Windows.Forms.Label();
            this.buttonServerChangeMap = new System.Windows.Forms.Button();
            this.numericUpDownServerPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxServerMap = new System.Windows.Forms.TextBox();
            this.labelServerMap = new System.Windows.Forms.Label();
            this.textBoxServerMemo = new System.Windows.Forms.TextBox();
            this.labelServerMemo = new System.Windows.Forms.Label();
            this.textBoxServerHostAuthId = new System.Windows.Forms.TextBox();
            this.labelServerHostAuthId = new System.Windows.Forms.Label();
            this.textBoxServerHostUsername = new System.Windows.Forms.TextBox();
            this.textBoxServerPassword = new System.Windows.Forms.TextBox();
            this.labelServerPassword = new System.Windows.Forms.Label();
            this.labelServerHostUsername = new System.Windows.Forms.Label();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.textBoxServerIpAddress = new System.Windows.Forms.TextBox();
            this.labelServerIpAddress = new System.Windows.Forms.Label();
            this.checkBoxServerEnabled = new System.Windows.Forms.CheckBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVconsolePort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVconsoleProtocol = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLibraries = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCommandType = new System.Windows.Forms.Button();
            this.trelloFAQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConfig)).BeginInit();
            this.splitConfig.Panel1.SuspendLayout();
            this.splitConfig.Panel2.SuspendLayout();
            this.splitConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClientPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.HideSelection = false;
            this.richTextBoxOutput.Location = new System.Drawing.Point(12, 300);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.ReadOnly = true;
            this.richTextBoxOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxOutput.Size = new System.Drawing.Size(776, 96);
            this.richTextBoxOutput.TabIndex = 0;
            this.richTextBoxOutput.Text = resources.GetString("richTextBoxOutput.Text");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uPnPToolStripMenuItem,
            this.saveOptionsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // uPnPToolStripMenuItem
            // 
            this.uPnPToolStripMenuItem.Name = "uPnPToolStripMenuItem";
            this.uPnPToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.uPnPToolStripMenuItem.Text = "Forward Port via UPnP";
            this.uPnPToolStripMenuItem.Click += new System.EventHandler(this.uPnPToolStripMenuItem_Click);
            // 
            // saveOptionsToolStripMenuItem
            // 
            this.saveOptionsToolStripMenuItem.Name = "saveOptionsToolStripMenuItem";
            this.saveOptionsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveOptionsToolStripMenuItem.Text = "Save Options";
            this.saveOptionsToolStripMenuItem.Click += new System.EventHandler(this.saveOptionsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.closeToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedOptionsToolStripMenuItem,
            this.gamemodeToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.saveOptionsOnExitToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // advancedOptionsToolStripMenuItem
            // 
            this.advancedOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientPlayerCollisionToolStripMenuItem,
            this.clientPrintVConsoleToolStripMenuItem,
            this.clientHostModeToolStripMenuItem,
            this.clientAutomaticallyReconnectToolStripMenuItem,
            this.serverDisableUserVConsoleInputToolStripMenuItem});
            this.advancedOptionsToolStripMenuItem.Name = "advancedOptionsToolStripMenuItem";
            this.advancedOptionsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.advancedOptionsToolStripMenuItem.Text = "Advanced Options";
            // 
            // clientPlayerCollisionToolStripMenuItem
            // 
            this.clientPlayerCollisionToolStripMenuItem.CheckOnClick = true;
            this.clientPlayerCollisionToolStripMenuItem.Enabled = false;
            this.clientPlayerCollisionToolStripMenuItem.Name = "clientPlayerCollisionToolStripMenuItem";
            this.clientPlayerCollisionToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientPlayerCollisionToolStripMenuItem.Text = "Client: Player Collision";
            this.clientPlayerCollisionToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clientPlayerCollisionToolStripMenuItem_CheckedChanged);
            // 
            // clientPrintVConsoleToolStripMenuItem
            // 
            this.clientPrintVConsoleToolStripMenuItem.CheckOnClick = true;
            this.clientPrintVConsoleToolStripMenuItem.Name = "clientPrintVConsoleToolStripMenuItem";
            this.clientPrintVConsoleToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientPrintVConsoleToolStripMenuItem.Text = "Client: Print VConsole";
            this.clientPrintVConsoleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clientPrintVConsoleToolStripMenuItem_CheckedChanged);
            // 
            // clientHostModeToolStripMenuItem
            // 
            this.clientHostModeToolStripMenuItem.CheckOnClick = true;
            this.clientHostModeToolStripMenuItem.Enabled = false;
            this.clientHostModeToolStripMenuItem.Name = "clientHostModeToolStripMenuItem";
            this.clientHostModeToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientHostModeToolStripMenuItem.Text = "Client: Host Mode";
            this.clientHostModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clientHostModeToolStripMenuItem_CheckedChanged);
            // 
            // clientAutomaticallyReconnectToolStripMenuItem
            // 
            this.clientAutomaticallyReconnectToolStripMenuItem.CheckOnClick = true;
            this.clientAutomaticallyReconnectToolStripMenuItem.Enabled = false;
            this.clientAutomaticallyReconnectToolStripMenuItem.Name = "clientAutomaticallyReconnectToolStripMenuItem";
            this.clientAutomaticallyReconnectToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientAutomaticallyReconnectToolStripMenuItem.Text = "Client: Automatically Reconnect";
            this.clientAutomaticallyReconnectToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clientAutomaticallyReconnectToolStripMenuItem_CheckedChanged);
            // 
            // serverDisableUserVConsoleInputToolStripMenuItem
            // 
            this.serverDisableUserVConsoleInputToolStripMenuItem.CheckOnClick = true;
            this.serverDisableUserVConsoleInputToolStripMenuItem.Name = "serverDisableUserVConsoleInputToolStripMenuItem";
            this.serverDisableUserVConsoleInputToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.serverDisableUserVConsoleInputToolStripMenuItem.Text = "Server: Disable User VConsole Input";
            this.serverDisableUserVConsoleInputToolStripMenuItem.CheckedChanged += new System.EventHandler(this.serverDisableUserVConsoleInputToolStripMenuItem_CheckedChanged);
            // 
            // gamemodeToolStripMenuItem
            // 
            this.gamemodeToolStripMenuItem.Name = "gamemodeToolStripMenuItem";
            this.gamemodeToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.gamemodeToolStripMenuItem.Text = "Gamemode";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // saveOptionsOnExitToolStripMenuItem
            // 
            this.saveOptionsOnExitToolStripMenuItem.Checked = true;
            this.saveOptionsOnExitToolStripMenuItem.CheckOnClick = true;
            this.saveOptionsOnExitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveOptionsOnExitToolStripMenuItem.Name = "saveOptionsOnExitToolStripMenuItem";
            this.saveOptionsOnExitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveOptionsOnExitToolStripMenuItem.Text = "Save Options on Exit";
            this.saveOptionsOnExitToolStripMenuItem.CheckedChanged += new System.EventHandler(this.saveOptionsOnExitToolStripMenuItem_CheckedChanged);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trelloFAQToolStripMenuItem,
            this.discordToolStripMenuItem,
            this.gitHubToolStripMenuItem,
            this.steamWorkshopToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // discordToolStripMenuItem
            // 
            this.discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            this.discordToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.discordToolStripMenuItem.Text = "Discord";
            this.discordToolStripMenuItem.Click += new System.EventHandler(this.discordToolStripMenuItem_Click);
            // 
            // gitHubToolStripMenuItem
            // 
            this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gitHubToolStripMenuItem.Text = "GitHub";
            this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.gitHubToolStripMenuItem_Click);
            // 
            // steamWorkshopToolStripMenuItem
            // 
            this.steamWorkshopToolStripMenuItem.Name = "steamWorkshopToolStripMenuItem";
            this.steamWorkshopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.steamWorkshopToolStripMenuItem.Text = "Steam Workshop";
            this.steamWorkshopToolStripMenuItem.Click += new System.EventHandler(this.steamWorkshopToolStripMenuItem_Click);
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(12, 282);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(48, 15);
            this.labelOutput.TabIndex = 2;
            this.labelOutput.Text = "Output:";
            // 
            // splitConfig
            // 
            this.splitConfig.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitConfig.IsSplitterFixed = true;
            this.splitConfig.Location = new System.Drawing.Point(12, 27);
            this.splitConfig.Name = "splitConfig";
            // 
            // splitConfig.Panel1
            // 
            this.splitConfig.Panel1.Controls.Add(this.buttonClientRandomizeAuthId);
            this.splitConfig.Panel1.Controls.Add(this.numericUpDownClientPort);
            this.splitConfig.Panel1.Controls.Add(this.textBoxClientMemo);
            this.splitConfig.Panel1.Controls.Add(this.labelClientMemo);
            this.splitConfig.Panel1.Controls.Add(this.textBoxClientAuthId);
            this.splitConfig.Panel1.Controls.Add(this.labelClientAuthId);
            this.splitConfig.Panel1.Controls.Add(this.textBoxClientUsername);
            this.splitConfig.Panel1.Controls.Add(this.textBoxClientPassword);
            this.splitConfig.Panel1.Controls.Add(this.labelClientPassword);
            this.splitConfig.Panel1.Controls.Add(this.labelClientUsername);
            this.splitConfig.Panel1.Controls.Add(this.labelClientPort);
            this.splitConfig.Panel1.Controls.Add(this.textBoxClientIpAddress);
            this.splitConfig.Panel1.Controls.Add(this.labelClientIpAddress);
            this.splitConfig.Panel1.Controls.Add(this.checkBoxClientEnabled);
            this.splitConfig.Panel1.Controls.Add(this.labelClient);
            this.splitConfig.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // splitConfig.Panel2
            // 
            this.splitConfig.Panel2.Controls.Add(this.buttonServerChangeMap);
            this.splitConfig.Panel2.Controls.Add(this.numericUpDownServerPort);
            this.splitConfig.Panel2.Controls.Add(this.textBoxServerMap);
            this.splitConfig.Panel2.Controls.Add(this.labelServerMap);
            this.splitConfig.Panel2.Controls.Add(this.textBoxServerMemo);
            this.splitConfig.Panel2.Controls.Add(this.labelServerMemo);
            this.splitConfig.Panel2.Controls.Add(this.textBoxServerHostAuthId);
            this.splitConfig.Panel2.Controls.Add(this.labelServerHostAuthId);
            this.splitConfig.Panel2.Controls.Add(this.textBoxServerHostUsername);
            this.splitConfig.Panel2.Controls.Add(this.textBoxServerPassword);
            this.splitConfig.Panel2.Controls.Add(this.labelServerPassword);
            this.splitConfig.Panel2.Controls.Add(this.labelServerHostUsername);
            this.splitConfig.Panel2.Controls.Add(this.labelServerPort);
            this.splitConfig.Panel2.Controls.Add(this.textBoxServerIpAddress);
            this.splitConfig.Panel2.Controls.Add(this.labelServerIpAddress);
            this.splitConfig.Panel2.Controls.Add(this.checkBoxServerEnabled);
            this.splitConfig.Panel2.Controls.Add(this.labelServer);
            this.splitConfig.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitConfig.Size = new System.Drawing.Size(776, 252);
            this.splitConfig.SplitterDistance = 388;
            this.splitConfig.TabIndex = 5;
            // 
            // buttonClientRandomizeAuthId
            // 
            this.buttonClientRandomizeAuthId.Location = new System.Drawing.Point(362, 72);
            this.buttonClientRandomizeAuthId.Name = "buttonClientRandomizeAuthId";
            this.buttonClientRandomizeAuthId.Size = new System.Drawing.Size(23, 23);
            this.buttonClientRandomizeAuthId.TabIndex = 20;
            this.buttonClientRandomizeAuthId.Text = "+";
            this.buttonClientRandomizeAuthId.UseVisualStyleBackColor = true;
            this.buttonClientRandomizeAuthId.Click += new System.EventHandler(this.buttonClientRandomizeAuthId_Click);
            // 
            // numericUpDownClientPort
            // 
            this.numericUpDownClientPort.Location = new System.Drawing.Point(206, 43);
            this.numericUpDownClientPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownClientPort.Name = "numericUpDownClientPort";
            this.numericUpDownClientPort.Size = new System.Drawing.Size(50, 23);
            this.numericUpDownClientPort.TabIndex = 19;
            this.numericUpDownClientPort.ValueChanged += new System.EventHandler(this.numericUpDownClientPort_ValueChanged);
            // 
            // textBoxClientMemo
            // 
            this.textBoxClientMemo.Location = new System.Drawing.Point(51, 101);
            this.textBoxClientMemo.Name = "textBoxClientMemo";
            this.textBoxClientMemo.Size = new System.Drawing.Size(335, 23);
            this.textBoxClientMemo.TabIndex = 14;
            this.textBoxClientMemo.TextChanged += new System.EventHandler(this.textBoxClientMemo_TextChanged);
            // 
            // labelClientMemo
            // 
            this.labelClientMemo.AutoSize = true;
            this.labelClientMemo.Location = new System.Drawing.Point(0, 104);
            this.labelClientMemo.Name = "labelClientMemo";
            this.labelClientMemo.Size = new System.Drawing.Size(45, 15);
            this.labelClientMemo.TabIndex = 13;
            this.labelClientMemo.Text = "Memo:";
            // 
            // textBoxClientAuthId
            // 
            this.textBoxClientAuthId.Location = new System.Drawing.Point(303, 72);
            this.textBoxClientAuthId.Name = "textBoxClientAuthId";
            this.textBoxClientAuthId.Size = new System.Drawing.Size(53, 23);
            this.textBoxClientAuthId.TabIndex = 12;
            this.textBoxClientAuthId.TextChanged += new System.EventHandler(this.textBoxClientAuthId_TextChanged);
            // 
            // labelClientAuthId
            // 
            this.labelClientAuthId.AutoSize = true;
            this.labelClientAuthId.Location = new System.Drawing.Point(250, 75);
            this.labelClientAuthId.Name = "labelClientAuthId";
            this.labelClientAuthId.Size = new System.Drawing.Size(47, 15);
            this.labelClientAuthId.TabIndex = 11;
            this.labelClientAuthId.Text = "AuthID:";
            // 
            // textBoxClientUsername
            // 
            this.textBoxClientUsername.Location = new System.Drawing.Point(69, 72);
            this.textBoxClientUsername.Name = "textBoxClientUsername";
            this.textBoxClientUsername.Size = new System.Drawing.Size(175, 23);
            this.textBoxClientUsername.TabIndex = 10;
            this.textBoxClientUsername.TextChanged += new System.EventHandler(this.textBoxClientUsername_TextChanged);
            // 
            // textBoxClientPassword
            // 
            this.textBoxClientPassword.Location = new System.Drawing.Point(328, 43);
            this.textBoxClientPassword.Name = "textBoxClientPassword";
            this.textBoxClientPassword.Size = new System.Drawing.Size(58, 23);
            this.textBoxClientPassword.TabIndex = 9;
            this.textBoxClientPassword.TextChanged += new System.EventHandler(this.textBoxClientPassword_TextChanged);
            // 
            // labelClientPassword
            // 
            this.labelClientPassword.AutoSize = true;
            this.labelClientPassword.Location = new System.Drawing.Point(262, 46);
            this.labelClientPassword.Name = "labelClientPassword";
            this.labelClientPassword.Size = new System.Drawing.Size(60, 15);
            this.labelClientPassword.TabIndex = 8;
            this.labelClientPassword.Text = "Password:";
            // 
            // labelClientUsername
            // 
            this.labelClientUsername.AutoSize = true;
            this.labelClientUsername.Location = new System.Drawing.Point(0, 75);
            this.labelClientUsername.Name = "labelClientUsername";
            this.labelClientUsername.Size = new System.Drawing.Size(63, 15);
            this.labelClientUsername.TabIndex = 7;
            this.labelClientUsername.Text = "Username:";
            // 
            // labelClientPort
            // 
            this.labelClientPort.AutoSize = true;
            this.labelClientPort.Location = new System.Drawing.Point(168, 46);
            this.labelClientPort.Name = "labelClientPort";
            this.labelClientPort.Size = new System.Drawing.Size(32, 15);
            this.labelClientPort.TabIndex = 5;
            this.labelClientPort.Text = "Port:";
            // 
            // textBoxClientIpAddress
            // 
            this.textBoxClientIpAddress.Location = new System.Drawing.Point(71, 43);
            this.textBoxClientIpAddress.Name = "textBoxClientIpAddress";
            this.textBoxClientIpAddress.Size = new System.Drawing.Size(91, 23);
            this.textBoxClientIpAddress.TabIndex = 4;
            this.textBoxClientIpAddress.TextChanged += new System.EventHandler(this.textBoxClientIpAddress_TextChanged);
            // 
            // labelClientIpAddress
            // 
            this.labelClientIpAddress.AutoSize = true;
            this.labelClientIpAddress.Location = new System.Drawing.Point(0, 46);
            this.labelClientIpAddress.Name = "labelClientIpAddress";
            this.labelClientIpAddress.Size = new System.Drawing.Size(65, 15);
            this.labelClientIpAddress.TabIndex = 3;
            this.labelClientIpAddress.Text = "IP Address:";
            // 
            // checkBoxClientEnabled
            // 
            this.checkBoxClientEnabled.AutoSize = true;
            this.checkBoxClientEnabled.Location = new System.Drawing.Point(0, 18);
            this.checkBoxClientEnabled.Name = "checkBoxClientEnabled";
            this.checkBoxClientEnabled.Size = new System.Drawing.Size(68, 19);
            this.checkBoxClientEnabled.TabIndex = 2;
            this.checkBoxClientEnabled.Text = "Enabled";
            this.toolTip.SetToolTip(this.checkBoxClientEnabled, resources.GetString("checkBoxClientEnabled.ToolTip"));
            this.checkBoxClientEnabled.UseVisualStyleBackColor = true;
            this.checkBoxClientEnabled.CheckedChanged += new System.EventHandler(this.checkBoxClientEnabled_CheckedChanged);
            // 
            // labelClient
            // 
            this.labelClient.AutoSize = true;
            this.labelClient.Location = new System.Drawing.Point(0, 0);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(41, 15);
            this.labelClient.TabIndex = 1;
            this.labelClient.Text = "Client:";
            // 
            // buttonServerChangeMap
            // 
            this.buttonServerChangeMap.Location = new System.Drawing.Point(328, 104);
            this.buttonServerChangeMap.Name = "buttonServerChangeMap";
            this.buttonServerChangeMap.Size = new System.Drawing.Size(56, 23);
            this.buttonServerChangeMap.TabIndex = 32;
            this.buttonServerChangeMap.Text = "Change";
            this.buttonServerChangeMap.UseVisualStyleBackColor = true;
            this.buttonServerChangeMap.Click += new System.EventHandler(this.buttonServerChangeMap_Click);
            // 
            // numericUpDownServerPort
            // 
            this.numericUpDownServerPort.Location = new System.Drawing.Point(206, 44);
            this.numericUpDownServerPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownServerPort.Name = "numericUpDownServerPort";
            this.numericUpDownServerPort.Size = new System.Drawing.Size(50, 23);
            this.numericUpDownServerPort.TabIndex = 31;
            this.numericUpDownServerPort.ValueChanged += new System.EventHandler(this.numericUpDownServerPort_ValueChanged);
            // 
            // textBoxServerMap
            // 
            this.textBoxServerMap.Location = new System.Drawing.Point(40, 104);
            this.textBoxServerMap.Name = "textBoxServerMap";
            this.textBoxServerMap.Size = new System.Drawing.Size(282, 23);
            this.textBoxServerMap.TabIndex = 30;
            this.textBoxServerMap.TextChanged += new System.EventHandler(this.textBoxServerMap_TextChanged);
            // 
            // labelServerMap
            // 
            this.labelServerMap.AutoSize = true;
            this.labelServerMap.Location = new System.Drawing.Point(0, 107);
            this.labelServerMap.Name = "labelServerMap";
            this.labelServerMap.Size = new System.Drawing.Size(34, 15);
            this.labelServerMap.TabIndex = 29;
            this.labelServerMap.Text = "Map:";
            // 
            // textBoxServerMemo
            // 
            this.textBoxServerMemo.Location = new System.Drawing.Point(119, 72);
            this.textBoxServerMemo.Name = "textBoxServerMemo";
            this.textBoxServerMemo.Size = new System.Drawing.Size(265, 23);
            this.textBoxServerMemo.TabIndex = 27;
            this.textBoxServerMemo.TextChanged += new System.EventHandler(this.textBoxServerMemo_TextChanged);
            // 
            // labelServerMemo
            // 
            this.labelServerMemo.AutoSize = true;
            this.labelServerMemo.Location = new System.Drawing.Point(0, 75);
            this.labelServerMemo.Name = "labelServerMemo";
            this.labelServerMemo.Size = new System.Drawing.Size(113, 15);
            this.labelServerMemo.TabIndex = 26;
            this.labelServerMemo.Text = "Message of the Day:";
            // 
            // textBoxServerHostAuthId
            // 
            this.textBoxServerHostAuthId.Enabled = false;
            this.textBoxServerHostAuthId.Location = new System.Drawing.Point(328, 133);
            this.textBoxServerHostAuthId.Name = "textBoxServerHostAuthId";
            this.textBoxServerHostAuthId.Size = new System.Drawing.Size(56, 23);
            this.textBoxServerHostAuthId.TabIndex = 25;
            this.textBoxServerHostAuthId.TextChanged += new System.EventHandler(this.textBoxServerHostAuthId_TextChanged);
            // 
            // labelServerHostAuthId
            // 
            this.labelServerHostAuthId.AutoSize = true;
            this.labelServerHostAuthId.Enabled = false;
            this.labelServerHostAuthId.Location = new System.Drawing.Point(247, 136);
            this.labelServerHostAuthId.Name = "labelServerHostAuthId";
            this.labelServerHostAuthId.Size = new System.Drawing.Size(75, 15);
            this.labelServerHostAuthId.TabIndex = 24;
            this.labelServerHostAuthId.Text = "Host AuthID:";
            // 
            // textBoxServerHostUsername
            // 
            this.textBoxServerHostUsername.Enabled = false;
            this.textBoxServerHostUsername.Location = new System.Drawing.Point(97, 133);
            this.textBoxServerHostUsername.Name = "textBoxServerHostUsername";
            this.textBoxServerHostUsername.Size = new System.Drawing.Size(144, 23);
            this.textBoxServerHostUsername.TabIndex = 23;
            this.textBoxServerHostUsername.TextChanged += new System.EventHandler(this.textBoxServerHostUsername_TextChanged);
            // 
            // textBoxServerPassword
            // 
            this.textBoxServerPassword.Location = new System.Drawing.Point(328, 43);
            this.textBoxServerPassword.Name = "textBoxServerPassword";
            this.textBoxServerPassword.Size = new System.Drawing.Size(56, 23);
            this.textBoxServerPassword.TabIndex = 22;
            this.textBoxServerPassword.TextChanged += new System.EventHandler(this.textBoxServerPassword_TextChanged);
            // 
            // labelServerPassword
            // 
            this.labelServerPassword.AutoSize = true;
            this.labelServerPassword.Location = new System.Drawing.Point(262, 46);
            this.labelServerPassword.Name = "labelServerPassword";
            this.labelServerPassword.Size = new System.Drawing.Size(60, 15);
            this.labelServerPassword.TabIndex = 21;
            this.labelServerPassword.Text = "Password:";
            // 
            // labelServerHostUsername
            // 
            this.labelServerHostUsername.AutoSize = true;
            this.labelServerHostUsername.Enabled = false;
            this.labelServerHostUsername.Location = new System.Drawing.Point(0, 136);
            this.labelServerHostUsername.Name = "labelServerHostUsername";
            this.labelServerHostUsername.Size = new System.Drawing.Size(91, 15);
            this.labelServerHostUsername.TabIndex = 20;
            this.labelServerHostUsername.Text = "Host Username:";
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(168, 46);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(32, 15);
            this.labelServerPort.TabIndex = 18;
            this.labelServerPort.Text = "Port:";
            // 
            // textBoxServerIpAddress
            // 
            this.textBoxServerIpAddress.Enabled = false;
            this.textBoxServerIpAddress.Location = new System.Drawing.Point(71, 43);
            this.textBoxServerIpAddress.Name = "textBoxServerIpAddress";
            this.textBoxServerIpAddress.Size = new System.Drawing.Size(91, 23);
            this.textBoxServerIpAddress.TabIndex = 17;
            this.textBoxServerIpAddress.TextChanged += new System.EventHandler(this.textBoxServerIpAddress_TextChanged);
            // 
            // labelServerIpAddress
            // 
            this.labelServerIpAddress.AutoSize = true;
            this.labelServerIpAddress.Location = new System.Drawing.Point(0, 46);
            this.labelServerIpAddress.Name = "labelServerIpAddress";
            this.labelServerIpAddress.Size = new System.Drawing.Size(65, 15);
            this.labelServerIpAddress.TabIndex = 16;
            this.labelServerIpAddress.Text = "IP Address:";
            // 
            // checkBoxServerEnabled
            // 
            this.checkBoxServerEnabled.AutoSize = true;
            this.checkBoxServerEnabled.Location = new System.Drawing.Point(0, 18);
            this.checkBoxServerEnabled.Name = "checkBoxServerEnabled";
            this.checkBoxServerEnabled.Size = new System.Drawing.Size(68, 19);
            this.checkBoxServerEnabled.TabIndex = 15;
            this.checkBoxServerEnabled.Text = "Enabled";
            this.toolTip.SetToolTip(this.checkBoxServerEnabled, resources.GetString("checkBoxServerEnabled.ToolTip"));
            this.checkBoxServerEnabled.UseVisualStyleBackColor = true;
            this.checkBoxServerEnabled.CheckedChanged += new System.EventHandler(this.checkBoxServerEnabled_CheckedChanged);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(0, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(42, 15);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "Server:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConnection,
            this.toolStripStatusLabelVersion,
            this.toolStripStatusLabelVconsolePort,
            this.toolStripStatusLabelVconsoleProtocol,
            this.toolStripStatusLabelLibraries});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabelConnection
            // 
            this.toolStripStatusLabelConnection.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
            this.toolStripStatusLabelConnection.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabelConnection.Text = "Status";
            // 
            // toolStripStatusLabelVersion
            // 
            this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusLabelVersion.Text = "Version: v0.0.0.0";
            // 
            // toolStripStatusLabelVconsolePort
            // 
            this.toolStripStatusLabelVconsolePort.Name = "toolStripStatusLabelVconsolePort";
            this.toolStripStatusLabelVconsolePort.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelVconsolePort.Text = "VConsole Port: 00000";
            // 
            // toolStripStatusLabelVconsoleProtocol
            // 
            this.toolStripStatusLabelVconsoleProtocol.Name = "toolStripStatusLabelVconsoleProtocol";
            this.toolStripStatusLabelVconsoleProtocol.Size = new System.Drawing.Size(129, 17);
            this.toolStripStatusLabelVconsoleProtocol.Text = "VConsole Protocol: 000";
            // 
            // toolStripStatusLabelLibraries
            // 
            this.toolStripStatusLabelLibraries.Name = "toolStripStatusLabelLibraries";
            this.toolStripStatusLabelLibraries.Size = new System.Drawing.Size(105, 17);
            this.toolStripStatusLabelLibraries.Text = "Libraries Loaded: 0";
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(94, 402);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(632, 23);
            this.textBoxInput.TabIndex = 6;
            this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInput_KeyPress);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(732, 402);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(56, 23);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCommandType
            // 
            this.buttonCommandType.Location = new System.Drawing.Point(12, 402);
            this.buttonCommandType.Name = "buttonCommandType";
            this.buttonCommandType.Size = new System.Drawing.Size(76, 23);
            this.buttonCommandType.TabIndex = 8;
            this.buttonCommandType.Text = "Chat";
            this.buttonCommandType.UseVisualStyleBackColor = true;
            this.buttonCommandType.Click += new System.EventHandler(this.buttonCommandType_Click);
            // 
            // trelloFAQToolStripMenuItem
            // 
            this.trelloFAQToolStripMenuItem.Name = "trelloFAQToolStripMenuItem";
            this.trelloFAQToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.trelloFAQToolStripMenuItem.Text = "Trello (F.A.Q.)";
            this.trelloFAQToolStripMenuItem.Click += new System.EventHandler(this.trelloFAQToolStripMenuItem_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCommandType);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitConfig);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.richTextBoxOutput);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "UserInterface";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiwi\'s Co-Op Mod for Half-Life: Alyx";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitConfig.Panel1.ResumeLayout(false);
            this.splitConfig.Panel1.PerformLayout();
            this.splitConfig.Panel2.ResumeLayout(false);
            this.splitConfig.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConfig)).EndInit();
            this.splitConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClientPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox richTextBoxOutput;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Label labelOutput;
        private SplitContainer splitConfig;
        private Label labelClientIpAddress;
        private CheckBox checkBoxClientEnabled;
        private ToolTip toolTip;
        private Label labelClient;
        private Label labelServer;
        private Label labelClientPort;
        private TextBox textBoxClientIpAddress;
        private TextBox textBoxClientMemo;
        private Label labelClientMemo;
        private TextBox textBoxClientAuthId;
        private Label labelClientAuthId;
        private TextBox textBoxClientUsername;
        private TextBox textBoxClientPassword;
        private Label labelClientPassword;
        private Label labelClientUsername;
        private TextBox textBoxServerMemo;
        private Label labelServerMemo;
        private TextBox textBoxServerHostAuthId;
        private Label labelServerHostAuthId;
        private TextBox textBoxServerHostUsername;
        private TextBox textBoxServerPassword;
        private Label labelServerPassword;
        private Label labelServerHostUsername;
        private Label labelServerPort;
        private TextBox textBoxServerIpAddress;
        private Label labelServerIpAddress;
        private CheckBox checkBoxServerEnabled;
        private TextBox textBoxServerMap;
        private Label labelServerMap;
        private StatusStrip statusStrip;
        private TextBox textBoxInput;
        private ToolStripStatusLabel toolStripStatusLabelVersion;
        private ToolStripMenuItem discordToolStripMenuItem;
        private ToolStripMenuItem gitHubToolStripMenuItem;
        private NumericUpDown numericUpDownClientPort;
        private NumericUpDown numericUpDownServerPort;
        private Button buttonStart;
        private ToolStripStatusLabel toolStripStatusLabelVconsolePort;
        private ToolStripStatusLabel toolStripStatusLabelConnection;
        private ToolStripStatusLabel toolStripStatusLabelVconsoleProtocol;
        private Button buttonCommandType;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem advancedOptionsToolStripMenuItem;
        private ToolStripMenuItem clientPrintVConsoleToolStripMenuItem;
        private ToolStripMenuItem clientHostModeToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem steamWorkshopToolStripMenuItem;
        private ToolStripMenuItem clientPlayerCollisionToolStripMenuItem;
        private ToolStripMenuItem clientAutomaticallyReconnectToolStripMenuItem;
        private Button buttonClientRandomizeAuthId;
        private ToolStripMenuItem saveOptionsOnExitToolStripMenuItem;
        private ToolStripMenuItem serverDisableUserVConsoleInputToolStripMenuItem;
        private ToolStripMenuItem saveOptionsToolStripMenuItem;
        private ToolStripMenuItem gamemodeToolStripMenuItem;
        private ToolStripMenuItem pluginsToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelLibraries;
        private ToolStripMenuItem uPnPToolStripMenuItem;
        private Button buttonServerChangeMap;
        private ToolStripMenuItem trelloFAQToolStripMenuItem;
    }
}