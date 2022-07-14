using System.Collections;
using System.Collections.Generic;using Assets.Scripts._3D.Misc.Room;
using Assets.Scripts._3D.Selecting;
using UnityEngine;

public class Interactor : RoomBaseBehaviour, ISelectionResponse
{
    private IInteractable interactable;

    private void Awake()
    {
        BaseInit();
        FpsControls.Player.Primary.performed += TryInteract;
    }

    private void OnDestroy()
    {
        FpsControls.Player.Primary.performed -= TryInteract;
    }

    private void TryInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        TryInteract();
    }

    public void OnSelect(IRoomSelectable selection)
    {
        interactable = selection as IInteractable;
    }

    public void OnDeselect(IRoomSelectable selection)
    {
        interactable = null;
    }

    public void TryInteract()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
