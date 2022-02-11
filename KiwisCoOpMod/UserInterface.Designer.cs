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
            this.trelloFAQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamWorkshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxServerEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxClientEnabled = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVconsolePort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVconsoleProtocol = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLibraries = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCommandType = new System.Windows.Forms.Button();
            this.tabControlBottom = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.tabPageCharacter = new System.Windows.Forms.TabPage();
            this.pictureBoxRightHand = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeftHand = new System.Windows.Forms.PictureBox();
            this.pictureBoxCollider = new System.Windows.Forms.PictureBox();
            this.pictureBoxHead = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxRightHand = new System.Windows.Forms.ComboBox();
            this.labelLeftHand = new System.Windows.Forms.Label();
            this.comboBoxLeftHand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCollider = new System.Windows.Forms.ComboBox();
            this.labelHead = new System.Windows.Forms.Label();
            this.comboBoxHead = new System.Windows.Forms.ComboBox();
            this.labelHat = new System.Windows.Forms.Label();
            this.pictureBoxHat = new System.Windows.Forms.PictureBox();
            this.comboBoxHat = new System.Windows.Forms.ComboBox();
            this.tabPageServer = new System.Windows.Forms.TabPage();
            this.buttonServerChangeMap = new System.Windows.Forms.Button();
            this.numericUpDownServerPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxServerMap = new System.Windows.Forms.TextBox();
            this.textBoxServerIpAddress = new System.Windows.Forms.TextBox();
            this.textBoxServerMemo = new System.Windows.Forms.TextBox();
            this.textBoxServerHostAuthId = new System.Windows.Forms.TextBox();
            this.textBoxServerPassword = new System.Windows.Forms.TextBox();
            this.textBoxServerHostUsername = new System.Windows.Forms.TextBox();
            this.labelServerIpAddress = new System.Windows.Forms.Label();
            this.labelServerMap = new System.Windows.Forms.Label();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.labelServerMemo = new System.Windows.Forms.Label();
            this.labelServerHostUsername = new System.Windows.Forms.Label();
            this.labelServerPassword = new System.Windows.Forms.Label();
            this.labelServerHostAuthId = new System.Windows.Forms.Label();
            this.tabPageClient = new System.Windows.Forms.TabPage();
            this.buttonClientRandomizeAuthId = new System.Windows.Forms.Button();
            this.numericUpDownClientPort = new System.Windows.Forms.NumericUpDown();
            this.labelClientIpAddress = new System.Windows.Forms.Label();
            this.textBoxClientMemo = new System.Windows.Forms.TextBox();
            this.textBoxClientIpAddress = new System.Windows.Forms.TextBox();
            this.textBoxClientAuthId = new System.Windows.Forms.TextBox();
            this.textBoxClientUsername = new System.Windows.Forms.TextBox();
            this.textBoxClientPassword = new System.Windows.Forms.TextBox();
            this.labelClientMemo = new System.Windows.Forms.Label();
            this.labelClientPort = new System.Windows.Forms.Label();
            this.labelClientUsername = new System.Windows.Forms.Label();
            this.labelClientAuthId = new System.Windows.Forms.Label();
            this.labelClientPassword = new System.Windows.Forms.Label();
            this.tabControlTop = new System.Windows.Forms.TabControl();
            this.buttonCharacterUpdate = new System.Windows.Forms.Button();
            this.labelCharacterUpdate = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControlBottom.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageCharacter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRightHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftHand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCollider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHat)).BeginInit();
            this.tabPageServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).BeginInit();
            this.tabPageClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClientPort)).BeginInit();
            this.tabControlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.HideSelection = false;
            this.richTextBoxOutput.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.ReadOnly = true;
            this.richTextBoxOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxOutput.Size = new System.Drawing.Size(752, 131);
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
            this.uPnPToolStripMenuItem.Click += new System.EventHandler(this.UPnPToolStripMenuItem_Click);
            // 
            // saveOptionsToolStripMenuItem
            // 
            this.saveOptionsToolStripMenuItem.Name = "saveOptionsToolStripMenuItem";
            this.saveOptionsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveOptionsToolStripMenuItem.Text = "Save Options";
            this.saveOptionsToolStripMenuItem.Click += new System.EventHandler(this.SaveOptionsToolStripMenuItem_Click);
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
            this.clientPlayerCollisionToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ClientPlayerCollisionToolStripMenuItem_CheckedChanged);
            // 
            // clientPrintVConsoleToolStripMenuItem
            // 
            this.clientPrintVConsoleToolStripMenuItem.CheckOnClick = true;
            this.clientPrintVConsoleToolStripMenuItem.Name = "clientPrintVConsoleToolStripMenuItem";
            this.clientPrintVConsoleToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientPrintVConsoleToolStripMenuItem.Text = "Client: Print VConsole";
            this.clientPrintVConsoleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ClientPrintVConsoleToolStripMenuItem_CheckedChanged);
            // 
            // clientHostModeToolStripMenuItem
            // 
            this.clientHostModeToolStripMenuItem.CheckOnClick = true;
            this.clientHostModeToolStripMenuItem.Enabled = false;
            this.clientHostModeToolStripMenuItem.Name = "clientHostModeToolStripMenuItem";
            this.clientHostModeToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientHostModeToolStripMenuItem.Text = "Client: Host Mode";
            this.clientHostModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ClientHostModeToolStripMenuItem_CheckedChanged);
            // 
            // clientAutomaticallyReconnectToolStripMenuItem
            // 
            this.clientAutomaticallyReconnectToolStripMenuItem.CheckOnClick = true;
            this.clientAutomaticallyReconnectToolStripMenuItem.Enabled = false;
            this.clientAutomaticallyReconnectToolStripMenuItem.Name = "clientAutomaticallyReconnectToolStripMenuItem";
            this.clientAutomaticallyReconnectToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientAutomaticallyReconnectToolStripMenuItem.Text = "Client: Automatically Reconnect";
            this.clientAutomaticallyReconnectToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ClientAutomaticallyReconnectToolStripMenuItem_CheckedChanged);
            // 
            // serverDisableUserVConsoleInputToolStripMenuItem
            // 
            this.serverDisableUserVConsoleInputToolStripMenuItem.CheckOnClick = true;
            this.serverDisableUserVConsoleInputToolStripMenuItem.Name = "serverDisableUserVConsoleInputToolStripMenuItem";
            this.serverDisableUserVConsoleInputToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.serverDisableUserVConsoleInputToolStripMenuItem.Text = "Server: Disable User VConsole Input";
            this.serverDisableUserVConsoleInputToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ServerDisableUserVConsoleInputToolStripMenuItem_CheckedChanged);
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
            this.saveOptionsOnExitToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveOptionsOnExitToolStripMenuItem_CheckedChanged);
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
            // trelloFAQToolStripMenuItem
            // 
            this.trelloFAQToolStripMenuItem.Name = "trelloFAQToolStripMenuItem";
            this.trelloFAQToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.trelloFAQToolStripMenuItem.Text = "Trello (F.A.Q.)";
            this.trelloFAQToolStripMenuItem.Click += new System.EventHandler(this.TrelloFAQToolStripMenuItem_Click);
            // 
            // discordToolStripMenuItem
            // 
            this.discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            this.discordToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.discordToolStripMenuItem.Text = "Discord";
            this.discordToolStripMenuItem.Click += new System.EventHandler(this.DiscordToolStripMenuItem_Click);
            // 
            // gitHubToolStripMenuItem
            // 
            this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.gitHubToolStripMenuItem.Text = "GitHub";
            this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.GitHubToolStripMenuItem_Click);
            // 
            // steamWorkshopToolStripMenuItem
            // 
            this.steamWorkshopToolStripMenuItem.Name = "steamWorkshopToolStripMenuItem";
            this.steamWorkshopToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.steamWorkshopToolStripMenuItem.Text = "Steam Workshop";
            this.steamWorkshopToolStripMenuItem.Click += new System.EventHandler(this.SteamWorkshopToolStripMenuItem_Click);
            // 
            // checkBoxServerEnabled
            // 
            this.checkBoxServerEnabled.AutoSize = true;
            this.checkBoxServerEnabled.Location = new System.Drawing.Point(6, 6);
            this.checkBoxServerEnabled.Name = "checkBoxServerEnabled";
            this.checkBoxServerEnabled.Size = new System.Drawing.Size(68, 19);
            this.checkBoxServerEnabled.TabIndex = 15;
            this.checkBoxServerEnabled.Text = "Enabled";
            this.toolTip.SetToolTip(this.checkBoxServerEnabled, resources.GetString("checkBoxServerEnabled.ToolTip"));
            this.checkBoxServerEnabled.UseVisualStyleBackColor = true;
            this.checkBoxServerEnabled.CheckedChanged += new System.EventHandler(this.CheckBoxServerEnabled_CheckedChanged);
            // 
            // checkBoxClientEnabled
            // 
            this.checkBoxClientEnabled.AutoSize = true;
            this.checkBoxClientEnabled.Location = new System.Drawing.Point(6, 6);
            this.checkBoxClientEnabled.Name = "checkBoxClientEnabled";
            this.checkBoxClientEnabled.Size = new System.Drawing.Size(68, 19);
            this.checkBoxClientEnabled.TabIndex = 2;
            this.checkBoxClientEnabled.Text = "Enabled";
            this.toolTip.SetToolTip(this.checkBoxClientEnabled, resources.GetString("checkBoxClientEnabled.ToolTip"));
            this.checkBoxClientEnabled.UseVisualStyleBackColor = true;
            this.checkBoxClientEnabled.CheckedChanged += new System.EventHandler(this.CheckBoxClientEnabled_CheckedChanged);
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
            this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxInput_KeyPress);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(732, 402);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(56, 23);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // buttonCommandType
            // 
            this.buttonCommandType.Location = new System.Drawing.Point(12, 402);
            this.buttonCommandType.Name = "buttonCommandType";
            this.buttonCommandType.Size = new System.Drawing.Size(76, 23);
            this.buttonCommandType.TabIndex = 8;
            this.buttonCommandType.Text = "Chat";
            this.buttonCommandType.UseVisualStyleBackColor = true;
            this.buttonCommandType.Click += new System.EventHandler(this.ButtonCommandType_Click);
            // 
            // tabControlBottom
            // 
            this.tabControlBottom.Controls.Add(this.tabPageOutput);
            this.tabControlBottom.Location = new System.Drawing.Point(12, 225);
            this.tabControlBottom.Name = "tabControlBottom";
            this.tabControlBottom.SelectedIndex = 0;
            this.tabControlBottom.Size = new System.Drawing.Size(772, 172);
            this.tabControlBottom.TabIndex = 10;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.richTextBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 24);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(764, 144);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Global";
            // 
            // tabPageCharacter
            // 
            this.tabPageCharacter.Controls.Add(this.labelCharacterUpdate);
            this.tabPageCharacter.Controls.Add(this.buttonCharacterUpdate);
            this.tabPageCharacter.Controls.Add(this.pictureBoxRightHand);
            this.tabPageCharacter.Controls.Add(this.pictureBoxLeftHand);
            this.tabPageCharacter.Controls.Add(this.pictureBoxCollider);
            this.tabPageCharacter.Controls.Add(this.pictureBoxHead);
            this.tabPageCharacter.Controls.Add(this.label2);
            this.tabPageCharacter.Controls.Add(this.comboBoxRightHand);
            this.tabPageCharacter.Controls.Add(this.labelLeftHand);
            this.tabPageCharacter.Controls.Add(this.comboBoxLeftHand);
            this.tabPageCharacter.Controls.Add(this.label1);
            this.tabPageCharacter.Controls.Add(this.comboBoxCollider);
            this.tabPageCharacter.Controls.Add(this.labelHead);
            this.tabPageCharacter.Controls.Add(this.comboBoxHead);
            this.tabPageCharacter.Controls.Add(this.labelHat);
            this.tabPageCharacter.Controls.Add(this.pictureBoxHat);
            this.tabPageCharacter.Controls.Add(this.comboBoxHat);
            this.tabPageCharacter.Location = new System.Drawing.Point(4, 24);
            this.tabPageCharacter.Name = "tabPageCharacter";
            this.tabPageCharacter.Size = new System.Drawing.Size(768, 164);
            this.tabPageCharacter.TabIndex = 2;
            this.tabPageCharacter.Text = "Character";
            // 
            // pictureBoxRightHand
            // 
            this.pictureBoxRightHand.Location = new System.Drawing.Point(318, 89);
            this.pictureBoxRightHand.Name = "pictureBoxRightHand";
            this.pictureBoxRightHand.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxRightHand.TabIndex = 29;
            this.pictureBoxRightHand.TabStop = false;
            // 
            // pictureBoxLeftHand
            // 
            this.pictureBoxLeftHand.Location = new System.Drawing.Point(402, 88);
            this.pictureBoxLeftHand.Name = "pictureBoxLeftHand";
            this.pictureBoxLeftHand.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxLeftHand.TabIndex = 28;
            this.pictureBoxLeftHand.TabStop = false;
            // 
            // pictureBoxCollider
            // 
            this.pictureBoxCollider.Location = new System.Drawing.Point(348, 61);
            this.pictureBoxCollider.Name = "pictureBoxCollider";
            this.pictureBoxCollider.Size = new System.Drawing.Size(48, 72);
            this.pictureBoxCollider.TabIndex = 27;
            this.pictureBoxCollider.TabStop = false;
            // 
            // pictureBoxHead
            // 
            this.pictureBoxHead.Location = new System.Drawing.Point(360, 31);
            this.pictureBoxHead.Name = "pictureBoxHead";
            this.pictureBoxHead.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxHead.TabIndex = 26;
            this.pictureBoxHead.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "Right Hand:";
            // 
            // comboBoxRightHand
            // 
            this.comboBoxRightHand.Enabled = false;
            this.comboBoxRightHand.FormattingEnabled = true;
            this.comboBoxRightHand.Location = new System.Drawing.Point(184, 89);
            this.comboBoxRightHand.Name = "comboBoxRightHand";
            this.comboBoxRightHand.Size = new System.Drawing.Size(128, 23);
            this.comboBoxRightHand.TabIndex = 24;
            this.comboBoxRightHand.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // labelLeftHand
            // 
            this.labelLeftHand.AutoSize = true;
            this.labelLeftHand.Location = new System.Drawing.Point(432, 92);
            this.labelLeftHand.Name = "labelLeftHand";
            this.labelLeftHand.Size = new System.Drawing.Size(62, 15);
            this.labelLeftHand.TabIndex = 23;
            this.labelLeftHand.Text = "Left Hand:";
            // 
            // comboBoxLeftHand
            // 
            this.comboBoxLeftHand.Enabled = false;
            this.comboBoxLeftHand.FormattingEnabled = true;
            this.comboBoxLeftHand.Location = new System.Drawing.Point(500, 89);
            this.comboBoxLeftHand.Name = "comboBoxLeftHand";
            this.comboBoxLeftHand.Size = new System.Drawing.Size(128, 23);
            this.comboBoxLeftHand.TabIndex = 22;
            this.comboBoxLeftHand.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Collider:";
            // 
            // comboBoxCollider
            // 
            this.comboBoxCollider.Enabled = false;
            this.comboBoxCollider.FormattingEnabled = true;
            this.comboBoxCollider.Location = new System.Drawing.Point(308, 138);
            this.comboBoxCollider.Name = "comboBoxCollider";
            this.comboBoxCollider.Size = new System.Drawing.Size(128, 23);
            this.comboBoxCollider.TabIndex = 20;
            this.comboBoxCollider.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // labelHead
            // 
            this.labelHead.AutoSize = true;
            this.labelHead.Location = new System.Drawing.Point(182, 35);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(38, 15);
            this.labelHead.TabIndex = 19;
            this.labelHead.Text = "Head:";
            // 
            // comboBoxHead
            // 
            this.comboBoxHead.Enabled = false;
            this.comboBoxHead.FormattingEnabled = true;
            this.comboBoxHead.Location = new System.Drawing.Point(226, 32);
            this.comboBoxHead.Name = "comboBoxHead";
            this.comboBoxHead.Size = new System.Drawing.Size(128, 23);
            this.comboBoxHead.TabIndex = 18;
            this.comboBoxHead.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // labelHat
            // 
            this.labelHat.AutoSize = true;
            this.labelHat.Location = new System.Drawing.Point(191, 7);
            this.labelHat.Name = "labelHat";
            this.labelHat.Size = new System.Drawing.Size(29, 15);
            this.labelHat.TabIndex = 17;
            this.labelHat.Text = "Hat:";
            // 
            // pictureBoxHat
            // 
            this.pictureBoxHat.Location = new System.Drawing.Point(360, 3);
            this.pictureBoxHat.Name = "pictureBoxHat";
            this.pictureBoxHat.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxHat.TabIndex = 1;
            this.pictureBoxHat.TabStop = false;
            // 
            // comboBoxHat
            // 
            this.comboBoxHat.Enabled = false;
            this.comboBoxHat.FormattingEnabled = true;
            this.comboBoxHat.Location = new System.Drawing.Point(226, 4);
            this.comboBoxHat.Name = "comboBoxHat";
            this.comboBoxHat.Size = new System.Drawing.Size(128, 23);
            this.comboBoxHat.TabIndex = 0;
            this.comboBoxHat.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged);
            // 
            // tabPageServer
            // 
            this.tabPageServer.Controls.Add(this.buttonServerChangeMap);
            this.tabPageServer.Controls.Add(this.checkBoxServerEnabled);
            this.tabPageServer.Controls.Add(this.numericUpDownServerPort);
            this.tabPageServer.Controls.Add(this.textBoxServerMap);
            this.tabPageServer.Controls.Add(this.textBoxServerIpAddress);
            this.tabPageServer.Controls.Add(this.textBoxServerMemo);
            this.tabPageServer.Controls.Add(this.textBoxServerHostAuthId);
            this.tabPageServer.Controls.Add(this.textBoxServerPassword);
            this.tabPageServer.Controls.Add(this.textBoxServerHostUsername);
            this.tabPageServer.Controls.Add(this.labelServerIpAddress);
            this.tabPageServer.Controls.Add(this.labelServerMap);
            this.tabPageServer.Controls.Add(this.labelServerPort);
            this.tabPageServer.Controls.Add(this.labelServerMemo);
            this.tabPageServer.Controls.Add(this.labelServerHostUsername);
            this.tabPageServer.Controls.Add(this.labelServerPassword);
            this.tabPageServer.Controls.Add(this.labelServerHostAuthId);
            this.tabPageServer.Location = new System.Drawing.Point(4, 24);
            this.tabPageServer.Name = "tabPageServer";
            this.tabPageServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServer.Size = new System.Drawing.Size(768, 164);
            this.tabPageServer.TabIndex = 1;
            this.tabPageServer.Text = "Server";
            // 
            // buttonServerChangeMap
            // 
            this.buttonServerChangeMap.Location = new System.Drawing.Point(334, 61);
            this.buttonServerChangeMap.Name = "buttonServerChangeMap";
            this.buttonServerChangeMap.Size = new System.Drawing.Size(56, 23);
            this.buttonServerChangeMap.TabIndex = 32;
            this.buttonServerChangeMap.Text = "Change";
            this.buttonServerChangeMap.UseVisualStyleBackColor = true;
            this.buttonServerChangeMap.Click += new System.EventHandler(this.ButtonServerChangeMap_Click);
            // 
            // numericUpDownServerPort
            // 
            this.numericUpDownServerPort.Location = new System.Drawing.Point(212, 31);
            this.numericUpDownServerPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownServerPort.Name = "numericUpDownServerPort";
            this.numericUpDownServerPort.Size = new System.Drawing.Size(50, 23);
            this.numericUpDownServerPort.TabIndex = 31;
            this.numericUpDownServerPort.ValueChanged += new System.EventHandler(this.NumericUpDownServerPort_ValueChanged);
            // 
            // textBoxServerMap
            // 
            this.textBoxServerMap.Location = new System.Drawing.Point(46, 60);
            this.textBoxServerMap.Name = "textBoxServerMap";
            this.textBoxServerMap.Size = new System.Drawing.Size(282, 23);
            this.textBoxServerMap.TabIndex = 30;
            this.textBoxServerMap.TextChanged += new System.EventHandler(this.TextBoxServerMap_TextChanged);
            // 
            // textBoxServerIpAddress
            // 
            this.textBoxServerIpAddress.Enabled = false;
            this.textBoxServerIpAddress.Location = new System.Drawing.Point(77, 31);
            this.textBoxServerIpAddress.Name = "textBoxServerIpAddress";
            this.textBoxServerIpAddress.Size = new System.Drawing.Size(91, 23);
            this.textBoxServerIpAddress.TabIndex = 17;
            this.textBoxServerIpAddress.TextChanged += new System.EventHandler(this.TextBoxServerIpAddress_TextChanged);
            // 
            // textBoxServerMemo
            // 
            this.textBoxServerMemo.Location = new System.Drawing.Point(515, 61);
            this.textBoxServerMemo.Name = "textBoxServerMemo";
            this.textBoxServerMemo.Size = new System.Drawing.Size(247, 23);
            this.textBoxServerMemo.TabIndex = 27;
            this.textBoxServerMemo.TextChanged += new System.EventHandler(this.TextBoxServerMemo_TextChanged);
            // 
            // textBoxServerHostAuthId
            // 
            this.textBoxServerHostAuthId.Enabled = false;
            this.textBoxServerHostAuthId.Location = new System.Drawing.Point(706, 30);
            this.textBoxServerHostAuthId.Name = "textBoxServerHostAuthId";
            this.textBoxServerHostAuthId.Size = new System.Drawing.Size(56, 23);
            this.textBoxServerHostAuthId.TabIndex = 25;
            this.textBoxServerHostAuthId.TextChanged += new System.EventHandler(this.TextBoxServerHostAuthId_TextChanged);
            // 
            // textBoxServerPassword
            // 
            this.textBoxServerPassword.Location = new System.Drawing.Point(334, 31);
            this.textBoxServerPassword.Name = "textBoxServerPassword";
            this.textBoxServerPassword.Size = new System.Drawing.Size(56, 23);
            this.textBoxServerPassword.TabIndex = 22;
            this.textBoxServerPassword.TextChanged += new System.EventHandler(this.TextBoxServerPassword_TextChanged);
            // 
            // textBoxServerHostUsername
            // 
            this.textBoxServerHostUsername.Enabled = false;
            this.textBoxServerHostUsername.Location = new System.Drawing.Point(493, 31);
            this.textBoxServerHostUsername.Name = "textBoxServerHostUsername";
            this.textBoxServerHostUsername.Size = new System.Drawing.Size(126, 23);
            this.textBoxServerHostUsername.TabIndex = 23;
            this.textBoxServerHostUsername.TextChanged += new System.EventHandler(this.TextBoxServerHostUsername_TextChanged);
            // 
            // labelServerIpAddress
            // 
            this.labelServerIpAddress.AutoSize = true;
            this.labelServerIpAddress.Location = new System.Drawing.Point(6, 34);
            this.labelServerIpAddress.Name = "labelServerIpAddress";
            this.labelServerIpAddress.Size = new System.Drawing.Size(65, 15);
            this.labelServerIpAddress.TabIndex = 16;
            this.labelServerIpAddress.Text = "IP Address:";
            // 
            // labelServerMap
            // 
            this.labelServerMap.AutoSize = true;
            this.labelServerMap.Location = new System.Drawing.Point(6, 63);
            this.labelServerMap.Name = "labelServerMap";
            this.labelServerMap.Size = new System.Drawing.Size(34, 15);
            this.labelServerMap.TabIndex = 29;
            this.labelServerMap.Text = "Map:";
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(174, 34);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(32, 15);
            this.labelServerPort.TabIndex = 18;
            this.labelServerPort.Text = "Port:";
            // 
            // labelServerMemo
            // 
            this.labelServerMemo.AutoSize = true;
            this.labelServerMemo.Location = new System.Drawing.Point(396, 65);
            this.labelServerMemo.Name = "labelServerMemo";
            this.labelServerMemo.Size = new System.Drawing.Size(113, 15);
            this.labelServerMemo.TabIndex = 26;
            this.labelServerMemo.Text = "Message of the Day:";
            // 
            // labelServerHostUsername
            // 
            this.labelServerHostUsername.AutoSize = true;
            this.labelServerHostUsername.Enabled = false;
            this.labelServerHostUsername.Location = new System.Drawing.Point(396, 34);
            this.labelServerHostUsername.Name = "labelServerHostUsername";
            this.labelServerHostUsername.Size = new System.Drawing.Size(91, 15);
            this.labelServerHostUsername.TabIndex = 20;
            this.labelServerHostUsername.Text = "Host Username:";
            // 
            // labelServerPassword
            // 
            this.labelServerPassword.AutoSize = true;
            this.labelServerPassword.Location = new System.Drawing.Point(268, 34);
            this.labelServerPassword.Name = "labelServerPassword";
            this.labelServerPassword.Size = new System.Drawing.Size(60, 15);
            this.labelServerPassword.TabIndex = 21;
            this.labelServerPassword.Text = "Password:";
            // 
            // labelServerHostAuthId
            // 
            this.labelServerHostAuthId.AutoSize = true;
            this.labelServerHostAuthId.Enabled = false;
            this.labelServerHostAuthId.Location = new System.Drawing.Point(625, 34);
            this.labelServerHostAuthId.Name = "labelServerHostAuthId";
            this.labelServerHostAuthId.Size = new System.Drawing.Size(75, 15);
            this.labelServerHostAuthId.TabIndex = 24;
            this.labelServerHostAuthId.Text = "Host AuthID:";
            // 
            // tabPageClient
            // 
            this.tabPageClient.Controls.Add(this.buttonClientRandomizeAuthId);
            this.tabPageClient.Controls.Add(this.checkBoxClientEnabled);
            this.tabPageClient.Controls.Add(this.numericUpDownClientPort);
            this.tabPageClient.Controls.Add(this.labelClientIpAddress);
            this.tabPageClient.Controls.Add(this.textBoxClientMemo);
            this.tabPageClient.Controls.Add(this.textBoxClientIpAddress);
            this.tabPageClient.Controls.Add(this.textBoxClientAuthId);
            this.tabPageClient.Controls.Add(this.textBoxClientUsername);
            this.tabPageClient.Controls.Add(this.textBoxClientPassword);
            this.tabPageClient.Controls.Add(this.labelClientMemo);
            this.tabPageClient.Controls.Add(this.labelClientPort);
            this.tabPageClient.Controls.Add(this.labelClientUsername);
            this.tabPageClient.Controls.Add(this.labelClientAuthId);
            this.tabPageClient.Controls.Add(this.labelClientPassword);
            this.tabPageClient.Location = new System.Drawing.Point(4, 24);
            this.tabPageClient.Name = "tabPageClient";
            this.tabPageClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClient.Size = new System.Drawing.Size(768, 164);
            this.tabPageClient.TabIndex = 0;
            this.tabPageClient.Text = "Client";
            // 
            // buttonClientRandomizeAuthId
            // 
            this.buttonClientRandomizeAuthId.Location = new System.Drawing.Point(734, 30);
            this.buttonClientRandomizeAuthId.Name = "buttonClientRandomizeAuthId";
            this.buttonClientRandomizeAuthId.Size = new System.Drawing.Size(24, 24);
            this.buttonClientRandomizeAuthId.TabIndex = 20;
            this.buttonClientRandomizeAuthId.Text = "+";
            this.buttonClientRandomizeAuthId.UseVisualStyleBackColor = true;
            this.buttonClientRandomizeAuthId.Click += new System.EventHandler(this.ButtonClientRandomizeAuthId_Click);
            // 
            // numericUpDownClientPort
            // 
            this.numericUpDownClientPort.Location = new System.Drawing.Point(212, 31);
            this.numericUpDownClientPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownClientPort.Name = "numericUpDownClientPort";
            this.numericUpDownClientPort.Size = new System.Drawing.Size(50, 23);
            this.numericUpDownClientPort.TabIndex = 19;
            this.numericUpDownClientPort.ValueChanged += new System.EventHandler(this.NumericUpDownClientPort_ValueChanged);
            // 
            // labelClientIpAddress
            // 
            this.labelClientIpAddress.AutoSize = true;
            this.labelClientIpAddress.Location = new System.Drawing.Point(6, 34);
            this.labelClientIpAddress.Name = "labelClientIpAddress";
            this.labelClientIpAddress.Size = new System.Drawing.Size(65, 15);
            this.labelClientIpAddress.TabIndex = 3;
            this.labelClientIpAddress.Text = "IP Address:";
            // 
            // textBoxClientMemo
            // 
            this.textBoxClientMemo.Location = new System.Drawing.Point(57, 60);
            this.textBoxClientMemo.Name = "textBoxClientMemo";
            this.textBoxClientMemo.Size = new System.Drawing.Size(333, 23);
            this.textBoxClientMemo.TabIndex = 14;
            this.textBoxClientMemo.TextChanged += new System.EventHandler(this.TextBoxClientMemo_TextChanged);
            // 
            // textBoxClientIpAddress
            // 
            this.textBoxClientIpAddress.Location = new System.Drawing.Point(77, 31);
            this.textBoxClientIpAddress.Name = "textBoxClientIpAddress";
            this.textBoxClientIpAddress.Size = new System.Drawing.Size(91, 23);
            this.textBoxClientIpAddress.TabIndex = 4;
            this.textBoxClientIpAddress.TextChanged += new System.EventHandler(this.TextBoxClientIpAddress_TextChanged);
            // 
            // textBoxClientAuthId
            // 
            this.textBoxClientAuthId.Location = new System.Drawing.Point(678, 30);
            this.textBoxClientAuthId.Name = "textBoxClientAuthId";
            this.textBoxClientAuthId.Size = new System.Drawing.Size(50, 23);
            this.textBoxClientAuthId.TabIndex = 12;
            this.textBoxClientAuthId.TextChanged += new System.EventHandler(this.TextBoxClientAuthId_TextChanged);
            // 
            // textBoxClientUsername
            // 
            this.textBoxClientUsername.Location = new System.Drawing.Point(465, 31);
            this.textBoxClientUsername.Name = "textBoxClientUsername";
            this.textBoxClientUsername.Size = new System.Drawing.Size(154, 23);
            this.textBoxClientUsername.TabIndex = 10;
            this.textBoxClientUsername.TextChanged += new System.EventHandler(this.TextBoxClientUsername_TextChanged);
            // 
            // textBoxClientPassword
            // 
            this.textBoxClientPassword.Location = new System.Drawing.Point(334, 31);
            this.textBoxClientPassword.Name = "textBoxClientPassword";
            this.textBoxClientPassword.Size = new System.Drawing.Size(56, 23);
            this.textBoxClientPassword.TabIndex = 9;
            this.textBoxClientPassword.TextChanged += new System.EventHandler(this.TextBoxClientPassword_TextChanged);
            // 
            // labelClientMemo
            // 
            this.labelClientMemo.AutoSize = true;
            this.labelClientMemo.Location = new System.Drawing.Point(6, 63);
            this.labelClientMemo.Name = "labelClientMemo";
            this.labelClientMemo.Size = new System.Drawing.Size(45, 15);
            this.labelClientMemo.TabIndex = 13;
            this.labelClientMemo.Text = "Memo:";
            // 
            // labelClientPort
            // 
            this.labelClientPort.AutoSize = true;
            this.labelClientPort.Location = new System.Drawing.Point(174, 34);
            this.labelClientPort.Name = "labelClientPort";
            this.labelClientPort.Size = new System.Drawing.Size(32, 15);
            this.labelClientPort.TabIndex = 5;
            this.labelClientPort.Text = "Port:";
            // 
            // labelClientUsername
            // 
            this.labelClientUsername.AutoSize = true;
            this.labelClientUsername.Location = new System.Drawing.Point(396, 34);
            this.labelClientUsername.Name = "labelClientUsername";
            this.labelClientUsername.Size = new System.Drawing.Size(63, 15);
            this.labelClientUsername.TabIndex = 7;
            this.labelClientUsername.Text = "Username:";
            // 
            // labelClientAuthId
            // 
            this.labelClientAuthId.AutoSize = true;
            this.labelClientAuthId.Location = new System.Drawing.Point(625, 35);
            this.labelClientAuthId.Name = "labelClientAuthId";
            this.labelClientAuthId.Size = new System.Drawing.Size(47, 15);
            this.labelClientAuthId.TabIndex = 11;
            this.labelClientAuthId.Text = "AuthID:";
            // 
            // labelClientPassword
            // 
            this.labelClientPassword.AutoSize = true;
            this.labelClientPassword.Location = new System.Drawing.Point(268, 34);
            this.labelClientPassword.Name = "labelClientPassword";
            this.labelClientPassword.Size = new System.Drawing.Size(60, 15);
            this.labelClientPassword.TabIndex = 8;
            this.labelClientPassword.Text = "Password:";
            // 
            // tabControlTop
            // 
            this.tabControlTop.Controls.Add(this.tabPageClient);
            this.tabControlTop.Controls.Add(this.tabPageServer);
            this.tabControlTop.Controls.Add(this.tabPageCharacter);
            this.tabControlTop.Location = new System.Drawing.Point(12, 27);
            this.tabControlTop.Name = "tabControlTop";
            this.tabControlTop.SelectedIndex = 0;
            this.tabControlTop.Size = new System.Drawing.Size(776, 192);
            this.tabControlTop.TabIndex = 9;
            // 
            // buttonCharacterUpdate
            // 
            this.buttonCharacterUpdate.Location = new System.Drawing.Point(708, 138);
            this.buttonCharacterUpdate.Name = "buttonCharacterUpdate";
            this.buttonCharacterUpdate.Size = new System.Drawing.Size(56, 23);
            this.buttonCharacterUpdate.TabIndex = 30;
            this.buttonCharacterUpdate.Text = "Update";
            this.buttonCharacterUpdate.UseVisualStyleBackColor = true;
            this.buttonCharacterUpdate.Click += new System.EventHandler(this.buttonCharacterUpdate_Click);
            // 
            // labelCharacterUpdate
            // 
            this.labelCharacterUpdate.AutoSize = true;
            this.labelCharacterUpdate.Location = new System.Drawing.Point(432, 20);
            this.labelCharacterUpdate.Name = "labelCharacterUpdate";
            this.labelCharacterUpdate.Size = new System.Drawing.Size(306, 45);
            this.labelCharacterUpdate.TabIndex = 31;
            this.labelCharacterUpdate.Text = "Clients can only see changes made to character models\r\nonce they have completely " +
    "loaded into the desired map.\r\nClick \"Update\" to re-transmit these changes to eve" +
    "ryone.\r\n";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlBottom);
            this.Controls.Add(this.tabControlTop);
            this.Controls.Add(this.buttonCommandType);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.statusStrip);
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
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControlBottom.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageCharacter.ResumeLayout(false);
            this.tabPageCharacter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRightHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftHand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCollider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHat)).EndInit();
            this.tabPageServer.ResumeLayout(false);
            this.tabPageServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).EndInit();
            this.tabPageClient.ResumeLayout(false);
            this.tabPageClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClientPort)).EndInit();
            this.tabControlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox richTextBoxOutput;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolTip toolTip;
        private StatusStrip statusStrip;
        private TextBox textBoxInput;
        private ToolStripStatusLabel toolStripStatusLabelVersion;
        private ToolStripMenuItem discordToolStripMenuItem;
        private ToolStripMenuItem gitHubToolStripMenuItem;
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
        private ToolStripMenuItem saveOptionsOnExitToolStripMenuItem;
        private ToolStripMenuItem serverDisableUserVConsoleInputToolStripMenuItem;
        private ToolStripMenuItem saveOptionsToolStripMenuItem;
        private ToolStripMenuItem gamemodeToolStripMenuItem;
        private ToolStripMenuItem pluginsToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelLibraries;
        private ToolStripMenuItem uPnPToolStripMenuItem;
        private ToolStripMenuItem trelloFAQToolStripMenuItem;
        private TabControl tabControlBottom;
        private TabPage tabPageOutput;
        private TabPage tabPageCharacter;
        private PictureBox pictureBoxRightHand;
        private PictureBox pictureBoxLeftHand;
        private PictureBox pictureBoxCollider;
        private PictureBox pictureBoxHead;
        private Label label2;
        private ComboBox comboBoxRightHand;
        private Label labelLeftHand;
        private ComboBox comboBoxLeftHand;
        private Label label1;
        private ComboBox comboBoxCollider;
        private Label labelHead;
        private ComboBox comboBoxHead;
        private Label labelHat;
        private PictureBox pictureBoxHat;
        private ComboBox comboBoxHat;
        private TabPage tabPageServer;
        private Button buttonServerChangeMap;
        private CheckBox checkBoxServerEnabled;
        private NumericUpDown numericUpDownServerPort;
        private TextBox textBoxServerMap;
        private TextBox textBoxServerIpAddress;
        private TextBox textBoxServerMemo;
        private TextBox textBoxServerHostAuthId;
        private TextBox textBoxServerPassword;
        private TextBox textBoxServerHostUsername;
        private Label labelServerIpAddress;
        private Label labelServerMap;
        private Label labelServerPort;
        private Label labelServerMemo;
        private Label labelServerHostUsername;
        private Label labelServerPassword;
        private Label labelServerHostAuthId;
        private TabPage tabPageClient;
        private Button buttonClientRandomizeAuthId;
        private CheckBox checkBoxClientEnabled;
        private NumericUpDown numericUpDownClientPort;
        private Label labelClientIpAddress;
        private TextBox textBoxClientMemo;
        private TextBox textBoxClientIpAddress;
        private TextBox textBoxClientAuthId;
        private TextBox textBoxClientUsername;
        private TextBox textBoxClientPassword;
        private Label labelClientMemo;
        private Label labelClientPort;
        private Label labelClientUsername;
        private Label labelClientAuthId;
        private Label labelClientPassword;
        private TabControl tabControlTop;
        private Label labelCharacterUpdate;
        private Button buttonCharacterUpdate;
    }
}