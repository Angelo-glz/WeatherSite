using Logica;
using Logica.Interfaces;
using System;

namespace Logica
{
  public class Fabrica
  {
    public static ILogicaCiudad GetLC() => LogicaCiudad.GetInstance();
    public static ILogicaEmpleado GetLE() => LogicaEmpleado.GetInstance();
    public static ILogicaMeteorologo GetLM() => LogicaMeteorologo.GetInstance();
    public static ILogicaPronostico GetLP() => LogicaPronostico.GetInstance();
  }
}
