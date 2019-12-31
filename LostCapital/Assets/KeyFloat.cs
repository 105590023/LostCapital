using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFloat : MonoBehaviour {

    float radian = 0; // 弧度
    float perRadian = 0.03f; // 每次变化的弧度
    float perRotate = 0.5f;  // 每次旋轉度數;
    float radius = 0.3f; // 半径
    public AudioClip _Clip;
    private AudioSource TakeKey;
    Vector3 oldPos; // 开始时候的坐标
                    // Use this for initialization
    void Start()
    {
        oldPos = transform.position; // 将最初的位置保存到oldPos
        TakeKey = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        radian += perRadian; // 弧度每次加0.03
        float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
        transform.position = oldPos + new Vector3(0, dy, 0);
        transform.Rotate(0, +perRotate, 0);
    }

    private void OnTriggerEnter(Collider other)         //人物觸碰到物件
    {
        if (other.name == "Ray" )
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
                r.enabled = false;                      //圖樣消失


            TakeKey.PlayOneShot(_Clip);                 //撥放音樂
            Destroy(gameObject, _Clip.length);           //持續到音樂結束才刪除
        }
    }

    private void OnTriggerExit(Collider other)          //[人物離開物件後，使人物不會二次觸發物件
    {
        if (other.name == "Ray" )
        {
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
