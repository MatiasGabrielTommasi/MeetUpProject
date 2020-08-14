USE [master]
GO
/****** Object:  Database [MeetUp]    Script Date: 13/8/2020 20:34:20 ******/
CREATE DATABASE [MeetUp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeetUp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MeetUp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MeetUp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MeetUp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MeetUp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeetUp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeetUp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeetUp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeetUp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeetUp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeetUp] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeetUp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MeetUp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeetUp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeetUp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeetUp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeetUp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeetUp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeetUp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeetUp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeetUp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MeetUp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeetUp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeetUp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeetUp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeetUp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeetUp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeetUp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeetUp] SET RECOVERY FULL 
GO
ALTER DATABASE [MeetUp] SET  MULTI_USER 
GO
ALTER DATABASE [MeetUp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeetUp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeetUp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeetUp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MeetUp] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MeetUp', N'ON'
GO
ALTER DATABASE [MeetUp] SET QUERY_STORE = OFF
GO
USE [MeetUp]
GO
/****** Object:  Table [dbo].[Componente]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Componente](
	[id_componente] [int] IDENTITY(1,1) NOT NULL,
	[id_componente_padre] [int] NOT NULL,
	[id_tipo_componente] [int] NOT NULL,
	[componente] [varchar](150) NOT NULL,
	[url_componente] [varchar](150) NOT NULL,
	[habilitar_componente] [bit] NOT NULL,
	[mostrar_componente] [bit] NOT NULL,
	[detalle_componente] [varchar](20) NOT NULL,
	[icono_componente] [varchar](50) NULL,
 CONSTRAINT [PK_Componente] PRIMARY KEY CLUSTERED 
(
	[id_componente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComponentePerfil]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentePerfil](
	[id_componente] [int] NOT NULL,
	[id_perfil] [int] NOT NULL,
 CONSTRAINT [PK_ComponentePerfil] PRIMARY KEY CLUSTERED 
(
	[id_componente] ASC,
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[id_evento] [int] IDENTITY(1,1) NOT NULL,
	[id_sala] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[evento] [varchar](50) NOT NULL,
	[fecha_evento] [datetime] NOT NULL,
	[todal_asistentes] [int] NOT NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[id_evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil](
	[id_perfil] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[perfil] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfilUsuario]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerfilUsuario](
	[id_perfil] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
 CONSTRAINT [PK_PerfilUsuario] PRIMARY KEY CLUSTERED 
(
	[id_perfil] ASC,
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[id_reserva] [int] IDENTITY(1,1) NOT NULL,
	[id_evento] [int] NOT NULL,
	[fecha_reserva] [datetime] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[qr] [varchar](max) NULL,
	[consumo] [numeric](2, 2) NOT NULL,
	[consumo_finalizado] [bit] NOT NULL,
 CONSTRAINT [PK_Reserva] PRIMARY KEY CLUSTERED 
(
	[id_reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sala]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sala](
	[id_sala] [int] IDENTITY(1,1) NOT NULL,
	[sala] [varchar](50) NOT NULL,
	[cupo] [int] NOT NULL,
	[ubicacion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Sala] PRIMARY KEY CLUSTERED 
(
	[id_sala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoComponente]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoComponente](
	[id_tipo_componente] [int] IDENTITY(1,1) NOT NULL,
	[tipo_componente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoComponente] PRIMARY KEY CLUSTERED 
(
	[id_tipo_componente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDocumento]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDocumento](
	[id_tipo_documento] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[tipo_documento] [varchar](20) NOT NULL,
 CONSTRAINT [PK_TipoDocumento] PRIMARY KEY CLUSTERED 
(
	[id_tipo_documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 13/8/2020 20:34:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[usuario] [varchar](40) NOT NULL,
	[contrasenia] [varchar](max) NOT NULL,
	[fecha_usuario] [datetime] NOT NULL,
	[correo_usuario] [varchar](100) NOT NULL,
	[nombre] [varchar](150) NOT NULL,
	[apellido] [varchar](150) NOT NULL,
	[id_tipo_documento] [int] NOT NULL,
	[numero_documento] [varchar](12) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Componente] ON 

INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (6, 0, 4, N'Menú Vertical', N' ', 1, 1, N'V', N' ')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (7, 0, 7, N'Paramétricas', N'Parametricas.aspx', 1, 1, N'Pagina web', N'ui-2_settings-90')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (10, 6, 2, N'Eventos', N'Eventos.aspx', 1, 1, N'Boton de eventos', N'ui-1_calendar-60')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (11, 6, 2, N'Reservas', N'Reservas.aspx', 1, 1, N'Boton de Reservas', N'travel_info')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (12, 6, 2, N'Salas', N'Salas.aspx', 1, 1, N'Botón de Salas', N'ui-1_lock-circle-open')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (13, 6, 2, N'Mi Cuenta', N'MiCuenta.aspx', 1, 1, N'Botón del Perfil de ', N'emoticons_satisfied')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (14, 6, 2, N'Usuarios', N'Usuarios.aspx', 1, 1, N'Botón Admin de usuar', N'business_badge')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (15, 6, 2, N'Paramétricas', N'Parametricas.aspx', 1, 1, N'Botón de Paramétrica', N'ui-2_settings-90')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (16, 0, 7, N'Salas', N'Salas.aspx', 1, 1, N'Pagina web', N'ui-1_lock-circle-open')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (17, 0, 7, N'Eventos', N'Eventos.aspx', 1, 1, N'Pagina web', N'ui-1_calendar-60')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (18, 0, 7, N'Reservas', N'Reservas.aspx', 1, 1, N'Pagina web', N'travel_info')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (19, 7, 3, N'Panel Perfiles', N'', 1, 1, N'pnlPerfiles', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (20, 7, 3, N'Panel Componentes', N'', 1, 1, N'pnlComponentes', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (21, 7, 3, N'Panel Tipo Componente', N'', 1, 1, N'pnlTiposComponentes', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (22, 7, 3, N'Panel Tipos Documentos', N'', 1, 1, N'pnlTiposDocumentos', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (23, 16, 3, N'Panel Salas', N'', 1, 1, N'pnlSalas', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (24, 17, 3, N'Panel Eventos', N'', 1, 1, N'pnlEventos', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (25, 18, 3, N'Panel Reservas', N'', 1, 1, N'pnlReservas', N'')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (26, 0, 7, N'Usuarios', N'Usuarios.aspx', 1, 1, N'Pagina de Usuarios', N'business_badge')
INSERT [dbo].[Componente] ([id_componente], [id_componente_padre], [id_tipo_componente], [componente], [url_componente], [habilitar_componente], [mostrar_componente], [detalle_componente], [icono_componente]) VALUES (27, 26, 3, N'Panel Usuarios', N'', 1, 1, N'pnlUsuarios', N'')
SET IDENTITY_INSERT [dbo].[Componente] OFF
GO
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (6, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (7, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (10, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (11, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (12, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (13, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (14, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (15, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (16, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (17, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (18, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (19, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (20, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (21, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (22, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (23, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (24, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (25, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (26, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (27, 2)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (6, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (10, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (11, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (12, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (13, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (16, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (17, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (18, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (23, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (24, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (25, 3)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (6, 4)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (11, 4)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (13, 4)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (18, 4)
INSERT [dbo].[ComponentePerfil] ([id_componente], [id_perfil]) VALUES (25, 4)
GO
SET IDENTITY_INSERT [dbo].[Evento] ON 

INSERT [dbo].[Evento] ([id_evento], [id_sala], [id_usuario], [evento], [fecha_evento], [todal_asistentes]) VALUES (2, 2, 2, N'Festejamos los logros del año!', CAST(N'2020-08-26T21:08:41.907' AS DateTime), 15)
SET IDENTITY_INSERT [dbo].[Evento] OFF
GO
SET IDENTITY_INSERT [dbo].[Perfil] ON 

INSERT [dbo].[Perfil] ([id_perfil], [perfil]) VALUES (2, N'System Admin')
INSERT [dbo].[Perfil] ([id_perfil], [perfil]) VALUES (3, N'Anfitrión')
INSERT [dbo].[Perfil] ([id_perfil], [perfil]) VALUES (4, N'Invitado')
SET IDENTITY_INSERT [dbo].[Perfil] OFF
GO
INSERT [dbo].[PerfilUsuario] ([id_perfil], [id_usuario]) VALUES (2, 2)
INSERT [dbo].[PerfilUsuario] ([id_perfil], [id_usuario]) VALUES (4, 3)
INSERT [dbo].[PerfilUsuario] ([id_perfil], [id_usuario]) VALUES (3, 4)
GO
SET IDENTITY_INSERT [dbo].[Reserva] ON 

INSERT [dbo].[Reserva] ([id_reserva], [id_evento], [fecha_reserva], [id_usuario], [qr], [consumo], [consumo_finalizado]) VALUES (2, 2, CAST(N'2020-08-11T22:17:22.333' AS DateTime), 2, N'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qKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmopqKWgloJaCmqZ97T93KEznzt05gW1FNRSUEtBLQW1FNRymTa4TBsU1FJQS0EtBbUU1DLvafuCWgpqKailoJaCWgpqmfmqXVwdp49YQS3znrYvqKWgloJaCmop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', CAST(0.00 AS Numeric(2, 2)), 0)
SET IDENTITY_INSERT [dbo].[Reserva] OFF
GO
SET IDENTITY_INSERT [dbo].[Sala] ON 

INSERT [dbo].[Sala] ([id_sala], [sala], [cupo], [ubicacion]) VALUES (2, N'Sala Arcoiris', 20, N'6to Pito of. 23')
INSERT [dbo].[Sala] ([id_sala], [sala], [cupo], [ubicacion]) VALUES (3, N'Sala .NET', 35, N'5to Piso of. 12')
INSERT [dbo].[Sala] ([id_sala], [sala], [cupo], [ubicacion]) VALUES (4, N'Sala Android', 24, N'7mo Piso of. 7')
SET IDENTITY_INSERT [dbo].[Sala] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoComponente] ON 

INSERT [dbo].[TipoComponente] ([id_tipo_componente], [tipo_componente]) VALUES (2, N'Boton')
INSERT [dbo].[TipoComponente] ([id_tipo_componente], [tipo_componente]) VALUES (3, N'Panel')
INSERT [dbo].[TipoComponente] ([id_tipo_componente], [tipo_componente]) VALUES (4, N'Menú')
INSERT [dbo].[TipoComponente] ([id_tipo_componente], [tipo_componente]) VALUES (5, N'Grilla')
INSERT [dbo].[TipoComponente] ([id_tipo_componente], [tipo_componente]) VALUES (7, N'Pagina')
INSERT [dbo].[TipoComponente] ([id_tipo_componente], [tipo_componente]) VALUES (8, N'Accion')
SET IDENTITY_INSERT [dbo].[TipoComponente] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoDocumento] ON 

INSERT [dbo].[TipoDocumento] ([id_tipo_documento], [tipo_documento]) VALUES (1, N'DNI')
INSERT [dbo].[TipoDocumento] ([id_tipo_documento], [tipo_documento]) VALUES (2, N'Documento Extranjero')
INSERT [dbo].[TipoDocumento] ([id_tipo_documento], [tipo_documento]) VALUES (3, N'Pasaporte')
INSERT [dbo].[TipoDocumento] ([id_tipo_documento], [tipo_documento]) VALUES (4, N'Libreta de Enrolamie')
INSERT [dbo].[TipoDocumento] ([id_tipo_documento], [tipo_documento]) VALUES (5, N'Acta de Nacimiento')
SET IDENTITY_INSERT [dbo].[TipoDocumento] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id_usuario], [usuario], [contrasenia], [fecha_usuario], [correo_usuario], [nombre], [apellido], [id_tipo_documento], [numero_documento]) VALUES (2, N'Charango', N'hHMylv7X7ifcMtSz9YrBoQ==', CAST(N'2020-08-11T20:58:10.027' AS DateTime), N'matiasgabrieltommasi@gmail.com', N'Matías Gabriel', N'Tommasi', 1, N'36402762')
INSERT [dbo].[Usuario] ([id_usuario], [usuario], [contrasenia], [fecha_usuario], [correo_usuario], [nombre], [apellido], [id_tipo_documento], [numero_documento]) VALUES (3, N'yoniyoni', N'hHMylv7X7ifcMtSz9YrBoQ==', CAST(N'2020-08-13T12:20:16.933' AS DateTime), N'yonissajaquez@gmail.com', N'Yonissa', N'Jaquez', 1, N'95111323')
INSERT [dbo].[Usuario] ([id_usuario], [usuario], [contrasenia], [fecha_usuario], [correo_usuario], [nombre], [apellido], [id_tipo_documento], [numero_documento]) VALUES (4, N'test', N'hHMylv7X7ifcMtSz9YrBoQ==', CAST(N'2020-08-13T12:33:52.270' AS DateTime), N'test@test.com', N'test', N'test', 1, N'321321321')
INSERT [dbo].[Usuario] ([id_usuario], [usuario], [contrasenia], [fecha_usuario], [correo_usuario], [nombre], [apellido], [id_tipo_documento], [numero_documento]) VALUES (6, N'asd', N'hHMylv7X7ifcMtSz9YrBoQ==', CAST(N'2020-08-13T13:06:40.223' AS DateTime), N'asd@asd.com', N'asd', N'asd', 1, N'36363636')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
/****** Object:  Index [IXFK_Componente_Componente]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Componente_Componente] ON [dbo].[Componente]
(
	[id_componente_padre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_Componente_TipoComponente]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Componente_TipoComponente] ON [dbo].[Componente]
(
	[id_tipo_componente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_ComponentePerfil_Componente]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_ComponentePerfil_Componente] ON [dbo].[ComponentePerfil]
(
	[id_componente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_ComponentePerfil_Perfil]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_ComponentePerfil_Perfil] ON [dbo].[ComponentePerfil]
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_Evento_Sala]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Evento_Sala] ON [dbo].[Evento]
(
	[id_sala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_Evento_Usuario]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Evento_Usuario] ON [dbo].[Evento]
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_PerfilUsuario_Perfil]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_PerfilUsuario_Perfil] ON [dbo].[PerfilUsuario]
(
	[id_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_PerfilUsuario_Usuario]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_PerfilUsuario_Usuario] ON [dbo].[PerfilUsuario]
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_Reserva_Evento]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Reserva_Evento] ON [dbo].[Reserva]
(
	[id_evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_Reserva_Usuario]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Reserva_Usuario] ON [dbo].[Reserva]
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_Usuario_TipoDocumento]    Script Date: 13/8/2020 20:34:31 ******/
CREATE NONCLUSTERED INDEX [IXFK_Usuario_TipoDocumento] ON [dbo].[Usuario]
(
	[id_tipo_documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [DF__Reserva__fecha_r__286302EC]  DEFAULT (getdate()) FOR [fecha_reserva]
GO
ALTER TABLE [dbo].[Reserva] ADD  CONSTRAINT [DF__Reserva__consumo__29572725]  DEFAULT ((0)) FOR [consumo_finalizado]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [fecha_usuario]
GO
ALTER TABLE [dbo].[Componente]  WITH CHECK ADD  CONSTRAINT [FK_Componente_TipoComponente] FOREIGN KEY([id_tipo_componente])
REFERENCES [dbo].[TipoComponente] ([id_tipo_componente])
GO
ALTER TABLE [dbo].[Componente] CHECK CONSTRAINT [FK_Componente_TipoComponente]
GO
ALTER TABLE [dbo].[ComponentePerfil]  WITH CHECK ADD  CONSTRAINT [FK_ComponentePerfil_Componente] FOREIGN KEY([id_componente])
REFERENCES [dbo].[Componente] ([id_componente])
GO
ALTER TABLE [dbo].[ComponentePerfil] CHECK CONSTRAINT [FK_ComponentePerfil_Componente]
GO
ALTER TABLE [dbo].[ComponentePerfil]  WITH CHECK ADD  CONSTRAINT [FK_ComponentePerfil_Perfil] FOREIGN KEY([id_perfil])
REFERENCES [dbo].[Perfil] ([id_perfil])
GO
ALTER TABLE [dbo].[ComponentePerfil] CHECK CONSTRAINT [FK_ComponentePerfil_Perfil]
GO
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Evento_Sala] FOREIGN KEY([id_sala])
REFERENCES [dbo].[Sala] ([id_sala])
GO
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK_Evento_Sala]
GO
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Evento_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK_Evento_Usuario]
GO
ALTER TABLE [dbo].[PerfilUsuario]  WITH CHECK ADD  CONSTRAINT [FK_PerfilUsuario_Perfil] FOREIGN KEY([id_perfil])
REFERENCES [dbo].[Perfil] ([id_perfil])
GO
ALTER TABLE [dbo].[PerfilUsuario] CHECK CONSTRAINT [FK_PerfilUsuario_Perfil]
GO
ALTER TABLE [dbo].[PerfilUsuario]  WITH CHECK ADD  CONSTRAINT [FK_PerfilUsuario_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[PerfilUsuario] CHECK CONSTRAINT [FK_PerfilUsuario_Usuario]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Evento] FOREIGN KEY([id_evento])
REFERENCES [dbo].[Evento] ([id_evento])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Evento]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reserva_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_TipoDocumento] FOREIGN KEY([id_tipo_documento])
REFERENCES [dbo].[TipoDocumento] ([id_tipo_documento])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_TipoDocumento]
GO
/****** Object:  StoredProcedure [dbo].[Componente_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Componente_del]
(
@id_componente as int
)
AS
BEGIN
DELETE FROM Componente
WHERE id_componente = @id_componente
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Componente_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Componente_get]
(
@id_componente as int,
@id_componente_padre as int,
@id_tipo_componente as int
)
AS
BEGIN
SELECT * FROM Componente c
inner join TipoComponente tc on c.id_tipo_componente = tc.id_tipo_componente
WHERE
(id_componente = @id_componente or @id_componente = 0 or @id_componente is null) AND
(id_componente_padre = @id_componente_padre or @id_componente_padre = 0 or @id_componente_padre is null) AND
(c.id_tipo_componente = @id_tipo_componente or @id_tipo_componente = 0 or @id_tipo_componente is null)
END

GO
/****** Object:  StoredProcedure [dbo].[Componente_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Componente_ins]
(
@id_componente_padre as int,
@id_tipo_componente as int,
@componente as varchar(150),
@url_componente as varchar(150),
@habilitar_componente as bit,
@mostrar_componente as bit,
@detalle_componente as varchar(20),
@icono_componente as varchar(50)
)
AS
BEGIN
INSERT INTO Componente (id_componente_padre, id_tipo_componente, componente, url_componente, habilitar_componente, mostrar_componente, detalle_componente, icono_componente)
VALUES (@id_componente_padre, @id_tipo_componente, @componente, @url_componente, @habilitar_componente, @mostrar_componente, @detalle_componente, @icono_componente)
select @@identity as '@identity'
END
GO
/****** Object:  StoredProcedure [dbo].[Componente_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Componente_upt]
(
@id_componente as int,
@id_componente_padre as int,
@id_tipo_componente as int,
@componente as varchar(150),
@url_componente as varchar(150),
@habilitar_componente as bit,
@mostrar_componente as bit,
@detalle_componente as varchar(20),
@icono_componente as varchar(50)
)
AS
BEGIN
UPDATE Componente SET
id_componente_padre = @id_componente_padre, id_tipo_componente = @id_tipo_componente, componente = @componente, url_componente = @url_componente, habilitar_componente = @habilitar_componente, mostrar_componente = @mostrar_componente, detalle_componente = @detalle_componente, icono_componente = @icono_componente
WHERE id_componente = @id_componente
select @@rowcount as '@rowcount'
END
GO
/****** Object:  StoredProcedure [dbo].[ComponentePerfil_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ComponentePerfil_del]
(
@id_perfil as int
)
AS
BEGIN
DELETE FROM ComponentePerfil
WHERE id_perfil = @id_perfil
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[ComponentePerfil_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[ComponentePerfil_get]
(
@id_perfil as int
)
AS
BEGIN
SELECT * FROM ComponentePerfil cp
inner join Componente c on cp.id_componente = c.id_componente
inner join TipoComponente tc on c.id_tipo_componente = tc.id_tipo_componente
WHERE
cp.id_perfil = @id_perfil
END

GO
/****** Object:  StoredProcedure [dbo].[ComponentePerfil_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ComponentePerfil_ins]
(
@id_perfil as int,
@id_componente as int
)
AS
BEGIN
INSERT INTO ComponentePerfil (id_perfil, id_componente)
VALUES (@id_perfil, @id_componente)
select @@ROWCOUNT as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Evento_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Evento_del]
(
@id_evento as int
)
AS
BEGIN
DELETE FROM Evento
WHERE id_evento = @id_evento
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Evento_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Evento_get]
(
@id_evento as int,
@id_sala as int,
@id_usuario as int
)
AS
BEGIN
SELECT e.*, s.id_sala, s.sala, s.ubicacion, u.id_usuario, u.usuario, u.nombre, u.apellido, u.numero_documento FROM Evento e
inner join Sala s on e.id_sala = s.id_sala
inner join Usuario u on e.id_usuario = u.id_usuario
WHERE
(id_evento = @id_evento or @id_evento = 0 or @id_evento is null) AND
(e.id_sala = @id_sala or @id_sala = 0 or @id_sala is null) AND
(e.id_usuario = @id_usuario or @id_usuario = 0 or @id_usuario is null)
END

GO
/****** Object:  StoredProcedure [dbo].[Evento_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Evento_ins]
(
@id_sala as int,
@id_usuario as int,
@evento as varchar(50),
@fecha_evento as datetime,
@todal_asistentes as int
)
AS
BEGIN
INSERT INTO Evento (id_sala, id_usuario, evento, fecha_evento, todal_asistentes)
VALUES (@id_sala, @id_usuario, @evento, @fecha_evento, @todal_asistentes)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[Evento_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Evento_upt]
(
@id_evento as int,
@id_sala as int,
@id_usuario as int,
@evento as varchar(50),
@fecha_evento as datetime,
@todal_asistentes as int
)
AS
BEGIN
UPDATE Evento SET
id_sala = @id_sala, id_usuario = @id_usuario, evento = @evento, fecha_evento = @fecha_evento, todal_asistentes = @todal_asistentes
WHERE id_evento = @id_evento
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Perfil_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_del]
(
@id_perfil as int
)
AS
BEGIN
DELETE FROM Perfil
WHERE id_perfil = @id_perfil
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Perfil_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Perfil_get]
(
@id_perfil as int
)
AS
BEGIN
SELECT * FROM Perfil
WHERE
(id_perfil = @id_perfil or @id_perfil = 0 or @id_perfil is null)
END

GO
/****** Object:  StoredProcedure [dbo].[Perfil_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_ins]
(
@perfil as varchar(50)
)
AS
BEGIN
INSERT INTO Perfil (perfil)
VALUES (@perfil)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[Perfil_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Perfil_upt]
(
@id_perfil as int,
@perfil as varchar(50)
)
AS
BEGIN
UPDATE Perfil SET
perfil = @perfil
WHERE id_perfil = @id_perfil
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[PerfilUsuario_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PerfilUsuario_del]
(
@id_usuario as int
)
AS
BEGIN
DELETE FROM PerfilUsuario
WHERE id_usuario = @id_usuario
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[PerfilUsuario_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[PerfilUsuario_get]
(
@id_usuario as int
)
AS
BEGIN
SELECT * FROM PerfilUsuario pu
inner join Perfil p on pu.id_perfil = p.id_perfil
WHERE
id_usuario = @id_usuario
END

GO
/****** Object:  StoredProcedure [dbo].[PerfilUsuario_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PerfilUsuario_ins]
(
@id_perfil as int,
@id_usuario as int
)
AS
BEGIN
INSERT INTO PerfilUsuario (id_perfil, id_usuario)
VALUES (@id_perfil, @id_usuario)
select @@ROWCOUNT as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Reserva_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Reserva_del]
(
@id_reserva as int
)
AS
BEGIN
DELETE FROM Reserva
WHERE id_reserva = @id_reserva
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Reserva_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Reserva_get]
(
@id_reserva as int,
@id_evento as int,
@id_usuario as int
)
AS
BEGIN
SELECT * FROM Reserva r
inner join Evento e on r.id_evento = e.id_evento
inner join Sala s on e.id_sala = s.id_sala
inner join Usuario u on r.id_usuario = u.id_usuario
WHERE
(id_reserva = @id_reserva or @id_reserva = 0 or @id_reserva is null) AND
(r.id_evento = @id_evento or @id_evento = 0 or @id_evento is null) AND
(r.id_usuario = @id_usuario or @id_usuario = 0 or @id_usuario is null)
END

GO
/****** Object:  StoredProcedure [dbo].[Reserva_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Reserva_ins]
(
@id_evento as int,
@fecha_reserva as datetime,
@id_usuario as int,
@consumo as varchar(5),
@consumo_finalizado as bit
)
AS
BEGIN
INSERT INTO Reserva (id_evento, fecha_reserva, id_usuario, consumo, consumo_finalizado)
VALUES (@id_evento, @fecha_reserva, @id_usuario, @consumo, @consumo_finalizado)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[ReservaQR_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReservaQR_ins](
@id_reserva as int
,@qr as varchar(max)
)
as
begin
update Reserva set qr = @qr
where id_reserva = @id_reserva
end

GO
/****** Object:  StoredProcedure [dbo].[Sala_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sala_del]
(
@id_sala as int
)
AS
BEGIN
DELETE FROM Sala
WHERE id_sala = @id_sala
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Sala_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Sala_get]
(
@id_sala as int
)
AS
BEGIN
SELECT s.*,
(select count(id_evento) from Evento where CAST(fecha_evento as date) = CAST(getdate() as date) and id_sala = s.id_sala) eventos_hoy,
(select count(id_evento) from Evento where CAST(fecha_evento as date) < CAST(getdate() as date) and id_sala = s.id_sala) eventos_vencidos,
(select count(id_evento) from Evento where CAST(fecha_evento as date) > CAST(getdate() as date) and id_sala = s.id_sala) eventos_vigentes
FROM Sala s
WHERE
(id_sala = @id_sala or @id_sala = 0 or @id_sala is null)
END
GO
/****** Object:  StoredProcedure [dbo].[Sala_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sala_ins]
(
@sala as varchar(50),
@cupo as int,
@ubicacion as varchar(50)
)
AS
BEGIN
INSERT INTO Sala (sala, cupo, ubicacion)
VALUES (@sala, @cupo, @ubicacion)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[Sala_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sala_upt]
(
@id_sala as int,
@sala as varchar(50),
@cupo as int,
@ubicacion as varchar(50)
)
AS
BEGIN
UPDATE Sala SET
sala = @sala, cupo = @cupo, ubicacion = @ubicacion
WHERE id_sala = @id_sala
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[TipoComponente_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipoComponente_del]
(
@id_tipo_componente as int
)
AS
BEGIN
DELETE FROM TipoComponente
WHERE id_tipo_componente = @id_tipo_componente
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[TipoComponente_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[TipoComponente_get]
(
@id_tipo_componente as int
)
AS
BEGIN
SELECT * FROM TipoComponente
WHERE
(id_tipo_componente = @id_tipo_componente or @id_tipo_componente = 0 or @id_tipo_componente is null)
END

GO
/****** Object:  StoredProcedure [dbo].[TipoComponente_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipoComponente_ins]
(
@tipo_componente as varchar(50)
)
AS
BEGIN
INSERT INTO TipoComponente (tipo_componente)
VALUES (@tipo_componente)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[TipoComponente_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipoComponente_upt]
(
@id_tipo_componente as int,
@tipo_componente as varchar(50)
)
AS
BEGIN
UPDATE TipoComponente SET
tipo_componente = @tipo_componente
WHERE id_tipo_componente = @id_tipo_componente
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[TipoDocumento_del](
@id_tipo_documento as int
)
as
begin
	delete from TipoDocumento
	where id_tipo_documento = @id_tipo_documento
	select @@rowcount as '@rowcount'
end
GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[TipoDocumento_get]
(
@id_tipo_documento as int
)
AS
BEGIN
SELECT * FROM TipoDocumento
WHERE
(id_tipo_documento = @id_tipo_documento or @id_tipo_documento = 0 or @id_tipo_documento is null)
END

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipoDocumento_ins]
(
@tipo_documento as varchar(20)
)
AS
BEGIN
INSERT INTO TipoDocumento (tipo_documento)
VALUES (@tipo_documento)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[TipoDocumento_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipoDocumento_upt]
(
@id_tipo_documento as int,
@tipo_documento as varchar(20)
)
AS
BEGIN
UPDATE TipoDocumento SET
tipo_documento = @tipo_documento
WHERE id_tipo_documento = @id_tipo_documento
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Usuario_del]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuario_del]
(
@id_usuario as int
)
AS
BEGIN
DELETE FROM Usuario
WHERE id_usuario = @id_usuario
select @@rowcount as '@rowcount'
END

GO
/****** Object:  StoredProcedure [dbo].[Usuario_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Usuario_get]
(
@id_usuario as int,
@usuario as varchar(40),
@nombre as varchar(150),
@apellido as varchar(150)
)
AS
BEGIN
SELECT * FROM Usuario u
inner join TipoDocumento tc on u.id_tipo_documento = tc.id_tipo_documento
WHERE
(id_usuario = @id_usuario or @id_usuario = 0 or @id_usuario is null) AND
(usuario  like '%' + @usuario +'%' or @usuario = '' or @usuario is null) AND
(nombre like '%' + @nombre + '%' or @nombre = '' or @nombre is null) AND
(apellido  like '%' + @apellido + '%' or @apellido = '' or @apellido is null)
END

GO
/****** Object:  StoredProcedure [dbo].[Usuario_ins]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_ins]
(
@usuario as varchar(40),
@contrasenia as varchar(MAX),
@correo_usuario as varchar(100),
@nombre as varchar(150),
@apellido as varchar(150),
@id_tipo_documento as int,
@numero_documento as varchar(12)
)
AS
BEGIN
INSERT INTO Usuario (usuario, contrasenia, fecha_usuario, correo_usuario, nombre, apellido, id_tipo_documento, numero_documento)
VALUES (@usuario, @contrasenia, getdate(), @correo_usuario, @nombre, @apellido, @id_tipo_documento, @numero_documento)
select @@identity as '@identity'
END

GO
/****** Object:  StoredProcedure [dbo].[Usuario_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_upt]
(
@id_usuario as int,
@usuario as varchar(40),
@correo_usuario as varchar(100),
@nombre as varchar(150),
@apellido as varchar(150),
@id_tipo_documento as int,
@numero_documento as varchar(12)
)
AS
BEGIN
UPDATE Usuario SET
usuario = @usuario, correo_usuario = @correo_usuario, nombre = @nombre, apellido = @apellido, id_tipo_documento = @id_tipo_documento, numero_documento = @numero_documento
WHERE id_usuario = @id_usuario
select @@rowcount as '@rowcount'
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioContrasenia_upt]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UsuarioContrasenia_upt]
(
@id_usuario as int,
@contrasenia as varchar(MAX)
)
AS
BEGIN
UPDATE Usuario SET
contrasenia = @contrasenia
WHERE id_usuario = @id_usuario
select @@rowcount as '@rowcount'
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioLogin_get]    Script Date: 13/8/2020 20:34:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








create PROCEDURE [dbo].[UsuarioLogin_get]
(
@usuario as varchar(40)
)
AS
BEGIN
SELECT * FROM Usuario u
inner join TipoDocumento tc on u.id_tipo_documento = tc.id_tipo_documento
WHERE
usuario = @usuario
END
GO
USE [master]
GO
ALTER DATABASE [MeetUp] SET  READ_WRITE 
GO
