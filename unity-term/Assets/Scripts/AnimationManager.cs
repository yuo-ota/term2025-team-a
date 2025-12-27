using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mySelf;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SlimeAppearanceContext appearanceContext;

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

    public void OnStateChanged(SlimeAppearanceAnimationState type)
    {
        SetAppearance(type);
    }

    public void SetAppearance(SlimeAppearanceAnimationState state)
    {
        animator.SetInteger("slime_status", (int)state);
    }

    public void Jump()
    {
        if (appearanceContext.CanJump())
        {
            animator.SetTrigger("handle_jump");
        }
    }

    public bool CanJump()
    {
        return appearanceContext.CanJump();
    }

    public bool CanMove()
    {
        return appearanceContext.CanMove();
    }
}
