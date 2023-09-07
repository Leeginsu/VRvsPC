using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IsJumping()
    {
        GetComponentInParent<PcPlayer>().isRocket = false;
        //GetComponent<Animator>().SetTrigger("Land");
        print("로켓 점프 끝");
    }
}
