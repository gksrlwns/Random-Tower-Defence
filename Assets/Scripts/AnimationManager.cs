using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animation anim;

    public void PanelAnimation()
    {
        print("클림됨");
        anim.Play("Panel_01");
    }
}
