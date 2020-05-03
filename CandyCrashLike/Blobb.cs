using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Blobb : MonoBehaviour
{
    //near blobs list
    public List<Blobb> nearBlobb_List = new List<Blobb>();
    ParticleSystem selector;
    bool isSelected = false;
    public blobb_Id id;
    public bool matchFound;
    int score = 0;
   public bool possiblePoints = true;
    //for check all neighbors in all directions
    Vector3[] checkDir = new Vector3[4] { Vector3.left, Vector3.right, Vector3.up, Vector3.down };
    void Start()
    {
        selector = GetComponentInChildren<ParticleSystem>();
        GetAllNeighbors();
        StartCoroutine(destroyFunction());
        score = (int)id+1 * 10;
    }

    //when click with mouse
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GetAllNeighbors();
            if (!Board.instance.isSwapping)
            {
                TouggleSelector();
                Board.instance.SwapBlobb(this);
            }
        }
    }

    //on selecting blobb
    public  void TouggleSelector()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            selector.Play();
        }
        else
        {
            selector.Stop();
        }
    }
    
    //clear list
    void GetAllNeighbors()
    {
        nearBlobb_List.Clear();
        //check in all directions, if find blobb add it to the list
        for(int i = 0; i < checkDir.Length; i++)
        {
           nearBlobb_List.Add(getNeighbor(checkDir[i]));
        }
    }

    //get return single blobb if find it with raycast
    public Blobb getNeighbor(Vector3 checkDirection)
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, checkDirection, out hitInfo))
        {
            if (hitInfo.collider != null)
            {
                Blobb b = hitInfo.collider.GetComponent<Blobb>();
                if (b != null) return b;
            }
        }
        return null;
    }

    public Blobb getNeighbor(Vector3 checkDirection, blobb_Id id)
    {
       
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, checkDirection, out hitInfo,0.8f))
        {
            if (hitInfo.collider != null)
            {
                Blobb b = hitInfo.collider.GetComponent<Blobb>();
                if (b != null&&b.id==id) return b;
            }
        }
        return null;
    }
    //check if list contains passed blobb obj
    public bool CheckIfNeighbor(Blobb b)
    {
        if (nearBlobb_List.Contains(b))
        {
            return true;
        }
        return false;
    }

    //buil list of equal blobbs
    List<Blobb> FindMatch(Vector3 checkDir)
    {
        List<Blobb> matchList = new List<Blobb>();
        List<Blobb> checkList = new List<Blobb>();
        //add thi element
        checkList.Add(this);
        //for all blob check id if is equal add to checklist
        for(int i = 0; i < checkList.Count; i++)
        {
            Blobb b = checkList[i].getNeighbor(checkDir,id);
            if (b != null && b.id == id)
            {
                checkList.Add(b);
            }
        }
        //implement check list to matchlist and return matchlist
        matchList.AddRange(checkList);
        return matchList;
    }

    bool CheckMatch(Vector3[] dirs)
    {
        List<Blobb> matchingBlobbList = new List<Blobb>();
        List<Blobb> sortedList = new List<Blobb>();
        for (int i = 0; i < dirs.Length; i++)
        {
            matchingBlobbList.AddRange(FindMatch(dirs[i]));
        }
        //add elements to sortedList
        for (int i = 0; i < matchingBlobbList.Count; i++)
        {
            if (!sortedList.Contains(matchingBlobbList[i]))
            {
                sortedList.Add(matchingBlobbList[i]);
            }
        }
        //delete all elements
        if (sortedList.Count >= 3)
        {
            return true;
        }
        return false;
    }
    void ClearMatch(Vector3[] dir)
    {
        List<Blobb> matchingBlobbList = new List<Blobb>();
        List<Blobb> sortedList = new List<Blobb>();
        for(int i = 0; i < dir.Length; i++)
        {
            matchingBlobbList.AddRange(FindMatch(dir[i]));
        }
        //add elements to sortedList
        for(int i = 0; i < matchingBlobbList.Count; i++)
        {
            if (!sortedList.Contains(matchingBlobbList[i]))
            {
                sortedList.Add(matchingBlobbList[i]);
            }
        }
        //delete all elements
        if (sortedList.Count >= 3)
        {
            for(int i = 0; i < sortedList.Count; i++)
            {
                sortedList[i].matchFound = true;
            }
        }
    }
   public void clearAllMatch()
    {
        ClearMatch(new Vector3[2]{ Vector3.left,Vector3.right});
        ClearMatch(new Vector3[2] { Vector3.up, Vector3.down });
        Board.instance.reportTurnDone();
    }

    //check for destroy blobbs (update every 0.25 seconds)
    IEnumerator destroyFunction()
    {
        while (true)
        {
            if (matchFound)
            {
                if(possiblePoints)
                GameManager.instance.addScore(score);
                Board.instance.CreateNewBlobb(this, transform.position);
                Destroy(gameObject);
                yield break;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public bool CheckNeighbours(Blobb b)
    {
        GetAllNeighbors();
        if (nearBlobb_List.Contains(b))
        {
            return true;
        }
        return false;
    }

    public bool checkForExistingMatch()
    {
        if(CheckMatch(new Vector3[2] { Vector3.left,Vector3.right})
        ||CheckMatch(new Vector3[2] {Vector3.up,Vector3.down })){
            return true;
        }
        return false;
    }
}

public enum blobb_Id
{
    green,
    blue,
    yellow,
    purple,
    orange,
    red
}