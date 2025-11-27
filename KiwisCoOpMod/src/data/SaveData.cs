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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace KiwisCoOpMod
{
    public static class SaveData
    {
        public static Dictionary<string, string> saveValues = new Dictionary<string, string>()
        {
            {"VConsoleIP", "127.0.0.1"},
            {"VConsolePort", "29000"},
            {"VConsoleProtocol", "211"},
            {"RetryTimeout", "5000"}
        };
        public static string saveFileName = "Options.json";
        public static bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(saveValues, Formatting.Indented);
                File.WriteAllText(saveFileName, json);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool Load()
        {
            try
            {
                bool go = true;
                if (!File.Exists(saveFileName))
                {
                    Console.WriteLine("Save file not found. Creating new one.");
                    go = Save();
                }
                if(go)
                {
                    string json = File.ReadAllText(saveFileName);
                    Dictionary<string, string>? loadedValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (loadedValues == null)
                    {
                        Console.WriteLine("Save file is corrupted.");
                        loadedValues = new Dictionary<string, string>();
                    }
                    // Merge loaded values into save values.
                    foreach (KeyValuePair<string, string> pair in saveValues)
                    {
                        if (loadedValues.ContainsKey(pair.Key))
                        {
                            if (loadedValues[pair.Key] != pair.Value)
                            {
                                saveValues[pair.Key] = loadedValues[pair.Key];
                            }
                        }
                        else
                        {
                            saveValues[pair.Key] = pair.Value;
                        }
                    }
                    // Save the new values.
                    Save();
                }
                else
                {
                    Console.WriteLine("Failed to load save file.");
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
