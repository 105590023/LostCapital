using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Vulture_AI : MonoBehaviour {

    private Animator ani;
    public float sp = 1f;
    public float Move_Distence = 15;
    public float Shooting_Distence = 8;
    public float Arrow_Distence = 15;
    public GameObject enemy;
    public GameObject Arrow;
    //float tx, ty;
    int state = 0;
    bool s = true;


    // Use this for initialization
    void Start () {
        ani = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        SetState();
        Vector3 TD = enemy.transform.position - transform.position;
        Vector3 nTD = Vector3.RotateTowards(transform.forward, TD, 10* Time.deltaTime, 0);
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Aim"))
        {
            nTD = Vector3.RotateTowards(transform.forward, TD, Time.deltaTime, 0);
        }
            nTD.Set(nTD.x, 0, nTD.z);

        if (state == 0)
        {
            ani.SetBool("IsMove", false);
            ani.SetBool("IsAttack", false);
        }

        if (state == 1) //Move
        {
            ani.SetBool("IsMove", true);
            ani.SetBool("IsAttack", false);
            transform.Translate(new Vector3(0, 0, sp * Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(nTD);
        }
        if (state == 2) //Attack
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") || ani.GetCurrentAnimatorStateInfo(0).IsName("Aim"))
            {
                transform.rotation = Quaternion.LookRotation(nTD);
                Arrow.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }
            ani.SetBool("IsAttack", true);
            ani.SetBool("IsMove", false);
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Shoot")) Shoot();
            else
            {
                Arrow.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                Arrow.SetActive(false);
            }
        }
    }

    void SetState()
    {
        float dis = Vector3.Distance(enemy.GetComponent<Transform>().position, transform.position);
        if (dis < Move_Distence && (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") || ani.GetCurrentAnimatorStateInfo(0).IsName("Walk")))
        {
            state = 1;
        }
        else state = 0;
        if (dis < Shooting_Distence)
        {
            state = 2;
        }

    }

    void Shoot()
    {
        if(Vector3.Distance(Arrow.transform.position, transform.position) < Arrow_Distence)
            {
                Arrow.SetActive(true);
                Arrow.transform.Translate(new Vector3(0,0,40f) * Time.deltaTime);
            }

    }
}
