using UnityEngine;
using System.Collections;

public class Gorilla : MonoBehaviour {
    public Animator gorilla;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            gorilla.SetBool("idle", true);
            gorilla.SetBool("walk", false);
            gorilla.SetBool("walk", false);
            gorilla.SetBool("pound", false);
            gorilla.SetBool("roll", false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            gorilla.SetBool("walk", true);
            gorilla.SetBool("idle", false);
            gorilla.SetBool("attack", false);
            gorilla.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            gorilla.SetBool("run", true);
            gorilla.SetBool("walk", false);
            gorilla.SetBool("attack", false);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            gorilla.SetBool("attack", true);
            gorilla.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            gorilla.SetBool("pound", true);
            gorilla.SetBool("attack", false);
            gorilla.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            gorilla.SetBool("jump", true);
            gorilla.SetBool("pound", false);
            gorilla.SetBool("sommersault", false);
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            gorilla.SetBool("sommersault", true);
            gorilla.SetBool("jump", false);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            gorilla.SetBool("roll", true);
            gorilla.SetBool("sommersault", false);
            gorilla.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            gorilla.SetBool("roll", true);
            gorilla.SetBool("sommersault", false);
            gorilla.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            gorilla.SetBool("swing", true);
            gorilla.SetBool("roll", false);
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            gorilla.SetBool("slapright", true);
            gorilla.SetBool("swing", false);
            gorilla.SetBool("slapleft", false);
            gorilla.SetBool("hit", false);
        }
        if (Input.GetKey(KeyCode.Keypad0))
        {
            gorilla.SetBool("slapleft", true);
            gorilla.SetBool("slapright", false);
            gorilla.SetBool("hit", false);
        }
        if (Input.GetKey(KeyCode.Keypad1))
        {
            gorilla.SetBool("hit", true);
            gorilla.SetBool("slapleft", false);
            gorilla.SetBool("slapright", false);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            gorilla.SetBool("die", true);
            gorilla.SetBool("hit", false);
        }
    }
}
