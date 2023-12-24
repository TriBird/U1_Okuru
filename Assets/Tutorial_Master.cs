using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_Master : MonoBehaviour{

    public List<Sprite> Tutorial_Sprite = new List<Sprite>();
    public Image Turorial_Image;
    public Text PageForward_Text;

    private int current_page = 1;

    public void page_forward(){
        current_page += 1;
        if(current_page >= 5){
            SceneManager.LoadScene("SampleScene");
        }else{
            if(current_page >= 4){
                PageForward_Text.text = "ゲームスタート";
            }
            Turorial_Image.sprite = Resources.Load<Sprite>("Tutorial/turorial_"+current_page);
        }
    }

}
