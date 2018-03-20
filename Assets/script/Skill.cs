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
};

public class Skill {

    //スキルの使用回数
    public int quantity = 5;
    public void HighJump(ref float jumppow,bool isJump)
    {
        ////四角ボタンを押した後にR1押すとハイジャンプ
        //ジャンプ力
        //着地フラグ
        //戻り値なし
        if (Input.GetButtonDown("Fire2") && quantity > 0 && !isJump)   
        {
            jumppow = 2.0f;
            --quantity; 
        }
        else if(isJump)
        {
            jumppow = 1;    //飛んだら元のジャンプ力にもどす
        }  
    }
 
}
