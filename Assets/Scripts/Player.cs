using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour {
    [SerializeField] Ball prefabBall;
    [SerializeField] PhysxBall pprefabBall;
    private NetworkCharacterControllerPrototype cc;
    [Networked]TickTimer delay{ get; set; }
    Vector3 fwd;

    void Awake() {
        cc = GetComponent<NetworkCharacterControllerPrototype>();
    }
    public override void FixedUpdateNetwork() {
        //print("We fixed");
        if (GetInput(out NetworkInputData data)) {
            print("Mobing");
            var dir = data.dir.normalized;
            Vector3 dir3 = dir.x * Vector3.right + dir.y * Vector3.forward;
            Vector3 delta = dir3 * 5 * Runner.DeltaTime;
            cc.Move(delta);

            if(!delay.ExpiredOrNotRunning(Runner)) return;
            if((data.buttons & NetworkInputData.MOUSEBUTTON1) != 0){
                fwd = transform.forward;
                delay = TickTimer.CreateFromSeconds(Runner, .5f);
                Runner.Spawn(prefabBall, transform.position+fwd, 
                Quaternion.LookRotation(fwd), Object.InputAuthority, 
                (runner, o) => o.GetComponent<Ball>().Init());
            }

            if((data.buttons & NetworkInputData.MOUSEBUTTON2) != 0){
                fwd = transform.forward;
                delay = TickTimer.CreateFromSeconds(Runner, .5f);
                Runner.Spawn(pprefabBall, transform.position+fwd, 
                Quaternion.LookRotation(fwd), Object.InputAuthority, 
                (runner, o) => o.GetComponent<PhysxBall>().Init(fwd));
            }
        }
    }
}
