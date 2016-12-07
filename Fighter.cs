using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fighter : MonoBehaviour {
    public enum PlayerType
    {
        HUMAN, AI
    };

    public static float MAX_HEALTH = 100f;

    public float health = MAX_HEALTH;
    private bool win = false;
    public string fighterName;
    public string thrust_but;
    public string hack_but;
    public string slash_but;
    public string restart_but;
    public string exit_but;
    public Fighter opponent;
    public PlayerType player;
    private AnimatorStateInfo currentstate;
    private int block = 0;
    private int hit = 0;
    private float attkStart = 0f;
    public float ThrustCD;
    public float HackCD;
    public float SlashCD;
    private float restart = 0f;
    private bool restartonce = true;
    private bool superUse1 = false;
    private bool superUse2 = false;
    private bool special = true;
    private bool ultTimer = false;
    private bool freezeani = false;
    private bool stanceswitchhack = true;
    private bool stanceswitchslash = true;
    private bool stanceswitchthrust = true;
    private bool firsthitlock = true;
    private int P1WinCount;
    private int P2WinCount;




    protected Animator animator;
    private Rigidbody myBody;
    private AudioSource audioPlayer;

    // Use this for initialization
    void Start() {
        myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    public void UpdateHumanInput()
    {
        superUse1 = false;
        superUse2 = false;
        if (freezeani == true)
        {
            StartCoroutine(AnimationWait(true));
            StartCoroutine(AnimationWait(false));
            freezeani = false;
        }

        if ((((Input.GetKeyDown(slash_but)) && Input.GetKeyDown(thrust_but))
              || (Input.GetKeyDown( hack_but) && Input.GetKeyDown(thrust_but)) ||
                 (Input.GetKeyDown(slash_but) && Input.GetKeyDown(hack_but))) && health <= 50
                 && special == true && ultTimer == false)
        {
            special = false;
            attkStart = Time.time;
            if (fighterName == "Davinia")
            {
                superUse1 = true;
            }
            if (fighterName == "Davinia2")
            {
                superUse2 = true;
            }
            animator.SetTrigger("Super");
        }



        else if (Input.GetKeyDown(thrust_but) && (attkStart + ThrustCD <= Time.time) && ultTimer == false)
        {
            if (stanceswitchthrust)
            {
                attkStart = Time.time - ThrustCD;
                stanceswitchthrust = false;
                stanceswitchhack = true;
                stanceswitchslash = true;
                animator.SetTrigger("Thrust");
            }
            else
            {
                attkStart = Time.time;
                animator.SetTrigger("Thrust");
            }
            firsthitlock = false;
        }
        else if (Input.GetKeyDown(hack_but) && (attkStart + HackCD <= Time.time) && ultTimer == false)
        {
            if (stanceswitchhack && (firsthitlock== false))
            {
                attkStart = Time.time - HackCD;
                stanceswitchthrust = true;
                stanceswitchhack = false;
                stanceswitchslash = true;
                animator.SetTrigger("Hack");
            }
            else
            {
                attkStart = Time.time;
                animator.SetTrigger("Hack");
            }
            firsthitlock = false;
        }
        else if (Input.GetKeyDown(slash_but) && (attkStart + SlashCD <= Time.time) && ultTimer == false)
        {
            if (stanceswitchslash)
            {
                attkStart = Time.time - SlashCD;
                stanceswitchthrust = true;
                stanceswitchhack = true;
                stanceswitchslash = false;
                animator.SetTrigger("Slash");
            }
            else
            {
                attkStart = Time.time;
                animator.SetTrigger("Slash");
            }
            firsthitlock = false;
        }
    }
    public void playSound(AudioClip sound){
        GameUtils.playSound(sound, audioPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(exit_but))
        {
            PlayerPrefs.SetInt("Player Score 1", 0);
            PlayerPrefs.SetInt("Player Score 2", 0);
            Application.Quit();
        }
        if (block == 1)
        {
            block = 0;
        }
        if (hit == 1 & health > 0)
        {
            hit = 0;
            animator.SetTrigger("Hit");
        }

        if (player == PlayerType.HUMAN)
        {
            UpdateHumanInput();
            currentstate = getCurrState();
        }
        if (win == true)
        {
            animator.SetTrigger("Victory");
        }

        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            P1WinCount = PlayerPrefs.GetInt("Player Score 1");
            P2WinCount = PlayerPrefs.GetInt("Player Score 2");
            /*if (restartonce == true)
            {
                restartonce = false;
                restart = Time.time;
            }
            if (restart + 7 <= Time.time)
            {
                restartonce = true;
                Application.LoadLevel(0);
            }*/
            if (P1WinCount < 2 && P2WinCount < 2)
            {
                if (restartonce == true)
                {
                    restartonce = false;
                    restart = Time.time;
                }
                if (restart + 7 <= Time.time)
                {
                    restartonce = true;
                    Application.LoadLevel(0);
                }
            }
            if (Input.GetKeyDown(restart_but) && (P1WinCount >=2 || P2WinCount>=2))
            {
                PlayerPrefs.SetInt("Music", 2);
                PlayerPrefs.SetInt("Player Score 1", 0);
                PlayerPrefs.SetInt("Player Score 2", 0);
                Application.LoadLevel(0);
            }
        }
        if (ultTimer)
        {
            StartCoroutine(UltWait(3.8f));
        }
    }
    IEnumerator UltWait(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        ultTimer = false;
        yield break;
    }

    IEnumerator AnimationWait(bool temp)
    {
        if (temp) {
            yield return new WaitForSeconds(.35f);
            animator.speed = 0;
            //animator.Stop();
            //animator.enabled = false;
            yield break;
        }
        else
        {
            if (fighterName == "Davinia") {
            yield return new WaitForSeconds(2.7f);
            animator.speed = 1.0f;
            //animator.enabled = true;
                yield break;
         }
            else if (fighterName == "Davinia2")
         {
             yield return new WaitForSeconds(3.5f);
           //  animator.enabled = true;
             animator.speed = 1.0f;
             yield break;
        }
        }
    }

public void UltTimer_Set()
    {
        ultTimer = true;
    }

    public void Freeze_animation()
    {
        freezeani = true;
    }

    public bool superUserRet()
    {
        return superUse1;
    }

    public bool superUserRet2()
    {
        return superUse2;
    }
    public float healthPercentage {
        get {
            return ((health / MAX_HEALTH)+.1f)*.9f;
        }

    }

    public float getCurrHealth()
    {
        return health;
    }
    public float setCurrHealth(float temp)
    {
        return health=temp;
    }
    public void setWin(bool temp)
    {
        win = temp;
    }
    public void set_block()
    {
        animator.SetTrigger("Block");
        block = 1;
    }
    public void set_hit()
    {
        hit = 1;
    }

    public AnimatorStateInfo getCurrState()
    {
        currentstate = animator.GetCurrentAnimatorStateInfo(0);
        return currentstate;
    }
    
   // public 

    public Rigidbody body
    {
        get
        {
            return this.myBody;
        }
    }

}
