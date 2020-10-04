using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordValues
{
    public float horizontalAxis;
    public float deltaTime;
    public bool jumpButtonDown;
    public bool isDead;

    public RecordValues(float _horizontalAxis, float _deltaTime, bool _jumpButtonDown, bool _isDead)
    {
        horizontalAxis = _horizontalAxis;
        deltaTime = _deltaTime;
        jumpButtonDown = _jumpButtonDown;
        isDead = _isDead;
    }
}
