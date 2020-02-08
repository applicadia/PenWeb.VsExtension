using JetBrains.ReSharper.Psi.Cpp.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    public class CppVisitorResult
    {
    }

    public class CppTreeVisitor : ICppDeclaredElementVisitor<CppVisitorResult>
    {
        public CppVisitorResult Visit(CppLinkageEntityDeclaredElement A_0)
        {
            return new CppVisitorResult();
        }

        public CppVisitorResult Visit(CppParserSymbolDeclaredElement A_0)
        {
            return new CppVisitorResult();
        }

        public CppVisitorResult Visit(CppPreprocessorDeclaredElement A_0)
        {
            return new CppVisitorResult();
        }

        public CppVisitorResult Visit(CppResolveEntityDeclaredElement A_0)
        {
            return new CppVisitorResult();
        }

        public CppVisitorResult Visit(ICppClrDeclaredElement A_0)
        {
            return new CppVisitorResult();
        }
    }
}
