using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBootstrap : SingletonBehaviour {

    void Start() {
        Debug.Log($"{nameof(ServerBootstrap)} starting.");
    }
}

