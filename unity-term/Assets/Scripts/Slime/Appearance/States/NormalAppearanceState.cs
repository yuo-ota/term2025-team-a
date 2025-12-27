public class NormalAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceStateTransition ChangeBrightness(float brightness)
    {
        return SlimeAppearanceStateTransition.None;
    }
    public SlimeAppearanceStateTransition ChangeHumidity(float humidity)
    {
        if (humidity < SlimeAppearanceStateConstants.TO_DRY_THRESHOLD)
        {
            return SlimeAppearanceStateTransition.ToDry;
        }

        return SlimeAppearanceStateTransition.None;
    }

    public SlimeAppearanceStateTransition ChangeTemperature(float temperature)
    {
        if (temperature > SlimeAppearanceStateConstants.TO_HOT_THRESHOLD)
        {
            return SlimeAppearanceStateTransition.ToHot;
        }

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
