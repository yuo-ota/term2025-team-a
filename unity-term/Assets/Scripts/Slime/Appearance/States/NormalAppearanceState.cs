using System;

public class NormalAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceAnimationState StateType =>
        SlimeAppearanceAnimationState.Normal;

    public SlimeAppearanceStateTransition ChangeDayNight()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;

        if (now >= SlimeAppearanceStateConstants.NIGHT_START || now < SlimeAppearanceStateConstants.NIGHT_END)
        {
            return SlimeAppearanceStateTransition.ToNight;
        }

        return SlimeAppearanceStateTransition.None;
    }
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
        if (SlimeAppearanceStateConstants.TO_HOT_THRESHOLD < temperature)
        {
            return SlimeAppearanceStateTransition.ToHot;
        }

        return SlimeAppearanceStateTransition.None;
    }

    public SlimeAppearanceStateTransition OnEnter(SlimeAppearanceContext context)
    {
        return SlimeAppearanceStateTransition.None;
    }

    public bool CanJump()
    {
        return true;
    }

    public bool CanMove()
    {
        return true;
    }
}
