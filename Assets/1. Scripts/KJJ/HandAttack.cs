using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public Transform hand;
    public OVRInput.Controller controller;

    public GameObject wave;
    //public GameObject carving;

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
        print("isGrab"+isGrab);
        if (isGrab)
        {
            //int layerMask = 1 << LayerMask.NameToLayer("Ground");

            Collider[] cols = Physics.OverlapSphere(hand.transform.position,0.5f);

            print("cols"+ cols);
            for (int i = 0; i < cols.Length; i++)
            {
                print("colsi" + cols[i]);
                if (cols[i] != null)
                {
                    if (cols[i].CompareTag("Ground"))
                    {
                        if (shock)
                        {
                            StartCoroutine(ShockWave());
                            shock = false;
                        }
                    }
                }
                print("충돌한 물체 : " + cols[i].gameObject.name);
            }
        }
    }

    IEnumerator ShockWave()
    {
        print("6");
        Ray ray = new Ray(hand.position, hand.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            for (int i = 0; i < 50; i++)
            {
                GameObject shockWave = Instantiate(wave);
                shockWave.transform.localPosition = hitInfo.point + new Vector3(1 * Random.value * 2f, 0, -i);
                //shockWave.transform.forward = hand.transform.forward + new Vector3(0,0,3);
                shockWave.transform.localScale = new Vector3(1, 1, 1) * Random.Range(1.8f, 2.8f);
                shockWave.transform.localRotation = Quaternion.Euler(1 * Random.value * 45, 1 * Random.value * 45, 1 * Random.value * 45);
                Destroy(shockWave.gameObject, 2);
                //for (int j = 0; j < 10; j++)
                //{
                //    GameObject carvingStone = Instantiate(carving);
                //    carvingStone.transform.localPosition = hitInfo.point + new Vector3(1, 0, -i);
                //    carvingStone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * Random.Range(0.5f, 2f);
                //    carvingStone.transform.localRotation = Quaternion.Euler(0, 0, 1 * Random.value * 130);
                //    Destroy(carvingStone.gameObject, 2);
                //}
                yield return new WaitForSeconds(0.01f);
            }


        }
    }
}
