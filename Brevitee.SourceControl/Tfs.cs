using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    public class Tfs
    {
        ConfigStack _config;
        public Tfs(string url)
        {
            this._config = new ConfigStack { Url = url };
        }

        public Tfs Url(string url)
        {
            this._config.Url = url;
            return this;
        }

        public static Tfs Server(string url)
        {
            return new Tfs(url);
        }
        
        internal ConfigStack Config
        {
            get
            {
                return _config;
            }
        }

        public Tfs TeamProjectCollections(Action<Tfs, TfsTeamProjectCollection[]> teamProjectCollectionsAction)
        {
            ReadOnlyCollection<CatalogNode> collectionNodes = GetProjectCollectionCatalogNodes();//GetCatalogNodes(new[] { CatalogResourceTypes.ProjectCollection });

            TfsTeamProjectCollection[] projectCollections = new TfsTeamProjectCollection[collectionNodes.Count];
            collectionNodes.Each((c, i) =>
            {
                Guid guid = new Guid(c.Resource.Properties["InstanceId"]);
                projectCollections[i] = _config.Server.GetTeamProjectCollection(guid);
            });

            teamProjectCollectionsAction(this, projectCollections);

            return this;
        }

        public Tfs TeamProjectCollection(string teamProjectCollectionName)
        {
            _config.TeamProjectCollectionName = teamProjectCollectionName;

            return this;
        }

        internal Tfs TeamProjectCollection(string teamProjectCollectionName, Action<TfsTeamProjectCollection> teamProjectCollectionAction)
        {
            _config.TeamProjectCollectionName = teamProjectCollectionName;
            TfsTeamProjectCollection teamProjectColl = _config.TeamProjectCollection;

            teamProjectCollectionAction(teamProjectColl);

            return this;
        }
          
        public Tfs GetTeamProjectNames(Action<string[]> teamprojectNamesAction)
        {
            string[] names = null;
            TeamProjectCatalogNodes(_config.TeamProjectCollection, (CatalogNode[] cArr) =>
            {
                names = new string[cArr.Length];
                cArr.Each((cn, i) =>
                {
                    names[i] = cn.Resource.DisplayName;
                });
            });

            teamprojectNamesAction(names);

            return this;
        }

        internal Tfs TeamProjectCatalogNodes(TfsTeamProjectCollection teamProjectCollection, Action<Tfs, CatalogNode[]> projectAction)
        {
            ReadOnlyCollection<CatalogNode> projectNodes = GetTeamProjectNodes(teamProjectCollection);

            projectAction(this, projectNodes.ToArray());

            return this;
        }

        internal Tfs TeamProjectCatalogNodes(TfsTeamProjectCollection teamProjectCollection, Action<CatalogNode[]> projectAction)
        {
            ReadOnlyCollection<CatalogNode> projectNodes = GetTeamProjectNodes(teamProjectCollection);

            projectAction(projectNodes.ToArray());

            return this;
        }

        internal Tfs TeamProjectCatalogNode(string teamProjectCollectionDisplayName, string teamProjectDisplayName, Action<CatalogNode> projectAction)
        {
            TeamProjectCollection(teamProjectCollectionDisplayName, (tfsTeamProjectCollection) =>
            {
                CatalogNode target = null;
                if (tfsTeamProjectCollection != null)
                {

                    ReadOnlyCollection<CatalogNode> projectNodes = GetTeamProjectNodes(tfsTeamProjectCollection);
                    target = (from node in projectNodes
                                          where node.Resource.DisplayName.Equals(teamProjectDisplayName)
                                          select node).FirstOrDefault();
                }
                projectAction(target);
            });

            return this;
        }
        
        public Tfs TeamProjects(Action<TeamProject[]> teamProjectsAction, bool refresh = false)
        {
            Args.ThrowIfNull(_config.VersionControlServer, "VersionControl");

            TeamProject[] projects = _config.VersionControlServer.GetAllTeamProjects(refresh);

            teamProjectsAction(projects);

            return this;
        }

        public Tfs TeamProject(string teamProjectName)
        {
            _config.TeamProjectName = teamProjectName;

            return this;
        }

        public Tfs Workspace(string name, DirectoryInfo localDir, string comment = null)
        {
            return Workspace(name, localDir.FullName, comment);
        }

        public Tfs Workspace(string name, string localPath, string comment = null)
        {
            _config.WorkspaceComment = comment;
            _config.WorkspaceName = name;
            _config.WorkspacePath = localPath;

            return this;
        }
        
        public Tfs QueryWorkspaces(Action<Workspace[]> workspaceAction, string workspaceName = null, string ownerName = null, string computerName = null)
        {
            Workspace[] workspaces = _config.QueryWorkspaces(workspaceName, ownerName, computerName);
            workspaceAction(workspaces);

            return this;
        }

        public Tfs DeleteWorkspace()
        {
            _config.DeleteWorkspace();
            return this;
        }

        public Tfs GetLatest()
        {
            Args.ThrowIfNull(_config.Workspace, "Workspace");
            _config.Workspace.Get();
            return this;
        }

        private static ReadOnlyCollection<CatalogNode> GetTeamProjectNodes(TfsTeamProjectCollection teamProjectCollection)
        {
            CatalogNode collectionNode = teamProjectCollection.CatalogNode;
            ReadOnlyCollection<CatalogNode> projectNodes = collectionNode.QueryChildren(
                    new[] { CatalogResourceTypes.TeamProject },
                    false, CatalogQueryOptions.None);
            return projectNodes;
        }

        private ReadOnlyCollection<CatalogNode> GetProjectCollectionCatalogNodes()
        {
            return _config.GetCatalogNodes();
        }

        
    }
}
