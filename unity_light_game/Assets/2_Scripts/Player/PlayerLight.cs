using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] Light2D light2d;
    [SerializeField] CircleCollider2D circleCollider;

    // Update is called once per frame
    void FixedUpdate()
    {
        circleCollider.radius = light2d.pointLightOuterRadius;
    }
}
