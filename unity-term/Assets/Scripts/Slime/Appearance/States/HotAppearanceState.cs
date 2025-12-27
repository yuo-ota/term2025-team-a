public class HotAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceStateTransition ChangeBrightness(float brightness)
    {
        return SlimeAppearanceStateTransition.None;
    }
    public SlimeAppearanceStateTransition ChangeHumidity(float humidity)
    {
        return SlimeAppearanceStateTransition.None;
    }

    public SlimeAppearanceStateTransition ChangeTemperature(float temperature)
    {
        if (temperature > SlimeAppearanceStateConstants.TO_HOT_THRESHOLD)
        {
            return SlimeAppearanceStateTransition.None;
        }

        return SlimeAppearanceStateTransition.ToStandard;
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
