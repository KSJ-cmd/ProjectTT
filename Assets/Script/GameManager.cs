using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int health;
    public Image[] UIhealth;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HealthDown()
    {
        if (player.HP< 1)
        {
            player.HP--;
            UIhealth[player.HP].color = new Color(1, 0, 0, 0.4f);
        }

        else
        {
            //All Health UI off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);

            // Player Die Effect
            player.OnDie();

            //Retry Button
            //UIRestarBtn.SetActive(true);
        }
    }
}
