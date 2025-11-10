using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string sceneName;
    public Vector3 position;
    public float health;
    public int xp;
}

public class PlayerStatus : MonoBehaviour
{
    public float health = 100;
    public int xp = 0;

    public PlayerData GetSaveData()
    {
        return new PlayerData
        {
            sceneName = SceneManager.GetActiveScene().name,
            position = transform.position,
            health = health,
            xp = xp
        };
    }

    public void LoadFromData(PlayerData data)
    {
        transform.position = data.position;
        health = data.health;
        xp = data.xp;
    }
}