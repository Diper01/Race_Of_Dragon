using UnityEngine;

public class костыль2 : MonoBehaviour
{
    public UITextField txt;
    public UITextField txt_yes;
    public UITextField txt_no;

	void Start()
	{
		
	}

	void Update()
	{
        txt.text = Localisation.GetString("Are you sure you want to quit?");
        txt_yes.text = Localisation.GetString("YES");
        txt_no.text = Localisation.GetString("NO");
    }
}
