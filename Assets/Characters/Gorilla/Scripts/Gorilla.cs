using UnityEngine;
using System.Collections;

public class Gorilla : MonoBehaviour 
{
    public Animator Animator;

	void Start () 
    {
	}

	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Animator.SetBool("idle", true);
            Animator.SetBool("walk", false);
            Animator.SetBool("walk", false);
            Animator.SetBool("pound", false);
            Animator.SetBool("roll", false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Animator.SetBool("walk", true);
            Animator.SetBool("idle", false);
            Animator.SetBool("attack", false);
            Animator.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Animator.SetBool("run", true);
            Animator.SetBool("walk", false);
            Animator.SetBool("attack", false);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Animator.SetBool("attack", true);
            Animator.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Animator.SetBool("pound", true);
            Animator.SetBool("attack", false);
            Animator.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Animator.SetBool("jump", true);
            Animator.SetBool("pound", false);
            Animator.SetBool("sommersault", false);
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            Animator.SetBool("sommersault", true);
            Animator.SetBool("jump", false);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            Animator.SetBool("roll", true);
            Animator.SetBool("sommersault", false);
            Animator.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            Animator.SetBool("roll", true);
            Animator.SetBool("sommersault", false);
            Animator.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            Animator.SetBool("swing", true);
            Animator.SetBool("roll", false);
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            Animator.SetBool("slapright", true);
            Animator.SetBool("swing", false);
            Animator.SetBool("slapleft", false);
            Animator.SetBool("hit", false);
        }
        if (Input.GetKey(KeyCode.Keypad0))
        {
            Animator.SetBool("slapleft", true);
            Animator.SetBool("slapright", false);
            Animator.SetBool("hit", false);
        }
        if (Input.GetKey(KeyCode.Keypad1))
        {
            Animator.SetBool("hit", true);
            Animator.SetBool("slapleft", false);
            Animator.SetBool("slapright", false);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            Animator.SetBool("die", true);
            Animator.SetBool("hit", false);
        }
    }
}
