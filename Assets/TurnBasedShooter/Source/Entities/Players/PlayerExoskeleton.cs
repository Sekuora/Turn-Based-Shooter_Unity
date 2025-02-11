using UnityEngine;

public class PlayerExoskeleton : MonoBehaviour
{
    
    // Exoskeleton Systems
    [SerializeField] private PlayerMovementSystem _playerMovementSystem;
   
    
    private void Awake()
    {
        //_playerMovementSystem = GetComponent<PlayerMovementSystem>();
    }

    public PlayerMovementSystem PlayerMovementSystem
    {
        get { return _playerMovementSystem; }
        set { _playerMovementSystem = value; }
    }


}
