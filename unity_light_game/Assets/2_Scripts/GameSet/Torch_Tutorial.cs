using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Tutorial : MonoBehaviour
{
    public int totalTorch = PlayerMove.litTorches;


    void Update()
    {
        totalTorch = PlayerMove.litTorches;
        if (totalTorch >= 1) {
            gameObject.SetActive(false); 
        }
    }
}
