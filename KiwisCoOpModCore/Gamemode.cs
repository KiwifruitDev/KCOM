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

namespace KiwisCoOpModCore
{
    public enum HandleState
    {
        Continue = 0,
        Handled
    }
    public enum GamemodeHandleType
    {
        None = 0,
        PreStart,
        PostStart,
        PreClose,
        PostClose,
        PreResponse,
        PostResponse,
        ClientOpen,
        ClientClose,
        Command
    }
    public static class GamemodeHandler
    {
        public static HandleState Handle(Type gamemodeType, GamemodeHandleType handleType, params object[]? vs)
        {
            IGamemode? newGamemode = (IGamemode?)Activator.CreateInstance(gamemodeType, handleType, vs);
            if (newGamemode != null && newGamemode.State != null)
            {
                return (HandleState)newGamemode.State;
            }
            return HandleState.Continue;
        }
        public static IGamemode? Instance(Type gamemodeType)
        {
            return (IGamemode?)Activator.CreateInstance(gamemodeType);
        }
    }
    public interface IGamemode
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public HandleState? State { get; set; }
    }
}
