// Fades out the Text of the HPParticle and Destroys it 

using UnityEngine;
using System.Collections;
using TMPro;

public class HPParticleScript : MonoBehaviour {

	public float Alpha =1f;
	public float FadeSpeed = 1f;

    public TextMeshPro textMeshPro;

	void FixedUpdate () 
	{
		Alpha = Mathf.Lerp(Alpha,0f,FadeSpeed * Time.deltaTime);

		Color CurrentColor = textMeshPro.color;
        textMeshPro.color = new Color(CurrentColor.r,CurrentColor.g,CurrentColor.b,Alpha);

		if (Alpha < 0.005f)
		{
			Destroy(gameObject);
		}
	}
}
