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
            Debug.Log("HandAnimGlobal de animator boþ!!!!");
        }
    }

    public void AttackTic(bool con)
    {

        int itemId = itemiddeneme;

        // // bu itemid "0 meele", "1 ranged" diye gidecek!
        // // bu þekilde itemýn ok mu yay mý olduðuna göre atack animasyonu deðiþecek!

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
