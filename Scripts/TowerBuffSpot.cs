using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuffSpot : MonoBehaviour
{
	// 声明并初始化一个TowerBuffSpot对象的静态列表，用于存放所有的塔加成点
	public static List<TowerBuffSpot> allBuffSpot=new List<TowerBuffSpot>();

	// 根据传入的坐标位置，返回该位置的所有塔加成效果ID列表
	public static List<int> GetBuffPIDList(Vector3 pos){
		for(int i=0; i<allBuffSpot.Count; i++){
			if(allBuffSpot[i].ComparePos(pos)){
				return new List<int>( allBuffSpot[i].buffEffectPIDList );
			}
		}
		return new List<int>();
	}

	// 清空所有塔加成点列表
	public static void ClearList(){ allBuffSpot.Clear(); }

	// 定义一个列表，存储此塔加成点所具有的所有加成效果的ID
	public List<int> buffEffectPIDList=new List<int>();

	// 当对象被唤醒时（通常是在开始运行游戏时），将此塔加成点加入到所有塔加成点的列表中
	void Awake(){
		allBuffSpot.Add(this);
	}

	// 检查输入的坐标是否与此塔加成点的坐标相近，如果相近则返回true，否则返回false
	public bool ComparePos(Vector3 pos){
		float th=TDTK.TowerManager.GetGridSize() * 0.25f;
		pos.y=0;
		return (Mathf.Abs(transform.position.x-pos.x)<th && Mathf.Abs(transform.position.z-pos.z)<th);
	}
}  

