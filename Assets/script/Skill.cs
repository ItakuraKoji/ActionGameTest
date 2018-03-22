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

    //スキルの使用回数
    public static int[] quantity = new int [(int)SkillType.MAX];
    public Skill()
    {
        for (int i = 0; i < (int)SkillType.MAX; ++i)
        {
            quantity[i] = 1;
        }
    }
    public void HighJump(ref float jumppow,bool isJump)
    {
        ////四角ボタンを押した後にR1押すとハイジャンプ
        //ジャンプ力
        //着地フラグ
        //戻り値なし
        if (Input.GetButtonDown("Fire2") && quantity[(int)SkillType.HIGH_JUMP] > 0 && !isJump)   
        {
            jumppow = 2.0f;
            --quantity[1]; 
        }
        else if(isJump)
        {
            jumppow = 1;    //飛んだら元のジャンプ力にもどす
        }
       

    }
    public void Slash()
    {
        if (Input.GetButtonDown("Attack") && quantity[(int)SkillType.SLASH] > 0)  //Yボタン
        {
            Debug.Log("切った");
            --quantity[(int)SkillType.SLASH];
        }
    }
}
