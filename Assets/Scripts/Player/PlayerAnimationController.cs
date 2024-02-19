using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>(); //playerparent altinda character
    }

    void Update()
    {
        if (UIManager.instance.levelState == LevelState.Playing)
        {
            RunAnim();
            if (Input.GetKeyDown(KeyCode.S))
            {
                SlideAnimStart();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                SlideAnimFinish();
            }
            CheckJumpAnim();

            //             if(Input.GetKeyUp(KeyCode.W)){
            //     JumpAnimFinish();
            // }
        }
    }

    private void CheckJumpAnim()
    {
        if (Input.GetKeyDown(KeyCode.W) )
        {
            JumpAnimStart();
        }
    }
    private void RunAnim()
    {
        anim.SetTrigger("isStarted");
    }

    private void SlideAnimStart()
    {
        anim.SetBool("isSlide", true);
    }
    private void SlideAnimFinish()
    {
        anim.SetBool("isSlide", false);
    }

    private void JumpAnimStart()
    {
        anim.SetBool("isJump", true);
    }
    //     private void JumpAnimFinish(){
    //     anim.SetBool("isJump", false);
    // }
}
