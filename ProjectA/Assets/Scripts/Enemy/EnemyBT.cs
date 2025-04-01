using System.Linq;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBT : MonoBehaviour
{
    protected BehaviorGraphAgent behaviorAgent;

    protected virtual void Start()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }

}
