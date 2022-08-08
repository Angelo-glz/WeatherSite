using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Net;

namespace Persistencia.Class
{
  internal class PersistenciaMeteorologo : IPersistenciaMeteorologo
  {
    private static PersistenciaMeteorologo _instance = null;
    private PersistenciaMeteorologo() { }
    public static PersistenciaMeteorologo GetInstance()
    {
      if (_instance == null)
        _instance = new PersistenciaMeteorologo();
      return _instance;
    }

    public Meteorologo Logueo(string usu, string pass)
    {
      Meteorologo M = null;
      SqlConnection cnx = new SqlConnection(Conexion._cnn());

      SqlCommand cmd = new SqlCommand("LogueoMeteorologo", cnx);
      //@nomusu varchar(10), @contraseña varchar(10)
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@nomusu", usu);
      cmd.Parameters.AddWithValue("@contraseña", pass);

      try
      {
        cnx.Open();
        SqlDataReader _reader = cmd.ExecuteReader();
        if (_reader.HasRows)
        {
          _reader.Read();
          M = new Meteorologo((string)_reader["email"], (int)_reader["telefono"], usu, pass, (string)_reader["nombreComp"]);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      finally
      {
        cnx.Close();
      }
      return M;
    }

    internal Meteorologo BuscarMeteorologoTodos(string nomUsu)
    {
      Meteorologo Usu = null;
      SqlConnection cnx = new SqlConnection(Conexion._cnn());

      SqlCommand cmd = new SqlCommand("BuscarMeteorologo", cnx);
      //@nomusu varchar(10), @contraseña varchar(10), @email varchar(35)
      //,@tel int, @NombreComp varchar(30)
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@nomusu", nomUsu);

      try
      {
        cnx.Open();
        SqlDataReader _reader = cmd.ExecuteReader();
        if (_reader.HasRows)
        {
          _reader.Read();
          string _email = (string)_reader["email"];
          int _tel = (int)_reader["telefono"];
          string _nomU = (string)_reader["nomUsu"];
          string _pass = (string)_reader["contraseña"];
          string _nomC = (string)_reader["nombreComp"];

          Usu = new Meteorologo(_email, _tel, nomUsu, _pass, _nomC);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      finally
      {
        cnx.Close();
      }
      return Usu;
    }

    public Meteorologo BuscarMeteorologo(string nomUsu, Empleado U)
    {
      Meteorologo Usu = null;
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("BuscarMeteorologoActivo", cnx);
      //@nomusu varchar(10), @contraseña varchar(10), @email varchar(35)
      //,@tel int, @NombreComp varchar(30)
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@nomusu", nomUsu);

      try
      {
        cnx.Open();
        SqlDataReader _reader = cmd.ExecuteReader();
        if (_reader.HasRows)
        {
          _reader.Read();
          string _email = (string)_reader["email"];
          int _tel = (int)_reader["telefono"];
          string _nomU = (string)_reader["nomUsu"];
          string _pass = (string)_reader["contraseña"];
          string _nomC = (string)_reader["nombreComp"];

          Usu = new Meteorologo(_email, _tel, nomUsu, _pass, _nomC);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      finally
      {
        cnx.Close();
      }
      return Usu;
    }


    public void Agregar(Meteorologo M, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@nomusu varchar(10), @contraseña varchar(10), @email varchar(35), @
      //tel int, @NombreComp varchar(30)
      SqlCommand cmd = new SqlCommand("AltaUsuarioMeteorologo", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@nomUsu", M.NomUsu);
      cmd.Parameters.AddWithValue("@contraseña", M.Pass);
      cmd.Parameters.AddWithValue("@nombreComp", M.NomCompleto);
      cmd.Parameters.AddWithValue("@tel", M.Tel);
      cmd.Parameters.AddWithValue("@email", M.Email);

      SqlParameter _ret = new SqlParameter("@Retorno", SqlDbType.Int);
      _ret.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(_ret);

      int af = -1;
      try
      {
        cnx.Open();
        cmd.ExecuteNonQuery();
        af = (int)cmd.Parameters["@Retorno"].Value;
        if (af == -1)
          throw new Exception("El Meteorologo ya esta registrado.");
        else if (af == -2)
          throw new Exception("Error en la creacion del usuario.");
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        cnx.Close();
      }
    }

    public void Eliminar(Meteorologo M, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("EliminarMeteorologo", cnx);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@nomUsu", M.NomUsu);

      SqlParameter _ret = new SqlParameter("@retorno", SqlDbType.Int);
      _ret.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(_ret);

      int af = -1;
      try
      {
        cnx.Open();
        cmd.ExecuteNonQuery();
        af = (int)cmd.Parameters["@retorno"].Value;
        if (af == -1)
          throw new Exception("No exsite un Meteorologo con ese nombre de usuario.");
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        cnx.Close();
      }
    }

    public void Modificar(Meteorologo M, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@nomusu varchar(10), @contraseña varchar(10),
      //@cargaHoraria int, @nombreComp varchar(30)
      SqlCommand cmd = new SqlCommand("ModificarMeteorologo", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@nomUsu", M.NomUsu);
      cmd.Parameters.AddWithValue("@contraseña", M.Pass);
      cmd.Parameters.AddWithValue("@nombreComp", M.NomCompleto);
      cmd.Parameters.AddWithValue("@telefono", M.Tel);
      cmd.Parameters.AddWithValue("@email", M.Email);

      SqlParameter _ret = new SqlParameter("@Retorno", SqlDbType.Int);
      _ret.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(_ret);

      int af = -1;
      try
      {
        cnx.Open();
        cmd.ExecuteNonQuery();
        af = (int)cmd.Parameters["@Retorno"].Value;
        if (af == -1)
          throw new Exception("El Meteorologo no esta registrada en la base de datos.");
        else if (af == -2)
          throw new Exception("Error en la actualizacion de datos.");
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        cnx.Close();
      }
    }

    public List<Meteorologo> ListarMeteorologoesSinP(Empleado U)
    {
      string _email;
      string _nomUsu;
      int _tel;
      string _pass;
      string _nomComp;
      List<Meteorologo> _list = new List<Meteorologo>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("ListarMeteorologoSinPronostico", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _email = (string)_reader["email"];
          _tel = (int)_reader["telefono"];
          _nomUsu = (string)_reader["nomUsu"];
          _pass = (string)_reader["contraseña"];
          _nomComp = (string)_reader["nombreComp"];

          Meteorologo c = new Meteorologo(_email, _tel, _nomUsu, _pass, _nomComp);
          
          _list.Add(c);
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

    public List<Meteorologo> ListarMeteorologoesSinPAño(int año, Empleado U)
    {
      string _email;
      string _nomUsu;
      int _tel;
      string _pass;
      string _nomComp;
      List<Meteorologo> _list = new List<Meteorologo>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("ListarMeteorologoSinPronosticoAño", cnx);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@fecha", año);

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _email = (string)_reader["email"];
          _tel = (int)_reader["telefono"];
          _nomUsu = (string)_reader["nomUsu"];
          _pass = (string)_reader["contraseña"];
          _nomComp = (string)_reader["nombreComp"];

          Meteorologo c = new Meteorologo(_email, _tel, _nomUsu, _pass, _nomComp);

          _list.Add(c);
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
