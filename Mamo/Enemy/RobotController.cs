using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public GameObject effect,ricompense;
    public GameObject[] robotPieces;
    public enum phases { intro,phase1,phase2,phase3,end}
    public phases currentPhase = phases.intro;
    public enemyAnimationManager anim;
    public GameObject entrace,robotActivetor;
    public float battleStartRange = 50f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<enemyAnimationManager>();
        entrace.SetActive(false);
        robotActivetor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == 0)
        {
            var distance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            if (distance <= battleStartRange)
            {
                goToNextPhase();
            }
        }

        if (GameManager.instace.respawning)
        {
            currentPhase = phases.intro;
            anim.animationPhase(0);
            gameObject.SetActive(false);
            robotActivetor.SetActive(true);
            entrace.SetActive(true);
            GameManager.instace.respawning = false;
        }
    }
    public void goToNextPhase()
    {
        
        if (currentPhase != phases.end)
        {
            currentPhase++;
            anim.TriggerAnim("hurt");
            switch (currentPhase)
            {
                case phases.phase1:anim.animationPhase(1); break;
                case phases.phase2:anim.animationPhase(2); break;
                case phases.phase3:anim.animationPhase(3); break;
                case phases.end:StartCoroutine(endBattle()); break;

            }
        }
        else
        {
            StartCoroutine(endBattle());
        }
    }
    public IEnumerator endBattle()
    {
        for (int i = 0; i < robotPieces.Length; i++)
        {
            Instantiate(effect, robotPieces[i].transform.position, robotPieces[i].transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        Instantiate(ricompense, Bag.instance.armSpawner.position, Bag.instance.armSpawner.rotation); 
        anim.animationDie();
        yield return new WaitForSeconds(3f);
        entrace.SetActive(true);
        gameObject.SetActive(false);
    }
   
}
