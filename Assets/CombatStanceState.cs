using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public ChaseState chaseState;
        public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

                if (enemyManager.currentRecoveryTime <= 0 && enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
                {
                    return attackState;
                }
                else if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
                {
                    return chaseState;
                }

                else
                {
                    return this;
                }
            
        }
    }
}
