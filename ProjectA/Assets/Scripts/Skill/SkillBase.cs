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
    [SerializeField] protected bool isTick;         // tick 스킬인지
    [SerializeField] private float tickTime;        // tick 스킬이라면 몇초마다 데미지를 줄것인지
    [SerializeField] private float duration;        // 스킬 지속시간
    [SerializeField] private float damage;          // 스킬의 데미지

    [SerializeField] private float range;           // 판별할 범위 크기
    [SerializeField] private Vector3 distance;      // 범위 거리
    [SerializeField] private Quaternion rotation;   // 범위 회전
    [SerializeField] private float delay;           // 데미지 판정이 스킬 시전 몇 초후에 시작될지

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
    /// 반복되는 루틴
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator PlaySkillRoutine();

    /// <summary>
    /// 스킬을 끝내는 루틴 계속 반복중인 스킬을 지속시간이 지나면 끝냄.
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
