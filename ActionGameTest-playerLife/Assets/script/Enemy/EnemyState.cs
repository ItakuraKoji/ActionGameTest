using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//あたり判定から必ず取れる敵のスキルなどの情報
public class EnemyState : MonoBehaviour {
    SkillType nowDoing;


	// Use this for initialization
	void Start () {
        this.nowDoing = SkillType.NONE;
	}

    public void SetSkillType(SkillType type)
    {
        this.nowDoing = type;
    }
    public SkillType GetSkillType()
    {
        return this.nowDoing;
    }
}
