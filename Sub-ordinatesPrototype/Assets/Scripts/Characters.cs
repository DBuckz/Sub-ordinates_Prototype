using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "New Character")]
public class Characters : ScriptableObject
{
    public int type;
    public bool deity;


    public int health;
    public int speed;
    public int jumpForce;
    public int jumps;
    public int attack;
    public float attackRate;
    public float block;
    public int meleeStore;
    public float blockWait;
    public int characterNumber;
    public float attacktime;
    public Sprite characterSprite;
    public Sprite attackSprite;
    //public AnimationClip[] animations;

    //public Sprite[] blocking;
    //public Sprite[] idle;
    //public Sprite[] jumping;
    //public Sprite[] attacking;
    //public Sprite[] moving;


    


}
