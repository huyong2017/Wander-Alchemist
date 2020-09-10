using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exam : MonoBehaviour
{
    public bool Fire;
    public bool Light;
    public bool Oxygen;
    public int pack=0;
    // Start is called before the first frame update
    void Start()
    {
        Light = false;
    }

    // Update is called once per frame
    void Update()
    {
        Fire = false;
        Oxygen = false;
        if (Input.GetKeyDown(KeyCode.G))
        {
            pack += 1;
            UIManager.instance.ChangePack(pack);
        }
        if (Input.GetKey(KeyCode.F))
        {
            Fire = true;
        }
        if (Input.GetKey(KeyCode.O))
        {
            Oxygen = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Light = !Light;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UIManager.instance.addPower(0, 0.3f);
        }
        UIManager.instance.useFire(Fire);
        UIManager.instance.useLight(Light);
        UIManager.instance.useOxygen(Oxygen);
    }
}
