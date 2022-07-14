using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._3D.Selecting;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private ProgramLinkingManager programLinking;

    public string GetHint()
    {
        return "Open Program";
    }

    public Vector3 Position { get; }
    public Vector3 HitPosition { get; set; }
    public void Interact()
    {
        programLinking.OpenRoboProgram();
    }
}
