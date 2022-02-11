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
using System.Drawing;

namespace KiwisCoOpModCore
{
    public class BaseTask : ITask
    {
        public BaseTask() : base()
        {
            Author = "KiwifruitDev";
            Name = "Base Task";
            Description = "The start to a new task.";
            Type = TaskType.None;
            DisplayColor = Color.White;
        }
        public BaseTask(params object[]? vs)
        { }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public TaskType? Type { get; set; }
        public bool? ToggleState { get; set; }
        public Color? DisplayColor { get; set; }
    }
}
