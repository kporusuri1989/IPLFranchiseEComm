using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public SwaggerDocumentFilter() { }
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new Dictionary<KeyValuePair<string, OpenApiPathItem>, int>();
            foreach (var path in swaggerDoc.Paths)
            {
                var orderAttribute = context.ApiDescriptions.FirstOrDefault(x => x.RelativePath.Replace("/", string.Empty)
                .Equals(path.Key.Replace("/", string.Empty), StringComparison.InvariantCultureIgnoreCase))?
                .ActionDescriptor?.EndpointMetadata?.FirstOrDefault(x => x is OrderOperationAttribute) as OrderOperationAttribute;
                if (orderAttribute == null)
                { continue;
                }
                int order = orderAttribute.Order;
                paths.Add(path, order);
            }
            var orderedPaths = paths.OrderBy(i=>i.Value).ToList();
            swaggerDoc.Paths.Clear();
            orderedPaths.ForEach(i => swaggerDoc.Paths.Add(i.Key.Key, i.Key.Value));
        }
    }

    public class OrderOperationAttribute : Attribute
    {
        private readonly int _order;

        public int Order { get { return _order; } }

        public OrderOperationAttribute(int order)
        {
            _order = order;
        }
    }
}
