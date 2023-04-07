using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    private const float TICK_TIMER_MAX = .5f;

    // actionlar çift ve tek tickte çaðýrýlacaðý için
    // .5f ile her saniyede bir action çaðýrýlýr

    private float tickTimer;
    public static int tick; 
    // public çünkü diðer scriptlerden bu deðer kontrol edilerek
    // mesela þu kadar tik sonra þunu yap diye bir iþlem yaptýrýlabilir.
    

    public static Action OnTick;
    public static Action LateTick;
    // diðer bir monobehaviour a bu actionlara function larý += ile ekleyip
    // her bir tickte o functionlarý çaðýrabiliriz.


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
