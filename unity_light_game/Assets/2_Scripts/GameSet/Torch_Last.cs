using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torch_Last : MonoBehaviour
{
    int torch = PlayerMove.litTorches;
    void Update()
    {
        torch = PlayerMove.litTorches;
        if (torch>=9) {
            gameObject.SetActive(false);
        }

    }
}
