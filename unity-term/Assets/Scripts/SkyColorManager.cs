using System;
using System.Collections;
using UnityEngine;

public class SkyColorManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skyRenderer;
    [SerializeField] private Color[] skyColors;

    [SerializeField] private int currentColorIndex = 0;
    private const float hourInterval = 3600f;

    [ContextMenu("Debug/add color index")]
    public void DebugAddColorIndex()
    {
        currentColorIndex = (currentColorIndex + 1) % skyColors.Length;
        skyRenderer.color = skyColors[currentColorIndex];
    }

    private void Start()
    {
        if (skyColors == null || skyColors.Length == 0 || skyRenderer == null)
            return;

        DateTime now = DateTime.Now;
        skyRenderer.color = skyColors[now.Hour];
        StartCoroutine(SkyColorRoutine());
    }

    private IEnumerator SkyColorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(hourInterval);

            DateTime now = DateTime.Now;
            skyRenderer.color = skyColors[now.Hour];
        }
    }
}
