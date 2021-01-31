using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    [SerializeField]
    private int torchFuel = 10;
    public int TorchFuel { get { return torchFuel; } set { torchFuel = value; SetTorchLight(); } }

    private Light2D torchLight;

    [SerializeField]
    private float torchDecayInterval = 5.0f;
    private MonoBehaviourTimer torchTimer;

    [SerializeField]
    private float torchInnerLightRadiusPerFuel;
    [SerializeField]
    private float torchOuterLightRadiusPerFuel;

    private void Start()
    {
        torchTimer = gameObject.GetComponent<MonoBehaviourTimer>();

        torchTimer.duration = torchDecayInterval;
        torchTimer.loopTimer = true;

        torchTimer.OnTimerFinished += SpendTorchFuel;

        torchTimer.StartTimer();

        SetTorchLight();
    }

    private void SpendTorchFuel()
    {
        TorchFuel -= 1;
    }

    private void SetTorchLight()
    {
        if (torchLight == null)
        {
            torchLight = GetComponent<Light2D>();
        }

        torchLight.pointLightInnerRadius = torchInnerLightRadiusPerFuel * torchFuel;
        torchLight.pointLightOuterRadius = torchOuterLightRadiusPerFuel * torchFuel;
    }
}
