public interface SlimeAppearanceState
{
    SlimeAppearanceStateTransition ChangeDayNight();
    SlimeAppearanceStateTransition ChangeTemperature(float temperature);
    SlimeAppearanceStateTransition ChangeHumidity(float humidity);
    SlimeAppearanceStateTransition ChangeBrightness(float brightness);
    SlimeAppearanceAnimationState StateType { get; }
}
