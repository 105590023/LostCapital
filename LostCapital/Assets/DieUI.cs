using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieUI : MonoBehaviour {

    public vThirdPersonController cc;
    public GameObject DieUi;
    public GameObject leftdown;

    private void Update()
    {
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
