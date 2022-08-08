using System.Collections.Generic;
using System.ServiceModel;
using System.Xml;
using EntidadesCompartidas;


namespace Servicio_WCF
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
  [ServiceContract]
  public interface IServicio
  {

    [OperationContract]
    void AltaCiudad(Ciudad C, Empleado U);
    [OperationContract]
    void EliminarCiudad(Ciudad C, Empleado U);
    [OperationContract]
    void ModificarCiudad(Ciudad C, Empleado U);
    [OperationContract]
    Ciudad BuscarCiudad(string pCod, Usuario U);
    [OperationContract]
    List<Ciudad> ListarCiudades(Empleado U);
    [OperationContract]
    List<Ciudad> ListarCiudadesSinP(Empleado U);
    [OperationContract]
    List<Ciudad> ListarCiudadesSinPAño(int año, Empleado U);

    [OperationContract]
    void AltaEmpleado(Empleado E, Empleado U);
    [OperationContract]
    void ModificarEmpleado(Empleado E, Empleado U);
    [OperationContract]
    void EliminarEmpleado(Empleado E, Empleado U);
    [OperationContract]
    Empleado BuscarEmpleado(string nomUsu, Empleado U);
    [OperationContract]
    Empleado LogueoEmpleado(string nomUsu, string pass);


    [OperationContract]
    void AltaMeteorologo(Meteorologo M, Empleado U);
    [OperationContract]
    void ModificarMeteorologo(Meteorologo M, Empleado U);
    [OperationContract]
    void EliminarMeteorologo(Meteorologo M, Empleado U);
    [OperationContract]
    Meteorologo BuscarMeteorologo(string nomUsu, Empleado U);
    [OperationContract]
    Meteorologo LogueoMeteorologo(string nomUsu, string pass);
    [OperationContract]
    List<Meteorologo> ListarMeteorologoSinP(Empleado U);
    [OperationContract]
    List<Meteorologo> ListarMeteorologoSinPAño(int año, Empleado U);


    [OperationContract]
    void AltaPronostico(Pronostico P, Meteorologo U);
    [OperationContract]
    string ListarPronosticoHoy();
    [OperationContract]
    List<Pronostico> ListarPronosticoUltimoAño(Empleado U);
  }
}
