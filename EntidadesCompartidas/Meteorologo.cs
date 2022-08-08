using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{

  [DataContract]
  public class Meteorologo : Usuario
  {
    private string _email;
    private int _tel;

    [DataMember]
    public string Email
    {
      get { return _email; }
      set
      {
        if (validarCorreo(value))
          _email = value;
        else
          throw new Exception("El correo electronico no es valido.");
      }
    }

    [DataMember]
    public int Tel
    {
      get { return _tel; }
      set
      {
        if (Tel.ToString().Length != 9)
          _tel = value;
        else
          throw new Exception("Inserte un numero de telefono valido.");
      }
    }

    
    public Meteorologo(string pEmail, int pTel, string pNomUsu, string pPass, string pNomCompleto) : base(pNomUsu, pPass, pNomCompleto)
    {
      Email = pEmail;
      Tel = pTel;
    }

    public Meteorologo()
    {
    }

    public Meteorologo(string pNomUsu, string pPass, string pNomCompleto) : base(pNomUsu, pPass, pNomCompleto)
    {
    }

    private static bool validarCorreo(string email)
    {
      string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
      var regex = new Regex(pattern, RegexOptions.IgnoreCase);
      return regex.IsMatch(email);
    }
  }
}
