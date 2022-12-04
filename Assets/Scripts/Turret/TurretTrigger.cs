using UnityEngine;

public class TurretTrigger : MonoBehaviour, ITrigger
{
    [SerializeField] private Transform[] firePointTransform;
    
    private IDamageType damageType;

    private void Start()
    {
        damageType = GetComponent<IDamageType>();
    }

    public void Shoot(Transform transform, UnitHealthSystem unitHealth, EnemyMovement enemyMovement)
    {
        damageType.SetBulletTransform(firePointTransform);
        damageType.SetTargetComponent(transform, unitHealth, enemyMovement);
        damageType.Shoot();
    }

    public void StopShoot()
    {
        damageType.StopShoot();
    }
}
