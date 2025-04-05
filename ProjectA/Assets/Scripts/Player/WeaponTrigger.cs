using System.Collections;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider weaponColider;

    public void WeaponColiderTrue()
    {
        weaponColider.enabled = true;
        StartCoroutine(WaitWeaponColiderFalse(.5f));

    }
    public void WeaponColiderFalse()
    {
        weaponColider.enabled = false;
    }

    private IEnumerator WaitWeaponColiderFalse(float delay)
    {
        yield return new WaitForSeconds(delay);
        WeaponColiderFalse();
    }
}
