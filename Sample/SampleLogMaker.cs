using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLogMaker : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Info log sample");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogWarning("Warning log sample");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogError("Error log sample");
        }
    }
}
