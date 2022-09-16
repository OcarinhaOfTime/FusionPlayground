using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PhysxBall : NetworkBehaviour {
    [Networked] private TickTimer life { get; set; }

    public void Init(Vector3 fwd) {
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        GetComponent<Rigidbody>().velocity = fwd;
    }

    public override void FixedUpdateNetwork() {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
    }
}
