using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using TDTK;

// 自定义TowerBuffSpot的Inspector界面
[CustomEditor(typeof(TowerBuffSpot))]
public class I_TowerBuffSpotEditor : _TDInspector
{
	private TowerBuffSpot instance; // 当前正在编辑的TowerBuffSpot实例

	// 当脚本加载时运行，用于获取当前实例
	public override void Awake(){
		base.Awake();
		instance = (TowerBuffSpot)target;
	}
	
	// 这个方法用于自定义Inspector界面
	public override void OnInspectorGUI(){
		base.OnInspectorGUI();
		
		// 如果当前实例为空，则重新获取
		if(instance==null){ Awake(); return; }

		// 显示分割线和标签
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Effects On Tower:");
		
		// 对于buffEffectPIDList中的每个效果，显示一个下拉菜单和一个删除按钮
		for(int i=0; i<instance.buffEffectPIDList.Count; i++){
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(" - ", GUILayout.MaxWidth(15));
				
			int effIdx=Effect_DB.GetPrefabIndex(instance.buffEffectPIDList[i]);		bool removeEff=false;
				
			effIdx = EditorGUILayout.Popup(effIdx, Effect_DB.label, GUILayout.MinHeight(20));
			if(GUILayout.Button("-", GUILayout.MaxWidth(20), GUILayout.MaxHeight(14))){ instance.buffEffectPIDList.RemoveAt(i); removeEff=true; }
				
			if(effIdx>=0 && !removeEff) instance.buffEffectPIDList[i]=Effect_DB.GetItem(effIdx).prefabID;
			
			GUILayout.EndHorizontal();
		}

		// 添加新效果的下拉菜单和"+"按钮
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(" - ", GUILayout.MaxWidth(15));
			
		int newEffIdx=-1;
		newEffIdx = EditorGUILayout.Popup(newEffIdx, Effect_DB.label);
		if(newEffIdx>=0){
			int newPID=Effect_DB.GetItem(newEffIdx).prefabID;
			instance.buffEffectPIDList.Add(newPID);
		}
			
		EditorGUILayout.LabelField("", GUILayout.MaxWidth(20));
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		// 显示默认的Inspector界面
		DefaultInspector();
		
		// 如果GUI改变，则保存对当前实例的更改
		if(GUI.changed) EditorUtility.SetDirty(instance);
	}
}

