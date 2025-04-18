using System.Collections;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    private Player player;
    [SerializeField] private BoxCollider weaponColider;
    [SerializeField] private GameObject slashEffect;
    private GameObject currentSlashEffect;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void WeaponColiderTrue()
    {
        weaponColider.enabled = true;
        StartCoroutine(WaitWeaponColiderFalse(.5f));

    }
    public void WeaponColiderFalse()
    {
        weaponColider.enabled = false;
    }

    public void StartSlashEffect()
    {
        int comboCount = player.anim.GetInteger("ComboCount");
        currentSlashEffect = GameManager.Pool.Get(slashEffect, player.slashTransform[comboCount].position, player.slashTransform[comboCount].rotation);
        GameManager.Pool.Release(currentSlashEffect, .8f);
    }

    public void EndSlashEffect()
    {
        //GameManager.Pool.Release(currentSlashEffect);
    }

    private IEnumerator WaitWeaponColiderFalse(float delay)
    {
        yield return new WaitForSeconds(delay);
        WeaponColiderFalse();
    }

    private void OnDrawGizmos()
    {
        
    }
}
