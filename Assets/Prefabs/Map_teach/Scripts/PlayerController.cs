using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Collider2D clid;

    public float moveSpeed;//移动速度
    public float jumpForce;//跳跃高度
    public int jumpTimes;//可以连续跳跃的次数

    public static PlayerController instance;

    private float bornPosX;//出生点的X坐标，用来平移环境图

    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;

    void Start()
    {
        instance = this;

        bornPosX = this.transform.position.x;

        clid = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        //角色左右移动
        Movement();

        //跳跃
        //判断是否在地面
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        Jump();

        //环境分层偏移
        EnvironmentBias();
    }

    private void Movement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(horizontalMove * moveSpeed, rigid.velocity.y);

        //左右翻转
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            jumpCount = jumpTimes;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        //二段跳,在空中时这样
        else if (jumpPressed && jumpCount >0 && !isGround)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    private void EnvironmentBias()
    {

    }
}
