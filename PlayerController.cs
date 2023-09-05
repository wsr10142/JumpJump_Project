using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //角色
    Rigidbody2D rigid2D;

    //跳躍
    float jumpForce = 800.0f;
    bool startFrame = false;
    float currentTime = 0;

    //控制左右方向
    int moveInput = 0;
    bool rightBtn = false;
    bool leftBtn = false;
    float walkForce = 80.0f;

    //切換相機
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
        //空白鍵按下時，開始記錄幀數
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            startFrame = true;
            currentTime = 0;
        }

        //空白鍵未被解除，幀數遞增
        if(startFrame == true)
        {
            currentTime += Time.deltaTime;
        }

        //偵測左右方向鍵是否被按下
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

        //空白鍵被放下，計算跳躍高度並執行跳躍動作
        if (Input.GetKeyUp(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            Debug.Log("currentTime:" + currentTime);
            Debug.Log("moveInput:" + moveInput);

            startFrame = false;

            //角色左右移動
            if ((rightBtn == true || leftBtn == true) && currentTime >= 0.2f)
            {
                this.rigid2D.AddForce(transform.right * moveInput * this.walkForce);
            }
            else if((rightBtn == true || leftBtn == true) && currentTime < 0.2f)
            {
                this.rigid2D.AddForce(transform.right * moveInput * (this.walkForce/2.0f));
            }
            Debug.Log("speed_x:" + (transform.right * moveInput * this.walkForce).x);

            //設定最高跳躍的臨界值
            if(currentTime > 0.8f)
            {
                currentTime = 0.8f;
            }

            //跳躍
            this.rigid2D.AddForce(transform.up * currentTime * this.jumpForce);
        }

        //切換關卡
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

    //切換關卡
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
