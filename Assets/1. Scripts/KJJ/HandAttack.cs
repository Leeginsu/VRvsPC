using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviourPun
{
    public Transform hand;
    public OVRInput.Controller controller;

    public GameObject wave;
    //public GameObject carving;

    bool isGrab;

    int waveNum = 20;
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

    public Collider[] cols;
    // 물리충돌계산
    private void FixedUpdate()
    {
        if (!isGrab && shock == false)
        {
            shock = true;
        }
        //print("isGrab"+isGrab);
        if (isGrab)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Ground");

            cols = Physics.OverlapSphere(hand.transform.position, 0.5f, layerMask);

            //print("cols"+ cols);
            for (int i = 0; i < cols.Length; i++)
            {
                //print("colsi" + cols[i]);
                if (cols[i] != null)
                {
                    print(cols);
                    if (cols[i].CompareTag("Ground"))
                    {
                        if (shock)
                        {
                            Vector3 dir = hand.forward;
                            dir.y = 0;
                            StartCoroutine(ShockWave(dir));
                            shock = false;
                        }
                    }
                }
                // print("충돌한 물체 : " + cols[i].gameObject.name);
            }
        }
    }

    IEnumerator ShockWave(Vector3 dir)
    {
        Ray ray = new Ray(hand.position, hand.forward);
        RaycastHit hitInfo;



        if (Physics.Raycast(ray, out hitInfo))
        {

            for (int i = 0; i < waveNum; i++)
            {
                //print("쇼크웨이브!");
                GameObject shockWave = PhotonNetwork.Instantiate("Wave", new Vector3(0, 0, 0), Quaternion.identity);
                //GameObject shockWave = Instantiate(wave);
                Vector3 fx = hitInfo.point + ((dir * (i * 4f)) + new Vector3(1 * Random.value * 3f, 0, 0));
                shockWave.transform.position = new Vector3(0, hitInfo.point.y, 0) + fx;
                shockWave.transform.localScale = new Vector3(2, 2, 2) * Random.Range(1.0f, 2.0f);
                shockWave.transform.localRotation = Quaternion.Euler(1 * Random.value * 45, 1 * Random.value * 45, 1 * Random.value * 45);
                PhotonView waveDestroy = shockWave.transform.GetComponent<PhotonView>();

                StartCoroutine(DestroyWave(shockWave, 1f));
                //Destroy(shockWave.gameObject, 1f);

                for (int j = 0; j < 2; j++)
                {
                    GameObject carving = PhotonNetwork.Instantiate("Carving", new Vector3(0, 0, 0), Quaternion.identity);
                    carving.transform.position = new Vector3(0, hitInfo.point.y, 0) + fx;
                    carving.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f) * Random.Range(1.0f, 2.0f);
                    carving.transform.localRotation = Quaternion.Euler(1 * Random.value * 45, 1 * Random.value * 45, 1 * Random.value * 45);
                    Destroy(carving.gameObject, 0.7f);
                }

                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    IEnumerator DestroyWave(GameObject target, float time)
    {
        yield return new WaitForSeconds(time);

        PhotonNetwork.Destroy(target);
    }
}
