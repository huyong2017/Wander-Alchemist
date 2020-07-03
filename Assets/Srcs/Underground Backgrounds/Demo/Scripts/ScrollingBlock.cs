using UnityEngine;

public class ScrollingBlock : MonoBehaviour 
{
	public virtual Bounds Bounds
	{
		get 
		{
			return GetComponent<Renderer>().bounds;
		}
	}
    
    public virtual bool CanBeRemoved()
    {
        return Camera.main.WorldToScreenPoint(Bounds.max).x < 0;
    }

}
