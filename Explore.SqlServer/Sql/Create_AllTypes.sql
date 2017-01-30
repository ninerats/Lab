USE [NSGWE]
GO

/****** Object:  Table [dbo].[AllTypes]    Script Date: 11/15/2015 9:08:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AllTypes](
	[ID] [int] NOT NULL,
	[Mybigint] [bigint] NULL,
	[Mybinary256] [binary](256) NULL,
	[Mybit] [bit] NULL,
	[Mychar10] [char](10) NULL,
	[Mydate] [date] NULL,
	[Mydatetime] [datetime] NULL,
	[Mydatetime2] [datetime2](7) NULL,
	[Mydatetimeoffset] [datetimeoffset](7) NULL,
	[Mydecimal18_0] [decimal](18, 0) NULL,
	[Mydecimal4_4] [decimal](4, 4) NULL,
	[Myfloat] [float] NULL,
	[Mygeography] [geography] NULL,
	[Mygeometry] [geometry] NULL,
	[Myhierarchyid] [hierarchyid] NULL,
	[Myimage] [image] NULL,
	[Myint] [int] NULL,
	[Mymoney] [money] NULL,
	[Mynchar10] [nchar](10) NULL,
	[Myntext] [ntext] NULL,
	[Mynumeric] [numeric](18, 0) NULL,
	[Mynvarchar50] [nvarchar](50) NULL,
	[Myreal] [real] NULL,
	[Mysmalldatetime] [smalldatetime] NULL,
	[Mysmallint] [smallint] NULL,
	[Mysmallmoney] [smallmoney] NULL,
	[Mysql_variant] [sql_variant] NULL,
	[Mytext] [text] NULL,
	[Mytime7] [time](7) NULL,
	[Mytimestamp] [timestamp] NULL,
	[Mytinyint] [tinyint] NULL,
	[Myuniqueidentifier] [uniqueidentifier] NULL,
	[Myvarbinary50] [varbinary](50) NULL,
	[Myvarchar50] [varchar](50) NULL,
	[Myxml] [xml] NULL,
	[MyFlagbit] [dbo].[Flag] NULL,
	[MyNameStylebit] [dbo].[NameStyle] NULL,
	[MyNamenvarchar50] [dbo].[Name] NULL,
 CONSTRAINT [PK_AllTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


