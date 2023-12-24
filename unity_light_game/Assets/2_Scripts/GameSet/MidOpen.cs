using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MidOpen : MonoBehaviour
{
    public TextMeshProUGUI yourTMPElement;
    private bool hasActivated = false;

    void Start()
    {
        yourTMPElement.gameObject.SetActive(false);
    }

    void Update()
    {
        int torch = PlayerMove.litTorches;

        if (torch >= 9 && !hasActivated) {
            yourTMPElement.gameObject.SetActive(true);
            hasActivated = true; 
            StartCoroutine(DeactivateTMPAfterDelay(4f));
        }
    }

    IEnumerator DeactivateTMPAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        yourTMPElement.gameObject.SetActive(false);
    }
}
