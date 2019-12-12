using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEvent : MonoBehaviour {

	public void Quit(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
