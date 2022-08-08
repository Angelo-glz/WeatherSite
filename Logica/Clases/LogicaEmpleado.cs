using System.Collections.Generic;
using EntidadesCompartidas;
using Logica.Interfaces;

namespace Logica
{
  internal class LogicaEmpleado : ILogicaEmpleado
  {
    private static LogicaEmpleado _instance = null;
    private LogicaEmpleado() { }
    public static LogicaEmpleado GetInstance()
    {
      if (_instance == null) _instance = new LogicaEmpleado();
      return _instance;
    }

    public void Alta(Empleado E, Empleado U)
    {
      Persistencia.Fabrica.GetPE().Agregar(E, U);
    }

    public Empleado Buscar(string nUsu, Empleado U)
    {
      return Persistencia.Fabrica.GetPE().BuscarEmpleado(nUsu, U);
    }

    public void Eliminar(Empleado E, Empleado U)
    {
      Persistencia.Fabrica.GetPE().Eliminar(E, U);
    }

    public void Modificar(Empleado E, Empleado U)
    {
      Persistencia.Fabrica.GetPE().Modificar(E, U);
    }

    public Empleado Logueo(string usu, string pass)
    {
      return Persistencia.Fabrica.GetPE().Logueo(usu, pass);
    }
  }
}
