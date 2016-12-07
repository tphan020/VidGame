using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public float Thrust_dmg;
    public float Slash_dmg;
    public float Hack_dmg;
    public float Super_dmg;
    public float HP1;
    public float HP2;
    public Fighter P1;
    public Fighter P2;
    private AnimatorStateInfo P1Current;
    private AnimatorStateInfo P2Current;
    private int skipcheck=  0;
    private int skipcheck2 = 0;
    private string P1Check;
    private string P2Check;
    private bool Color = false;
    private bool P1Alt = true;
    private float P1ColorChange=0;
    private bool P2Alt = true;
    private float P2ColorChange = 0;
    private bool P1SuperAvail = true;
    private bool P2SuperAvail = true;
    private bool P1P2SuperFin = false;
    private bool P2P1SuperFin = false;
    public int finalScoreP1 = 0;
    public int finalScoreP2 = 0;
    private bool scorestop = false;

    public SpriteRenderer Sprite;
    public Image imgHp1;
    public Image imgHp2;
    public Image Bar;
    public Image imgHp1White;
    public Image imgHp2White;
    public Image Win0;
    public Image Win1;
    public Image Win2;
    public Image Win3;
    public Supers supers1;
    public Supers supers2;
    public BannerWins BannerP1;
    public BannerWins BannerP2;
    public AudioSource playMusic;
    public AudioClip backgroundMusic;
    public int musicloop = 0;
    public GameObject musicPlayer;


    // Use this for initialization

    void Awake()
    {
        musicloop = PlayerPrefs.GetInt("Music");
        if (musicloop == 1)
        {
            DestroyImmediate(playMusic);
        }
        if (musicloop == 0) {
            playMusic.name = "Take1";
            DontDestroyOnLoad(playMusic);
        }
        if (musicloop == 2)
        {
            DestroyImmediate(GameObject.Find("Take1"));
            PlayerPrefs.SetInt("Music", 1);
            playMusic.name = "Take1";
            DontDestroyOnLoad(playMusic);
        }

    }

    void Start () {
        musicloop = PlayerPrefs.GetInt("Music");
        Win0.color = new Color(255f, 255f, 255f, 0f);
        Win1.color = new Color(255f, 255f, 255f, 0f);
        Win2.color = new Color(255f, 255f, 255f, 0f);
        Win3.color = new Color(255f, 255f, 255f, 0f);
        finalScoreP1 = PlayerPrefs.GetInt("Player Score 1");
        finalScoreP2 = PlayerPrefs.GetInt("Player Score 2");
        if (finalScoreP1 == 1)
        {
            Win0.color = new Color(255f, 255f, 255f, 255f);
        }
        if (finalScoreP2 == 1)
        {
            Win2.color = new Color(255f, 255f, 255f, 255f);
        }
        //GameUtils.playSound(backgroundMusic, playMusic);
        playMusic.PlayOneShot(backgroundMusic, .3F);
    }
	
	// Update is called once per frame
	void Update () {
        P1Current = P1.getCurrState();
        P2Current = P2.getCurrState();
        check_attacks();
        win_conditions();
        if (Color == true)
        {
            StartCoroutine(changeCam(1.0f));
        }
        Update_wins();
        BarSuperchanges();
     
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Player Score 1", 0);
        PlayerPrefs.SetInt("Player Score 2", 0);
        PlayerPrefs.SetInt("Music", 0);
    }
    public void Update_wins()
    {
        if (scorestop == false)
        {
            if (HP2 <= 0)
            {
                if (finalScoreP1 == 1)
                {
                    finalScoreP1++;
                    Win1.color = new Color(255f, 255f, 255f, 255f);
                    BannerP1.triggerWin1();
                }
                else
                {
                    finalScoreP1 = 1;
                    Win0.color = new Color(255f, 255f, 255f, 255f);
                }
                PlayerPrefs.SetInt("Player Score 1", finalScoreP1);
                scorestop = true;
                PlayerPrefs.SetInt("Music", 1);
            }
            if (HP1 <= 0)
            {
                if (finalScoreP2 == 1)
                {
                    finalScoreP2++;
                    Win3.color = new Color(255f, 255f, 255f, 255f);
                    BannerP2.triggerWin2();
                }
                else
                {
                    finalScoreP2 = 1;
                    Win2.color = new Color(255f, 255f, 255f, 255f);
                }
                PlayerPrefs.SetInt("Player Score 2", finalScoreP2);
                scorestop = true;
                PlayerPrefs.SetInt("Music", 1);
            }
        }

    }
    public void BarSuperchanges()
    {
        if (HP1 <= 50 && P1SuperAvail)
        {
            if (P2SuperAvail == false)
            {
                if (P2P1SuperFin == true)
                {
                    if (P1ColorChange + .3 <= Time.time)
                    {
                        P1ColorChange = Time.time;
                        if (P1Alt)
                        {
                            imgHp1White.color = new Color(255f, 255f, 255f, 255f);
                            imgHp1.color = new Color(255f, 255f, 255f, 0f);
                            P1Alt = false;
                        }
                        else
                        {
                            imgHp1White.color = new Color(255f, 255f, 255f, 0f);
                            imgHp1.color = new Color(255f, 255f, 255f, 255f);
                            P1Alt = true;
                        }
                    }
                }
            }
            else
            {
                if (P1ColorChange + .3 <= Time.time)
                {
                    P1ColorChange = Time.time;
                    if (P1Alt)
                    {
                        imgHp1White.color = new Color(255f, 255f, 255f, 255f);
                        imgHp1.color = new Color(255f, 255f, 255f, 0f);
                        P1Alt = false;
                    }
                    else
                    {
                        imgHp1White.color = new Color(255f, 255f, 255f, 0f);
                        imgHp1.color = new Color(255f, 255f, 255f, 255f);
                        P1Alt = true;
                    }
                }
            }
        }
        if (HP2 <= 50 && P2SuperAvail)
        {
            if (P1SuperAvail == false)
            {
                if (P1P2SuperFin == true)
                {
                    if (P2ColorChange + .3 <= Time.time)
                    {
                        P2ColorChange = Time.time;
                        if (P2Alt)
                        {
                            imgHp2White.color = new Color(255f, 255f, 255f, 255f);
                            imgHp2.color = new Color(255f, 255f, 255f, 0f);
                            P2Alt = false;
                        }
                        else
                        {
                            imgHp2White.color = new Color(255f, 255f, 255f, 0f);
                            imgHp2.color = new Color(255f, 255f, 255f, 255f);
                            P2Alt = true;
                        }
                    }
                }
            }
            else
            {
                if (P2ColorChange + .3 <= Time.time)
                {
                    P2ColorChange = Time.time;
                    if (P2Alt)
                    {
                        imgHp2White.color = new Color(255f, 255f, 255f, 255f);
                        imgHp2.color = new Color(255f, 255f, 255f, 0f);
                        P2Alt = false;
                    }
                    else
                    {
                        imgHp2White.color = new Color(255f, 255f, 255f, 0f);
                        imgHp2.color = new Color(255f, 255f, 255f, 255f);
                        P2Alt = true;
                    }
                }
            }
        }
    }

    IEnumerator changeCam(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        Color = false;
        Sprite.color = new Color(255f, 255f, 255f, 255f);
        imgHp1.color= new Color(255f, 255f, 255f, 255f);
        imgHp2.color =new Color(255f, 255f, 255f, 255f);
        imgHp1White.color = new Color(255f, 255f, 255f, 0f);
        imgHp2White.color = new Color(255f, 255f, 255f, 0f);
        Bar.color = new Color(255f, 255f, 255f, 255f);
        if (P1SuperAvail== false)
        {
            P1P2SuperFin = true;
        }
        if (P2SuperAvail == false)
        {
            P2P1SuperFin = true;
        }
        yield break;
    }

    public void win_conditions()
    {
        if (HP1<=0)
        {
            P2.setWin(true);
        }
        if (HP2 <= 0)
        {
            P1.setWin(true);
        }
    }
    private void Cam_change()
    {
        Sprite.color = new Color(0f, 0f, 0f, 1f);
        imgHp1.color = new Color(0f, 0f, 0f, 1f);
        imgHp2.color = new Color(0f, 0f, 0f, 1f);
        Bar.color = new Color(0f, 0f, 0f, 1f);
        imgHp1White.color = new Color(0f, 0f, 0f, 1f);
        imgHp2White.color = new Color(0f, 0f, 0f, 1f);
        Color = true;
    }
    public void check_attacks()
    {
        if (P1Current.IsName("Hack_Attack"))
        {
            if (skipcheck == 0)
            {
                if (P2Current.IsName("Thrust_Idle 2") == false && P2Current.IsName("Thrust_Attack 2") == false && P2Current.IsName("Thrust_Block 2") == false)
                {
                    HP2 = HP2 - Hack_dmg;
                    P2.set_hit();
                    P2.setCurrHealth(HP2);
                }
                else
                {
                    if (P2Current.IsName("Thrust_Attack 2") == false && P2Current.IsName("Hack_Attack 2") == false && P2Current.IsName("Slash_Attack 2") == false) {
                    P2.set_block();
                    }
                }
                skipcheck = 1;
                P1Check = "Hack_Attack";
            }
        }
        if (P1Current.IsName("Slash_Attack"))
        {
            if (skipcheck == 0)
            {
                if (P2Current.IsName("Hack_Idle 2") == false && P2Current.IsName("Hack_Attack 2") == false && P2Current.IsName("Hack_Block 2") == false)
                {
                    HP2 = HP2 - Slash_dmg;
                    P2.set_hit();
                    P2.setCurrHealth(HP2);
                }
                else
                {
                    if(P2Current.IsName("Thrust_Attack 2") == false && P2Current.IsName("Hack_Attack 2") == false && P2Current.IsName("Slash_Attack 2") == false){
                    P2.set_block(); }
                }
                skipcheck = 1;
                P1Check = "Slash_Attack";
            }
        }

        if (P1Current.IsName("Thrust_Attack"))
        {
            if (skipcheck == 0)
            {
                if (P2Current.IsName("Slash_Idle 2") == false && P2Current.IsName("Slash_Attack 2") == false && P2Current.IsName("Slash_Block 2") == false)
                {
                    HP2 = HP2 - Thrust_dmg;
                    P2.set_hit();
                    P2.setCurrHealth(HP2);
                }
                else
                {
                    if (P2Current.IsName("Thrust_Attack 2") == false && P2Current.IsName("Hack_Attack 2") == false && P2Current.IsName("Slash_Attack 2") == false) {
                    P2.set_block();
                }
                }
                skipcheck = 1;
                P1Check = "Thrust_Attack";
            }
        }

        if (P1Current.IsName("Hack_Super"))
        {
            if (skipcheck == 0)
            {
                if (P2Current.IsName("Thrust_Idle 2") == false && P2Current.IsName("Thrust_Attack 2") == false && P2Current.IsName("Thrust_Block 2") == false)
                {
                    P2.set_hit();
                    StartCoroutine(UltWait(3.7f, "P1"));
                }
                else
                {
                    P2.set_block();
                }
                P1.UltTimer_Set();
                P2.UltTimer_Set();
                P2.Freeze_animation();
                skipcheck = 1;
                P1Check = "Hack_Super";
                supers1.triggerSuper1();
                Cam_change();
                P1SuperAvail = false;
            }
        }

        if (P1Current.IsName("Slash_Super"))
        {
            if (skipcheck == 0)
            {
                if (P2Current.IsName("Hack_Idle 2") == false&& P2Current.IsName("Hack_Attack 2") == false && P2Current.IsName("Hack_Block 2") == false)
                {
                    P2.set_hit();
                    StartCoroutine(UltWait(3.7f, "P1"));
                }
                else
                {
                    P2.set_block();
                }
                P1.UltTimer_Set();
                P2.UltTimer_Set();
                P2.Freeze_animation();
                skipcheck = 1;
                P1Check = "Slash_Super";
                supers1.triggerSuper1();
                Cam_change();
                P1SuperAvail = false;
            }
        }

        if (P1Current.IsName("Thrust_Super"))
        {
            if (skipcheck == 0)
            {
                if (P2Current.IsName("Slash_Idle 2") == false && P2Current.IsName("Slash_Attack 2") == false && P2Current.IsName("Slash_Block 2") == false)
                {
                    P2.set_hit();
                    StartCoroutine(UltWait(3.7f, "P1"));
                }
                else
                {
                    P2.set_block();
                }
                P1.UltTimer_Set();
                P2.UltTimer_Set();
                P2.Freeze_animation();
                skipcheck = 1;
                P1Check = "Thrust_Super";
                supers1.triggerSuper1();
                Cam_change();
                P1SuperAvail = false;
            }
        }



        if (P1Current.IsName(P1Check)==false)
        {
            skipcheck = 0;
        }

        // Player 2 inputs

        if (P2Current.IsName("Hack_Attack 2"))
        {
            if (skipcheck2 == 0)
            {
                if (P1Current.IsName("Thrust_Idle") == false && P1Current.IsName("Thrust_Attack") == false && P1Current.IsName("Thrust_Block") == false)
                {
                    HP1 = HP1 - Hack_dmg;
                    P1.set_hit();
                    P1.setCurrHealth(HP1);
                }
                else
                {
                    if (P1Current.IsName("Thrust_Attack") == false && P1Current.IsName("Hack_Attack") == false && P1Current.IsName("Slash_Attack") == false)
                    {
                        P1.set_block();
                    }
                }
                skipcheck2 = 1;
                P2Check = "Hack_Attack 2";
            }
        }

        if (P2Current.IsName("Slash_Attack 2"))
        {
            if (skipcheck2 == 0)
            {
                if (P1Current.IsName("Hack_Idle 0") == false && P1Current.IsName("Hack_Attack") == false && P1Current.IsName("Hack_Block") == false)
                {
                    HP1 = HP1 - Slash_dmg;
                    P1.set_hit();
                    P1.setCurrHealth(HP1);
                }
                else
                {
                    if (P1Current.IsName("Thrust_Attack") == false && P1Current.IsName("Hack_Attack") == false && P1Current.IsName("Slash_Attack") == false)
                    {
                        P1.set_block();
                    }
                }
                skipcheck2 = 1;
                P2Check = "Slash_Attack 2";
            }
        }

        if (P2Current.IsName("Thrust_Attack 2"))
        {
            if (skipcheck2 == 0)
            { 
                if (P1Current.IsName("Slash_Idle") == false && P1Current.IsName("Slash_Attack") == false  && P1Current.IsName("Slash_Block") == false)
                {
                    HP1 = HP1 - Thrust_dmg;
                    P1.set_hit();
                    P1.setCurrHealth(HP1);
                }
                else
                {
                    if (P1Current.IsName("Thrust_Attack") == false && P1Current.IsName("Hack_Attack") == false && P1Current.IsName("Slash_Attack") == false)
                    {
                        P1.set_block();
                    }
                }
                skipcheck2 = 1;
                P2Check = "Thrust_Attack 2";
            }
        }

        if (P2Current.IsName("Thrust_Super 2"))
        {
            if (skipcheck2 == 0)
            {
                if (P1Current.IsName("Slash_Idle") == false && P1Current.IsName("Slash_Attack") == false && P1Current.IsName("Slash_Block") == false)
                {
                    P1.set_hit();
                    StartCoroutine(UltWait(2.7f, "P2"));
                }
                else
                {
                    P1.set_block();
                }
                P1.UltTimer_Set();
                P2.UltTimer_Set();
                P1.Freeze_animation();
                skipcheck2 = 1;
                P2Check = "Thrust_Super 2";
                supers2.triggerSuper2();
                Cam_change();
                P2SuperAvail = false;
            }
        }

        if (P2Current.IsName("Slash_Super 2"))
        {
            if (skipcheck2 == 0)
            {
                if (P1Current.IsName("Hack_Idle 0") == false && P1Current.IsName("Hack_Attack") == false && P1Current.IsName("Hack_Block") == false)
                {
                    P1.set_hit();
                    StartCoroutine(UltWait(2.7f, "P2"));
                }
                else
                {
                    P1.set_block();
                }
                P1.UltTimer_Set();
                P2.UltTimer_Set();
                P1.Freeze_animation();
                skipcheck2 = 1;
                P2Check = "Slash_Super 2";
                supers2.triggerSuper2();
                Cam_change();
                P2SuperAvail = false;
            }
        }

        if (P2Current.IsName("Hack_Super 2"))
        {
            if (skipcheck2 == 0)
            {
                if (P1Current.IsName("Thrust_Idle") == false && P1Current.IsName("Thrust_Attack") == false && P1Current.IsName("Thrust_Block") == false)
                {
                    P1.set_hit();
                    StartCoroutine(UltWait(2.7f, "P2"));
                }
                else
                {
                    P1.set_block();
                }
                P1.UltTimer_Set();
                P2.UltTimer_Set();
                P1.Freeze_animation();
                skipcheck2 = 1;
                P2Check = "Hack_Super 2";
                supers2.triggerSuper2();
                Cam_change();
                P2SuperAvail = false;
            }
        }

        if (P2Current.IsName(P2Check) == false)
        {
            skipcheck2 = 0;
        }

    }
    IEnumerator UltWait(float timeDelay, string temp)
    {
        yield return new WaitForSeconds(timeDelay);
        if (temp == "P1")
        {
            HP2 = HP2 - Super_dmg;
            P2.setCurrHealth(HP2);
        }
        else if (temp == "P2")
        {
            HP1 = HP1 - Super_dmg;
            P1.set_hit();
            P1.setCurrHealth(HP1);
        }
        yield break;
    }
}
