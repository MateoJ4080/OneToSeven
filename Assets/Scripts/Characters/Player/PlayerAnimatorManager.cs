using UnityEngine;
using System.Collections;

namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        #region Private Fields

        [SerializeField]
        private float directionDampTime = 0.25f;

        #endregion

        private Animator animator;

        #region MonoBehaviour Callbacks

        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        void Update()
        {
            if (!animator)
            {
                return;
            }
            float h = InputManager.Instance.GetPlayerMovement().x;
            float v = InputManager.Instance.GetPlayerMovement().y;
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);

        }

        #endregion
    }
}