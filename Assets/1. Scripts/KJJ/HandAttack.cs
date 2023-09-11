using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public Transform hand;
    public OVRInput.Controller controller;

    public GameObject wave;

    bool isGrab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 주먹을 쥔 상태
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            isGrab = true;
        }
        // 주먹을 편 상태
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            isGrab = false;
        }
    }

    bool shock = true;
    // 물리충돌계산
    private void FixedUpdate()
    {
        if (isGrab)
        {
            Collider[] cols = Physics.OverlapSphere(hand.transform.position, 0.5f);

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i] != null)
                {
                    if (cols[i].CompareTag("Ground"))
                    {
                        if (shock)
                        {
                           
                           StartCoroutine(ShockWave());
                            shock = false;
                        }
                        //print("쇼크웨이브");
                    }
                }
                print("충돌한 물체 : " + cols[i].gameObject.name);
            }
        }
    }

    IEnumerator ShockWave()
    {
        Ray ray = new Ray(hand.position, hand.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            for (int i = 0; i < 50; i++)
            {
                GameObject shockWave = Instantiate(wave);
                shockWave.transform.localPosition = hitInfo.point + new Vector3(1 * Random.value * 2f, 0, -i);
                shockWave.transform.forward = hand.transform.forward + new Vector3(0,0,3);
                shockWave.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1.8f, 2.8f);
                shockWave.transform.localRotation = Quaternion.Euler(1 * Random.value * 45, 1 * Random.value * 45, 1 * Random.value * 45);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
