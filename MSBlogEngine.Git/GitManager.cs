using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSBlogEngine.Git
{
    public class GitManager
    {
        private readonly IGitDirectoryProvider _gitDirectoryProvider;

        public GitManager(IGitDirectoryProvider gitDirectoryProvider )
        {
            _gitDirectoryProvider = gitDirectoryProvider;
        }

        public bool Pull(string directory)
        {
            var gitInfo = new ProcessStartInfo();
            gitInfo.CreateNoWindow = true;
            gitInfo.UseShellExecute = false;
            gitInfo.RedirectStandardError = true;
            gitInfo.RedirectStandardOutput = true;
            gitInfo.FileName = _gitDirectoryProvider.GitExecutablePath + @"\bin\git.exe";

            var gitProcess = new Process();
            gitInfo.Arguments = "pull";
            gitInfo.WorkingDirectory = directory;

            gitProcess.StartInfo = gitInfo;
            gitProcess.Start();

            var sterr = gitProcess.StandardError.ReadToEnd();
            var stout = gitProcess.StandardOutput.ReadToEnd();

            gitProcess.WaitForExit();
            gitProcess.Close();

            //if (sterr.Any())
            //    throw new Exception(sterr);

            if(stout.Contains("Already up-to-date."))
                return false;

            if (stout.Contains("Updating"))
                return true;

            return false;
        }


    }
}
