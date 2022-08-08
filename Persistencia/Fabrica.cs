using Persistencia.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
  public class Fabrica
  {
    public static IPersistenciaCiudad GetPC() => PersistenciaCiudad.GetInstance();
    public static IPersistenciaEmpleado GetPE() => PersistenciaEmpleado.GetInstance();
    public static IPersistenciaMeteorologo GetPM() => PersistenciaMeteorologo.GetInstance();
    public static IPersistenciaPronostico GetPP() => PersistenciaPronostico.GetInstance();
  }
}
