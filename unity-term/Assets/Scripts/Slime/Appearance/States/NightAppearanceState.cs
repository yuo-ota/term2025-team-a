using System;

public class NightAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceAnimationState StateType =>
        SlimeAppearanceAnimationState.Night;

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
}
