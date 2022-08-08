using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace EntidadesCompartidas
{ 
  [DataContract]
  [KnownType(typeof(Meteorologo))]
  [KnownType(typeof(Empleado))]
  public abstract class Usuario
  {
    private string _nomUsu;
    private string _pass;
    private string _nomCompleto;

    [DataMember]
    public string NomUsu
    {
      get { return _nomUsu; }
      set
      {
        if (value.Length <= 10 && value.Length >= 3)
          _nomUsu = value;
        else
          throw new Exception("Error en el nombre de usuario.");
      }
    }

    [DataMember]
    public string Pass
    {
      get { return _pass; }
      set
      {
        if (ValidarPass(value))
          _pass = value;
        else
          throw new Exception("Error al asignar la contraseña.");
      }
    }

    [DataMember]
    public string NomCompleto
    {
      get { return _nomCompleto; }
      set
      {
        if (value.Length >= 5 && value.Length <= 30)
          _nomCompleto = value;
        else
          throw new Exception("Error en el nombre completo.");
      }
    }

    public Usuario(string pNomUsu, string pPass, string pNomCompleto)
    {
      NomUsu = pNomUsu;
      Pass = pPass;
      NomCompleto = pNomCompleto;
    }

    public Usuario()
    {
    }
    
    private bool ValidarPass(string pass)
    {
      string pattern = @"[a-zA-Z]+[a-zA-Z]+[0-9]+[0-9]+[^a-zA-Z0-9]+[a-zA-Z]+[a-zA-Z]+[0-9]+[^a-zA-Z0-9]";
      var regex = new Regex(pattern, RegexOptions.IgnoreCase);
      return regex.IsMatch(pass);
    }
  }
}
