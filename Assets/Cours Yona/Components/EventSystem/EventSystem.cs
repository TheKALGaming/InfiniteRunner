using System;
using UnityEngine;

public static class EventSystem
{
    public static Action<bool> OnPlayerSlideDown;
    public static Action OnPlayerCollision;
    public static Action<int> OnPlayerLifeUpdate;
}