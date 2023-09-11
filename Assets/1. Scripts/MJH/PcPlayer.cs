using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcPlayer : MonoBehaviour
{

    Animator anim;
    public GameObject player;

    // 플레이어 속도
    [Range(1,10)]
    public float moveSpeed = 5f;

    // 플레이어 점프 가능 횟수
    [SerializeField]
    int jumpCount = 0;
    bool isJump = false;
    public float jumpPower = 10f;

    // 로켓 아이템
    //[SerializeField]
    public int rocketCount = 0;
    public bool isRocket = false;
    public float rocketPower = 20f;


    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 1;
        rocketCount = 2;
        anim = player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }


    void PlayerMove()
    {
        float moveZ = 0f;
        float moveX = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveZ += 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount > 0)
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJump = true;
                anim.SetTrigger("Jump");
                jumpCount--;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(rocketCount > 0 && isRocket == false)
            {
                transform.GetComponent<Rigidbody>().AddForce(Vector3.up * rocketPower, ForceMode.Impulse);
                isRocket = true;
                anim.SetTrigger("Jumping");
                rocketCount--;
            }
        }

        Vector3 dir = new Vector3(moveX, 0, moveZ);
        dir.Normalize();
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        if(moveX != 0 || moveZ != 0)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 20f);
        }

        anim.SetFloat("Horizontal", moveZ);
        anim.SetFloat("Vertical", moveX);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            jumpCount = 1;
            if (isJump == true)
            {
                anim.SetTrigger("Land");
                isJump = false;
            }

            if(isRocket == true)
            {
                anim.SetTrigger("Land");
                isRocket = false;
            }
            else
            {
                anim.SetTrigger("Land");
            }

        }
    }

}
