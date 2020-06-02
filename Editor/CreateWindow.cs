using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDesignerWindow : EditorWindow
{
    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        //create window instance
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        //set size window size
        window.minSize = new Vector2(600, 300);
        //for see the window
        window.Show();
    }
}
