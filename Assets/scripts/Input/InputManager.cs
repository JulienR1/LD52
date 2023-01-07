using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance = null;

    public List<KeyMap> keyMaps;
    public List<AxisMap> axisMaps;

    private Dictionary<KeyAction, KeyCode[]> computedKeyMaps;
    private Dictionary<Axis, Dictionary<KeyCode, Vector2>> computedAxisMaps;

    private void Awake()
    {
        if (InputManager.instance == null)
        {
            InputManager.instance = this;
            InitializeManager();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void InitializeManager()
    {
        computedKeyMaps = new Dictionary<KeyAction, KeyCode[]>();
        foreach (var action in (KeyAction[])Enum.GetValues(typeof(KeyAction)))
        {
            var maps = keyMaps.FindAll((map) => map.action == action);
            computedKeyMaps.Add(action, maps.Select((map) => map.key).ToArray());
        }

        computedAxisMaps = new Dictionary<Axis, Dictionary<KeyCode, Vector2>>();
        foreach (var axis in (Axis[])Enum.GetValues(typeof(Axis)))
        {
            var maps = axisMaps.FindAll((map) => map.axis == axis);
            var axisMap = new Dictionary<KeyCode, Vector2>();
            foreach (var map in maps)
            {
                foreach (var key in map.keys)
                {
                    if (axisMap.ContainsKey(key))
                    {
                        var effect = axisMap[key] + map.action;
                        axisMap[key] = new Vector2(Mathf.Clamp(effect.x, -1, 1), Mathf.Clamp(effect.y, -1, 1));
                    }
                    else
                    {
                        axisMap.Add(key, new Vector2(Math.Clamp(map.action.x, -1, 1), Math.Clamp(map.action.y, -1, 1)));
                    }
                }
            }
            computedAxisMaps.Add(axis, axisMap);
        }
    }

    public static bool GetAction(KeyAction action)
    {
        var keys = InputManager.instance.computedKeyMaps[action];
        return keys.Any(key => Input.GetKey(key));
    }

    public static Vector2 GetAxis(Axis axis)
    {
        var maps = InputManager.instance.computedAxisMaps[axis];

        var output = Vector2.zero;
        foreach (var key in maps.Keys)
        {
            if (Input.GetKey(key))
            {
                output += maps[key];
            }
        }

        return new Vector2(Mathf.Clamp(output.x, -1, 1), Mathf.Clamp(output.y, -1, 1));
    }
}
