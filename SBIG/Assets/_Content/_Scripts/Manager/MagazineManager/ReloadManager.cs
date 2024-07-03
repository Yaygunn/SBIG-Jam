using Scriptables.Magazines;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Singleton;

namespace Managers.Magazines
{
    public class ReloadManager : Singleton<ReloadManager>
    { 
        [SerializeField] private MagHolder _magHolder;

        private List<BaseMagazine> _priorityMagList = new List<BaseMagazine>();
        public BaseMagazine GetNewMagazine()
        {
            if(_priorityMagList.Count > 0)
            {
                BaseMagazine returnMagazine = _priorityMagList[0];
                _priorityMagList.RemoveAt(0);
                return returnMagazine;
            }
            else
            {
                return _magHolder.GetRandomMagazine();
            }
        }

        public void AddPriorityMagazine(BaseMagazine magazine)
        {
            _priorityMagList.Add(magazine);
        }

    }
}
