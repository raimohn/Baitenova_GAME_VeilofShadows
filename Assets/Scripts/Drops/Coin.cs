using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DroppableCurrency
{
    [Header(" Actions ")]
    public static Action<Coin> onCollected;

    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }
}
