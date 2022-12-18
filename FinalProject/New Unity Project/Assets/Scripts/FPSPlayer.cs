using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{ 
    public Gun cGun;
    public Gun[] gun;

    int gunIndex = 0;

    public int lockGun = 1;

    Rigidbody character;
    CapsuleCollider controller;
    //摄像机
	Camera camera;

    public LayerMask layerMask;

    public bool IsGrounded;
    public float GroundCheckDistance = 0.05f;
    //移动速度
	public float jumpSpeed = 200;
	//移动速度
	public float speed = 5;
    public float RotationSpeed = 2f;
    float m_CameraVerticalAngle = 0f;
    bool m_Jump;
	void Start () 
	{
		//获取摄像机
		camera = this.GetComponentInChildren<Camera>();
        character = this.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        controller = this.GetComponent<CapsuleCollider>();

        for (int i = 0; i < this.gun.Length; i++)
        {
            this.gun[i].gameObject.SetActive(false);
        }
        this.cGun = this.gun[this.gunIndex];
        this.cGun.gameObject.SetActive(true);
	}

    public void gameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        this.enabled = false;
    }

    Vector3 GetCapsuleBottomHemisphere()
    {
        return transform.position + (transform.up * controller.radius);
    }

    Vector3 GetCapsuleTopHemisphere()
    {
        return transform.position + (transform.up * controller.height);
    }
	
	// Update is called once per frame
	void Update ()
	{
         //自己朝向
        transform.Rotate(new Vector3(0f,Input.GetAxis("Mouse X")*RotationSpeed,0f), Space.Self);
        //摄像机的上下朝向
        m_CameraVerticalAngle -= Input.GetAxis("Mouse Y") * RotationSpeed;
        m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -89f, 89f);
        camera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);

        if (Input.GetButtonDown("Jump") && !m_Jump)
        {
            m_Jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && this.gunIndex != 0 )
        {
           this.changeGun(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && this.gunIndex != 1 && this.lockGun > 1)
        {
           this.changeGun(1);
        }
    }

    void changeGun(int index)
    {
        this.gunIndex = index;
        this.cGun.gameObject.SetActive(false);
        this.cGun = this.gun[this.gunIndex];
        this.cGun.gameObject.SetActive(true);
        this.cGun.transform.Translate(new Vector3(0,-1f,0),Space.Self);
    }

    void FixedUpdate()
    {
        
        //检测地板
        IsGrounded = false;
        if(Physics.CapsuleCast(GetCapsuleBottomHemisphere(),GetCapsuleTopHemisphere(),controller.radius,Vector3.down,out RaycastHit hit,GroundCheckDistance,layerMask,QueryTriggerInteraction.Ignore))
        {
            IsGrounded = true;
        }

		//获取左右移动控制
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

        //转换为自己的朝向
        Vector3 input = this.transform.TransformVector(new Vector3(h,0,v));

		//移动
		this.character.MovePosition(this.character.position+input*speed*Time.fixedDeltaTime);
        
        if( IsGrounded && m_Jump)
        {
           this.character.AddForce(Vector3.up*this.jumpSpeed);
        }

        m_Jump = false;
    }
}