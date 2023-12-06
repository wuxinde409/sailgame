using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;
    public float laneDistance = 4;//這條路的距離
    public float jumpForce;//跳的高度
    public float gravity = -20;
    public float maxJumpHeight = 1.0f;
    public float delayBeforeExit=2.0f;
    public float maxSpeed;

    // -1 0 1
    private int _dir;

    private float initialY; // 記錄跳躍起始點的 Y 軸位置

    private void OnEnable()
    {
        SerialReader.Instance.OnDataReceived += OnDataReceived;
    }

    private void OnDisable()
    {
        SerialReader.Instance.OnDataReceived -= OnDataReceived;
    }

    void Start()// 物件出現運行
    {
        controller = GetComponent<CharacterController>(); //拿到外面controller物件
        initialY = transform.position.y; // 在 Start 函數中記錄起始點的 Y 軸位置
    }

    void Update()//
    {
        if (forwardSpeed< maxSpeed)
        {
            
        }
        forwardSpeed+=0.2f * Time.deltaTime;
        direction.z = forwardSpeed;
        direction.y += gravity * Time.deltaTime;
        //CheckCollisionRaycast();
        

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsBelowMaxJumpHeight())
        {
            Jump();
        }

        if (_dir == 1)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;//讓它往右不會超出地圖
        }

        else if (_dir == -1)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;//讓它往左不會超出地圖
        }
        else
        {
            desiredLane = 1;
        }
        

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        transform.position = targetPosition;
        controller.center = controller.center;
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (direction.y < jumpForce)
        {
            direction.y = jumpForce;
        }
    }

    private bool IsBelowMaxJumpHeight()
    {
        return transform.position.y - initialY < maxJumpHeight;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) //假設player 撞到 有TAG  "Obstacle"  的物體
    {
        if (hit.transform.tag=="Obstacle")
        {
            PlayerManger.gameOver = true;
        }
    }
    private void OnDataReceived(float data)
    {
        if(data > 7f)
            _dir = 1;
        else if(data < -7f)
            _dir = -1;
        else
            _dir = 0;
    }

}

