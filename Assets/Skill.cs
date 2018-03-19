using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill
{
   HighJump,
   Punch,
   Slash,
};
public class SkillFunctions{
    
    public void HighJump(ref float jumppow)
    {
        jumppow *= 2;
    }
}

