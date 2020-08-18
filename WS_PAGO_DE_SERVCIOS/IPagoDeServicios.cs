using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ws_pago_de_servicios_entidades.Exceptions;
using ws_pago_de_servicios_entidades.Requests;
using ws_pago_de_servicios_entidades.Response;
using ws_pago_de_servicios_entidades.Responses;

namespace WS_PAGO_DE_SERVCIOS
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPagoDeServicios
    {
        [OperationContract]
        [FaultContract(typeof(_ExceptionPagarServicios))]
   
        _ResponsePagarServicios PagarServicio(RequestPagarServicios request);

        [OperationContract]
        [FaultContract(typeof(_ExceptionPagarServicios))]
        _ResponseConsultaSaldo ConsultarSaldoSerivicio(RequestPagarServicios requestPagarServicios);


        [OperationContract]
        [FaultContract(typeof(_ExceptionPagarServicios))]
        _ResponseConfirmaTransaccion ConfirmaTransaccion(RequestPagarServicios requestPagarServicios);


        [OperationContract]
        [FaultContract(typeof(_ExceptionPagarServicios))]
        _ResponseConsultaProductos ConsultaProductos();

        [OperationContract]
        string GenerarToken();


    }

}
