using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framerate : MonoBehaviour
{
    public float framerate;
    private float i;
    private float o;

	void Update ()
    {
        
        framerate = 1 / Time.deltaTime;
        i = 1 / framerate;
        o = Time.deltaTime;
	}
}
