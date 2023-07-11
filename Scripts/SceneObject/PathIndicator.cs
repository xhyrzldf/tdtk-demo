using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TDTK;

namespace TDTK
{

    public class PathIndicator : MonoBehaviour
    {

        public Path path;  // 路径对象
        public LineRenderer rend;  // 线渲染器
        public float scrollSpeed = 0.5F;  // 滚动速度

        void Start()
        {
            UpdatePath();  // 开始时更新路径
        }

        void Update()
        {
            // 路径线条滚动效果
            float offset = Time.time * -scrollSpeed;
            rend.material.mainTextureOffset = new Vector2(offset, 0);
        }

        void OnEnable()
        {
            TDTK.onNewTowerE += UpdatePath;  // 当新塔被建立时更新路径
        }
        void OnDisable()
        {
            TDTK.onNewTowerE -= UpdatePath;  // 当禁用时解除事件绑定
        }

        void UpdatePath(UnitTower tower = null)
        {
            StartCoroutine(_UpdatePath());  // 更新路径，利用协程进行异步处理
        }

        IEnumerator _UpdatePath()
        {
            yield return new WaitForSeconds(0.1f);  // 等待0.1秒以确保路径更新

            List<Vector3> allPosList = path.GetAllWaypointList();  // 获取所有路径点

            Path nextPath = path.GetNextShortestPath();
            // 检查并获取下一个路径，如果存在
            while (nextPath != null)
            {
                allPosList.AddRange(nextPath.GetAllWaypointList());  // 添加下个路径的所有路径点
                nextPath = (!nextPath.IsEnd()) ? nextPath.GetNextShortestPath() : null;  // 如果不是最后一段路径则获取下一个路径
            }

            // 如果路径是一个闭环，添加第一个路点作为最后一个路点
            if (path.loop && !path.warpToStart) allPosList.Add(path.GetFirstWP());

            // 更新路径线条
            rend.positionCount = allPosList.Count;
            rend.SetPositions(allPosList.ToArray());
        }
    }
}
