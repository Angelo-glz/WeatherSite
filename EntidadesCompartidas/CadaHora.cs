using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
  [DataContract]
  public class CadaHora
  {
    private int _hora;
    private int _tempMax;
    private int _tempMin;
    private int _velViento;
    private string _tipoCielo;
    private int _probLluvia;
    private int _probTormenta;

    [DataMember]
    public int Hora
    {
      get { return _hora; }
      set
      {
        if (value >= 0 && value <= 23)
          _hora = value;
        else
          throw new Exception("La hora es incorrecta, debe ser un numero entre 0 y 23");
      }
    }
    [DataMember]
    public int TempMax
    {
      get { return _tempMax; }
      set { if (value <= 70 && value >= -20) 
          _tempMax = value;
        else
          throw new Exception("Error la temperatura debe estar entre -20° y 70°");
      }
    }

    [DataMember]
    public int TempMin
    {
      get { return _tempMin; }
      set
      {
        if (value >= -70 && value <= 40) _tempMin = value;
        else
          throw new Exception("Error la temperatura debe estar entre -70° y 40°");
      }
    }
    [DataMember]
    public int VelViento 
    { 
      get { return _velViento; }
      set
      {
        if (value >= 0 && value <= 200) _velViento = value;
        else
          throw new Exception("La velocidad del viento debe ser entre 0 y 200.");
      }
    }
    [DataMember]
    public string TipoCielo 
    {
      get { return _tipoCielo; }
      set
      {
        if (value == "Despejado" || value == "Parcialmente Nuboso" || value == "Nuboso") _tipoCielo = value;
        else
          throw new Exception("El tipo de cielo debe ser 'Despejado', 'Parcialmente Nuboso', 'Nuboso'");
      }
    }
    [DataMember]
    public int ProbLluvia 
    {
      get {return _probLluvia;}
      set
      {
        if (value >= 0 && value <= 100) _probLluvia = value;
        else
          throw new Exception("La probabilidad de lluvia debe estar entre 0 y 100.");
      }
    }
    [DataMember]
    public int ProbTormenta
    {
      get { return _probTormenta; }
      set
      {
        if (value >= 0 && value <= 100) _probTormenta = value;
        else
          throw new Exception("La probabilidad de Tormenta debe estar entre 0 y 100.");
      }
    }

    public CadaHora(int hora, int tempMax, int tempMin, int velViento, string tipoCielo, int probLluvia, int probTormenta)
    {
      Hora = hora;
      TempMax = tempMax;
      TempMin = tempMin;
      VelViento = velViento;
      TipoCielo = tipoCielo;
      ProbLluvia = probLluvia;
      ProbTormenta = probTormenta;
    }

    public CadaHora()
    {

    }
  }
}
