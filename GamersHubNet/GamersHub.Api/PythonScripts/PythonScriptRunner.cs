using System.Diagnostics;

namespace GamersHub.Api.PythonScripts
{
    public static class PythonScriptRunner
    {
        private const string PathToPython = @"C:\Users\marcin\AppData\Local\Programs\Python\Python38\python.exe";
        public static void RunScript(string scriptPath, string scriptArgv)
        {
            var start = new ProcessStartInfo
            {
                FileName = PathToPython,
                Arguments = string.Format("{0} {1}", scriptPath, scriptArgv),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
    }
}
