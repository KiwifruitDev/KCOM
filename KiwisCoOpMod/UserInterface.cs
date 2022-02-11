/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2022 KiwifruitDev
    All rights reserved.
    This software is licensed under the MIT License.
    -----------------------------------------------------------------------------
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    -----------------------------------------------------------------------------
*/
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
        private readonly Channel channel = new("KCOM", "Kiwi's Co-Op Mod", Color.Purple);
        private readonly Random random = new();
        private Type gamemodeType = typeof(CoreGamemode);
        private readonly List<AddonInitializer> addons = new();
        private readonly List<Type> plugins = new();
        private readonly List<ICustomizationOption> customizations = new();
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
            CoreGamemode gamemode = new();
            ToolStripMenuItem baseItem = new(gamemode.Name)
            {
                ToolTipText = gamemode.Author + "\n" + gamemode.Description
            };
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
                        if (type.GetInterface("IGamemode") != null)
                        {
                            IGamemode? newGamemode = (IGamemode?)Activator.CreateInstance(type);
                            if (newGamemode != null)
                            {
                                if (newGamemode.Name != null)
                                {
                                    ToolStripMenuItem item = new(newGamemode.Name)
                                    {
                                        ToolTipText = newGamemode.Author + "\n" + newGamemode.Description
                                    };
                                    item.Click += SelectAddon;
                                    gamemodeToolStripMenuItem.DropDownItems.Add(item);
                                    addons.Add(new AddonInitializer(newGamemode.Name, type, AddonType.Gamemode));
                                    if (newGamemode.Default)
                                    {
                                        SelectAddon(item, new EventArgs());
                                        if (first)
                                            first = false;
                                    }
                                }
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
                        if (type.GetInterface("IPlugin") != null)
                        {
                            IPlugin? newPlugin = (IPlugin?)Activator.CreateInstance(type);

                            if (newPlugin != null)
                            {
                                if (newPlugin.Name != null)
                                {
                                    ToolStripMenuItem item = new(newPlugin.Name)
                                    {
                                        ToolTipText = newPlugin.Author + "\n" + newPlugin.Description
                                    };
                                    item.Click += SelectAddon;
                                    pluginsToolStripMenuItem.DropDownItems.Add(item);

                                    
                                    if (newPlugin.Default)
                                    {
                                        SelectAddon(item, new EventArgs());
                                    }
                                }
                            }
                        }
                    }
                }
                if (first)
                {
                    SelectAddon(baseItem, new EventArgs());
                    first = false;
                }
            }
            // Programs
            clientProgram = new ClientProgram(this);
            if(!Settings.Default.ClickedUPnP)
                AskUPnP(true);
        }
        public void AddCustomizationOption(ICustomizationOption option)
        {
            ComboBox comboBox = option.Type switch
            {
                CustomizationOptionType.Hat => comboBoxHat,
                CustomizationOptionType.Head => comboBoxHead,
                CustomizationOptionType.Collider => comboBoxCollider,
                CustomizationOptionType.LeftHand => comboBoxLeftHand,
                CustomizationOptionType.RightHand => comboBoxRightHand,
                _ => new ComboBox(),
            };
            if (comboBox.Name != "")
            {
                comboBox.Enabled = true;
                int index = comboBox.Items.Add(option.Name);
                customizations.Add(option);
                if (option.Default)
                {
                    comboBox.SelectedIndex = index;
                    //SelectedIndexChanged(comboBox, new EventArgs());
                }
            }
            else
            {
                comboBox.Dispose();
            }
        }
        public void UseUriScheme(Uri uri)
        {
            string[] args = uri.ToString().Replace("kcom://", "").Split("/");
            if (args.Length > 0)
                UseArgs(args);
        }
        public void UseArgs(string[] args)
        {
            if (args.Length >= 2)
            {
                switch (args[0].ToLower())
                {
                    case "connect":
                        string[] ipPort = args[1].Split(":");
                        Settings.Default.ClientEnabled = true;
                        checkBoxClientEnabled.Checked = true;
                        Settings.Default.ServerEnabled = false;
                        checkBoxServerEnabled.Checked = false;
                        Settings.Default.ClientIpAddress = ipPort[0];
                        textBoxClientIpAddress.Text = ipPort[0];
                        if (ipPort.Length > 1)
                        {
                            Settings.Default.ClientPort = int.Parse(ipPort[1]);
                            numericUpDownClientPort.Value = int.Parse(ipPort[1]);
                        }
                        if(args.Length >= 3)
                        {
                            Settings.Default.ClientPassword = args[2];
                            textBoxClientPassword.Text = args[2];
                        }
                        else
                        {
                            Settings.Default.ClientPassword = "";
                            textBoxClientPassword.Text = "";
                        }
                        break;
                }
            }
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
                    clientProgram.Start(plugins);
                }
                PluginHandler.Handle(plugins, PluginHandleType.UserInterface_PostStart, this);
            }
            else
            {
                PluginHandler.Handle(plugins, PluginHandleType.UserInterface_PreClose, this);
                buttonStart.Text = "Start";
                SetStatus(Color.Red, "Inactive");
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
                                //LogToOutput(channel, "Gamemode changed to", item.Text);
                                break;
                            case AddonType.Plugin:
                                item.Checked = !item.Checked;
                                if (item.Checked)
                                {
                                    plugins.Add(initializer.Type);
                                    PluginHandler.Handle(initializer.Type, PluginHandleType.UserInterface_Initialized);
                                    //LogToOutput(channel, "Plugin", item.Text, "added");
                                }
                                else
                                {
                                    PluginHandler.Handle(initializer.Type, PluginHandleType.UserInterface_Exit);
                                    plugins.Remove(initializer.Type);
                                    //LogToOutput(channel, "Plugin", item.Text, "removed");
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
            string friendlyName = channel.GetFriendlyName();
            if (!tabControlBottom.TabPages.ContainsKey("tabPage" + friendlyName.Replace(" ", "")))
            {
                TabPage newTab = new(friendlyName)
                {
                    Name = "tabPage" + friendlyName.Replace(" ", "")
                };
                RichTextBox textBox = new()
                {
                    TabIndex = 0,
                    HideSelection = false,
                    ReadOnly = true,
                    ScrollBars = RichTextBoxScrollBars.Vertical,
                    Size = new(752, 131),
                    Location = new(6, 6)
                };
                textBox.Select(textBox.TextLength, 0);
                textBox.SelectionColor = channel.GetColor();
                textBox.AppendText(channel + string.Join(" ", text));
                newTab.Controls.Add(textBox);
                tabControlBottom.Controls.Add(newTab);
            }
            else
            {
                RichTextBox textBox = ((RichTextBox)tabControlBottom.TabPages[tabControlBottom.TabPages.IndexOfKey("tabPage" + friendlyName.Replace(" ", ""))].Controls[0]);
                textBox.Select(textBox.TextLength, 0);
                textBox.SelectionColor = channel.GetColor();
                textBox.AppendText("\n" + channel + string.Join(" ", text));
            }
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

        private void DiscordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/3X3teNecWs") { UseShellExecute = true });
        }

        private void GitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/TeamPopplio/kcom") { UseShellExecute = true });
        }

        private void CheckBoxClientEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientEnabled = checkBoxClientEnabled.Checked;
        }

        private void ButtonCommandType_Click(object sender, EventArgs e)
        {
            buttonCommandType.Text = buttonCommandType.Text switch
            {
                "Chat" => "Command",
                "Command" => "VConsole",
                _ => "Chat",
            };
        }

        private void SteamWorkshopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://steamcommunity.com/sharedfiles/filedetails/?id=2739356543") { UseShellExecute = true });
        }

        private void ClientPrintVConsoleToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPrintVconsole = clientPrintVConsoleToolStripMenuItem.Checked;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void TextBoxInput_Enter(object sender, EventArgs e)
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

        private void TextBoxInput_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ButtonClientRandomizeAuthId_Click(object sender, EventArgs e)
        {
            string randomizedAuthId = "";
            for (int i = 0; i < 6; i++)
            {
                int randomNumber = random.Next(63)+1;
                char character = randomNumber switch
                {
                    1 => '1',
                    2 => '2',
                    3 => '3',
                    4 => '4',
                    5 => '5',
                    6 => '6',
                    7 => '7',
                    8 => '8',
                    9 => '9',
                    10 => '0',
                    11 => 'a',
                    12 => 'b',
                    13 => 'c',
                    14 => 'd',
                    15 => 'e',
                    16 => 'f',
                    17 => 'g',
                    18 => 'h',
                    19 => 'i',
                    20 => 'j',
                    21 => 'k',
                    22 => 'l',
                    23 => 'm',
                    24 => 'n',
                    25 => 'o',
                    26 => 'p',
                    27 => 'q',
                    28 => 'r',
                    29 => 's',
                    30 => 't',
                    31 => 'u',
                    32 => 'v',
                    33 => 'w',
                    34 => 'x',
                    35 => 'y',
                    36 => 'z',
                    37 => 'A',
                    38 => 'B',
                    39 => 'C',
                    40 => 'D',
                    41 => 'E',
                    42 => 'F',
                    43 => 'G',
                    44 => 'H',
                    45 => 'I',
                    46 => 'J',
                    47 => 'K',
                    48 => 'L',
                    49 => 'M',
                    50 => 'N',
                    51 => 'O',
                    52 => 'P',
                    53 => 'Q',
                    54 => 'R',
                    55 => 'S',
                    56 => 'T',
                    57 => 'U',
                    58 => 'V',
                    59 => 'W',
                    60 => 'X',
                    61 => 'Y',
                    62 => 'Z',
                    _ => (char)randomNumber,
                };
                randomizedAuthId += character;
            }
            Settings.Default.ClientAuthId = randomizedAuthId;
            textBoxClientAuthId.Text = randomizedAuthId;
        }

        private void TextBoxClientIpAddress_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientIpAddress = textBoxClientIpAddress.Text;
        }

        private void TextBoxClientPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPassword = textBoxClientPassword.Text;
        }

        private void TextBoxClientUsername_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientUsername = textBoxClientUsername.Text;
        }

        private void TextBoxClientAuthId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientAuthId = textBoxClientAuthId.Text;
        }

        private void TextBoxClientMemo_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientMemo = textBoxClientMemo.Text;
        }

        private void TextBoxServerIpAddress_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerIpAddress = textBoxServerIpAddress.Text;
        }

        private void NumericUpDownClientPort_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPort = (int)numericUpDownClientPort.Value;
        }

        private void NumericUpDownServerPort_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerPort = (int)numericUpDownServerPort.Value;
        }

        private void TextBoxServerPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerPassword = textBoxServerPassword.Text;
        }

        private void TextBoxServerMemo_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerMemo = textBoxServerMemo.Text;
        }

        private void TextBoxServerMap_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerMap = textBoxServerMap.Text;
        }

        private void TextBoxServerHostUsername_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerHostUsername = textBoxServerHostUsername.Text;
        }

        private void TextBoxServerHostAuthId_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerHostAuthId = textBoxServerHostAuthId.Text;
        }

        private void ClientAutomaticallyReconnectToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientAutomaticallyReconnect = clientAutomaticallyReconnectToolStripMenuItem.Checked;
        }

        private void ClientPlayerCollisionToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientPlayerCollision = clientPlayerCollisionToolStripMenuItem.Checked;
        }

        private void ClientHostModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ClientHostMode = clientHostModeToolStripMenuItem.Checked;
        }

        private void SaveOptionsOnExitToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SaveOptionsOnExit = saveOptionsOnExitToolStripMenuItem.Checked;
        }

        private void CheckBoxServerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerEnabled = checkBoxServerEnabled.Checked;
        }

        private void ServerDisableUserVConsoleInputToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ServerDisableUserVconsoleInput = serverDisableUserVConsoleInputToolStripMenuItem.Checked;
        }

        private void SaveOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Save();
            SetStatus(channel.GetColor(), "Options saved");
        }

        private void UPnPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskUPnP(false);
        }

        private static async void AskUPnP(bool firstTime)
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
                        NatDiscoverer discoverer = new();
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

        private void ButtonServerChangeMap_Click(object sender, EventArgs e)
        {
            ServerProgram.instance.ChangeMap(Settings.Default.ServerMap);
        }

        private void TrelloFAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://trello.com/b/2xYfYklG/kiwis-co-op-mod-for-half-life-alyx-kcom") { UseShellExecute = true });
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                ICustomizationOption? option = customizations.Find(o => o.Name == comboBox.SelectedItem.ToString());
                if (option != null)
                {
                    toolTip.SetToolTip(comboBox, option.Author + "\n" + option.Description + "\n" + option.ModelName);
                    PictureBox pictureBox;
                    if (option.DisplayImageBase64 != null)
                    {
                        pictureBox = option.Type switch
                        {
                            CustomizationOptionType.Hat => pictureBoxHat,
                            CustomizationOptionType.Head => pictureBoxHead,
                            CustomizationOptionType.Collider => pictureBoxCollider,
                            CustomizationOptionType.LeftHand => pictureBoxLeftHand,
                            CustomizationOptionType.RightHand => pictureBoxRightHand,
                            _ => new PictureBox(),
                        };
                        if (pictureBox.Name != "")
                            pictureBox.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(option.DisplayImageBase64)));
                        else
                            pictureBox.Dispose();
                    }
                    if (option.ModelName != null)
                        clientProgram.ChangeCustomizationOption(option.ModelName);
                }
            }
        }

        private void buttonCharacterUpdate_Click(object sender, EventArgs e)
        {
            customizations.ForEach(c => clientProgram.ChangeCustomizationOption(c.ModelName));
        }
    }
}