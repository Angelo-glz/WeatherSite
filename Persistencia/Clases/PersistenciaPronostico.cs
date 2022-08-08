using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Net;


namespace Persistencia.Class
{
  internal class PersistenciaPronostico : IPersistenciaPronostico
  {
    private static PersistenciaPronostico _instance = null;
    private PersistenciaPronostico() { }
    public static PersistenciaPronostico GetInstance()
    {
      if (_instance == null)
        _instance = new PersistenciaPronostico();
      return _instance;
    }

    public void Agregar(Pronostico P, Meteorologo U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@Fecha datetime, @Ciudad varchar(6), @NomUsu varchar(10)
      SqlCommand cmd = new SqlCommand("AltaPronostico", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@fecha", P.Fecha);
      cmd.Parameters.AddWithValue("@ciudad", P.Ciudad.Codigo);
      cmd.Parameters.AddWithValue("@nomUsu", P.Meteorologo.NomUsu);

      SqlParameter _ret = new SqlParameter("@Retorno", SqlDbType.Int);
      _ret.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(_ret);

      SqlTransaction trs = null;

      int af = -1;
      try
      {
        cnx.Open();

        trs = cnx.BeginTransaction();
        cmd.Transaction = trs;
        cmd.ExecuteNonQuery();

        af = (int)cmd.Parameters["@Retorno"].Value;
        if (af == -1)
          throw new Exception("El meteorologo no existe en la base de datos.");
        else if (af == -2)
          throw new Exception("Error al agregar el pronostico.");

        foreach (CadaHora cadaHora in P.CadaHora)
        {
          PersistenciaCadaHora.GetInstance().AgregarCadaHora(cadaHora, af, trs);
        }
        trs.Commit();
      }
      catch (Exception ex)
      {
        trs.Rollback();
        throw new ApplicationException("Problemas en la base de datos: " + ex.Message);
      }
      finally
      {
        cnx.Close();
      }
    }

    public List<Pronostico> ListarUltimoAño(Empleado U)
    {
      int _codigo;
      DateTime _fecha;
      string _nomUsu;
      string _ciudad;
      List<Pronostico> _list = new List<Pronostico>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@Fecha datetime, @Ciudad varchar(6), @NomUsu varchar(10)
      SqlCommand cmd = new SqlCommand("ListarPronosticosEsteAño", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _codigo = (int)_reader["codigo"];
          _fecha = (DateTime)_reader["fecha"];
          _nomUsu = (string)_reader["nomUsu"];
          _ciudad = (string)_reader["ciudad"];

          Pronostico P = new Pronostico(_codigo, _fecha, PersistenciaCiudad.GetInstance().BuscarCiudadTodos(_ciudad), PersistenciaMeteorologo.GetInstance().BuscarMeteorologoTodos(_nomUsu), PersistenciaCadaHora.GetInstance().ListarCadaHora(_codigo));
          _list.Add(P);
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
      return _list;
    }

    public List<Pronostico> ListarHoy()
    {
      int _codigo;
      DateTime _fecha;
      string _nomUsu;
      string _ciudad;
      List<Pronostico> _list = new List<Pronostico>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn());

      //@Fecha datetime, @Ciudad varchar(6), @NomUsu varchar(10)
      SqlCommand cmd = new SqlCommand("ListarPronosticosHoy", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _codigo = (int)_reader["codigo"];
          _fecha = (DateTime)_reader["fecha"];
          _nomUsu = (string)_reader["nomUsu"];
          _ciudad = (string)_reader["ciudad"];

          Pronostico P = new Pronostico(_codigo, _fecha, PersistenciaCiudad.GetInstance().BuscarCiudadTodos(_ciudad), PersistenciaMeteorologo.GetInstance().BuscarMeteorologoTodos(_nomUsu), PersistenciaCadaHora.GetInstance().ListarCadaHora(_codigo));
          _list.Add(P);
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
      return _list;
    }
  }
}
