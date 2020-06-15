using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        healthBar.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
