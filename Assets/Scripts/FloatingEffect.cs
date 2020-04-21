using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
	[SerializeField] private float floatingAmplitude;
	[SerializeField] private float floatingSpeed;
    
	private void Update()
	{
		Vector2 position = transform.position;
		position.y += floatingAmplitude * Mathf.Sin(floatingSpeed *Time.time);
		transform.position = position;   
	}
}