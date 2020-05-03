using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    itemType type;
    [SerializeField]
    float value = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (type)
            {
                //スコアアップ
                case itemType.scoreUp:
                    break;
                    //時間アップ
                case itemType.TimeUp:
                    break;
                    //回復
                case itemType.Healing:
                    break;
            }
            Destroy(gameObject);
        }

    }

    public enum itemType
{
    scoreUp,
    TimeUp,
    Healing
}
}