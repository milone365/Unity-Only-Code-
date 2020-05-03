using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rpg.Movement;
using System;
using rpg.Combat;

namespace rpg.Control { 

public class PlayerController : MonoBehaviour
{
        Health health;
        Mover mover;

       
        [System.Serializable]
        struct cursorMapping
        {
            public cursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] cursorMapping[] cursorMappings = null;
    void Start()
    {
        mover = GetComponent<Mover>();
        health = GetComponent<Health>();
     }

     void Update()
    {
            if (health.isdead())
            {
                SetCursor(cursorType.none);
                return;
            }

            if (interactWithComponent())return ;
            
            if(interactMove())return;
            SetCursor(cursorType.none);
    }

        private bool interactWithComponent()
        {
            RaycastHit[] hits = raycastAllSorted();
            foreach(RaycastHit hit in hits)
            {
              IRaycastable[] raycastables= hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycast in raycastables)
                {
                    if (raycast.handleRayCast(this))
                    {
                        SetCursor(cursorType.atk);
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] raycastAllSorted()
        {
            RaycastHit[]hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for(int i = 0; i < distances.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }
        public bool interactMove()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                   
                    mover.StartMoveAction(hit.point,1);
                   

                }
                SetCursor(cursorType.moving);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        void SetCursor(cursorType type)
        {
            cursorMapping mapping = getCursorMapping(type);
            Cursor.SetCursor(mapping.texture,mapping.hotspot,CursorMode.Auto);
        }

        cursorMapping getCursorMapping(cursorType _type)
        {
            foreach(cursorMapping mapping in cursorMappings)
            {
                if (mapping.type == _type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
    }
}