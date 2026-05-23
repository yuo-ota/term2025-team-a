using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class TaskUIManager : MonoBehaviour
{
    [SerializeField]
    private int _taskId;

    [SerializeField]
    private GameObject taskUI;
    [SerializeField]
    private TextMeshProUGUI taskText;
    [SerializeField]
    private SlimeAppearanceContext appearanceContext;

    private Dictionary<int, string> taskMap = new Dictionary<int, string>()
    {
        {0, "" },
        {1, "スライムが乾燥してしまっています" },
        {2, "暑さでぐったりしています" },
        {3, "明るすぎて寝れないようです" }
    };

    public int TaskId // 0: タスクなし 1: 温度 2: 湿度 3: 光量
    {
        get => _taskId;
        set
        {
            _taskId = value;
            UpdateUI();
        }
    }
    private void Awake()
    {
        if (appearanceContext == null)
        {
            appearanceContext = GetComponent<SlimeAppearanceContext>();
        }

        appearanceContext.OnStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        if (appearanceContext != null)
        {
            appearanceContext.OnStateChanged -= OnStateChanged;
        }
    }

    [ContextMenu("Debug/TaskId 0")]
    public void DebugTaskId0()
    {
        TaskId = 0;
    }

    [ContextMenu("Debug/TaskId 1")]
    public void DebugTaskId1()
    {
        TaskId = 1;
    }

    public void OnStateChanged(SlimeAppearanceAnimationState type)
    {
        TaskId = (int)type;
    }

    private void UpdateUI()
    {
        if (TaskId == 0 || TaskId == 4)
        {
            taskUI.SetActive(false);
            return;
        }

        taskUI.SetActive(true);
        UpdateTask();
    }

    private void UpdateTask()
    {
        taskText.SetText(taskMap.GetValueOrDefault(TaskId, ""));
    }
}
