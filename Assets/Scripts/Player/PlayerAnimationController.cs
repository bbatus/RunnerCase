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
            // if (Input.GetKeyDown(KeyCode.S))
            // {
            //     SlideAnimStart();
            // }
            // if (Input.GetKeyUp(KeyCode.S))
            // {
            //     SlideAnimFinish();
            // }
            CheckJumpAnim();
            CheckSlideAnim();

            //             if(Input.GetKeyUp(KeyCode.W)){
            //     JumpAnimFinish();
            // }


        }
        if (UIManager.instance.levelState == LevelState.Lose)
        {
            anim.SetTrigger("isDeath");
        }
    }

    private void CheckJumpAnim()
    {
        anim.SetBool("isJump", Input.GetKey(KeyCode.W));
    }

    private void CheckSlideAnim(){
        anim.SetBool("isSlide", Input.GetKey(KeyCode.S));
    }
    private void RunAnim()
    {
        anim.SetTrigger("isStarted");
    }

    // private void SlideAnimStart()
    // {
    //     anim.SetBool("isSlide", true);
    // }
    // private void SlideAnimFinish()
    // {
    //     anim.SetBool("isSlide", false);
    // }

    // private void JumpAnimStart()
    // {
    //     anim.SetTrigger("isJump");
    // }
    //     private void JumpAnimFinish(){
    //     anim.SetBool("isJump", false);
    // }
}
