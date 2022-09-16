using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Player2D : NetworkBehaviour {
    [SerializeField] Ball prefabBall;
    private NetworkTransform nt;
    [Networked]TickTimer delay{ get; set; }

    void Awake() {
        nt = GetComponent<NetworkTransform>();
    }
    public override void FixedUpdateNetwork() {
        //print("We fixed");
        if (GetInput(out NetworkInputData data)) {
            var dir = data.dir.normalized;
            Vector3 delta = dir * 5 * Runner.DeltaTime;
            transform.position += delta;

            if(!delay.ExpiredOrNotRunning(Runner)) return;
            if((data.buttons & NetworkInputData.MOUSEBUTTON1) != 0){
                delay = TickTimer.CreateFromSeconds(Runner, .5f);
                Runner.Spawn(prefabBall, (Vector2)transform.position+Vector2.right, 
                Quaternion.LookRotation(Vector2.right), Object.InputAuthority, 
                (runner, o) => o.GetComponent<Ball>().Init());
            }
        }
    }
}
