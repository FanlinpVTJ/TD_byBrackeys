using UnityEngine;

[CreateAssetMenu(menuName = "TurretProject/TurretConfig")]
public class TurretConfig : ScriptableObject
{
    [SerializeField] private Turret turret;
    [SerializeField] private int cost;
    [SerializeField] private float sellCostCoeff;
    [SerializeField] private float fireRate;
    [SerializeField] private float startBulletDamage;
    [SerializeField] private float turretFireRange;
    [SerializeField] private float speedRotationOfTurret;

    public Turret Turret { get => turret; }
    public int Cost { get => cost; }
    public float SellCostCoeff { get => sellCostCoeff; }
    public float FireRate { get => fireRate; }
    public float StartBulletDamage { get => startBulletDamage; }
    public float TurretFireRange { get => turretFireRange; }
    public float SpeedRotationOfTurret { get => speedRotationOfTurret; }
}
