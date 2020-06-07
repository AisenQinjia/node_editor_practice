using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;
using UnityEngine;
using System.IO;
namespace NodeEditorFramework.TestGame
{
    class GameConfig
    {
        public static string LuaPath = Application.dataPath + "/../Lua/";
    }
    class LuaManager
    {
        private LuaEnv _luaEnv;
        public static LuaManager _ins;
        
        public static LuaManager Instance
        {
            get
            {
                if (_ins != null)
                {
                    return _ins;
                }
                else
                {
                    _ins = new LuaManager();
                    return _ins;
                }
            }
        }

        public void Init()
        {
            if (_luaEnv == null)
            {
                _luaEnv = new LuaEnv();
                _luaEnv.AddLoader(CustomLoader);
                _luaEnv.DoString("print('hello from lua manager')");
            }
            _luaEnv.DoString(@"require 'xluaTest'");
            var luaFunc = _luaEnv.Global.Get<LuaMainPrint>("Xluatext");
            luaFunc("hello from c# called lua function");
        }
        [CSharpCallLua]
        public delegate void LuaMainPrint(string str);

        private static byte[] CustomLoader(ref string filepath)
        {
            filepath = filepath.Replace(".", "/") + ".lua";
            var fullPath = GameConfig.LuaPath + filepath;

            return ReadFile(fullPath);
        }

        private static byte[] ReadFile(string filepath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] codeBuffer = new byte[fileStream.Length];
                    fileStream.Read(codeBuffer, 0, (int)fileStream.Length);
                    return codeBuffer;
                }
            }
            catch(Exception e)
            {
                Debug.LogError("ReadFile error: " + e.ToString());
                return null;
            }
        }
    }
}
