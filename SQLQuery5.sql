USE [master]
GO
/****** Object:  Database [AplicacionCapas]    Script Date: 06/10/2022 19:28:33 ******/
CREATE DATABASE [AplicacionCapas]
go
USE [AplicacionCapas]
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 06/10/2022 19:28:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Usuario] [nvarchar](20) NOT NULL,
	[Contraseña] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Usuario] ([Usuario], [Contraseña]) VALUES (N'juan', N'pepito')
GO
/****** Object:  StoredProcedure [dbo].[NuevoUsuario]    Script Date: 06/10/2022 19:28:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		javier
-- Create date: 01/10/2022
-- Description:	este procedimeinto graba un nuevo 
-- =============================================
CREATE PROCEDURE  [dbo].[NuevoUsuario] 
	@usuario nvarchar(20),
	@contraseña nvarchar(20)
	AS
BEGIN
	insert into Usuario(usuario,Contraseña) 
	values(@usuario,@contraseña)


END
GO
/****** Object:  StoredProcedure [dbo].[Validausuario]    Script Date: 06/10/2022 19:28:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Validausuario]
	@usuario nvarchar(20),
	@contraseña nvarchar(20)
AS
BEGIN
	select usuario 
	from usuario
	where Usuario=@usuario and Contraseña=@contraseña

END
GO
USE [master]
GO
ALTER DATABASE [AplicacionCapas] SET  READ_WRITE 
GO
