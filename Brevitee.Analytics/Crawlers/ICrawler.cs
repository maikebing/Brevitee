using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Analytics.Crawlers
{
    public interface ICrawler
    {
        string Root { get; set; }
        string Name { get; set; }

        string ThreadName { get; }
        string[] ExtractTargets(string target);

        void ProcessTarget(string target);

        void Crawl();
        void Crawl(string rootTarget);
    }
}
