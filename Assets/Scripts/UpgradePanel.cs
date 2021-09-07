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

    private Soldier soldier = null;

    public void SetValue(Soldier soldier)
    {
        soldierNameText.text = soldier.name;
        priceText.text = string.Format("{0} 에너지", soldier.price);
        amountText.text = string.Format("{0}", soldier.amount);
        this.soldier = soldier;
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
        amountText.text = string.Format("{0}", soldierInList.amount);
        priceText.text = string.Format("{0} 에너지", soldierInList.price);
        GameManager.Instance.uiManager.UpdateEnergyPanel();
    }
}
