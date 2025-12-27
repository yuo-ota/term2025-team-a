public class DryAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceStateTransition ChangeBrightness(float brightness)
    {
        return SlimeAppearanceStateTransition.None;
    }
    public SlimeAppearanceStateTransition ChangeHumidity(float humidity)
    {
        if (humidity < SlimeAppearanceStateConstants.TO_DRY_THRESHOLD)
        {
            return SlimeAppearanceStateTransition.None;
        }

        return SlimeAppearanceStateTransition.ToStandard;
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
