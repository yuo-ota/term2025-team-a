using UnityEngine;

public class App : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
    }
}
