using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Essence : DroppableCurrency
{
    [Header(" Actions ")]
    public static Action<Essence> onCollected;

    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }
}
