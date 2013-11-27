using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Yaml;
using Brevitee.Configuration;

namespace Brevitee.Server
{
    public class PageModel
    {
        public PageModel()
        {
            this.LayoutTemplate = "layout";
            this._scripts = new List<string>();
        }

        public void AddScript(string scriptName)
        {
            this._scripts.Add(scriptName);
        }

        public void RemoveScript(string scriptName)
        {
            this._scripts.Remove(scriptName);
        }

        public void AddStylesheet(string stylesheetName)
        {
            this._stylesheets.Add(stylesheetName);
        }

        public void RemoveStylesheet(string stylesheetName)
        {
            this._stylesheets.Remove(stylesheetName);
        }

        /// <summary>
        /// The name of the layout template (dust template) to use, found in ~/content/templates/
        /// </summary>
        public string LayoutTemplate { get; set; }
        
        /// <summary>
        /// The name of the application
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// The name of the page
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The title of the page
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Another list of link data (intended to be a static
        /// set of options to appear after the FirstNavList, 
        /// may be used in any way you feel useful)
        /// </summary>
        public LinkModel[] NavList { get; set; }

        /// <summary>
        /// A list of pages (intended to be used as the list 
        /// of other pages in the main menu bar)
        /// </summary>
        public LinkModel[] Pages { get; set; }

        /// <summary>
        /// A list of sections on the current page
        /// </summary>
        public LinkModel[] SectionList { get; set; }

        /// <summary>
        /// The copyright year
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// The content
        /// </summary>
        public string Content { get; set; }

        List<string> _scripts;
        /// <summary>
        /// A list of scripts in ~/scripts (intended to be included
        /// in the head section of the page, see layout.dust)
        /// </summary>
        public string[] Scripts
        {
            get
            {
                return _scripts.ToArray();
            }
            set
            {
                _scripts = new List<string>(value);
            }
        }

        List<string> _stylesheets;
        /// <summary>
        /// A list of stylesheets in ~/content/css (intended to be included in 
        /// the head section of the page, see layout.dust)
        /// </summary>
        public string[] Stylesheets
        {
            get
            {
                return _stylesheets.ToArray();
            }
            set
            {
                _stylesheets = new List<string>(value);
            }
        }
    }
}
