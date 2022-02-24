using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Win_State_Manager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI win_message;
    [SerializeField] string blue_win_message = "PLAYER : BLUE";
    [SerializeField] string orange_win_message = "PLAYER : ORANGE";
    [SerializeField] GameObject orangeTank;
    [SerializeField] GameObject blueTank;
    [SerializeField] Image background;
    [SerializeField] Color orange_color;
    [SerializeField] Color blue_color;
    bool game_is_over = false;
    void Start(){
        orangeTank = GameObject.Find("orange_oiltank");
        blueTank = GameObject.Find("blue_oiltank");
        if (orangeTank == null || blueTank == null){
            Debug.LogError("COULD NOT FIND TANK OBJECTS IN SCENE");
        }
    }

    void Update(){
        if (game_is_over == false)
        {
            if (orangeTank == null ){
                win_message.text = blue_win_message;
                background.color = blue_color;
                set_trigger_and_destroy();
            }

            if (blueTank == null ){
                win_message.text = orange_win_message;
                background.color = orange_color;
                set_trigger_and_destroy();
            }
        }
        else{
            if (Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void set_trigger_and_destroy(){
            anim.SetTrigger("end_game");
            game_is_over = true;
    }
}
