using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Net;

namespace Persistencia.Class
{
  internal class PersistenciaCiudad : IPersistenciaCiudad
  {
    private static PersistenciaCiudad _instance = null;
    private PersistenciaCiudad() { }
    public static PersistenciaCiudad GetInstance()
    {
      if (_instance == null)
        _instance = new PersistenciaCiudad();
      return _instance;
    }

    public void Agregar(Ciudad C, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("AltaCiudad", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@codigo", C.Codigo);
      cmd.Parameters.AddWithValue("@NombrePais", C.NombrePais);
      cmd.Parameters.AddWithValue("@NombreCiudad", C.NombreCiudad);

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
          throw new Exception("La ciudad ya esta registrada en la base de datos.");
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

    public void Eliminar(Ciudad C, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      SqlCommand cmd = new SqlCommand("EliminarCiudad", cnx);
      //@codigo varchar(6)
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@codigo", C.Codigo);

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
          throw new Exception("No exsite una ciudad con ese codigo.");
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
    public void Modificar(Ciudad C, Empleado U)
    {
      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("ModificarCiudad", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@codigo", C.Codigo);
      cmd.Parameters.AddWithValue("@NombrePais", C.NombrePais);
      cmd.Parameters.AddWithValue("@NombreCiudad", C.NombreCiudad);

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
          throw new Exception("La ciudad no esta registrada en la base de datos.");
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

    public List<Ciudad> ListarCiudades(Empleado U)
    {
      string _codigo;
      string _nomPais;
      string _nomCiudad;
      List<Ciudad> _list = new List<Ciudad>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("ListarCiudades", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _codigo = (string)_reader["codigo"];
          _nomCiudad = (string)_reader["nombreCiudad"];
          _nomPais = (string)_reader["nombrePais"];

          Ciudad c = new Ciudad(_codigo, _nomPais, _nomCiudad);
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

    public Ciudad BuscarCiudad(string pCode, Usuario U)
    {
      string _codigo;
      string _nombreCiudad;
      string _nombrePais;
      Ciudad C = null;

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("BuscarCiudadActivo", cnx);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@codigo", pCode);

      SqlDataReader _reader;
      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        if (_reader.Read())
        {
          _codigo = (string)_reader["codigo"];
          _nombreCiudad = (string)_reader["nombreCiudad"];
          _nombrePais = (string)_reader["nombrePais"];
          C = new Ciudad(_codigo, _nombrePais, _nombreCiudad);
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
      return C;
    }

    internal Ciudad BuscarCiudadTodos(string pCode)
    {
      string _codigo;
      string _nombreCiudad;
      string _nombrePais;
      Ciudad C = null;

      SqlConnection cnx = new SqlConnection(Conexion._cnn());

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("BuscarCiudad", cnx);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@codigo", pCode);

      SqlDataReader _reader;
      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        if (_reader.Read())
        {
          _codigo = (string)_reader["codigo"];
          _nombreCiudad = (string)_reader["nombreCiudad"];
          _nombrePais = (string)_reader["nombrePais"];
          C = new Ciudad(_codigo, _nombrePais, _nombreCiudad);
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
      return C;
    }

    public List<Ciudad> ListarCiudadesSinP(Empleado U)
    {
      string _codigo;
      string _nomPais;
      string _nomCiudad;
      List<Ciudad> _list = new List<Ciudad>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("ListarCiudadSinPronostico", cnx);
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _codigo = (string)_reader["codigo"];
          _nomCiudad = (string)_reader["nombreCiudad"];
          _nomPais = (string)_reader["nombrePais"];

          Ciudad c = new Ciudad(_codigo, _nomPais, _nomCiudad);
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

    public List<Ciudad> ListarCiudadesSinPAño(int año, Empleado U)
    {
      string _codigo;
      string _nomPais;
      string _nomCiudad;
      List<Ciudad> _list = new List<Ciudad>();

      SqlConnection cnx = new SqlConnection(Conexion._cnn(U));

      //@codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
      SqlCommand cmd = new SqlCommand("ListarCiudadSinPronosticoAño", cnx);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@fecha", año);

      SqlDataReader _reader;

      try
      {
        cnx.Open();
        _reader = cmd.ExecuteReader();

        while (_reader.Read())
        {
          _codigo = (string)_reader["codigo"];
          _nomCiudad = (string)_reader["nombreCiudad"];
          _nomPais = (string)_reader["nombrePais"];

          Ciudad c = new Ciudad(_codigo, _nomPais, _nomCiudad);
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
