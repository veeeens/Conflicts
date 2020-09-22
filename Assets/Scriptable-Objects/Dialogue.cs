﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Responses{
	[TextArea(1,5)]
	public string text;
	public Dialogue nextDialogue;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
	public Character speakerLeft;
	public Character speakerRight;
	[TextArea(2,5)]
    public string[] dialogues;
    public Responses[] responseOptions;
    public Dialogue continueDialogue;
}