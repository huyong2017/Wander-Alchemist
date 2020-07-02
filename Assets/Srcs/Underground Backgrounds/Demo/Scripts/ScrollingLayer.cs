using UnityEngine;
using System.Collections;

public abstract class ScrollingLayer : MonoBehaviour {

    public float speed;

    public float Distance { get; private set; }

    protected float currentSpeed;
    protected float deltaTime;


    public abstract float Height { get; }

    ///////////////////////////////////////////////////////////////////////
    protected virtual void Start()
    {
        Distance = 0;

        currentSpeed = speed;
    }

    ///////////////////////////////////////////////////////////////////////
    protected virtual void Update()
    {
        deltaTime = Time.smoothDeltaTime;

        Distance += deltaTime * currentSpeed;
    }

	
}
