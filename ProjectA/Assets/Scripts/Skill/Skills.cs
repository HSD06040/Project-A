using UnityEngine;

public class Skills : MonoBehaviour
{
    private Player player;

    #region skills
    public SwordSkill swordSkill { get; private set; }
    public MagicSkill slashSkill { get; private set; }

    #endregion
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        swordSkill = GetComponentInChildren<SwordSkill>();
        slashSkill = GetComponentInChildren<MagicSkill>();

        swordSkill.Init(player, "Sword");
        slashSkill.Init(player, "Sword");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            swordSkill.UseSkill();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            slashSkill.UseSkill();
    }
}
