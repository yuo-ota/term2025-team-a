public interface SlimeAppearanceState
{
    SlimeMoveStateTransition Jump();
    void Feed();
    SlimeAppearanceStateTransition ChangeTemperature(float temperature);
    SlimeAppearanceStateTransition ChangeHumidity(float humidity);
    SlimeAppearanceStateTransition ChangeBrightness(float brightness);
    SlimeAppearanceAnimationState StateType { get; }
}
