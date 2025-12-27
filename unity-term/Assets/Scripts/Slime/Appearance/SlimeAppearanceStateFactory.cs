using System;

public class SlimeAppearanceStateFactory
{
    public SlimeAppearanceState Create(SlimeAppearanceStateTransition transition)
    {
        return transition switch
        {
            SlimeAppearanceStateTransition.ToStandard => new NormalAppearanceState(),
            SlimeAppearanceStateTransition.ToDry => new DryAppearanceState(),
            SlimeAppearanceStateTransition.ToHot => new HotAppearanceState(),
            SlimeAppearanceStateTransition.ToNight => new NightAppearanceState(),
            SlimeAppearanceStateTransition.ToSleep => new SleepAppearanceState(),
            SlimeAppearanceStateTransition.None =>
                throw new InvalidOperationException("Cannot create state from None"),

            _ => throw new InvalidOperationException(
                $"Unhandled transition: {transition}"
            )
        };
    }
}