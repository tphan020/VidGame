using UnityEngine;
using System.Collections;

public class FighterSounds : StateMachineBehaviour   {

    //public FighterStates behaviorState;
    public AudioClip soundEffect;
    public AudioClip soundEffect1;
    public AudioClip soundEffect2;
    public AudioClip soundEffect3;
    public AudioClip soundEffect4;

    public AudioClip battleEffects;
    public AudioClip battleEffects1;
    public AudioClip battleEffects2;
    public AudioClip super;
    private AudioSource audio;

    protected Fighter fighter;
    public int randomnumber = 0;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fighter == null)
        {
            fighter = animator.gameObject.GetComponent<Fighter>();
        }
        audio = fighter.GetComponent<AudioSource>();
        if (soundEffect != null && soundEffect1 != null && soundEffect2 != null && soundEffect3 != null && soundEffect4 != null)
        {
            randomnumber = Random.Range(0, 4);
            if (randomnumber == 0)
            {
                audio.PlayOneShot(soundEffect, 1F);
            }
            if (randomnumber == 1)
            {
                audio.PlayOneShot(soundEffect1, 1F);
            }
            if (randomnumber == 2)
            {
                audio.PlayOneShot(soundEffect2, 1F);
            }
            if (randomnumber == 3)
            {
                audio.PlayOneShot(soundEffect3, 1F);
            }
            if (randomnumber == 4)
            {
                audio.PlayOneShot(soundEffect4, 1F);
            }
        }
        if (soundEffect != null && soundEffect1 != null && soundEffect2 != null && soundEffect3 != null && soundEffect4 == null)
        {
            randomnumber = Random.Range(0, 3);
            if (randomnumber == 0)
            {
                audio.PlayOneShot(soundEffect, 1F);
            }
            if (randomnumber == 1)
            {
                audio.PlayOneShot(soundEffect1, 1F);
            }
            if (randomnumber == 2)
            {
                audio.PlayOneShot(soundEffect2, 1F);
            }
            if (randomnumber == 3)
            {
                audio.PlayOneShot(soundEffect3, 1F);
            }

        }
        if (soundEffect != null && soundEffect1 != null && soundEffect2 != null && soundEffect3 == null && soundEffect4 == null)
        {
            randomnumber = Random.Range(0, 2);
            if (randomnumber == 0)
            {
                audio.PlayOneShot(soundEffect, 1F);
            }
            if (randomnumber == 1)
            {
                audio.PlayOneShot(soundEffect1, 1F);
            }
            if (randomnumber == 2)
            {
                audio.PlayOneShot(soundEffect2, 1F);
            }
        }
        if (soundEffect != null && soundEffect1 != null && soundEffect2 == null && soundEffect3 == null && soundEffect4 == null)
        {
            randomnumber = Random.Range(0, 1);
                if (randomnumber == 0)
                {
                    audio.PlayOneShot(soundEffect, 1F);
                }
                if (randomnumber == 1)
                {
                    audio.PlayOneShot(soundEffect1, 1F);
                }
            }
        if (soundEffect != null && soundEffect1 == null && soundEffect2 == null && soundEffect3 == null && soundEffect4 == null)
        {
                audio.PlayOneShot(soundEffect, 1F);
        }

        if (battleEffects!= null && battleEffects1!= null && battleEffects2!= null)
        {
            randomnumber = Random.Range(0, 2);
            if (randomnumber == 0)
            {
                audio.PlayOneShot(battleEffects, 1F);
            }
            if (randomnumber == 1)
            {
                audio.PlayOneShot(battleEffects1, 1F);
            }
            if (randomnumber == 2)
            {
                audio.PlayOneShot(battleEffects2, 1F);
            }
        }
        if (battleEffects != null && battleEffects1 != null && battleEffects2 == null)
        {
            randomnumber = Random.Range(0, 1);
            if (randomnumber == 0)
            {
                audio.PlayOneShot(battleEffects, 1F);
            }
            if (randomnumber == 1)
            {
                audio.PlayOneShot(battleEffects1, 1F);
            }
        }
        if (battleEffects != null && battleEffects1 == null && battleEffects2 == null)
        {
            audio.PlayOneShot(battleEffects, 1F);
        }
        if (super != null)
        {
            audio.PlayOneShot(super, 1F);
        }
    }

}
