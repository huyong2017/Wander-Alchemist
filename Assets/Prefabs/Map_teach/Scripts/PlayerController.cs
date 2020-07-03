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

    private Vector3[] bornPos = new Vector3[7];//出生时候背景的坐标，用来平移环境图
    public GameObject Camera;
    private float initPos;//摄像机的初始x位置

    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;

    //背景层次
    public Transform[] EnvirLayer;

    void Start()
    {
        instance = this;

        for (int i = 0; i < EnvirLayer.Length; i++)
        {
            bornPos[i] = EnvirLayer[i].position;
        }
        initPos = Camera.transform.position.x;

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

    // 按照背景的不同层次增添一个水平偏移,共7个层次
    // 01-大背景，随主角移动 其次的一次减少偏移量，最后一层要反向偏移，0506不偏倚
    // 02-远山，03-云层与天空建筑，04-近处的偏移物，05-路左边的物体，06-路右边和路上的物体 07-近景的遮挡物
    float[] biasLength = new float[7] { 0.9f, 0.85f, 0.75f, 0.2f, 0, 0, -0.2f };
    private void EnvironmentBias()
    {
        float biasCamera = Camera.transform.position.x - initPos;
        //Debug.Log(bornPosX[0] + biasCamera);
        for (int i = 0; i < EnvirLayer.Length; i++)
        {
            EnvirLayer[i].transform.position = new Vector3((bornPos[i].x + biasCamera) * biasLength[i], bornPos[i].y, bornPos[i].z);
        }
        
    }

    
}
