using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JU
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 50;
        public int maxHealth;
        public int currentHealth;

       

        void Update()
        {
            currentHealth = maxHealth;
            
        }

        
    }
}
