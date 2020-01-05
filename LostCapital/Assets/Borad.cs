using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Borad : MonoBehaviour {
    public vThirdPersonController cc;
    public GameObject BoradUI;

    private void Update()
    {
        ControllBorad();
    }

    public void ControllBorad()
    {
        if (cc.RayHit.transform.name == "Cylinder" && BoradUI.activeSelf==false && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = true;
            BoradUI.SetActive(true);
        }
    }

    public void Onclick()
    {
        Cursor.visible = false;
        BoradUI.SetActive(false);
    }
}
