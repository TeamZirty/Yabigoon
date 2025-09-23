using UnityEngine;

public class SpawnControllerUI : MonoBehaviour
{
    [Tooltip("Drag the Monster Spawner from your scene here.")]
    public MonsterSpawner monsterSpawner;

    [Tooltip("The amount to decrease the spawn interval by (making it faster).")]
    public float changeAmount = 1f;

    public void OnFasterButtonClick()
    {
        if (monsterSpawner != null)
        {
            monsterSpawner.DecreaseInterval(changeAmount);
        }
        else
        {
            Debug.LogError("Monster Spawner is not assigned in the SpawnControllerUI!");
        }
    }

    public void OnSlowerButtonClick()
    {
        if (monsterSpawner != null)
        {
            monsterSpawner.IncreaseInterval(changeAmount);
        }
        else
        {
            Debug.LogError("Monster Spawner is not assigned in the SpawnControllerUI!");
        }
    }
}
