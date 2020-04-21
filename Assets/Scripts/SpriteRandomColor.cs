using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomColor : MonoBehaviour
{
	[SerializeField] private List<Color> colorList;
	[SerializeField] private List<SpriteRenderer> spriteList;
	
	void Start()
	{
		Color color;
		
		if (colorList.Count <= 0)
			color = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
		else
			color = colorList[Random.Range(0, colorList.Count)];
		color.a = 1.0f;
		for (int i = 0; i < spriteList.Count; i++)
			spriteList[i].color = color;
	}
}