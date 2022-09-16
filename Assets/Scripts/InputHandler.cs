using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    ControlMap controlMap;
    public Vector2 dir;
    public bool fired;
    public bool fired2;

    void Awake(){
        controlMap = new ControlMap();
        controlMap.Enable();
        controlMap.Player.Move.performed += ctx => dir = ctx.ReadValue<Vector2>();
        controlMap.Player.Move.canceled += ctx => dir = Vector2.zero;
        controlMap.Player.Fire.performed += ctx => fired = true;
        controlMap.Player.Fire.canceled += ctx => fired = false;

        controlMap.Player.Aim.performed += ctx => fired2 = true;
        controlMap.Player.Aim.canceled += ctx => fired2 = false;
    }
}
