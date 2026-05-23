public interface SlimeAppearanceState
{
    bool CanJump();
    bool CanMove();
    SlimeAppearanceStateTransition ChangeDayNight();
    SlimeAppearanceStateTransition ChangeTemperature(float temperature);
    SlimeAppearanceStateTransition ChangeHumidity(float humidity);
    SlimeAppearanceStateTransition ChangeBrightness(float brightness);
    SlimeAppearanceStateTransition OnEnter(SlimeAppearanceContext context);
    SlimeAppearanceAnimationState StateType { get; }
}
