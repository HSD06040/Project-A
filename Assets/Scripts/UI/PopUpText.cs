using System.Collections;
using TMPro;
using UnityEngine;

public class PopUpText : BillboardObject
{
    [SerializeField] private float scaleSpeed = 5f;
    [SerializeField] private float startUpSpeed = 1f;
    [SerializeField] private float endUpSpeed = 1f;
    [SerializeField] private float colorLossSpeed = 1f;
    [SerializeField] private TextMeshPro[] damageText;
    [SerializeField] private TextMeshPro critOrHealText;
    [SerializeField] private float spacing = 0.5f;
    [SerializeField] private float dropDistance = 2f;
    [SerializeField] private float dropDuration = 0.5f;
    [SerializeField] private float dropDelay = 0.1f;

    private bool isEnded = false;

    private void Update()
    {
        Billboard();

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.6f, 0.6f, 0.6f), scaleSpeed * Time.deltaTime);

        if(critOrHealText != null)
        {
            Color critColor = ColorLoss(critOrHealText);
            critOrHealText.color = critColor;
        }

        if (!isEnded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * startUpSpeed, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * endUpSpeed, transform.position.z);

            for (int i = 0; i < damageText.Length; i++)
            {
                if (damageText[i].gameObject.activeSelf)
                {
                    Color color = ColorLoss(damageText[i]);
                    damageText[i].color = color;

                    if (damageText[i].color.a < 0.01f)
                        Destroy(gameObject);
                }
            }
        }
    }

    private Color ColorLoss(TextMeshPro text)
    {
        Color color = text.color;
        color.a -= colorLossSpeed * Time.deltaTime;
        color.a = Mathf.Max(color.a, 0f);
        return color;
    }

    public void SetupText(string text)
    {
        char[] c = text.ToCharArray();

        for (int i = 0; i < damageText.Length; i++)
        {
            damageText[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < c.Length; i++)
        {
            damageText[i].text = c[i].ToString();
            damageText[i].gameObject.SetActive(true);

            damageText[i].transform.localPosition = new Vector3(i * spacing, dropDistance, 0);
        }

        StartCoroutine(AnimateAllTexts(c.Length));
    }

    private IEnumerator AnimateAllTexts(int count)
    {
        isEnded = false;

        for (int i = 0; i < count; i++)
        {
            StartCoroutine(PopUpTextCoroutine(damageText[i], dropDuration, i * dropDelay));
        }

        yield return new WaitForSeconds(.5f);
        isEnded = true;
    }

    IEnumerator PopUpTextCoroutine(TextMeshPro text, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 startPos = text.transform.localPosition;
        Vector3 endPos = new Vector3(startPos.x, 0f, startPos.z);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            text.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}
