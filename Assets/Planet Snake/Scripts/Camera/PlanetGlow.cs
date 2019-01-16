using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGlow : MonoBehaviour {
public Color brightSide = Color.green;
public Color darkSide = Color.red;
public MeshRenderer meshRenderer;
public Transform sun;
MaterialPropertyBlock matProperty;

	void Start () 
	{
		matProperty = new MaterialPropertyBlock();
	}
	
	void Update () 
	{
		transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(90, 0, 0);

		
		float dot = Vector3.Dot(transform.up, sun.forward); //Vector3.Dot vector direction similarity comparison 
		dot = (dot + 1) * 0.5f; //Bring the dot from -1 to 1, 0 to 1
		

		Color finalColor = Color.Lerp(darkSide, brightSide, dot);

		matProperty.SetColor("_TintColor", finalColor);
		meshRenderer.SetPropertyBlock(matProperty);
	}
}
