using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfController : MonoBehaviour
{
    float speed = 40;
    float rotSpeed = 120;
    float rot = 0;
    float gravity = 4000;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    public SwitchManager switchManager;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
    }

    void Movement()
    {
       // if (controller.isGrounded)
       // {

            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, -1);
                    moveDir = moveDir * speed;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

      //  }

        rot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);


        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }
                if (anim.GetBool("running") == false)
                {
                    Attacking();
                }

            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                switchManager.SwitchCharacter("warrior");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate")
        {
            FindObjectOfType<CanvasController>().ChangeQuest();
        }
    }
    void Attacking()
    {

        StartCoroutine(AttackRoutine());

    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }

  
}
