using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChest : MonoBehaviour
{
    private bool Interacted=false;
    private bool Isopen = false;
    private Animator animator;
    void Start()
    {
        
    }
    public void SetInteracted(bool flag) { Interacted = flag; }
    public bool IsInteracted() { return Interacted; }
    public void ActivateChestMob()
    {
        Interacted = true;
        //Animator
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
