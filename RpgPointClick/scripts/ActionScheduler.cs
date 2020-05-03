using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpg.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void startAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction!= null)
            {
                currentAction.Cancel();
            }
             currentAction = action;
            
        }
        public void cancelCurrentAction()
        {
            startAction(null);
    }
    }
   

}