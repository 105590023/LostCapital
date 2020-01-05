using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KEYCount : MonoBehaviour {
    public vThirdPersonController cc;
    private void Update()
    {
        GetComponent<Text>().text = "X "+cc.Keynum;
    }

}
