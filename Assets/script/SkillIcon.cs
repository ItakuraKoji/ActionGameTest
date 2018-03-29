using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour {

    public GameObject[] ButtonInfo;
    public PlayerControler player;

    //もうごり押しよ
    public Sprite noneIcon;
    public Sprite slashIcon;
    public Sprite jumpIcon;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < this.ButtonInfo.Length; ++i)
        {
            this.ButtonInfo[i].GetComponent<Image>().sprite = this.noneIcon;
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {
        for (int i = 0; i < ButtonInfo.Length; ++i)
        {
            switch (player.GetSkill(i).type)
            {
                case SkillType.NONE:
                    this.ButtonInfo[i].GetComponent<Image>().sprite = this.noneIcon;
                    break;
                case SkillType.HIGH_JUMP:
                    this.ButtonInfo[i].GetComponent<Image>().sprite = this.jumpIcon;
                    break;
                case SkillType.SLASH:
                    this.ButtonInfo[i].GetComponent<Image>().sprite = this.slashIcon;
                    break;
            }
            this.ButtonInfo[i].transform.GetChild(0).GetComponent<Text>().text = player.GetSkill(i).numUsage.ToString();
        }
        string[] button = { "Play1", "Play2", "Play3", "Play4"};
        for(int i = 0; i < 4; ++i)
        {
            //押したボタンをわかりやすくする
            if (Input.GetButton(button[i]))
            {
                this.ButtonInfo[i].transform.localScale = new Vector3(1.0f, 1.0f);
            }
            else
            {
                this.ButtonInfo[i].transform.localScale = new Vector3(0.5f, 0.5f);
            }
        }

    }
}
