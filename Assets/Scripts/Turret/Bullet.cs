using System.Collections;
using UnityEngine;

//ѕонимаю, что тут должен быть либо абстрактный класс либо интерфейс,
//но € чутка устал ковыр€ть это, попозже сделаю что то из этого
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float bulletSpeed;
    [SerializeField] protected int bulletDamage;

    private Transform currentTargetTransform;
    private UnitHealthSystem targetUnitHealthSystem;

    private void Start()
    {
        gameObject.transform.parent = null;
    }
    public void ShotBullet(Transform currentTargetTransform)
    {
        this.currentTargetTransform = currentTargetTransform;
        if(currentTargetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(FlyToTarget());
    }
    public void SetUnitHealthSystem(UnitHealthSystem targetUnitHealthSystem)
    {
        this.targetUnitHealthSystem = targetUnitHealthSystem;
    }
    private IEnumerator FlyToTarget()
    {
        float timeElapsed = 0;
        var currentBulletTransfom = transform.position;
        while (timeElapsed < bulletSpeed)
        {
            transform.position = Vector3.Lerp(currentBulletTransfom, currentTargetTransform.position, timeElapsed / bulletSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = currentTargetTransform.position;
        transform.LookAt(currentTargetTransform);
        HitTarget();
        
    }
    private void HitTarget()
    {
        Damage(targetUnitHealthSystem);
        GameObject particalEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(particalEffect, 2f);
        Destroy(gameObject);
    }
    public virtual void Damage(UnitHealthSystem enemy)
    {
        enemy.DealDamage(bulletDamage);
    }
}
