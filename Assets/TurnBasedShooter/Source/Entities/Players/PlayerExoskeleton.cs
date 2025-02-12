// Copyright(c) 2025 Fyragic. All rights reserved.
using UnityEngine;

public class PlayerExoskeleton : MonoBehaviour
{
    
    // Exoskeleton Systems
    [SerializeField] 
    private Player player;
   
    
    private void Awake()
    {
        // Reference to Player
        player = GetComponent<Player>();
        
    }

    virtual protected void Start()
    {
        Player = player;
    }

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }


}
