using System;

public class SleepAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceAnimationState StateType =>
        SlimeAppearanceAnimationState.Sleep;

    public SlimeAppearanceStateTransition ChangeDayNight()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;

        if (now >= SlimeAppearanceStateConstants.NIGHT_START || now < SlimeAppearanceStateConstants.NIGHT_END)
        {
            return SlimeAppearanceStateTransition.None;
        }

        return SlimeAppearanceStateTransition.ToStandard;
    }
    public SlimeAppearanceStateTransition ChangeBrightness(float brightness)
    {
        if (brightness < SlimeAppearanceStateConstants.TO_BRIGHT_THRESHOLD)
        {
            return SlimeAppearanceStateTransition.None;
        }

        return SlimeAppearanceStateTransition.ToNight;
    }
    public SlimeAppearanceStateTransition ChangeHumidity(float humidity)
    {
        return SlimeAppearanceStateTransition.None;
    }

    public SlimeAppearanceStateTransition ChangeTemperature(float temperature)
    {
        return SlimeAppearanceStateTransition.None;
    }

    public bool CanJump()
    {
        return false;
    }

    public bool CanMove()
    {
        return false;
    }
}
