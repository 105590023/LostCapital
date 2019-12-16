using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_Camera : MonoBehaviour {

    private Ray ray;
    RaycastHit hit;
    private float rayLength= 5.0f;
    public Camera _camera;


    private void Update()
    {
        ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


        if (Physics.Raycast(ray, out hit, rayLength))
        {
            hit.transform.SendMessage("HitByRaycast", gameObject, SendMessageOptions.DontRequireReceiver);
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            print(hit.transform.name);
        }
    }
}
