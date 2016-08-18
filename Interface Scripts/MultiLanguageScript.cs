using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Reflection;

public class MultiLanguageScript : MonoBehaviour
{
	public GameObject obiectWithScript;
	public bool textsInScripts = false;
	public TextsClass[] listOfClassWithoutScripts = new TextsClass[1];
	public TextInsideScripts[] listTextsInsideScript = new TextInsideScripts[1];
	private List<TextsClass> attTexts = new List<TextsClass> ();
	private List<TextInsideScripts> insiderText = new List<TextInsideScripts> ();
	private int actualIndex = 1;
	public static bool lowerWord = false;
	public int indexOfRussia = 3;
	public int valueOfWielkoscLiter = 20;
	//Rosyjski index to 3 a wielkosc to 35
	void Awake ()
	{
		for (int i = 0; i < listOfClassWithoutScripts.Length; i++) {
			//for (int j = 0; j < listOfClass [i].languages.Length; j++) {
			attTexts.Add (new TextsClass (listOfClassWithoutScripts [i].obiectWithText, listOfClassWithoutScripts [i].obiectWithText.GetComponent<Text> (),
				listOfClassWithoutScripts [i].languages));
			//}
		}
	}

	void Start ()
	{
		
		for (int i = 0; i < listTextsInsideScript.Length; i++) {
			insiderText.Add (new TextInsideScripts (listTextsInsideScript [i].nameOfSht));
		}
		//Debug.Log ("Lower word: " + lowerWord);
		actualIndex = MenuScript.indexOfLang;
		if (obiectWithScript == null)
			obiectWithScript = this.gameObject;

		if (actualIndex == indexOfRussia) {
			SetHighWord (actualIndex, valueOfWielkoscLiter);
			lowerWord = true;
		}
		ChangeLange ();
	}

	void Update ()
	{
		/*if (Input.GetKeyDown (KeyCode.Home))
			MenuScript.indexOfLang++;*/
		if (actualIndex != MenuScript.indexOfLang) {
			actualIndex = MenuScript.indexOfLang;

			if (actualIndex == indexOfRussia && lowerWord == false) {
				SetHighWord (actualIndex, valueOfWielkoscLiter);
				lowerWord = true;
			} else if (actualIndex != indexOfRussia && lowerWord == true) {
				SetHighWord (actualIndex, valueOfWielkoscLiter);
				lowerWord = false;
			}
			ChangeLange ();
		}
	}

	public void ChangeLange ()
	{
		for (int i = 0; i < attTexts.Count; i++) {
			attTexts [i].textsInsideObiect.text = attTexts [i].languages [actualIndex];
		}
		if (textsInScripts == true) {
			for (int z = 0; z < listTextsInsideScript.Length; z++) {
				for (int j = 0; j < listTextsInsideScript [z].nameOfSht.Length; j++) {
					string[] str = new string[2];
					str [0] = listTextsInsideScript [z].nameOfSht [j].languagesScript [actualIndex];
					str [1] = listTextsInsideScript [z].nameOfSht [j].nameOfVariable;
					obiectWithScript.SendMessage ("SetNewString", str);
					/*if (obiectWithScript.name == "BomberArea") {
						obiectWithScript.SendMessage ("SetNewString", str);
					}*/
				}
			}
		}
	}

	private void SetHighWord (int idx, int wielkosc)
	{
		if (idx == indexOfRussia) { //Należt podać wartość indexu dla jez rosyjskiego
			for (int i = 0; i < listOfClassWithoutScripts.Length; i++) {
				//Debug.Log(attTexts [i].textsInsideObiect.name);
				if (attTexts [i].textsInsideObiect.fontSize - wielkosc > 25)
					attTexts [i].textsInsideObiect.fontSize -= wielkosc;
			}
			if (textsInScripts == true) {
				for (int i = 0; i < listTextsInsideScript.Length; i++) {
					for (int j = 0; j < listTextsInsideScript [i].nameOfSht.Length; j++) {
						if (insiderText [i].nameOfSht [j].textToCHange.fontSize - wielkosc > 25)
							insiderText [i].nameOfSht [j].textToCHange.fontSize -= wielkosc;
					}
				}
			}
		} else {
			for (int i = 0; i < attTexts.Count; i++) {
				if (attTexts [i].textsInsideObiect.fontSize - wielkosc > 25)
					attTexts [i].textsInsideObiect.fontSize += wielkosc;
			}
			if (textsInScripts == true) {
				for (int i = 0; i < listTextsInsideScript.Length; i++) {
					for (int j = 0; j < listTextsInsideScript [i].nameOfSht.Length; j++) {
						if (insiderText [i].nameOfSht [j].textToCHange.fontSize - wielkosc > 25)
							insiderText [i].nameOfSht [j].textToCHange.fontSize += wielkosc;
					}
				}
			}
		}
	}

}

[Serializable]
public class TextsClass
{

	public GameObject obiectWithText;
	[HideInInspector]public Text textsInsideObiect;
	public string[] languages;

	public TextsClass (GameObject objWithText, Text teksts, string[] langs)
	{
		this.obiectWithText = objWithText;
		this.textsInsideObiect = teksts;
		this.languages = langs;
	}
}

[Serializable]
public class TextInsideScripts
{

	public ZmienneDoNapisow[] nameOfSht;

	public TextInsideScripts (ZmienneDoNapisow[] nazwaZmiennej)
	{
		this.nameOfSht = nazwaZmiennej;
	}
}

[Serializable]
public class ZmienneDoNapisow
{
	
	public Text textToCHange;
	public string nameOfVariable;
	public string[] languagesScript;

	public ZmienneDoNapisow (Text tToCHange, string name, string[] langs)
	{
		this.textToCHange = tToCHange;
		this.nameOfVariable = name;
		this.languagesScript = langs;
	}
}
