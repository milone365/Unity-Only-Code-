using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityes : MonoBehaviour
{
   
    public GameObject effect;
    public Transform[] teleportPoints;
    private int currentTeleportPoint;
    public GameObject bullet;
    public Transform bulletSpawner;
    public float minSize=0.5f;
    public GameObject enemyToSpawn;
    public BossBattlePhae bossPha;
    public enemyAnimationManager anim;
    public Transform[] spawnPoints;
    public BossHealth bh;

    private enum CoRoutineNames
    {
        Duplicate = 0,
        Teleport,
        Evocation,
        none
    }
    [SerializeField]
    private CoRoutineNames phase1 = CoRoutineNames.Teleport;
    [SerializeField]
    private CoRoutineNames phase2 = CoRoutineNames.Teleport;
    [SerializeField]
    private CoRoutineNames phase3 = CoRoutineNames.Teleport;

    private List<string> _coNames = new List<string>()
    {
        "Duplicate",
        "Teleport",
        "Evocation",
        "none"
    };
    private void Awake()
    {

    }
    public void Start() 
    {
        bh= GetComponent<BossHealth>();
        bossPha = GetComponent<BossBattlePhae>();
        anim.GetComponent<enemyAnimationManager>();
    }

    public void wave()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(bullet, new Vector3(bulletSpawner.position.x + Random.Range(-2f,2f), bulletSpawner.position.y, bulletSpawner.position.z + Random.Range(-2f, 2f)), bulletSpawner.rotation);
        }
        
    }
    public void bulletGen()
    {
        Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
    }

    public void  activePhase1()
    {
        bh.goingTotheNextPhase = false;
        StartCoroutine(_coNames[(int)phase1]);
    }
    public void activePhase2()
    {
        bh.goingTotheNextPhase = false;
        StartCoroutine(_coNames[(int)phase2]);
    }
    public void activePhase3()
    {
        bh.goingTotheNextPhase = false;
        StartCoroutine(_coNames[(int)phase3]);
    }

     private IEnumerator Duplicate()
    {
        yield return new WaitForSeconds(0.5f);
        transform.localScale = transform.localScale * 0.5f;
        if (transform.localScale.y >= minSize)
        {
            GameObject clone1 = Instantiate(enemyToSpawn, new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            GameObject clone2 = Instantiate(enemyToSpawn, new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            clone1.transform.localScale = transform.localScale;
            clone2.transform.localScale = transform.localScale;
            bossPha.checkState();
        }
        else
        {
            Destroy(gameObject);
        }
        }

    public void activeAbility(string name)
    {
        StartCoroutine(name);
    }
    private IEnumerator Evocation()
    {
        anim.TriggerAnim("ability");
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(enemyToSpawn, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        
    }
    private IEnumerator none()
    {
        yield return new WaitForSeconds(1f);

    }
    private IEnumerator Teleport()
    {

        yield return new WaitForSeconds(0.2f);
        
        anim.TriggerAnim("teleport");
        yield return new WaitForSeconds(1.5f);
        Instantiate(effect, transform.position, transform.rotation);
        transform.position = teleportPoints[currentTeleportPoint].position;
        currentTeleportPoint++;
        if (currentTeleportPoint >= teleportPoints.Length)
        {
            currentTeleportPoint = 0;
        }
        yield return new WaitForSeconds(1f);
        bossPha.checkState();
    }
}
