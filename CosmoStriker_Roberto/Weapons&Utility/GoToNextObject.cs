using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextObject : MonoBehaviour
{
    [SerializeField]GameObject[] array;
    int current;

    public void activeNext(int obj)
    {
        array[obj].SetActive(true);
    }


}
