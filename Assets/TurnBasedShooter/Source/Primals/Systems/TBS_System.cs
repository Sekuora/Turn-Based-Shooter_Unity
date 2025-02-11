using UnityEngine;

public class TBS_System : MonoBehaviour
{
    private PlayerExoskeleton player;

    protected PlayerExoskeleton GetPlayer()
    {
        return player = FindFirstObjectByType<PlayerExoskeleton>();
    }

    protected void Awake()
    {
        GetPlayer();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
