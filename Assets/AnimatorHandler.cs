using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class AnimatorHandler : MonoBehaviour          //this is Animator Manager in SG
    {
        public Animator animator;
        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnim, 0.2f);
        }
    }
}
