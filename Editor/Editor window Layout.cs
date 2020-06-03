using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSelectionTexture;
    Texture2D mage_selectionTexture;
    Texture2D warrion_selectionTexture;
    Texture2D rogue_selectionTexture;
    Color headerColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect header_selection;
    Rect mage_selection;
    Rect warrior_selection;
    Rect rogue_selection;

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

    private void OnEnable()
    {
        InitTexture();
    }
    void InitTexture()
    {
        headerSelectionTexture = new Texture2D(1, 1);
        headerSelectionTexture.SetPixel(0, 0, headerColor);
        headerSelectionTexture.Apply();
        //load texture from resources 
        mage_selectionTexture = Resources.Load<Texture2D>("icons/editor_mage_gradient");
       warrion_selectionTexture = Resources.Load<Texture2D>("icons/editor_warrior_gradient");
        rogue_selectionTexture = Resources.Load<Texture2D>("icons/editor_rogue_gradient");

    }
    private void OnGUI()
    {
        DrawLayout();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
    }
    void DrawLayout()
    {
        header_selection.x = 0;
        header_selection.y = 0;
        header_selection.width = Screen.width;
        header_selection.height = 50;
        //
        mage_selection.x = 0;
        mage_selection.y = 50;
        mage_selection.width = Screen.width / 3;
        mage_selection.height = Screen.width - 50;
        //
        warrior_selection.x = Screen.width / 3;
        warrior_selection.y = 50;
        warrior_selection.width = Screen.width / 3;
        warrior_selection.height = Screen.width - 50;
        //
        rogue_selection.x = (Screen.width / 3) * 2;
        rogue_selection.y = 50;
        rogue_selection.width = Screen.width / 3;
        rogue_selection.height = Screen.width - 50;
        
        //drawn on the top
        GUI.DrawTexture(header_selection, headerSelectionTexture);
        //draw texture in left side
        GUI.DrawTexture(mage_selection, mage_selectionTexture);
        GUI.DrawTexture(warrior_selection, warrion_selectionTexture);
        GUI.DrawTexture(rogue_selection, rogue_selectionTexture);
    }

    
    void DrawHeader()
    {
        GUILayout.BeginArea(header_selection);

        GUILayout.EndArea();
    }
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mage_selection);

        GUILayout.EndArea();
    }
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warrior_selection);

        GUILayout.EndArea();
    }
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogue_selection);

        GUILayout.EndArea();
    }
}
