using UnityEngine;

namespace Modules.Unit
{
    [RequireComponent(typeof(Animator))]
    public class BaseView : MonoBehaviour
    {
        protected Animator GetAnimator() 
        {
            Animator animator = GetComponent<Animator>();
            return animator;
        }
    }
}
