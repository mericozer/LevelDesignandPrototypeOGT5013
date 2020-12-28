using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchManager : MonoBehaviour
{
    public GameObject warrior;
    public GameObject elf;
    public CinemachineVirtualCamera warriorCam;
    public CinemachineVirtualCamera elfCam;
    public ParticleSystem changeFire;
    
    private void Awake()
    {
      
    }
    // Start is called before the first frame update
    void Start()
    {
        elf.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCharacter(string charType)
    {
        if (charType == "warrior" && !warrior.activeSelf)
        {
            changeFire.Play();

            warrior.transform.position = elf.transform.position;
            warrior.transform.rotation = elf.transform.rotation;
            changeFire.transform.position = elf.transform.position;

            FindObjectOfType<MonsterBehaviour>().ChangeLookAt();

            CanvasController.instance.SwitchCharStats(0);
            elfCam.gameObject.SetActive(false);
            warriorCam.gameObject.SetActive(true);
            elf.SetActive(false);
            warrior.SetActive(true);
        }
        else if (charType == "elf" && !elf.activeSelf)
        {
            changeFire.Play();

            elf.transform.position = warrior.transform.position;
            elf.transform.rotation = warrior.transform.rotation;
            changeFire.transform.position = warrior.transform.position;

            FindObjectOfType<MonsterBehaviour>().ChangeLookAt();

            CanvasController.instance.SwitchCharStats(1);
            warriorCam.gameObject.SetActive(false);
            elfCam.gameObject.SetActive(true);
            warrior.SetActive(false);
            elf.SetActive(true);
        }
    }
}
