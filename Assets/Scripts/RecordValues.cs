using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordValues
{
    public float horizontalAxis;
    public bool jumpButtonDown;
    public bool isDead;

    public RecordValues(float _horizontalAxis, bool _jumpButtonDown, bool _isDead)
    {
        horizontalAxis = _horizontalAxis;
        jumpButtonDown = _jumpButtonDown;
        isDead = _isDead;
    }
}
