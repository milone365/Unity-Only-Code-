using rpg.Combat;
using UnityEngine;

namespace rpg.Control
{
    public interface IRaycastable
    {
       
        cursorType getCursorType();
        bool handleRayCast(PlayerController callingController);
    }

}

