using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class SingletonBehaviour : MonoBehaviour {

    private static SingletonBehaviour _instance;
    public static SingletonBehaviour Instance {
        get { return _instance; }
        protected set {
            if (_instance != null) throw new Exception($"Only one {value.GetType().Name} expected.");
            _instance = value;
        }
    }


    protected virtual void Awake() {
        SingletonBehaviour.Instance = this;
    }

    protected virtual void OnDestroy() {
        if (SingletonBehaviour.Instance == this)
            SingletonBehaviour.Instance = null;
    }

}

