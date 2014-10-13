using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	public SortedDictionary<GameObject,int> InvDictionary = new SortedDictionary<GameObject,int>();

	private bool InventoryOpen = false;
	private int SortType = 0;
	private string[] SortTypeList = new string[] {"Name","Quantity"};

	public int SelectedItem;

	void Start () {

	}

	void Update () {
		if (Input.GetButtonDown("Inventory") && InventoryOpen) {
			InventoryOpen = false;
		}
		else if (Input.GetButtonDown("Inventory") && !InventoryOpen) {
			InventoryOpen = true;
		}
	}

	public void AddRemoveItem(GameObject ObjKey, int ObjAmount) {
		InvDictionary.Add(ObjKey,ObjAmount);
	}

	void OnGUI() {
		if (InventoryOpen) {
			GUI.BeginGroup(new Rect(Screen.width/10,
			                             Screen.height/10,
			                             (Screen.width/10)*3,
			                    		(Screen.height/10)*8));
			GUI.Box(new Rect(0,0,(Screen.width/10)*3,(Screen.height/10)*8),"Inventory");
			GUI.Label(new Rect(32,32,64,24), "Sort by:");
			SortType = GUI.Toolbar(new Rect(96,32,128,24), SortType,SortTypeList);
			if (InvDictionary.Count > 0) {
				GameObject[] objs = new GameObject[] { };
				if (SortType == 0) {
					//Items ordered by name
					InvDictionary.OrderBy(key => key.Key);
					//foreach (KeyValuePair<GameObject,int> item in InvDictionary.OrderBy(key => key.Key)) {
						/*
						item_base ObjItem = this.gameObject.AddComponent("item_base");
						ObjItem = item.Key.GetComponent("item_base") as item_base;
						Texture2D DispTex = ObjItem.InvTexture;
						GUILayout.Button(DispTex);
						*/
					//}
				}
				else {
					//Items ordered by quantity
					InvDictionary.OrderBy(key => key.Value);
					//foreach (KeyValuePair<GameObject,int> item in InvDictionary.OrderBy(key => key.Value)) {
						/*
						item_base ObjItem = this.gameObject.AddComponent("item_base");
						ObjItem = item.Key.GetComponent("item_base") as item_base;
						Texture2D DispTex = ObjItem.InvTexture;
						GUILayout.Button(DispTex);
						*/
					//}
				}
				InvDictionary.Keys.CopyTo(objs,0);
				//SelectedItem = GUI.SelectionGrid(new Rect(),SelectedItem,objs.ToString(),5);
			}
			GUI.EndGroup();
		}
	}
}
