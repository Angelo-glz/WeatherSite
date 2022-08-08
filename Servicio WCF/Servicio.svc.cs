using System.Collections.Generic;
using System.ServiceModel;
using EntidadesCompartidas;
using Logica;

using System.Xml;

namespace Servicio_WCF
{
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
  // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
  public class Servicio : IServicio
  {
    public void AltaCiudad(Ciudad C, Empleado U)
    {
      Fabrica.GetLC().Alta(C, U);
    }

    public void AltaEmpleado(Empleado E, Empleado U)
    {
      Fabrica.GetLE().Alta(E, U);
    }

    public void AltaMeteorologo(Meteorologo M, Empleado U)
    {
      Fabrica.GetLM().Alta(M, U);
    }

    public void AltaPronostico(Pronostico P, Meteorologo U)
    {
      Fabrica.GetLP().Alta(P, U);
    }

    public Ciudad BuscarCiudad(string pCod, Usuario U)
    {
      return Fabrica.GetLC().Buscar(pCod, U);
    }

    public Empleado BuscarEmpleado(string nomUsu, Empleado U)
    {
      return Fabrica.GetLE().Buscar(nomUsu, U);
    }

    public Meteorologo BuscarMeteorologo(string nomUsu, Empleado U)
    {
      return Fabrica.GetLM().Buscar(nomUsu, U);
    }

    public void EliminarCiudad(Ciudad C, Empleado U)
    {
      Fabrica.GetLC().Eliminar(C, U);
    }

    public void EliminarEmpleado(Empleado E, Empleado U)
    {
      Fabrica.GetLE().Eliminar(E, U);
    }

    public void EliminarMeteorologo(Meteorologo M, Empleado U)
    {
      Fabrica.GetLM().Eliminar(M, U);
    }

    public List<Ciudad> ListarCiudades(Empleado U)
    {
      return Fabrica.GetLC().ListarCiudades(U);
    }

    public List<Ciudad> ListarCiudadesSinP(Empleado U)
    {
      return Fabrica.GetLC().ListarCiudadesSinP(U);
    }

    public List<Ciudad> ListarCiudadesSinPAño(int año, Empleado U)
    {
      return Fabrica.GetLC().ListarCiudadesSinPAño(año, U);
    }

    public List<Meteorologo> ListarMeteorologoSinP(Empleado U)
    {
      return Fabrica.GetLM().ListarMeteorologosSinP(U);
    }

    public List<Meteorologo> ListarMeteorologoSinPAño(int año, Empleado U)
    {
      return Fabrica.GetLM().ListarMeterologosSinPAño(año, U);
    }

    public string ListarPronosticoHoy()
    {
      List<Pronostico> lPronosticos = Fabrica.GetLP().ListarPronosticoHoy();

      XmlDocument doc = new XmlDocument();
      doc.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <Pronostico></Pronostico>");
      XmlNode raiz = doc.DocumentElement;

      foreach (Pronostico P in lPronosticos)
      {
        XmlElement ciudad = doc.CreateElement("Ciudad");
        ciudad.InnerText = P.Ciudad.NombreCiudad;

        XmlElement pais = doc.CreateElement("Pais");
        pais.InnerText = P.Ciudad.NombrePais;

        XmlElement node = doc.CreateElement("Pronostico");
        node.AppendChild(ciudad);
        node.AppendChild(pais);

        foreach (CadaHora cadaHora in P.CadaHora)
        {
          XmlElement Hora = doc.CreateElement("Hora");
          Hora.InnerText = cadaHora.Hora.ToString();

          XmlElement TempMax = doc.CreateElement("Temperatura_Maxima");
          TempMax.InnerText = cadaHora.TempMax.ToString();

          XmlElement TempMin = doc.CreateElement("Temperatura_Minima");
          TempMin.InnerText = cadaHora.TempMin.ToString();

          XmlElement VelViento = doc.CreateElement("Velocidad_viento");
          VelViento.InnerText = cadaHora.VelViento.ToString();

          XmlElement TipoCielo = doc.CreateElement("Tipo_de_Cielo");
          TipoCielo.InnerText = cadaHora.TipoCielo;

          XmlElement ProbLluvia = doc.CreateElement("Probabilidad_de_lluvia");
          ProbLluvia.InnerText = cadaHora.ProbLluvia.ToString() + "%";

          XmlElement ProbTormenta = doc.CreateElement("Probabilidad_de_tormenta");
          ProbTormenta.InnerText = cadaHora.ProbTormenta.ToString() + "%";

          XmlElement subnode = doc.CreateElement("cadaHora");
          subnode.AppendChild(Hora);
          subnode.AppendChild(TempMax);
          subnode.AppendChild(TempMin);
          subnode.AppendChild(VelViento);
          subnode.AppendChild(TipoCielo);
          subnode.AppendChild(ProbLluvia);
          subnode.AppendChild(ProbTormenta);

          node.AppendChild(subnode);
        }
        raiz.AppendChild(node);
      }

      return doc.OuterXml;
    }

    public List<Pronostico> ListarPronosticoUltimoAño(Empleado U)
    {
      return Fabrica.GetLP().ListarUltimoAño(U);
    }

    public Empleado LogueoEmpleado(string nomUsu, string pass)
    {
      return Fabrica.GetLE().Logueo(nomUsu, pass);
    }

    public Meteorologo LogueoMeteorologo(string nomUsu, string pass)
    {
      return Fabrica.GetLM().Logueo(nomUsu, pass);
    }

    public void ModificarCiudad(Ciudad C, Empleado U)
    {
      Fabrica.GetLC().Modificar(C, U);
    }

    public void ModificarEmpleado(Empleado E, Empleado U)
    {
      Fabrica.GetLE().Modificar(E, U);
    }

    public void ModificarMeteorologo(Meteorologo M, Empleado U)
    {
      Fabrica.GetLM().Modificar(M, U);
    }

  }
}
