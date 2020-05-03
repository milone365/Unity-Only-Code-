using rpg.Combat;
using rpg.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour,IRaycastable
{
    [SerializeField] WeaponConfig weapon = null;
    float deativeTime = 5;

    public cursorType getCursorType()
    {
        return cursorType.pickUp;
    }

    public bool handleRayCast(PlayerController callingController)
    {
        if (Input.GetMouseButtonDown(0))
        {
            pickUp(callingController);
        }
        getCursorType();
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickUp(other.GetComponent<PlayerController>());
        }
    }

    private void pickUp(PlayerController callingController)
    {
        callingController.GetComponent<Fighter>().EquipWeapon(weapon);
        StartCoroutine(showafterTime(deativeTime));
    }

    IEnumerator showafterTime(float deativeTime)
    {
        showPickUp(false);
        yield return new WaitForSeconds(deativeTime);
        showPickUp(true);
    }

    private void showPickUp(bool v)
    {
        GetComponent<Collider>().enabled = v;
        GetComponentInChildren<MeshRenderer>().enabled = v;
    }
}
