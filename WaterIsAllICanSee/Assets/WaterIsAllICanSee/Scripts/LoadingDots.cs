using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingDots : MonoBehaviour
{
    [SerializeField] float timeToChangeDots = 1f;
    int currentDots = 3;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("LoadingDot", 0, timeToChangeDots);
    }

    void LoadingDot()
    {
        if (currentDots == 3)
        {
            currentDots = 0;
            text.text = "Loading";
        }
        else
        {
            currentDots += 1;
            text.text = text.text + ".";
        }

    }
}
