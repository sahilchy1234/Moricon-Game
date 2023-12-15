using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.KnifeHit
{
	public class GamePlayManager : MonoBehaviour
	{
		
		public static GamePlayManager instance;
		[Header("Circle Setting")]
		public Circle[] circlePrefabs;
		public Bosses[] BossPrefabs;

		public Transform circleSpawnPoint;
		[Range(0f, 1f)] public float circleWidthByScreen = .5f;

		[Header("Timer")]
		public const float TIME_LIMIT = 60F;
		public static float timeRemaining = 60f;
		private float timer = 0F;
		//private int wrongcounter = 0;
		public Text timeText;
		public bool isGameOver;


		[Header("Knife Setting")]
		public Knife knifePrefab;
		public Knife knifePrefab1;
		public Knife knifePrefab2;
		public Knife knifePrefab3;
		public Knife knifePrefab4;
		public Knife knifePrefab5;
		public Knife knifePrefab6;
		public Knife knifePrefab7;
		public Knife knifePrefab8;
		public Transform KnifeSpawnPoint;
		[Range(0f, 1f)] public float knifeHeightByScreen = .1f;

		public GameObject ApplePrefab;
		[Header("UI Object")]
		public Text lblScore;
		public Text lblStage;
		public List<Image> stageIcons;
		public Color stageIconActiveColor;
		public Color stageIconNormalColor;

		public bool isplayingBossLevel;

		[Header("UI Boss")]

		public GameObject bossFightStart;
		public GameObject bossFightEnd;
		public AudioClip[] bossFightStartSounds;
		public AudioClip[] bossFightEndSounds;
		[Header("Ads Show")]
	//	public GameObject adsShowView;
	//	public Image adTimerImage;
	//	public Text adSocreLbl;


		[Header("GameOver Popup")]
	//	public GameObject gameOverView;
	//	public Text gameOverSocreLbl, gameOverStageLbl;
	//	public GameObject newBestScore;
		//public AudioClip gameOverSfx;

		[Space(50)]

		public int cLevel = 0;
		public bool isDebug = false;
		string currentBossName = "";
		public Circle currentCircle;
		Knife currentKnife;
		bool usedAdContinue;
		public int totalSpawnKnife
		{
			get
			{
				return _totalSpawnKnife;
			}
			set
			{
				_totalSpawnKnife = value;

			}
		}
		int _totalSpawnKnife;

	void Awake()
		{		
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		void Start()
		{
			timeRemaining = 60f;
			isGameOver = false;
			setupGame();
			GameManager.Apple = 0;
			GameManager.Apple += 1;
			Debug.Log("Asking to start game");
			PhotonGameController.instance.UpdateScore(0);
			GameManager.Stage = 1;
			GameManager.isGameOver = false;
			usedAdContinue = false;
			if (isDebug)
			{
				GameManager.Stage = cLevel;
			}
			isplayingBossLevel = false;
		}

		/*private void OnEnable()
		{
			startGame();
			Timer.Schedule(this, 0.1f, AddEvents);
		}*/

		public void startGame()
		{
			
			
		}
		public void UpdateLable()
		{
			lblScore.text = GameManager.score + "";
			if (GameManager.Stage % 5 == 0)
			{
				for (int i = 0; i < stageIcons.Count - 1; i++)
				{
					stageIcons[i].gameObject.SetActive(false);
				}
				stageIcons[stageIcons.Count - 1].color = stageIconActiveColor;
				lblStage.color = stageIconActiveColor;
				lblStage.text = currentBossName;
			}
			else
			{
				lblStage.text = "STAGE " + GameManager.Stage;
				for (int i = 0; i < stageIcons.Count; i++)
				{
					stageIcons[i].gameObject.SetActive(true);
					stageIcons[i].color = GameManager.Stage % stageIcons.Count <= i ? stageIconNormalColor : stageIconActiveColor;
				}
				lblStage.color = stageIconNormalColor;
			}
		}
		public void setupGame()
		{
			Debug.Log("Level number is: " + GameManager.Stage);
			spawnCircle();
			KnifeCounter.intance.setUpCounter(currentCircle.totalKnife);

			totalSpawnKnife = 0;
			StartCoroutine(GenerateKnife());
		}
		void Update()
		{
			if (currentKnife == null)
				return;

			if (Input.GetMouseButtonDown(0) && !currentKnife.isFire)
			{
				KnifeCounter.intance.setHitedKnife(totalSpawnKnife);
				currentKnife.ThrowKnife();
				StartCoroutine(GenerateKnife());
			}
			TimeCalculator();

		}

		private void TimeCalculator()
		{
			this.timer += Time.deltaTime;

			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
				timeText.text = (Mathf.RoundToInt(timeRemaining % 60)).ToString();
			}
			else
			{
				//Debug.LogError("Going to call Game end popup");
				GameManager.isGameOver = true;
				showGameOverPopup();
				GameObject.Find("KnifeCounter").GetComponent<KnifeCounter>().enabled = false;
				//GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>.enabled = true;
				GamePlayManager.instance.isGameOver = true;
				//gameOverView.SetActive(true);
				//gameOverPanel.SetActive(true);
				//stageContainer.SetActive(false);
				//GmeManagerScr.InstanceGame.IsGameOver = true;

			}
			// check if it's time to switch scenes
			//if (timeRemaining >= TIME_LIMIT || timeRemaining <= 0)
			//{
			//          Debug.LogError("Came to this part");
			//	//     gameOverPanel.SetActive(true);
			//	//  stageContainer.SetActive(false);
			//	//  GmeManagerScr.InstanceGame.IsGameOver = true;
			//	// Knife.Instance.Hit = true;
			//}
		}


		public void spawnCircle()
		{
			GameObject tempCircle;
			if (GameManager.Stage % 5 == 0)
			{

				Bosses b = BossPrefabs[Random.Range(0, BossPrefabs.Length)];

				tempCircle = Instantiate<Circle>(b.BossPrefab, circleSpawnPoint.position, Quaternion.identity, circleSpawnPoint).gameObject;
				currentBossName = "Boss : " + b.Bossname;
				UpdateLable();
				OnBossFightStart();
			}
			else
			{
				if (GameManager.Stage > 50)
				{
					tempCircle = Instantiate<Circle>(circlePrefabs[Random.Range(11, circlePrefabs.Length - 1)], circleSpawnPoint.position, Quaternion.identity, circleSpawnPoint).gameObject;
				}
				else
				{
					//tempCircle = Instantiate<Circle>(circlePrefabs[GameManager.Stage - 1], circleSpawnPoint.position, Quaternion.identity, circleSpawnPoint).gameObject;
					tempCircle = Instantiate<Circle>(circlePrefabs[Random.Range(0, circlePrefabs.Length - 1)], circleSpawnPoint.position, Quaternion.identity, circleSpawnPoint).gameObject;
				}
			}

			tempCircle.transform.localScale = Vector3.one;
			float circleScale = (GameManager.ScreenWidth * circleWidthByScreen) / tempCircle.GetComponent<SpriteRenderer>().bounds.size.x;
			tempCircle.transform.localScale = Vector3.one * .2f;
			LeanTween.scale(tempCircle, new Vector3(circleScale, circleScale, circleScale), .3f).setEaseOutBounce();
			//tempCircle.transform.localScale = Vector3.one*circleScale;
			currentCircle = tempCircle.GetComponent<Circle>();

		}
		public IEnumerator OnBossFightStart()
		{
			bossFightStart.SetActive(true);
			SoundManager.instance.PlaySingle(bossFightStartSounds[Random.Range(0, bossFightEndSounds.Length - 1)], 1f);
			yield return new WaitForSeconds(.5f);
			bossFightStart.SetActive(false);
			isplayingBossLevel = true;
			setupGame();
		}

		public IEnumerator OnBossFightEnd()
		{
			bossFightEnd.SetActive(true);
			SoundManager.instance.PlaySingle(bossFightEndSounds[Random.Range(0, bossFightEndSounds.Length - 1)], 1f);
			yield return new WaitForSeconds(.5f);
			bossFightEnd.SetActive(false);
			isplayingBossLevel = false;
			setupGame();
		}
		public IEnumerator GenerateKnife()
		{
			yield return new WaitForSeconds(0.1f);
			/*yield return new WaitUntil(() =>
			{
				return KnifeSpawnPoint.childCount == 0;

			});*/
			if (currentCircle.totalKnife > totalSpawnKnife && !GameManager.isGameOver)
			{
				totalSpawnKnife++;
				GameObject tempKnife = new GameObject();
				if (GameManager.selectedKnifePrefab == null)
				{
					if (GameManager.Stage == 1)
					{
						tempKnife = Instantiate<Knife>(knifePrefab, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage == 2)
					{
						tempKnife = Instantiate<Knife>(knifePrefab1, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage == 3)
					{
						tempKnife = Instantiate<Knife>(knifePrefab2, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage == 4)
					{
						tempKnife = Instantiate<Knife>(knifePrefab3, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage == 5)
					{
						tempKnife = Instantiate<Knife>(knifePrefab4, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage == 6)
					{
						tempKnife = Instantiate<Knife>(knifePrefab5, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage == 7)
					{
						tempKnife = Instantiate<Knife>(knifePrefab6, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}
					else if (GameManager.Stage >= 8 && GameManager.Stage <= 20)
					{
						tempKnife = Instantiate<Knife>(knifePrefab7, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;
					}

				}
				else
				{
					tempKnife = Instantiate<Knife>(GameManager.selectedKnifePrefab, new Vector3(KnifeSpawnPoint.position.x, KnifeSpawnPoint.position.y - 2f, KnifeSpawnPoint.position.z), Quaternion.identity, KnifeSpawnPoint).gameObject;

				}
				tempKnife.transform.localScale = Vector3.one;
				float knifeScale = (GameManager.ScreenHeight * knifeHeightByScreen) / tempKnife.GetComponent<SpriteRenderer>().bounds.size.y;
				tempKnife.transform.localScale = Vector3.one * knifeScale;
				LeanTween.moveLocalY(tempKnife, 0, 0.1f);
				tempKnife.name = "Knife" + totalSpawnKnife;
				currentKnife = tempKnife.GetComponent<Knife>();
				//RandomKnife.instance.setRandomKnife(tempKnife.transform);
			}

		}
		public void NextLevel()
		{
			GameManager.Apple = 0;
			GameManager.Apple += 1;
			Debug.Log("Next Level");
			Debug.Log("Next Level going to start");
			if (currentCircle != null)
			{
				currentCircle.destroyMeAndAllKnives();
			}
			if (GameManager.Stage % 5 == 0)
			{
				GameManager.Stage++;
				StartCoroutine(OnBossFightEnd());

			}
			else
			{
				GameManager.Stage++;
				if (GameManager.Stage % 5 == 0)
				{
					StartCoroutine(OnBossFightStart());
				}
				else
				{
					Invoke("setupGame", .3f);
				}
			}
		}

		
		public void GameOver()
		{
			GameManager.isGameOver = true;
			timeRemaining = 60f;
		}
		public IEnumerator showAdPopup()
		{
			//adsShowView.SetActive(true);
			//adSocreLbl.text = GameManager.score + "";
			SoundManager.instance.PlayTimerSound();
			for (float i = 1f; i > 0; i -= 0.01f)
			{
				//adTimerImage.fillAmount = i;
				yield return new WaitForSeconds(0.1f);
			}
			CancleAdsShow();
			SoundManager.instance.StopTimerSound();
		}


		public void CancleAdsShow()
		{
			SoundManager.instance.StopTimerSound();
			SoundManager.instance.PlaybtnSfx();
		//	StopCoroutine(currentShowingAdsPopup);
			//adsShowView.SetActive(false);
			showGameOverPopup();
		}
		public void showGameOverPopup()
		{
			//Debug.Log("Showing game over popup");
			//gameOverView.SetActive(true);
			//gameOverSocreLbl.text = GameManager.score + "";
			//gameOverStageLbl.text = "Stage " + GameManager.Stage;

			//if (GameManager.score >= GameManager.HighScore)
			//{
			//	GameManager.HighScore = GameManager.score;
			//	newBestScore.SetActive(true);
			//}
			//else
			//{
			//	newBestScore.SetActive(false);
			//}

			//CUtils.ShowInterstitialAd();
		}
	}

	[System.Serializable]
	public class Bosses
	{
		public string Bossname;
		public Circle BossPrefab;
	}
}