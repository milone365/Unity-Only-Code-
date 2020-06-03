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
    //scriptable object class obj
    static EnemySample mage_En;
    static EnemySample warrior_En;
    static EnemySample rogue_En;
    //properties return so
    public static EnemySample mage_info { get { return mage_En; } }
    public static EnemySample warrior_info { get { return warrior_En; } }
    public static EnemySample rogue_info { get { return rogue_En; } }

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

    #region INIT
    private void OnEnable()
    {
        InitTexture();
        InitData();
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
    
    //create scriptable object enemy to pass
    public static void InitData()
    {
        mage_En = (EnemySample)ScriptableObject.CreateInstance(typeof(EnemySample));
        warrior_En = (EnemySample)ScriptableObject.CreateInstance(typeof(EnemySample));
        rogue_En = (EnemySample)ScriptableObject.CreateInstance(typeof(EnemySample));
    }
    #endregion

    #region DRAW
    private void OnGUI()
    {
        DrawLayout();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
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
        GUILayout.Label("Enemy Designer");
        GUILayout.EndArea();
    }
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mage_selection);
        //write label
        GUILayout.Label("Mage");
        EditorGUILayout.BeginHorizontal();
        //create enum popup
        EditorGUILayout.EnumPopup(mage_En.arm);
        EditorGUILayout.EndHorizontal();
        //butto for create character, open new window
        if(GUILayout.Button("Create"))
        {
            GenerateSettings.OpenWindow(GenerateSettings.settings.MAGE);
        }

        GUILayout.EndArea();
    }
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warrior_selection);
        //write label
        GUILayout.Label("Warrior");
        //create enum popup
        //warrior_En.arm = (ArmType)EditorGUILayout.EnumPopup(ArmType.TWOHAND);
        EditorGUILayout.EnumPopup(warrior_En.arm);
        //butto for create character, open new window
        if (GUILayout.Button("Create"))
        {
            GenerateSettings.OpenWindow(GenerateSettings.settings.WARRIOR);
        }
        GUILayout.EndArea();
    }
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogue_selection);
        //write label
        GUILayout.Label("Rogue");
        //create enum popup
       EditorGUILayout.EnumPopup(rogue_En.arm);
        //butto for create character, open new window
        if (GUILayout.Button("Create"))
        {
            GenerateSettings.OpenWindow(GenerateSettings.settings.ROGUE);
        }
        GUILayout.EndArea();
    }
    #endregion
}
public class GenerateSettings:EditorWindow
{
    public enum settings
    {
        MAGE,
        WARRIOR,
        ROGUE
    }
    static settings dataset;
    //intance
    static GenerateSettings window;

    public static void OpenWindow(settings type)
    {
        dataset = type;
        window = (GenerateSettings)GetWindow(typeof(GenerateSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }
}