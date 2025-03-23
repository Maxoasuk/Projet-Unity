using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float autioVolume;
    
    void Awake()
    {
        var gms = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        
        Debug.Log(gms.Length);
        if (gms.Length > 1)
            DestroyImmediate(gameObject);
        else
            DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAutioVolume(float vol)
    {
        autioVolume = vol;
    }
}
