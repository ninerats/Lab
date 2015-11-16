using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using NUnit.Framework;

//http://blogs.msdn.com/b/buckh/archive/2012/03/10/team-foundation-version-control-client-api-example-for-tfs-2010-and-newer.aspx
namespace Explore.TFS
{
    [TestFixture]
    public class VersionControlTest
    {
        [Test]
        public void GetItemTest()
        {
            Example.Execute("http://tfs2013:8080/tfs", "$/RDB/Lab/Explore.TFS/Explore.TFS/TestProject");
        }
    }

    class Example 
    {
        public static void Execute(string collectionUrl, string teamProjectPath)
        {
            // Verify that we have the arguments we require.
            if (collectionUrl ==null || teamProjectPath == null)
            {
                String appName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                Console.Error.WriteLine("Usage: {0} collectionUrl teamProjectPath", appName);
                Console.Error.WriteLine("Example: {0} http://tfsserver:8080/tfs/DefaultCollection $/MyProject", appName);
                Environment.Exit(1);
            }

            // Get a reference to our Team Foundation Server.
            TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(collectionUrl));

            // Get a reference to Version Control.
            VersionControlServer versionControl = tpc.GetService<VersionControlServer>();

            // Listen for the Source Control events.
            versionControl.NonFatalError += OnNonFatalError;
            versionControl.Getting += OnGetting;
            versionControl.BeforeCheckinPendingChange += OnBeforeCheckinPendingChange;
            versionControl.NewPendingChange += OnNewPendingChange;

            // Create a workspace.
            Workspace workspace = versionControl.CreateWorkspace("BasicSccExample", versionControl.AuthorizedUser);

            String topDir = null;

            try
            {
                String localDir = @"c:\temp\BasicSccExample";
                Console.WriteLine("\r\n--- Create a mapping: {0} -> {1}", teamProjectPath, localDir);
                workspace.Map(teamProjectPath, localDir);

                Console.WriteLine("\r\n--- Get the files from the repository.\r\n");
                workspace.Get();

                Console.WriteLine("\r\n--- Create a file.");
                topDir = Path.Combine(workspace.Folders[0].LocalItem, "sub");
                Directory.CreateDirectory(topDir);
                String fileName = Path.Combine(topDir, "basic.cs");
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("revision 1 of basic.cs");
                }

                Console.WriteLine("\r\n--- Now add everything.\r\n");
                workspace.PendAdd(topDir, true);

                Console.WriteLine("\r\n--- Show our pending changes.\r\n");
                PendingChange[] pendingChanges = workspace.GetPendingChanges();
                Console.WriteLine("  Your current pending changes:");
                foreach (PendingChange pendingChange in pendingChanges)
                {
                    Console.WriteLine("    path: " + pendingChange.LocalItem +
                                      ", change: " + PendingChange.GetLocalizedStringForChangeType(pendingChange.ChangeType));
                }

                Console.WriteLine("\r\n--- Checkin the items we added.\r\n");
                int changesetNumber = workspace.CheckIn(pendingChanges, "Sample changes");
                Console.WriteLine("  Checked in changeset " + changesetNumber);

                Console.WriteLine("\r\n--- Checkout and modify the file.\r\n");
                workspace.PendEdit(fileName);
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("revision 2 of basic.cs");
                }

                Console.WriteLine("\r\n--- Get the pending change and check in the new revision.\r\n");
                pendingChanges = workspace.GetPendingChanges();
                changesetNumber = workspace.CheckIn(pendingChanges, "Modified basic.cs");
                Console.WriteLine("  Checked in changeset " + changesetNumber);
            }
            finally
            {
                if (topDir != null)
                {
                    Console.WriteLine("\r\n--- Delete all of the items under the test project.\r\n");
                    workspace.PendDelete(topDir, RecursionType.Full);
                    PendingChange[] pendingChanges = workspace.GetPendingChanges();
                    if (pendingChanges.Length > 0)
                    {
                        workspace.CheckIn(pendingChanges, "Clean up!");
                    }

                    Console.WriteLine("\r\n--- Delete the workspace.");
                    workspace.Delete();
                }
            }
        }

        internal static void OnNonFatalError(Object sender, ExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.Error.WriteLine("  Non-fatal exception: " + e.Exception.Message);
            }
            else
            {
                Console.Error.WriteLine("  Non-fatal failure: " + e.Failure.Message);
            }
        }

        internal static void OnGetting(Object sender, GettingEventArgs e)
        {
            Console.WriteLine("  Getting: " + e.TargetLocalItem + ", status: " + e.Status);
        }

        internal static void OnBeforeCheckinPendingChange(Object sender, ProcessingChangeEventArgs e)
        {
            Console.WriteLine("  Checking in " + e.PendingChange.LocalItem);
        }

        internal static void OnNewPendingChange(Object sender, PendingChangeEventArgs e)
        {
            Console.WriteLine("  Pending " + PendingChange.GetLocalizedStringForChangeType(e.PendingChange.ChangeType) +
                              " on " + e.PendingChange.LocalItem);
        }
    }
}
