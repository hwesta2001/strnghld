using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    private const float TICK_TIMER_MAX = .5f;

    // actionlar �ift ve tek tickte �a��r�laca�� i�in
    // .5f ile her saniyede bir action �a��r�l�r

    private float tickTimer;
    public static int tick; 
    // public ��nk� di�er scriptlerden bu de�er kontrol edilerek
    // mesela �u kadar tik sonra �unu yap diye bir i�lem yapt�r�labilir.
    

    public static Action OnTick;
    public static Action LateTick;
    // di�er bir monobehaviour a bu actionlara function lar� += ile ekleyip
    // her bir tickte o functionlar� �a��rabiliriz.


    private void Awake()
    {
        tick = 0;
    }
    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            if (tick % 2 == 0)
            {
                OnTick?.Invoke();
            }
            else
            {
                LateTick?.Invoke();
            }

            tick++;
        }
    }
}
