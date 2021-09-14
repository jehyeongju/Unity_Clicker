using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Image soldierImage = null;
    [SerializeField]
    private Text soldierNameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Button purchaseButton = null;
    [SerializeField]
    private Sprite[] soldierSprite = null;

    private Soldier soldier = null;

    public void SetValue(Soldier soldier)
    {
        this.soldier = soldier;
        UpdateUI();
    }

    public void UpdateUI()
    {
        soldierImage.sprite = soldierSprite[soldier.imageNnmber];
        soldierNameText.text = soldier.name;
        priceText.text = string.Format("{0} ¿¡³ÊÁö", soldier.price);
        amountText.text = string.Format("{0}", soldier.amount);
    }

    public void OnClickPurchase()
    {
        if (GameManager.Instance.CurrentUser.energy < soldier.price)
        {
            return;
        }
        GameManager.Instance.CurrentUser.energy -= soldier.price;
        Soldier soldierInList = GameManager.Instance.CurrentUser.soldierList.Find((x) => x == soldier);
        soldierInList.amount++;
        soldier.price = (long)(soldierInList.price * 1.25f);
        UpdateUI();
        GameManager.Instance.uiManager.UpdateEnergyPanel();
    }
}
