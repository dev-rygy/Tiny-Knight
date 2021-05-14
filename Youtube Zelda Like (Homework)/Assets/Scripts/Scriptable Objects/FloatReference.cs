using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization; // used to serialize the class

[System.Serializable] // Make the class serializable so we can see it's variable's in the inspector
public class FloatReference
{
    public bool useConstant = false;
    public float constantValue;
    public FloatVariable variable;

    public float GetValue()
    {
        return useConstant ? constantValue : variable.value;
    }

    public void SubtractValue(float subtrahend)
    {
        if (useConstant == true)
        {
            constantValue -= subtrahend;
        }
        else
        {
            variable.SubtractValue(subtrahend);
        }
    }
}
