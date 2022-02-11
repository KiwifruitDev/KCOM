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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwisCoOpModCore
{
    public class Wrapper
    {
        private string left = "[";
        private string right = "]";
        private string suffix = " ";
        public Wrapper()
        { }
        public Wrapper(string left, string right)
        {
            this.left = left;
            this.right = right;
        }
        public Wrapper(string left, string right, string suffix)
        {
            this.left = left;
            this.right = right;
            this.suffix = suffix;
        }
        public string GetLeft()
        {
            return left;
        }
        public void SetLeft(string left)
        {
            this.left = left;
        }
        public string GetRight()
        {
            return right;
        }
        public void SetRight(string right)
        {
            this.right = right;
        }
        public string GetSuffix()
        {
            return suffix;
        }
        public void SetSuffix(string suffix)
        {
            this.suffix = suffix;
        }
        public string Wrap(string text)
        {
            return left + text + right + suffix;
        }
        public Wrapper Empty()
        {
            left = "";
            right = "";
            suffix = "";
            return this;
        }
        public override string ToString()
        {
            return left + right + suffix;
        }
    }
    public class Channel
    {
        private Color color = Color.Gray;
        private string name = "GENERIC";
        private string friendlyName = "Generic";
        private Wrapper wrapper = new();
        public Channel(string name, string friendlyName)
        {
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
        }
        public Channel(string name, string friendlyName, Wrapper wrapper)
        {
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
            this.wrapper = wrapper;
        }
        public Channel(string name, string friendlyName, string leftBracket, string rightBracket)
        {
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
            wrapper = new Wrapper(leftBracket, rightBracket);
        }
        public Channel(string name, string friendlyName, string leftBracket, string rightBracket, string suffix)
        {
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
            wrapper = new Wrapper(leftBracket, rightBracket, suffix);
        }
        public Channel(string name, string friendlyName, Color color)
        {
            this.color = color;
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
        }
        public Channel(string name, string friendlyName, Color color, Wrapper wrapper)
        {
            this.color = color;
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
            this.wrapper = wrapper;
        }
        public Channel(string name, string friendlyName, Color color, string leftBracket, string rightBracket)
        {
            this.color = color;
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
            wrapper = new Wrapper(leftBracket, rightBracket);
        }
        public Channel(string name, string friendlyName, Color color, string leftBracket, string rightBracket, string suffix)
        {
            this.color = color;
            this.name = name.ToUpper();
            this.friendlyName = friendlyName;
            wrapper = new Wrapper(leftBracket, rightBracket, suffix);
        }
        public string GetName()
        {
            return name;
        }
        public void SetName(string name)
        {
            this.name = name.ToUpper();
        }
        public string GetFriendlyName()
        {
            return friendlyName;
        }
        public void SetFriendlyName(string friendlyName)
        {
            this.friendlyName = friendlyName;
        }
        public Color GetColor()
        {
            return color;
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }
        public Wrapper GetWrapper()
        {
            return wrapper;
        }
        public void SetWrapper(Wrapper wrapper)
        {
            this.wrapper = wrapper;
        }
        public override string ToString()
        {
            return wrapper.Wrap(name);
        }
    }
}
