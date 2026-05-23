using System;
using static UnityEngine.Rendering.DebugUI.Table;

public class DryAppearanceState : SlimeAppearanceState
{
    public SlimeAppearanceAnimationState StateType =>
        SlimeAppearanceAnimationState.Dry;

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
            return SlimeAppearanceStateTransition.None;
        }

        return SlimeAppearanceStateTransition.ToStandard;
    }

    public SlimeAppearanceStateTransition ChangeTemperature(float temperature)
    {
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
