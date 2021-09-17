using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class AnimatorManager: AnimatorHandler   
    {
        float snappedHorizontal;
        float snappedVertical;
        private void Awake()
        {
            animator = GetComponent<Animator>();

        }

        public void HandleAnimatorValues(float horizonalMovement, float verticalMovement, bool isRunning)
        {
            if (horizonalMovement > 0)
            {
                snappedHorizontal = 1;
            }
            else if (horizonalMovement < 0)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }

            if (verticalMovement > 0)
            {
                snappedVertical = 1;
            }
            else if (verticalMovement < 0)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }
            if (isRunning && snappedVertical > 0)
            {
                snappedVertical = 2;
            }

            animator.SetFloat("Horizontal", snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat("Vertical", snappedVertical, 0.1f, Time.deltaTime);

        }
    }
}