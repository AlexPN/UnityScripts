using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Dictionary<GameObject,float> InvDictionary = new Dictionary<GameObject,float>();
	public GameObject test;

	private bool InventoryOpen = false;

	void Start () {
		AddRemoveItem(test,1.0f);
	}

	void Update () {
	
	}

	public void AddRemoveItem(GameObject ObjKey, float ObjAmount) {
		InvDictionary.Add(ObjKey,ObjAmount);
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width/10,
		                             Screen.height/10,
		                             (Screen.width/10)*3,
		                    		(Screen.height/10)*8));
		GUI.Box(new Rect(0,0,(Screen.width/10)*3,(Screen.height/10)*8),"Inventory");
		if (InvDictionary.Count > 0) {
			foreach (KeyValuePair<GameObject,float> item in InvDictionary) {
				/*
				item_base ObjItem = this.gameObject.AddComponent("item_base");
				ObjItem = item.Key.GetComponent("item_base") as item_base;
				Texture2D DispTex = ObjItem.InvTexture;
				GUILayout.Button(DispTex);
				*/
			}
		}
		GUILayout.EndArea();
	}
}
