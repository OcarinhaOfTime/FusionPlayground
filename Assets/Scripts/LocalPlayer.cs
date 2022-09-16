using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour {
    public float speed = 5;
    InputHandler handler;
    CharacterController cc;
    void Awake(){
        handler = GetComponent<InputHandler>();
        cc = GetComponent<CharacterController>();
    }

    void Update(){
        cc.Move(speed * handler.dir * Time.deltaTime);
    }
}
