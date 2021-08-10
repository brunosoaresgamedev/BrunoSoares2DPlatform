using UnityEngine;
using System.Collections;
using Platformer2D.Character;

public class PlayerHealthManager : MonoBehaviour {
    
	public int startingHealth;
	public int currentHealth;
	CharacterMovement2D playerMovement;
	PlayerController playerController;
	public Animator anim;
	bool canRecover;
	[SerializeField]
	int HealthRecover;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		playerController = GetComponent<PlayerController>();
		playerMovement = GetComponent<CharacterMovement2D>();
		currentHealth = startingHealth;
		
		canRecover = true;
	}

	//gameObject.SetActive(false);

	// Update is called once per frame
	void Update () {
		
		if(currentHealth <= 0)
		{

			playerController.canControl = false;
			playerMovement.StopImmediately();
			anim.SetBool("IsDead",true);
			gameObject.layer = 12;

		}
		if(currentHealth <= startingHealth && canRecover)
        {
			StartCoroutine(HealthRecoverRate());
			
        }
	}
	public void RecoverPlayer(int Recover)
	{
        
			currentHealth += Recover;
		
		
	}
	private void OnTriggerEnter2D(Collider2D other)
    {


		if (other.tag == "EnemyAttack")
		{

			HurtPlayer(5);
		}
		
    }
    public void HurtPlayer(int damage)
	{
		currentHealth -= damage;
	}
	IEnumerator HealthRecoverRate()
    {
		canRecover = false;

		yield return new WaitForSeconds(1);
		RecoverPlayer(HealthRecover);
		canRecover = true;
	}
}