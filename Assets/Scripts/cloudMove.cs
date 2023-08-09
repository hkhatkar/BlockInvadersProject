using UnityEngine;
using System.Collections;

public class cloudMove : MonoBehaviour {

	public float scrollSpeed = 0.5F;	//The speed the of the texture offset
	
	void Update()
	{
		//Make it smooth
		float offset = Time.time * scrollSpeed;
		
		//Set the texture offset
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
