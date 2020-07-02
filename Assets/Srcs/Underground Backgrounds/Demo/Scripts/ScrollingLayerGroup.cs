using UnityEngine;
using System.Collections;

public class ScrollingLayerGroup : MonoBehaviour {

    public ScrollingLayer[] layerRows;

    ///////////////////////////////////////////////////////////////////////
    void Start () {
        for (int i = 1; i < layerRows.Length; i++)
        {
            layerRows[i].transform.position = new Vector3(layerRows[i-1].transform.position.x,
                layerRows[i-1].transform.position.y + layerRows[i-1].Height, layerRows[i-1].transform.position.z);
        }
	}
	
}
