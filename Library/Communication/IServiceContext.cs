using System.Collections.Generic;
using System.ServiceModel;
namespace Library.Communication
{
    [ServiceContract]
    public interface IServiceContext
    {
        [OperationContract]
        void CreateContext(List<ContextMessage> messageContext);

        [OperationContract]
        void DisposeContext();
    }
}
