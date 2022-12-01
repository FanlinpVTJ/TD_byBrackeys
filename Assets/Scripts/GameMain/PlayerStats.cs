using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int startMoney = 400;
    [SerializeField] private int startLives;
    [SerializeField] private NodeUI nodeUI;

    public int PlayerMoney { get; private set; }
    public int LiveCount { get; private set; }
    private void OnEnable()
    {
        BuildingManager.Instance.OnTurretBuilding += ChangePlayerMoney;
        nodeUI.OnSellTurret += ChangePlayerMoney;
        nodeUI.OnUpgradeTurret += ChangePlayerMoney;
    }
    private void OnDisable()
    {
        BuildingManager.Instance.OnTurretBuilding -= ChangePlayerMoney;
        nodeUI.OnSellTurret -= ChangePlayerMoney;
        nodeUI.OnUpgradeTurret -= ChangePlayerMoney;
    }
    private void Awake()
    {
        PlayerMoney = startMoney;
        LiveCount = startLives;
    }

    private void ChangePlayerMoney(int moneyChange)
    {
        PlayerMoney += moneyChange;
    }
    private void ChangePlayerLives(int livesChange)
    {
        LiveCount -= livesChange;
    }

    public void SetActionToStats(UnitHealthSystem unitHealthSystem)
    {
        unitHealthSystem.OnDeathChangeMoney += ChangePlayerMoney;
        unitHealthSystem.gameObject.GetComponent<EnemyMovement>().OnDeathChangeLives += ChangePlayerLives; //:D
        unitHealthSystem.OnDeath += RemoveActionFromStats;
    }
    public void RemoveActionFromStats(UnitHealthSystem unitHealthSystem)
    {
        unitHealthSystem.OnDeathChangeMoney -= ChangePlayerMoney;
        unitHealthSystem.gameObject.GetComponent<EnemyMovement>().OnDeathChangeLives -= ChangePlayerLives; //:D
        unitHealthSystem.OnDeath -= RemoveActionFromStats;
    }
}
