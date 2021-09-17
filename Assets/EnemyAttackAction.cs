using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    [CreateAssetMenu(menuName = "A.I./Enemy Action/Attack Action")]
    public class EnemyAttackAction : EnemyAction
    {
        public int attackScore = 3;
        public float recoveryTime = 2;

        public float maxmimumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        public float minimumDistanceNeededToAttack = 0;
        public float maxmiumDistanceNeededToAttack = 3;
    }
}
