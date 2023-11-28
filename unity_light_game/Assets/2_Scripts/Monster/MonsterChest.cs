using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChest : MonoBehaviour
{
    private bool Interacted=false;
    private int count = 0;
    private float time = 0;
    private bool Isopen = false;
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
        else if (Interacted) {
            time += Time.deltaTime;
            Debug.Log(time);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
