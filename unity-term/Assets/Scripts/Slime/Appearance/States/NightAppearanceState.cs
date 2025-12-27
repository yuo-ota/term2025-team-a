public class NightAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceAnimationState StateType =>
        SlimeAppearanceAnimationState.Night;
    public SlimeAppearanceStateTransition ChangeBrightness(float brightness)
    {
        if (brightness < SlimeAppearanceStateConstants.TO_BRIGHT_THRESHOLD)
        {
            return SlimeAppearanceStateTransition.ToSleep;
        }

        return SlimeAppearanceStateTransition.None;
    }
    public SlimeAppearanceStateTransition ChangeHumidity(float humidity)
    {
        return SlimeAppearanceStateTransition.None;
    }

    public SlimeAppearanceStateTransition ChangeTemperature(float temperature)
    {
        return SlimeAppearanceStateTransition.None;
    }

    public void Feed()
    {
        throw new System.NotImplementedException();
    }

    public SlimeMoveStateTransition Jump()
    {
        return SlimeMoveStateTransition.ToJump;
    }
}
