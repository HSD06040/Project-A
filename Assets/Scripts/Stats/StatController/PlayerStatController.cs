using UnityEditor.Playables;
using UnityEngine;

public class PlayerStatController : CharacterStatController
{
    protected override void Start()
    {
        stat = GameManager.Data.playerStat;
        base.Start();
    }
}
