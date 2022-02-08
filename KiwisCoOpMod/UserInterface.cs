using KiwisCoOpModCore;
using System.Reflection;
using Open.Nat;
using System.Diagnostics;

namespace KiwisCoOpMod
{
    public partial class UserInterface : Form
    {
        public ClientProgram clientProgram;
        private bool started = false;
        private DateTime startTime;
        private Channel channel = new Channel("KCOM", Color.Purple);
        private Random random = new Random();
        private Type gamemodeType = typeof(CoreGamemode);
        private List<AddonInitializer> addons = new List<AddonInitializer>();
        private List<Type> plugins = new List<Type>();
        public UserInterface()
        {
            InitializeComponent();
            // Status bar
            toolStripStatusLabelConnection.Text = "Inactive";
            toolStripStatusLabelConnection.ForeColor = Color.Red;
            toolStripStatusLabelVersion.Text = "Version: v" + ProductVersion.ToString();
            toolStripStatusLabelVconsolePort.Text = "VConsole Port: " + Settings.Default.VconsolePort;
            toolStripStatusLabelVconsoleProtocol.Text = "VConsole Protocol: " + Settings.Default.VconsoleProtocol;
            // Client settings
            checkBoxClientEnabled.Checked = Settings.Default.ClientEnabled;
            clientPlayerCollisionToolStripMenuItem.Checked = Settings.Default.ClientPlayerCollision;
            clientPrintVConsoleToolStripMenuItem.Checked = Settings.Default.ClientPrintVconsole;
            clientHostModeToolStripMenuItem.Checked = Settings.Default.ClientHostMode;
            clientAutomaticallyReconnectToolStripMenuItem.Checked = Settings.Default.ClientAutomaticallyReconnect;
            textBoxClientIpAddress.Text = Settings.Default.ClientIpAddress;
            textBoxClientPassword.Text = Settings.Default.ClientPassword;
            textBoxClientMemo.Text = Settings.Default.ClientMemo;
            textBoxClientUsername.Text = Settings.Default.ClientUsername;
            textBoxClientAuthId.Text = Settings.Default.ClientAuthId;
            numericUpDownClientPort.Value = Settings.Default.ClientPort;
            // Server settings
            checkBoxServerEnabled.Checked = Settings.Default.ServerEnabled;
            textBoxServerIpAddress.Text = Settings.Default.ServerIpAddress;
            textBoxServerPassword.Text = Settings.Default.ServerPassword;
            textBoxServerMemo.Text = Settings.Default.ServerMemo;
            textBoxServerHostUsername.Text = Settings.Default.ServerHostUsername;
            textBoxServerHostAuthId.Text = Settings.Default.ServerHostAuthId;
            textBoxServerMap.Text = Settings.Default.ServerMap;
            numericUpDownServerPort.Value = Settings.Default.ServerPort;
            // Options
            saveOptionsOnExitToolStripMenuItem.Checked = Settings.Default.SaveOptionsOnExit;
            Application.ApplicationExit += new EventHandler(Exit);
            // Base gamemode
            CoreGamemode gamemode = new CoreGamemode();
            ToolStripMenuItem baseItem = new ToolStripMenuItem(gamemode.Name);
            baseItem.ToolTipText = gamemode.Author + "\n" + gamemode.Description;
            baseItem.Click += SelectAddon;
            gamemodeToolStripMenuItem.DropDownItems.Add(baseItem);
            if(gamemode.Name != null)
                addons.Add(new AddonInitializer(gamemode.Name, typeof(CoreGamemode), AddonType.Gamemode));
            // Load extensions
            string? appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (appDir != null)
            {
                // Load libraries
                string libraryFolderPath = Path.Combine(appDir, "libraries");
                Directory.CreateDirectory(libraryFolderPath);
                string[] libraryFiles = Directory.GetFiles(libraryFolderPath, "*.dll", SearchOption.TopDirectoryOnly);
                foreach (string dll in libraryFiles)
                {
                    Assembly.LoadFrom(Path.Combine(appDir, dll));
                }
                toolStripStatusLabelLibraries.Text = "Libraries Loaded: " + libraryFiles.Length;
                // Populate gamemode menu
                bool first = true;
                string gamemodesFolderPath = Path.Combine(appDir, "gamemodes");
                Directory.CreateDirectory(gamemodesFolderPath);
                string[] files = Directory.GetFiles(gamemodesFolderPath, "*.dll", SearchOption.TopDirectoryOnly);
                foreach (string dll in files)
                {
                    Assembly gamemodeAssembly = Assembly.LoadFrom(Path.Combine(appDir, dll));
                    foreach (Type type in gamemodeAssembly.GetTypes())
                    {
                        if (type.GetInterface("Gamemode") != null)
                        {
                            Gamemode? newGamemode = (Gamemode?)Activator.CreateInstance(type);
                            if (newGamemode != null)
                            {
                                ToolStripMenuItem item = new ToolStripMenuItem(newGamemode.Name);
                                item.ToolTipText = newGamemode.Author + "\n" + newGamemode.Description;
                                item.Click += SelectAddon;
                                gamemodeToolStripMenuItem.DropDownItems.Add(item);
                                if (first)
                                {
                                    item.Checked = true;
                                    gamemodeType = type;
                                    first = false;
                                }
                                if (newGamemode.Name != null)
                                    addons.Add(new AddonInitializer(newGamemode.Name, type, AddonType.Gamemode));
                            }
                        }
                    }
                }
                // Populate plugins menu
                string pluginsFolderPath = Path.Combine(appDir, "plugins");
                Directory.CreateDirectory(pluginsFolderPath);
                string[] pluginsFiles = Directory.GetFiles(pluginsFolderPath, "*.dll", SearchOption.TopDirectoryOnly);
                foreach (string dll in pluginsFiles)
                {
                    Assembly pluginAssembly = Assembly.LoadFrom(Path.Combine(appDir, dll));
                    foreach (Type type in pluginAssembly.GetTypes())
                    {
                        if (type.GetInterface("Plugin") != null)
                        {
                            Plugin? newPlugin = (Plugin?)Activator.CreateInstance(type);
                            if (newPlugin != null)
                            {
                                ToolStripMenuItem item = new ToolStripMenuItem(newPlugin.Name);
                                item.ToolTipText = newPlugin.Author + "\n" + newPlugin.Description;
                                item.Click += SelectAddon;
                                pluginsToolStripMenuItem.DropDownItems.Add(item);
                                if (newPlugin.Name != null)
                                    addons.Add(new AddonInitializer(newPlugin.Name, type, AddonType.Plugin));
                                if (newPlugin.Default)
                                {
                                    item.Checked = true;
                                    plugins.Add(type);
                                    PluginHandler.Handle(type, PluginHandleType.UserInterface_Initialized);
                                }
                            }
                        }
                    }
                }
                if (first)
                {
                    baseItem.Checked = true;
                    first = false;
                }
            }
            // Programs
            clientProgram = new ClientProgram(this);
            if(!Settings.Default.ClickedUPnP)
                AskUPnP(true);
        }
        private void Exit(object? sender, EventArgs e)
        {
            Settings.Default.Save();
        }
        public void Start()
        {
            if(!started && (checkBoxServerEnabled.Checked || checkBoxClientEnabled.Checked))
            {
                PluginHandler.Handle(plugins, PluginHandleType.UserInterface_PreStart, this);
                started = true;
                buttonStart.Text = "Stop";
                SetStatus(Color.Green, "Active");
                startTime = DateTime.Now;
                LogToOutput(channel, "Starting at " + startTime.ToString(@"hh\:mm\:ss"));
                if (checkBoxServerEnabled.Checked)
                    ServerProgram.instance.Start(gamemodeType, plugins);
                if (checkBoxClientEnabled.Checked)
                {
                    clientProgram.chances = 0; //Settings.Default.ClientAutomaticallyReconnect ? 2 : 0;
                    clientProgram.Start(plugins);
                }
                PluginHandler.Handle(plugins, PluginHandleType.UserInterface_PostStart, this);
            }
            else
            {
                PluginHandler.Handle(plugins, PluginHandleType.UserInterface_PreClose, this);
                buttonStart.Text = "Start";
                SetStatus(Color.Red, "Inactive");
                clientProgram.chances = 0;
                ServerProgram.instance.Close();
                clientProgram.Close();
                started = false;
                DateTime endTime = DateTime.Now;
                LogToOutput(channel, "Stopped at", endTime.ToString(@"hh\:mm\:ss") + ", active for", (endTime - startTime).ToString(@"hh\:mm\:ss"));
                PluginHandler.Handle(plugins, PluginHandleType.UserInterface_PostClose, this);
                Settings.Default.Save();
                Application.Restart();
            }
            gamemodeToolStripMenuItem.Enabled = !started;
            pluginsToolStripMenuItem.Enabled = !started;
        }

        public void SelectAddon(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                foreach (AddonInitializer initializer in addons)
                {
                    if (initializer.Name == item.Text)
                    {
                        switch (initializer.AddonType)
                        {
                            case AddonType.Gamemode:
                                foreach (ToolStripMenuItem menuItem in gamemodeToolStripMenuItem.DropDownItems)
                                {
                                    menuItem.Checked = false;
                                }
                                gamemodeType = initializer.Type;
                                item.Checked = true;
                                LogToOutput(channel, "Gamemode changed to", item.Text);
                                break;
                            case AddonType.Plugin:
                                item.Checked = !item.Checked;
                                if (item.Checked)
                                {
                                    plugins.Add(initializer.Type);
                                    PluginHandler.Handle(initializer.Type, PluginHandleType.UserInterface_Initialized);
                                    LogToOutput(channel, "Plugin", item.Text, "added");
                                }
                                else
                                {
                                    PluginHandler.Handle(initializer.Type, PluginHandleType.UserInterface_Exit);
                                    plugins.Remove(initializer.Type);
                                    LogToOutput(channel, "Plugin", item.Text, "removed");
                                }
                                break;
                        }
                        break;
                    }
                }
            }
        }

        public void LogToOutput(params object[] text)
        {
            richTextBoxOutput.Select(richTextBoxOutput.TextLength, 0);
            richTextBoxOutput.SelectionColor = Color.Black;
            richTextBoxOutput.AppendText("\n" + string.Join(" ", text));
        }

        public void LogToOutput(Color color, params object[] text)
        {
            richTextBoxOutput.Select(richTextBoxOutput.TextLength, 0);
            richTextBoxOutput.SelectionColor = color;
            richTextBoxOutput.AppendText("\n" + string.Join(" ", text));
        }

        public void LogToOutput(Channel channel, params object[] text)
        {
            /*
            if(richTextBoxOutput.Lines.Length+1 >= 64)
            {
                List<string> lines = richTextBoxOutput.Lines.ToList();
                lines.RemoveAt(0);
                richTextBoxOutput.Lines = lines.ToArray();
            }
            */
            richTextBoxOutput.Select(richTextBoxOutput.TextLength, 0);
            richTextBoxOutput.SelectionColor = channel.GetColor();
            richTextBoxOutput.AppendText("\n" + channel + string.Join(" ", text));
        }

        public void SetStatus(Color color, params object[] text)
        {
            toolStripStatusLabelConnection.ForeColor = color;
            toolStripStatusLabelConnection.Text = string.Join(" ", text);
        }
        public void SetStatus(Channel channel, params object[] text)
        {
            toolStripStatusLabelConnection.ForeColor = channel.GetColor();
            toolStripStatusLabelConnection.Text = channel + string.Join(" ", text);
        }

        private void discordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/3X3teNecWs") { UseShellExecute = true });
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/TeamPopplio/kcom") { UseShellExecute = true });
        }

        private void checkBoxClientEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientEnabled = checkBoxClientEnabled.Checked;
        }

        private void buttonCommandType_Click(object sender, EventArgs e)
        {
            switch(buttonCommandType.Text)
            {
                case "Chat":
                    buttonCommandType.Text = "Command";
                    break;
                case "Command":
                    buttonCommandType.Text = "VConsole";
                    break;
                default:
                    buttonCommandType.Text = "Chat";
                    break;
            }
        }

        private void steamWorkshopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://steamcommunity.com/sharedfiles/filedetails/?id=2739356543") { UseShellExecute = true });
        }

        private void clientPrintVConsoleToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPrintVconsole = clientPrintVConsoleToolStripMenuItem.Checked;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void textBoxInput_Enter(object sender, EventArgs e)
        {
            string prefix = "";
            switch(buttonCommandType.Text)
            {
                case "Command":
                    prefix = "/";
                    break;
                case "VConsole":
                    prefix = "/vc ";
                    break;
            }
            clientProgram.Chat(prefix + textBoxInput.Text);
            textBoxInput.Text = "";
        }

        private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                string prefix = "";
                switch (buttonCommandType.Text)
                {
                    case "Command":
                        prefix = "/";
                        break;
                    case "VConsole":
                        prefix = "/vc ";
                        break;
                }
                LogToOutput("> "+prefix + textBoxInput.Text);
                clientProgram.Chat(prefix + textBoxInput.Text);
                textBoxInput.Text = "";
            }
        }

        private void buttonClientRandomizeAuthId_Click(object sender, EventArgs e)
        {
            string randomizedAuthId = "";
            for (int i = 0; i < 6; i++)
            {
                char character = ' ';
                int randomNumber = random.Next(63)+1;
                switch(randomNumber)
                {
                    case 1: character = '1'; break; case 2: character = '2'; break;
                    case 3: character = '3'; break; case 4: character = '4'; break;
                    case 5: character = '5'; break; case 6: character = '6'; break;
                    case 7: character = '7'; break; case 8: character = '8'; break;
                    case 9: character = '9'; break; case 10: character = '0'; break;

                    case 11: character = 'a'; break; case 12: character = 'b'; break;
                    case 13: character = 'c'; break; case 14: character = 'd'; break;
                    case 15: character = 'e'; break; case 16: character = 'f'; break;
                    case 17: character = 'g'; break; case 18: character = 'h'; break;
                    case 19: character = 'i'; break; case 20: character = 'j'; break;
                    case 21: character = 'k'; break; case 22: character = 'l'; break;
                    case 23: character = 'm'; break; case 24: character = 'n'; break;
                    case 25: character = 'o'; break; case 26: character = 'p'; break;
                    case 27: character = 'q'; break; case 28: character = 'r'; break;
                    case 29: character = 's'; break; case 30: character = 't'; break;
                    case 31: character = 'u'; break; case 32: character = 'v'; break;
                    case 33: character = 'w'; break; case 34: character = 'x'; break;
                    case 35: character = 'y'; break; case 36: character = 'z'; break;

                    case 37: character = 'A'; break; case 38: character = 'B'; break;
                    case 39: character = 'C'; break; case 40: character = 'D'; break;
                    case 41: character = 'E'; break; case 42: character = 'F'; break;
                    case 43: character = 'G'; break; case 44: character = 'H'; break;
                    case 45: character = 'I'; break; case 46: character = 'J'; break;
                    case 47: character = 'K'; break; case 48: character = 'L'; break;
                    case 49: character = 'M'; break; case 50: character = 'N'; break;
                    case 51: character = 'O'; break; case 52: character = 'P'; break;
                    case 53: character = 'Q'; break; case 54: character = 'R'; break;
                    case 55: character = 'S'; break; case 56: character = 'T'; break;
                    case 57: character = 'U'; break; case 58: character = 'V'; break;
                    case 59: character = 'W'; break; case 60: character = 'X'; break;
                    case 61: character = 'Y'; break; case 62: character = 'Z'; break;

                    default: character = (char)randomNumber;break;
                }
                randomizedAuthId += character;
            }
            Settings.Default.ClientAuthId = randomizedAuthId;
            textBoxClientAuthId.Text = randomizedAuthId;
        }

        private void textBoxClientIpAddress_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientIpAddress = textBoxClientIpAddress.Text;
        }

        private void textBoxClientPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPassword = textBoxClientPassword.Text;
        }

        private void textBoxClientUsername_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientUsername = textBoxClientUsername.Text;
        }

        private void textBoxClientAuthId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientAuthId = textBoxClientAuthId.Text;
        }

        private void textBoxClientMemo_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientMemo = textBoxClientMemo.Text;
        }

        private void textBoxServerIpAddress_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerIpAddress = textBoxServerIpAddress.Text;
        }

        private void numericUpDownClientPort_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPort = (int)numericUpDownClientPort.Value;
        }

        private void numericUpDownServerPort_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerPort = (int)numericUpDownServerPort.Value;
        }

        private void textBoxServerPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerPassword = textBoxServerPassword.Text;
        }

        private void textBoxServerMemo_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerMemo = textBoxServerMemo.Text;
        }

        private void textBoxServerMap_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerMap = textBoxServerMap.Text;
        }

        private void textBoxServerHostUsername_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerHostUsername = textBoxServerHostUsername.Text;
        }

        private void textBoxServerHostAuthId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerHostAuthId = textBoxServerHostAuthId.Text;
        }

        private void clientAutomaticallyReconnectToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientAutomaticallyReconnect = clientAutomaticallyReconnectToolStripMenuItem.Checked;
        }

        private void clientPlayerCollisionToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPlayerCollision = clientPlayerCollisionToolStripMenuItem.Checked;
        }

        private void clientHostModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientHostMode = clientHostModeToolStripMenuItem.Checked;
        }

        private void saveOptionsOnExitToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SaveOptionsOnExit = saveOptionsOnExitToolStripMenuItem.Checked;
        }

        private void checkBoxServerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerEnabled = checkBoxServerEnabled.Checked;
        }

        private void serverDisableUserVConsoleInputToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerDisableUserVconsoleInput = serverDisableUserVConsoleInputToolStripMenuItem.Checked;
        }

        private void saveOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            SetStatus(channel.GetColor(), "Options saved");
        }

        private void uPnPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskUPnP(false);
        }

        private async void AskUPnP(bool firstTime)
        {
            Settings.Default.ClickedUPnP = true;
            string prefix = "";
            string suffix = "";
            if (firstTime)
            {
                prefix = "Welcome to Kiwi's Co-Op Mod!\n\nThis software requires an open port in order to run a server. Using the UPnP protocol, this step can be done automatically.\n\n";
                suffix = "You may access this dialog box at any time via 'File' > 'Forward Port via UPnP'.";
            }
            DialogResult res = MessageBox.Show(prefix+ "The Universal Plug and Play protocol, also known as UPnP, is a feature available on most routers designed to allow software to set port forwarding rules seamlessly.\n\n" +
                "If you would like to host a server, click 'Yes' to open the TCP port '"+Settings.Default.ServerPort+"' via UPnP.\n\n" +
                "If you are connecting to a remote server, click 'No' to proceed.\n\n" +
                suffix, "Forward Port via UPnP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch(res)
            {
                case DialogResult.Yes:
                    try
                    {
                        NatDiscoverer discoverer = new NatDiscoverer();
                        NatDevice device = await discoverer.DiscoverDeviceAsync();
                        await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, Settings.Default.ServerPort, Settings.Default.ServerPort, "Kiwi's Co-Op Mod"));
                        MessageBox.Show("Successfully forwarded port '" + Settings.Default.ServerPort + "'!", "Forward Port via UPnP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error has occured while attempting to forward port '" + Settings.Default.ServerPort + "':\n\n"+ex.Message+
                            "\n\nYour router may not support UPnP. If the mapping does not already exist, please forward the port manually.", "Forward Port via UPnP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void buttonServerChangeMap_Click(object sender, EventArgs e)
        {
            ServerProgram.instance.ChangeMap(Settings.Default.ServerMap);
        }
    }
}