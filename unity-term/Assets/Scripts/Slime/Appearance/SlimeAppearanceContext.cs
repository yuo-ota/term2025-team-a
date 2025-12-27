using System;
using UnityEngine;

public class SlimeAppearanceContext : MonoBehaviour
{
    private SlimeAppearanceState currentState;
    private SlimeAppearanceStateFactory factory;
    [SerializeField] private float brightness = SlimeAppearanceStateConstants.BRIGHTNESS_DEFAULT;
    [SerializeField] private float humidity = SlimeAppearanceStateConstants.HUMIDITY_DEFAULT;
    [SerializeField] private float temperature = SlimeAppearanceStateConstants.TEMPERATURE_DEFAULT;
    public event Action<SlimeAppearanceAnimationState> OnStateChanged;

    [ContextMenu("Debug/Humidity 80")]
    public void DebugHumidity80()
    {
        ChangeHumidity(80f);
    }

    [ContextMenu("Debug/Humidity 10")]
    public void DebugHumidity10()
    {
        ChangeHumidity(10f);
    }

    [ContextMenu("Debug/Temperature 25")]
    public void DebugTemperature25()
    {
        ChangeTemperature(25f);
    }

    [ContextMenu("Debug/Temperature 40")]
    public void DebugTemperature40()
    {
        ChangeTemperature(40f);
    }


    void Awake()
    {
        factory = new SlimeAppearanceStateFactory();
        currentState = factory.Create(SlimeAppearanceStateTransition.ToStandard);
    }

    public void ChangeDayNight()
    {
        SlimeAppearanceStateTransition transition = currentState.ChangeDayNight();

        HandleState(transition);
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
        // —Dæ‡ˆÊ brightness > humidity > temperature
        var transitions = new[]
        {
            currentState.ChangeDayNight(),
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

    private void ChangeState(SlimeAppearanceState newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState.StateType);
    }

    public bool CanJump()
    {
        return currentState.CanJump();
    }

    public bool CanMove()
    {
        return currentState.CanMove();
    }
}
