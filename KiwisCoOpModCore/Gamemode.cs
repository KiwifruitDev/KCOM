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
    }
    public static class GamemodeHandler
    {
        public static HandleState Handle(Type gamemodeType, GamemodeHandleType handleType, params object[]? vs)
        {
            Gamemode? newGamemode = (Gamemode?)Activator.CreateInstance(gamemodeType, handleType, vs);
            if (newGamemode != null && newGamemode.State != null)
            {
                return (HandleState)newGamemode.State;
            }
            return HandleState.Continue;
        }
        public static Gamemode? Instance(Type gamemodeType)
        {
            return (Gamemode?)Activator.CreateInstance(gamemodeType);
        }
    }
    public interface Gamemode
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public HandleState? State { get; set; }
    }
}
