using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnergyText : MonoBehaviour
{
    private Text energyText = null;

    public void Show(int number)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        energyText = GetComponent<Text>();
        energyText.text = string.Format("+{0}", number);
        energyText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 100f;
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }

    private void Despawn()
    {
        energyText.DOFade(1f, 0f);
        energyText.transform.SetParent(GameManager.Instance.Pool);
        energyText.gameObject.SetActive(false);
    }
}
