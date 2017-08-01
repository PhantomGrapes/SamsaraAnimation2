using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenModeController : MonoBehaviour {
    public Toggle windows;
    public Toggle full;

	public void ChangeModeWindows()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ChangeModeFull()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
