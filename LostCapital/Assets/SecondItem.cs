using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondItem : MonoBehaviour
{

    public vThirdPersonController cc;
    Image myImage;
    public Sprite Potion;
    public Sprite Lily;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (cc.Second_Prop == "enhance_potion")
        {
            Debug.Log("我的第二個圖案是" + cc.Second_Prop);
            myImage.sprite = Potion;
        }
        else if (cc.Second_Prop == "White_Lily")
        {
            Debug.Log("我的第二個圖案是" + cc.Second_Prop);
            myImage.sprite = Lily;
        }
        else
        {
            myImage.sprite = null;
        }

    }
}