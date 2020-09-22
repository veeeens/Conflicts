using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	public Text textDisplay;

	public Dialogue dialogue;

	private int index;

	public GameObject continueButton;
    public GameObject button1;
    public GameObject button2;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private List<string> choicesMade = new List<string>();
	

    void Start()
    {
    	speakerUILeft  = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();
        Initialize();
        index++;
        StartCoroutine(Type());
    }

    private void Initialize() {
        //conversationStarted = true;
        index = -1;
        speakerUILeft.Speaker = dialogue.speakerLeft;
        speakerUIRight.Speaker = dialogue.speakerRight;
    }

    void Update()
    {
    	if(index%2 == 0){
    		speakerUILeft.Show();
    		speakerUIRight.Hide();
    	}
    	else{
    		speakerUILeft.Hide();
    		speakerUIRight.Show();
    	}
        if(textDisplay.text == dialogue.dialogues[index]){
        	continueButton.SetActive(true);
        }
    }

    IEnumerator Type(){
		foreach(char letter in dialogue.dialogues[index].ToCharArray()){
			textDisplay.text += letter;
			yield return new WaitForSeconds(0.01f);
		}
	}

	public void NextSentence(){

		continueButton.SetActive(false);

		if(index < dialogue.dialogues.Length-1){
			index++;
			textDisplay.text = "";
			StartCoroutine(Type());
		}
		else{
			continueButton.SetActive(false);
			textDisplay.text = "";
			if(dialogue.continueDialogue != null){
				ContinueDialogue();
			}
			else if(dialogue.responseOptions.Length != 0){
				index = 0;
				button1.SetActive(true);
				button1.GetComponent<Text>().text = dialogue.responseOptions[0].text;
				button2.SetActive(true);
				button2.GetComponent<Text>().text = dialogue.responseOptions[1].text;
			}
			else{
				textDisplay.text = "end";
			}
		}
	}

	public void ContinueDialogue(){
		dialogue = dialogue.continueDialogue;
		Initialize();
		NextSentence();
	}

	public void ChooseOption1(){
		choicesMade.Add(dialogue.responseOptions[0].text);
		dialogue = dialogue.responseOptions[0].nextDialogue;
		button1.SetActive(false);
		button2.SetActive(false);
		Initialize();
		NextSentence();
	}
	public void ChooseOption2(){
		choicesMade.Add(dialogue.responseOptions[1].text);
		dialogue = dialogue.responseOptions[1].nextDialogue;
		button1.SetActive(false);
		button2.SetActive(false);
		Initialize();
		NextSentence();
	}
}
