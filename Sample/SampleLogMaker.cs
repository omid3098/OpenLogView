using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLogMaker : MonoBehaviour
{
    float counter = 0;
    float timeInterval = 2;
    private void Awake()
    {
        var LogView = GetComponent<OpenLogView.LogView>();
        LogView.ToggleLogView();
        ScheduleLog();
    }

    private static void ScheduleLog()
    {
        Debug.Log("Schedule Info log sample");
        Debug.LogWarning("Schedule Warning log sample");
        Debug.LogError("Schedule Error log sample");
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter > timeInterval)
        {
            counter = 0;
            ScheduleLog();
        }
        
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
