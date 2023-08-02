using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    [SerializeField] Image fillImageBack;

    [SerializeField] Image fillImageFront;

    [SerializeField] bool delayFill =true;

    [SerializeField] float fillDelay = 0.5f;

    [SerializeField] float fillSpeed = 0.1f;

    float currentFillAmount;

    float targetFillAmount;

    float t;

    WaitForSeconds waitForDelayFill;

    Coroutine bufferedFillingCoroutine;

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        waitForDelayFill = new WaitForSeconds(fillDelay);
    }

    public void Initialize(float currentValue, float maxValue)
    {
        currentFillAmount = currentValue / maxValue;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }


    public void UpdateStats(float currentValue, float maxValue)
    {
        targetFillAmount = currentValue / maxValue;

        if (bufferedFillingCoroutine != null)
        {
            StopCoroutine(bufferedFillingCoroutine);
        }

        if (currentFillAmount > targetFillAmount)
        {
            fillImageFront.fillAmount = targetFillAmount;
            bufferedFillingCoroutine = StartCoroutine(BufferedFillingCorouting(fillImageBack));
        }

        if (currentFillAmount < targetFillAmount)
        {
            fillImageBack.fillAmount = targetFillAmount;
            bufferedFillingCoroutine = StartCoroutine(BufferedFillingCorouting(fillImageFront));
        }
    }

    IEnumerator BufferedFillingCorouting(Image image)
    {
        if (delayFill)
        {
            yield return waitForDelayFill;
        }

        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fillSpeed;
            currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, t);
            image.fillAmount = currentFillAmount;

            yield return null;
        }
    }
}
