using System;
using UnityEngine;

public class CameraSwitchSystem : PlayerExoskeleton
{

    [SerializeField] private GameObject actionCameraGameObject;

    private void Awake()
    {
        PrimalAction.OnAnyActionStart += OnAnyActionStart_Event;
        PrimalAction.OnAnyActionComplete += OnAnyActionComplete_Event;
    }

    override protected void Start()
    {
        HideActionCamera();
    }

    // Any Action Starts
    private void OnAnyActionStart_Event(object sender, EventArgs e)
    {
        switch(sender)
        {
            case ShootAction shootAction:
                Player playerUnit = shootAction.Player;
                Player targetUnit = shootAction.TargetUnit;

                Vector3 cameraCharacterHeight = Vector3.up * playerUnit.Height;

                Vector3 shootDirection = (targetUnit.GetWorldPosition() - playerUnit.GetWorldPosition()).normalized;

                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDirection * shoulderOffsetAmount;

                Vector3 actionCameraPosition = playerUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset + (shootDirection * -1);

                actionCameraGameObject.transform.position = actionCameraPosition;
                actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);

                ShowActionCamera();
                break;
        }
    }

    // Any Action Ends
    private void OnAnyActionComplete_Event(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }

    private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGameObject.SetActive(false);
    }

}
