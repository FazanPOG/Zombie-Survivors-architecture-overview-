using UnityEngine;

namespace Modules.Unit.Interfaces
{
    public interface IUnitMovement : IUnitSpeed
    {
        void MoveToTarget();
        void StopMove();
        void SetMoveTarget(Vector3 target);
    }
}
