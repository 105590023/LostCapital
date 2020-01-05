using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour {

    Transform _transform;
    Transform _transformOfPartner;
    bool isOpen;
    public vThirdPersonController cc;
    public GameObject partner;
    string _Object= "";


    private void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _transformOfPartner = partner.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cc.isOpenDoor)
        {

            _Object = cc.RayHit.transform.name;
            if(cc.RayHit.transform.tag == "Door_L"|| cc.RayHit.transform.tag=="Door_R")
                isOpen = true;

            else if ((cc.RayHit.transform.tag == "IronDoor_L" || cc.RayHit.transform.tag == "IronDoor_R") && cc.Keynum == 0)
            {
                Debug.Log("你需要鑰匙!!!!");
            }
            else if((cc.RayHit.transform.tag == "IronDoor_L" || cc.RayHit.transform.tag == "IronDoor_R") && cc.Keynum > 0)
            {
                isOpen = true;
            }
        }
        else if (!cc.isOpenDoor)
        {
            isOpen = false;
        }
        if (isOpen)
        {
            if ((this.transform.tag == "Door_L" || this.transform.tag == "IronDoor_L") && _Object == this.transform.name)
            {
                HitByRay_L();
            }
                    
            else if ((this.transform.tag == "Door_R"||this.transform.tag == "IronDoor_R") && _Object == this.transform.name)
            {
                HitByRay_R();
            }
        }
    }

    private void HitByRay_R()
    {
        if (_transform.localEulerAngles.y <90.0f)
            _transform.Rotate(Vector3.forward * 0.5f);
        if (_transformOfPartner.localEulerAngles.y > 270.0f || _transformOfPartner.localEulerAngles.y == 0)
            _transformOfPartner.Rotate(Vector3.forward * -0.5f);
    }

    private void HitByRay_L()
    {
        if (_transform.localEulerAngles.y>270.0f || _transform.localEulerAngles.y==0)
            _transform.Rotate(Vector3.forward * -0.5f);
        if (_transformOfPartner.localEulerAngles.y < 90.0f)
            _transformOfPartner.Rotate(Vector3.forward * 0.5f);
    }
}
