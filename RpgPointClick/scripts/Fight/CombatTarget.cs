using rpg.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpg.Combat
{
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public cursorType getCursorType()
        {
            return cursorType.atk;
        }

        public bool handleRayCast(PlayerController callingController)
        {
            
            if (!callingController.GetComponent<Fighter>().canAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Fighter>().attack(gameObject);
            }
           
            return true;
        }
    }
}

