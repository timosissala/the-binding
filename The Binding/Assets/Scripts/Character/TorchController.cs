using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    private int torchFuel = 10;
    public int TorchFuel { get { return torchFuel; } set { torchFuel = value; SetTorchLight(); } }

    private Light2D torchLight;

    [SerializeField]
    private float torchDecayInterval = 5.0f;
    private MonoBehaviourTimer torchTimer;

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

        torchLight.pointLightInnerRadius = Mathf.Clamp(TorchFuel / 6, 0.6f, 1.8f);
        torchLight.pointLightOuterRadius = Mathf.Clamp(TorchFuel / 3, 1.2f, 3.6f);

        torchLight.intensity = Mathf.Clamp(TorchFuel / 6, 0.8f, 1.5f);
    }
}
