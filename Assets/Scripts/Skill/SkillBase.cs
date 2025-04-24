using System.Collections;
using UnityEngine;

public struct SkillVector
{
    public Vector3 forward;
    public Vector3 right;
    public Vector3 up;
}

public abstract class SkillBase : MonoBehaviour
{
    protected Player player;
    private Animator anim => player.anim;

    private SkillVector skillVec;

    private Coroutine skillRoutine;

    [SerializeField] private SkillOverlapDebugger debugger;
    public SkillOverlap overlap;

    [Header("Skill overlap data")]
    [Tooltip("스킬 범위의 넓이")]
    [SerializeField] private float range;

    [Tooltip("스킬 기준으로 범위의 생성 위치 조정")]
    [SerializeField] private Vector3 distance;

    [Tooltip("스킬 시전 후 몇초 후 데미지 판정이 시작할지")]
    [SerializeField] private float delay;

    [SerializeField] private Quaternion rotation;

    [Header("Skill damage data")]
    [Tooltip("스킬이 Tick 데미지 인지를 결정")]
    public bool isTick;

    [Tooltip("스킬이 움직임이 있는지를 결정")]
    public bool isMove;

    [Tooltip("Tick 데미지가 들어가는 간격 (초)")]
    [SerializeField] private float tickTime;

    [Tooltip("스킬의 속도")]
    [SerializeField] private Vector3 velocity;

    [SerializeField] private float damage;

    [SerializeField] private float coolTime;

    [Tooltip("스킬의 지속시간 (지속시간 후에는 반드시 스킬이 사라짐)")]
    [SerializeField] private float duration;

    [SerializeField] protected GameObject effect;


    private GameObject currentEffect;

    private string animBool;
    private float timer;

    protected WaitForSeconds waitTickDelay;
    protected WaitForSeconds waitDelay;

    private void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    private void OnValidate()
    {
        if (debugger == null) return;

        if(overlap.overlapType == OverlapType.Capsule)
        {
            debugger.capsule.gameObject.SetActive(true);
            debugger.box.gameObject.SetActive(false);
            debugger.sphere.gameObject.SetActive(false);
            debugger.capsule.center = new Vector3(0, 0.5f, 0) + distance;
            debugger.capsule.height = overlap.capsuleHeight;
            debugger.capsule.radius = range;
        }
        else if(overlap.overlapType == OverlapType.Box)
        {
            debugger.capsule.gameObject.SetActive(false);
            debugger.box.gameObject.SetActive(true);
            debugger.sphere.gameObject.SetActive(false);
            debugger.box.center = new Vector3(0, 0.5f, 0) + distance;
            debugger.box.size = overlap.boxRatio;
        }
        else
        {
            debugger.capsule.gameObject.SetActive(false);
            debugger.box.gameObject.SetActive(false);
            debugger.sphere.gameObject.SetActive(true);
            debugger.sphere.center = new Vector3(0, 0.5f, 0) + distance;
            debugger.sphere.radius = range;
        }
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

    private Collider[] GetTargets() { return overlap.GetOverlap(currentEffect.transform, distance, range, skillVec); }

    protected void CreateEffect() 
    { 
        currentEffect = GameManager.Pool.Get(effect, transform.position, transform.rotation);

        skillVec.forward    = currentEffect.transform.forward;
        skillVec.right      = currentEffect.transform.right;
        skillVec.up         = currentEffect.transform.up;

        currentEffect.transform.rotation *= rotation;
    }

    protected void CreateEffect(float delay)
    {
        StartCoroutine(DelayCreateEffect(delay));
    }

    private IEnumerator DelayCreateEffect(float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateEffect();
    }

    private void DestroyEffect() { GameManager.Pool.Release(currentEffect); }
}
