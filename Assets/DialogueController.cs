using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	public Text textDisplay;
	//public string[] dialogues;

	public Dialogue dialogue;

	private int index;
	//public Response[] responses;

	public GameObject continueButton;
    public GameObject button1;
    public GameObject button2;
	

    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        if(textDisplay.text == dialogue.dialogues[index]){
        	continueButton.SetActive(true);
        }
    }

    IEnumerator Type(){
		foreach(char letter in dialogue.dialogues[index].ToCharArray()){
			textDisplay.text += letter;
			yield return new WaitForSeconds(0.02f);
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
			textDisplay.text = "";
			continueButton.SetActive(false);
			if(dialogue.responseOptions.Length != 0){
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

	public void ChooseOption1(){
		index = -1;
		dialogue = dialogue.responseOptions[0].nextDialogue;
		button1.SetActive(false);
		button2.SetActive(false);
		NextSentence();
	}
	public void ChooseOption2(){
		index = -1;
		dialogue = dialogue.responseOptions[1].nextDialogue;
		button1.SetActive(false);
		button2.SetActive(false);
		NextSentence();
	}
}
