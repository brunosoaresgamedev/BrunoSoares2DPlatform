using UnityEngine;
using System.Collections;
using Platformer2D.Character;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

	public static EnemyHealthManager HealthManager;

	public Text levelText1;
	public int startingHealth;
	public int currentHealth;
	[SerializeField]
	MinoEnemieAIController enemieAIController;
	[SerializeField]
	JumpSlime enemieSlimeAIController;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	CharacterMovement2D characterMovement2D;
	public GameObject gem;
	[SerializeField]
	GameObject HitFX;
	bool isDead;
	public Transform gemPoint;
	//[SerializeField]
	//SpawnEnemy spawnEnemy;
	LevelAIManager levelAI;
	[SerializeField]
	XpManager xpManager;
	[SerializeField]
	PlayerInput playerInput;
	bool hasScaled;
	[SerializeField]
	PlayerHealthManager playerHealthManager;

	GameObject PlayerGameObject;
	GameObject SelfGameObject;

	[SerializeField]
	Text DamageText;
	[SerializeField]
	public int DamageAmount;

	bool isFxInUse;



	public int LevelAi;
	private void Awake()
	{

		if (HealthManager == null)
		{
			HealthManager = this;

		}
		enemieAIController = GetComponent<MinoEnemieAIController>();
		enemieSlimeAIController = GetComponent<JumpSlime>();


		if (enemieAIController != null)
		SelfGameObject = GameObject.Find("Minotaur");
		if (enemieSlimeAIController != null)
		SelfGameObject = GameObject.Find("NewSlime");

	//	levelText1 = SelfGameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();

		PlayerGameObject = GameObject.Find("Player");
		xpManager = PlayerGameObject.GetComponent<XpManager>();
	

		characterMovement2D = GetComponent<CharacterMovement2D>();
		
		
		levelAI = GetComponent<LevelAIManager>();
	


		anim = GetComponent<Animator>();
	

	}

	IEnumerator IACounter()
	{
		while (true)
		{
			LevelAi =	xpManager.level + (Random.Range(-1, 1));
			levelText1.text = LevelAi.ToString();
			yield return new WaitForSeconds(5);

		}
	}
	// Use this for initialization
	void Start()
	{

		StartCoroutine(IACounter());

		if (LevelAi <= 0)
		{
			LevelAi = 1;
		}





		if (enemieAIController != null)
			startingHealth = 50 * (LevelAi + 1);

		if (enemieSlimeAIController != null)
			startingHealth = 5 * (LevelAi + 1);

		currentHealth = startingHealth;

		/*
		if (LevelAi < (XpManager.instance.level - 5) && hasScaled)
		{
			hasScaled = true;
			//	gameObject.transform.localScale += new Vector3(1, 1, 1);
			levelText1.color = Color.green;
		}

		if (LevelAi < XpManager.instance.level && LevelAi > (XpManager.instance.level - 5) && hasScaled)
		{
			hasScaled = true;
			//gameObject.transform.localScale += new Vector3(1.2f, 1.2f, 1.2f);
			levelText1.color = Color.blue;
		}

		if (LevelAi < (XpManager.instance.level + 5) && LevelAi > XpManager.instance.level && hasScaled)
		{
			hasScaled = true;
			//gameObject.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
			levelText1.color = Color.yellow;
		}
		if (LevelAi < (XpManager.instance.level + 7) && LevelAi > (XpManager.instance.level + 5) && hasScaled)
		{
			hasScaled = true;
			//	gameObject.transform.localScale += new Vector3(1.7f, 1.7f, 1.7f);
			levelText1.color = Color.red;
		}
		*/

	}
	public void PlayerDamage()
	{






	}
	// Update is called once per frame
	void Update()
	{



		if (currentHealth <= 0 && !isDead)
		{
			//	if (enemieSlimeAIController != null)
			//	SpawnEnemy.instanceSpawnEnemy.CurrentSpawnNumber--;
			Death();
			StartCoroutine(death());
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Attack")
		{
			TakeDamage(2 * XpManager.instance.level);
			applyDamage();

		}
		

	}

	public void applyDamage()
	{
		if (!isFxInUse)
			StartCoroutine(Hit());
	}
	IEnumerator Hit()
	{
		{
			isFxInUse = true;
			HitFX.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			HitFX.SetActive(false);
			isFxInUse = false;
		}

	}



	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}
	IEnumerator death()
	{
		{
			yield return new WaitForSeconds(2.5f);
			Destroy(gameObject);
		}

	}

	public void Death()
	{
		isDead = true;
		anim.SetBool("IsDead", true);

		if (enemieAIController != null)
		{
			enemieAIController.isDead = true;

		}

		/*
		if (enemieSlimeAIController != null)
		{
			enemieSlimeAIController.isDead = true;
		SpawnEnemy.instanceSpawnEnemy.CurrentSpawnNumber--;
		}
		*/
		XpManager.instance.AddXP(100 * LevelAi);
		Instantiate(gem, gemPoint.position, gemPoint.rotation);
		//	gameObject.layer = 16;
		characterMovement2D.StopImmediately();
	}
}