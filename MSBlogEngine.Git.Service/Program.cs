using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSBlogEngine.Git.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var gitManager = new GitManager(new GitDirectoryProvider());

            while (true)
            {
                var result = gitManager.Pull(Properties.Settings.Default.Repository);
                Console.WriteLine("Update: " + result);

                Console.WriteLine("Sleeping for " + Properties.Settings.Default.Pause);
                Thread.Sleep(Properties.Settings.Default.Pause);
            }
        }
    }

    public class GitDirectoryProvider : IGitDirectoryProvider
    {
        public string GitExecutablePath { get { return Properties.Settings.Default.GitDirectory; } }
    }
}
