using UnityEngine;

namespace Modules.Unit
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField] private BaseView _baseView;

        protected BaseView BaseView => _baseView;
    }
}
