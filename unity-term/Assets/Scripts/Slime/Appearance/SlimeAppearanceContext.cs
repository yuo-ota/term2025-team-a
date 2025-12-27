using UnityEngine;

public class SlimeAppearanceContext : MonoBehaviour
{
    private SlimeAppearanceState currentState;
    private SlimeAppearanceStateFactory factory;
    [SerializeField] private float brightness;
    [SerializeField] private float humidity;
    [SerializeField] private float temperature;

    void Awake()
    {
        factory = new SlimeAppearanceStateFactory();
        currentState = factory.Create(SlimeAppearanceStateTransition.ToStandard);
    }

    public void ChangeBrightness(float brightness)
    {
        SlimeAppearanceStateTransition transition = currentState.ChangeBrightness(brightness);
        this.brightness = brightness;

        HandleState(transition);
    }

    public void ChangeHumidity(float humidity)
    {
        SlimeAppearanceStateTransition transition = currentState.ChangeHumidity(humidity);
        this.humidity = humidity;

        HandleState(transition);
    }

    public void ChangeTemperature(float temperature)
    {
        SlimeAppearanceStateTransition transition = currentState.ChangeTemperature(temperature);
        this.temperature = temperature;

        HandleState(transition);
    }

    private void HandleState(SlimeAppearanceStateTransition transition)
    {
        if (transition == SlimeAppearanceStateTransition.None)
            return;

        ChangeState(factory.Create(transition));

        if (transition == SlimeAppearanceStateTransition.ToStandard)
            ReCalcState();
    }

    private void ReCalcState()
    {
        // —Dæ‡ˆÊ brightness > humidity > temerature
        var transitions = new[]
        {
            currentState.ChangeBrightness(brightness),
            currentState.ChangeHumidity(humidity),
            currentState.ChangeTemperature(temperature),
        };

        foreach (var t in transitions)
        {
            if (t != SlimeAppearanceStateTransition.None)
            {
                HandleState(t);
                return;
            }
        }
    }

    private void ChangeState(SlimeAppearanceState sas)
    {
        currentState = sas;
    }
}
