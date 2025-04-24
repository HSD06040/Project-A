using UnityEngine;

public class Skills : MonoBehaviour
{
    private Player player;

    #region skills
    public SwordSkill swordSkill { get; private set; }
    public MagicSkill magicSkill { get; private set; }
    public SlashSkill slashSkill { get; private set; }

    #endregion
    private void Awake()
    {
        player = GetComponentInParent<Player>();

        swordSkill = GetComponentInChildren<SwordSkill>();
        magicSkill = GetComponentInChildren<MagicSkill>();
        slashSkill = GetComponentInChildren<SlashSkill>();

        swordSkill.Init(player, "Sword");
        magicSkill.Init(player, "Sword");
        slashSkill.Init(player, "Sword");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            swordSkill.UseSkill();

        if (Input.GetKeyDown(KeyCode.Q))
            magicSkill.UseSkill();

        if(Input.GetKeyDown(KeyCode.R))
            slashSkill.UseSkill();
    }
}
