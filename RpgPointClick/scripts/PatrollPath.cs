using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpg.Control
{
    public class PatrollPath : MonoBehaviour
    {
        const float waypointsGizmosradius = 0.5f;
        private void OnDrawGizmos()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                int j = getNextIndex(i);
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointsGizmosradius);
                Gizmos.DrawLine(getWayPoint(i), getWayPoint(j));
            }
        }

        public int getNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 getWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
