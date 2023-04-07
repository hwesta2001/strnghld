using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimGlobal : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    public int itemiddeneme;
    public static HandAnimGlobal ins;

    void Awake()
    {
        ins = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        animator = GameObject.FindGameObjectWithTag("handAnimTag").GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("HandAnimGlobal de animator bo�!!!!");
        }
    }

    public void AttackTic(bool con)
    {

        int itemId = itemiddeneme;

        // // bu itemid "0 meele", "1 ranged" diye gidecek!
        // // bu �ekilde item�n ok mu yay m� oldu�una g�re atack animasyonu de�i�ecek!

        //if (item == meele)  
        //{
        //    itemId = 0;
        //}
        //else if (item == ranged)
        //{
        //    itemId = 1;
        //}


        animator.SetInteger("itemId", itemId);
        animator.SetBool("attacking", con);
    }
}
