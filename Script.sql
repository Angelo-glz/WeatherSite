use master
go

if exists(select * from SysDataBases where name='BiosWeather')
 drop database BiosWeather
go

create database BiosWeather
go
use BiosWeather
go


create table Usuarios
(
	NomUsu varchar(10) not null primary key,
	Contraseña varchar(10) not null check (Contraseña like '[a-Z][a-Z][0-9][0-9][^a-Z 0-9][a-Z][a-Z][0-9][^a-Z 0-9]'),--2 letras 2 numeros 1 simbolo 2 letras 1 numero 1 simbolo
	NombreComp varchar(30) not null,
)
go

create table Empleado(
	CargaHoraria int not null check (CargaHoraria between 36 and 56),
	NomUsu varchar(10) not null primary key foreign key references Usuarios(NomUsu)
)
go

create table Meteorologo(
	Email varchar(35) not null check(Email like '%_@__%.__%'),
	Telefono int not null,
	BajaLogica bit not null default 0,
	NomUsu varchar(10) not null primary key foreign key references Usuarios(NomUsu)
)
go

create table Ciudad(
	Codigo varchar(6) not null primary key check (Codigo like '[a-Z][a-Z][a-Z][a-Z][a-Z][a-Z]'),
	NombrePais varchar(15) not null,
	BajaLogica bit not null default 0,
	NombreCiudad varchar(20) not null
)
go

create table Pronostico(
	Codigo int identity(1,1) not null primary key,
	Fecha date not null, --check (Fecha > getdate()),
	Ciudad varchar(6) not null foreign key references Ciudad(codigo),
	NomUsu varchar(10) not null foreign key references Meteorologo(NomUsu)
)
go

create table CadaHora(
	Hora int not null check( Hora between 0 and 23),
	TempMax int not null check (TempMax < 100),
	TempMin int not null check (TempMin > -100),
	VelocidadViento int not null check(VelocidadViento > 0),
	TipoCielo varchar(20) check(TipoCielo in('Despejado', 'Parcialmente Nuboso', 'Nuboso')),
	ProbLluvia int not null check(ProbLluvia between 0 and 100),
	ProbTormenta int not null check(ProbTormenta between 0 and 100),
	Codigo int not null foreign key references Pronostico(Codigo)
	Primary Key(Hora, Codigo)
)
go

use master
go
create login [IIS APPPOOL\DefaultAppPool] from windows
go


use BiosWeather
Create user [IIS APPPOOL\DefaultAppPool] for login [IIS APPPOOL\DefaultAppPool]
go

create role Meteorologo
go

create role Empleado
go

create role Publico
go

exec sp_addrolemember 'Publico', 'IIS APPPOOL\DefaultAppPool'
go


create procedure LogueoEmpleado @nomusu varchar(10), @contraseña varchar(10)
as
begin
	select U.Contraseña, u.NombreComp, u.NomUsu, E.CargaHoraria from Usuarios U, Empleado E where E.NomUsu = U.NomUsu and E.NomUsu = @nomusu and Contraseña = @contraseña
end
go

create procedure LogueoMeteorologo @nomusu varchar(10), @contraseña varchar(10)
as
begin
	select u.Contraseña, u.NombreComp, m.Email, m.Telefono from Usuarios U, Meteorologo M where M.NomUsu = @nomusu and U.NomUsu = @nomusu and Contraseña = @contraseña and BajaLogica = 0
end
go

create procedure BuscarMeteorologoActivo @nomUsu varchar(10)
as
begin
	select M.Email, M.Telefono, M.BajaLogica, M.NomUsu, U.Contraseña, U.NombreComp 
	from Meteorologo M, Usuarios U 
	where M.NomUsu = @nomUsu and U.NomUsu = @nomUsu and BajaLogica = 0
end
go

create procedure BuscarMeteorologo @nomUsu varchar(10)
as
begin
	select M.Email, M.Telefono, M.BajaLogica, M.NomUsu, U.Contraseña, U.NombreComp 
	from Meteorologo M, Usuarios U 
	where M.NomUsu = @nomUsu and U.NomUsu = @nomUsu
end
go

create procedure AltaUsuarioMeteorologo @nomusu varchar(10), @contraseña varchar(10), @email varchar(35), @tel int, @NombreComp varchar(30)
as
begin
	declare @sentence varchar(200)

		if(exists(select * from Meteorologo where NomUsu = @nomusu and BajaLogica = 1))
		begin
		begin tran
			update Meteorologo set BajaLogica = 0, Email = @email where NomUsu = @nomusu
			if(@@ERROR <> 0)
			begin
				rollback tran
				return -2
			end
			update Usuarios set Contraseña = @contraseña, NombreComp = @NombreComp where NomUsu = @nomusu
			if(@@ERROR <> 0)
			begin
				rollback tran
				return -2
			end

			set @sentence = 'create login [' + @nomusu + '] with password = ' + QUOTENAME(@contraseña,'''')
			exec (@sentence)
			if(@@ERROR <> 0)
			begin
				rollback tran
				return -2
			end
	
			set @sentence = 'create user [' + @nomusu + '] from login [' + @nomusu + ']'
			exec (@sentence)
			if(@@ERROR <> 0)
				begin
					rollback tran
					return -2
				end
		commit tran
			
		set @sentence = 'exec sp_addrolemember "Meteorologo", ' + @nomusu
		exec (@sentence)
		return 1
		end

		if(exists(select * from Meteorologo where NomUsu = @nomusu and BajaLogica = 0))
			return -1

		if(exists(select * from Usuarios where NomUsu = @nomusu))
			return -1
		
		begin tran
			insert Usuarios values (@nomusu, @contraseña, @NombreComp)
			if(@@ERROR <>0)
			begin
				rollback tran
				return -2
			end

			insert Meteorologo (Email, Telefono, NomUsu) values(@email, @tel, @nomusu)
			if(@@ERROR)<>0
				begin
					rollback tran
					return -2
				end

			set @sentence = 'create login [' + @nomusu + '] with password = ' + QUOTENAME(@contraseña,'''')
			exec (@sentence)

			if(@@ERROR <> 0)
			begin
				rollback tran
				return -2
			end

			set @sentence = 'create user [' + @nomusu + '] from login [' + @nomusu + ']'
			exec (@sentence)

			if(@@ERROR <> 0)
				begin
					rollback tran
					return -2
				end
		commit tran

	set @sentence = 'exec sp_addrolemember "Meteorologo", ' + @nomusu
	exec (@sentence)

end
go

create procedure ModificarMeteorologo @nomUsu varchar(10), @email varchar(35), @telefono int, @contraseña varchar(10), @NombreComp varchar(30)
as
begin
	if(not exists(select * from Meteorologo where NomUsu = @nomUsu and BajaLogica = 0))
		return -1

	begin tran
	update Usuarios set NombreComp = @NombreComp where NomUsu = @nomUsu
	if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
	update Meteorologo set Email = @email, Telefono = @telefono where NomUsu = @nomUsu
	if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
	commit tran
end
go

create procedure EliminarMeteorologo @nomUsu varchar(10)
as
begin
	if(not exists(select * from Meteorologo where NomUsu = @nomUsu))
		return -1

declare @sentence varchar(200)
declare @sentence2 varchar(200)

set @sentence = 'drop user ' + @nomUsu
set @sentence2 = 'drop login ' + @nomUsu

	if(exists(select * from Pronostico where NomUsu = @nomUsu))
	begin
	begin tran
		update Meteorologo set BajaLogica = 1 where NomUsu = @nomUsu
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end

		exec (@sentence)
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
		exec (@sentence2)
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
	commit tran
	return 1
	end
	
	begin tran
		delete Meteorologo where NomUsu = @nomUsu
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
		delete Usuarios where NomUsu = @nomUsu
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
		exec (@sentence)
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
		exec (@sentence2)
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
	commit tran
end
go

create procedure BuscarEmpleado @nomusu varchar(10)
as
begin
	select U.Contraseña, U.NombreComp, U.NomUsu, E.CargaHoraria 
	from Usuarios U, Empleado E 
	where E.NomUsu = @nomusu and U.NomUsu = @nomusu
end
go

create procedure AltaUsuarioEmpleado @nomusu varchar(10), @contraseña varchar(10), @cargaHoraria int, @nombreComp varchar(30)
as
begin
	declare @sentence varchar(200)
		
		if(exists(select * from Usuarios where NomUsu = @nomusu))
			return -1
		
		begin tran
			insert Usuarios values (@nomusu, @contraseña, @NombreComp)
			if(@@ERROR <>0)
			begin
				rollback tran
				return -2
			end

			insert Empleado(CargaHoraria, NomUsu) values(@cargaHoraria, @nomusu)
			if(@@ERROR)<>0
				begin
					rollback tran
					return -2
				end

			set @sentence = 'create login [' + @nomusu + '] with password = ' + QUOTENAME(@contraseña,'''')
			exec (@sentence)

			if(@@ERROR <> 0)
			begin
				rollback tran
				return -2
			end

			set @sentence = 'create user [' + @nomusu + '] from login [' + @nomusu + ']'
			exec (@sentence)

			if(@@ERROR <> 0)
				begin
					rollback tran
					return -2
				end
		commit tran

	set @sentence = 'exec sp_addrolemember "Empleado", ' + @nomusu
	exec (@sentence)

	--set @sentence = 'exec sp_addrolemember "db_securityadmin", ' + @nomusu
	--exec (@sentence)

	set @sentence = 'exec sp_addsrvrolemember "' + @nomusu +'", "securityadmin"'
	exec (@sentence)
end
go

create procedure ModificarEmpleado @nomUsu varchar(10), @cargaHoraria int, @contraseña varchar(10), @NombreComp varchar(30)
as
begin
declare @sentence varchar(200)
	
	if(not exists(select * from Empleado where NomUsu = @nomUsu))
		return -1

	begin tran
		update Usuarios set Contraseña = @contraseña, NombreComp = @NombreComp where NomUsu = @nomUsu
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end

		update Empleado set CargaHoraria = @cargaHoraria where NomUsu = @nomUsu
		if @@ERROR <> 0
		begin
			rollback tran
			return -2
		end
	commit tran
end
go

create procedure EliminarEmpleado @nomUsu varchar(10)
as
begin
declare @sentence varchar(200)
set @sentence = 'drop user ' + @nomUsu

	if(not exists(select * from Empleado where NomUsu = @nomUsu))
		return -1

	begin tran
	delete Empleado where NomUsu = @nomUsu
	if @@ERROR <> 0
	begin
		rollback tran
		return -2
	end
	delete Usuarios where NomUsu = @nomUsu
	if @@ERROR <> 0
	begin
		rollback tran
		return -2
	end
	exec (@sentence)
	if @@ERROR <> 0
	begin
		rollback tran
		return -2
	end
	set @sentence = 'drop login ' + @nomUsu
	exec (@sentence)
	if @@ERROR <> 0
	begin
		rollback tran
		return -2
	end
	commit tran
end
go


create procedure AltaPronostico @Fecha date, @Ciudad varchar(6), @NomUsu varchar(10)
as
begin

	if(not exists(select * from Meteorologo where NomUsu = @NomUsu))
		return -1
	if(exists(select * from Meteorologo where NomUsu = @NomUsu and BajaLogica = 1))
		return -1
	if(exists(select * from Ciudad where Codigo = @Ciudad and BajaLogica = 1))
		return -2
	if(not exists(select * from Ciudad where Codigo = @Ciudad))
		return -2
	else
	begin
		insert Pronostico values(@Fecha, @Ciudad, @NomUsu)
		if @@ERROR <> 0
			return -3
		else
			return @@identity
	end
end
go

create procedure AltaPronosticoHora @hora int, @TempMax int, @TempMin int, @VelocidadViento int, @TipoCielo varchar(20), @ProbLluvia int, @ProbTormenta int, @Codigo int
as
begin
	if(not exists(select * from Pronostico where Codigo = @Codigo))
		return -1
	if(exists(select * from CadaHora where Hora = @hora and Codigo = @Codigo))
		return -2
	else
	begin
		insert CadaHora values(@hora, @TempMax, @TempMin, @VelocidadViento, @TipoCielo, @ProbLluvia, @ProbTormenta, @Codigo)
		if @@ERROR<>0
			return -3
		else
			return 1
	end

end
go

create procedure BuscarCiudad @codigo varchar(6)
as
begin
	select * from Ciudad where Codigo = @codigo
end
go

create procedure BuscarCiudadActivo @codigo varchar(6)
as
begin
	select * from Ciudad where Codigo = @codigo and BajaLogica = 0
end
go

create procedure AltaCiudad @codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
as
begin
	if(exists(select * from Ciudad where Codigo = @codigo and BajaLogica = 0))
		return -1
	if(exists(select * from Ciudad where Codigo = @codigo and BajaLogica = 1))
		update Ciudad  set BajaLogica = 0 where Codigo = @codigo
	else
		insert Ciudad (Codigo, NombrePais, NombreCiudad) values(@codigo, @NombrePais, @NombreCiudad)
		if @@ERROR <> 0
			return -2
		else
			return 1
end
go


create procedure ModificarCiudad @codigo varchar(6), @NombrePais varchar(15), @NombreCiudad varchar(20)
as
begin
	if(not exists(select * from Ciudad where Codigo = @codigo))
		return -1
	if(exists(select * from Ciudad where Codigo = @codigo and BajaLogica = 1))
		return -1
	else
	begin
		update Ciudad set NombreCiudad = @NombreCiudad, NombrePais = @NombrePais where Codigo = @codigo
		if(@@ERROR = 0)
			return 1
		else
			return -2
	end
end
go

create procedure EliminarCiudad @codigo varchar(6)
as
begin
	if(not exists(select * from Ciudad where Codigo = @codigo))
		return -1
	if(exists(select * from Pronostico where Ciudad = @codigo))
	begin
		update Ciudad set BajaLogica = 1 where Codigo =@codigo
		return 1
	end
	else
	begin
		delete Ciudad where Codigo = @codigo
		return 1
	end
end
go

create procedure ListarPronosticosEsteAño
as
begin
	select * from Pronostico where DATEPART(YEAR, Fecha) = DATEPART(YEAR, GETDATE())
end
go

create procedure ListarPronosticosHoy
as
begin
	select * from Pronostico where Fecha = Cast(getdate() as date)
end
go

create procedure ListarCadaHora @codigo int
as
begin
	select * from CadaHora where Codigo = @codigo
end
go

create procedure ListarCiudades
as
begin
	select * from Ciudad
end
go

create procedure ListarCiudadesActivo
as
begin
	select * from Ciudad where BajaLogica = 0
end
go

create procedure ListarCiudadSinPronostico
as
begin
	select * from Ciudad where Codigo not in(select Ciudad from Pronostico) and BajaLogica = 0
end
go

create procedure ListarMeteorologoSinPronostico
as
begin
	select * from Meteorologo M
	inner join Usuarios U
	on M.NomUsu = U.NomUsu
	where M.NomUsu not in(select NomUsu from Pronostico)
end
go

create procedure ListarCiudadSinPronosticoAño @fecha int
as
begin
	select * from Ciudad where Codigo not in (select Ciudad from Pronostico where datepart(year, Fecha) = @fecha)
end
go

create procedure ListarMeteorologoSinPronosticoAño @fecha int
as
begin
	select M.Email, M.NomUsu, M.Telefono, U.Contraseña, U.NombreComp from Meteorologo M
	inner join Usuarios U
	on M.NomUsu = U.NomUsu
	where M.NomUsu not in (select NomUsu from Pronostico where datepart(year, Fecha) = @fecha)
end
go

--------------------~~~*_ PERMISOS ROLES _*~~~--------------------


grant execute on dbo.LogueoEmpleado
to Publico
go

grant execute on dbo.LogueoMeteorologo
to Publico
go

grant execute on dbo.ListarPronosticosHoy
to Publico
go

grant execute on dbo.BuscarCiudad
to Publico, Meteorologo
go

grant execute on dbo.BuscarMeteorologo
to Publico
go

grant execute on dbo.AltaPronostico
to Meteorologo
go

grant execute on dbo.AltaPronosticoHora
to Meteorologo
go

grant execute on dbo.BuscarMeteorologoActivo
to Empleado
go

grant execute on dbo.ListarPronosticosEsteAño
to Empleado
go

grant execute on dbo.ListarCadaHora
to Empleado, Publico
go

grant execute on dbo.ListarCiudades
to Empleado
go

grant execute on dbo.ListarCiudadSinPronostico
to Empleado
go

grant execute on dbo.ListarCiudadSinPronosticoAño
to Empleado
go

grant execute on dbo.ListarMeteorologoSinPronostico
to Empleado
go

grant execute on dbo.ListarMeteorologoSinPronosticoAño
to Empleado
go

grant execute on dbo.BuscarMeteorologo
to Empleado
go

grant execute on dbo.AltaUsuarioMeteorologo
to Empleado
go

grant execute on dbo.ModificarMeteorologo
to Empleado
go

grant execute on dbo.EliminarMeteorologo
to Empleado
go

grant execute on dbo.AltaUsuarioEmpleado
to Empleado
go

grant execute on dbo.ModificarEmpleado
to Empleado
go

grant execute on dbo.EliminarEmpleado
to Empleado
go

grant execute on dbo.BuscarEmpleado
to Empleado
go

grant execute on dbo.BuscarCiudadActivo
to Empleado, Meteorologo
go

grant execute on dbo.AltaCiudad
to Empleado
go

grant execute on dbo.ModificarCiudad
to Empleado
go

grant execute on dbo.EliminarCiudad
to Empleado
go

grant create schema
to Empleado
go
grant alter any user 
to Empleado
go
grant alter any role 
to Empleado
go

--Datos de prueba
--Usuarios Contraseña 2 letras 2 numeros 1 simbolo 2 letras 1 numero 1 simbolo
--Empleados Carga horaria entre 36 y 56

exec AltaUsuarioEmpleado 'usuario001', 'ab12.ab1.', 50, 'Empleado 1'
go
exec AltaUsuarioEmpleado 'usuario002', 'ab12.ab1.', 50, 'Empleado 2'
go
exec AltaUsuarioEmpleado 'usuario003', 'ab12.ab1.', 50, 'Empleado 3'
go
exec AltaUsuarioEmpleado 'usuario004', 'ab12.ab1.', 50, 'Empleado 4'
go
exec AltaUsuarioEmpleado 'usuario005', 'ab12.ab1.', 50, 'Empleado 5'
go

--Meteorologos

exec AltaUsuarioMeteorologo 'usuario006', 'ab12.ab1.', 'meteo1@gmail.com', 099999999, 'Meteorologo 1'
go
exec AltaUsuarioMeteorologo 'usuario007', 'ab12.ab1.', 'meteo2@gmail.com', 099999999, 'Meteorologo 2'
go
exec AltaUsuarioMeteorologo 'usuario008', 'ab12.ab1.', 'meteo3@gmail.com', 099999999, 'Meteorologo 3'
go
exec AltaUsuarioMeteorologo 'usuario009', 'ab12.ab1.', 'meteo4@gmail.com', 099999999, 'Meteorologo 4'
go
exec AltaUsuarioMeteorologo 'usuario010', 'ab12.ab1.', 'meteo5@gmail.com', 099999999, 'Meteorologo 5'
go
exec AltaUsuarioMeteorologo 'usuario011', 'ab12.ab1.', 'meteo6@gmail.com', 099999999, 'Meteorologo 6'
go
exec AltaUsuarioMeteorologo 'usuario012', 'ab12.ab1.', 'meteo7@gmail.com', 099999999, 'Meteorologo 7'
go

--Ciudad 6 letras

exec AltaCiudad 'MVDUYU', 'Uruguay', 'Montevideo'
go
exec AltaCiudad 'CDMMEX', 'Mexico', 'Ciudad de Mexico'
go
exec AltaCiudad 'NYCUSA', 'Estados Unidos', 'Ciudad de Nueva York'
go
exec AltaCiudad 'BCNESP', 'España', 'Barcelona'
go
exec AltaCiudad 'BHDGBT', 'Gran Bretaña', 'Belfast'
go
exec AltaCiudad 'BUEARG', 'Argentina', 'Buenos Aires'
go
exec AltaCiudad 'PDPUYU', 'Uruguay', 'Punta del Este'
go
exec AltaCiudad 'CYRUYU', 'Uruguay', 'Colonia'
go
exec AltaCiudad 'FLNBRA', 'Brasil', 'Florianopolis'
go
exec AltaCiudad 'FORBRA', 'Brasil', 'Fortaleza'
go
exec AltaCiudad 'RIOBRA', 'Brasil', 'Rio de Janeiro'
go

--Pronostico

exec AltaPronostico '2022/03/15', 'MVDUYU', 'usuario006'
go
exec AltaPronostico '2022/03/16', 'MVDUYU', 'usuario007'
go
exec AltaPronostico '2022/05/15', 'MVDUYU', 'usuario006'
go
exec AltaPronostico '2022/03/15', 'NYCUSA', 'usuario011'
go
exec AltaPronostico '2022/03/15', 'BCNESP', 'usuario010'
go
exec AltaPronostico '2022/12/08', 'BCNESP', 'usuario009'
go
exec AltaPronostico '2022/03/15', 'PDPUYU', 'usuario008'
go
exec AltaPronostico '2022/03/15', 'CYRUYU', 'usuario007'
go
exec AltaPronostico '2022/07/16', 'NYCUSA', 'usuario006'
go
exec AltaPronostico '2021/03/15', 'RIOBRA', 'usuario011'
go
--CadaHora Hora 0 a 23  TipoCielo Despejado, Parcialmente Nuboso, Nuboso

exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,1
go
exec AltaPronosticoHora 2, 25,10,61,'Despejado',5,0,1
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,1
go
exec AltaPronosticoHora 4, 26,12,63,'Parcialmente Nuboso',5,0,1
go

exec AltaPronosticoHora 1, 20,15,60,'Nuboso',5,0,2
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,2
go
exec AltaPronosticoHora 3, 24,11,62,'Nuboso',5,0,2
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,2
go

exec AltaPronosticoHora 1, 20,15,60,'Nuboso',5,0,3
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,3
go
exec AltaPronosticoHora 3, 24,11,62,'Nuboso',5,0,3
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,3
go

exec AltaPronosticoHora 1, 20,15,60,'Nuboso',5,0,4
go
exec AltaPronosticoHora 2, 25,10,61,'Despejado',5,0,4
go
exec AltaPronosticoHora 3, 24,11,62,'Parcialmente Nuboso',5,0,4
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,4
go

exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,5
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,5
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,5
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,5
go


exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,6
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,6
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,6
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,6
go


exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,7
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,7
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,7
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,7
go


exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,8
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,8
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,8
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,8
go


exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,9
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,9
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,9
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,9
go


exec AltaPronosticoHora 1, 20,15,60,'Despejado',5,0,10
go
exec AltaPronosticoHora 2, 25,10,61,'Nuboso',5,0,10
go
exec AltaPronosticoHora 3, 24,11,62,'Despejado',5,0,10
go
exec AltaPronosticoHora 4, 26,12,63,'Nuboso',5,0,10
go


