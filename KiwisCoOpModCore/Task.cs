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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;

namespace KiwisCoOpModCore
{
    public enum TaskType
    {
        None = 0,
        Button,
        Toggle,
    }
    public static class TaskHandler
    {
        public static bool Handle(List<Type> tasks, params object[]? vs)
        {
            bool handled = false;
            foreach(Type taskType in tasks)
            {
                ITask? newGamemode;
                if (vs != null)
                    newGamemode = (ITask?)Activator.CreateInstance(taskType);
                else
                    newGamemode = (ITask?)Activator.CreateInstance(taskType, vs);
                if (newGamemode != null)
                    handled = true;
            }
            return handled;
        }
        public static bool Handle(Type taskType)
        {
            bool handled = false;
            ITask? newGamemode = (ITask?)Activator.CreateInstance(taskType);
            if (newGamemode != null)
                handled = true;
            return handled;
        }
        public static ITask? Instance(Type taskType)
        {
            return (ITask?)Activator.CreateInstance(taskType);
        }
    }
    public interface ITask
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public TaskType? Type { get; set; }
        public bool? ToggleState { get; set; }
        public Color? DisplayColor { get; set; }
    }
}
