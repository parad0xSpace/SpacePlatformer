using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaManger : MonoBehaviour
{

    private Queue<string> sentences;
    private Queue<Sprite> portraits;

    public TextMeshProUGUI npcName;
    public TextMeshProUGUI diaText;

    public Image image;

    public Animator anim;


    void Start()
    {
        sentences = new Queue<string>();
        portraits = new Queue<Sprite>();
    }

    public void StarDia(Dialogue dia)
    {
        anim.SetBool("isOpen", true);

        npcName.text = dia.name;

        sentences.Clear();
        portraits.Clear();

        foreach(string sentence in dia.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach(Sprite portrait in dia.portraits)
        {
            portraits.Enqueue(portrait);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDia();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Sprite portrait = portraits.Dequeue();
        image.GetComponent<Image>().sprite = portrait;
    }

    IEnumerator TypeSentence (string sentence)
    {
        diaText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            diaText.text += letter;
            yield return null;
        }
    }

    public void EndDia()
    {
        anim.SetBool("isOpen", false);
    }
}
