using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{

    Rect fpsRect;
    GUIStyle style;
    float fps;

    // Use this for initialization
    void Start () {
        fpsRect = new Rect(100, 100, 400, 100);
        style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.white;

        StartCoroutine("FPSroutine");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.Label(fpsRect, "FPS: " + fps, style);
    }

    IEnumerator FPSroutine()
    {
        while (true)
        {
            fps = 1 / Time.deltaTime;
            

            yield return new WaitForSeconds(1);
        }
    }
}
