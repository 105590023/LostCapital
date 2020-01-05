using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadBeliever_AI : MonoBehaviour {
    private Animator ani;
    public float sp = 1f;
    public float Move_Distence = 5;
    float nsp;
    public GameObject enemy;
    //public Rigidbody Rb;
    float tx, ty;
    int state = 0;

    // Use this for initialization
    void Start()
    {
        ani = this.GetComponent<Animator>();
        nsp = sp;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("Speed", nsp);
        SetState();
        Vector3 TD = enemy.transform.position - transform.position;
        Vector3 nTD = Vector3.RotateTowards(transform.forward, TD, 3*Time.deltaTime, 0);
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
            transform.Translate(new Vector3(0, 0, nsp * Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(nTD);
            if (nsp < 3f) nsp = nsp + 0.3f * Time.deltaTime;
            print(nsp);
        }
        if (state == 2) //Attack
        {
            ani.SetBool("IsAttack", true);
            ani.SetBool("IsMove", false);
            ani.SetTrigger("Attack");
            nsp = sp;
        }
    }

    void SetState()
    {
        float dis = Distence();
        if (dis < Move_Distence && !(ani.GetCurrentAnimatorStateInfo(0).IsName("Standing_Attack") || ani.GetCurrentAnimatorStateInfo(0).IsName("Dash_Attack")))
        {
            state = 1;
        }
        else state = 0;
        if (dis < 1.3f)
        {
            state = 2;
            print("state = 2");
        }

    }

    float Distence()
    {
        float dis, x, y;
        dis = 0;
        x = enemy.GetComponent<Transform>().position.x - transform.position.x;
        y = enemy.GetComponent<Transform>().position.z - transform.position.z;
        dis = Mathf.Pow(Mathf.Pow(x, 2) + Mathf.Pow(y, 2), 0.5f);
        //print(dis);
        return dis;
    }
}

