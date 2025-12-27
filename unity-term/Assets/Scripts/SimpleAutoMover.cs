using UnityEngine;
using System.Collections;

public class SimpleAutoMover : MonoBehaviour
{
    [Header("移動先範囲")]
    [SerializeField]
    private float minPositionX; // 最小座標
    [SerializeField]
    private float maxPositionX; // 最大座標

    [Header("インターバル設定")]
    [SerializeField]
    private float minMoveIntervalSecond = 5f;
    [SerializeField]
    private float maxMoveIntervalSecond = 30f;
    [SerializeField]
    private float minJumpIntervalSecond = 10f;
    [SerializeField]
    private float maxJumpIntervalSecond = 30f;

    [Header("ステータス設定")]
    [SerializeField]
    private float moveSpeed = 4f;

    [Header("その他設定")]
    [SerializeField]
    private AnimationManager animationManager;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        StartCoroutine(MoveRoutine());
        StartCoroutine(JumpRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            // 移動先設定
            float target = Random.Range(minPositionX, maxPositionX);
            float start = transform.position.x;

            if (!animationManager.CanMove())
            {
                yield return new WaitForSeconds(60f);
            }

            // 画像の向き設定
            bool isGoalLeft = target - start < 0;
            FlipSprite(isGoalLeft);

            // 移動
            while (Mathf.Abs(transform.position.x - target) > 0.01f)
            {
                float newX = Vector3.MoveTowards(
                    new Vector3(transform.position.x, 0, 0),
                    new Vector3(target, 0, 0),
                    moveSpeed * Time.deltaTime
                ).x;

                transform.position = new Vector3(newX, transform.position.y, 0);
                yield return null;
            }

            // 静止
            float waitTime = CalcRandomWaitTime(minMoveIntervalSecond, maxMoveIntervalSecond);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void FlipSprite(bool direction)
    {
        spriteRenderer.flipX = direction;
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            animationManager.Jump();

            float waitTime = CalcRandomWaitTime(minJumpIntervalSecond, maxJumpIntervalSecond);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private float CalcRandomWaitTime(float minTime, float maxTime)
    {
        return Random.Range(minTime, maxTime);
    }
}
