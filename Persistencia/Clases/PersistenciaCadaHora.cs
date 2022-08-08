using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Net;


namespace Persistencia.Class
{
  internal class PersistenciaCadaHora
  {
    private static PersistenciaCadaHora _instance = null;
    private PersistenciaCadaHora() { }
    public static PersistenciaCadaHora GetInstance()
    {
      if (_instance == null)
        _instance = new PersistenciaCadaHora();
      return _instance;
    }


    internal void AgregarCadaHora(CadaHora cH, int codPronost, SqlTransaction trs)
    {
      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("AltaPronosticoHora", trs.Connection);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@hora", cH.Hora);
      cmd.Parameters.AddWithValue("@tempMax", cH.TempMax);
      cmd.Parameters.AddWithValue("@tempMin", cH.TempMin);
      cmd.Parameters.AddWithValue("@velocidadViento", cH.VelViento);
      cmd.Parameters.AddWithValue("@tipoCielo", cH.TipoCielo);
      cmd.Parameters.AddWithValue("@probLluvia", cH.ProbLluvia);
      cmd.Parameters.AddWithValue("@probTormenta", cH.ProbTormenta);
      cmd.Parameters.AddWithValue("@codigo", codPronost);

      SqlParameter _retorno = new SqlParameter("@retorno", SqlDbType.Int);
      _retorno.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(_retorno);

      int af = -1;

      try
      {
        cmd.Transaction = trs;
        cmd.ExecuteNonQuery();
        af = (int)cmd.Parameters["@retorno"].Value;
        if (af == -1)
          throw new Exception("No existe un pronostico con ese codigo");
        if (af == -2)
          throw new Exception("Error.");

      }
      catch (Exception)
      {
        throw;
      }
    }

    internal List<CadaHora> ListarCadaHora(int codigo)
    {
      List<CadaHora> _lista = new List<CadaHora>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn());
      SqlCommand _Comando = new SqlCommand("ListarCadaHora", cnx);
      _Comando.CommandType = CommandType.StoredProcedure;

      _Comando.Parameters.AddWithValue("@codigo", codigo);

      SqlDataReader _reader;
      try
      {
        cnx.Open();
        _reader = _Comando.ExecuteReader();

        while (_reader.Read())
        {
          int _hora = (int)_reader["hora"];
          int _tempMax = (int)_reader["tempMax"];
          int _tempMin = (int)_reader["tempMin"];
          int _velViento = (int)_reader["velocidadViento"];
          int _probLluvia = (int)_reader["probLluvia"];
          int _probTormenta = (int)_reader["probTormenta"];
          string _tipoCielo = (string)_reader["tipoCielo"];

          CadaHora f = new CadaHora(_hora, _tempMax, _tempMin, _velViento, _tipoCielo, _probLluvia, _probTormenta);

          _lista.Add(f);
        }

        _reader.Close();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      finally
      {
        cnx.Close();
      }

      return _lista;
    }
  }
}
