using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    private Light2D torchLight;
    private bool interacted = false;
    private CircleCollider2D torchCollider;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        torchLight = GetComponentInChildren<Light2D>();
        torchLight.intensity = 0f;
        torchCollider = GetComponentInChildren<CircleCollider2D>();
        torchCollider.radius = 0f;
    }

    public bool IsInteracted() => interacted;

    public void ActivateTorch()
{
    if (!interacted) {
        audioSource.Play();
        torchLight.intensity = 1.5f;
        torchCollider.radius = 1.2f;
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
