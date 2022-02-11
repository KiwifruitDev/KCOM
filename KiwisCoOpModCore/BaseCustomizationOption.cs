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
    public class BaseCustomizationOption : ICustomizationOption
    {
        public BaseCustomizationOption() : base()
        {
            Author = "KiwifruitDev";
            Name = "Base CustomizationOption";
            Description = "The start to a new customization option.";
            ModelName = "models/characters/alyx/alyx.vmdl";
            Type = CustomizationOptionType.None;
            DisplayImageBase64 = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==";
            Default = false;
        }
        public BaseCustomizationOption(params object[]? vs)
        { }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? ModelName { get; set; }
        public CustomizationOptionType? Type { get; set; }
        public string? DisplayImageBase64 { get; set; }
        public bool Default { get; set; }
    }
}
