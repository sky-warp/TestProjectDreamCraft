using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "EnemySet")]
public class EnemySet : ScriptableObject
{
    public EnemySpawnData[] EnemySpawnDatas;
}