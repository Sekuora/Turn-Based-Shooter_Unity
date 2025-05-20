using System;
using UnityEngine;

public class UnitAnimatorSystem : PlayerExoskeleton
{

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform bulletProjectile;

    [SerializeField]
    private Transform bulletSpawnPosition;

    private MoveAction moveAction;
    private ShootAction shootAction;


    override protected void Start()
    {

        moveAction = Player.GetAction<MoveAction>();
        shootAction = Player.GetAction<ShootAction>();

        moveAction.OnStartMoving += OnStartMoving_Event;
        moveAction.OnStopMoving += OnStopMoving_Event;

        shootAction.OnShoot += OnShoot_Event;

    }

    private void OnShoot_Event(object sender, EventArgs e)
    {
        animator.SetTrigger("Shoot");

         // Instantiate bullet at bullet spawn position
        Transform bulletProjectileTransform = Instantiate(bulletProjectile, bulletSpawnPosition.transform.position, Quaternion.identity);

    }

    private void OnStartMoving_Event(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }
    private void OnStopMoving_Event(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }

}
