using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
  [DataContract]
  public class Pronostico
  {
    private int _codigo;
    private DateTime _fecha;
    private Ciudad _ciudad;
    private List<CadaHora> _cadaHora;
    private Meteorologo _meteorologo;

    [DataMember]
    public int Codigo
    {
      get { return _codigo; }
      set { _codigo = value; }
    }

    [DataMember]
    public DateTime Fecha
    {
      get { return _fecha; }
      set { _fecha = value; }
    }

    [DataMember]
    public Ciudad Ciudad
    {
      get { return _ciudad; }
      set
      {
        if (value != null)
          _ciudad = value;
        else
          throw new Exception("Introduzca una ciudad valida.");
      }
    }


    [DataMember]
    public Meteorologo Meteorologo
    {
      get { return _meteorologo; }
      set { if (value == null) throw new Exception("Debe tener un meteorologo valido asociado."); else _meteorologo = value; }
    }

    [DataMember]
    public List<CadaHora> CadaHora 
    { 
      get { return _cadaHora; }
      set 
      {
        if(value == null)
          throw new Exception("El pronostico debe contener 1 pronostico obligatorio para cada hora del dia.");
        if (value.Count <= 0 || value.Count > 24)
          throw new Exception("El pronostico debe contener 1 pronostico obligatorio para cada hora del dia.");
        else
          _cadaHora = value;
      }
    }

    public Pronostico(int codigo, DateTime fecha, Ciudad ciudad, Meteorologo meteorologo, List<CadaHora> pronosticoHoras)
    {
      Codigo = codigo;
      Fecha = fecha;
      Ciudad = ciudad;
      Meteorologo = meteorologo;
      CadaHora = pronosticoHoras;
    }

    public Pronostico()
    {
    }
  }
}
