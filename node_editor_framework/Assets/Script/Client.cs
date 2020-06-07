using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace NodeEditorFramework.TestGame
{
    class Client : MonoBehaviour 
    {
        private LuaManager _luaMgr;
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            _luaMgr = LuaManager.Instance;
            _luaMgr.Init();
        }
    }
}
