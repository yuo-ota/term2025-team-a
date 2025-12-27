using System;

public static class SlimeAppearanceStateConstants
{
    public const float TEMPERATURE_DEFAULT = 25f;
    public const float HUMIDITY_DEFAULT = 25f;
    public const float BRIGHTNESS_DEFAULT = 80f;

    public const float TO_HOT_THRESHOLD = 30f;
    public const float TO_DRY_THRESHOLD = 20f;
    public const float TO_BRIGHT_THRESHOLD = 1f;

    public static readonly TimeSpan NIGHT_START = new TimeSpan(18, 0, 0);
    public static readonly TimeSpan NIGHT_END = new TimeSpan(6, 0, 0);
}
