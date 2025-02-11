using UnityEngine;

public class PlayerExoskeleton : MonoBehaviour
{
    
    // Exoskeleton Systems
    [SerializeField] private Player _playerMovementSystem;
   
    
    private void Awake()
    {
        //_playerMovementSystem = GetComponent<PlayerMovementSystem>();
    }

    public Player PlayerMovementSystem
    {
        get { return _playerMovementSystem; }
        set { _playerMovementSystem = value; }
    }


}
