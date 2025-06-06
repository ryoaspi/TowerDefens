using UnityEngine;

[System.Serializable]
public class EnemyWaveData
{
    public GameObject m_enemyPrefab;
    public int m_count;
    public float m_spawnDelay;
}

[System.Serializable]
public class WaveData
{
    public EnemyWaveData[] m_enemiesInWave;
}

[CreateAssetMenu(fileName = "WaveSet", menuName = "Scriptable Objects/WaveSet")]
public class WaveSet : ScriptableObject
{
    public WaveData[] m_waves;
    public float m_waveDelay = 5f;
}
