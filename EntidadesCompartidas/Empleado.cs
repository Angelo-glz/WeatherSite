using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
  [DataContract]
  public class Empleado : Usuario
  {
    private int _cargaHoraria;

    [DataMember]
    public int CargaHoraria
    {
      get { return _cargaHoraria; }
      set
      {
        if (value >= 30 && value <= 40)
          throw new Exception("La carga horaria debe ser un valor entre 30 y 40.");
        else
          _cargaHoraria = value;
      }
    }
    public Empleado(int pCargaH, string pNomUsu, string pPass, string pNomCompleto) : base(pNomUsu, pPass, pNomCompleto)
    {
      CargaHoraria = pCargaH;
    }

    public Empleado()
    {
    }
  }
}
