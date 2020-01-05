using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public vThirdPersonController cc;
    public GameObject DieUi;
    public GameObject leftdown;
    public GameObject PassUi;
    private float PassTime = 5.0f;
    private bool isPass;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name=="Ray")
        {
            PassUi.SetActive(true);
            leftdown.SetActive(false);
            isPass = true;
        }
    }

    private void Update()
    {
        if (isPass)
        {
            PassTime -= Time.deltaTime;
            if (PassTime < 0)
            {
                SceneManager.LoadScene(0);
                Cursor.visible = true;
            }
        }
        if (cc.isDead)
        {
            Cursor.visible = true;
            DieUi.SetActive(true);
            leftdown.SetActive(false);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(2);
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
