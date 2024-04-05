using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("--------BULLET SETTINGS")]
    [SerializeField] private GameObject[] Bullets;
    [SerializeField] private float BulletPower;
    [SerializeField] private Animator HowitzerAnimator;
    [SerializeField] private ParticleSystem ShotEfekt;
    [SerializeField] private ParticleSystem[] BulletEffects;
    [SerializeField] private AudioSource[] BasketSounds;
    [SerializeField] private GameObject FirePoint;
    private int activeBulletIndex;
    private int BasketSoundIndex;

    [Header("--------LEVEL SETTINGS")]
    [SerializeField] private int CurrentBallCount; 
    [SerializeField] private int RequiredBallCount;
    [SerializeField] private TextMeshProUGUI RemainderBallCountText;
    [SerializeField] private Slider CompletedBallSlider;
    private int ValidNumber;

    [Header("--------UI SETTINGS")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private TextMeshProUGUI WinLevelText;
    [SerializeField] private TextMeshProUGUI LoseLevelText;

    [Header("--------OTHER SETTINGS")]
    [SerializeField] private Renderer BucketTransparent;
    [SerializeField] private AudioSource[] OtherSounds;
    private float BucketStartValue;
    private float BucketStepValue;
    private string LevelName;


    void Start()
    {
        activeBulletIndex = 0;
        BucketStartValue = .5f;
        BucketStepValue = .25f / RequiredBallCount;
        LevelName = SceneManager.GetActiveScene().name;
        CompletedBallSlider.maxValue = RequiredBallCount;
        RemainderBallCountText.text = CurrentBallCount.ToString();
    }

    void Update()
    {
        if (Time.timeScale != 0)
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
            WinPanel();
        }
    }
    public void InvalidBalls() 
    {
        if (CurrentBallCount == 0) //Lose
        {
            LosePanel();
        }
        
        if ((ValidNumber + CurrentBallCount) < RequiredBallCount) //Lose
        {
            LosePanel();
        }
    }
    public void WinPanel()
    {
        Time.timeScale = 0;
        OtherSounds[2].Play();
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 15);
        CoinText.text = PlayerPrefs.GetInt("Coin").ToString();
        Panels[1].SetActive(true);
        WinLevelText.text = "Level : " + LevelName;
    }
    public void LosePanel()
    {
        Time.timeScale = 0;
        OtherSounds[3].Play();
        Panels[2].SetActive(true);
        LoseLevelText.text = "Level : " + LevelName;
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