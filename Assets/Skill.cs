using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int quantity = 1;
    public void HighJump(ref float jumppow,bool hitGround)
    {
        
        if (Input.GetButtonDown("Fire2") && quantity > 0)   //四角ボタンとR1同時押しでハイ
        {
            jumppow *= 2.0f;
            --quantity;
        }
        else if(hitGround)
        {
            jumppow = 1;
        }  
    }
}
