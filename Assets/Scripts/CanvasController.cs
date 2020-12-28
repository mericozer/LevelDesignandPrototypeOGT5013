using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    public Slider life, mana;
    public Image avatar;

    public Sprite warrior;
    public Sprite thief;
    public SwitchManager switchManager;

    private int heroCounter = 2;
    private int heroNum = 0;

    public Animator warriorAnim;
    public Animator elfAnim;
    public Animator canvasAnim;
    void Awake()
    {
        instance = this;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        

        life.value = 100;
        mana.value = 100;
        PlayerPrefs.SetFloat("warriorLife", 100);
        PlayerPrefs.SetFloat("warriorMana", 100);
        PlayerPrefs.SetFloat("elfLife", 100);
        PlayerPrefs.SetFloat("elfMana", 100);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar()
    {
        Debug.Log("hereUpdateHealthBar");
        life.value = life.value - 5;
        if (life.value == 0)
        {
            heroCounter--;
            Debug.Log(heroCounter);
            if (heroCounter > 0)
            {
                switch (heroNum)
                {
                    case 0:
                        SwitchCharStats(1);
                        switchManager.SwitchCharacter("elf");
                        heroNum = 1;
                        break;
                    case 1:
                        SwitchCharStats(0);
                        switchManager.SwitchCharacter("warrior");
                        heroNum = 0;
                        break;
                }
            }
            else {
                switch (heroNum)
                {
                    case 0:
                        warriorAnim.Play("Falling Back Death");
                        Debug.Log("warrior dead");
                        break;
                    case 1:
                        elfAnim.Play("Falling Back Death");
                        break;
                }

            }
           
        }
    }

    public void SwitchCharStats(int charNum)
    {
        switch (charNum)
        {
            case 0:
                PlayerPrefs.SetFloat("elfLife", life.value);
                PlayerPrefs.SetFloat("elfMana", life.value);
                life.value = PlayerPrefs.GetFloat("warriorLife");
                life.value = PlayerPrefs.GetFloat("warriorMana");
                avatar.sprite = warrior;
                heroNum = charNum;
                break;
            case 1:
                PlayerPrefs.SetFloat("warriorLife", life.value);
                PlayerPrefs.SetFloat("warriorMana", life.value);
                life.value = PlayerPrefs.GetFloat("elfLife");
                life.value = PlayerPrefs.GetFloat("elfMana");
                avatar.sprite = thief;
                heroNum = charNum;
                break;
        }

            
        
    }

    public void ChangeQuest()
    {
        canvasAnim.SetBool("Gate", true);
        //canvasAnim.Play("Quest Change");
    }
}
