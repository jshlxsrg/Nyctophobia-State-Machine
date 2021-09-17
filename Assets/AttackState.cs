using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;
        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;
        public override State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            #region Attacks

            if (enemyManager.isPreformingAction)
                return combatStanceState;


            if (currentAttack != null)
            {
                if(enemyManager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                else if (enemyManager.distanceFromTarget < currentAttack.maxmiumDistanceNeededToAttack)
                {
                    if(enemyManager.viewableAngle <= currentAttack.maxmimumAttackAngle &&
                       enemyManager.viewableAngle >= currentAttack.minimumAttackAngle)
                    {
                        if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPreformingAction == false)
                        {
                            enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.animator.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                            enemyManager.isPreformingAction = true;
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
                        }
                    }
                }
            }
            else
            {
                GetNewAttack(enemyManager);
            }

            return combatStanceState;

        }
        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetsDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyManager.distanceFromTarget <= enemyAttackAction.maxmiumDistanceNeededToAttack
                    && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maxmimumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyManager.distanceFromTarget <= enemyAttackAction.maxmiumDistanceNeededToAttack
                    && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maxmimumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }

        }

            #endregion
    }
}
