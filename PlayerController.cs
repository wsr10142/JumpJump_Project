using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //����
    Rigidbody2D rigid2D;

    //���D
    float jumpForce = 800.0f;
    bool startFrame = false;
    float currentTime = 0;

    //����k��V
    int moveInput = 0;
    bool rightBtn = false;
    bool leftBtn = false;
    float walkForce = 80.0f;

    //�����۾�
    Camera Camera_level_one;
    Camera Camera_level_two;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        Camera_level_one = GameObject.Find("Main Camera").GetComponent<Camera>(); ;
        Camera_level_two = GameObject.Find("LevelTwoCamera").GetComponent<Camera>(); ;
        Camera_level_two.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�ť�����U�ɡA�}�l�O���V��
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            startFrame = true;
            currentTime = 0;
        }

        //�ť��䥼�Q�Ѱ��A�V�ƻ��W
        if(startFrame == true)
        {
            currentTime += Time.deltaTime;
        }

        //�������k��V��O�_�Q���U
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveInput = 1;
            rightBtn = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveInput = -1;
            leftBtn = true;
        }

        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveInput = 0;
            rightBtn = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveInput = 0;
            leftBtn = false;
        }

        //�ť���Q��U�A�p����D���רð�����D�ʧ@
        if (Input.GetKeyUp(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            Debug.Log("currentTime:" + currentTime);
            Debug.Log("moveInput:" + moveInput);

            startFrame = false;

            //���⥪�k����
            if ((rightBtn == true || leftBtn == true) && currentTime >= 0.2f)
            {
                this.rigid2D.AddForce(transform.right * moveInput * this.walkForce);
            }
            else if((rightBtn == true || leftBtn == true) && currentTime < 0.2f)
            {
                this.rigid2D.AddForce(transform.right * moveInput * (this.walkForce/2.0f));
            }
            Debug.Log("speed_x:" + (transform.right * moveInput * this.walkForce).x);

            //�]�w�̰����D���{�ɭ�
            if(currentTime > 0.8f)
            {
                currentTime = 0.8f;
            }

            //���D
            this.rigid2D.AddForce(transform.up * currentTime * this.jumpForce);
        }

        //�������d
        if(this.rigid2D.position.y < 5.0f )
        {
            Camera_level_one.enabled = true;
            Camera_level_two.enabled = false;
        }

        if (this.rigid2D.position.y > 5.0f && this.rigid2D.position.y < 10.0f)
        {
            Camera_level_one.enabled = false;
            Camera_level_two.enabled = true;
        }
    }

    //�������d
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "level_one_two")
        {
            Debug.Log("touch_first_wall");
        }
    }
    */
}
