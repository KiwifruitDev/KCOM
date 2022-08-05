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
using Microsoft.Win32;

namespace KiwisCoOpMod
{
    internal static class Program
    {
        public static readonly UserInterface userInterface = new(); // WORKAROUND SPECIFICALLY FOR LUA
        const string UriScheme = "kcom";
        const string FriendlyName = "Kiwi's Co-Op Mod";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
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
                    userInterface.LogToOutputGeneric("Received the following URI scheme:", uri);
                    userInterface.UseUriScheme(uri);
                }
                else
                {
                    userInterface.LogToOutputGeneric("Received the following command line arguments:", string.Join(" ", args));
                    userInterface.UseArgs(args);
                }
            }
            Application.Run(userInterface);
        }
    }
}