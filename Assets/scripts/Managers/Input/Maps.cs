using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class KeyMap
{
    public KeyCode key;
    public KeyAction action;
}

[System.Serializable]
public class AxisMap
{
    public Axis axis;
    public List<KeyCode> keys;
    public Vector2 action;
}