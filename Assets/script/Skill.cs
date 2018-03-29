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


    public AudioClip slashclip;
    private AudioSource slashSe;
    //スキルの使用回数
    public static int[] quantity = new int[(int)SkillType.MAX];

    public void Start()
    {
        this.slashSe = gameObject.AddComponent<AudioSource>();
        this.slashSe.clip = this.slashclip;

        for (int i = 0; i < (int)SkillType.MAX; ++i)
        {
            quantity[i] = 0;
        }
        animObj = playBackObj.GetComponent<CreateAnim>();
    }
    
    public int HighJump(ref float jumppow,bool isJump)
    {
        ////四角ボタンを押した後にR1押すとハイジャンプ
        //ジャンプ力
        //着地フラグ
        //戻り値なし
        if (!isJump)   
        {
            jumppow = 2.0f;
            --quantity[(int)SkillType.HIGH_JUMP];
        }
        //仮として、スキルの残数を返す
        return quantity[(int)SkillType.HIGH_JUMP];
    }
    public int Slash(Vector3 pos,Vector3 angle)
    {
        slashSe.Play();
        --quantity[(int)SkillType.SLASH];
        animObj.CreateObj(pos, angle);
        //仮として、スキルの残数を返す
        return quantity[(int)SkillType.SLASH];
    }
}
