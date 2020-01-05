using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_Enbale : MonoBehaviour {
    public GameObject Target;
    public GameObject MB;
    public float ddis = 10;
    float dis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        dis = Vector3.Distance(Target.GetComponent<Transform>().position, transform.position);
        print(dis);
        if (dis < ddis) MB.SetActive(true);
        else MB.SetActive(false);
    }

}
