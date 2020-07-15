using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIButtonLayout : MonoBehaviour
{
    public UIPanel panel;

    private GComponent ui;
    private Transition tranStart;

    // Start is called before the first frame update
    void Start()
    {
        GComponent ui = panel.ui;
        tranStart = ui.GetTransition("start");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            tranStart.Play();
        }
    }
}
