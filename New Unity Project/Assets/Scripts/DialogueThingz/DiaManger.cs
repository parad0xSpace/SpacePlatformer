using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiaManger : MonoBehaviour
{

    private Queue<string> sentences;
    private Queue<Sprite> portraits;

    private float timer;
    private float timerPrelim = 1.1f;

    private bool timerstart = false;

    public TextMeshProUGUI npcName;
    public TextMeshProUGUI diaText;

    public Image image;

    public DiaTrigger diaTrigger; //Dia Trigger script on NPC prefab

    public Animator anim;


    void Start()
    {
        sentences = new Queue<string>();
        portraits = new Queue<Sprite>();
        timer = timerPrelim;
    }

    void Update()
    {
        if (timer > 0f && timerstart)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0f)
        {
            diaTrigger.diaSpace.SetActive(false);
            timerstart = false;
            timer = timerPrelim;
        }
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
        diaTrigger.charaCon.isTalking = false;
        timerstart = true;
    }
}
