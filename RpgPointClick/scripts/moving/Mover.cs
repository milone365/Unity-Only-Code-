using rpg.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace rpg.Movement { 

public class Mover : MonoBehaviour,IAction
{
    //[SerializeField] float maxSpeed=6;
   
    Transform target;
    Ray lastRay;
    NavMeshAgent agent;
    
    void Start()
    {
            agent = GetComponent<NavMeshAgent>();
            target = this.transform;
    }

    
    void Update()
    {
        updateAnimator();
            
    }

    private void updateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("speed",speed);
    }

    public void moveTo(Vector3 point,float speedFraction)
    {
        agent.destination = point;
           // agent.speed=maxSpeed*Mathf.Clamp01(speedFraction);
        agent.isStopped = false;
    }

   public void StartMoveAction(Vector3 destination,float speed)
   {
            GetComponent<ActionScheduler>().startAction(this);
            moveTo(destination,speed);
   }

        public void Cancel()
        {
            if (agent.enabled==false) return;
            agent.isStopped = true;
        }

        internal bool CanMoveTo(Vector3 position)
        {
             /* NavMeshPath path = new NavMeshPath();
              bool hasPath = NavMesh.CalculatePath(transform.position, position, NavMesh.AllAreas, path);
              if (!hasPath) return false;
              if (path.status != NavMeshPathStatus.PathComplete) return false;
              if (GetPathLenght(path) > maxNavPathLenght) return false;
              */
            return true;
            
        }
    }
}