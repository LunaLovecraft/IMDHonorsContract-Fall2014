//Adam Kaufman
using UnityEngine;
using System.Collections;

//Allows for a sprite to fade in/out depending on whether or 
//not a player has collided with it
public class SeeThroughSprites : AbstractTriggerZone {

	//The sprite thay will disappear when the player enters its zone
	public SpriteRenderer Overlay;
	public SpriteRenderer Crack;

	bool isIn = false;

	public override void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject == GameObject.Find ("Player")) 
		{
			isIn = true;
			Overlay.material.color = new Color (Overlay.material.color.r, Overlay.material.color.g, Overlay.material.color.b, Mathf.Lerp (Overlay.material.color.a, 0, Time.deltaTime * 2));
			//Overlay.enabled = false;

			if(Crack != null)
				Crack.material.color = new Color (Crack.material.color.r, Crack.material.color.g, Crack.material.color.b, Mathf.Lerp (Crack.material.color.a, 0, Time.deltaTime * 2));

		}

	}

	public override void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject == GameObject.Find ("Player")) 
		{
			//Overlay.enabled = true;
			isIn = false;
		}
	}

	public override void OnTriggerEnter2D(Collider2D other)
	{

	}

	public void Update()
	{
		if (!isIn)
		{
			Overlay.material.color = new Color (Overlay.material.color.r, Overlay.material.color.g, Overlay.material.color.b, Mathf.Lerp (Overlay.material.color.a, 1, Time.deltaTime * 2));

			if(Crack != null)
				Crack.material.color = new Color (Crack.material.color.r, Crack.material.color.g, Crack.material.color.b, Mathf.Lerp (Crack.material.color.a, 1, Time.deltaTime * 2));

		}
	}
}
