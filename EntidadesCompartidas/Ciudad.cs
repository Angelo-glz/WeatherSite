using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
  [DataContract]
  public class Ciudad
  {
    private string _codigo;
    private string _nombrePais;
    private string _nombreCiudad;

    [DataMember]
    public string Codigo
    {
      get { return _codigo; }
      set
      {
        if (value.Length == 6 && Regex.IsMatch(value, @"^[a-zA-Z]+$"))
          _codigo = value;
        else
          throw new Exception("El codigo de ciudad no es valido.");
      }
    }

    [DataMember]
    public string NombrePais
    {
      get { return _nombrePais; }
      set
      {
        if (value.Length >= 4 && value.Length <= 15)
          _nombrePais = value;
        else
          throw new Exception("El nombre de pais debe tener entre 4 y 15 caracteres.");
      }
    }

    [DataMember]
    public string NombreCiudad
    {
      get { return _nombreCiudad; }
      set
      {
        if (value.Length >= 4 && value.Length <= 20)
          _nombreCiudad = value;
        else
          throw new Exception("El nombre de ciudad debe tener entre 4 y 20 caracteres.");
      }
    }
    public Ciudad(string codigo, string nombrePais, string nombreCiudad)
    {
      Codigo = codigo;
      NombrePais = nombrePais;
      NombreCiudad = nombreCiudad;
    }

    public Ciudad()
    {
    }
  }
}
