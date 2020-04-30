using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimation", menuName = "ScriptableObjects/SpriteAnimationList", order = 1)]
public class SpriteAnimationObject : ScriptableObject
{
    public Sprite[] Sprites;
}