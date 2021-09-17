using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{

    public class EnemyAnimatorManager : AnimatorHandler         //Animator Manager in SG
    {
        EnemyManager enemyManager;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            enemyManager = GetComponent<EnemyManager>();
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidBody.velocity = velocity;

        }
    }
}