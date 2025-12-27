using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mySelf;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SlimeAppearanceContext context;

    private void Awake()
    {
        if (context == null)
        {
            context = GetComponent<SlimeAppearanceContext>();
        }

        context.OnStateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        if (context != null)
        {
            context.OnStateChanged -= OnStateChanged;
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            animator.SetTrigger("handle_jump");
            Debug.Log("ジャンプトリガーが発動しました");
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 0.01f;
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += 0.01f;
        }

        transform.position = pos;
    }

    public void OnStateChanged(SlimeAppearanceAnimationState type)
    {
        SetAppearance(type);
    }

    public void SetAppearance(SlimeAppearanceAnimationState state)
    {
        animator.SetInteger("slime_status", (int)state);
    }
}
