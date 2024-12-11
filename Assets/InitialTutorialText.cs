using UnityEngine;
using System.Collections;

public class InitialTutorialText : MonoBehaviour {

    UITextField text;
	void Start ()
    {
        text = GetComponent<UITextField>();
        switch (Localisation.CurrentLanguage)
        {
            case Languages.Ukrainian:
                {
                    text.text = "Зараз ви летите вперше, після навчання ви зможете змінити дракона та рівень";
                    break;
                }
            case Languages.Turkish:            
                {
                    text.text = "Şimdi ilk kez uçuyorsunuz. Eğitimi tamamladıktan sonra başka bir ejderha seçebilir ve seviyeyi değiştirebilirsiniz.";
                    break;
                }
            case Languages.Spanish:
                {
                    text.text = "Ahora estás volando por primera vez. Después de completar el entrenamiento, puedes elegir otro dragón y cambiar el nivel.";
                    break;
                }
            case Languages.Portuguese:
                {
                    text.text = "Agora você está voando pela primeira vez. Depois de completar o treinamento, você pode escolher outro dragão e mudar o nível.";
                    break;
                }
            case Languages.Polish:
                {
                    text.text = "Teraz lecisz po raz pierwszy. Po ukończeniu szkolenia możesz wybrać innego smoka i zmienić jego poziom.";
                    break;
                }
            case Languages.Italian:
                {
                    text.text = "Ora stai volando per la prima volta. Dopo aver completato l'allenamento, puoi scegliere un altro drago e cambiare il livello.";
                    break;
                }
            case Languages.German:
                {
                    text.text = "Jetzt fliegen Sie zum ersten Mal. Nach Abschluss des Trainings kannst du einen anderen Drachen wählen und das Level wechseln.";
                    break;
                }
            case Languages.French:
                {
                    text.text = "Maintenant, vous volez pour la première fois. Après avoir terminé la formation, vous pouvez choisir un autre dragon et modifier le niveau.";
                    break;
                }
            case Languages.English:
                {
                    text.text = "Now you are flying for the first time. After completing the training, you can choose another dragon and change the level.";
                    break;
                }
            case Languages.Arabic:
                {
                    text.text = "أنت الآن تطير لأول مرة. بعد الانتهاء من التدريب ، يمكنك اختيار تنين آخر وتغيير المستوى.";
                    break;
                }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
