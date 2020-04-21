using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVolume : MonoBehaviour
{
    public void GameManagerVolume(float value)
    {
        GameManager.Instance.volume = value;
    }
}
