using System.Collections.Generic;
using EntidadesCompartidas;
using Logica.Interfaces;


namespace Logica
{
  internal class LogicaCiudad : ILogicaCiudad
  {
    private static LogicaCiudad _instance = null;
    private LogicaCiudad() { }
    public static LogicaCiudad GetInstance()
    {
      if (_instance == null) _instance = new LogicaCiudad();
      return _instance;
    }

    public void Alta(Ciudad C, Empleado U)
    {
      Persistencia.Fabrica.GetPC().Agregar(C, U);
    }

    public Ciudad Buscar(string Code, Usuario U)
    {
      return Persistencia.Fabrica.GetPC().BuscarCiudad(Code, U);
    }

    public void Eliminar(Ciudad C, Empleado U)
    {
      Persistencia.Fabrica.GetPC().Eliminar(C, U);
    }

    public List<Ciudad> ListarCiudades(Empleado U)
    {
      return Persistencia.Fabrica.GetPC().ListarCiudades(U);
    }

    public List<Ciudad> ListarCiudadesSinP(Empleado U)
    {
      return Persistencia.Fabrica.GetPC().ListarCiudadesSinP(U);
    }

    public List<Ciudad> ListarCiudadesSinPAño(int año, Empleado U)
    {
      return Persistencia.Fabrica.GetPC().ListarCiudadesSinPAño(año, U);
    }

    public void Modificar(Ciudad C, Empleado U)
    {
      Persistencia.Fabrica.GetPC().Modificar(C, U);
    }
  }
}
