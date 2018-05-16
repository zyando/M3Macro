using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using IronPython;
using MikuMikuPlugin;

namespace M3MacroPlugin
{
    public class Core : IResidentPlugin
    {
        public static string PluginRootPath => Application.StartupPath + "\\M3Macro\\";
        public static string SettingPythonFilePath => PluginRootPath + "config.py";

        public Guid GUID => new Guid();
        public IWin32Window ApplicationForm { get; set; }
        public Scene Scene { get; set; }

        public string Description => "M3Macro Plugin by zyando";
        public string Text => "M3Macro Plugin";
        public string EnglishText => "M3Macro Plugin";

        public Image Image { get; set; }
        public Image SmallImage => Image;

        public PythonExecutor Executor { get; } 
        private HookNativeWindow HookNativeWindow { get; }

        public dynamic ScriptDynamic { get; private set; }

        public Core()
        {
            Executor = new PythonExecutor();
            HookNativeWindow = new HookNativeWindow(this);
        }

        public void Initialize()
        {
            try
            {
                ScriptDynamic = Executor.Engine.ExecuteFile(SettingPythonFilePath, Executor.RootScope);
                HookNativeWindow.RegisterHotKeys(ScriptDynamic.init_hotkeys());
                HookNativeWindow.StartHook(Control.FromHandle(ApplicationForm.Handle));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Dispose()
        {

        }

        public void Update(float Frame, float ElapsedTime)
        {

        }

        public void Disabled()
        {
        }

        public void Enabled()
        {
        }
    }
}
