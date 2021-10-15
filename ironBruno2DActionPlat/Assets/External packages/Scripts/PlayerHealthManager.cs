using UnityEngine;
using System.Collections;
using Platformer2D.Character;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
	[SerializeField]
	public Text DamageText;
	[SerializeField]
	Transform playerTransform;
	public float startingHealth;
	public float currentHealth;
	CharacterMovement2D playerMovement;
	PlayerController playerController;
	public Animator anim;
	bool canRecover;
	[SerializeField]
	int HealthRecover;
	XpManager xpManager;
	[SerializeField]
	Transform SpawnPosition;
	PlayerAimWeapon playerAimWeapon;

	[SerializeField]
	GameObject DeathUICanvas;

	RecoveryGem recoveryGem;
	[SerializeField]
	Transform spawnRecoveryGemPosition;
	[SerializeField]
	GameObject GemDroppedSpawnGameobject;
	//public int GemDroppedAmount;
	[SerializeField]
	bool isDead;
	State state;
	bool canKnockBack;
	bool canUseRecover;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;

	[SerializeField]
	EnemyHealthManager healthmanage;

	[SerializeField]
	knockback knockback;

	[SerializeField]
	Animator UInim;

	[SerializeField]
	public float damageAmount;


	private enum State
	{
		Live,
		Dead,
		AfterDead,


	}
	// Use this for initialization
	private void Awake()
	{
		GameObject spawnTransform = GameObject.Find("/Portal");
		if(spawnTransform!=null)
		SpawnPosition = spawnTransform.GetComponent<Transform>();

		GameObject theDeathCanvas = GameObject.Find("/Character/DeathCanvas");
		UInim = theDeathCanvas.GetComponent<Animator>();
		canUseRecover = true;
		theDeathCanvas.SetActive(false);

		//isDead = false;
	}
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		recoveryGem = GetComponent<RecoveryGem>();
		knockback = GetComponent<knockback>();
		playerAimWeapon = GetComponent<PlayerAimWeapon>();
		xpManager = GetComponent<XpManager>();
		anim = GetComponent<Animator>();
		playerController = GetComponent<PlayerController>();
		playerMovement = GetComponent<CharacterMovement2D>();
		currentHealth = startingHealth;
		if (startingHealth <= 0)
		{
			startingHealth = 100;
		}
		canRecover = true;
		rb = GetComponent<Rigidbody2D>();
	}

	//gameObject.SetActive(false);

	// Update is called once per frame
	void Update()
	{
		if (currentHealth <= 0)
		{
			//Debug.Log("Tomorto");
			isDead = true;

			//	GemDroppedAmount = GameManager.instance.coin;

			//StartCoroutine(CoinDeletedDelay());

		}



		if (currentHealth <= startingHealth && canRecover && canUseRecover)
		{
			StartCoroutine(HealthRecoverRate());

		}
		if (currentHealth > startingHealth)
		{
			currentHealth = startingHealth;
		}


		switch (state)
		{
			default:
			case State.Live:
				canUseRecover = true;
				isPlayerDead();
				break;

			case State.Dead:

				IsDead();
				//Debug.Log("tatentando");
				break;
			case State.AfterDead:
				StopCoroutine(DeathRecoverRate());
				isDead = false;
				state = default;
				break;
		}
	}

	public bool isPlayerDead()
	{
		if (isDead)
		{
			state = State.Dead;
			return true;
		}
		return isDead;

	}
	public void IsDead()
	{
		DeathUICanvas.SetActive(true);
		canUseRecover = false;
		playerAimWeapon.canAim = false;
		playerController.canControl = false;
		playerMovement.StopImmediately();
		anim.SetBool("IsDead", true);
		gameObject.layer = 12;
		StartCoroutine(DeathRecoverRate());
		Instantiate(GemDroppedSpawnGameobject, spawnRecoveryGemPosition.position, spawnRecoveryGemPosition.rotation);
		state = State.AfterDead;
		
	}
	public void RecoverPlayer(int Recover)
	{

		currentHealth += Recover;


	}
	IEnumerator CoinDeletedDelay()
	{


		yield return new WaitForSeconds(1);
		GameManager.instance.coin -= GameManager.instance.coin; ;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "MinotaurTag")
		{
			// DamageUIText.SetActive(true);
			spriteRenderer.color = new Color(1, 0.5f, 0.5f, 1);
			playerController.canControl = false;
			canKnockBack = false;
			playerController.PlayermovementInput.x = 0;
			rb.gravityScale = 0;
			playerMovement.StopImmediately();
			anim.SetTrigger("GetHit");
			gameObject.layer = 12;

			damageAmount = 25 * xpManager.level * Random.Range(1f, 2f);
			HurtPlayer(damageAmount);
			DamageText.text = damageAmount.ToString();

			StartCoroutine(RealeaseKnockback());

		}
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == ("EnemyCanDamage"))
		{

			// DamageUIText.SetActive(true);
			spriteRenderer.color = new Color(1, 0.5f, 0.5f, 1);
			playerController.canControl = false;
			canKnockBack = false;
			playerController.PlayermovementInput.x = 0;
			rb.gravityScale = 0;
			playerMovement.StopImmediately();
			anim.SetTrigger("GetHit");
			gameObject.layer = 12;

			damageAmount = 5 * xpManager.level * Random.Range(1f, 2f);
			HurtPlayer(damageAmount);
			DamageText.text = damageAmount.ToString();

			StartCoroutine(RealeaseKnockback());

		}

		
	}
	
	

IEnumerator RealeaseKnockback()
	{
		yield return new WaitForSeconds(0.2f);
		playerController.canControl = true;
		rb.gravityScale = 1;
		yield return new WaitForSeconds(1);
		gameObject.layer = 9;
		spriteRenderer.color = new Color(1, 1, 1, 1);
	}
	public void HurtPlayer(float damage)
	{
		currentHealth -= damage;
	}

	IEnumerator DeathRecoverRate()
	{

		yield return new WaitForSeconds(6);
		gameObject.layer = 9;
		UInim.SetBool("IsLive", false);
		yield return new WaitForSeconds(0.1f);
		currentHealth = startingHealth;
		playerTransform.position = SpawnPosition.position;
		canUseRecover = true;
		anim.SetBool("IsDead", false);
		playerAimWeapon.canAim = true;

		yield return new WaitForSeconds(6);

		//SceneManager.LoadScene(0);
		UInim.SetBool("IsLive", true);
		yield return new WaitForSeconds(0.2f);
		playerController.canControl = true;
		StopCoroutine(DeathRecoverRate());

		yield return new WaitForSeconds(2);
		DeathUICanvas.SetActive(false);

	}
	IEnumerator HealthRecoverRate()
	{
		canRecover = false;

		yield return new WaitForSeconds(1);
		RecoverPlayer(HealthRecover);
		canRecover = true;
	}
}