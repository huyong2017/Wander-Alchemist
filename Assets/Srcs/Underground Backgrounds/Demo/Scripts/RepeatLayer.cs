using UnityEngine;
using System.Collections.Generic;

public class RepeatLayer : ScrollingLayer {

	public ScrollingBlock[] blocks;

	private List<int> orderedBlocks;

    public override float Height
    {
        get
        {
            return blocks[0].Bounds.size.y;
        }
    }

    ///////////////////////////////////////////////////////////////////////
    protected override void Start () {
        base.Start();
		orderedBlocks = new List<int>();
		for (int i=0; i<blocks.Length; i++) 
		{
			orderedBlocks.Add(i);
		}

		for (int i=1; i<blocks.Length; i++)
		{
            blocks[i].transform.localPosition = new Vector3(blocks[i - 1].transform.localPosition.x + blocks[i - 1].GetComponent<Renderer>().bounds.extents.x + blocks[i].GetComponent<Renderer>().bounds.extents.x,
                                                  blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);
		}
	}

	///////////////////////////////////////////////////////////////////////
	protected override void Update () {
        base.Update();

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.localPosition = new Vector3(blocks[i].transform.localPosition.x - deltaTime * currentSpeed,
                                                  blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);
        }

		for (int i=0; i<blocks.Length; i++)
		{
			if (blocks[i].CanBeRemoved())
			{
                if (orderedBlocks.Count > 0)
                {
                    orderedBlocks.RemoveAt(0);
                }

                float x = blocks[orderedBlocks[orderedBlocks.Count - 1]].GetComponent<Renderer>().bounds.max.x + blocks[i].GetComponent<Renderer>().bounds.extents.x;

                blocks[i].transform.localPosition = new Vector3(x - transform.position.x, blocks[i].transform.localPosition.y, blocks[i].transform.localPosition.z);

                orderedBlocks.Add(i);
			}
		}
	}

}
