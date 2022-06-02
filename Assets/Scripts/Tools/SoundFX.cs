using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public static SoundFX instance;
    private void Awake() => instance = this;

    //Maquinas
    [SerializeField] AudioClip Pinche_Open;
    [SerializeField] AudioClip Pinche_Close;
    [SerializeField] AudioClip DoorStartMovement;
    [SerializeField] AudioClip DoorStopMovement;
    [SerializeField] AudioClip TouchHandle_Go;
    [SerializeField] AudioClip TouchHandle_Back;
    [SerializeField] AudioClip Cut_the_rope;
    [SerializeField] AudioClip saw_hit_someting;
    [SerializeField] AudioClip saw_hit_capi;
    [SerializeField] AudioClip shooter_shoot;
    [SerializeField] AudioClip arrow_hit_and_destroy;
    [SerializeField] AudioClip arrow_Sticks;
    [SerializeField] AudioClip arrow_hit_capi;
    [SerializeField] AudioClip tic;
    [SerializeField] AudioClip tac;
    [SerializeField] AudioClip leaves_touch;

    //capi interact
    [SerializeField] AudioClip capi_button_touch_begin;
    [SerializeField] AudioClip capi_button_touch_end;
    [SerializeField] AudioClip capi_recolect;
    [SerializeField] AudioClip capi_SplashWater;
    [SerializeField] AudioClip capi_leaves_move;

    //Drag
    [SerializeField] AudioClip begin_drag_wood;
    [SerializeField] AudioClip end_drag_wood;
    [SerializeField] AudioClip begin_drag_steel;
    [SerializeField] AudioClip end_drag_steel;

    //Capi
    [SerializeField] AudioClip Blood_Splash;
    [SerializeField] AudioClip capi_death;
    [SerializeField] AudioClip capi_footsepA; //tal vez pensar en un sequencer para que no pisen todos el mismo sonido
    [SerializeField] AudioClip capi_footsepB;
    [SerializeField] AudioClip capi_spawn;
    [SerializeField] AudioClip capi_sucessfull;

    //UI
    [SerializeField] AudioClip ui_mouse_over;
    [SerializeField] AudioClip ui_click_simple;
    [SerializeField] AudioClip ui_click_double;
    [SerializeField] AudioClip ui_Transition_01;
    [SerializeField] AudioClip ui_Transition_02;
    [SerializeField] AudioClip ui_Transition_03;
    [SerializeField] AudioClip ui_Bird_On;
    [SerializeField] AudioClip ui_Bird_Off;
    

    //GameLoop
    [SerializeField] AudioClip gameWin;
    [SerializeField] AudioClip gameLose;

    


    [SerializeField] AudioClip Glitter_Normal;
    [SerializeField] AudioClip Glitter_Long;

    public void Start()
    {
        //MAQUINAS
        if (Pinche_Open)                        AudioManager.instance.GetSoundPoolFastRegistry2D(Pinche_Open);
        if (Pinche_Close)                       AudioManager.instance.GetSoundPoolFastRegistry2D(Pinche_Close);
        if (DoorStartMovement)                  AudioManager.instance.GetSoundPoolFastRegistry2D(DoorStartMovement);
        if (DoorStopMovement)                   AudioManager.instance.GetSoundPoolFastRegistry2D(DoorStopMovement);
        if (TouchHandle_Go)                     AudioManager.instance.GetSoundPoolFastRegistry2D(TouchHandle_Go);
        if (TouchHandle_Back)                   AudioManager.instance.GetSoundPoolFastRegistry2D(TouchHandle_Back);
        if (Cut_the_rope)                       AudioManager.instance.GetSoundPoolFastRegistry2D(Cut_the_rope);
        if (saw_hit_someting)                   AudioManager.instance.GetSoundPoolFastRegistry2D(saw_hit_someting);
        if (saw_hit_capi)                       AudioManager.instance.GetSoundPoolFastRegistry2D(saw_hit_capi);
        if (shooter_shoot)                      AudioManager.instance.GetSoundPoolFastRegistry2D(shooter_shoot);
        if (arrow_hit_and_destroy)              AudioManager.instance.GetSoundPoolFastRegistry2D(arrow_hit_and_destroy);
        if (arrow_hit_capi)                     AudioManager.instance.GetSoundPoolFastRegistry2D(arrow_hit_capi);
        if (arrow_Sticks)                       AudioManager.instance.GetSoundPoolFastRegistry2D(arrow_Sticks);
        if (tic)                                AudioManager.instance.GetSoundPoolFastRegistry2D(tac);
        if (tic)                                AudioManager.instance.GetSoundPoolFastRegistry2D(tac);
        if (leaves_touch)                       AudioManager.instance.GetSoundPoolFastRegistry2D(leaves_touch);

        //capi interact
        if (capi_button_touch_begin)            AudioManager.instance.GetSoundPoolFastRegistry2D(capi_button_touch_begin);
        if (capi_button_touch_end)              AudioManager.instance.GetSoundPoolFastRegistry2D(capi_button_touch_end);
        if (capi_recolect)                      AudioManager.instance.GetSoundPoolFastRegistry2D(capi_recolect);
        if (capi_SplashWater)                   AudioManager.instance.GetSoundPoolFastRegistry2D(capi_SplashWater);
        if (capi_leaves_move)                   AudioManager.instance.GetSoundPoolFastRegistry2D(capi_leaves_move);

        //drag
        if (begin_drag_wood)                    AudioManager.instance.GetSoundPoolFastRegistry2D(begin_drag_wood);
        if (end_drag_wood)                      AudioManager.instance.GetSoundPoolFastRegistry2D(end_drag_wood);
        if (begin_drag_steel)                   AudioManager.instance.GetSoundPoolFastRegistry2D(begin_drag_steel);
        if (end_drag_steel)                     AudioManager.instance.GetSoundPoolFastRegistry2D(end_drag_steel);

        //capi
        if (Blood_Splash)                       AudioManager.instance.GetSoundPoolFastRegistry2D(Blood_Splash);
        if (capi_death)                         AudioManager.instance.GetSoundPoolFastRegistry2D(capi_death);
        if (capi_footsepA)                      AudioManager.instance.GetSoundPoolFastRegistry2D(capi_footsepA);
        if (capi_footsepB)                      AudioManager.instance.GetSoundPoolFastRegistry2D(capi_footsepB);
        if (capi_spawn)                         AudioManager.instance.GetSoundPoolFastRegistry2D(capi_spawn);
        if (capi_sucessfull)                    AudioManager.instance.GetSoundPoolFastRegistry2D(capi_sucessfull);

        //ui
        if (ui_mouse_over)                      AudioManager.instance.GetSoundPoolFastRegistry2D(ui_mouse_over);
        if (ui_click_simple)                    AudioManager.instance.GetSoundPoolFastRegistry2D(ui_click_simple);
        if (ui_click_double)                    AudioManager.instance.GetSoundPoolFastRegistry2D(ui_click_double);
        if (ui_Transition_01)                   AudioManager.instance.GetSoundPoolFastRegistry2D(ui_Transition_01);
        if (ui_Transition_02)                   AudioManager.instance.GetSoundPoolFastRegistry2D(ui_Transition_02);
        if (ui_Transition_03)                   AudioManager.instance.GetSoundPoolFastRegistry2D(ui_Transition_03);
        if (ui_Bird_On)                         AudioManager.instance.GetSoundPoolFastRegistry2D(ui_Bird_On);
        if (ui_Bird_Off)                        AudioManager.instance.GetSoundPoolFastRegistry2D(ui_Bird_Off);


        //gameloop
        if (gameWin)                            AudioManager.instance.GetSoundPoolFastRegistry2D(gameWin);
        if (gameLose)                           AudioManager.instance.GetSoundPoolFastRegistry2D(gameLose);
        
        if (Glitter_Normal)                     AudioManager.instance.GetSoundPoolFastRegistry2D(Glitter_Normal);
        if (Glitter_Long)                       AudioManager.instance.GetSoundPoolFastRegistry2D(Glitter_Long);
    }

    #region Sin integrar

    //MAQUINAS
    public static void Play_Pinche_Open() => AudioManager.instance.PlaySound(instance.Pinche_Open.name);
    public static void Play_Pinche_Close() => AudioManager.instance.PlaySound(instance.Pinche_Close.name);
    public static void Play_DoorStartMovement() => AudioManager.instance.PlaySound(instance.DoorStartMovement.name);
    public static void Play_DoorStopMovement() => AudioManager.instance.PlaySound(instance.DoorStopMovement.name);
    public static void Play_TouchHandle_Go() => AudioManager.instance.PlaySound(instance.TouchHandle_Go.name);
    public static void Play_TouchHandle_Back() => AudioManager.instance.PlaySound(instance.TouchHandle_Back.name);
    public static void Play_Cut_the_rope() => AudioManager.instance.PlaySound(instance.Cut_the_rope.name);
    public static void Play_saw_hit_someting() => AudioManager.instance.PlaySound(instance.saw_hit_someting.name);
    public static void Play_saw_hit_capi() => AudioManager.instance.PlaySound(instance.saw_hit_capi.name);
    public static void Play_shooter_shoot() => AudioManager.instance.PlaySound(instance.shooter_shoot.name);
    public static void Play_arrow_hit_destroy() => AudioManager.instance.PlaySound(instance.arrow_hit_and_destroy.name);
    public static void Play_arrow_hit_capi() => AudioManager.instance.PlaySound(instance.arrow_hit_capi.name);
    public static void Play_arrow_Sticks() => AudioManager.instance.PlaySound(instance.arrow_Sticks.name);
    public static void Play_Tic() => AudioManager.instance.PlaySound(instance.tic.name);
    public static void Play_Tac() => AudioManager.instance.PlaySound(instance.tac.name);
    public static void Play_LeavesTouch() => AudioManager.instance.PlaySound(instance.leaves_touch.name);

    //capi interact
    public static void Play_capi_button_touch_begin() => AudioManager.instance.PlaySound(instance.capi_button_touch_begin.name);
    public static void Play_capi_button_touch_end() => AudioManager.instance.PlaySound(instance.capi_button_touch_end.name);
    public static void Play_capi_recolect() => AudioManager.instance.PlaySound(instance.capi_recolect.name);
    public static void Play_capi_Splash_Water() => AudioManager.instance.PlaySound(instance.capi_SplashWater.name);
    public static void Play_capi_Leaves_Move() => AudioManager.instance.PlaySound(instance.capi_leaves_move.name);

    //drag
    public static void Play_begin_drag_wood() => AudioManager.instance.PlaySound(instance.begin_drag_wood.name);
    public static void Play_end_drag_wood() => AudioManager.instance.PlaySound(instance.end_drag_wood.name);
    public static void Play_begin_drag_steel() => AudioManager.instance.PlaySound(instance.begin_drag_steel.name);
    public static void Play_end_drag_steel() => AudioManager.instance.PlaySound(instance.end_drag_steel.name);

    //capi
    public static void Play_Blood_Splash() => AudioManager.instance.PlaySound(instance.Blood_Splash.name);
    public static void Play_capi_death() => AudioManager.instance.PlaySound(instance.capi_death.name);
    public static void Play_capi_footsepA() => AudioManager.instance.PlaySound(instance.capi_footsepA.name);
    public static void Play_capi_footsepB() => AudioManager.instance.PlaySound(instance.capi_footsepB.name);
    public static void Play_capi_spawn() => AudioManager.instance.PlaySound(instance.capi_spawn.name);
    public static void Play_capi_sucessfull() => AudioManager.instance.PlaySound(instance.capi_sucessfull.name);

    //ui
    public static void Play_ui_mouse_over() => AudioManager.instance.PlaySound(instance.ui_mouse_over.name);
    public static void Play_ui_click_simple() => AudioManager.instance.PlaySound(instance.ui_click_simple.name);
    public static void Play_ui_click_double() => AudioManager.instance.PlaySound(instance.ui_click_double.name);
    public static void Play_ui_Transition_01() => AudioManager.instance.PlaySound(instance.ui_Transition_01.name);
    public static void Play_ui_Transition_02() => AudioManager.instance.PlaySound(instance.ui_Transition_02.name);
    public static void Play_ui_Transition_03() => AudioManager.instance.PlaySound(instance.ui_Transition_03.name);
    public static void Play_ui_Bird_On() => AudioManager.instance.PlaySound(instance.ui_Bird_On.name);
    public static void Play_ui_Bird_Off() => AudioManager.instance.PlaySound(instance.ui_Bird_Off.name);

    //gameloop
    public static void Play_gameWin() => AudioManager.instance.PlaySound(instance.gameWin.name);
    public static void Play_gameLose() => AudioManager.instance.PlaySound(instance.gameLose.name);
    
    public static void Play_Glitter_Normal() => AudioManager.instance.PlaySound(instance.Glitter_Normal.name);
    public static void Play_Glitter_Long() => AudioManager.instance.PlaySound(instance.Glitter_Long.name);
    #endregion

    #region integrados

    #endregion

}
