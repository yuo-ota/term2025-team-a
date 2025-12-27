public interface SlimeAppearanceState
{
    SlimeAppearanceStateTransition ChangeTemperature(float temperature);
    SlimeAppearanceStateTransition ChangeHumidity(float humidity);
    SlimeAppearanceStateTransition ChangeBrightness(float brightness);
    SlimeAppearanceAnimationState StateType { get; }
}
