using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGrid))]
public class GameGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameGrid grid = (GameGrid) target;
        if (GUILayout.Button("Generate new grid")){
            grid.CreateGrid();
        }
        if (GUILayout.Button("Link tiles")){
            grid.LinkTiles();
        }
        if (GUILayout.Button("Destroy all tiles")){
            grid.DestroyAllTiles();
        }
    }
}
