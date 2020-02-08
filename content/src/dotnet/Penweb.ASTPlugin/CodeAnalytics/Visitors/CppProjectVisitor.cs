using JetBrains.ProjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppProjectVisitor : ProjectVisitor
    {
        public TextWriter TextWriter { get; set; }
        public CppProjectVisitor(TextWriter textWriter)
        {
            this.TextWriter = textWriter;
        }

        public override void VisitProjectItem(IProjectItem projectItem)
        {
            switch (projectItem.Kind)
            {
                case ProjectItemKind.PHYSICAL_FILE:
                    this.TextWriter.WriteLine($"{projectItem.Name,-30} {projectItem.GetType()}" );
                    break;
            }

            base.VisitProjectItem(projectItem);
        }
    }
}
