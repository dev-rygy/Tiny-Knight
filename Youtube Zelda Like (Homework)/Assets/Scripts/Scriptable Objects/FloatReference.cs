using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization; // used to serialize the class
using UnityEditor;

[System.Serializable]
public class FloatReference
{
    public bool useConstant = false;
    public float constantValue;
    public FloatVariable variable;

    public float Value()
    {
        return useConstant ? constantValue : variable.value;
    }
}
