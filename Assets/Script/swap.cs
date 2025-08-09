using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public GameObject malePrefab;
    public GameObject femalePrefab;

    private GameObject currentPlayer;

    void Start()
    {
        // Spawn the default player (male)
        currentPlayer = Instantiate(malePrefab, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        // Swap on key press (example: press "Q")
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapCharacter();
        }
    }

    void SwapCharacter()
    {
        if (currentPlayer != null)
        {
            Vector3 pos = currentPlayer.transform.position;
            Quaternion rot = currentPlayer.transform.rotation;

            // Destroy current player
            Destroy(currentPlayer);

            // Instantiate the other prefab
            if (currentPlayer.name.Contains(malePrefab.name))
            {
                currentPlayer = Instantiate(femalePrefab, pos, rot);
            }
            else
            {
                currentPlayer = Instantiate(malePrefab, pos, rot);
            }
        }
    }
}
