using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    List<Blobb> allBlobs = new List<Blobb>();
    public int width;
    public int height;
    public GameObject[] blobblist;
    public static Board instance;
    public bool isSwapping;
    Blobb lastBlobb;
    bool turnChecked;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fillBoard();
        PermaBoardCheck();
    }
    //generate blomb in all space of grid
    void fillBoard()
    {
        blobb_Id[] previousLeft = new blobb_Id[height];
        blobb_Id previousBelow = 0;
        //if (blobblist.Length < 1) return;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                List<GameObject> possibleBlobbs = new List<GameObject>();
                possibleBlobbs.AddRange(blobblist);
                
                //check for automatic delete
                for(int k = possibleBlobbs.Count - 1; k >=0; k--)
                {
                    blobb_Id idToCheck = possibleBlobbs[k].GetComponent<Blobb>().id;
                    if (idToCheck == previousLeft[j]||idToCheck==previousBelow)
                    {
                        possibleBlobbs.RemoveAt(k);
                    }
                }
                //instantiate in position
                Vector3 pos = new Vector3(transform.position.x + i, transform.position.y + j, 0);
                int randomindex = Random.Range(0, possibleBlobbs.Count);
                GameObject newBlobb = Instantiate(possibleBlobbs[randomindex], pos, transform.rotation);
                //set parent this obj
                newBlobb.transform.SetParent(this.transform);
                //add to list
                Blobb b = newBlobb.GetComponent<Blobb>();
                allBlobs.Add(b);

                //save id
                previousBelow = b.id;
                previousLeft[j] = b.id;

            }
        }
    }



    #region Movement
    public void SwapBlobb(Blobb currentBlobb)
    {
        if (lastBlobb == null)
        {
            lastBlobb = currentBlobb;
        }
        else if (lastBlobb == currentBlobb)
        {
            lastBlobb = null;
        }
        else
        {
            //if lastblobb have currentblobb in neighbor list
            if (lastBlobb.CheckIfNeighbor(currentBlobb))
            {
                StartCoroutine(SwapCo(currentBlobb));
            }
            else
            {
                //deative particle system
                lastBlobb.TouggleSelector();
                lastBlobb = currentBlobb;
            }
        }
    }
    IEnumerator SwapCo(Blobb currentBlobb)
    {
        Blobb b1 = lastBlobb;
        Blobb b2 = currentBlobb;

        //change position from b1 and b2
        Vector3 b1StartPos = b1.transform.position;
        Vector3 b2StartPos = b2.transform.position;

        Vector3 b1EndPos = b2StartPos;
        Vector3 b2EndPos = b1StartPos;

        if (isSwapping)
        {
            yield break;
        }
        isSwapping = true;
        TouglePhysics(true);
        while (moveToSwapLocation(b1, b1EndPos) 
            && moveToSwapLocation(b2, b2EndPos))
        {
            yield return null;
        }

        b1.clearAllMatch();
        b2.clearAllMatch();

        while (!turnChecked)
        {
            yield return null;
        }
        bool b1Found = b1.matchFound;
        bool b2Found = b2.matchFound;
        //comeback if not found match
        if (!b1Found && !b2Found)
        {
            while (moveToSwapLocation(b1, b1StartPos)
            && moveToSwapLocation(b2, b2StartPos))
            {
                yield return null;
            }
        }
        //report matches
        if (b1Found || b2Found)
        {
            GameManager.instance.updateMatches();
        }
        turnChecked = false;
        //reset all
        TouglePhysics(false);
        isSwapping = false;
        b1.TouggleSelector();
        b2.TouggleSelector();
        lastBlobb = null;
    }
    //movement
    bool moveToSwapLocation(Blobb b, Vector3 goal)
    {
        return b.transform.position != 
            (b.transform.position=Vector3.MoveTowards(b.transform.position,goal,2*Time.deltaTime));
    }
    //change from dynamic to kinematic and reverse
    void TouglePhysics(bool flag)
    {
        for(int i = 0; i < allBlobs.Count; i++)
        {
            allBlobs[i].GetComponent<Rigidbody>().isKinematic = flag;
        }
    }
    #endregion
    /// DrawGrid

    public void CreateNewBlobb(Blobb b,Vector3 pos)
    {
        //remove
        allBlobs.Remove(b);
        //create new and add to list
        int rand = Random.Range(0, blobblist.Length);
        GameObject newBlobb = Instantiate(blobblist[rand], new Vector3(pos.x, pos.y+9, pos.z), Quaternion.identity);
        allBlobs.Add(newBlobb.GetComponent<Blobb>());
        newBlobb.transform.SetParent(this.transform);
    }
    //end turn
    public void reportTurnDone()
    {
        turnChecked = true;
    }
    //check if pieces are moving
   public bool checkIfBoardIsMoving()
    {
        for(int i = 0; i < allBlobs.Count; i++)
        {
            if (allBlobs[i].transform.localPosition.y > 7.6f)
            {
                return true;
            }
            if (allBlobs[i].GetComponent<Rigidbody>().velocity.y >0.1f)
            {
                return true;
            }
        }
        return false;
    }
    //automatic check
    IEnumerator PermaBoardCheck()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            if (!isSwapping && !checkIfBoardIsMoving())
            {
                for(int i = 0; i < allBlobs.Count; i++)
                {
                    allBlobs[i].clearAllMatch();
                }
            }
            if (!checkForPossibleMeshes())
            {
                Debug.Log("GAME OVER");
            }
            for(int i = 0; i < allBlobs.Count; i++)
            {
                allBlobs[i].possiblePoints = false;
                allBlobs[i].matchFound = true;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    bool checkForPossibleMeshes()
    {
        TouglePhysics(true);
        for(int i = 0; i < allBlobs.Count; i++)
        {
            for(int j = 0; j < allBlobs.Count; j++)
            {
                if (allBlobs[i].CheckNeighbours(allBlobs[j]))
                {
                    Blobb b1 = allBlobs[i];
                    Blobb b2 = allBlobs[j];
                    //neighbor list
                    List<Blobb> tempNeighbors = new List<Blobb>(b1.nearBlobb_List);
                    //positions
                    Vector3 b1tempPos = b1.transform.position;
                    Vector3 b2tempPos = b2.transform.position;
                    b1.transform.position = b2tempPos;
                    b2.transform.position = b1tempPos;
                    b1.nearBlobb_List = b2.nearBlobb_List;
                    b2.nearBlobb_List = tempNeighbors;

                    if (b1.checkForExistingMatch()|| b2.checkForExistingMatch())
                    {
                        b1.transform.position = b1tempPos;
                        b2.transform.position = b2tempPos;
                        b2.nearBlobb_List = b1.nearBlobb_List;
                        b1.nearBlobb_List = tempNeighbors;
                        TouglePhysics(false);
                        return true;
                    }
                 
                }
               
            }
        }
        return false;
    }
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        for( int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Gizmos.DrawWireCube(new Vector3(transform.position.x + i,transform.position.y+j,0),new Vector3(1,1,1));
            }
        }
    }

#endif
}
//fillboard

/* Vector3 pos = new Vector3(transform.position.x + i, transform.position.y + j, 0);
             int randomindex = Random.Range(0, blobblist.Length);
             GameObject newBlobb = Instantiate(blobblist[randomindex], pos,transform.rotation);
             //set parent this obj
             newBlobb.transform.SetParent(this.transform);
             //add to list
             allBlobs.Add(newBlobb.GetComponent<Blobb>());*/
