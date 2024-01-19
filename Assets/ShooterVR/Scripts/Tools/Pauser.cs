using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = false;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Time.timeScale = hasFocus ? 1 : 0;
    }
}
