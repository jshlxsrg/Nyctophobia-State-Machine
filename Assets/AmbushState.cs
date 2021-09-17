using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectionRadius = 2;
        public string sleepAnimation;
        public string wakeAnimation;

        public LayerMask detectionLayer;

        public ChaseState chaseState;

        public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);
            }

            #region Handle Target Detection
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                PlayerStats playerStats = colliders[i].transform.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    Vector3 targetsDirection = playerStats.transform.position - enemyManager.transform.position;
                    enemyManager.viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);

                    if(enemyManager.viewableAngle > enemyManager.minimumDetectionAngle
                        && enemyManager.viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = playerStats;
                        isSleeping = false;
                        enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }

            #endregion

            #region Handle State Change
            if (enemyManager.currentTarget != null)
            {
                return chaseState;
            }
            else
            {
                return this;
            }

            #endregion

        }
    }
}
