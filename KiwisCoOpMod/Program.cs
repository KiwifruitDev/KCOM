using KiwisCoOpModCore;
using Microsoft.Win32;

namespace KiwisCoOpMod
{
    internal static class Program
    {
        const string UriScheme = "kcom";
        const string FriendlyName = "Kiwi's Co-Op Mod";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            UserInterface userInterface = new UserInterface();
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\" + UriScheme);
            RegistryKey defaultIcon = key.CreateSubKey("DefaultIcon");
            RegistryKey commandKey = key.CreateSubKey(@"shell\open\command");
            string applicationLocation = Application.ExecutablePath;

            key.SetValue("", "URL:" + FriendlyName);
            key.SetValue("URL Protocol", "");
            commandKey.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
            defaultIcon.SetValue("", applicationLocation + ",1");

            if (args.Length > 0)
            {
                if (Uri.TryCreate(args[0], UriKind.Absolute, out var uri) && string.Equals(uri.Scheme, UriScheme, StringComparison.OrdinalIgnoreCase))
                {
                    userInterface.LogToOutput("Received the following URI scheme:", uri);
                    userInterface.UseUriScheme(uri);
                }
                else
                {
                    userInterface.LogToOutput("Received the following command line arguments:", string.Join(" ", args));
                    userInterface.UseArgs(args);
                }
            }
            Application.Run(userInterface);
        }
    }
}