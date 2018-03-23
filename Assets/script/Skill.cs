using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//スキルの実態を管理するクラス
//今のところは1回使ったら使用できなくなる
public enum SkillType
{
   NONE,
   HIGH_JUMP,
   PUNCH,
   SLASH,
   MAX
};

public class Skill: MonoBehaviour{

    public GameObject playBackObj;
    private CreateAnim animObj;
    
    public void Start()
    {
        for (int i = 0; i < (int)SkillType.MAX; ++i)
        {
            quantity[i] = 1;
        }
        animObj = playBackObj.GetComponent<CreateAnim>();
    }
    //スキルの使用回数
    public static int[] quantity = new int [(int)SkillType.MAX];
    
    public void HighJump(ref float jumppow,bool isJump)
    {
        ////四角ボタンを押した後にR1押すとハイジャンプ
        //ジャンプ力
        //着地フラグ
        //戻り値なし
        if (Input.GetButtonDown("Fire2") && quantity[(int)SkillType.HIGH_JUMP] > 0 && !isJump)   
        {
            jumppow = 2.0f;
            --quantity[(int)SkillType.HIGH_JUMP]; 
        }
        else if(isJump)
        {
            jumppow = 1;    //飛んだら元のジャンプ力にもどす
        }
       

    }
    public void Slash(Vector3 pos,Vector3 angle)
    {
        if (Input.GetButtonDown("Attack") && quantity[(int)SkillType.SLASH] > 0)  //Yボタン
        {
            Debug.Log("切った");
            --quantity[(int)SkillType.SLASH];
            animObj.CreateObj(pos, angle);
        }
    }
}
