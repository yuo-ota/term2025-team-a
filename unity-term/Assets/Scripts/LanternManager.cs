using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lanternRenderer;
    [SerializeField] private Light2D lanternLight;
    [SerializeField] private int _output;

    private void Start()
    {
        UpdateLantern(Output);
    }

    public int Output
    {
        get => _output;
        set
        {
            _output = value;
            UpdateLantern(_output);
        }
    }

    private void UpdateLantern(int output)
    {
        if (lanternRenderer == null || lanternLight == null)
        {
            return;
        }

        int outputClamped = CalcOutput(output);

        lanternRenderer.color = new Color(1f, 1f, 1f, outputClamped / 100f);
        lanternLight.intensity = Mathf.Sqrt(outputClamped / 100f) * 1.5f;
    }

    private int CalcOutput(int output)
    {
        TimeSpan now = DateTime.Now.TimeOfDay;

        if (now <= SlimeAppearanceStateConstants.LANTERN_START && now > SlimeAppearanceStateConstants.LANTERN_END)
        {
            return 0;
        }

        return Mathf.Clamp(output, 0, 100);
    }
}
