using Assets.Scripts._3D.Misc.Room;
using Assets.Scripts._3D.Selecting;
using UnityEngine;



public class DotResponse : RoomBaseBehaviour, ISelectionResponse
{
    
    [SerializeField]private RectTransform dot;
    [SerializeField] private float minDistance = 0.3f;
    [SerializeField] private float maxDistance = 1f;
    private Vector2 initialScale;


    private void Awake()
    {
        BaseInit();
        initialScale = dot.sizeDelta;
    }

    public void OnDeselect(ISelectable selection)
    {
        dot.gameObject.SetActive(false);

    }

    public void OnSelect(ISelectable selection)
    {
        dot.gameObject.SetActive(true);
        
        ControlSize(selection.Position);
    }

    private void ControlSize(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(targetPosition, PlayerCamera.transform.position);
        float factor = distance.Remap(minDistance, 0, maxDistance, 1);

        //float dotProduct = Vector2.Dot(PlayerCamera.transform.forward, lookPos);
        //dotProduct = Mathf.Pow(dotProduct, Pow);
        dot.sizeDelta = initialScale * factor;
    }

}
