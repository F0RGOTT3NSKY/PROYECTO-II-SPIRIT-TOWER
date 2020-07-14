using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object

/*
 ISerializationCallbackReceiver:

Locks the value given in the inspector so it will not change during the game
*/

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    //Variable creation
    public float InitialValue;  // Initial value

    [HideInInspector]
    public float RunTimeValue;  // Current value of something (Doesnt appear in the object inspector)

    public void OnAfterDeserialize()
    {
        RunTimeValue = InitialValue;
    }

    public void OnBeforeSerialize(){}
}
