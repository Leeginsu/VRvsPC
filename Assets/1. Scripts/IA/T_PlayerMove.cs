using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        transform.position += dir * 50 * Time.deltaTime;
    }
}
