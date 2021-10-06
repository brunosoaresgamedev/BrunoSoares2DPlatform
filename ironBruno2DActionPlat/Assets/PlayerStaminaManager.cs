using Platformer2D.Character;
using System.Collections;
using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour
{
	public int startingStamina;
	public int currentStamina;
	CharacterMovement2D playerMovement;
	PlayerController playerController;
	PlayerAimWeapon playerAimWeapon;
	public Animator anim;
	public bool canRecover;
	XpManager xpManager;
	[SerializeField]
	int StaminaRecover;
	
	// Use this for initialization
	void Start()
	{
		xpManager = GetComponent<XpManager>();
		playerAimWeapon = GetComponent<PlayerAimWeapon>();
		anim = GetComponent<Animator>();
		playerController = GetComponent<PlayerController>();
		playerMovement = GetComponent<CharacterMovement2D>();
		currentStamina = startingStamina;

		canRecover = true;
	}

	//gameObject.SetActive(false);

	// Update is called once per frame
	void Update()
	{

		
		if (currentStamina <= startingStamina && canRecover)
		{
			if(playerAimWeapon.canrecoverstam)
			StartCoroutine(StaminaRecoverRate());
		}
		if(currentStamina > startingStamina)
        {
			currentStamina = startingStamina;
        }
       
	}
	public void RecoverPlayer(int Recover)
	{

		currentStamina += Recover;


	}
	
	public void LostStamPlayer(int stamina)
	{
		currentStamina -= stamina;
	}
	IEnumerator StaminaRecoverRate()
	{
		canRecover = false;

		yield return new WaitForSeconds(1);
		RecoverPlayer(StaminaRecover);
		canRecover = true;
	}
}
