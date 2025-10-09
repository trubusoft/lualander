using System;
using UnityEngine;

public class Lander : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;
    
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        throw new NotImplementedException();
    }
}