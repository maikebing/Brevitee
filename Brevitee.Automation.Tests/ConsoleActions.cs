using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Data;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Automation;
using Brevitee.Automation.Nuget;
using System.Collections.ObjectModel;
using Brevitee.SourceControl;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.VersionControl;
using Microsoft.TeamFoundation.VersionControl.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Brevitee.Automation.Tests
{
    [Serializable]
    public class ConsoleActions: CommandLineTestInterface
    {
        public const string TFSServer = "http://tfs.klgates.com:8080/tfs";
        public const string TeamProjectCollection = "ISDEV";
        
        [ConsoleAction("List team projects")]
        public void ListTeamProjects()
        {
            Tfs.Server(TFSServer)
                .TeamProjectCollection("ISDEV")
                .GetTeamProjectNames(names =>
                {
                    names.Each(name =>
                    {
                        Out(name, ConsoleColor.Cyan);
                    });
                });
        }

        [ConsoleAction("Delete workspace")]
        public void DeleteWorkspace()
        {
            string workspaceName = Prompt("Please enter the workspace name to be deleted");
            Tfs.Server(TFSServer).TeamProjectCollection(TeamProjectCollection).QueryWorkspaces(wsArr =>
            {
                if (wsArr.Length > 0)
                {
                    Workspace workspaceToDelete = wsArr[0];
                    if (wsArr.Length > 1)
                    {
                        Out("Multiple workspaces found", ConsoleColor.Blue);
                        wsArr.Each((ws, i) =>
                        {
                            OutFormat("{0}. {1}", ConsoleColor.Yellow, i + 1, ws.Name);
                        });

                        int toDelete = NumberPrompt("Enter the number of the workspace to delete");
                        --toDelete;
                        Expect.IsTrue(toDelete >= 0 && toDelete < wsArr.Length, "Invalid selection");
                        workspaceToDelete = wsArr[toDelete];
                    }

                    workspaceToDelete.Delete();
                }
            }, workspaceName);
        }

        [ConsoleAction("Query workspaces")]
        public void QueryWorkspaces()
        {
            string workspaceName = Prompt("Please enter the workspace name to query or blank");
            string ownerName = Prompt("Please enter the owner name to query or blank");
            string computerName = Prompt("Please enter the computer name to query or blank");

            workspaceName = string.IsNullOrEmpty(workspaceName) ? null : workspaceName;
            ownerName = string.IsNullOrEmpty(ownerName) ? null : ownerName;
            computerName = string.IsNullOrEmpty(computerName) ? null : computerName;

            Tfs.Server(TFSServer)
                .TeamProjectCollection(TeamProjectCollection)
                .QueryWorkspaces(wsArr =>
                {
                    wsArr.Each(ws =>
                    {
                        OutFormat("WorkspaceName: {0}, Owner: {1}, Computer: {2}", ConsoleColor.Cyan, ws.Name, ws.OwnerDisplayName, ws.Computer);
                        OutFormat("\t{0}", ConsoleColor.Blue, ws.Comment);
                    });
                }, workspaceName, ownerName, computerName);
        }
    }
}
