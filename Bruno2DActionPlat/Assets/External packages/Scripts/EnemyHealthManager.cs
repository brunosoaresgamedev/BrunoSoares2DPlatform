using UnityEngine;
using System.Collections;
using Platformer2D.Character;

public class EnemyHealthManager : MonoBehaviour {
    
	public int startingHealth;
	public int currentHealth;
	EnemieAIController enemieAIController;
	public Animator anim;
	CharacterMovement2D characterMovement2D;
	

	// Use this for initialization
	void Start () {
		characterMovement2D = GetComponent<CharacterMovement2D>();
		enemieAIController = GetComponent<EnemieAIController>();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if(currentHealth <= 0)
		{

			StartCoroutine(death());
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Attack")
		{

			TakeDamage(1);
		}
	}
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}
	IEnumerator death()
    {
		{
			gameObject.layer = 16;
			characterMovement2D.StopImmediately();
			enemieAIController.isDead = true;
			anim.SetBool("IsDead", true);
			yield return new WaitForSeconds(2.5f);
			Destroy(gameObject);
		}

	}
}