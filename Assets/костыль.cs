using UnityEngine;

public class костыль : MonoBehaviour
{
    public Transform go1;
    public Transform go2;

	void Start()
	{
		
	}

	void Update()
	{
        foreach (Transform ch in go1)
            ch.gameObject.SetActive(true);

        foreach (Transform ch in go2)
            ch.gameObject.SetActive(true);
    }
}
