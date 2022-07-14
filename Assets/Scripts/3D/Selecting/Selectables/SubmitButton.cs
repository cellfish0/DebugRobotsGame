using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._3D.Selecting;
using UnityEngine;

public class SubmitButton : MonoBehaviour, IRoomSelectable
{
    public string GetHint()
    {
        return "Submit!";
    }

    public Vector3 Position => transform.position;
    public Vector3 HitPosition { get; set; }
}
