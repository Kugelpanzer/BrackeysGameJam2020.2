using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialScript : MonoBehaviour
{
    [TextArea]
    public List<string> tutorialTexts = new List<string>();
    public TextMeshProUGUI currentText;
    public int currentIndex = 0;
    public void Next()
    {

        if (currentIndex == tutorialTexts.Count - 1) return;
        currentIndex++;
        currentText.text = tutorialTexts[currentIndex];
    }
    public void Previus()
    {
        if (currentIndex == 0) return;
        currentIndex--;
        currentText.text = tutorialTexts[currentIndex];
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        currentText.text = tutorialTexts[currentIndex];
    }
}
