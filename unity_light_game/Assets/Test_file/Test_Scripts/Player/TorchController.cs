using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    private Light2D torchLight;
    private bool interacted = false;

    private void Start()
    {
        torchLight = GetComponentInChildren<Light2D>();
        torchLight.intensity = 0f;
    }

    public bool IsInteracted() => interacted;

    public void ActivateTorch()
{
    if (!interacted) {
        torchLight.intensity = 1f;
        interacted = true;
        if(PlayerVision.Instance != null)
        {
            PlayerVision.Instance.LightTorch();
        }
        else
        {
            Debug.Log("PlayerVision.Instance is null");
        }
    }
}


    public void SetInteracted(bool value) => interacted = value;

    public void SetIntensity(float intensity)
    {
        torchLight.intensity = 1f;
    }
}
