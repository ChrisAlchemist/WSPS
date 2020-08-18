using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace WS_PAGO_DE_SERVCIOS.Interceptor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Interceptor : Attribute, IServiceBehavior, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            MessageInspector inspector = new MessageInspector();
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            ChannelDispatcher channelDispatcher = endpointDispatcher.ChannelDispatcher;
            if (channelDispatcher != null)
            {
                foreach (EndpointDispatcher ed in channelDispatcher.Endpoints)
                {
                    MessageInspector inspector = new MessageInspector();
                    ed.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }
            

        public void Validate(ServiceEndpoint endpoint)
        { }
               
        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher eDispatcher in cDispatcher.Endpoints)
                {
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(new MessageInspector());
                }
            }
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { }
        
        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { }
    }

}