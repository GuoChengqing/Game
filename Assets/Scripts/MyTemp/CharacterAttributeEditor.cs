using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAttributeEditor : MonoBehaviour
{
    public Player player;
    public InputField currentHealthInputField;
    public InputField maxHealthInputField;
    public InputField armorInputField;
    public InputField attackInputField;
    public InputField currentLevelInputField;

    // Start is called before the first frame update
    void Start()
    {
        currentHealthInputField.SetTextWithoutNotify(player.currentHealth + "");
        maxHealthInputField.SetTextWithoutNotify(player.maxHealth + "");
        armorInputField.SetTextWithoutNotify(player.armor + "");
        attackInputField.SetTextWithoutNotify(player.attack + "");
        currentLevelInputField.SetTextWithoutNotify(player.currentLevel + "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
