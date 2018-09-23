using UnityEngine;

public class actorHealth : MonoBehaviour
{

	public float health = 100f;

	public void takeDamage(float damageValue)
	{
		health -= damageValue;
		if(health <= 0f)
		{
			DestroyActor();
		}
	}

	void DestroyActor()
	{
		Destroy(gameObject);
	}
}
