using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tiny.Language.AbstractSyntax
{
    public abstract class AstNode
    {
        [JsonProperty(Order = -20)]
        [JsonIgnore]
        public Position Position { get; set; }

        public virtual IEnumerable<AstNode> GetChildren()
        {
            yield break;
        } 

        public abstract void Accept(IAstNodeVisitor visitor);
    }
}