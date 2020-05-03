using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationTrigger : MonoBehaviour
{
    [SerializeField] GameObject drone=null;
    //偽物のドローン
    [SerializeField] GameObject[] fakeDrone;
    bool isTheFirstTime;
    // Start is called before the first frame update
    void Start()
    {
        isTheFirstTime = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isTheFirstTime)
            {
                isTheFirstTime = false;
                //偽物の画像を消して、おっもののドローンを作成する
                foreach (GameObject dr in fakeDrone)
                {
                    
                    Instantiate(drone, dr.transform.position, dr.transform.rotation);
                    dr.SetActive(false);
                }


            }
        }
    }
}
