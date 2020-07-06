using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("移动")]
    [SerializeField] float WalkSpeed;
    [SerializeField] float AccelerateTime;
    [SerializeField] float DecelerateTime;
    [SerializeField] Vector2 InputOffset;
    [SerializeField] bool CanMove = true;

    [Header("跳跃")]
    [SerializeField] float JumpingSpeed;
    [SerializeField] float FallMultiplier;
    [SerializeField] float LowJumpMultiplier;
    [SerializeField] bool CanJump = true;


    [Header("触地判定")]
    [SerializeField] private Vector2 PointOffset;
    [SerializeField] private Vector2 Size;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] bool GravityModifier = true;


    Vector2 dir;

    Rigidbody2D Rig;
    Animator Anim;

    [SerializeField] bool isMoving;
    [SerializeField] bool isOnGround;
    [SerializeField] float velocityX;
    [SerializeField] bool isJumping;
    [SerializeField] bool GravitySwitch = true;

    [SerializeField] bool isAbleToCtrl = true;
    [SerializeField] bool isDead = false;

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

    void Awake()
    {
        Rig = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Rig.velocity = Vector2.zero;
    }


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
        //if (Input.GetButtonDown("Jump") && jumpCount > 0)
        //{
        //    jumpPressed = true;
        //}
    }

    void FixedUpdate()
    {
        ////角色左右移动
        //Movement();

        ////跳跃
        ////判断是否在地面
        //isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        //Jump();


        if (isAbleToCtrl)
        {

            isOnGround = OnGround();

            #region 移动
            if (CanMove)
            {
                if (Input.GetAxisRaw("Horizontal") > InputOffset.x)
                {
                    if (Rig.velocity.x < WalkSpeed * Time.fixedDeltaTime * 60)
                        Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x,
                            WalkSpeed * Time.fixedDeltaTime * 60,
                            ref velocityX, AccelerateTime), Rig.velocity.y);
                    isMoving = true;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    Anim.SetBool("run", true);
                }

                else if (Input.GetAxisRaw("Horizontal") < InputOffset.x * -1)
                {
                    if (Rig.velocity.x > WalkSpeed * Time.fixedDeltaTime * 60 * -1)
                        Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x,
                            WalkSpeed * Time.fixedDeltaTime * 60 * -1,
                            ref velocityX, AccelerateTime), Rig.velocity.y);
                    isMoving = true;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    Anim.SetBool("run", true);
                }

                else
                {
                    isMoving = false;
                    Rig.velocity = new Vector2(Mathf.SmoothDamp(Rig.velocity.x,
                        0, ref velocityX,
                        DecelerateTime), Rig.velocity.y);
                    if (Rig.velocity.x <= 0.01f)
                    {
                        Rig.velocity = new Vector2(0, Rig.velocity.y);
                    }
                    Anim.SetBool("run", false);
                }
            }
            #endregion

            #region 跳跃
            if (CanJump)
            {
                if (Input.GetAxisRaw("Jump") == 1 && !isJumping)
                {
                    Rig.velocity = new Vector2(Rig.velocity.x, JumpingSpeed);
                    isJumping = true;
                    Anim.SetTrigger("takeof");
                }

                if (isOnGround && Input.GetAxisRaw("Jump") == 0)
                {
                    isJumping = false;

                }
                if (isOnGround)
                {
                    Anim.SetBool("jump", false);
                    Anim.SetBool("jumpup", false);
                    Anim.SetBool("jumpdown", false);
                }
                else
                {
                    Anim.SetBool("jump", true);
                }
            }
            #endregion

            #region 重力控制器
            if (GravityModifier)
            {
                if (Rig.velocity.y < 0)//玩家下坠
                {
                    Anim.SetBool("jumpdown", true);
                    Rig.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.fixedDeltaTime;//加速下坠
                }

                //if (prop1equip)
                //{
                //if (Rig.velocity.y > 0 && Input.GetAxis("Jump") != 1)//玩家上升，并且没有按下跳跃键
                //{
                //    Anim.SetBool("jumpup", true);
                //    Rig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.fixedDeltaTime;//减缓上升
                //}
                //}
                //else
                //{
                if (Rig.velocity.y > 0)
                {
                    Anim.SetBool("jumpup", true);
                }

                Rig.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.fixedDeltaTime;//减缓上升
                                                                                                                 //}

            }
            #endregion

            //#region 冲刺
            //if (prop2equip)
            //{
            //    if (Input.GetAxisRaw("Dash") == 1 && !WasDashed)
            //    {
            //        WasDashed = true;
            //        dir = GetDir();
            //        StartCoroutine(Dash());//使用
            //                               //将玩家当前所有动量清零
            //        Rig.velocity = Vector2.zero;
            //        //施加一个力，让玩家飞出去
            //        Rig.velocity += dir.normalized * DragForce;

            //    }

            //    if (isOnGround && Input.GetAxisRaw("Dash") == 0)
            //    {
            //        WasDashed = false;
            //    }
            //}
            //#endregion
        }

        //环境分层偏移
        EnvironmentBias();
    }


    public Vector2 GetDir()
    {
        Vector2 tempDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (tempDir.x == 0 && tempDir.y == 0)
        {
            tempDir.x = transform.eulerAngles.y == 0 ? 1 : -1;
        }

        return tempDir;
    }

    bool OnGround()
    {
        Collider2D Coll = Physics2D.OverlapBox((Vector2)transform.position + PointOffset, Size, 0, GroundLayerMask);
        bool isbool = Coll != null ? true : false;
        return isbool;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + PointOffset, Size);
    }







    void Death()
    {
        Vector3 deathPoint = transform.position;

        //重置
        CanMove = true;
        CanJump = true;
        GravityModifier = true;
        Rig.gravityScale = 1;
        //Anim.SetBool("isjump", false);
        Anim.SetBool("isrunning", false);
        Rig.velocity = Vector2.zero;
        //设置位置

        //Vector3 rebrithPoint = GameCtrl.gc.GetRebrithPoint(deathPoint);
        //GameCtrl.gc.Warp(transform, rebrithPoint);

        isAbleToCtrl = true;
        isDead = false;


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
    float[] biasLength = new float[8] { 0.9f, 0.65f, 0.35f, 0.2f, 0.1f, 0, 0, -0.2f };
    private void EnvironmentBias()
    {
        float biasCamera = Camera.transform.position.x - initPos;
        //Debug.Log(bornPosX[0] + biasCamera);
        for (int i = 0; i < EnvirLayer.Length; i++)
        {
            EnvirLayer[i].transform.position = new Vector3(bornPos[i].x + biasCamera * biasLength[i], bornPos[i].y, bornPos[i].z);
        }
        
    }

    
}
