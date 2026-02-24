using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int bestScore = 0;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    public List<TargetBehaviour> targets = new List<TargetBehaviour>();
    public List<GameObject> weapons = new List<GameObject>();
    private List<Vector3> weaponBasePositions = new List<Vector3>();
    private List<Quaternion> weaponBaseRotations = new List<Quaternion>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        foreach (GameObject weapon in weapons)
        {
            weaponBasePositions.Add(weapon.transform.position);
            weaponBaseRotations.Add(weapon.transform.rotation);
        }
    }
    public void AddScore(int points)
    {
        score += points;
        currentScoreText.text = score.ToString();
        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = bestScore.ToString();
        }
    }
    public void ResetScore()
    {
        score = 0;
        currentScoreText.text = score.ToString();
    }
    public void SwitchTargetMovement()
    {
        foreach (TargetBehaviour target in targets)
        {
            if (target.moving)
            {
                target.SwitchMovement(false);
            }
            else
            {
                target.SwitchMovement(true);
            }
        }
    }
    public void ResetWeaponPositions()
    {
        Debug.Log("Resetting weapon positions");
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            weapons[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            weapons[i].transform.position = weaponBasePositions[i];
            weapons[i].transform.rotation = weaponBaseRotations[i];
        }
    }
}
