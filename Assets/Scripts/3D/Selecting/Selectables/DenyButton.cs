using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using Assets.Scripts._3D.Selecting;
using UnityEngine;

public class DenyButton : MonoBehaviour, IRoomSelectable
{
    public string GetHint()
    {
        return "Deny!";
    }

    public Vector3 Position => transform.position;
    public Vector3 HitPosition { get; set; }
}
