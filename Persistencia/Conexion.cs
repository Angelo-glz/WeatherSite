using EntidadesCompartidas;
using System;

namespace Persistencia
{
  internal class Conexion
  {
    internal static string _cnn(Usuario pUsu = null)
    {
      if (pUsu == null)
        return "Data Source =.; Initial Catalog = BiosWeather; Integrated Security = true";
      else
        return "Data Source =.; Initial Catalog = BiosWeather; User=" + pUsu.NomUsu + ";Password=" + pUsu.Pass;
    }
  }
}
