/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2024 KiwifruitDev. All rights reserved.
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

using System;
using System.Threading;
using System.Globalization;

namespace KiwisCoOpMod
{
    public static class Program
    {
        // VConsole thread
        public static void Main()
        {
            Console.WriteLine("Starting Kiwi's Co-Op Mod for Half-Life: Alyx...");
            // Load save data
            SaveData.Load();
            // Connect to VConsole
            Connect();
            // Input
            while (true)
            {
                string? input = Console.ReadLine();
                if(input != null && VConsole.Connected)
                {
                    VConsole.Command(input.Replace("\n", ""));
                }
            }
        }

        private static void Connect()
        {
            Console.WriteLine("Connecting to VConsole...");
            // Create new thread
            Thread vConsoleThread = new Thread(new ThreadStart(VConsoleThread));
            vConsoleThread.Start();
        }
        private static void VConsoleThread()
        {
            if (VConsole.Connect())
            {
                Console.WriteLine("Connected to VConsole.");
            }
            else
            {
                Console.WriteLine("Failed to connect to VConsole.");
            }
            // Do not exit the thread while connected
            while (VConsole.Connected)
            {
                //VConsole.Focus();
                VConsole.Read();
            }
            int retryTimeout = int.Parse(SaveData.saveValues["RetryTimeout"], CultureInfo.InvariantCulture);
            Console.WriteLine("Disconnected from VConsole, retrying in " + (retryTimeout / 1000) + " seconds...");
            Thread.Sleep(retryTimeout);
            Connect();
        }
    }
}
