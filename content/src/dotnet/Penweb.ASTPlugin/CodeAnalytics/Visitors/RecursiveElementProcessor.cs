using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class RecursiveElementProcessor : IRecursiveElementProcessor
    {
        public bool ProcessingIsFinished => throw new NotImplementedException();

        public bool InteriorShouldBeProcessed([NotNull] ITreeNode element)
        {
            throw new NotImplementedException();
        }

        public void ProcessAfterInterior([NotNull] ITreeNode element)
        {
            throw new NotImplementedException();
        }

        public void ProcessBeforeInterior([NotNull] ITreeNode element)
        {
            throw new NotImplementedException();
        }
    }
}
