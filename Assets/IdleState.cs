using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class IdleState : State
    {
        public ChaseState chaseState;
        public SearchingState searchingState;

        public LayerMask detectionLayer;

        float timer;
        public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            #region Handle Enemy Target Detection
           
                Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    PlayerStats playerStats = colliders[i].transform.GetComponent<PlayerStats>();

                    if (playerStats != null)
                    {
                        Vector3 targetDirection = playerStats.transform.position - transform.position;
                        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                        {
                            enemyManager.currentTarget = playerStats;
                        }
                    }


                }
            
            #endregion

            #region Handle Switching to Next State
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