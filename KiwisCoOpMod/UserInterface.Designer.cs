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
            this.gamemodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverDisableUserVConsoleInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientPrintVConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveOptionsOnExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamWorkshopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koFiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabPageServer = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelHelpServerPassword = new System.Windows.Forms.Label();
            this.labelHelpServerPort = new System.Windows.Forms.Label();
            this.buttonServerVconsoleSend = new System.Windows.Forms.Button();
            this.labelServerVconsole = new System.Windows.Forms.Label();
            this.textBoxServerVconsole = new System.Windows.Forms.TextBox();
            this.buttonServerChangeMap = new System.Windows.Forms.Button();
            this.numericUpDownServerPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxServerMap = new System.Windows.Forms.TextBox();
            this.textBoxServerPassword = new System.Windows.Forms.TextBox();
            this.labelServerMap = new System.Windows.Forms.Label();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.labelServerPassword = new System.Windows.Forms.Label();
            this.tabPageClient = new System.Windows.Forms.TabPage();
            this.labelHelpUsername = new System.Windows.Forms.Label();
            this.labelHelpClientPassword = new System.Windows.Forms.Label();
            this.labelHelpClientIpAddressPort = new System.Windows.Forms.Label();
            this.numericUpDownClientPort = new System.Windows.Forms.NumericUpDown();
            this.labelClientIpAddress = new System.Windows.Forms.Label();
            this.textBoxClientIpAddress = new System.Windows.Forms.TextBox();
            this.textBoxClientUsername = new System.Windows.Forms.TextBox();
            this.textBoxClientPassword = new System.Windows.Forms.TextBox();
            this.labelClientPort = new System.Windows.Forms.Label();
            this.labelClientUsername = new System.Windows.Forms.Label();
            this.labelClientPassword = new System.Windows.Forms.Label();
            this.tabControlTop = new System.Windows.Forms.TabControl();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControlBottom.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
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
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
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
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gamemodeToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.clientPrintVConsoleToolStripMenuItem,
            this.serverDisableUserVConsoleInputToolStripMenuItem,
            this.saveOptionsOnExitToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.editToolStripMenuItem.Text = "Options";
            // 
            // gamemodeToolStripMenuItem
            // 
            this.gamemodeToolStripMenuItem.Name = "gamemodeToolStripMenuItem";
            this.gamemodeToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.gamemodeToolStripMenuItem.Text = "Gamemode";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // serverDisableUserVConsoleInputToolStripMenuItem
            // 
            this.serverDisableUserVConsoleInputToolStripMenuItem.CheckOnClick = true;
            this.serverDisableUserVConsoleInputToolStripMenuItem.Name = "serverDisableUserVConsoleInputToolStripMenuItem";
            this.serverDisableUserVConsoleInputToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.serverDisableUserVConsoleInputToolStripMenuItem.Text = "Server: Disable User VConsole Input";
            this.serverDisableUserVConsoleInputToolStripMenuItem.Click += new System.EventHandler(this.ServerDisableUserVConsoleInputToolStripMenuItem_CheckedChanged);
            // 
            // clientPrintVConsoleToolStripMenuItem
            // 
            this.clientPrintVConsoleToolStripMenuItem.CheckOnClick = true;
            this.clientPrintVConsoleToolStripMenuItem.Name = "clientPrintVConsoleToolStripMenuItem";
            this.clientPrintVConsoleToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.clientPrintVConsoleToolStripMenuItem.Text = "Client: Print VConsole";
            this.clientPrintVConsoleToolStripMenuItem.Click += new System.EventHandler(this.ClientPrintVConsoleToolStripMenuItem_CheckedChanged);
            // 
            // saveOptionsOnExitToolStripMenuItem
            // 
            this.saveOptionsOnExitToolStripMenuItem.Checked = true;
            this.saveOptionsOnExitToolStripMenuItem.CheckOnClick = true;
            this.saveOptionsOnExitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveOptionsOnExitToolStripMenuItem.Name = "saveOptionsOnExitToolStripMenuItem";
            this.saveOptionsOnExitToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.saveOptionsOnExitToolStripMenuItem.Text = "Save Options on Exit";
            this.saveOptionsOnExitToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveOptionsOnExitToolStripMenuItem_CheckedChanged);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always On Top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discordToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // discordToolStripMenuItem
            // 
            this.discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            this.discordToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.discordToolStripMenuItem.Text = "Discord";
            this.discordToolStripMenuItem.Click += new System.EventHandler(this.DiscordToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gitHubToolStripMenuItem,
            this.steamWorkshopToolStripMenuItem,
            this.koFiToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
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
            // koFiToolStripMenuItem
            // 
            this.koFiToolStripMenuItem.Name = "koFiToolStripMenuItem";
            this.koFiToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.koFiToolStripMenuItem.Text = "Donate via Ko-fi";
            this.koFiToolStripMenuItem.Click += new System.EventHandler(this.koFiToolStripMenuItem_Click);
            // 
            // checkBoxServerEnabled
            // 
            this.checkBoxServerEnabled.AutoSize = true;
            this.checkBoxServerEnabled.Location = new System.Drawing.Point(6, 6);
            this.checkBoxServerEnabled.Name = "checkBoxServerEnabled";
            this.checkBoxServerEnabled.Size = new System.Drawing.Size(513, 19);
            this.checkBoxServerEnabled.TabIndex = 15;
            this.checkBoxServerEnabled.Text = "Enabled - Check if hosting a server. Make sure to provide players with your publi" +
    "c IP address.";
            this.checkBoxServerEnabled.UseVisualStyleBackColor = true;
            this.checkBoxServerEnabled.CheckedChanged += new System.EventHandler(this.CheckBoxServerEnabled_CheckedChanged);
            // 
            // checkBoxClientEnabled
            // 
            this.checkBoxClientEnabled.AutoSize = true;
            this.checkBoxClientEnabled.Location = new System.Drawing.Point(6, 6);
            this.checkBoxClientEnabled.Name = "checkBoxClientEnabled";
            this.checkBoxClientEnabled.Size = new System.Drawing.Size(717, 19);
            this.checkBoxClientEnabled.TabIndex = 2;
            this.checkBoxClientEnabled.Text = "Enabled - Check if connecting to a remote server. Make sure to enter a public IP " +
    "address unless you are connecting to a local server.";
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
            this.statusStrip.Location = new System.Drawing.Point(0, 429);
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
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(91, 17);
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
            this.textBoxInput.Size = new System.Drawing.Size(608, 23);
            this.textBoxInput.TabIndex = 6;
            this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxInput_KeyPress);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(708, 402);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(76, 23);
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
            // tabPageServer
            // 
            this.tabPageServer.Controls.Add(this.label2);
            this.tabPageServer.Controls.Add(this.label1);
            this.tabPageServer.Controls.Add(this.labelHelpServerPassword);
            this.tabPageServer.Controls.Add(this.labelHelpServerPort);
            this.tabPageServer.Controls.Add(this.buttonServerVconsoleSend);
            this.tabPageServer.Controls.Add(this.labelServerVconsole);
            this.tabPageServer.Controls.Add(this.textBoxServerVconsole);
            this.tabPageServer.Controls.Add(this.buttonServerChangeMap);
            this.tabPageServer.Controls.Add(this.checkBoxServerEnabled);
            this.tabPageServer.Controls.Add(this.numericUpDownServerPort);
            this.tabPageServer.Controls.Add(this.textBoxServerMap);
            this.tabPageServer.Controls.Add(this.textBoxServerPassword);
            this.tabPageServer.Controls.Add(this.labelServerMap);
            this.tabPageServer.Controls.Add(this.labelServerPort);
            this.tabPageServer.Controls.Add(this.labelServerPassword);
            this.tabPageServer.Location = new System.Drawing.Point(4, 24);
            this.tabPageServer.Name = "tabPageServer";
            this.tabPageServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServer.Size = new System.Drawing.Size(768, 164);
            this.tabPageServer.TabIndex = 1;
            this.tabPageServer.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 15);
            this.label2.TabIndex = 39;
            this.label2.Text = "Type in a VConsole command to be input by every client.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(345, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "The current map, players will be forced to load this map when connecting.";
            // 
            // labelHelpServerPassword
            // 
            this.labelHelpServerPassword.AutoSize = true;
            this.labelHelpServerPassword.Location = new System.Drawing.Point(274, 63);
            this.labelHelpServerPassword.Name = "labelHelpServerPassword";
            this.labelHelpServerPassword.Size = new System.Drawing.Size(442, 15);
            this.labelHelpServerPassword.TabIndex = 37;
            this.labelHelpServerPassword.Text = "If desired, set this value. Every client should enter this password before connec" +
    "ting.";
            // 
            // labelHelpServerPort
            // 
            this.labelHelpServerPort.AutoSize = true;
            this.labelHelpServerPort.Location = new System.Drawing.Point(275, 34);
            this.labelHelpServerPort.Name = "labelHelpServerPort";
            this.labelHelpServerPort.Size = new System.Drawing.Size(484, 15);
            this.labelHelpServerPort.TabIndex = 36;
            this.labelHelpServerPort.Text = "Set this to an available port number. Make sure to try UPnP port mapping in the F" +
    "ile menu.";
            // 
            // buttonServerVconsoleSend
            // 
            this.buttonServerVconsoleSend.Location = new System.Drawing.Point(275, 118);
            this.buttonServerVconsoleSend.Name = "buttonServerVconsoleSend";
            this.buttonServerVconsoleSend.Size = new System.Drawing.Size(64, 23);
            this.buttonServerVconsoleSend.TabIndex = 35;
            this.buttonServerVconsoleSend.Text = "Send";
            this.buttonServerVconsoleSend.UseVisualStyleBackColor = true;
            this.buttonServerVconsoleSend.Click += new System.EventHandler(this.buttonServerVconsoleSend_Click);
            // 
            // labelServerVconsole
            // 
            this.labelServerVconsole.AutoSize = true;
            this.labelServerVconsole.Location = new System.Drawing.Point(12, 121);
            this.labelServerVconsole.Name = "labelServerVconsole";
            this.labelServerVconsole.Size = new System.Drawing.Size(60, 15);
            this.labelServerVconsole.TabIndex = 34;
            this.labelServerVconsole.Text = "VConsole:";
            // 
            // textBoxServerVconsole
            // 
            this.textBoxServerVconsole.Location = new System.Drawing.Point(78, 118);
            this.textBoxServerVconsole.Name = "textBoxServerVconsole";
            this.textBoxServerVconsole.Size = new System.Drawing.Size(191, 23);
            this.textBoxServerVconsole.TabIndex = 33;
            this.textBoxServerVconsole.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxServerVconsole_KeyPress);
            // 
            // buttonServerChangeMap
            // 
            this.buttonServerChangeMap.Location = new System.Drawing.Point(275, 88);
            this.buttonServerChangeMap.Name = "buttonServerChangeMap";
            this.buttonServerChangeMap.Size = new System.Drawing.Size(64, 23);
            this.buttonServerChangeMap.TabIndex = 32;
            this.buttonServerChangeMap.Text = "Update";
            this.buttonServerChangeMap.UseVisualStyleBackColor = true;
            this.buttonServerChangeMap.Click += new System.EventHandler(this.ButtonServerChangeMap_Click);
            // 
            // numericUpDownServerPort
            // 
            this.numericUpDownServerPort.Location = new System.Drawing.Point(77, 31);
            this.numericUpDownServerPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownServerPort.Name = "numericUpDownServerPort";
            this.numericUpDownServerPort.Size = new System.Drawing.Size(192, 23);
            this.numericUpDownServerPort.TabIndex = 31;
            this.numericUpDownServerPort.ValueChanged += new System.EventHandler(this.NumericUpDownServerPort_ValueChanged);
            // 
            // textBoxServerMap
            // 
            this.textBoxServerMap.Location = new System.Drawing.Point(78, 89);
            this.textBoxServerMap.Name = "textBoxServerMap";
            this.textBoxServerMap.Size = new System.Drawing.Size(191, 23);
            this.textBoxServerMap.TabIndex = 30;
            this.textBoxServerMap.TextChanged += new System.EventHandler(this.TextBoxServerMap_TextChanged);
            this.textBoxServerMap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxServerMap_KeyPress);
            // 
            // textBoxServerPassword
            // 
            this.textBoxServerPassword.Location = new System.Drawing.Point(77, 60);
            this.textBoxServerPassword.Name = "textBoxServerPassword";
            this.textBoxServerPassword.Size = new System.Drawing.Size(192, 23);
            this.textBoxServerPassword.TabIndex = 22;
            this.textBoxServerPassword.TextChanged += new System.EventHandler(this.TextBoxServerPassword_TextChanged);
            // 
            // labelServerMap
            // 
            this.labelServerMap.AutoSize = true;
            this.labelServerMap.Location = new System.Drawing.Point(37, 92);
            this.labelServerMap.Name = "labelServerMap";
            this.labelServerMap.Size = new System.Drawing.Size(34, 15);
            this.labelServerMap.TabIndex = 29;
            this.labelServerMap.Text = "Map:";
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(39, 34);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(32, 15);
            this.labelServerPort.TabIndex = 18;
            this.labelServerPort.Text = "Port:";
            // 
            // labelServerPassword
            // 
            this.labelServerPassword.AutoSize = true;
            this.labelServerPassword.Location = new System.Drawing.Point(11, 63);
            this.labelServerPassword.Name = "labelServerPassword";
            this.labelServerPassword.Size = new System.Drawing.Size(60, 15);
            this.labelServerPassword.TabIndex = 21;
            this.labelServerPassword.Text = "Password:";
            // 
            // tabPageClient
            // 
            this.tabPageClient.Controls.Add(this.labelHelpUsername);
            this.tabPageClient.Controls.Add(this.labelHelpClientPassword);
            this.tabPageClient.Controls.Add(this.labelHelpClientIpAddressPort);
            this.tabPageClient.Controls.Add(this.checkBoxClientEnabled);
            this.tabPageClient.Controls.Add(this.numericUpDownClientPort);
            this.tabPageClient.Controls.Add(this.labelClientIpAddress);
            this.tabPageClient.Controls.Add(this.textBoxClientIpAddress);
            this.tabPageClient.Controls.Add(this.textBoxClientUsername);
            this.tabPageClient.Controls.Add(this.textBoxClientPassword);
            this.tabPageClient.Controls.Add(this.labelClientPort);
            this.tabPageClient.Controls.Add(this.labelClientUsername);
            this.tabPageClient.Controls.Add(this.labelClientPassword);
            this.tabPageClient.Location = new System.Drawing.Point(4, 24);
            this.tabPageClient.Name = "tabPageClient";
            this.tabPageClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClient.Size = new System.Drawing.Size(768, 164);
            this.tabPageClient.TabIndex = 0;
            this.tabPageClient.Text = "Client";
            // 
            // labelHelpUsername
            // 
            this.labelHelpUsername.AutoSize = true;
            this.labelHelpUsername.Location = new System.Drawing.Point(275, 92);
            this.labelHelpUsername.Name = "labelHelpUsername";
            this.labelHelpUsername.Size = new System.Drawing.Size(357, 15);
            this.labelHelpUsername.TabIndex = 23;
            this.labelHelpUsername.Text = "Type a username to identify yourself to both the server and others.\r\n";
            // 
            // labelHelpClientPassword
            // 
            this.labelHelpClientPassword.AutoSize = true;
            this.labelHelpClientPassword.Location = new System.Drawing.Point(275, 63);
            this.labelHelpClientPassword.Name = "labelHelpClientPassword";
            this.labelHelpClientPassword.Size = new System.Drawing.Size(351, 15);
            this.labelHelpClientPassword.TabIndex = 22;
            this.labelHelpClientPassword.Text = "If the remote server has a password set, type it in here if provided.\r\n";
            // 
            // labelHelpClientIpAddressPort
            // 
            this.labelHelpClientIpAddressPort.AutoSize = true;
            this.labelHelpClientIpAddressPort.Location = new System.Drawing.Point(275, 34);
            this.labelHelpClientIpAddressPort.Name = "labelHelpClientIpAddressPort";
            this.labelHelpClientIpAddressPort.Size = new System.Drawing.Size(480, 15);
            this.labelHelpClientIpAddressPort.TabIndex = 21;
            this.labelHelpClientIpAddressPort.Text = "Set this to the IP address of the remote server, or use \"localhost\" if you\'re run" +
    "ning a server.";
            // 
            // numericUpDownClientPort
            // 
            this.numericUpDownClientPort.Location = new System.Drawing.Point(205, 31);
            this.numericUpDownClientPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownClientPort.Name = "numericUpDownClientPort";
            this.numericUpDownClientPort.Size = new System.Drawing.Size(64, 23);
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
            // textBoxClientIpAddress
            // 
            this.textBoxClientIpAddress.Location = new System.Drawing.Point(77, 31);
            this.textBoxClientIpAddress.Name = "textBoxClientIpAddress";
            this.textBoxClientIpAddress.Size = new System.Drawing.Size(96, 23);
            this.textBoxClientIpAddress.TabIndex = 4;
            this.textBoxClientIpAddress.TextChanged += new System.EventHandler(this.TextBoxClientIpAddress_TextChanged);
            // 
            // textBoxClientUsername
            // 
            this.textBoxClientUsername.Location = new System.Drawing.Point(77, 89);
            this.textBoxClientUsername.Name = "textBoxClientUsername";
            this.textBoxClientUsername.Size = new System.Drawing.Size(192, 23);
            this.textBoxClientUsername.TabIndex = 10;
            this.textBoxClientUsername.TextChanged += new System.EventHandler(this.TextBoxClientUsername_TextChanged);
            // 
            // textBoxClientPassword
            // 
            this.textBoxClientPassword.Location = new System.Drawing.Point(77, 60);
            this.textBoxClientPassword.Name = "textBoxClientPassword";
            this.textBoxClientPassword.Size = new System.Drawing.Size(192, 23);
            this.textBoxClientPassword.TabIndex = 9;
            this.textBoxClientPassword.TextChanged += new System.EventHandler(this.TextBoxClientPassword_TextChanged);
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
            this.labelClientUsername.Location = new System.Drawing.Point(8, 92);
            this.labelClientUsername.Name = "labelClientUsername";
            this.labelClientUsername.Size = new System.Drawing.Size(63, 15);
            this.labelClientUsername.TabIndex = 7;
            this.labelClientUsername.Text = "Username:";
            // 
            // labelClientPassword
            // 
            this.labelClientPassword.AutoSize = true;
            this.labelClientPassword.Location = new System.Drawing.Point(11, 63);
            this.labelClientPassword.Name = "labelClientPassword";
            this.labelClientPassword.Size = new System.Drawing.Size(60, 15);
            this.labelClientPassword.TabIndex = 8;
            this.labelClientPassword.Text = "Password:";
            // 
            // tabControlTop
            // 
            this.tabControlTop.Controls.Add(this.tabPageClient);
            this.tabControlTop.Controls.Add(this.tabPageServer);
            this.tabControlTop.Location = new System.Drawing.Point(12, 27);
            this.tabControlTop.Name = "tabControlTop";
            this.tabControlTop.SelectedIndex = 0;
            this.tabControlTop.Size = new System.Drawing.Size(776, 192);
            this.tabControlTop.TabIndex = 9;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 451);
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
        private Button buttonStart;
        private ToolStripStatusLabel toolStripStatusLabelVconsolePort;
        private ToolStripStatusLabel toolStripStatusLabelConnection;
        private ToolStripStatusLabel toolStripStatusLabelVconsoleProtocol;
        private Button buttonCommandType;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem saveOptionsOnExitToolStripMenuItem;
        private ToolStripMenuItem saveOptionsToolStripMenuItem;
        private ToolStripMenuItem gamemodeToolStripMenuItem;
        private ToolStripMenuItem pluginsToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelLibraries;
        private ToolStripMenuItem uPnPToolStripMenuItem;
        private TabControl tabControlBottom;
        private TabPage tabPageOutput;
        private TabPage tabPageServer;
        private Button buttonServerChangeMap;
        private CheckBox checkBoxServerEnabled;
        private NumericUpDown numericUpDownServerPort;
        private TextBox textBoxServerMap;
        private TextBox textBoxServerPassword;
        private Label labelServerMap;
        private Label labelServerPort;
        private Label labelServerPassword;
        private TabPage tabPageClient;
        private CheckBox checkBoxClientEnabled;
        private NumericUpDown numericUpDownClientPort;
        private Label labelClientIpAddress;
        private TextBox textBoxClientIpAddress;
        private TextBox textBoxClientUsername;
        private TextBox textBoxClientPassword;
        private Label labelClientPort;
        private Label labelClientUsername;
        private Label labelClientPassword;
        private TabControl tabControlTop;
        private Label labelHelpClientPassword;
        private Label labelHelpClientIpAddressPort;
        private Label labelHelpUsername;
        private Button buttonServerVconsoleSend;
        private Label labelServerVconsole;
        private TextBox textBoxServerVconsole;
        private Label labelHelpServerPort;
        private Label labelHelpServerPassword;
        private Label label1;
        private Label label2;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem koFiToolStripMenuItem;
        private ToolStripMenuItem steamWorkshopToolStripMenuItem;
        private ToolStripMenuItem serverDisableUserVConsoleInputToolStripMenuItem;
        private ToolStripMenuItem clientPrintVConsoleToolStripMenuItem;
        private ToolStripMenuItem gitHubToolStripMenuItem;
    }
}