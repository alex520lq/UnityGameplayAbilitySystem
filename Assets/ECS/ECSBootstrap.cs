﻿using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using Unity.Entities;
using UnityEngine;

public class ECSBootstrap : MonoBehaviour {

    private void Start() {
        // get ECS PlayerLoop
        var playerLoop = ScriptBehaviourUpdateOrder.CurrentPlayerLoop;

        // set to UniRx.Async PlayerLoop
        PlayerLoopHelper.Initialize(ref playerLoop);

    }
}