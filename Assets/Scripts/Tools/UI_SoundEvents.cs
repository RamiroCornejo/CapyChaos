using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SoundEvents : MonoBehaviour
{
    public void EVENT_Play_Click_Simple() => SoundFX.Play_ui_click_simple();
    public void EVENT_Play_Click_Double() => SoundFX.Play_ui_click_double();
    public void EVENT_Play_MouseOver() => SoundFX.Play_ui_mouse_over();
    public void EVENT_Play_Bird_On() => SoundFX.Play_ui_Bird_On();
    public void EVENT_Play_Bird_Off() => SoundFX.Play_ui_Bird_Off();
    public void EVENT_Play_GameWin() => SoundFX.Play_gameWin();
    public void EVENT_Play_GameLose() => SoundFX.Play_gameLose();
}
