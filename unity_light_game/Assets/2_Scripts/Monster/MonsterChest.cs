using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChest : MonoBehaviour
{
    private bool Interacted=false;
    private int count = 0;
    private float time = 0;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetInteracted(bool flag) { Interacted = flag; }
    public bool IsInteracted() { return Interacted; }
    public void ActivateChestMob()
    {
        Interacted = true;
        //Animator
    }
    public void TransUpdate() {

        if (Interacted && count == 0)
        {
            animator.SetBool("IsATT", Interacted);
            count = 1;
        }
        else if (Interacted && count == 1)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if (time > 1.45f)
            {
                animator.SetInteger("motionT", 10);
                count = 10;
            }
        }
        else if (count == 10) {

            count = 0;
            time = 0;
            Interacted = false;
            animator.SetBool("IsATT", Interacted);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TransUpdate();
    }
}
