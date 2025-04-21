using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class SkillBase : MonoBehaviour
{
    protected Player player;

    private Coroutine skillRoutine;
    private Animator anim => player.anim;

    [SerializeField] private bool isGizmos;
    [SerializeField] private SkillOverlap overlap;

    [Header("Skill damage data")]
    [SerializeField] protected bool isTick;         // tick ��ų����
    [SerializeField] private float tickTime;        // tick ��ų�̶�� ���ʸ��� �������� �ٰ�����
    [SerializeField] private float duration;        // ��ų ���ӽð�
    [SerializeField] private float damage;          // ��ų�� ������

    [SerializeField] private float range;           // �Ǻ��� ���� ũ��
    [SerializeField] private Vector3 distance;      // ���� �Ÿ�
    [SerializeField] private Quaternion rotation;   // ���� ȸ��
    [SerializeField] private float delay;           // ������ ������ ��ų ���� �� ���Ŀ� ���۵���

    [Header("Skill data")]
    [SerializeField] protected GameObject effect;
    private GameObject currentEffect;

    [SerializeField] private float coolTime;
    private string animBool;
    private float timer;

    protected WaitForSeconds waitTickDelay;
    protected WaitForSeconds waitDelay;

    private void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    public void Init(Player player, string animBool)
    {
        this.player = player;
        this.animBool = animBool;

        waitDelay = new WaitForSeconds(delay);
        waitTickDelay = new WaitForSeconds(tickTime);
    }

    public void UseSkill()
    {
        if (SkillCondition() && CanUse())
        {
            StartCoroutine(SkillMainRoutine());
            timer = coolTime;
        }
    }
    public void CancelSkill()
    {
        if (skillRoutine != null)
        {
            StopCoroutine(skillRoutine);
            DestroyEffect();
            skillRoutine = null;
        }
    }

    private bool CanUse() => timer <= 0f;

    protected abstract bool SkillCondition();
    private IEnumerator SkillMainRoutine()
    {
        OnStartSkill();

        skillRoutine ??= StartCoroutine(PlaySkillRoutine());
        yield return StartCoroutine(WaitSkillDurationRoutine());

        OnEndSkill();
    }

    /// <summary>
    /// �ݺ��Ǵ� ��ƾ
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator PlaySkillRoutine();

    /// <summary>
    /// ��ų�� ������ ��ƾ ��� �ݺ����� ��ų�� ���ӽð��� ������ ����.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitSkillDurationRoutine()
    {
        yield return new WaitForSeconds(duration);
        CancelSkill();
    }

    protected void DealDamageToTargets()
    {
        foreach (var hit in GetTargets())
        {
            if (hit.TryGetComponent(out IDamagable target))
            {
                target.TakeDamage(player.statCon, damage);
            }
        }
    }

    private void OnStartSkill() => anim.SetBool(animBool, true);
    private void OnEndSkill() => anim.SetBool(animBool, false);

    private Collider[] GetTargets() { return overlap.GetOverlap(currentEffect.transform.position, distance, range, rotation); }

    protected void CreateEffect() { currentEffect = GameManager.Pool.Get(effect, transform.position, transform.rotation); }

    protected void CreateEffect(float delay)
    {
        StartCoroutine(DelayCreateEffect(transform.position, Quaternion.identity, delay));
    }

    private IEnumerator DelayCreateEffect(Vector3 position, Quaternion rotation, float delay)
    {
        yield return new WaitForSeconds(delay);

        currentEffect = GameManager.Pool.Get(effect, position, rotation);
    }

    private void DestroyEffect() { GameManager.Pool.Release(currentEffect); }

    private void OnDrawGizmos()
    {
        if(overlap != null && currentEffect != null)
        {
            overlap.DrawGizmos(currentEffect.transform.position, distance, range, rotation);
        }
        if(isGizmos)
        {
            overlap.DrawGizmos(transform.position, distance, range, rotation);
        }
    }
}
