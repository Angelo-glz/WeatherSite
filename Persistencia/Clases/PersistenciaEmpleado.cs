using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Net;

namespace Persistencia.Class
{
  internal class PersistenciaEmpleado : IPersistenciaEmpleado
  {
    private static PersistenciaEmpleado _instance = null;
    private PersistenciaEmpleado() { }
    public static PersistenciaEmpleado GetInstance()
    {
      if (_instance == null)
        _instance = new PersistenciaEmpleado();
      return _instance;
    }


    //buscar atla mod elimin logueo
    public Empleado Logueo(string usu, string pass)
    {
      Empleado E = null;
      SqlConnection cnx = new SqlConnection(Conexion._cnn());

      SqlCommand cmd = new SqlCommand("LogueoEmpleado", cnx);
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

          E = new Empleado((int)_reader["cargaHoraria"], usu, pass, (string)_reader["NombreComp"]);
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
      return E;
    }

    public Empleado BuscarEmpleado(string nomUsu, Empleado U)
    {
      Empleado Usu = null;
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("BuscarEmpleado", cnx);
      //@nomusu varchar(10), @contraseña varchar(10), @cargaHoraria int,
      //@nombreComp varchar(30)
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@nomusu", nomUsu);

      try
      {
        cnx.Open();
        SqlDataReader _reader = cmd.ExecuteReader();
        if (_reader.HasRows)
        {
          _reader.Read();
          int _cH = (int)_reader["cargaHoraria"];
          string _nomU = (string)_reader["nomUsu"];
          string _pass = (string)_reader["contraseña"];
          string _nomC = (string)_reader["nombreComp"];

          Usu = new Empleado(_cH, nomUsu, _pass, _nomC);
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

    public void Agregar(Empleado E, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@nomusu varchar(10), @contraseña varchar(10),
      //@cargaHoraria int, @nombreComp varchar(30)
      SqlCommand cmd = new SqlCommand("AltaUsuarioEmpleado", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@nomUsu", E.NomUsu);
      cmd.Parameters.AddWithValue("@contraseña", E.Pass);
      cmd.Parameters.AddWithValue("@nombreComp", E.NomCompleto);
      cmd.Parameters.AddWithValue("@cargaHoraria", E.CargaHoraria);

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
          throw new Exception("El empleado ya esta registrado.");
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

    public void Eliminar(Empleado E, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("EliminarEmpleado", cnx);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@nomUsu", E.NomUsu);

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
          throw new Exception("No exsite un empleado con ese nombre de usuario.");
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

    public void Modificar(Empleado E, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));
      SqlCommand cmd = new SqlCommand("ModificarEmpleado", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlParameter nomusu = new SqlParameter("@nomUsu", E.NomUsu);
      SqlParameter contraseña = new SqlParameter("@contraseña", E.Pass);
      SqlParameter nombreComp = new SqlParameter("@nombreComp", E.NomCompleto);
      SqlParameter cargaHoraria = new SqlParameter("@cargaHoraria", E.CargaHoraria);

      SqlParameter _ret = new SqlParameter("@Retorno", SqlDbType.Int);
      _ret.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(_ret);

      int af = -8;

      cmd.Parameters.Add(nomusu);
      cmd.Parameters.Add(contraseña);
      cmd.Parameters.Add(nombreComp);
      cmd.Parameters.Add(cargaHoraria);

      try
      {
        cnx.Open();
        cmd.ExecuteNonQuery();
        af = (int)cmd.Parameters["@Retorno"].Value;
        if (af == -1)
          throw new Exception("El empleado no esta registrado en la base de datos.");
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


  }
}
