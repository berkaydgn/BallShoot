using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("--------BULLET SETTINGS")]
    public GameObject[] Bullets;
    public GameObject FirePoint;
    public float BulletPower;
    int activeBulletIndex;
    public Animator HowitzerAnimator;
    public ParticleSystem ShotEfekt;
    public ParticleSystem[] BulletEffects;
    [SerializeField] AudioSource[] BasketSounds;
    int BasketSoundIndex;
    [SerializeField] AudioSource[] OtherSounds;

    [Header("--------LEVEL SETTINGS")]
    [SerializeField] private int CurrentBallCount; 
    [SerializeField] private int RequiredBallCount;      
    [SerializeField] private TextMeshProUGUI RemainderBallCountText;
    [SerializeField] private Slider CompletedBallSlider;
    private int ValidNumber;

    [Header("--------UI SETTINGS")]
    public GameObject[] Panels;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI WinLevelText;
    public TextMeshProUGUI LoseLevelText;

    [Header("--------UI SETTINGS")]
    public Renderer BucketTransparent;
    float BucketStartValue;
    float BucketStepValue;


    void Start()
    {
        activeBulletIndex = 0;
        BucketStartValue = .5f;
        BucketStepValue = .25f / RequiredBallCount;

        CompletedBallSlider.maxValue = RequiredBallCount;
        RemainderBallCountText.text = CurrentBallCount.ToString();

        //BucketTransparent.material.SetTextureScale("_MainTex", new Vector2(1f, .3f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OtherSounds[1].Play();
            HowitzerAnimator.Play("HowitzerShot");
            ShotEfekt.Play();
            CurrentBallCount--;
            RemainderBallCountText.text = CurrentBallCount.ToString();
            Bullets[activeBulletIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            Bullets[activeBulletIndex].SetActive(true);
            Bullets[activeBulletIndex].GetComponent<Rigidbody>().AddForce(Bullets[activeBulletIndex].transform.TransformDirection(90, 90, 0) * BulletPower, ForceMode.Force);

            if (Bullets.Length - 1 == activeBulletIndex)
                activeBulletIndex = 0;
            else 
                activeBulletIndex++;
        }
    }

    public void ParcEffects(Vector3 position, Color color)
    {
        BulletEffects[activeBulletIndex].transform.position = position;
        var main = BulletEffects[activeBulletIndex].main;
        main.startColor = color;
        BulletEffects[activeBulletIndex].gameObject.SetActive(true);
        activeBulletIndex++;

        if (activeBulletIndex == BulletEffects.Length -1)
            activeBulletIndex = 0;
    }

    public void ValidBalls()
    {
        BasketSounds[BasketSoundIndex].Play();
        BasketSoundIndex++;
        if (BasketSoundIndex == BasketSounds.Length - 1)
            BasketSoundIndex = 0;

        ValidNumber++;
        CompletedBallSlider.value = ValidNumber;

        BucketStartValue -= BucketStepValue;
        BucketTransparent.material.SetTextureScale("_MainTex", new Vector2(1f, BucketStartValue));

        if (ValidNumber == RequiredBallCount) //Win
        {
            OtherSounds[2].Play();
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 15);
            CoinText.text = PlayerPrefs.GetInt("Coin").ToString();
            Panels[1].SetActive(true);
            WinLevelText.text = "Level : " + SceneManager.GetActiveScene().name;
        }
    }

    public void InvalidBalls() 
    {
        if (CurrentBallCount == 0) //Lose
        {
            OtherSounds[3].Play();
            Panels[2].SetActive(true);
            LoseLevelText.text = "Level : " + SceneManager.GetActiveScene().name;
        }
        
        if ((ValidNumber + CurrentBallCount) < RequiredBallCount) //Lose
        {
            OtherSounds[3].Play();
            Panels[2].SetActive(true);
            LoseLevelText.text = "Level : " + SceneManager.GetActiveScene().name;
        }
    }

    public void StopTheGame()
    {
        Time.timeScale = 0;
        Panels[0].SetActive(true);
    }

    public void PanelButtons(string process)
    {
        switch (process)
        {
            case "Resume":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Settings":
                break;
            case "Retry":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
        }


    }

}
