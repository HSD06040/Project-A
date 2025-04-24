using UnityEngine;

public enum PopUpType
{
    Damage, Crit, Heal
}
public class EntityFX : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject critPopUp;
    [SerializeField] private GameObject healPopUp;
    private PopUpType type;
    public void CreatePopUpText(string text, Vector3 position, PopUpType _type = PopUpType.Damage)
    {
        GameObject popUpPrefab;

        type = _type;

        switch (type)
        {
            case PopUpType.Crit:
                popUpPrefab = critPopUp;
                break;
            case PopUpType.Heal:
                popUpPrefab = healPopUp;
                break;
            case PopUpType.Damage:
                popUpPrefab = popUp;
                break;
            default:
                popUpPrefab = popUp;
                    break;
        }

        Vector3 offset = new Vector3(Random.Range(-.1f, .1f), 2 + Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
        GameObject popUpText = Instantiate(popUpPrefab, position + offset, Quaternion.identity);
        popUpText.GetComponent<PopUpText>().SetupText(text);
    }
}
