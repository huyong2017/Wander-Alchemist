using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;

    public float moveSpeed;//移动速度
    public float jumpForce;//跳跃高度

    public static PlayerController instance;

    private float bornPosX;//出生点的X坐标，用来平移环境图

    

    void Start()
    {
        instance = this;

        bornPosX = this.transform.position.x;
    }

    void FixedUpdate()
    {
        //角色移动
        Movement();

        //环境分层偏移
        EnvironmentBias();
    }

    private void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirectiong = Input.GetAxisRaw("Horizontal");

        //水平移动
        if (horizontalMove != 0)
        {
            rigid.velocity = new Vector2(horizontalMove * moveSpeed * Time.deltaTime, rigid.velocity.y);
        }

        //跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * Time.deltaTime);
        }
    }

    private void EnvironmentBias()
    {

    }
}
