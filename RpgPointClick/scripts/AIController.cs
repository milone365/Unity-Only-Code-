using rpg.Combat;
using rpg.Core;
using rpg.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpg.Control
{
    public class AIController : MonoBehaviour
    {
        [Range(0,1)]
        [SerializeField] float patrollSpeedfraction = 0.2f;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField]GameObject player;
        Fighter fighter;
        Health health;
        float timeSinceAggraved = 0;
        Vector3 guardLocation;
        Mover mover;
        float timeSinceLastSeePlayer = Mathf.Infinity;
        float suspicionTime=3;
        [SerializeField] PatrollPath path=null;
        [SerializeField] float wayPointTollerance = 1f;
        int waypointIndex = 0;
        [SerializeField] float waypointDwellTime=3;
        float timeSinceArivedAtPoint = 0;
        [SerializeField] float agroCountdouwn=5, shoutDistance=5;
        

        private void Start()
        {
            mover = GetComponent<Mover>();
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            guardLocation = transform.position;
            
           
        }
        void Update()
        {
            if (health.isdead()) return;
            if (isAggraved() && fighter.canAttack(player))
            {
               
                Attackbehaviour();

            }
            else if (timeSinceLastSeePlayer < suspicionTime)
            {
                suspiciusBeahviour();

            }
            else
            {
                pathrolBeaviour();

            }
            updateTimers();
        }

        public void Aggravate()
        {
            timeSinceAggraved = 0;
        }
        private bool isAggraved()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void updateTimers()
        {
            timeSinceLastSeePlayer += Time.deltaTime;
            timeSinceArivedAtPoint += Time.deltaTime;
            timeSinceAggraved += Time.deltaTime;
        }

        private void pathrolBeaviour()
        {
            Vector3 nextPos = guardLocation;
            if (path != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArivedAtPoint = 0;
                    cycleWaypoint();
                }
                nextPos = geturrentWaypoint();
            }
            if (timeSinceArivedAtPoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPos,patrollSpeedfraction);
            }
            
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, geturrentWaypoint());
            return distanceToWaypoint < wayPointTollerance;
        }

        private void cycleWaypoint()
        {
            waypointIndex = path.getNextIndex(waypointIndex);
            
        }

         Vector3 geturrentWaypoint()
        {
           return path.getWayPoint(waypointIndex);
        }

        private void suspiciusBeahviour()
        {
            GetComponent<ActionScheduler>().cancelCurrentAction();
        }

        private void Attackbehaviour()
        {
            timeSinceLastSeePlayer = 0;
            fighter.attack(player);
            AggravateNearblyEnemies();
        }

        private void AggravateNearblyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            foreach(RaycastHit hit in hits)
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if (ai == null) continue;
                ai.Aggravate();
            }
        }

        private bool inAttackRange()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance < chaseDistance;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
    
}
