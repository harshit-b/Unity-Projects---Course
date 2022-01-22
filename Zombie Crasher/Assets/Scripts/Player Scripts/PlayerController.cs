using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    private Rigidbody myBody;
    // Start is called before the first frame update
    public Transform bullet_StartPoint;
    public GameObject bullet_Prefab;
    public ParticleSystem shootFX;

    private Animator shootSliderAnim;

    [HideInInspector]
    public bool canShoot;


    void Start()
    {
        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        GameObject.Find("ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovementWithKeyboard();
        ChangeRotation();
    }

    void FixedUpdate()
    {
        MoveTank();
        
    }

    void MoveTank()
    {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    void ControlMovementWithKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }

        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }

        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveNormal();
        }
    }

    void ChangeRotation()
    {
        if (speed.x>0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }

        else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }

        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl()
    {
        if (Time.timeScale != 0)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();
                canShoot = false;
                shootSliderAnim.Play("Fill");
            }
        }
       
        
        
    }
}
