using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnim : MonoBehaviour {

    //オブジェクトのタグを判別し、そのタグのオブジェクトを生成する
    public GameObject attackObj;    //生成する敵オブジェ
    public Transform GeneratPos;    //生成するキャラの位置
    public Vector3 GeneratAngle;    //生成するキャラの向き

    //敵の生成処理
    //生成する「位置・向き」を外部から設定できる
    //デフォルトにするには、位置に[自身の位置]
    //向きに[Vector3(0,0,0)]を入れる
    public void    CreateEnemy()
    {
       Vector3 pos = new Vector3(
           GeneratPos.position.x,
           GeneratPos.position.y,
           GeneratPos.position.z);
        Vector3 angle = new Vector3(
            GeneratAngle.x,
            GeneratAngle.y,
            GeneratAngle.z);
       Instantiate(attackObj, pos, Quaternion.Euler(angle));
    }


    //-------------------------------------------------------
    //位置・向きを設定して生成する
    //-------------------------------------------------------
    public void    CreateObj(Vector3 pos,Vector3 angle)
    {
        Instantiate(attackObj, pos, Quaternion.Euler(angle));
    }


    //----------------------------------------------
    //オブジェクトの種類・位置・向きを設定して生成する
    //----------------------------------------------
    void    CreateMotion(GameObject obj,Vector3 pos,Vector3 angle)
    {
        Instantiate(obj, pos, Quaternion.Euler(angle));
    }
}
