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
    [Tooltip("��ų ������ ����")]
    [SerializeField] private float range;

    [Tooltip("��ų �������� ������ ���� ��ġ ����")]
    [SerializeField] private Vector3 distance;

    [Tooltip("��ų ���� �� ���� �� ������ ������ ��������")]
    [SerializeField] private float delay;

    [SerializeField] private Quaternion rotation;

    [Header("Skill damage data")]
    [Tooltip("��ų�� Tick ������ ������ ����")]
    public bool isTick;

    [Tooltip("��ų�� �������� �ִ����� ����")]
    public bool isMove;

    [Tooltip("Tick �������� ���� ���� (��)")]
    [SerializeField] private float tickTime;

    [Tooltip("��ų�� �ӵ�")]
    [SerializeField] private Vector3 velocity;

    [SerializeField] private float damage;

    [SerializeField] private float coolTime;

    [Tooltip("��ų�� ���ӽð� (���ӽð� �Ŀ��� �ݵ�� ��ų�� �����)")]
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
