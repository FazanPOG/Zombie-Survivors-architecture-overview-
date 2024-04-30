
namespace Modules.Game.StateMachine.Scripts
{
    public interface IGameStateMachine
    {
        void EnterIn<T>() where T : IGameState;
    }
}
