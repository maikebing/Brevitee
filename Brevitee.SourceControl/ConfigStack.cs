using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Data;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.Automation;
using Brevitee.Automation.Nuget;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.VersionControl.Common;
using Microsoft.TeamFoundation.VersionControl;

namespace Brevitee.SourceControl
{
    internal class ConfigStack
    {
        public TfsConfigurationServer Server { get; set; }
        public TfsTeamProjectCollection TeamProjectCollection { get; set; }
        public VersionControlServer VersionControlServer { get; set; }

        string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                Server = TfsConfigurationServerFactory.GetConfigurationServer(new Uri(_url));
            }
        }

        string _teamProjectCollectionName;
        public string TeamProjectCollectionName
        {
            get
            {
                return _teamProjectCollectionName;
            }
            set
            {
                _teamProjectCollectionName = value;
                ConfigureTeamProjectCollection();
            }
        }

        string _teamProjectName;
        public string TeamProjectName
        {
            get
            {
                return _teamProjectName;
            }
            set
            {
                _teamProjectName = value;
                ConfigureTeamProject();
            }
        }

        public TeamProject TeamProject { get; private set; }

        string _workspaceName;
        public string WorkspaceName
        {
            get
            {
                return _workspaceName;
            }
            set
            {
                _workspaceName = value;
                ConfigureWorkspace();
            }
        }
        string _workspacePath;
        public string WorkspacePath
        {
            get
            {
                return _workspacePath;
            }
            set
            {
                _workspacePath = value;
                ConfigureWorkspace();
            }
        }

        public string WorkspaceComment { get; set; }

        public Workspace Workspace { get; private set; }

        internal void DeleteWorkspace()
        {
            if (Workspace != null)
            {
                Workspace.Delete();
            }
        }

        public Workspace[] QueryWorkspaces(string workspaceName = null, string ownerName = null, string computerName = null)
        {
            Args.ThrowIf<InvalidOperationException>(VersionControlServer == null, "The VersionControlServer was null; Set TeamProjectCollection and TeamProject first");
            return VersionControlServer.QueryWorkspaces(workspaceName, ownerName, computerName);            
        }

        internal void ConfigureTeamProject()
        {
            if (!string.IsNullOrEmpty(TeamProjectName))
            {
                TeamProject = VersionControlServer.GetTeamProject(TeamProjectName);
            }
        }

        internal void ConfigureWorkspace()
        {
            if (!string.IsNullOrEmpty(WorkspaceName) && !string.IsNullOrEmpty(WorkspacePath))
            {
                CreateWorkspaceParameters parameters = new CreateWorkspaceParameters(WorkspaceName);
                parameters.Comment = WorkspaceComment;
                parameters.Folders = new WorkingFolder[] { new WorkingFolder("$/{0}"._Format(TeamProjectName), WorkspacePath) };

                Workspace = (VersionControlServer.QueryWorkspaces(parameters.WorkspaceName, parameters.OwnerName, parameters.Computer)).FirstOrDefault();
                
                if (Workspace == null)
                {
                    Workspace = VersionControlServer.CreateWorkspace(parameters);                    
                }
            }
        }

        internal void ConfigureTeamProjectCollection()
        {
            if (!string.IsNullOrEmpty(TeamProjectCollectionName))
            {
                ReadOnlyCollection<CatalogNode> teamProjectCollection = GetCatalogNodes(CatalogResourceTypes.ProjectCollection);

                CatalogNode node = (from coll in teamProjectCollection
                                    where coll.Resource.DisplayName.Equals(TeamProjectCollectionName)
                                    select coll).FirstOrDefault();

                TfsTeamProjectCollection teamProjectColl = null;
                if (node != null)
                {
                    Guid id = new Guid(node.Resource.Properties["InstanceId"]);
                    teamProjectColl = Server.GetTeamProjectCollection(id);
                    TeamProjectCollection = teamProjectColl;
                    VersionControlServer = teamProjectColl.GetService<VersionControlServer>();
                }
            }
        }

        internal ReadOnlyCollection<CatalogNode> GetCatalogNodes(params Guid[] catalogResourceTypes)
        {
            if (catalogResourceTypes == null)
            {
                catalogResourceTypes = new[] { CatalogResourceTypes.ProjectCollection };
            }

            if (catalogResourceTypes.Length == 0)
            {
                catalogResourceTypes = new[] { CatalogResourceTypes.ProjectCollection };
            }

            ReadOnlyCollection<CatalogNode> collectionNodes = Server.CatalogNode.QueryChildren(
                catalogResourceTypes,
                false, CatalogQueryOptions.None);
            return collectionNodes;
        }
    }

}
