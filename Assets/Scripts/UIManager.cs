using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text energyText = null;
    [SerializeField]
    private Animator beakerAnimator = null;
    [SerializeField]
    private UpgradePanel upgradePanelTemplate = null;
    [SerializeField]
    private EnergyText energyTextTemplate = null;

    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>();
    void Start()
    {
        UpdateEnergyPanel();
        CreatePanels();
    }

    public void CreatePanels()
    {
        GameObject panel = null;
        UpgradePanel panelComponent = null;

        foreach (Soldier soldier in GameManager.Instance.CurrentUser.soldierList)
        {
            panel = Instantiate(upgradePanelTemplate.gameObject, upgradePanelTemplate.transform.parent);
            panelComponent = panel.GetComponent<UpgradePanel>();
            panelComponent.SetValue(soldier);
            panel.SetActive(true);
            upgradePanelList.Add(panelComponent);
        }
    }

    public void OnClickBeaker()
    {
        GameManager.Instance.CurrentUser.energy += 1;
        beakerAnimator.Play("Click");

        EnergyText newText = null;

        if(GameManager.Instance.Pool.childCount > 0)
        {
            newText = GameManager.Instance.Pool.GetChild(0).GetComponent<EnergyText>();
            newText.transform.SetParent(GameManager.Instance.Pool.parent);
        }
        else
        {
            newText = Instantiate(energyTextTemplate, GameManager.Instance.Pool.parent).GetComponent<EnergyText>();
        }
        newText.gameObject.SetActive(true);
        newText.Show(1);
        UpdateEnergyPanel();
    }

    public void UpdateEnergyPanel()
    {
        energyText.text = string.Format("{0} ¿¡³ÊÁö", GameManager.Instance.CurrentUser.energy);
    }
}
