
                                                  Notas del proyecto


No logre que los request entraran en el methodo "GetPalabraEnIdioma


URL de la web API: http://localhost:52383/


---------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------

                                             --Script de la base de datos


===================================================================================================================
===================================================================================================================

                                                --SELECT STATEMENTS

SELECT * FROM TBL_USUARIOS
SELECT * FROM TBL_IDIOMAS
SELECT * FROM TBL_PRIMERA_PALABRA
SELECT * FROM TBL_PALABRAS
SELECT * FROM TBL_CONSULTAS_FRASES
SELECT * FROM TBL_PALABRAS_CONSULTADAS

===================================================================================================================
===================================================================================================================

                                                --CREATE STATEMENTS

CREATE TABLE TBL_USUARIOS(
CEDULA INT PRIMARY KEY,
NOMBRE VARCHAR(50),
APELLIDO VARCHAR(50),
NOMBRE_USUARIO VARCHAR(50))

CREATE TABLE TBL_IDIOMAS(
NOMBRE_IDIOMA VARCHAR(50) PRIMARY KEY,
PAIS_ORIGEN VARCHAR(50))

CREATE TABLE TBL_PRIMERA_PALABRA (
PALABRA_PRIMER_REGISTRO VARCHAR(50) PRIMARY KEY,
NOMBRE_IDIOMA VARCHAR(50) FOREIGN KEY (NOMBRE_IDIOMA) REFERENCES TBL_IDIOMAS (NOMBRE_IDIOMA),
TIPO VARCHAR(50))

CREATE TABLE TBL_PALABRAS 
(PALABRA VARCHAR(50) PRIMARY KEY,
 NOMBRE_IDIOMA VARCHAR(50) FOREIGN KEY (NOMBRE_IDIOMA) REFERENCES TBL_IDIOMAS (NOMBRE_IDIOMA),
 PALABRA_PRIMER_REGISTRO VARCHAR(50) FOREIGN KEY (PALABRA_PRIMER_REGISTRO) REFERENCES TBL_PRIMERA_PALABRA (PALABRA_PRIMER_REGISTRO),
 TIPO VARCHAR(50))

 CREATE TABLE TBL_CONSULTAS_FRASES (
 CODIGO_CONSULTA VARCHAR(50) PRIMARY KEY,
 CEDULA INT FOREIGN KEY (CEDULA) REFERENCES TBL_USUARIOS (CEDULA),
 FRASE VARCHAR(100),
 TRADUCCION_ESPANOL VARCHAR(100),
 CANTIDAD_PALABRAS INT,
 FECHA_CONSULTA VARCHAR(50),
 POPULARIDAD INT)

 CREATE TABLE TBL_PALABRAS_CONSULTADAS(
 CODIGO_REGISTRO INT PRIMARY KEY,
 CODIGO_CONSULTA VARCHAR(50) FOREIGN KEY (CODIGO_CONSULTA) REFERENCES TBL_CONSULTAS_FRASES (CODIGO_CONSULTA),
 PALABRA VARCHAR(50) FOREIGN KEY (PALABRA) REFERENCES TBL_PALABRAS (PALABRA))

===================================================================================================================
===================================================================================================================

  
  
  
                                                  --STORED PROCEDURES

-- procedimientos almacenados de Usuarios

--Create

GO
CREATE PROCEDURE [dbo].[CRE_USUARIO_PR]    
   (@P_CEDULA VARCHAR(50),
	@P_NOMBRE VARCHAR(50),
	@P_APELLIDO VARCHAR(50),
	@P_NOMBRE_USUARIO VARCHAR(50))
AS
	INSERT INTO [dbo].[TBL_USUARIOS] VALUES(@P_CEDULA, @P_NOMBRE, @P_APELLIDO , @P_NOMBRE_USUARIO );

--Retrieve all.

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_USUARIOS_PR]
AS
	SELECT * FROM TBL_USUARIOS;
RETURN 0

--Retrieve by user name

GO
CREATE PROCEDURE [dbo].[RET_USUARIO_PR]
	@P_NOMBRE_USUARIO VARCHAR(50)
AS
	SELECT * FROM TBL_USUARIOS WHERE NOMBRE_USUARIO=@P_NOMBRE_USUARIO;
RETURN 0




-- procedimientos almacenados de Idiomas

--Create

GO
CREATE PROCEDURE [dbo].[CRE_IDIOMA_PR]    
   (@P_NOMBRE_IDIOMA VARCHAR(50),
	@P_PAIS_ORIGEN VARCHAR(50))
AS
	INSERT INTO [dbo].[TBL_IDIOMAS] VALUES(@P_NOMBRE_IDIOMA, @P_PAIS_ORIGEN);

--Retrieve all.

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_IDIOMAS_PR]
AS
	SELECT * FROM TBL_IDIOMAS;
RETURN 0

--Retrieve by user name

GO
CREATE PROCEDURE [dbo].[RET_IDIOMA_PR]
	@P_NOMBRE_IDIOMA VARCHAR(50)
AS
	SELECT * FROM TBL_IDIOMAS WHERE NOMBRE_IDIOMA=@P_NOMBRE_IDIOMA;
RETURN 0




-- procedimientos almacenados de Palabras

--Create

GO
CREATE PROCEDURE [dbo].[CRE_PALABRA_PR]    
   (@P_PALABRA VARCHAR(50),
	@P_NOMBRE_IDIOMA VARCHAR(50),
	@P_PALABRA_PRIMER_REGISTRO VARCHAR(50),
	@P_TIPO VARCHAR(50))
AS
	INSERT INTO [dbo].[TBL_PALABRAS] VALUES(@P_PALABRA,@P_NOMBRE_IDIOMA,@P_PALABRA_PRIMER_REGISTRO,@P_TIPO );



GO
CREATE PROCEDURE [dbo].[CRE_PRIMERA_PALABRA_PR]    
   (@P_PALABRA_PRIMER_REGISTRO VARCHAR(50),
	@P_NOMBRE_IDIOMA VARCHAR(50),
	@P_TIPO VARCHAR(50))
AS
	INSERT INTO [dbo].[TBL_PRIMERA_PALABRA] VALUES(@P_PALABRA_PRIMER_REGISTRO, @P_NOMBRE_IDIOMA, @P_TIPO );

--Retrieve all.

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_PALABRAS_PR]
AS
	SELECT * FROM TBL_PALABRAS;
RETURN 0



GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_PRIMERAS_PALABRAS_PR]
AS
	select DISTINCT PALABRA_PRIMER_REGISTRO as PALABRA, NOMBRE_IDIOMA, PALABRA_PRIMER_REGISTRO, TIPO from TBL_PRIMERA_PALABRA
RETURN 0


--Retrieve by user name

GO
CREATE PROCEDURE [dbo].[RET_PALABRA_PR]
	@P_PALABRA VARCHAR(50)
AS
	SELECT * FROM TBL_PALABRAS WHERE PALABRA=@P_PALABRA;
RETURN 0



GO
CREATE PROCEDURE [dbo].[RET_PRIMERA_PALABRA_PR]
@P_PALABRA_PRIMER_REGISTRO VARCHAR(50)
AS
	select DISTINCT PALABRA_PRIMER_REGISTRO as PALABRA, NOMBRE_IDIOMA, PALABRA_PRIMER_REGISTRO, TIPO from TBL_PRIMERA_PALABRA
	where PALABRA_PRIMER_REGISTRO = @P_PALABRA_PRIMER_REGISTRO;
RETURN 0


--Retrieve by idioma and primeraPalabra

GO
CREATE PROCEDURE [dbo].[RET_PALABRA_EN_IDIOMA_PR]
(@P_PALABRA_PRIMER_REGISTRO VARCHAR(50),
@P_NOMBRE_IDIOMA VARCHAR(50))
AS
	select PALABRA, NOMBRE_IDIOMA, PALABRA_PRIMER_REGISTRO, TIPO FROM TBL_PALABRAS WHERE PALABRA_PRIMER_REGISTRO = @P_PALABRA_PRIMER_REGISTRO AND NOMBRE_IDIOMA= @P_NOMBRE_IDIOMA;
RETURN 0

-- procedimientos almacenados de consultasFrases

--Create

GO
CREATE PROCEDURE [dbo].[CRE_CONSULTA_FRASES_PR]    
   (@P_CODIGO_CONSULTA VARCHAR(50),
	@P_CEDULA INT,
	@P_FRASE VARCHAR(50),
	@P_TRADUCCION_ESPANOL VARCHAR(50),
	@P_CANTIDAD_PALABRAS INT,
	@P_FECHA_CONSULTA VARCHAR(50),
	@P_POPULARIDAD INT)
AS
	INSERT INTO [dbo].[TBL_CONSULTAS_FRASES] VALUES(@P_CODIGO_CONSULTA,@P_CEDULA,@P_FRASE,@P_TRADUCCION_ESPANOL,@P_CANTIDAD_PALABRAS,@P_FECHA_CONSULTA,@P_POPULARIDAD);

--Retrieve all.

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_CONSULTA_FRASES_PR]
AS
	SELECT * FROM TBL_CONSULTAS_FRASES;
RETURN 0


GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_CONSULTA_FRASES_CANTIDAD_PR]
AS
	select count(codigo_consulta)+1 as CONTADOR from TBL_CONSULTAS_FRASES
RETURN 0



--Retrieve by CODIGO

GO
CREATE PROCEDURE [dbo].[RET_CONSULTA_FRASES_PR]
	@P_CODIGO_CONSULTA VARCHAR(50) 
AS
	SELECT * FROM TBL_CONSULTAS_FRASES WHERE CODIGO_CONSULTA=@P_CODIGO_CONSULTA;
RETURN 0



-- procedimientos almacenados de consultas palabras


--Create

GO
CREATE PROCEDURE [dbo].[CRE_CONSULTA_PALABRA_PR]    
   (@P_CODIGO_REGISTRO INT,
	@P_CODIGO_CONSULTA VARCHAR(50),
	@P_PALABRA VARCHAR(50))
AS
	INSERT INTO [dbo].[TBL_PALABRAS_CONSULTADAS] VALUES(@P_CODIGO_REGISTRO, @P_CODIGO_CONSULTA, @P_PALABRA);

--Retrieve all.

GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RET_ALL_CONSULTAS_PALABRA_PR]
AS
	SELECT * FROM TBL_PALABRAS_CONSULTADAS;
RETURN 0

--Retrieve by CODIGO

GO
CREATE PROCEDURE [dbo].[RET_CONSULTA_PALABRA_PR]
	@P_CODIGO_REGISTRO VARCHAR(50) 
AS
	SELECT * FROM TBL_PALABRAS_CONSULTADAS WHERE CODIGO_REGISTRO=@P_CODIGO_REGISTRO;
RETURN 0


----------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------

																Definicion del problema



                                 DESCRIPCION

El motivo del proyecto es crear una aplicacion capaz de dar un servicio de traduccion.
Este servicio sera capaz de agregar palabras y definir cuales son los idiomas mas populares.

Descripcion del proyecto segun PDF subido al moodle.

===============================================================================

						INTERFAZ DE TRADUCCION (Application Console) 50pts 


1. Al iniciar, el programa debe saludar al usuario por su nombre y preguntarle a que idioma desea traducir hoy. a. Si el usuario ingresa un idioma que no existe en el sistema, el BOT deberá consultarle si desea agregar el idioma a la lista de idiomas disponibles. 

2. Una vez seleccionado el idioma, el BOT debe solicitarle al usuario que por favor le indique cual es la frase que desea traducir. 
	a. Pueden ser una o varias palabras. 
	b. Si la alguna de las palabras no existe en el diccionario de traducción, el BOT debe informar al usuario y consultarle si conoce la traducción de la palabra, esto con el objetivo de alimentar el diccionario de traducción del BOT en dicho idioma.  
3. Cuando el usuario haya brindado la frase el bot debe traducir la misma y mostrársela en pantalla en el idioma correspondiente. 
 
4. Una vez traducida la frase, debe preguntársele al usuario si desea traducir más frases, de ser así debe repetirse el ciclo de pasos necesarios para la traducción.

5. Debe llevarse un registro de todas las traducciones que se han realizado, almacenando: 
	a. Usuario 
	b. Fecha 
	c. Idioma 
	d. Frase en español
	e. Frase en idioma destino 
	f. Popularidad, esta se calcula a partir de la sumatoria de la cantidad de veces que han sido traducidas las palabras que forman parte de la frase traducida, por ejemplo “Buenos días profesor”, tendría una popularidad de 664, según la tabla mostrada abajo. 
 
PALABRA CANT TRADUCCIONES 
BUENOS 100 
DIAS  563 
PROFESOR 1 
 
 
						CONSULTA DE TRADUCCIONES (WEB API) 50 pts. 

6. Debe existir un servicio web, que permita a otros sistemas consultar: 
	a. Idiomas disponibles. 
	b. Idioma más popular. 
	c. 100 palabras más populares. 
	d. 10 palabras más populares por día de la semana. 
	e. Palabras disponibles, y en que idiomas, por ejemplo, la palabra “Hola” está disponible en 2 idiomas (alemán e inglés). 
	f. Diccionario completo de palabras según idioma solicitado. 
	g. Histórico de traducciones de una palabra, indicando fecha, usuario, idioma, entre otros. 
	h. Usuario con más traducciones.

===============================================================================



