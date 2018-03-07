SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Users-test](
	[Id] [uniqueidentifier] NOT NULL,
	[BirthDate] [datetime2](7) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[DeactivatedOn] [datetime2](7) NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[DeletedOn] [datetime2](7) NULL,
	[FirstName] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[HasImage] [bit] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[IsDeactivated] [bit] NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[Position] [nvarchar](max) NULL,
	[TeamId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
	[Key] [nvarchar](450) NULL,
	[Language] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [Users-test] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO