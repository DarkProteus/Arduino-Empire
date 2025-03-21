using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PositionManager : MonoBehaviour
{
    private Position pos;
    public int index;
    void Start()
    {
        pos = AssetDatabase.LoadAssetAtPath<Position>($"Assets/Scriptables/Positions/{transform.parent.gameObject.name}.asset");
        index = pos.index;
    }
}
