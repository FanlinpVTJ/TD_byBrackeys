using System.Collections;
using UnityEngine;

//�������, ��� ��� ������ ���� ���� ����������� ����� ���� ���������,
//�� � ����� ����� �������� ���, ������� ������ ��� �� �� �����
//��� ��� ����� ������, �� � �� ������� ��� ���������� �� MissingReferens
//�� try catch ����� �� ��������, � ��� ����� ����� �������,�� �������� �� null ���� � ������� �� �������
//��� ��������, ����� � ������ ���������� 3 ��� ������ � ���� ��������, ��������
// ���������� ����� �� ����� ����, ��� ���� ����� � �����, ����� �������� ������ �� ���������, ��� �������
//�� ��� �������� ��� �������� � ��.
//� ����� ���� ��� ��������, �� ����� � �������, ��� ��� ���� �� ������, �����
public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float bulletFlyToTargetTime = 0.05f;
    
    protected int bulletDamage;
    private Transform currentTargetTransform;
    private UnitHealthSystem targetUnitHealthSystem;
    
    public void SetBulletDamage(int bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    public void ShotBullet(Transform currentTargetTransform, UnitHealthSystem targetUnitHealthSystem)
    {
        this.currentTargetTransform = currentTargetTransform;
        this.targetUnitHealthSystem = targetUnitHealthSystem;
        if (currentTargetTransform == null || targetUnitHealthSystem == null)
        {
            Destroy(gameObject);
            return;
        }
        StartCoroutine(FlyToTarget());
    }
    
    private IEnumerator FlyToTarget()
    {
        float timeElapsed = 0;
        var currentBulletTransfom = transform.position;
        
        while (timeElapsed < bulletFlyToTargetTime)
        {
            try
            {
                transform.position = Vector3.Lerp(currentBulletTransfom, currentTargetTransform.position, timeElapsed / bulletFlyToTargetTime);
                timeElapsed += Time.deltaTime;
            }
            catch (System.Exception)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
        try
        {
            transform.position = currentTargetTransform.position;
        }
        catch (System.Exception)
        {
            Destroy(gameObject);
        }
        transform.LookAt(currentTargetTransform);
        HitTarget();
        yield return null;
    }
    private void HitTarget()
    {
        GameObject particalEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(particalEffect, 2f);
        Damage(targetUnitHealthSystem);
    }
    public virtual void Damage(UnitHealthSystem enemy)
    {
        try
        {
            enemy.DealDamage(bulletDamage);
        }
        catch (System.Exception)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
