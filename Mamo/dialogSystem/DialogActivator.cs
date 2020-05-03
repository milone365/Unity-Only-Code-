using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] dialogLines;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!DialogManager.instance.dialogActive)
                {
                    DialogManager.instance.dialogLines = dialogLines;
                    DialogManager.instance.currentline = 0;
                    DialogManager.instance.showDialog();
                }
            }
        }
    }
}
