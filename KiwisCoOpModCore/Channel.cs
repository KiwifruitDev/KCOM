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
        private Wrapper wrapper = new Wrapper();
        public Channel(string name)
        {
            this.name = name.ToUpper();
        }
        public Channel(string name, Wrapper wrapper)
        {
            this.name = name.ToUpper();
            this.wrapper = wrapper;
        }
        public Channel(string name, string leftBracket, string rightBracket)
        {
            this.name = name.ToUpper();
            wrapper = new Wrapper(leftBracket, rightBracket);
        }
        public Channel(string name, string leftBracket, string rightBracket, string suffix)
        {
            this.name = name.ToUpper();
            wrapper = new Wrapper(leftBracket, rightBracket, suffix);
        }
        public Channel(string name, Color color)
        {
            this.color = color;
            this.name = name.ToUpper();
        }
        public Channel(string name, Color color, Wrapper wrapper)
        {
            this.color = color;
            this.name = name.ToUpper();
            this.wrapper = wrapper;
        }
        public Channel(string name, Color color, string leftBracket, string rightBracket)
        {
            this.color = color;
            this.name = name.ToUpper();
            wrapper = new Wrapper(leftBracket, rightBracket);
        }
        public Channel(string name, Color color, string leftBracket, string rightBracket, string suffix)
        {
            this.color = color;
            this.name = name.ToUpper();
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
