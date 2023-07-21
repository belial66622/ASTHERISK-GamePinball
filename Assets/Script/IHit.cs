using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit 
{
    public event Action<int> OnHitBall;
    public event Action Disable;
}
