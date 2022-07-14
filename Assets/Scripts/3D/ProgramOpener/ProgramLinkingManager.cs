using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._3D.Misc.Room;
using Assets.Scripts.Puzzles.Robo.Environment;
using UnityEngine;

public class ProgramLinkingManager : MonoBehaviour
{
    private Camera ProgramCamera => RoboScenePersistentObject.Instance.componentHolder.MainCam;
    private Camera PlayerCamera => RoomScenePersistentObject.Instance.componentHolder.PlayerCamera;

    public void OpenRoboProgram()
    {
        ProgramCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseRoboProgram()
    {
        ProgramCamera.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
