using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._3D.Misc.Room;
using Assets.Scripts._3D.Selecting;
using UnityEngine;

public class DotResponce : RoomBaseBehaviour, ISelectionResponse
{
    [SerializeField] private RectTransform dotTransform;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private Vector2 initialDelta;

    private void Awake()
    {
        BaseInit();
        initialDelta = dotTransform.sizeDelta;
        dotTransform.gameObject.SetActive(false);
    }
    public void OnDeselect(IRoomSelectable selection)
    {
       dotTransform.gameObject.SetActive(false);
    }

    public void OnSelect(IRoomSelectable selection)
    {
        dotTransform.gameObject.SetActive(true);

        SetSize(selection);
    }

    private void SetSize(IRoomSelectable selection)
    {
        Vector3 adjustedPosition = selection.HitPosition;
        adjustedPosition.y = PlayerCamera.transform.position.y;
        float distance = Vector3.Distance(adjustedPosition, PlayerCamera.transform.position);
        float fraction = 1 - Remap01(distance, minDistance, maxDistance);
        dotTransform.sizeDelta = initialDelta * fraction;
    }

    private float Remap01(float value, float min, float max)
    {
        float absVal = value - min;
        float absMax = max - min;
        float normal = absVal / absMax;

        return normal;
    }
}
