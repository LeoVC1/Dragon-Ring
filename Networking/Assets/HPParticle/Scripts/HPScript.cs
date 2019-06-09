// this script controls the HP and Instantiates an HP Particle

using UnityEngine;
using System.Collections;
using TMPro;

public class HPScript : MonoBehaviour {

	//the current HP of the character/gameobject
	public float HP = 100;
    public float MaxHP = 100;

    //the HP Particle
    public GameObject HPParticle;

	//Default Forces
	public Vector3 DefaultForce = new Vector3(0f,1f,0f);
	public float DefaultForceScatter = 0.5f;

    private AvatarSetup avatarSetup;

    private void Start()
    {
        avatarSetup = GetComponent<AvatarSetup>();
    }

    //Change the HP without an effect
    public void ChangeHPValue(float Delta)
	{
		HP = HP + Delta;
        avatarSetup.ChangeHP(HP);
    }

    public void ChangeMaxHPValue(float Delta)
    {
        MaxHP += Delta;
        avatarSetup.ChangeMaxHP(MaxHP);
    }

    //Change the HP and Instantiates an HP Particle with a Custom Force and Color
    public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter, Color ThisColor)
    { 
        ChangeHPValue(Delta);


        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;

        if (Delta > 0)
		{
			TM.text = "+" + Delta.ToString();
		}
		else
		{
			TM.text = Delta.ToString();
		}

		TM.color =  ThisColor;

		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with a Custom Force
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;

        if (Delta > 0f)
		{
			TM.text = "+" + Delta.ToString();
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.text = Delta.ToString();
			TM.color =  new Color(1f,0f,0f,1f);
		}
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x/* + Random.Range(-ForceScatter,ForceScatter)*/,Force.y + Random.Range(ForceScatter / 1.5f, ForceScatter),Force.z/* + Random.Range(-ForceScatter,ForceScatter)*/));
	}

	//Change the HP and Instantiates an HP Particle with a Custom Color
	public void ChangeHP(float Delta,Vector3 Position, Color ThisColor)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;

        if (Delta > 0)
		{
			TM.text = "+" + Delta.ToString();
		}
		else
		{
			TM.text = Delta.ToString();
		}

		TM.color =  ThisColor;
		
		NewHPP.GetComponent<Rigidbody>().AddForce(new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with default force and color
	public void ChangeHP(float Delta,Vector3 Position)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;

        if (Delta > 0f)
		{
			TM.text = "+" + Delta.ToString();
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.text = Delta.ToString();
			TM.color =  new Color(1f,0f,0f,1f);
		}

		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text
	public void ChangeHP(float Delta,Vector3 Position, string text)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;
        TM.text = text;
		
		if (Delta > 0f)
		{
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.color =  new Color(1f,0f,0f,1f);
		}
		
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text and Force,
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter, string text)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;
        TM.text = text;
		
		if (Delta > 0f)
		{
			TM.color =  new Color(0f,1f,0f,1f);
		}
		else
		{
			TM.color =  new Color(1f,0f,0f,1f);
		}
		
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text, Force and Color
	public void ChangeHP(float Delta,Vector3 Position, Vector3 Force, float ForceScatter, Color ThisColor, string text)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;
        TM.text = text;
		TM.color =  ThisColor;

		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(Force.x + Random.Range(-ForceScatter,ForceScatter),Force.y + Random.Range(-ForceScatter,ForceScatter),Force.z + Random.Range(-ForceScatter,ForceScatter)));
	}

	//Change the HP and Instantiates an HP Particle with Custom Text and Color
	public void ChangeHP(float Delta,Vector3 Position, Color ThisColor, string text)
	{
        ChangeHPValue(Delta);

        GameObject NewHPP = Instantiate(HPParticle,Position,gameObject.transform.rotation) as GameObject;
        NewHPP.GetComponent<AlwaysFace>().Target = GetComponent<AvatarSetup>().myCamera.gameObject;

        TextMeshPro TM = NewHPP.GetComponent<HPParticleScript>().textMeshPro;
        TM.text = text;
		TM.color =  ThisColor;
		
		NewHPP.GetComponent<Rigidbody>().AddForce( new Vector3(DefaultForce.x + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.y + Random.Range(-DefaultForceScatter,DefaultForceScatter),DefaultForce.z + Random.Range(-DefaultForceScatter,DefaultForceScatter)));
	}
	
}
