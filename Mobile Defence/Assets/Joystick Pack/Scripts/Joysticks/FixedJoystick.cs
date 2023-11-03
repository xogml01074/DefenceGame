using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FixedJoystick : Joystick
{
    private void Awake()
    {
#if !UNITY_ANDROID
        gameObject.SetActive(false);
#else
        gameObject.SetActive(true);
#endif
    }
}