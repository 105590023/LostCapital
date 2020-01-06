using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightPowerController : MonoBehaviour {
	public float MaxPower;
	public float power;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = new Vector3((-221 + 221 * ((power -= 10 * Time.deltaTime) /MaxPower)), 0.0f, 0.0f);
	}
}
