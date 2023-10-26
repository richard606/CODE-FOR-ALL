using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeForAll.APP.UI
{
    public class HomeMenuUI : MonoBehaviour
    {
        [SerializeField] HomeModel homeModel;
        [SerializeField] Button m_playBtn;
        [SerializeField] Button m_avatarBtn;

        private void Start()
        {
            m_playBtn.onClick.AddListener(()=> homeModel.Play());
            
        }
    }

}

