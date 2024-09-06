using System.Collections;
using UnityEngine;

public class EraserScript : MonoBehaviour
{
    [SerializeField] GameObject black1;
    [SerializeField] GameObject black2;
    [SerializeField] GameObject parent;

    float maxHeight = 2.43f;
    float minHeight = -6.72f;
    float forwardDistance = 3f;
    float verticalMovementDuration = 5f; // Duration to move up or down
    float forwardMovementDuration = 2f; // Duration to move forward
    float waitDuration = 1f; // Duration to wait between movements
    float speedModifier = 1f;
    bool goingUp = false;
    bool started = false;
    public bool Started { get => started; set => started = value; }

    public static EraserScript Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void StartEraser()
    {
        // Start going down
        if (!started)
        {
            started = true;
            StartCoroutine(GoDown());
        }
    }

    public void SetSpeedModifier(float newValue)
    {
        speedModifier += newValue/50;
    }

    private IEnumerator GoDown()
    {
        black1.SetActive(true);
        black2.SetActive(false);

        yield return StartCoroutine(MoveVertical(minHeight, verticalMovementDuration / speedModifier));

        yield return new WaitForSeconds(waitDuration / speedModifier);

        StartCoroutine(GoForward());
    }

    private IEnumerator GoUp()
    {
        black1.SetActive(false);
        black2.SetActive(true);

        yield return StartCoroutine(MoveVertical(maxHeight, verticalMovementDuration / speedModifier));

        yield return new WaitForSeconds(waitDuration / speedModifier);

        StartCoroutine(GoForward());
    }

    private IEnumerator GoForward()
    {
        parent.transform.localPosition = transform.localPosition;
        black1.SetActive(false);
        black2.SetActive(false);

        yield return StartCoroutine(MoveForward(forwardDistance, forwardMovementDuration / speedModifier));

        yield return new WaitForSeconds(waitDuration / speedModifier);

        if (goingUp)
        {
            goingUp = false;
            StartCoroutine(GoDown());
        }
        else
        {
            goingUp = true;
            StartCoroutine(GoUp());
        }
    }

    private IEnumerator MoveVertical(float targetHeight, float duration)
    {
        float startHeight = transform.localPosition.y;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newHeight = Mathf.Lerp(startHeight, targetHeight, elapsedTime / duration);
            transform.localPosition = new Vector3(transform.localPosition.x, newHeight, transform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, targetHeight, transform.localPosition.z);
    }

    private IEnumerator MoveForward(float distance, float duration)
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(distance, 0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}
