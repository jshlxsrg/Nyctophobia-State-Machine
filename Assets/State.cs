using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager);
    }
}
